using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace CalculatorOOPv1._0.Classes
{
    public class Display
    {
        public static readonly History History = new History();
        public static readonly Form HistoryForm = new Form();

        private readonly string[] _buttons =
        {
            "%", "CE", "C", "<",
            "1/x", "x^2", "√x", "/",
            "7", "8", "9", "x",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "±", "0", ",", "="
        };

        public static readonly Label LabelPrevValue = new Label
        {
            Location = new Point(10, 30),
            Size = new Size(310, 20),
            ForeColor = Color.White,
            Font = new Font("Britannic Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            TextAlign = ContentAlignment.MiddleRight
        };

        public static readonly Label LabelCurrentValue = new Label
        {
            Location = new Point(10, 50),
            Size = new Size(310, 20),
            ForeColor = Color.White,
            Font = new Font("Britannic Bold", 18F),
            TextAlign = ContentAlignment.MiddleRight
        };

        private bool _wasCalculated;
        private string _prevValue = "";
        private string _currentValue = "";
        private string _operand;

        private void BtnClick(object sender, EventArgs e)
        {
            var value = (sender as Button)?.Text;
            var isNum = double.TryParse(value, out _);
            var isOperand = new[] {"+", "-", "x", "/"}.Contains(value);
            var isModificator = new[] {"%", "1/x", "x^2", "√x"}.Contains(value);

            if (isModificator)
                Modificate(value);
            else if (_wasCalculated && ValidValue(value, isOperand, isModificator))
                AddDigit(value);
            else if (isNum || value == "," && !LabelCurrentValue.Text.Contains(","))
                LabelCurrentValue.Text += value;
            else if (_prevValue == "" && isOperand && LabelCurrentValue.Text != "")
                CalcOperand(value);
            else if (isOperand && _currentValue != "")
                AddOperand(value, _currentValue);
            else if (isOperand && LabelCurrentValue.Text != "")
                AddOperand(value, LabelCurrentValue.Text);
            else if (value == "=")
                Equal();
            else
                Actions(value);
        }

        private void AddDigit(string value)
        {
            _currentValue = LabelCurrentValue.Text;
            LabelCurrentValue.Text = value;
            _wasCalculated = false;
        }

        private static bool ValidValue(string value, bool isOperand, bool isModificator)
        {
            return !isModificator && !isOperand && value != "C" && value != "CE" && value != "<" && value != "±" &&
                   value != "=";
        }

        private void CalcOperand(string value)
        {
            _operand = value;
            _prevValue = LabelCurrentValue.Text;
            LabelPrevValue.Text = _prevValue + _operand;
            LabelCurrentValue.Text = "";
        }

        private void AddOperand(string value, string currentValue)
        {
            LabelPrevValue.Text += LabelCurrentValue.Text + value;
            var calc = new Calculate(currentValue, _prevValue, _operand);
            LabelCurrentValue.Text = Calculate.CalculateResult(LabelCurrentValue.Text, _prevValue, _operand);
            _operand = value;
            _wasCalculated = true;
            _currentValue = "";
        }

        private void Modificate(string value)
        {
            var calc = new Calculate(LabelCurrentValue.Text, _prevValue, _operand);
            LabelCurrentValue.Text =
                calc.CalculateModificator(value) == "Error" ? "" : calc.CalculateModificator(value);
        }

        private void Equal()
        {
            var calc = new Calculate(LabelCurrentValue.Text, _prevValue, _operand);
            LabelPrevValue.Text += LabelCurrentValue.Text;
            LabelCurrentValue.Text =
                Calculate.CalculateResult(LabelCurrentValue.Text, _prevValue, _operand) == "Error"
                    ? LabelCurrentValue.Text
                    : Calculate.CalculateResult(LabelCurrentValue.Text, _prevValue, _operand);
            History.AddHistory(LabelPrevValue.Text, LabelCurrentValue.Text);
            LabelPrevValue.Text = "";
            _prevValue = "";
            _operand = "";
        }

        private void Actions(string value)
        {
            try
            {
                switch (value)
                {
                    case "C":
                        LabelCurrentValue.Text = "";
                        LabelPrevValue.Text = "";
                        _prevValue = "";
                        _operand = "";
                        break;
                    case "<":
                        LabelCurrentValue.Text =
                            LabelCurrentValue.Text.Remove(LabelCurrentValue.Text.Length - 1);
                        break;
                    case "±":
                        LabelCurrentValue.Text = (Convert.ToDouble(LabelCurrentValue.Text) * -1).ToString();
                        break;
                    case "CE":
                        LabelCurrentValue.Text = "";
                        break;
                }
            }
            catch
            {
                //ignore
            }
        }

        private static void OpenHistory(object sender, EventArgs e)
        {
            var top = 10;
            foreach (var item in History.HistoryItems)
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
                top += 40;
                HistoryForm.Controls.Add(prevValue);
                HistoryForm.Controls.Add(result);
            }

            HistoryForm.ShowDialog();
        }

        public void GenerateStructure(Form form)
        {
            var top = 120;
            var textIndex = 0;
            History.CreateHistory(form);
            GenerateMemoryButtons(form);
            form.Controls.Add(LabelPrevValue);
            form.Controls.Add(LabelCurrentValue);
            for (var i = 0; i < 6; i++)
            {
                var left = 10;
                for (var j = 0; j < 4; j++)
                {
                    var btn = new Button
                    {
                        Text = _buttons[textIndex],
                        Location = new Point(left, top),
                        Size = new Size(74, 47),
                        BackColor = Color.FromArgb(53, 57, 87),
                        Cursor = Cursors.Hand,
                        FlatAppearance =
                        {
                            BorderColor = Color.White, BorderSize = 0, MouseDownBackColor = Color.Transparent,
                            MouseOverBackColor = Color.FromArgb(108, 99, 255)
                        },
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        ForeColor = Color.Transparent,
                    };
                    btn.Click += BtnClick;

                    form.Controls.Add(btn);
                    textIndex++;
                    left += btn.Width + 2;
                }

                top += 50;
            }
        }

        private static void GenerateMemoryButtons(Control form)
        {
            var memoryButtons = new string[] {"MC", "MR", "+", "-", "MS"};
            var left = 10;
            foreach (var button in memoryButtons)
            {
                var btn = new Button
                {
                    Text = button,
                    Location = new Point(left, 90),
                    Size = new Size(58, 27),
                    BackColor = Color.FromArgb(53, 57, 87),
                    Cursor = Cursors.Hand,
                    FlatAppearance =
                    {
                        BorderColor = Color.White, BorderSize = 0, MouseDownBackColor = Color.Transparent,
                        MouseOverBackColor = Color.FromArgb(108, 99, 255)
                    },
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Britannic Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.Transparent,
                };
                Memory.MemoryAction(btn, button);
                left += btn.Width + 3;
                form.Controls.Add(btn);
            }
        }
    }
}