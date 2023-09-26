// Decompiled with JetBrains decompiler
// Type: Robot.Tournament.RobotStepCompletedArgs
// Assembly: Robot.Tournament, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 62B4B0F1-8974-46DC-9DC4-634CEB6178F4
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Tournament.dll

using System;

namespace Robot.Tournament
{
  public class RobotStepCompletedArgs : EventArgs
  {
    public Robot.Common.Robot Robot;
    public RobotStepHistory History;
  }
}
