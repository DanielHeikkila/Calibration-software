using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test1_472;

namespace test1_472
{
    public partial class ChoiceForm : Form
    {
        private bool dragging = false;
        private Point dragStart;

        public ChoiceForm(int one)
        {
            InitializeComponent();
        }
        public ChoiceForm(string two)
        {
            InitializeComponent();
            comboBox2.Visible = true;
            label2.Visible = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStart = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point newPos = PointToScreen(new Point(e.X - dragStart.X, e.Y - dragStart.Y));
                DesktopLocation = newPos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string obtainEncoderChoice()
        {
            try
            {
                return comboBox1.Text;
            }
            catch 
            {
                return null;
            }
        }
        public string obtainEncoder1Choice()
        {
            try
            {
                return comboBox2.Text;
            }
            catch
            {
                return null;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
