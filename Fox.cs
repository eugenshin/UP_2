using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    internal class Fox : Entity
    {
        public Fox()
        {
            satiety = 15;
            brush = new SolidBrush(Color.OrangeRed);
            place.X = random.Next(Constants.TileCount - 1);
            place.Y = random.Next(Constants.TileCount - 1);
        }
        public Fox(Point p)
        {
            satiety = 15;
            place = p;
            brush = new SolidBrush(Color.OrangeRed);
        }

        protected override bool CanGoThrough(Object obj)
        {
            if ((obj is Grass) || (obj is Mouse))
                return true;
            return false;
        }

        protected override Entity Birth()
        {
            return new Fox(place);
        }

        protected override int IsFood(Object obj)
        {
            if ((obj is Mouse) && (satiety <= 15))
                return 10;
            return 0;
        }

        protected override int IsPartner(Object obj)
        {
            if ((obj is Fox) && (obj != this) && (satiety > 15))
                return -5;
            return 0;
        }
    }
}
