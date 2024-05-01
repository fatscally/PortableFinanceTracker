using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFT.Base
{
    public class Transaction_Tag
    {

        private int _transactionId;
        public int TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        private int _tagId;
        public int TagId
        {
            get { return _tagId; }
            set { _tagId = value; }
        }
        
    }
}
