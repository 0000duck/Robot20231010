// Decompiled with JetBrains decompiler
// Type: RobotChallenge.ColorsFactory
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace RobotChallenge
{
  public class ColorsFactory
  {
    public static Dictionary<string, System.Windows.Media.Color> OwnerColors;

    public static void Initialize(List<Owner> owners)
    {
      List<System.Windows.Media.Color> list = ((IEnumerable<System.Drawing.Color>) new System.Drawing.Color[15]
      {
        System.Drawing.Color.FromArgb(30, 144, (int) byte.MaxValue),
        System.Drawing.Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue),
        System.Drawing.Color.FromArgb(50, 205, 50),
        System.Drawing.Color.FromArgb(107, 142, 35),
        System.Drawing.Color.FromArgb(188, 143, 143),
        System.Drawing.Color.FromArgb(240, 128, 128),
        System.Drawing.Color.FromArgb(178, 34, 34),
        System.Drawing.Color.FromArgb(218, 165, 32),
        System.Drawing.Color.FromArgb(184, 134, 11),
        System.Drawing.Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0),
        System.Drawing.Color.FromArgb((int) byte.MaxValue, 0, 0),
        System.Drawing.Color.FromArgb(128, 0, 0),
        System.Drawing.Color.FromArgb(192, 192, 192),
        System.Drawing.Color.FromArgb(0, 0, 0),
        System.Drawing.Color.FromArgb(0, (int) byte.MaxValue, 0)
      }).Select<System.Drawing.Color, System.Windows.Media.Color>((Func<System.Drawing.Color, System.Windows.Media.Color>) (color => new System.Windows.Media.Color()
      {
        A = color.A,
        B = color.B,
        G = color.G,
        R = color.R
      })).ToList<System.Windows.Media.Color>();
      list.Add(Colors.DarkRed);
      list.Add(Colors.HotPink);
      list.Add(Colors.CadetBlue);
      list.Add(Colors.DarkKhaki);
      int index = 0;
      ColorsFactory.OwnerColors = new Dictionary<string, System.Windows.Media.Color>();
      foreach (Owner owner in owners)
      {
        ColorsFactory.OwnerColors.Add(owner.Name, list[index]);
        ++index;
      }
    }
  }
}
