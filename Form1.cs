using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;

namespace KP_2
{
    public partial class Form1 : Form
    {
        List<Object> objects = new List<Object>();
        Rectangle[,] map = new Rectangle[Constants.TileCount, Constants.TileCount];
        int GrassCount = 0, MouseCount = 0, FoxCount = 0, BoarCount = 0, GrassGrowingSpeed = 0;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            for (int i = 0; i < Constants.TileCount; i++)
                for (int j = 0; j < Constants.TileCount; j++)
                    map[i, j] = new Rectangle(Constants.SideBarSize + i * Constants.TileSize, j * Constants.TileSize, Constants.TileSize, Constants.TileSize);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in objects)
                e.Graphics.FillRectangle(i.Brush, map[i.Place.X, i.Place.Y]);
            for (int i = 0; i < Constants.TileCount; i++)
                for (int j = 0; j < Constants.TileCount; j++)
                    e.Graphics.DrawRectangle(new Pen(Color.Black), map[i, j]);

            label2.Text = objects.Count.ToString();
            int grass = 0, mouse = 0, fox = 0, boar = 0;
            foreach (var i in objects) {
                if (i is Grass)
                    ++grass;
                else if (i is Mouse)
                    ++mouse;
                else if (i is Fox)
                    ++fox;
                else if (i is Boar)
                    ++boar;
            }
            label9.Text = grass.ToString();
            label10.Text = mouse.ToString();
            label11.Text = fox.ToString();
            label12.Text = boar.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
                numericUpDown1.Value = GrassCount;
            else if (listBox1.SelectedIndex == 1)
                numericUpDown1.Value = MouseCount;
            else if (listBox1.SelectedIndex == 2)
                numericUpDown1.Value = FoxCount;
            else if (listBox1.SelectedIndex == 3)
                numericUpDown1.Value = BoarCount;
            else if (listBox1.SelectedIndex == 4)
                numericUpDown1.Value = GrassGrowingSpeed;
        }

        private void globalTimer_Tick(object sender, EventArgs e)
        {
            if ((!checkBox1.Checked) && (button1.Text == "Завершить"))
            {
                for (int i = 0; i < GrassGrowingSpeed; i++) {
                    Grass grass = new Grass();
                    if (objects.TrueForAll(k => k.Place != grass.Place))
                        objects.Insert(0, grass);
                }

                foreach (var i in objects.ToArray())
                {
                    if (i is Entity)
                    {
                        (i as Entity).SeekGoal(objects);
                        (i as Entity).Move(objects);
                        (i as Entity).Execute(objects);
                        (i as Entity).Hunger(objects);
                    }
                }
            }
            Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value >= Constants.MaxObjects)
                numericUpDown1.Value = Constants.MaxObjects;
            if (listBox1.SelectedIndex == 0)
                GrassCount = (int)numericUpDown1.Value;
            else if (listBox1.SelectedIndex == 1)
                MouseCount = (int)numericUpDown1.Value;
            else if (listBox1.SelectedIndex == 2)
                FoxCount = (int)numericUpDown1.Value;
            else if (listBox1.SelectedIndex == 3)
                BoarCount = (int)numericUpDown1.Value;
            else if (listBox1.SelectedIndex == 4)
                GrassGrowingSpeed = (int)numericUpDown1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            globalTimer.Interval = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Завершить")
            {
                objects.Clear();
                button1.Text = "Старт";
            }
            else
            {
                Object obj;
                for (int i = 0; i < GrassCount; i++)
                {
                    obj = new Grass();
                    if (objects.TrueForAll(k => k.Place != obj.Place))
                        objects.Add(obj);
                    else
                        --i;
                }
                for (int i = 0; i < MouseCount; i++)
                {
                    obj = new Mouse();
                    if (objects.TrueForAll(k => k.Place != obj.Place))
                        objects.Add(obj);
                    else
                        --i;
                }
                for (int i = 0; i < FoxCount; i++)
                {
                    obj = new Fox();
                    if (objects.TrueForAll(k => k.Place != obj.Place))
                        objects.Add(obj);
                    else
                        --i;
                }
                for (int i = 0; i < BoarCount; i++)
                {
                    obj = new Boar();
                    if (objects.TrueForAll(k => k.Place != obj.Place))
                        objects.Add(obj);
                    else
                        --i;
                }
                button1.Text = "Завершить";
            }
            Refresh();
        }
    }
}
