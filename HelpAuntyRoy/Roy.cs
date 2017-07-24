using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpAuntyRoy
{
    enum RoyMove
    { None, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight }

    class Roy
    {
        private Grid[,] envirment; //ground=0, dust=1~10, shit=50, hamster=100, unsee or wall=-1
        private List<Door> whereIsDoor;
        private int row, col;
        private Hashtable timesIntoDoors = new Hashtable();

        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                if(value >= 0)
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
        
        public void LookAround(int[,] env, List<Door> doors)
        {

            this.envirment = new Grid[env.GetLength(0),env.GetLength(1)];
            for (int i = 0; i < env.GetLength(0); i++)
                for (int j = 0; j < env.GetLength(1);j++ )
                {
                    envirment[i, j] = new Grid(i, j, env[i, j]);
                }
                this.whereIsDoor = doors;
        }

        public RoyMove NextMove()
        {
            // 为Roy设计的行动代码写在这里
            Grid target = new Grid(-1,-1,-1);
            int minDistance = 9999;
            foreach(Grid g in envirment)
            {
                if (g.Value > 0)
                {
                    int distance = (g.Row - row) * (g.Row - row) + (g.Col - col) * (g.Col - col);
                    if (distance < minDistance)
                    {
                        target = g;
                        minDistance = distance;
                    }
                }
            }

            if (target.Row == -1)
            {
                
                if (whereIsDoor.Count > 0)
                {
                    Door targetDoor = whereIsDoor[0];
                    int minTimesDoor = 10^20;
                    foreach(Door door in whereIsDoor)
                    {
                        if(!timesIntoDoors.Contains(door))
                        {
                            timesIntoDoors.Add(door, 0);
                            targetDoor = door;
                            break;
                        }
                        else
                        {
                            int times = (int)timesIntoDoors[door];
                            if(times<minTimesDoor)
                            {
                                minTimesDoor = times;
                                targetDoor = door;
                            }
                        }
                    }

                    int distance = (targetDoor.X-row)*(targetDoor.X-row)+(targetDoor.Y-col)*(targetDoor.Y-col);
                    if(distance<=2)
                    {
                        timesIntoDoors[targetDoor] = (int)timesIntoDoors[targetDoor]+1;
                    }

                    return MovetoDoor(targetDoor);
                }
                else
                {
                    return RoyMove.None;
                }
            }
            else
            {
                return Moveto(target);
            }

            
        }

        private  RoyMove Moveto(Grid grid)
        {
            return Moveto(grid.Row, grid.Col);
        }

        private RoyMove Moveto(int target_row, int target_col)
        {
            Random rand = new Random();
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

            switch(state)
            {
                case 11:
                    return RoyMove.None;
                case 12:
                    return RoyMove.Down;
                case 10:
                    return RoyMove.Up;
                case 21:
                    return RoyMove.Right;
                case 22:
                    if (envirment[row + 1, col + 1].Value >= 0)
                    {
                        return RoyMove.DownRight;
                    }
                    else
                    {
                        if (rand.Next(2) < 1)
                        {
                            return RoyMove.Down;
                        }
                        else
                        {
                            return RoyMove.Right;
                        }
                    }
                case 20:
                    if (envirment[row + 1, col - 1].Value >= 0)
                    {
                        return RoyMove.UpRight;
                    }
                    else
                    {
                        if (rand.Next(2) < 1)
                        {
                            return RoyMove.Up;
                        }
                        else
                        {
                            return RoyMove.Right;
                        }
                    }
                case 1:
                    return RoyMove.Left;
                case 2:
                    if (envirment[row - 1, col + 1].Value>=0)
                    {
                        return RoyMove.DownLeft;
                    }
                    else
                    {
                        if(rand.Next(2)<1)
                        {
                            return RoyMove.Down;
                        }
                        else
                        {
                            return RoyMove.Left;
                        }
                    }
                case 0:
                    if (envirment[row - 1, col - 1].Value >= 0)
                    {
                        return RoyMove.UpLeft;
                    }
                    else
                    {
                        if (rand.Next(2) < 1)
                        {
                            return RoyMove.Up;
                        }
                        else
                        {
                            return RoyMove.Left;
                        }
                    }
                default:
                    return RoyMove.None;
            }
        }
        
        private RoyMove MovetoDoor(Door door)
        {
            return Moveto(door.X, door.Y);
        }

    }
}
