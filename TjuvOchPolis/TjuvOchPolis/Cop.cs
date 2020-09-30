using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TjuvOchPolis
{
    class Cop : Person
    {


        public List<Backpack> CopBackpack { get; set; }
        public Cop(int x, int y)
        {

            List<Backpack> copBackpack = new List<Backpack>();
            Xposition = x;
            Yposition = y;

            Symbol = 'C';
            CopBackpack = copBackpack;
        }


        public override int DoIHaveItems()
        {
            return CopBackpack.Count;
        }

        public override void TakeItem(Backpack item)
        {
            CopBackpack.Add(item);
        }

    }
}
