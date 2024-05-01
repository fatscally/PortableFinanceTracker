using System;

using System.Runtime.Serialization;
using CLPSI.Web.BusinessObjects.BaseData.Data;
using CLPSI.Web.BusinessObjects.Base.Models;

namespace CLPSI.Web.BusinessObjects.BaseData.Controllers
{
    /// <summary>Use as a container for a list of Person objects.</summary>
    [DataContract]
    public class PersonColController
    {
        private PersonColData _personColData;

        /// <summary>
        /// 
        /// </summary>
        public PersonColController()
        {
            _personColData = new PersonColData();
        }

        /// <summary>
        /// Load matches Details back to the screen
        /// </summary>
        public void SearchForMatch(Person personToSearch, PersonCol personColReturn)
        {
            _personColData.Search(personToSearch, personColReturn);
        }
    }
}
