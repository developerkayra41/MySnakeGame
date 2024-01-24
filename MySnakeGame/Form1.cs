using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySnakeGame
{
    public partial class Form1 : Form
    {
        Panel bait = new Panel();
        Random rnd = new Random();

        int locX;
        int locY;

        int yemX;
        int yemY;


        Panel snake = new Panel();
        List<Panel> snakes = new List<Panel>();


        string yon = "ust";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GoFullscreen(true);
            panel1.Paint += Panel1_Paint;
            AddBait();
            AddSnake();

        }

        #region Form Ayarları
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor = Color.Lime;
            int borderWidth = 5;

            Panel panel = (Panel)sender;

            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, panel.Width - 1, panel.Height - 1);

            }
        }
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }
        }
        #endregion

        #region AddBait
        private void AddBait()
        {


            yemX = rnd.Next(1880);
            yemY = rnd.Next(1000);

            yemX -= yemX % 40;
            yemY -= yemY % 40;

            //bait = new Panel();
            bait.BackColor = Color.Red;
            bait.Size = new Size(40, 40);
            bait.Location = new Point(yemX, yemY);
            panel1.Controls.Add(bait);

        }
        #endregion

        #region AddSnake
        private void AddSnake()
        {

            snake.BackColor = Color.Lime;
            snake.Size = new Size(40, 40);
            snake.Location = new Point(400, 400);
            panel1.Controls.Add(snake);
            snakes.Add(snake);
        }
        #endregion

        #region Keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) yon = "ust";

            if (e.KeyCode == Keys.S) yon = "alt";

            if (e.KeyCode == Keys.D) yon = "sag";

            if (e.KeyCode == Keys.A) yon = "sol";

        }
        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {
            locX = snakes[0].Location.X;
            locY = snakes[0].Location.Y;

            DidSnakeEat();
            FollowHeadToQueue();
            HaveCollision();

            if (yon == "ust")
            {
        


                if (locY > 0)
                {
                    locY -= 40;
                }
                else
                {
                    locY = 960;
                }
            }


            if (yon == "alt")
            {
           

                if (locY < 960)
                {
                    locY += 40;
                }
                else
                {
                    locY = 0;
                }
            }

            if (yon == "sag")
            {
      

                if (locX < 1880)
                {
                    locX += 40;
                }
                else
                {
                    locX = 0;
                }
            }

            if (yon == "sol")
            {
 
                if (locX > 0)
                {
                    locX -= 40;
                }
                else
                {
                    locX = 1880;
                }
            }
            snakes[0].Location = new Point(locX, locY);
        }

        private void DidSnakeEat()
        {
            if (snakes[0].Location == bait.Location)
            {
                panel1.Controls.Remove(bait);
                AddBait();
                AddQueue();
            }
        }

        private void AddQueue()
        {
            Panel queue = new Panel();
            queue.BackColor = Color.Lime;
            queue.Size = new Size(40, 40);
            panel1.Controls.Add(queue);
            snakes.Add(queue);
        }

        private void FollowHeadToQueue()
        {
            for (int i = snakes.Count - 1; i > 0; i--)
            {
                snakes[i].Location = snakes[i - 1].Location;
            }
        }
        private void HaveCollision()
        {
            for (int i = 2; i < snakes.Count; i++)
            {
                if (snakes[0].Location == snakes[i].Location) { timer1.Stop(); label1.Visible = true; }
            }
        }
    }
}
