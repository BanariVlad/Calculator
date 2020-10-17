using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalculatorOOPv1._0.Classes;

namespace CalculatorOOPv1._0
{
    public partial class Form1 : Form
    {
        private readonly Display _display = new Display();

        public Form1()
        {
            _display.GenerateStructure(this);
            InitializeComponent();
        }
    }
}