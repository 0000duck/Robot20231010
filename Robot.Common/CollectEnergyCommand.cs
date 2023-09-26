// Decompiled with JetBrains decompiler
// Type: Robot.Common.CollectEnergyCommand
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System;
using System.Collections.Generic;

namespace Robot.Common
{
  public sealed class CollectEnergyCommand : RobotCommand
  {
    public override UpdateViewAfterRobotStepEventArgs ChangeModel(
      IList<Robot> robots,
      int currentIndex,
      Map map)
    {
      UpdateViewAfterRobotStepEventArgs robotStepEventArgs = new UpdateViewAfterRobotStepEventArgs();
      Robot robot = robots[currentIndex];
      List<EnergyStation> nearbyResources = map.GetNearbyResources(robot.Position, Variant.GetInstance().CollectingDistance);
      if (nearbyResources.Count == 0)
        this.Description = "FAILED: no resource to collect energy";
      foreach (EnergyStation energyStation in nearbyResources)
      {
        if (energyStation != null)
        {
          int num = Math.Min(energyStation.Energy, Variant.GetInstance().MaxEnergyCanCollect);
          robot.Energy += num;
          energyStation.Energy -= num;
          robotStepEventArgs.TotalEnergyChange = num;
          this.Description = string.Format("COLLECT: {0}", (object) num);
        }
        else
          this.Description = "ERROR: station is null";
      }
      return robotStepEventArgs;
    }
  }
}
