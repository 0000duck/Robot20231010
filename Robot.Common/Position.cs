// Decompiled with JetBrains decompiler
// Type: Robot.Common.Position
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

namespace Robot.Common
{
  public sealed class Position
  {
    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString() => string.Format("({0},{1})", (object) this.X, (object) this.Y);

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return !(obj.GetType() != typeof (Position)) && this.Equals((Position) obj);
    }

    public Position Copy() => new Position()
    {
      X = this.X,
      Y = this.Y
    };

    public Position()
    {
    }

    public Position(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    public static bool operator ==(Position a, Position b)
    {
      if ((object) a == (object) b)
        return true;
      return (object) a != null && (object) b != null && a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Position a, Position b) => !(a == b);

    public bool Equals(Position other)
    {
      if ((object) other == null)
        return false;
      return (object) this == (object) other || other.X == this.X && other.Y == this.Y;
    }

    public override int GetHashCode() => this.X * 397 ^ this.Y;
  }
}
