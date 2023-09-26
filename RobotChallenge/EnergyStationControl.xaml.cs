// Decompiled with JetBrains decompiler
// Type: RobotChallenge.EnergyStationControl
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace RobotChallenge
{
  public partial class EnergyStationControl : UserControl, IComponentConnector
  {
    private double _left;
    private double _top;
    private bool _contentLoaded;

    public EnergyStationControl() => this.InitializeComponent();

    public double Left
    {
      get => this._left;
      set
      {
        this._left = value;
        Canvas.SetLeft((UIElement) this, this._left);
      }
    }

    public double Top
    {
      get => this._top;
      set
      {
        this._top = value;
        Canvas.SetTop((UIElement) this, this._top);
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RobotChallenge;component/energystationcontrol.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
