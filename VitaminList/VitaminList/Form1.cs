using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VitaminList
{
    public partial class Form1 : Form
    {
        private VitaminsData data = null;

        public Form1()
        {
            InitializeComponent();
            flowLayoutPanel1.BackColor = Color.FromArgb(255, 232, 232);
            data = new VitaminsData(flowLayoutPanel1);
        }



        private void button5_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(500 + (int)Math.Pow(2,data.avarage-1)*200, 1000 + data.avarage * 60);
            data.VievDefults();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(500, 1000);
            flowLayoutPanel1.Controls.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string inf = textBox1.Text.ToString();
            textBox1.Text = "";
            flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(500 + (int)Math.Pow(2, data.avarage - 1) * 200, 1000 + data.avarage * 60);
            data.FindByName(inf);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox4.Text != "" && textBox3.Text != "") {
                data.AddNewElement(textBox3.Text.ToString(), Int32.Parse(textBox4.Text.ToString()));
            }
            textBox4.Text = "";
            textBox3.Text = "";
            flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(500 + (int)Math.Pow(2, data.avarage - 1) * 200, 1000 + data.avarage * 60);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            data.DeleteByName(textBox2.Text.ToString());
            textBox2.Text = "";
            flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(500 + (int)Math.Pow(2, data.avarage - 1) * 200, 1000 + data.avarage * 60);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
    


}
