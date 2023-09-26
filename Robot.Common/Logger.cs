// Decompiled with JetBrains decompiler
// Type: Robot.Common.Logger
// Assembly: Robot.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 156B1DBD-D85D-4D40-93D3-F4AF8080C42B
// Assembly location: C:\users\olecs\OneDrive\Рабочий стол\Tournament\Robot.Common.dll

using System.Diagnostics;

namespace Robot.Common
{
  public static class Logger
  {
    public static event LogRoundEventHandler OnLogRound;

    public static void LogRound(int roundNumber)
    {
      Debug.WriteLine(string.Format("ROUND NOMBER: {0}", (object) roundNumber));
      LogRoundEventHandler onLogRound = Logger.OnLogRound;
      if (onLogRound == null)
        return;
      onLogRound((object) null, new LogRoundEventArgs()
      {
        Number = roundNumber
      });
    }

    public static event LogEventHandler OnLogMessage;

    private static void LogMessage(LogEventArgs e)
    {
      Debug.WriteLine(e.Message);
      LogEventHandler onLogMessage = Logger.OnLogMessage;
      if (onLogMessage == null)
        return;
      onLogMessage((object) null, e);
    }

    public static void LogMessage(string owner, string message, LogValue priority = LogValue.Normal) => Logger.LogMessage(new LogEventArgs(owner, message, priority));
  }
}
