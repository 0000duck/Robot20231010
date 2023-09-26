// Decompiled with JetBrains decompiler
// Type: RobotChallenge.MainWindow
// Assembly: RobotChallenge, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D6D709A0-F187-45EA-8914-98F48EAE1590
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\RobotChallenge.exe

using Robot.Common;
using Robot.Tournament;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RobotChallenge
{
  public partial class MainWindow : Window, IComponentConnector
  {
    public Dictionary<Position, RobotControl> RobotPlace = new Dictionary<Position, RobotControl>();
    private const int maxNumber = 13;
    private Runner _runner;
    private Map _map;
    private const int CellWidth = 9;
    private const int CellCount = 100;
    private Storyboard currentAnimation;
    private Dictionary<Position, RobotControl> _positionChanges;
    private static int _speed = 1500;
    internal Grid ChalangePanel;
    internal Button buttonStart;
    internal ComboBox comboBoxSpeed;
    internal Button buttonShowHideLog;
    internal TextBlock TextBlockRoundNumber;
    internal DataGrid ListStatistics;
    internal DataGrid LogList;
    internal Canvas RobotGrid;
    private bool _contentLoaded;

    public IList<OwnerStatistics> RobotStatistics { get; set; }

    public int Variant { get; set; }

    public MainWindow()
    {
      this.InitializeComponent();
      this.CreateGrid();
      Logger.OnLogRound += (LogRoundEventHandler) ((e, args) =>
      {
        this.TextBlockRoundNumber.Text = args.Number.ToString();
        this.BindStatistics();
      });
      Logger.OnLogMessage += new LogEventHandler(this.LogMessage);
      this.Messsages = new ObservableCollection<RobotChallenge.LogMessage>();
      this.LogList.ItemsSource = (IEnumerable) this.Messsages;
    }

    public ObservableCollection<RobotChallenge.LogMessage> Messsages { get; set; }

    public void LogMessage(object sender, LogEventArgs args)
    {
      SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Red);
      string str = "";
      if (args.OwnerName != null)
      {
        solidColorBrush = new SolidColorBrush(ColorsFactory.OwnerColors[args.OwnerName]);
        str = args.OwnerName;
      }
      if (!this.IsLogVisisble())
        return;
      this.Messsages.Insert(0, new RobotChallenge.LogMessage()
      {
        Color = solidColorBrush,
        Message = args.Message,
        Name = str
      });
      if (this.Messsages.Count <= 13)
        return;
      this.Messsages.RemoveAt(13);
    }

    public void InitializeChellange(int variant)
    {
      try
      {
        this._map = new Map(variant, ReflectionScanner.ScanLibs().Length * 10);
        this._runner = new Runner(this._map, new RobotStepCompletedEventHandler(this.ModelChanged));
        ColorsFactory.Initialize(this._runner.Owners);
        this.PaintRobots(this._runner.Robots);
        this.PaintStations();
        this.Height = 1250.0;
        this.BindStatistics();
      }
      catch (FileLoadException ex)
      {
        int num = (int) MessageBox.Show(string.Format("Error loading dll. Possible reason - dll was downloaded from Internet and should bu unblocked (in file properties). Error: {0}. Internal exception {1}", (object) ex.Message, (object) ex.InnerException));
        throw;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(string.Format("Error: {0}. Internal exception {1}", (object) ex.Message, (object) ex.InnerException));
        throw;
      }
    }

    private void BindStatistics()
    {
      List<OwnerStatistics> ownerStatistics = this._runner.CalculateOwnerStatistics();
      List<OwnerLegend> ownerLegendList = new List<OwnerLegend>();
      foreach (OwnerStatistics stat in ownerStatistics)
        ownerLegendList.Add(new OwnerLegend(stat));
      this.ListStatistics.ItemsSource = (IEnumerable) ownerLegendList;
    }

    private void PaintRobots(IList<Robot.Common.Robot> robots)
    {
      foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>) robots)
        this.CreateRobot(robot.OwnerName, robot.Position);
    }

    public void SetAnimation(RobotControl robotControl, Position from, Position to, bool isLast)
    {
      this.currentAnimation = new Storyboard();
      TimeSpan ts = TimeSpan.FromMilliseconds((double) MainWindow._speed);
      this.currentAnimation.Children.Add((Timeline) this.MoveLeftAnimation(robotControl, (double) (from.X * 9), (double) (to.X * 9), ts));
      this.currentAnimation.Children.Add((Timeline) this.MoveRightAnimation(robotControl, (double) (from.Y * 9), (double) (to.Y * 9), ts));
      if (isLast)
        this.currentAnimation.Completed += new EventHandler(this.ViewUpdatedChanged);
      this.currentAnimation.Begin();
    }

    public DoubleAnimation MoveLeftAnimation(
      RobotControl robotControl,
      double fromX,
      double toX,
      TimeSpan ts)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(fromX);
      doubleAnimation.To = new double?(toX);
      doubleAnimation.Duration = (Duration) ts;
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath("(Canvas.Left)", new object[0]));
      Storyboard.SetTarget((DependencyObject) element, (DependencyObject) robotControl);
      return element;
    }

    public DoubleAnimation MoveRightAnimation(
      RobotControl robotControl,
      double fromX,
      double toX,
      TimeSpan ts)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(fromX);
      doubleAnimation.To = new double?(toX);
      doubleAnimation.Duration = (Duration) ts;
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTargetProperty((DependencyObject) element, new PropertyPath("(Canvas.Top)", new object[0]));
      Storyboard.SetTarget((DependencyObject) element, (DependencyObject) robotControl);
      return element;
    }

    public void ModelChanged(object sender, UpdateViewAfterRobotStepEventArgs args)
    {
      this._positionChanges = new Dictionary<Position, RobotControl>();
      if (args.MovedFrom == null || args.MovedFrom.Count == 0)
      {
        if (args.NewRobotPosition != (Position) null)
        {
          this.CreateRobot(args.OwnerName, args.NewRobotPosition);
          this.currentAnimation = this.RobotPlace[args.NewRobotPosition].AnimateOpacity();
          this.currentAnimation.Completed += new EventHandler(this.ViewUpdatedChanged);
          this.currentAnimation.Begin();
        }
        else
        {
          if (!(args.RobotPosition != (Position) null))
            return;
          this.currentAnimation = this.RobotPlace[args.RobotPosition].AnimateOpacity();
          this.currentAnimation.Completed += new EventHandler(this.ViewUpdatedChanged);
          this.currentAnimation.Begin();
        }
      }
      else
      {
        for (int index = 0; index < args.MovedFrom.Count; ++index)
        {
          Position position1 = args.MovedFrom[index];
          Position position2 = args.MovedTo[index];
          RobotControl robotControl = this.RobotPlace[position1];
          this.SetAnimation(robotControl, position1, position2, index == args.MovedFrom.Count - 1);
          this.RobotPlace.Remove(position1);
          this._positionChanges.Add(position2, robotControl);
        }
      }
    }

    public void ViewUpdatedChanged(object sender, EventArgs args)
    {
      if (this.currentAnimation != null && this.currentAnimation.Children != null)
        this.currentAnimation.Children.Clear();
      this.currentAnimation = (Storyboard) null;
      try
      {
        if (this._positionChanges != null)
        {
          foreach (Position key in this._positionChanges.Keys)
            this.RobotPlace.Add(key, this._positionChanges[key]);
          this._positionChanges.Clear();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this._runner.DoStep();
    }

    private void PaintStations()
    {
      foreach (EnergyStation station in (IEnumerable<EnergyStation>) this._map.Stations)
      {
        EnergyStationControl energyStationControl = new EnergyStationControl();
        this.RobotGrid.Children.Add((UIElement) energyStationControl);
        this.SetPosition(energyStationControl, station.Position);
      }
    }

    private void SetPosition(EnergyStationControl control, Position position)
    {
      control.Top = (double) (position.Y * 9);
      control.Left = (double) (position.X * 9);
    }

    private void SetPosition(RobotControl control, Position position)
    {
      control.Top = (double) (position.Y * 9);
      control.Left = (double) (position.X * 9);
    }

    private void CreateRobot(string ownerName, Position position)
    {
      RobotControl robotControl = new RobotControl();
      robotControl.BackgroundPanel.Fill = (Brush) new SolidColorBrush(ColorsFactory.OwnerColors[ownerName]);
      this.RobotGrid.Children.Add((UIElement) robotControl);
      robotControl.BackgroundPanel.Height = 9.0;
      robotControl.BackgroundPanel.Width = 9.0;
      this.SetPosition(robotControl, position);
      this.SetValue(Panel.ZIndexProperty, (object) 0);
      this.RobotPlace.Add(position, robotControl);
    }

    private void CreateGrid()
    {
      for (int index = 0; index <= 100; ++index)
      {
        UIElementCollection children1 = this.RobotGrid.Children;
        Line element1 = new Line();
        element1.X1 = (double) (index * 9);
        element1.X2 = (double) (index * 9);
        element1.Y1 = 0.0;
        element1.Y2 = 900.0;
        element1.StrokeThickness = 1.0;
        element1.Stroke = (Brush) new SolidColorBrush(Colors.Black);
        children1.Add((UIElement) element1);
        UIElementCollection children2 = this.RobotGrid.Children;
        Line element2 = new Line();
        element2.Y1 = (double) (index * 9);
        element2.Y2 = (double) (index * 9);
        element2.X1 = 0.0;
        element2.X2 = 900.0;
        element2.StrokeThickness = 1.0;
        element2.Stroke = (Brush) new SolidColorBrush(Colors.Black);
        children2.Add((UIElement) element2);
      }
    }

    private void buttonStart_Click(object sender, RoutedEventArgs e)
    {
      this.buttonStart.IsEnabled = false;
      this.ViewUpdatedChanged((object) null, (EventArgs) null);
    }

    public static int Speed => MainWindow._speed;

    private void comboBoxSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.comboBoxSpeed.SelectedValue == null)
        return;
      MainWindow._speed = int.Parse(this.comboBoxSpeed.SelectedValue.ToString());
    }

    private bool IsLogVisisble() => this.LogList.Visibility == Visibility.Visible;

    private void buttonShowHideLog_Click(object sender, RoutedEventArgs e)
    {
      if (this.LogList.Visibility == Visibility.Hidden)
      {
        this.buttonShowHideLog.Content = (object) "Hide log";
        this.LogList.Visibility = Visibility.Visible;
        this.LogList.ItemsSource = (IEnumerable) this.Messsages;
      }
      else
      {
        this.buttonShowHideLog.Content = (object) "Show log";
        this.LogList.Visibility = Visibility.Hidden;
        this.LogList.ItemsSource = (IEnumerable) null;
        this.Messsages.Clear();
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/RobotChallenge;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.ChalangePanel = (Grid) target;
          break;
        case 2:
          this.buttonStart = (Button) target;
          this.buttonStart.Click += new RoutedEventHandler(this.buttonStart_Click);
          break;
        case 3:
          this.comboBoxSpeed = (ComboBox) target;
          this.comboBoxSpeed.SelectionChanged += new SelectionChangedEventHandler(this.comboBoxSpeed_SelectionChanged);
          break;
        case 4:
          this.buttonShowHideLog = (Button) target;
          this.buttonShowHideLog.Click += new RoutedEventHandler(this.buttonShowHideLog_Click);
          break;
        case 5:
          this.TextBlockRoundNumber = (TextBlock) target;
          break;
        case 6:
          this.ListStatistics = (DataGrid) target;
          break;
        case 7:
          this.LogList = (DataGrid) target;
          break;
        case 8:
          this.RobotGrid = (Canvas) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
