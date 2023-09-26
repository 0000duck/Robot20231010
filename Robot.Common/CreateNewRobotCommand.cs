// Decompiled with JetBrains decompiler
// Type:   CreateNewRobotCommand
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\  dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.Common
{
  public sealed class CreateNewRobotCommand : RobotCommand
  {
    public int NewRobotEnergy { get; set; } = 100;

    public override UpdateViewAfterRobotStepEventArgs ChangeModel(
      IList<  Robot> robots,
      int currentIndex,
      Map map)
    {
      UpdateViewAfterRobotStepEventArgs robotStepEventArgs = new UpdateViewAfterRobotStepEventArgs();
        Robot myRobot = robots[currentIndex];
      int toCreateNewRobot = Variant.GetInstance().EnergyLossToCreateNewRobot;
      int num = toCreateNewRobot + this.NewRobotEnergy;
      if (this.NewRobotEnergy <= 0 || this.NewRobotEnergy > 1073741823)
        this.Description = "FAILED: illegal value for new robot energy of " + myRobot.OwnerName + " .";
      else if (robots.Count<  Robot>((Func<  Robot, bool>) (r => r.OwnerName == myRobot.OwnerName)) >= 100)
        this.Description = "FAILED: number of " + myRobot.OwnerName + " robots reached 100.";
      else if (myRobot.Energy > num)
      {
        Position freeCell = map.FindFreeCell(myRobot.Position, robots);
          Robot robot = new   Robot()
        {
          Position = freeCell,
          Energy = this.NewRobotEnergy,
          OwnerName = myRobot.OwnerName
        };
        robots.Add(robot);
        myRobot.Energy -= num;
        robotStepEventArgs.NewRobotPosition = freeCell;
        robotStepEventArgs.TotalEnergyChange = -toCreateNewRobot;
        this.Description = string.Format("New: {0}", (object) robotStepEventArgs.NewRobotPosition);
      }
      else
        this.Description = "FAILED: not enough energy to create new robot";
      return robotStepEventArgs;
    }
  }
}
