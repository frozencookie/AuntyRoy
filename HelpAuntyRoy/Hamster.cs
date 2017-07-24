using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpAuntyRoy
{

    enum HamsterMove
    { None, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight, Shit }

    class Hamster
    {
        private Grid[,] envirment; //Roy=999, ground or door=0, unsee or wall=-1
        private int row, col;
        public double probShit = 0.1;
        private int haveShitted = 0;
        private Random rand = new Random();

        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                if (value >= 0)
                {
                    row = value;
                }
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }

            set
            {
                if (value >= 0)
                {
                    this.col = value;
                }
            }
        }

        public void LookAround(int[,] env)
        {

            this.envirment = new Grid[env.GetLength(0), env.GetLength(1)];
            for (int i = 0; i < env.GetLength(0); i++)
                for (int j = 0; j < env.GetLength(1); j++)
                {
                    envirment[i, j] = new Grid(i, j, env[i, j]);
                }
        }

        public HamsterMove NextMove()
        {

             if( NaturalCall())
             {
                 return HamsterMove.Shit;
             }
             haveShitted++;

            // 为Hamster设计的行动代码写在这里
            foreach(Grid grid in envirment)
            {
                if(grid.Value ==999)
                {
                    int drow = row - grid.Row;
                    int dcol = col - grid.Col;

                    Grid target = envirment[row-1,col-1];
                    int maxdistance = (target.Row - grid.Row) * (target.Row - grid.Row) + (target.Col - grid.Col) * (target.Col - grid.Col);
                    for(int i =-1;i<=1;i++)
                        for(int j=-1;j<=1;j++)
                        {
                            Grid newTarget = envirment[row + i, col + j];
                            if (newTarget.Value == 0)
                            {
                                int dis = (newTarget.Row - grid.Row) * (newTarget.Row - grid.Row) + (newTarget.Col - grid.Col) * (newTarget.Col - grid.Col);
                                if(dis>maxdistance)
                                {
                                    target = newTarget;
                                    maxdistance = dis;
                                }
                            }
                        }
                    return Moveto(target);
                }
            }

            return (HamsterMove)((int)(rand.Next(999) / 1000.0 / 0.125) + 1);
        }

        private bool NaturalCall()
        {
            if(haveShitted >= 5)
            {
                if(rand.Next(999)/1000.0 < probShit)
                {
                    return true;
                }
            }

            return false;
        }

        private HamsterMove Moveto(Grid grid)
        {
            return Moveto(grid.Row, grid.Col);
        }

        private HamsterMove Moveto(int target_row, int target_col)
        {
            int state = 11;
            if (target_row > row && row + 1 < envirment.GetLength(0) && envirment[row + 1, col].Value >= 0)
            {
                state += 10;
            }
            else if (target_row < row && row - 1 >= 0 && envirment[row - 1, col].Value >= 0)
            {
                state -= 10;
            }

            if (target_col > col && col + 1 < envirment.GetLength(1) && envirment[row, col + 1].Value >= 0)
            {
                state += 1;
            }
            else if (target_col < col && col - 1 >= 0 && envirment[row, col - 1].Value >= 0)
            {
                state -= 1;
            }

            switch (state)
            {
                case 11:
                    return HamsterMove.None;
                case 12:
                    return HamsterMove.Down;
                case 10:
                    return HamsterMove.Up;
                case 21:
                    return HamsterMove.Right;
                case 22:
                    return HamsterMove.DownRight;
                case 20:
                    return HamsterMove.UpRight;
                case 1:
                    return HamsterMove.Left;
                case 2:
                    return HamsterMove.DownLeft;
                case 0:
                    return HamsterMove.UpLeft;
                default:
                    return HamsterMove.None;
            }
        }



    }
}
