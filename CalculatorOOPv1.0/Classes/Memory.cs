using System;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class Memory : Calculate
    {
        private static string _memoryValue;

        private static void SaveMemory()
        {
            _memoryValue = Display.PrevValue == ""
                ? Display.LabelCurrentValue.Text
                : CalculateResult(Display.LabelCurrentValue.Text, Display.PrevValue, Display.Operand);
        }

        private static void ClearMemory()
        {
            _memoryValue = "";
        }

        private static void UseMemory()
        {
            Display.LabelCurrentValue.Text = _memoryValue;
        }

        private static void PlusMemory()
        {
            _memoryValue = (Convert.ToDouble(_memoryValue) + Convert.ToDouble(_memoryValue)).ToString();
        }

        private static void MinusMemory()
        {
            _memoryValue = (Convert.ToDouble(_memoryValue) - Convert.ToDouble(_memoryValue)).ToString();
        }

        public static void MemoryAction(Button btn, string value)
        {
            switch (value)
            {
                case "MC":
                    btn.Click += (sender, e) => ClearMemory();
                    break;
                case "MR":
                    btn.Click += (sender, e) => UseMemory();
                    break;
                case "+":
                    btn.Click += (sender, e) => PlusMemory();
                    break;
                case "-":
                    btn.Click += (sender, e) => MinusMemory();
                    break;
                case "MS":
                    btn.Click += (sender, e) => SaveMemory();
                    break;
            }
        }

        public Memory(string currentValue, string prevValue, string operand) : base(currentValue, prevValue, operand)
        {
        }
    }
}