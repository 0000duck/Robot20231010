// Decompiled with JetBrains decompiler
// Type: Robot.Tournament.Runner
// Assembly: Robot.Tournament, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 62B4B0F1-8974-46DC-9DC4-634CEB6178F4
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Tournament.dll

using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.Tournament
{
  public class Runner
  {
    public const int InitialRobotsCount = 10;
    public List<Owner> Owners;
    public Dictionary<string, IRobotAlgorithm> Algorithms = new Dictionary<string, IRobotAlgorithm>();
    private int _roundNumber;
    private RobotStepCompletedEventHandler _callback;
    private int _currentRobotIndex;

    public Map Map { get; set; }

    public IList<Robot.Common.Robot> Robots { get; set; }

    public Runner(Map map, RobotStepCompletedEventHandler callback)
    {
      this.Map = map;
      this._callback = callback;
      this.InitializeOwnersAndRobots(50);
    }

    private OwnerStatistics CalculateOwnerStatistics(string ownerName)
    {
      OwnerStatistics ownerStatistics = new OwnerStatistics();
      foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>) this.Robots)
      {
        if (!(robot.OwnerName != ownerName))
        {
          ++ownerStatistics.RobotsCount;
          ownerStatistics.TotalEnergy += robot.Energy;
        }
      }
      return ownerStatistics;
    }

    public List<OwnerStatistics> CalculateOwnerStatistics()
    {
      Dictionary<string, OwnerStatistics> dictionary = new Dictionary<string, OwnerStatistics>();
      foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>) this.Robots)
      {
        if (!dictionary.ContainsKey(robot.OwnerName))
          dictionary.Add(robot.OwnerName, this.CalculateOwnerStatistics(robot.OwnerName));
      }
      List<OwnerStatistics> ownerStatistics1 = new List<OwnerStatistics>();
      foreach (string key in dictionary.Keys)
      {
        OwnerStatistics ownerStatistics2 = dictionary[key];
        ownerStatistics2.Owner = key;
        ownerStatistics1.Add(ownerStatistics2);
      }
      ownerStatistics1.Sort((Comparison<OwnerStatistics>) ((a, b) => string.Compare(a.Owner, b.Owner, StringComparison.Ordinal)));
      return ownerStatistics1;
    }

    public IList<Robot.Common.Robot> CopyRopots()
    {
      Dictionary<Owner, Owner> dictionary = new Dictionary<Owner, Owner>();
      List<Robot.Common.Robot> robotList = new List<Robot.Common.Robot>();
      foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>) this.Robots)
        robotList.Add(new Robot.Common.Robot()
        {
          Energy = robot.Energy,
          Position = robot.Position.Copy(),
          OwnerName = robot.OwnerName
        });
      return (IList<Robot.Common.Robot>) robotList;
    }

    public void UpdateResources()
    {
      foreach (EnergyStation station in (IEnumerable<EnergyStation>) this.Map.Stations)
      {
        station.Energy += station.RecoveryRate;
        station.Energy = Math.Min(station.Energy, Robot.Common.Variant.GetInstance().MaxStationEnergy);
      }
    }

    public int MaxNumbersOfRound { set; get; }

    public void InitializeOwnersAndRobots(int maxNumbersOfRound)
    {
      this.MaxNumbersOfRound = maxNumbersOfRound;
      this._roundNumber = 0;
      List<IRobotAlgorithm> source1 = ReflectionScanner.Scan();
      foreach (IRobotAlgorithm robotAlgorithm in source1)
      {
        if (this.Algorithms.ContainsKey(robotAlgorithm.Author))
          throw new Exception("At least 2 libraries of ssame author: " + robotAlgorithm.Author + ". Tournament terminated.");
        this.Algorithms.Add(robotAlgorithm.Author, robotAlgorithm);
      }
      this.Robots = (IList<Robot.Common.Robot>) new List<Robot.Common.Robot>();
      List<Owner> list = source1.Select<IRobotAlgorithm, Owner>((Func<IRobotAlgorithm, Owner>) (algorithm => new Owner()
      {
        Name = algorithm.Author
      })).ToList<Owner>();
      Random random = new Random();
      this.Owners = new List<Owner>();
      while (list.Count > 0)
      {
        int index = random.Next(list.Count);
        this.Owners.Add(list[index]);
        list.RemoveAt(index);
      }
      List<Position> source2 = new List<Position>();
      while (source2.Count < this.Owners.Count)
      {
        Position newPosition = new Position()
        {
          X = random.Next(this.Map.MaxPozition.X),
          Y = random.Next(this.Map.MaxPozition.Y)
        };
        if (source2.FirstOrDefault<Position>((Func<Position, bool>) (pos => Math.Abs(newPosition.X - pos.X) < 10 && Math.Abs(newPosition.Y - pos.Y) < 10)) == (Position) null)
          source2.Add(newPosition);
      }
      for (int index1 = 0; index1 < 10; ++index1)
      {
        for (int index2 = 0; index2 < this.Owners.Count; ++index2)
        {
          Position position;
          do
          {
            position = new Position()
            {
              X = source2[index2].X - 20 + random.Next(41),
              Y = source2[index2].Y - 20 + random.Next(41)
            };
          }
          while (!this.Map.IsValid(position) || this.Robots.Any<Robot.Common.Robot>((Func<Robot.Common.Robot, bool>) (rob => rob.Position == position)));
          this.Robots.Add(new Robot.Common.Robot()
          {
            Energy = 100,
            OwnerName = this.Owners[index2].Name,
            Position = position
          });
        }
      }
    }

    public void DoStep()
    {
      UpdateViewAfterRobotStepEventArgs e = new UpdateViewAfterRobotStepEventArgs();
      if (this._roundNumber > this.MaxNumbersOfRound)
        return;
      if (this._currentRobotIndex >= this.Robots.Count)
        this.PrepareNextRound();
      e.OwnerName = this.Robots[this._currentRobotIndex].OwnerName;
      string ownerName = e.OwnerName;
      e.RobotPosition = this.Robots[this._currentRobotIndex].Position;
      if (this.Robots[this._currentRobotIndex].Energy > 0)
      {
        try
        {
          RobotCommand robotCommand = this.Algorithms[e.OwnerName].DoStep(this.CopyRopots(), this._currentRobotIndex, this.Map.Copy());
          if (new List<Type>()
          {
            typeof (CreateNewRobotCommand),
            typeof (MoveCommand),
            typeof (CollectEnergyCommand)
          }.Contains(robotCommand.GetType()))
          {
            e = robotCommand.ChangeModel(this.Robots, this._currentRobotIndex, this.Map);
            e.OwnerName = ownerName;
            e.RobotPosition = this.Robots[this._currentRobotIndex].Position;
            Logger.LogMessage(ownerName, robotCommand.Description);
          }
          else
          {
            Logger.LogMessage(ownerName, this.Robots[this._currentRobotIndex].OwnerName + " is nasty cheater, let's kill his robot for that ))", LogValue.High);
            this.Robots[this._currentRobotIndex].Energy = 0;
          }
        }
        catch (Exception ex)
        {
          Logger.LogMessage(ownerName, "Error: " + ex.Message + " ", LogValue.Error);
          this._callback((object) null, e);
        }
      }
      ++this._currentRobotIndex;
      this._callback((object) null, e);
    }

    public void PrepareNextRound()
    {
      this._currentRobotIndex = 0;
      ++this._roundNumber;
      Logger.LogRound(this._roundNumber);
      this.UpdateResources();
    }
  }
}
