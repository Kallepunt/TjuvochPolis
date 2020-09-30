using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Civ : Person

    {
        public List<Backpack> Backpack { get; set;}
        private static Random random = new Random();
        public Civ(int x, int y)
        {

            List<Backpack> backpack = new List<Backpack>();
            Xposition = x;
            Yposition = y;
            backpack.Add(new Backpack("Keys"));
            backpack.Add(new Backpack("Money"));
            backpack.Add(new Backpack("Watch"));
            backpack.Add(new Backpack("Phone"));
            Symbol = 'M';
            Backpack = backpack;
        }


        public override int DoIHaveItems()
        {
            return Backpack.Count;
        }

        public override Backpack GetRandom()
        {
            return Backpack[random.Next(0, Backpack.Count)];
        }

        public override void RemoveItem(Backpack item)
        {
            Backpack.Remove(item);
        }

    }
}
