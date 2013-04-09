using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCam_01._04._2013
{
    public partial class DialogThreshold : Form
    {
        public System.Windows.Forms.DialogResult response;
        public int R_low = 0, G_low = 0, B_low = 0;
        public int R_high = 255, G_high = 255, B_high = 255;
        Threshold parent;

        public DialogThreshold(Threshold parent)
        {
            this.parent = parent;
            InitializeComponent();
            trackBar4.Value = 255;
            trackBar5.Value = 255;
            trackBar6.Value = 255;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            response = System.Windows.Forms.DialogResult.OK;
            int.TryParse(textBox1.Text, out R_low);
            int.TryParse(textBox2.Text, out G_low);
            int.TryParse(textBox3.Text, out B_low);
            int.TryParse(textBox4.Text, out R_high);
            int.TryParse(textBox5.Text, out G_high);
            int.TryParse(textBox6.Text, out B_high);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            response = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void DialogMirror_Shown(object sender, EventArgs e)
        {
            textBox1.Text = R_low.ToString();
            textBox2.Text = G_low.ToString();
            textBox3.Text = B_low.ToString();
            textBox4.Text = R_high.ToString();
            textBox5.Text = G_high.ToString();
            textBox6.Text = B_high.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            this.parent.R_low = trackBar1.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
            this.parent.G_low = trackBar2.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();
            this.parent.B_low = trackBar3.Value;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = trackBar4.Value.ToString();
            this.parent.R_high = trackBar4.Value;
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            textBox5.Text = trackBar5.Value.ToString();
            this.parent.G_high = trackBar5.Value;
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = trackBar6.Value.ToString();
            this.parent.B_high = trackBar6.Value;
        }

        private void DialogThreshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close(); 
            }
        }
    }
}
