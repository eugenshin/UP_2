using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    internal class Grass : Object
    {
        public Grass()
        {
            place.X = random.Next(Constants.TileCount);
            place.Y = random.Next(Constants.TileCount);
            brush = new SolidBrush(Color.Green);
        }
    }
}
