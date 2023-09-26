// Decompiled with JetBrains decompiler
// Type: RobotChallenge.App
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace RobotChallenge
{
  public class App : Application
  {
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent() => this.StartupUri = new Uri("StartWindow.xaml", UriKind.Relative);

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
