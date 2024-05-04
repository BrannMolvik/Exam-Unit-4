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

        private const string START_STRING = "[START]: ";
        private const string STOP_STRING = "[END]";
        private const string TIME_STRING = "[TIME]: ";
        private const string FAIL_BUTTON_STRING = "[FAIL]: WrongButton";
        private const string FAIL_TIME_STRING = "[FAIL]: TimeOut";
        #endregion

        public static void WriteToLog(int infoType, int failType = 0, string time = "")
        {
            if(infoType == TIME)
            {
                Debug.WriteLine(TIME_STRING + time);
            }
            else if(infoType == STOP)
            {
                if(failType == FAIL_BUTTON)
                {
                    Debug.WriteLine(FAIL_BUTTON_STRING);
                }
                else if(failType == FAIL_TIME)
                {
                    Debug.WriteLine(FAIL_TIME_STRING);
                }
                Thread.Sleep(10);
                Debug.WriteLine(STOP_STRING);
            }
            else if (infoType == START)
            {
                Debug.WriteLine(START_STRING);
            }
        }
    }
}
