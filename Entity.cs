using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KP_2
{
    abstract internal class Entity : Object
    {
        protected int satiety, path;
        protected Object goal;
        public Object Goal { get { return goal; } set { goal = null; } }
        public void Hunger(List<Object> obj)
        {
            if (0 >= --satiety)
            {
                foreach (var i in obj)
                    if (i is Entity)
                        if ((i as Entity).Goal == goal)
                            (i as Entity).Goal = null;
                obj.Remove(this);
            }
        }
        public void Move(List<Object> obj)
        {
            if (goal != null)
            {
                if (goal.Place.X > Place.X)
                {
                    if (obj.TrueForAll(k => CanGoThrough(k) || (k.Place.X != place.X + 1) || (k.Place.Y != place.Y) || (k == this)))
                    {
                        ++place.X;
                        return;
                    }
                }
                else if (goal.Place.X < Place.X)
                {
                    if (obj.TrueForAll(k => CanGoThrough(k) || (k.Place.X != place.X - 1) || (k.Place.Y != place.Y) || (k == this)))
                    {
                        --place.X;
                        return;
                    }
                }
                if (goal.Place.Y > Place.Y)
                {
                    if (obj.TrueForAll(k => CanGoThrough(k) || (k.Place.X != place.X) || (k.Place.Y != place.Y + 1) || (k == this)))
                    {
                        ++place.Y;
                        return;
                    }
                }
                else if (goal.Place.Y < Place.Y)
                {
                    if (obj.TrueForAll(k => CanGoThrough(k) || (k.Place.X != place.X) || (k.Place.Y != place.Y - 1) || (k == this)))
                    {
                        --place.Y;
                        return;
                    }
                }
            }
        }
        public void SeekGoal(List<Object> obj)
        {
            foreach (var i in obj)
            {
                if (IsPartner(i) != IsFood(i))
                {
                    int newpath = Math.Abs(i.Place.X - place.X) + Math.Abs(i.Place.Y - place.Y);
                    if ((goal == null) || (newpath < path))
                    {
                        path = newpath;
                        goal = i;
                    }
                }
            }
        }
        public void Execute(List<Object> obj)
        {
            if (path <= 1)
            {
                Object _goal = goal;
                int _satiety = IsFood(goal);
                if (_satiety != 0)
                {
                    satiety += _satiety;
                    obj.Remove(goal);
                }
                _satiety = IsPartner(goal);
                if (_satiety != 0)
                {
                    satiety += _satiety;
                    obj.Add(Birth());
                }
                foreach (var i in obj)
                    if (i is Entity)
                        if ((i as Entity).Goal == _goal)
                            (i as Entity).Goal = null;
            }
        }
        protected abstract bool CanGoThrough(Object obj);
        protected abstract Entity Birth();
        protected abstract int IsFood(Object obj);
        protected abstract int IsPartner(Object obj);
    }
}
