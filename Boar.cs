using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    internal class Boar : Entity
    {
        public Boar()
        {
            satiety = 10;
            brush = new SolidBrush(Color.SaddleBrown);
            place.X = random.Next(Constants.TileCount - 1);
            place.Y = random.Next(Constants.TileCount - 1);
        }
        public Boar(Point p)
        {
            satiety = 10;
            place = p;
            brush = new SolidBrush(Color.SaddleBrown);
        }

        protected override bool CanGoThrough(Object obj)
        {
            if ((obj is Grass) || (obj is Mouse))
                return true;
            return false;
        }

        protected override Entity Birth()
        {
            return new Boar(place);
        }

        protected override int IsFood(Object obj)
        {
            if (satiety <= 10) {
                if (obj is Grass)
                    return 5;
                if (obj is Mouse)
                    return 10;
            }
            return 0;
        }

        protected override int IsPartner(Object obj)
        {
            if ((obj is Boar) && (obj != this) && (satiety > 10))
                return -5;
            return 0;
        }
    }
}
