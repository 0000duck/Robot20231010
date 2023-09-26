// Decompiled with JetBrains decompiler
// Type: RobotChallenge.StartWindow
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace RobotChallenge
{
  public partial class StartWindow : Window, IComponentConnector
  {
    private int variant = 0;
    internal StackPanel rbHolder1;
    internal Button buttonStart;
    private bool _contentLoaded;

    public StartWindow()
    {
      this.InitializeComponent();
      this.buttonStart.IsEnabled = false;
    }

    private void buttonStart_Click(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = new MainWindow();
      mainWindow.InitializeChellange(this.variant);
      mainWindow.Show();
      this.Close();
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
      this.variant = 1;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
    {
      this.variant = 2;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
    {
      this.variant = 3;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
    {
      this.variant = 4;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
    {
      this.variant = 5;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
    {
      this.variant = 6;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
    {
      this.variant = 7;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_7(object sender, RoutedEventArgs e)
    {
      this.variant = 8;
      this.buttonStart.IsEnabled = true;
    }

    private void RadioButton_Checked_8(object sender, RoutedEventArgs e)
    {
      this.variant = 9;
      this.buttonStart.IsEnabled = true;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RobotChallenge;component/startwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.rbHolder1 = (StackPanel) target;
          break;
        case 2:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked);
          break;
        case 3:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_1);
          break;
        case 4:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_2);
          break;
        case 5:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_3);
          break;
        case 6:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_4);
          break;
        case 7:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_5);
          break;
        case 8:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_6);
          break;
        case 9:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_7);
          break;
        case 10:
          ((ToggleButton) target).Checked += new RoutedEventHandler(this.RadioButton_Checked_8);
          break;
        case 11:
          this.buttonStart = (Button) target;
          this.buttonStart.Click += new RoutedEventHandler(this.buttonStart_Click);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
