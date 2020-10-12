using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorOOPv1._0.Classes
{
    public class CustomBtn : Button
    {
        public void CreateBtn(Form form, string text, int x, int y, int width, int height, EventHandler e)
        {
            var btn = new Button {Text = text, Location = new Point(x, y), Size = new Size(width, height)};
            btn.Click += e;
            
            form.Controls.Add(btn);
        }
    }
}