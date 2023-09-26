// Decompiled with JetBrains decompiler
// Type: Robot.Common.Variant
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System;

namespace Robot.Common
{
  public class Variant
  {
    private static Variant _instance;
    public readonly int MaxStationEnergy = 1000;
    internal readonly int MaxEnergyGrowth = 10;
    internal readonly int MinEnergyGrowth = 5;
    internal readonly int EnergyStationForAttendant = 5;
    internal readonly int CollectingDistance = 0;
    internal readonly int MaxEnergyCanCollect = 200;
    internal readonly int EnergyLossToCreateNewRobot = 200;
    internal readonly int AttackEnergyLoss = 10;
    internal readonly double StoleRateEnergyAtAttack = 0.0;

    public static void Initialize(int variantNum) => Variant._instance = Variant._instance == null ? new Variant(variantNum) : throw new Exception("Варіант вже ініціалізований");

    public static Variant GetInstance() => Variant._instance;

    private Variant(int variantNum)
    {
      switch (variantNum)
      {
        case 1:
          this.MaxEnergyGrowth = 40;
          this.MinEnergyGrowth = 20;
          this.MaxStationEnergy = 5000;
          this.CollectingDistance = 1;
          this.EnergyStationForAttendant = 5;
          this.EnergyLossToCreateNewRobot = 100;
          break;
        case 2:
          this.EnergyStationForAttendant = 5;
          this.MaxEnergyGrowth = 100;
          this.MinEnergyGrowth = 50;
          this.MaxStationEnergy = 20000;
          this.CollectingDistance = 2;
          this.MaxEnergyCanCollect = 40;
          this.EnergyLossToCreateNewRobot = 100;
          break;
        case 3:
          this.EnergyStationForAttendant = 2;
          this.MaxEnergyGrowth = 100;
          this.MinEnergyGrowth = 50;
          this.MaxStationEnergy = 10000;
          this.StoleRateEnergyAtAttack = 0.05;
          this.AttackEnergyLoss = 50;
          break;
        case 4:
          this.EnergyStationForAttendant = 2;
          this.MaxEnergyGrowth = 30;
          this.MinEnergyGrowth = 10;
          this.CollectingDistance = 2;
          this.StoleRateEnergyAtAttack = 0.1;
          this.AttackEnergyLoss = 30;
          break;
        case 5:
          this.EnergyStationForAttendant = 5;
          this.MaxEnergyGrowth = 40;
          this.MinEnergyGrowth = 20;
          this.StoleRateEnergyAtAttack = 0.05;
          this.AttackEnergyLoss = 50;
          this.CollectingDistance = 1;
          this.EnergyLossToCreateNewRobot = 100;
          break;
        case 6:
          this.EnergyStationForAttendant = 5;
          this.MaxEnergyGrowth = 100;
          this.MinEnergyGrowth = 50;
          this.MaxStationEnergy = 20000;
          this.CollectingDistance = 2;
          this.MaxEnergyCanCollect = 40;
          this.EnergyLossToCreateNewRobot = 50;
          this.StoleRateEnergyAtAttack = 0.1;
          this.AttackEnergyLoss = 20;
          break;
        case 7:
          this.EnergyStationForAttendant = 2;
          this.MaxEnergyGrowth = 100;
          this.MinEnergyGrowth = 50;
          this.CollectingDistance = 3;
          this.EnergyLossToCreateNewRobot = 50;
          break;
        case 8:
          this.EnergyStationForAttendant = 2;
          this.MaxEnergyGrowth = 30;
          this.MinEnergyGrowth = 10;
          this.StoleRateEnergyAtAttack = 0.05;
          this.AttackEnergyLoss = 30;
          this.CollectingDistance = 2;
          this.MaxEnergyCanCollect = 300;
          break;
        case 9:
          this.MaxEnergyGrowth = 40;
          this.MinEnergyGrowth = 20;
          this.MaxStationEnergy = 5000;
          this.CollectingDistance = 2;
          this.MaxEnergyCanCollect = 300;
          this.EnergyStationForAttendant = 20;
          this.EnergyLossToCreateNewRobot = 100;
          this.StoleRateEnergyAtAttack = 0.1;
          this.AttackEnergyLoss = 30;
          break;
        case 10:
          this.MaxEnergyGrowth = 50;
          this.MinEnergyGrowth = 10;
          this.MaxStationEnergy = 20000;
          this.CollectingDistance = 4;
          this.MaxEnergyCanCollect = 500;
          this.EnergyStationForAttendant = 10;
          this.EnergyLossToCreateNewRobot = 1000;
          this.StoleRateEnergyAtAttack = 0.3;
          this.AttackEnergyLoss = 20;
          break;
        default:
          throw new Exception("Not supported variant");
      }
    }
  }
}
