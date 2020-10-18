using System;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class Memory
    {
        public HistoryItem[] History = {};

        public void AddHistory(string prevValue, string result)
        {
            var item = new HistoryItem {PrevValue = prevValue, Result = result};
            Array.Resize(ref History, History.Length + 1);
            History[^1] = item;
        }
    }

    public class HistoryItem
    {
        public string PrevValue;
        public string Result;
    }
}