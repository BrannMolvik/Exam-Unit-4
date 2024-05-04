using System;
using System.Device.Gpio;
using System.Threading;

namespace Physical_Game
{
    public class HardwareInteraction
    {
        #region Variables

        public const int TOGGLE_ALL = -1;
        public const int CENTER_LED = 3;
        GpioController controller;
        GpioPin led0;
        GpioPin led1;
        GpioPin led2;
        GpioPin led3;
        GpioPin led4;
        GpioPin led5;
        GpioPin led6;
        GpioPin button0;
        GpioPin button1;
        GpioPin button2;
        GpioPin button3;
        GpioPin button4;
        GpioPin button5;
        GpioPin button6;

        #endregion

        #region Functions
        public HardwareInteraction()
        {
            controller = new GpioController();
            led0 = controller.OpenPin(4, PinMode.Output);
            led1 = controller.OpenPin(16, PinMode.Output);
            led2 = controller.OpenPin(17, PinMode.Output);
            led3 = controller.OpenPin(5, PinMode.Output);
            led4 = controller.OpenPin(18, PinMode.Output);
            led5 = controller.OpenPin(19, PinMode.Output);
            led6 = controller.OpenPin(21, PinMode.Output);
            button0 = controller.OpenPin(32, PinMode.InputPullUp);
            button1 = controller.OpenPin(33, PinMode.InputPullUp);
            button2 = controller.OpenPin(25, PinMode.InputPullUp);
            button3 = controller.OpenPin(26, PinMode.InputPullUp);
            button4 = controller.OpenPin(27, PinMode.InputPullUp);
            button5 = controller.OpenPin(13, PinMode.InputPullUp);
            button6 = controller.OpenPin(22, PinMode.InputPullUp);
        }

        public void ToggleLed(int ledNumber)
        {
            switch (ledNumber)
            {
                case 0:
                    led0.Toggle();
                    break;
                case 1:
                    led1.Toggle();
                    break;
                case 2:
                    led2.Toggle();
                    break;
                case 3:
                    led3.Toggle();
                    break;
                case 4:
                    led4.Toggle();
                    break;
                case 5:
                    led5.Toggle();
                    break;
                case 6:
                    led6.Toggle();
                    break;
                case TOGGLE_ALL:
                    led0.Toggle();
                    led1.Toggle();
                    led2.Toggle();
                    led3.Toggle();
                    led4.Toggle();
                    led5.Toggle();
                    led6.Toggle();
                    break;
                default:
                    break;
            }
        }

        public int GetInput()
        {
            bool buttonNotPressed = true;
            int pressedButtonNumber = -1;
            while(buttonNotPressed)
            {
                if (button0.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 0;
                }
                else if (button1.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 1;
                }
                else if (button2.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 2;
                }
                else if (button3.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 3;
                }
                else if (button4.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 4;
                }
                else if (button5.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 5;
                }
                else if (button6.Read() == PinValue.Low)
                {
                    buttonNotPressed = false;
                    pressedButtonNumber = 6;
                }
            }
            return pressedButtonNumber;
        }

        public void FlashLed(int ledNumber, int flashAmount, int flashTime, HardwareInteraction hardware)
        {
            for(int i = 0; i < flashAmount; i++)
            {
                hardware.ToggleLed(ledNumber);
                Thread.Sleep(flashTime);
                hardware.ToggleLed(ledNumber);
                Thread.Sleep(flashTime);
            }
        }

        #endregion
    }
}
