// Decompiled with JetBrains decompiler
// Type: Robot.Tournament.RobotStepHistory
// Assembly: Robot.Tournament, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 62B4B0F1-8974-46DC-9DC4-634CEB6178F4
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Tournament.dll

using Robot.Common;

namespace Robot.Tournament
{
  public class RobotStepHistory
  {
    public bool IsAlive { get; set; }

    public Position OldPozition { get; set; }

    public Position NewPozition { get; set; }
  }
}
