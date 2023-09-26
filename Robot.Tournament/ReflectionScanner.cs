// Decompiled with JetBrains decompiler
// Type: Robot.Tournament.ReflectionScanner
// Assembly: Robot.Tournament, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 62B4B0F1-8974-46DC-9DC4-634CEB6178F4
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Tournament.dll

using Robot.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Robot.Tournament
{
  public class ReflectionScanner
  {
    public static string DirPath = AppDomain.CurrentDomain.BaseDirectory + "AlgorithmDlls\\";

    public static string[] ScanLibs() => Directory.GetFiles(ReflectionScanner.DirPath, "*.dll");

    public static List<IRobotAlgorithm> Scan()
    {
      List<IRobotAlgorithm> robotAlgorithmList = new List<IRobotAlgorithm>();
      foreach (string scanLib in ReflectionScanner.ScanLibs())
      {
        Assembly assembly = Assembly.LoadFrom(scanLib);
        List<Type> list = ((IEnumerable<Type>) assembly.GetTypes()).Where<Type>((Func<Type, bool>) (t => typeof (IRobotAlgorithm).IsAssignableFrom(t))).ToList<Type>();
        if (list.Count > 0)
        {
          Type type = list[0];
          IRobotAlgorithm instance = (IRobotAlgorithm) assembly.CreateInstance(type.ToString());
          if (instance == null)
            throw new Exception("Could not initialize instance in dll: " + scanLib);
          if (instance.Author == null)
            throw new Exception("Author name could not be null in dll: " + scanLib);
          robotAlgorithmList.Add(instance);
        }
      }
      return robotAlgorithmList;
    }
  }
}
