using System;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class Memory : Calculate
    {
        private static string _currentValue;
        private static string _prevValue;
        private static string _operand;
        private static string _memoryValue;

        public Memory(string currentValue, string prevValue, string operand) : base(currentValue, prevValue, operand)
        {
            _currentValue = currentValue;
            _prevValue = prevValue;
            _operand = operand;
        }

        private static void SaveMemory()
        {
            var result = CalculateResult(_currentValue, _prevValue, _operand);
            Console.WriteLine(result);
        }

        private static void ClearMemory()
        {
            _memoryValue = "";
        }

        public static void MemoryAction(Button btn, string value)
        {
            switch (value)
            {
                case "MC":
                    btn.Click += (sender, e) => ClearMemory();
                    break;
                case "MR":
                    break;
                case "+":
                    break;
                case "-":
                    break;
                case "MS":
                    btn.Click += (sender, e) => SaveMemory();
                    break;
            }
        }
    }
}