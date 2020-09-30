using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolis
{
    public class Person
    {
        public int Xposition;
        public int Yposition;
       
        public char Symbol { get; set; }
        
       

        public void MovePlayer(int xMove, int yMove)
        {
            Xposition += xMove;
            Yposition += yMove;
        }

        public virtual int DoIHaveItems()
        {
            return 0;
        }

        public virtual Backpack GetRandom()
        {
            return null;
        }

        public virtual void TakeItem(Backpack item)
        {

        }
        public virtual void RemoveItem(Backpack item)
        {

        }

        public virtual List<Backpack> TakeAllItems()
        {
            return null;

        }

        public virtual int GetJailTimer()
        {
            return 0;
        }
        public virtual void IncreaseJailTimer()
        {

        }
        public virtual void ResetJailTimer()
        {

        }

    }
}
