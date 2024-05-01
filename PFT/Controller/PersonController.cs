using System;
using CLPSI.Web.BusinessObjects.Base.Models;
using CLPSI.Web.BusinessObjects.BaseData.Data;
using System.Runtime.Serialization;

namespace CLPSI.Web.BusinessObjects.BaseData.Controllers
{
    /// <summary>Use to hold person details. This inherits from Client object.</summary>
    [DataContract]
    public class PersonController
    {
        private PersonData _personData;

        /// <summary>Default constructor</summary>
        public PersonController()
        {
            _personData = new PersonData();
        }

        /// <summary>Load person information into person object, based on the ClientId property.</summary>
        public void Load(Person person)
        {
            _personData.Load(person);
        }

        /// Load matches Details back to the screen
        /// 
        /// </summary>
        public Person LoadMatchedClient(Person person, int ClientID)
        {
            _personData.Load(person);

            return person;
        }
        /// <summary>Save person information from the person object.</summary>
        public void Save(Person person)
        {
            try
            {
                int clientId;
                _personData.Save(person, out clientId);
                person.ClientId = clientId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
