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
        private readonly string[] _buttons =
        {
            "%", "CE", "C", "<",
            "1/x", "x^2", "√x", "/",
            "7", "8", "9", "x",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "±", "0", ",", "="
        };

        private readonly Label _labelPrevValue = new Label
        {
            Location = new Point(10, 10),
            Size = new Size(320, 20)
        };

        private readonly Label _labelCurrentValue = new Label
        {
            Location = new Point(10, 50),
            Size = new Size(320, 20)
        };

        private string _prevValue = "";
        private string _operand;

        private void BtnClick(object sender, EventArgs e)
        {
            var value = (sender as Button)?.Text;
            var isNum = double.TryParse(value, out _);
            var isOperand = new[] {"+", "-", "x", "/"}.Contains(value);
            var isModificator = new[] {"%", "1/x", "x^2", "√x"}.Contains(value);

            if (isNum || value == "," && !_labelCurrentValue.Text.Contains(","))
                _labelCurrentValue.Text += value;
            else if (_prevValue == "" && isOperand && _labelCurrentValue.Text != "")
                CalcOperand(value);
            else if (isOperand && _labelCurrentValue.Text != "")
                AddOperand(value);
            else if (isModificator)
                Modificate(value);
            else if (value == "=")
                Equal(value);
            else
                Actions(value);
        }

        private void CalcOperand(string value)
        {
            _operand = value;
            _prevValue = _labelCurrentValue.Text;
            _labelPrevValue.Text = _prevValue + _operand;
            _labelCurrentValue.Text = "";
        }

        private void AddOperand(string value)
        {
            _labelPrevValue.Text += _labelCurrentValue.Text + value;
            var calc = new Calculate(_labelCurrentValue.Text, _prevValue, _operand);
            _labelCurrentValue.Text = calc.CalculateResult();
            _operand = value;
        }

        private void Modificate(string value)
        {
            var calc = new Calculate(_labelCurrentValue.Text, _prevValue, _operand);
            _labelCurrentValue.Text =
                calc.CalculateModificator(value) == "Error" ? "" : calc.CalculateModificator(value);
        }

        private void Equal(string value)
        {
            var calc = new Calculate(_labelCurrentValue.Text, _prevValue, _operand);
            _labelCurrentValue.Text =
                calc.CalculateResult() == "Error" ? _labelCurrentValue.Text : calc.CalculateResult();
            _labelPrevValue.Text = "";
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
                        _labelCurrentValue.Text = "";
                        _labelPrevValue.Text = "";
                        _prevValue = "";
                        _operand = "";
                        break;
                    case "<":
                        _labelCurrentValue.Text =
                            _labelCurrentValue.Text.Remove(_labelCurrentValue.Text.Length - 1);
                        break;
                    case "±":
                        _labelCurrentValue.Text = (Convert.ToDouble(_labelCurrentValue.Text) * -1).ToString();
                        break;
                    case "CE":
                        _labelCurrentValue.Text = "";
                        break;
                }
            }
            catch
            {
                //ignore
            }
        }

        public void GenerateStructure(Form form)
        {
            var top = 100;
            var textIndex = 0;
            form.Controls.Add(_labelPrevValue);
            form.Controls.Add(_labelCurrentValue);
            for (var i = 0; i < 6; i++)
            {
                var left = 10;
                for (var j = 0; j < 4; j++)
                {
                    var btn = new Button
                    {
                        Text = _buttons[textIndex], Location = new Point(left, top),
                        Size = new Size(74, 47)
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