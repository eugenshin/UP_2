using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    abstract internal class Object
    {
        protected static Random random = new Random();
        protected Point place;
        protected SolidBrush brush;
        public SolidBrush Brush { get { return brush; } }
        public Point Place { get { return place; } }
    }
}
