// Decompiled with JetBrains decompiler
// Type: Robot.Common.Map
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot.Common
{
  public sealed class Map
  {
    private readonly Position _maxPozitionForSmallMap = new Position()
    {
      X = 100,
      Y = 100
    };
    public Position MinPozition = new Position()
    {
      X = 0,
      Y = 0
    };
    public Position MaxPozition;
    public IList<EnergyStation> Stations = (IList<EnergyStation>) new List<EnergyStation>();

    public bool IsValid(Position position) => position.X >= 0 && position.X < this.MaxPozition.X && position.Y >= 0 && position.Y < this.MaxPozition.Y;

    public Position FindFreeCell(Position nearPosition, IList<Robot> robots)
    {
      for (int index1 = 1; index1 < 100; ++index1)
      {
        for (int index2 = -index1; index2 <= index1; ++index2)
        {
          for (int index3 = -index1; index3 <= index1; ++index3)
          {
            Position newPos = new Position(nearPosition.X + index2, nearPosition.Y + index3);
            if (this.IsValid(newPos) && robots.All(r => r.Position != newPos))
              return newPos;
          }
        }
      }
      throw new Exception("All cells are filled");
    }

    public EnergyStation GetResource(Position pozition) => this.Stations.FirstOrDefault<EnergyStation>((Func<EnergyStation, bool>) (x => x.Position.X == pozition.X && x.Position.Y == pozition.Y));

    public List<EnergyStation> GetNearbyResources(Position pozition, int distance) => this.Stations.Where<EnergyStation>((Func<EnergyStation, bool>) (station => Math.Abs(station.Position.X - pozition.X) <= distance && Math.Abs(station.Position.Y - pozition.Y) <= distance)).ToList<EnergyStation>();

    private List<EnergyStation> CopyResources() => this.Stations.Select<EnergyStation, EnergyStation>((Func<EnergyStation, EnergyStation>) (energyResource => new EnergyStation()
    {
      Energy = energyResource.Energy,
      Position = energyResource.Position.Copy(),
      RecoveryRate = energyResource.RecoveryRate
    })).ToList<EnergyStation>();

    public Map Copy() => new Map()
    {
      MaxPozition = this._maxPozitionForSmallMap,
      Stations = (IList<EnergyStation>) this.CopyResources()
    };

    public Map()
    {
    }

    public Map(int variant, int initialRobotCount)
    {
      this.MaxPozition = this._maxPozitionForSmallMap;
      Variant.Initialize(variant);
      Variant instance = Variant.GetInstance();
      int num = initialRobotCount * instance.EnergyStationForAttendant;
      List<Position> positionList = new List<Position>();
      Random random = new Random();
      for (int index = 0; index < num; ++index)
      {
        Position position;
        do
        {
          position = new Position()
          {
            X = random.Next(this.MaxPozition.X),
            Y = random.Next(this.MaxPozition.Y)
          };
        }
        while (positionList.Contains(position));
        int minEnergyGrowth = instance.MinEnergyGrowth;
        int maxEnergyGrowth = instance.MaxEnergyGrowth;
        this.Stations.Add(new EnergyStation()
        {
          RecoveryRate = minEnergyGrowth + random.Next(maxEnergyGrowth - minEnergyGrowth),
          Energy = 0,
          Position = position
        });
      }
    }
  }
}
