using System;
using System.Diagnostics;
using System.IO;

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
        private const string GamesCrashed = "Games that were started but stopped abruptly: ";
        private const string Splitter = "=========================";
        private const string LogFilePath = "logfile.txt";
        private const string EnterScoresMessage = "Enter the number of scores to display:";
        private const string InvalidNumberMessage = "Invalid input. Please enter a valid number.";
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
                        File.AppendAllText(LogFilePath, $"Score: {score}\n");
                        score = 0;
                    }
                    else if (failType == FAIL_TIME)
                    {
                        Debug.WriteLine(FailTimeString);
                        File.AppendAllText(LogFilePath, $"Score: {score}\n");
                        score = 0;
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

        public static void DisplayScores(int numberOfScores)
        {
            string[] lines = File.ReadAllLines(LogFilePath);
            int startIndex = Math.Max(0, lines.Length - numberOfScores);
            int endIndex = lines.Length - 1;

            for (int i = startIndex; i <= endIndex; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string welcomeMessage = "Game CLI something";
            const string promptMessage = "Enter a command (start/stop/scores/exit): ";
            const string exitMessage = "Exiting the game. God save the queen!";
            const string invalidCommandMessage = "Invalid command. Please enter 'start', 'stop', 'scores', or 'exit'.";

            Console.WriteLine(welcomeMessage);

            while (true)
            {
                Console.WriteLine(promptMessage);
                string input = Console.ReadLine()?.ToLower();

                switch (input)
                {
                    case "start":
                        Log.WriteToLog(Log.START);
                        break;
                    case "stop":
                        Log.WriteToLog(Log.STOP);
                        break;
                    case "scores":
                        Console.WriteLine(EnterScoresMessage);
                        if (int.TryParse(Console.ReadLine(), out int numberOfScores))
                        {
                            Log.DisplayScores(numberOfScores);
                        }
                        else
                        {
                            Console.WriteLine(InvalidNumberMessage);
                        }
                        break;
                    case "exit":
                        Console.WriteLine(exitMessage);
                        return;
                    default:
                        Console.WriteLine(invalidCommandMessage);
                        break;
                }

                Log.WriteOverallStats();
            }
        }
    }
}