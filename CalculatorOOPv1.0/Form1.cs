using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorOOPv1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            var top = 50;
            for (var i = 0; i < 6; i++)
            {
                var left = 10;
                for (var j = 0; j < 4; j++)
                {
                    var button = new Button();
                    button.Left = left;
                    button.Top = top;

                    this.Controls.Add(button);
                    left += button.Width + 2;
                }

                top += 30;
                left = 10;
            };
            InitializeComponent();
        }

    }
}
