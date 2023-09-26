// Decompiled with JetBrains decompiler
// Type: Robot.Common.UpdateViewAfterRobotStepEventArgs
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System;
using System.Collections.Generic;

namespace Robot.Common
{
  public class UpdateViewAfterRobotStepEventArgs : EventArgs
  {
    public string OwnerName;
    public Position RobotPosition;
    public Position NewRobotPosition;
    public int TotalEnergyChange;
    public List<Position> MovedFrom;
    public List<Position> MovedTo;
  }
}
