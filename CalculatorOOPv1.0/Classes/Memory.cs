using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class Memory
    {
        public HistoryItem[] History = { };

        public void AddHistory(string prevValue, string result)
        {
            try
            {
                if (prevValue != "" && result != "" && result != "," && prevValue != result)
                {
                    var item = new HistoryItem {PrevValue = prevValue, Result = result};
                    Array.Resize(ref History, History.Length + 1);
                    History[^1] = item;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                //ignore
            }
        }
        
        public static void CreateHistory(Control form)
        {
            var historyBtn = new Button
            {
                Text = @"H",
                Location = new Point(150, 5),
                Size = new Size(25, 25),
                BackColor = Color.FromArgb(53, 57, 87),
                Cursor = Cursors.Hand,
                FlatAppearance =
                {
                    BorderColor = Color.White, BorderSize = 0, MouseDownBackColor = Color.Transparent,
                    MouseOverBackColor = Color.FromArgb(255, 221, 60)
                },
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.Transparent
            };
            historyBtn.Click += GenerateHistory;
            form.Controls.Add(historyBtn);
        }
        
        private static void GenerateHistory(object sender, EventArgs e)
        {
            var top = 10;
            foreach (var item in Display.memory.History)
            {
                var prevValue = new Label
                {
                    Text = item.PrevValue,
                    Location = new Point(10, top),
                    Size = new Size(100, 20)
                };
                var result = new Label
                {
                    Text = item.Result,
                    Location = new Point(10, top + 20),
                    Size = new Size(100, 20)
                };
                prevValue.Tag = new HistoryItem{PrevValue = prevValue.Text, Result = result.Text};
                prevValue.Click += OpenHistory;
                top += 40;
                Display.history.Controls.Add(prevValue);
                Display.history.Controls.Add(result);
            }

            Display.history.ShowDialog();
        }

        private static void OpenHistory(object sender, EventArgs e)
        {
            var test = ((Label)sender).Tag;
        }
    }

    public class HistoryItem
    {
        public string PrevValue;
        public string Result;
    }
}