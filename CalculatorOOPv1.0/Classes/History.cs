using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class History
    {
        public HistoryItem[] HistoryItems = { };

        public void AddHistory(string prevValue, string result)
        {
            try
            {
                if (prevValue != "" && result != "" && result != "," && prevValue != result)
                {
                    var item = new HistoryItem {PrevValue = prevValue, Result = result};
                    Array.Resize(ref HistoryItems, HistoryItems.Length + 1);
                    HistoryItems[^1] = item;
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
                    MouseOverBackColor = Color.FromArgb(108, 99, 255)
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
            foreach (var item in Display.History.HistoryItems)
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
                prevValue.Click += (sender, e) => OpenHistory(prevValue.Text, result.Text);
                top += 40;
                Display.HistoryForm.Controls.Add(prevValue);
                Display.HistoryForm.Controls.Add(result);
            }

            Display.HistoryForm.ShowDialog();
        }

        private static void OpenHistory(string prev, string res)
        {
            Display.LabelCurrentValue.Text = res;
            Display.LabelPrevValue.Text = prev;
            Display.HistoryForm.Close();
        }
    }

    public class HistoryItem
    {
        public string PrevValue;
        public string Result;
    }
}