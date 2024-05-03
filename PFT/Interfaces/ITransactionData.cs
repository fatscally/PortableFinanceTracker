using PFT.Base;
using System;

namespace PFT.Interfaces
{
    public interface ITransactionData
    {
        void Save(Transaction transaction);
        void Delete(Transaction transaction);
    }
}
