namespace ConsoleAutoclicker
{
    using System.Threading;
    using System;
    using static Input.ME;
    using static Input.VK;
    using static Input.PI;
    using static System.Console;

    class Program
    {
        public static string delay;
        public static double result;

        static void Main()
        {
            MainLoop();
        }

        public static void MainLoop()
        {
            WriteLine("Enter delay amount: ");
            delay = ReadLine();
            WriteLine("Type start to start the auto clicker.");
            string start = ReadLine();

            if (start == "start" || start == "Start")
            {
                bool createdNew;
                var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "AutoclickerClicking", out createdNew);
                if (!createdNew)
                {
                    waitHandle.Set();
                    return;
                }

                if (double.TryParse(delay, out result))
                {
                    var timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(double.Parse(delay)));
                    waitHandle.WaitOne();
                }
                else
                {
                    WriteLine("You did not enter a floating point number");
                    MainLoop();
                }
            }
            else
            {
                WriteLine("You entered something other than start.");
                MainLoop();
            }
        }

        private static void OnTimerElapsed(object state)
        {
            Autoclicker();
        }

        public static void Autoclicker()
        {
            if (GetAsyncKeyState((int)VK_MBUTTON) != 0)
            {
                mouse_event((UInt32)MOUSEEVENTF_LEFTDOWN, 0, 0, 0, (UIntPtr)0);
                mouse_event((UInt32)MOUSEEVENTF_LEFTUP, 0, 0, 0, (UIntPtr)0);
            }
        }
    }
}
