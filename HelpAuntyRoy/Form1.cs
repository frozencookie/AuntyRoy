using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace HelpAuntyRoy
{
    public partial class Form1 : Form
    {
        private static string HouseFile = "House.txt";
        private static string RoyFile = ".\\bmp\\roy.gif";
        private static string WallFile = ".\\bmp\\wall.gif";
        private static string GroundFile = ".\\bmp\\ground.gif";
        private static string DoorFile = ".\\bmp\\door.gif";
        private static string DustFile = ".\\bmp\\dust.gif";
        private static string HamsterFile = ".\\bmp\\hamster.gif";
        private static string ShitFile = ".\\bmp\\shit.gif";
        private static string BlackFile = ".\\bmp\\black.gif";
        private static Image RoyImage = Image.FromFile(RoyFile);
        private static Image WallImage = Image.FromFile(WallFile);
        private static Image GroundImage = Image.FromFile(GroundFile);
        private static Image DoorImage = Image.FromFile(DoorFile);
        private static Image DustImage = Image.FromFile(DustFile);
        private static Image HamsterImage = Image.FromFile(HamsterFile);
        private static Image ShitImage = Image.FromFile(ShitFile);
        private static Image BlackImage = Image.FromFile(BlackFile);
        private const int WIDTH = 50;
        private const int DUST_WIDTH = 10;
        private static int[,] house = new int[800/50, 600/50];
        private static int[,] dust = new int[house.GetLength(0),house.GetLength(1)];
        private List<Door> doors = new List<Door>();
        private Roy roy = new Roy();
        private double probIniDust = 0.1;
        private double probDust = 0.01;
        private int maxDust = 10;
        private bool CreateRandomDust = false;
        private Random rand = new Random();
        private int clean = 500;
        private Font font = new Font("Arial", 12); 
        private Font fontLarge = new Font("Arial", 20);
        private Brush brush = new SolidBrush(Color.Red);
        private static bool running = false;
        private static int MoveSpeed = 1000;
        private DateTime lastLoop = DateTime.Now;
        private static int RoyMoveSteps = 0;
        private static DateTime StartTime = DateTime.Now;
        private Hamster ham = new Hamster();
        private bool haveHamster = false;
        private bool DrawHamsterView = false;
        private double probShit = 0.1;
        private bool HamIsFree = false;

        public Form1()
        {
            InitializeComponent();
            canvas.Size = new System.Drawing.Size(800, 600);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            CreateRandomDust = ckb_randomDust.Checked;

            MoveSpeed = 1000;
            try
            {
                MoveSpeed = int.Parse(txtb_moveTime.Text);
            }
            catch(Exception ex)
            {
                ex = null;
            }

            try
            {
                probDust = double.Parse(txtb_probDust.Text);
            }
            catch (Exception ex)
            {
                ex = null;
            }

            haveHamster = ckb_hamster.Checked;
            if (haveHamster)
            {
                DrawHamsterView = ckb_hansterView.Checked;
            }
            else
            {
                DrawHamsterView = false;
            }

            try
            {
                probShit = double.Parse(txtb_hamsterShit.Text);
            }
            catch (Exception ex)
            {
                ex = null;
            }
            ham.probShit = probShit;

            btn_start.Enabled = false;

            this.ReadHouse(HouseFile);

            int[] royxy = this.GetRoyIniXY();
            roy.Row = royxy[0];
            roy.Col = royxy[1];

            this.IniDust();

            RoyMoveSteps = 0;
            StartTime = DateTime.Now;

            this.Update(RoyMove.None, HamsterMove.None);

            this.DrawAll();
            
            running = true;
            Thread loop = new Thread(new ThreadStart(LoopThread));
            loop.IsBackground = true;
            lastLoop = DateTime.Now;
            loop.Start();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            running = false;
            btn_start.Enabled = true;
        }

        private void LoopThread()
        {
            while (true)
            {
                if (!running)
                {
                    return;
                }
                DateTime now = DateTime.Now;
                if ((now - lastLoop).Ticks >= MoveSpeed * 10000)
                {
                    if (CreateRandomDust)
                    {
                        this.DynamicDust();
                    }

                    HamsterMove hmove = HamsterMove.None;
                    if(haveHamster)
                    {
                        if(HamIsFree)
                        {
                            ham.LookAround(this.WhatHamSee());
                            hmove = ham.NextMove();
                        }
                        else
                        {
                            if(rand.Next(100)<10)
                            {
                                this.CreateHamster();
                            }
                        }
                    }

                    roy.LookAround(this.WhatRoySee(), this.DoorsRoySee());
                    RoyMove move = roy.NextMove();

                    this.Update(move, hmove);

                    clean = 500;
                    for (int i = 0; i < dust.GetLength(0); i++)
                        for (int j = 0; j < dust.GetLength(1); j++)
                        {
                            clean -= dust[i, j];
                        }

                    if(clean <= -500)
                    {
                        running = false;
                    }

                    this.DrawAll();
                    lastLoop = now;
                }
            }
        }

        private int[] GetRoyIniXY()
        {
            int[] xy = new int[2];
            for (int i = 0; i < house.GetLength(0); i++)
                for (int j = 0; j < house.GetLength(1); j++)
                {
                    if (house[i, j] > 0)
                    {
                        xy[0] = i;
                        xy[1] = j;
                        return xy;
                    }
                }
            return xy;
        }

        private void ReadHouse(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    
                    for(int j =0;j<house.GetLength(1);j++)
                    {
                        string line = sr.ReadLine();
                        if(line == null || line =="")
                        {
                            for(int i=0;i<house.GetLength(0);i++)
                            {
                                house[i, j] = -1;
                            }
                        }
                        else
                        {
                            string[] strs = line.Split(new char[] { ',' });
                            for (int i = 0; i < house.GetLength(0); i++)
                            {
                                if (i < strs.Length)
                                {
                                    house[i, j] = int.Parse(strs[i]);
                                }
                                else
                                {
                                    house[i, j] = -1;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("House文件无法打开！");
                for(int i =0;i<house.GetLength(0);i++)
                    for (int j = 0; j < house.GetLength(1); j++)
                    {
                        house[i, j] = -1;
                    }
            }

            for (int i = 0; i < house.GetLength(0); i++)
                for (int j = 0; j < house.GetLength(1); j++)
                {
                    if(house[i, j] == 0)
                    {
                        Door door = new Door(i, j);
                        if (i - 1 >= 0 && house[i - 1, j] > 0)
                        {
                            door.linkRooms.Add(house[i - 1, j]);
                            
                        }
                        if (i + 1 < house.GetLength(0) && house[i + 1, j] > 0)
                        {
                            door.linkRooms.Add(house[i +1, j]);
                            
                        }
                        if (j - 1 >= 0 && house[i, j-1] > 0)
                        {
                            door.linkRooms.Add(house[i, j-1]);

                        }
                        if (j + 1 < house.GetLength(1) && house[i, j+1] > 0)
                        {
                            door.linkRooms.Add(house[i, j+1]);

                        }

                        doors.Add(door);
                    }
                }
        }

        private void IniDust()
        {
            for (int i = 0; i < dust.GetLength(0); i++)
                for (int j = 0; j < dust.GetLength(1); j++)
                {
                    dust[i,j]=0;
                    if(house[i,j]>0)
                    {
                        if(rand.Next(99)/100.0<probIniDust)
                        {
                            dust[i, j] = rand.Next(1,maxDust);
                        }
                    }
                }
        }

        private void DynamicDust()
        {
            for (int i = 0; i < dust.GetLength(0); i++)
                for (int j = 0; j < dust.GetLength(1); j++)
                {
                    if (house[i, j] > 0)
                    {
                        if (rand.Next(999) / 1000.0 < probDust && dust[i, j] < 10)
                        {
                            dust[i, j] += rand.Next(maxDust - dust[i, j]);
                        }
                    }
                }
        }

        private void CreateHamster()
        {
            int[] rows = new int[house.GetLength(0)];
            for(int i =0;i<rows.Length;i++)
            {
                rows[i] = i;
            }
            for (int i = 0; i < rows.Length; i++)
            {
                int randi = rand.Next(rows.Length);
                int _i = rows[randi];
                rows[randi] = rows[i];
                rows[i] = _i;
            }
            int[] cols = new int[house.GetLength(1)];
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = i;
            }
            for (int i = 0; i < cols.Length; i++)
            {
                int randi = rand.Next(cols.Length);
                int _i = cols[randi];
                cols[randi] = cols[i];
                cols[i] = _i;
            }
            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < cols.Length; j++ )
                {
                    if(house[rows[i],cols[j]] > 0)
                    {
                        if(Math.Abs( roy.Row - rows[i]) > 1 || Math.Abs(roy.Col - cols[j]) > 1)
                        {
                            ham.Row = rows[i];
                            ham.Col = cols[j];
                            HamIsFree = true;
                            return;
                        }
                    }
                }
                    
        }

        private int[,] WhatHamSee()
        {
            int[,] env = new int[house.GetLength(0),house.GetLength(1)];
            for(int i = 0; i < house.GetLength(0); i++)
                for(int j = 0; j<house.GetLength(1);j++)
                {
                    if(Math.Abs(i-ham.Row)>2 || Math.Abs(j-ham.Col)>2)
                    {
                        env[i, j] = -1;
                    }
                    else
                    {
                        env[i, j] = Math.Min(0, house[i, j]);
                    }
                }
            if(Math.Abs(roy.Row-ham.Row)<=2 && Math.Abs(roy.Col - ham.Col)<=2)
            {
                env[roy.Row,roy.Col] = 999;
            }

            return env;
        }

        private void Update(RoyMove move, HamsterMove hmove)
        {
            #region HamMove
            int hamDx = 0, hamDy = 0;
            #region switch
            switch (hmove)
            {
                case HamsterMove.Up:
                    hamDy = -1;
                    break;
                case HamsterMove.Down:
                    hamDy = 1;
                    break;
                case HamsterMove.Left:
                    hamDx = -1;
                    break;
                case HamsterMove.Right:
                    hamDx = 1;
                    break;
                case HamsterMove.UpLeft:
                    hamDy = -1;
                    hamDx = -1;
                    break;
                case HamsterMove.UpRight:
                    hamDy = -1;
                    hamDx = 1;
                    break;
                case HamsterMove.DownLeft:
                    hamDy = 1;
                    hamDx = -1;
                    break;
                case HamsterMove.DownRight:
                    hamDy = 1;
                    hamDx = 1;
                    break;
                case HamsterMove.Shit:
                    hamDx = 0;
                    hamDy = 0;
                    dust[ham.Row, ham.Col] = 50;
                    break;
            }
            #endregion

            if (ham.Row + hamDx < 0 || ham.Row + hamDx >= house.GetLength(0) || ham.Col + hamDy < 0 || ham.Col + hamDy >= house.GetLength(1) || house[ham.Row + hamDx, ham.Col + hamDy] < 0)
            {
                hamDx = 0;
                hamDy = 0;
            }
            else if (house[ham.Row + hamDx, ham.Col + hamDy] == 0)
            {
                int newdx = hamDx * 2;
                if (ham.Row + newdx < 0 || ham.Row + newdx >= house.GetLength(0) || ham.Col + hamDy < 0 || ham.Col + hamDy >= house.GetLength(1) || house[ham.Row + newdx, ham.Col + hamDy] <= 0)
                {
                    int newdy = hamDy * 2;
                    if (ham.Row + hamDx < 0 || ham.Row + hamDx >= house.GetLength(0) || ham.Col + newdy < 0 || ham.Col + newdy >= house.GetLength(1) || house[ham.Row + hamDx, ham.Col + newdy] <= 0)
                    {
                        hamDx = 0;
                        hamDy = 0;
                    }
                    else
                    {
                        hamDy = newdy;
                    }
                }
                else
                {
                    hamDx = newdx;
                }
            }

            ham.Row += hamDx;
            ham.Col += hamDy;

            #endregion

            #region RoyMove
            int dx = 0, dy = 0;
            #region switch
            switch (move)
            {
                case RoyMove.Up:
                    dy = -1;
                    break;
                case RoyMove.Down:
                    dy = 1;
                    break;
                case RoyMove.Left:
                    dx = -1;
                    break;
                case RoyMove.Right:
                    dx = 1;
                    break;
                case RoyMove.UpLeft:
                    dy = -1;
                    dx = -1;
                    break;
                case RoyMove.UpRight:
                    dy = -1;
                    dx = 1;
                    break;
                case RoyMove.DownLeft:
                    dy = 1;
                    dx = -1;
                    break;
                case RoyMove.DownRight:
                    dy = 1;
                    dx = 1;
                    break;
            }
            #endregion

            if (roy.Row + dx < 0 || roy.Row + dx >= house.GetLength(0) || roy.Col + dy < 0 || roy.Col + dy >= house.GetLength(1) || house[roy.Row + dx, roy.Col + dy] < 0)
            {
                dx = 0;
                dy = 0;
            }
            else if (house[roy.Row + dx, roy.Col + dy] == 0)
            {
                int newdx = dx * 2;
                if(roy.Row + newdx < 0 || roy.Row + newdx >= house.GetLength(0) || roy.Col + dy < 0 || roy.Col + dy >= house.GetLength(1) || house[roy.Row + newdx, roy.Col + dy] <= 0)
                {
                    int newdy = dy * 2;
                    if (roy.Row + dx < 0 || roy.Row + dx >= house.GetLength(0) || roy.Col + newdy < 0 || roy.Col + newdy >= house.GetLength(1) || house[roy.Row + dx, roy.Col + newdy] <= 0)
                    {
                        dx = 0;
                        dy = 0;
                    }
                    else
                    {
                        dy = newdy;
                    }
                }
                else
                {
                    dx = newdx;
                }
            }

            roy.Row += dx;
            roy.Col += dy;

            if(dx != 0 || dy!=0)
            {
                RoyMoveSteps++;
            }

            dust[roy.Row, roy.Col] = 0;
            if(roy.Row==ham.Row && roy.Col==ham.Col)
            {
                HamIsFree = false;
            }
            #endregion
        }

        private int[,] WhatRoySee()
        {
            int[,] _seen = new int[house.GetLength(0),house.GetLength(1)];
            int room = house[roy.Row, roy.Col];
            if(room > 0)
            {
                for (int i = 0; i < house.GetLength(0); i++)
                    for (int j = 0; j < house.GetLength(1); j++)
                    {
                        if(house[i,j]==room)
                        {
                            _seen[i, j] = dust[i, j];
                        }
                        else if (house[i, j] == 0)
                        {
                            _seen[i, j] = 0;
                        }
                        else
                        {
                            _seen[i, j] = -1;
                        }
                    }

                if(HamIsFree)
                {
                    int hroom = house[ham.Row, ham.Col];
                    if(room==hroom)
                    {
                        _seen[ham.Row, ham.Col] = 100;
                    }
                }
            }
            else
            {
                MessageBox.Show("严重错误！！");
                throw (new Exception("RoomIdError"));
            }

            return _seen;
        }

        private List<Door> DoorsRoySee()
        {
            List<Door> seeDoors = new List<Door>();
            int room = house[roy.Row, roy.Col];
            if(room > 0)
            {
                foreach(Door door in doors)
                {
                    if(door.linkRooms.Contains(room))
                    {
                        seeDoors.Add(door);
                    }
                }
            }
            else
            {
                MessageBox.Show("严重错误！！");
                throw (new Exception("RoomIdError"));
            }

            return seeDoors;
        }

        private void DrawAll()
        {
            canvas.Invalidate();
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            DrawRoom(e.Graphics);
            DrawDust(e.Graphics);
            DrawPeople(e.Graphics);
            DrawClean(e.Graphics);
            if(DrawHamsterView)
            {
                DrawViewBlack(e.Graphics);
            }
        }

        private void DrawRoom(Graphics g)
        {
            for (int i = 0; i < house.GetLength(0); i++)
                for (int j = 0; j < house.GetLength(1); j++)
                {
                    Rectangle Rect = new Rectangle(i * WIDTH, j * WIDTH, WIDTH, WIDTH);

                    if(house[i,j]<0)
                    {
                        g.DrawImage(WallImage, Rect);
                    }
                    else if(house[i,j]==0)
                    {
                        g.DrawImage(DoorImage, Rect);
                    }
                    else
                    {
                        g.DrawImage(GroundImage, Rect);
                    }
                }
        }

        private void DrawPeople(Graphics g)
        {
            Rectangle Rect = new Rectangle(roy.Row * WIDTH, roy.Col * WIDTH, WIDTH, WIDTH);
            g.DrawImage(RoyImage, Rect);
            if (HamIsFree)
            {
                Rectangle Rect2 = new Rectangle(ham.Row * WIDTH, ham.Col * WIDTH, WIDTH, WIDTH);
                g.DrawImage(HamsterImage, Rect2);
            }
        }

        private void DrawDust(Graphics g)
        {
            for (int i = 0; i < dust.GetLength(0); i++)
                for (int j = 0; j < dust.GetLength(1); j++)
                {
                    if(dust[i,j]>0)
                    {
                        if (dust[i, j] <= 10)
                        {
                            for (int k = 0; k < dust[i, j]; k++)
                            {
                                int dx = rand.Next(WIDTH - DUST_WIDTH);
                                int dy = rand.Next(WIDTH - DUST_WIDTH);
                                Rectangle Rect = new Rectangle(i * WIDTH + dx, j * WIDTH + dy, DUST_WIDTH, DUST_WIDTH);
                                g.DrawImage(DustImage, Rect);
                            }
                        }
                        else if (dust[i, j] == 50)
                        {
                            Rectangle Rect = new Rectangle(i * WIDTH, j * WIDTH, WIDTH, WIDTH);
                            g.DrawImage(ShitImage, Rect);
                        }
                    }
                }
        }

        private void DrawClean(Graphics g)
        {
            RectangleF Rect = new RectangleF(650,20,150,50);
            g.DrawString("Clean: " + clean.ToString(), font, brush, Rect);
            RectangleF Rect3 = new RectangleF(650, 100, 150, 50);
            g.DrawString("Roy Moved " + RoyMoveSteps.ToString(), font, brush, Rect3);
            RectangleF Rect4 = new RectangleF(650, 180, 150, 50);
            g.DrawString("Times: " + ((int)(DateTime.Now-StartTime).TotalSeconds).ToString(), font, brush, Rect4);
            RectangleF Rect2 = new RectangleF(300, 250, 500, 100);
            if(clean <= -500)
            {
                g.DrawString("Game Over", fontLarge, brush, Rect2);
            }
        }

        private void DrawViewBlack(Graphics g)
        {
            for(int i = 0; i < house.GetLength(0); i++)
                for (int j = 0; j < house.GetLength(1); j++)
                {
                    if(Math.Abs(i-ham.Row) >2 || Math.Abs(j-ham.Col)>=2)
                    {
                        Rectangle Rect2 = new Rectangle(i * WIDTH, j * WIDTH, WIDTH, WIDTH);
                        g.DrawImage(BlackImage, Rect2);
                    }
                }
        }
    }
}
