using System;
using System.Drawing;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace CalculatorOOPv1._0.Classes
{
    public class Display
    {
        public readonly string[] Buttons =
        {
            "%", "CE", "C", "<",
            "1/x", "x^2", "√x", "/", 
            "7", "8", "9", "x",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "±", "0", ".", "=" 
        };
        public readonly Label LabelPrevValue = new Label {Location = new Point(10, 10), Size = new Size(320, 20)};
        public readonly Label LabelCurrentValue = new Label {Location = new Point(10, 50), Size = new Size(320, 20)};
        private string _currentValue;
        public string PrevValue;

        private void BtnClick(object sender, EventArgs e)
        {
            _currentValue += (sender as Button)?.Text;
            LabelCurrentValue.Text = _currentValue;
        }

        public void GenerateStructure(Form form)
        {
            var top = 100;
            var textIndex = 0;
            form.Controls.Add(LabelPrevValue);
            form.Controls.Add(LabelCurrentValue);
            for (var i = 0; i < 6; i++)
            {
                var left = 10;
                for (var j = 0; j < 4; j++)
                {
                    var btn = new Button
                    {
                        Text = Buttons[textIndex], Location = new Point(left, top), Size = new Size(74, 47)
                    };
                    btn.Click += BtnClick;

                    form.Controls.Add(btn);
                    textIndex++;
                    left += btn.Width + 2;
                }

                top += 50;
            }
        }
    }
}