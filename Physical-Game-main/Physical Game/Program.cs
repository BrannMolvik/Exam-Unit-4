using System;
using System.Diagnostics;
using System.Threading;

namespace Physical_Game
{
    public class Program
    {
        static bool isPlaying = true;
        public static void Main()
        {
            HardwareInteraction hardware = new();
            hardware.FlashLed(HardwareInteraction.TOGGLE_ALL, 1, 2000, hardware);
            hardware.ToggleLed(HardwareInteraction.CENTER_LED);

            while(isPlaying)
            {
                if (hardware.GetInput() == HardwareInteraction.CENTER_LED)
                {
                    GameLogic game = new GameLogic(hardware);
                }
                else if(hardware.GetInput() == 0)
                {
                    if (hardware.GetInput() == HardwareInteraction.CENTER_LED)
                    {
                        isPlaying = false;
                    }
                }
            }

        }
    }
}
