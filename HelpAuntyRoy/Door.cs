using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpAuntyRoy
{
    class Door
    {
        private int x, y;
        public List<int> linkRooms = new List<int>();

        public int X
        {
            set
            {
                if (value > 0)
                {
                    x = value;
                }
            }

            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value >= 0)
                {
                    this.y = value;
                }
            }
        }

        public Door(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
