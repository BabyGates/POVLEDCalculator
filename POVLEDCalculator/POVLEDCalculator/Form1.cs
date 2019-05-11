using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POVLEDCalculator
{
    public partial class Form1 : Form
    {
        const int colDelay = 10;
        const int ledCountVert = 120;
        const int ledCountHori = 5;
        const int offsetFromPole = 17 + (ledCountVert/2);
        int maxX;
        int maxY;
        int[,] mainList = new int[ledCountHori,ledCountVert];
        public Form1()
        {
            InitializeComponent();
            foreach(Control control in this.Controls)
            {
                if (control is Panel)
                {
                    control.Click += panel_Enter;
                }
            }
        }

        private void panel_Enter(object sender, EventArgs e)
        {
            
            var p = (Panel)sender;
            if (p.BackColor == Color.White)
            {
                p.BackColor = Color.Black;
            }
            else
            {
                p.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    Point locationOnForm = control.FindForm().PointToClient(control.Parent.PointToScreen(control.Location));
                    locationOnForm.X -= 7;
                    locationOnForm.Y -= 9;
                    int x = (locationOnForm.X / 24);
                    int y = (locationOnForm.Y / 23) + offsetFromPole; //offset from top of globe
                    if (y > maxY) { maxY = y; } //maybe needed idk dude. hardcoding numbers freaks me out
                    if (control.BackColor == Color.Black)
                    {
                        mainList[x, y] = 1;
                    }
                    else
                    {
                        mainList[x, y] = 0;
                    }
                }
            }
            //software fix for led strip being installed upsidedown. oops
            
            //print
            for (int i = 0; i < ledCountHori; i++)
            {
                int counter = 0;
                for (int j = 0; j < ledCountVert; j++)
                {
                    if (mainList[i, j] == 1)
                    {
                        textBox1.AppendText("currChar[" + i + "][" + j + "] = '1';");
                        textBox1.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        textBox1.AppendText("currChar[" + i + "][" + j + "] = '0';");
                        textBox1.AppendText(Environment.NewLine);
                    }
                    counter++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    control.BackColor = Color.White;
                }
            }
        }
    }
}
