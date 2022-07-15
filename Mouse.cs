using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    internal class Mouse : Entity
    {
        public Mouse()
        {
            satiety = 10;
            brush = new SolidBrush(Color.Gray);
            place.X = random.Next(Constants.TileCount);
            place.Y = random.Next(Constants.TileCount);
        }
        public Mouse(Point p)
        {
            satiety = 10;
            brush = new SolidBrush(Color.Gray);
            place = p;
        }

        protected override bool CanGoThrough(Object obj)
        {
            if(obj is Grass)
                return true;
            return false;
        }

        protected override Entity Birth()
        {
            return new Mouse(place);
        }

        protected override int IsFood(Object obj)
        {
            if ((obj is Grass) && (satiety <= 10))
                return 5;
            return 0;
        }

        protected override int IsPartner(Object obj)
        {
            if ((obj is Mouse) && (obj != this) && (satiety > 10))
                return -5;
            return 0;
        }
    }
}
