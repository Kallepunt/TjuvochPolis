using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    class Pickpocket : Person
    {
        public int JailTimer { get; set; }
        public List<Backpack> ThiefBackpack { get; set; }

        public Pickpocket(int x, int y, int jailTimer)
        {
            Symbol = 'T';
            
            Xposition = x;
            Yposition = y;
            
            
            List<Backpack> thiefBackpack = new List<Backpack>();
            ThiefBackpack = thiefBackpack;

            JailTimer = jailTimer;
        }



        public override int DoIHaveItems()
        {
            return ThiefBackpack.Count;
        }


        public override void TakeItem(Backpack item)
        {
            ThiefBackpack.Add(item);
        }

        public override List<Backpack> TakeAllItems()
        {
            return new List<Backpack>(ThiefBackpack);
        }

        public override void RemoveItem(Backpack item)
        {
            ThiefBackpack.Remove(item);
        }

        public override int GetJailTimer()
        {
            return JailTimer;
        }

        public override void IncreaseJailTimer()
        {
            JailTimer++;
        }

        public override void ResetJailTimer()
        {
            JailTimer = 0;
        }



    }
}
