using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using test1_472;

namespace Splash_Screen_nTest
{
    public partial class SplashScreen1 : Form
    {
        public SplashScreen1()
        {
            InitializeComponent();
        }

        int time = 10;

        private void SplashScreen1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time -= 1;
            }
            else if (time == 0)
            {
                this.Close();
            }
        }
    }
}
