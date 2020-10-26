using System;
using System.Diagnostics;

namespace CalculatorOOPv1._0.Classes
{
    public class Calculate
    {
        private readonly string _currentValue;
        private readonly string _prevValue;
        private readonly string _operand;

        public Calculate(string currentValue, string prevValue, string operand)
        {
            _currentValue = currentValue;
            _prevValue = prevValue;
            _operand = operand;
        }

        public static string CalculateResult(string currentValue, string prevValue, string operand)
        {
            try
            {
                var curVal = Convert.ToDouble(currentValue);
                var prevVal = Convert.ToDouble(prevValue);
                if (curVal == 0 && operand == "/")
                {
                    throw new Exception();
                }
                return operand switch
                {
                    "+" => (curVal + prevVal).ToString(),
                    "-" => (prevVal - curVal).ToString(),
                    "x" => (prevVal * curVal).ToString(),
                    "/" => (prevVal / curVal).ToString()
                };
            }
            catch
            {
                return "Error";
            }
        }

        public string CalculateModificator(string value)
        {
            try
            {
                var curVal = Convert.ToDouble(_currentValue);
                return value switch
                {
                    "%" => (curVal / 100).ToString(),
                    "1/x" => (1 / curVal).ToString(),
                    "x^2" => (curVal * curVal).ToString(),
                    "√x" => Math.Sqrt(curVal).ToString()
                };
            }
            catch
            {
                return "Error";
            }
        }
    }
}