using System;
using System.Threading;
using System.Diagnostics;

namespace Physical_Game
{
    public class GameLogic
    {
        #region Variables
        
        HardwareInteraction hardware;
        Random random;
        Stopwatch stopWatch;
        bool isRunning = true;
        int currentButton;
        int previousButton = -1;
        int currentTimeLimit = 3000;
        long timeOfLightUp;
        long timeActual;

        #endregion

        #region Functions
        public GameLogic(HardwareInteraction hardwareInstance)
        {
            hardware = hardwareInstance;
            random = new Random();
            StartGame();
        }
        
        void StartGame()
        {
            hardware.ToggleLed(HardwareInteraction.CENTER_LED);
            Thread.Sleep(1000);
            hardware.FlashLed(HardwareInteraction.CENTER_LED, 3, 500, hardware);
            stopWatch = new();
            RunGame();
        }
        void RunGame()
        {
            stopWatch.Start();
            Log.WriteToLog(Log.START);
            while(isRunning)
            {
                do
                {
                    currentButton = random.Next(7);
                }
                while (currentButton == previousButton);
                
                hardware.ToggleLed(currentButton);
                timeOfLightUp = stopWatch.ElapsedMilliseconds;

                if(hardware.GetInput() != currentButton)
                {
                    Log.WriteToLog(Log.STOP, Log.FAIL_BUTTON);
                    isRunning = false;
                }
                
                timeActual = stopWatch.ElapsedMilliseconds - timeOfLightUp;
                Log.WriteToLog(Log.TIME,0, timeActual.ToString());
                
                if(timeActual > currentTimeLimit)
                {
                    Log.WriteToLog(Log.STOP, Log.FAIL_TIME);
                    isRunning = false;
                }

                if(currentTimeLimit > 1500)
                {
                    currentTimeLimit -= 50;
                }

                hardware.ToggleLed(currentButton);
                previousButton = currentButton;
                Thread.Sleep(250);
            }
            EndGame();
        }
        void EndGame()
        {
            stopWatch.Stop();
            hardware.FlashLed(HardwareInteraction.TOGGLE_ALL, 2, 500, hardware);
            hardware.ToggleLed(HardwareInteraction.CENTER_LED);
        }
        #endregion
    }
}
