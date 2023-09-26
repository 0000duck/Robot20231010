// Decompiled with JetBrains decompiler
// Type: RobotChallenge.OwnerLegend
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using Robot.Tournament;
using System.Windows.Media;

namespace RobotChallenge
{
  public class OwnerLegend : OwnerStatistics
  {
    public SolidColorBrush Color { get; set; }

    public string Name { get; set; }

    public OwnerLegend(OwnerStatistics stat)
    {
      this.TotalEnergy = stat.TotalEnergy;
      this.RobotsCount = stat.RobotsCount;
      this.Name = stat.Owner;
      this.Color = new SolidColorBrush(ColorsFactory.OwnerColors[stat.Owner]);
    }
  }
}
