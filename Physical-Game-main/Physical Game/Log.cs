using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Physical_Game
{
    public static class Log
    {
        #region Variables
        public const int START = 0;
        public const int STOP = 1;
        public const int TIME = 2;
        public const int FAIL_BUTTON = 1;
        public const int FAIL_TIME = 2;

        private const string StartString = "[START]: ";
        private const string StopString = "[END]";
        private const string TimeString = "[TIME]: ";
        private const string FailButtonString = "[FAIL]: WrongButton";
        private const string FailTimeString = "[FAIL]: TimeOut";

        private static int gamesStarted = 0;
        private static int gamesStopped = 0;
        private static int score = 0;

        private const string OverallStatsTitle = "=== Overall Statistics ===";
        private const string GamesStartedStat = "Total Games Started: ";
        private const string GamesStoppedStat = "Total Games Stopped: ";
        private const string ScoreStat = "Score last round: ";
        private const string GamesCrashed = "Games that were started but stopped abdruptly: ";
        private const string Splitter = "=========================";
        #endregion

        public static void WriteToLog(int infoType, int failType = 0, string time = "")
        {
            switch (infoType)
            {
                case TIME:
                    Debug.WriteLine(TimeString + time);
                    score++;
                    break;
                case STOP:
                    if (failType == FAIL_BUTTON)
                    {
                        Debug.WriteLine(FailButtonString);
                    }
                    else if (failType == FAIL_TIME)
                    {
                        Debug.WriteLine(FailTimeString);
                    }
                    Thread.Sleep(10);
                    Debug.WriteLine(StopString);
                    gamesStopped++;
                    break;
                case START:
                    Debug.WriteLine(StartString);
                    gamesStarted++;
                    break;
            }
        }

        public static void WriteOverallStats()
        {
            Debug.WriteLine(OverallStatsTitle);
            Debug.WriteLine(GamesStartedStat + gamesStarted);
            Debug.WriteLine(GamesStoppedStat + gamesStopped);
            Debug.WriteLine(ScoreStat + score);
            Debug.WriteLine(GamesCrashed + (gamesStarted - gamesStopped));
            Debug.WriteLine(Splitter);
        }
    }
}