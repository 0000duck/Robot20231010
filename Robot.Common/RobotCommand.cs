// Decompiled with JetBrains decompiler
// Type: Robot.Common.RobotCommand
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System.Collections.Generic;

namespace Robot.Common
{
  public abstract class RobotCommand
  {
    public abstract UpdateViewAfterRobotStepEventArgs ChangeModel(
      IList<Robot> robots,
      int currentIndex,
      Map map);

    public void Apply(IList<Robot> robots, int currentIndex, Map map) => this.InvokeRobotStepCompleted(this.ChangeModel(robots, currentIndex, map));

    public event RobotStepCompletedEventHandler RobotStepCompleted;

    public void InvokeRobotStepCompleted(UpdateViewAfterRobotStepEventArgs e)
    {
      RobotStepCompletedEventHandler robotStepCompleted = this.RobotStepCompleted;
      if (robotStepCompleted == null)
        return;
      robotStepCompleted((object) this, e);
    }

    public string Description { get; set; }
  }
}
