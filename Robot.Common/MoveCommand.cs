// Decompiled with JetBrains decompiler
// Type: MoveCommand
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\ dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.Common
{
  public sealed class MoveCommand : RobotCommand
  {
    public Position NewPosition { get; set; }

    private int Min2D(int x1, int x2) => ((IEnumerable<int>) new int[3]
    {
      (int) Math.Pow((double) (x1 - x2), 2.0),
      (int) Math.Pow((double) (x1 - x2 + 100), 2.0),
      (int) Math.Pow((double) (x1 - x2 - 100), 2.0)
    }).Min();

    private int CalculateLoss(Position p1, Position p2) => this.Min2D(p1.X, p2.X) + this.Min2D(p1.Y, p2.Y);

    public override UpdateViewAfterRobotStepEventArgs ChangeModel(
      IList<Robot> robots,
      int currentIndex,
      Map map)
    {
      UpdateViewAfterRobotStepEventArgs robotStepEventArgs = new UpdateViewAfterRobotStepEventArgs();
      if (!map.IsValid(this.NewPosition))
      {
        this.Description = string.Format("FAILED: {0} position not valid ", (object) this.NewPosition);
        return robotStepEventArgs;
      }
      Robot robot1 = robots[currentIndex];
      Position position1 = robots[currentIndex].Position;
      int loss = this.CalculateLoss(this.NewPosition, position1);
      Robot robot2 = ( Robot) null;
      foreach ( Robot robot3 in (IEnumerable< Robot>) robots)
      {
        if (robot3 != robot1 && robot3.Position == this.NewPosition)
        {
          loss += Variant.GetInstance().AttackEnergyLoss;
          robot2 = robot3;
        }
      }
      if (robot1.Energy < loss)
      {
        this.Description = "FAILED: not enough energy to move";
        return new UpdateViewAfterRobotStepEventArgs();
      }
      robotStepEventArgs.TotalEnergyChange = -loss;
      robotStepEventArgs.MovedFrom = new List<Position>()
      {
        position1
      };
      robotStepEventArgs.MovedTo = new List<Position>()
      {
        this.NewPosition
      };
      robot1.Energy -= loss;
      robot1.Position = this.NewPosition;
      this.Description = string.Format("MOVE: {0}-> {1}", (object) position1, (object) this.NewPosition);
      if (robot2 != null)
      {
        int x = 2 * this.NewPosition.X - position1.X;
        int y = 2 * this.NewPosition.Y - position1.Y;
        Position position2 = robot2.Position;
        Position freeCell = map.FindFreeCell(new Position(x, y), robots);
        robot2.Position = freeCell;
        double rateEnergyAtAttack = Variant.GetInstance().StoleRateEnergyAtAttack;
        robot1.Energy += (int) ((double) robot2.Energy * rateEnergyAtAttack);
        robot2.Energy -= (int) ((double) robot2.Energy * rateEnergyAtAttack);
        robotStepEventArgs.MovedFrom.Insert(0, position2);
        robotStepEventArgs.MovedTo.Insert(0, robot2.Position);
        this.Description = string.Format("Attacked {0} robot at ({1})", (object) robot2.OwnerName, (object) this.NewPosition);
      }
      return robotStepEventArgs;
    }
  }
}
