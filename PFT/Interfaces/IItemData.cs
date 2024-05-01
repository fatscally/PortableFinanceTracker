using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFT.Base;

namespace PFT.Interfaces
{
    interface IItemData
    {
        void Save(Item item);
        //void Update(Item item);
        void Delete(Item item);

        Item Select(int itemId);
    }
}
