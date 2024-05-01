using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PFT
{
    public static class Constants
    {
        public enum InsertTypes
        {
            [Description("M")]
            Manual,
            [Description("A")]
            AutoMatic,
            [Description("V")]
            Verified
        }

    }
}
