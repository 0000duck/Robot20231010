// Decompiled with JetBrains decompiler
// Type: RobotChallenge.RobotControl
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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RobotChallenge
{
  public partial class RobotControl : UserControl, IComponentConnector
  {
    private double _left;
    private double _top;
    internal Ellipse BackgroundPanel;
    private bool _contentLoaded;

    public RobotControl()
    {
      this.InitializeComponent();
      this.SetValue(Panel.ZIndexProperty, (object) 1);
    }

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

    public Storyboard AnimateOpacity()
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(0.0);
      doubleAnimation.To = new double?(100.0);
      doubleAnimation.Duration = (Duration) TimeSpan.FromMilliseconds((double) (MainWindow.Speed / 6));
      doubleAnimation.AutoReverse = true;
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath((object) Brush.OpacityProperty));
      Storyboard.SetTarget((DependencyObject) element, (DependencyObject) this.BackgroundPanel);
      Storyboard storyboard = new Storyboard();
      storyboard.Children.Add((Timeline) element);
      storyboard.AutoReverse = true;
      return storyboard;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RobotChallenge;component/robotcontrol.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId == 1)
        this.BackgroundPanel = (Ellipse) target;
      else
        this._contentLoaded = true;
    }
  }
}
