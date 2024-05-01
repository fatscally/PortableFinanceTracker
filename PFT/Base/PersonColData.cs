using System;
using CLPSI.Web.BusinessObjects.Base.Models;
using Sybase.Data.AseClient;
using System.Data;
using System.Linq;
using CLPSI.Web.Libraries.WcfUtils;

namespace CLPSI.Web.BusinessObjects.BaseData.Data
{
    internal class PersonColData
    {
        /// <summary>
        /// Loads a person from the database
        /// </summary>
        /// <param name="clientId">The client id of the person</param>
        /// <returns>The popualetd person object</returns>
        public void Search(Person person, PersonCol personColReturn)
        {
            AseConnection con = null;
            string sqlString = string.Empty;
            bool closeConnection = false;
            int matchPercent;
            string addressLine1 = string.Empty;
            string addressLine2 = string.Empty;
            string addressLine3 = string.Empty;
            string addressLine4 = string.Empty;
            string addressLine5 = string.Empty;
            string postCode = string.Empty;

            try
            {
                con = DatabaseUtil.GetCon();
                if (con.Transaction == null)
                    closeConnection = true;


                //Based on the person details, determine if there is a matching client (same surname, forename, date of birth and address)
                using (AseCommand sqlCmd = new AseCommand("RON_GetClientSearch", con))
                {
                    sqlCmd.Parameters.Add("@surname", AseDbType.VarChar).Value = person.Surname;
                    sqlCmd.Parameters.Add("@forename", AseDbType.VarChar).Value = person.Forenames;
                    sqlCmd.Parameters.Add("@dateOfBirth", AseDbType.DateTime).Value = person.DateOfBirth;

                    DataSet dataset = new DataSet();
                    sqlString = DatabaseUtil.GetProcAndArgs(sqlCmd);
                    DatabaseUtil.FetchData(sqlCmd, out dataset, con);

                    if (dataset.Tables.Count > 0)
                    {
                        foreach (DataRow tmpRow in dataset.Tables[0].Rows)
                        {
                            Person searchClient = new Person();

                            if (tmpRow[0] != System.DBNull.Value)
                            {
                                searchClient.ClientId = Convert.ToInt32(tmpRow["CLIENT_ID"].ToString());
                            }
                            personColReturn.Collection.Add(searchClient);
                        }
                    }
                }

                // Now check we have potential matching information, build up the online match percent, based on name, address etc. (based on DES mathing criteria)
                foreach (Person matchingPerson in personColReturn.Collection)
                {
                    // Initialise the match percent to be 0
                    matchPercent = 0;
                    PersonData personData = new PersonData();

                    // Get the matching person details
                    personData.Load(matchingPerson);

                    // Build up the match percent
                    if (person.Surname == matchingPerson.Surname)
                        matchPercent += 10;

                    if (person.Forenames == matchingPerson.Forenames)
                        matchPercent += 20;

                    // To ensure we are matching the title correctly, we compare like for like...
                    if (matchingPerson.Title == "Other")
                    {
                        if (person.OtherTitle == matchingPerson.OtherTitle)
                            matchPercent += 5;
                    }
                    else
                    {
                        if (person.Title == matchingPerson.Title)
                            matchPercent += 5;
                    }

                    if (Convert.ToDateTime(person.DateOfBirth) == Convert.ToDateTime(matchingPerson.DateOfBirth))
                        matchPercent += 20;

                    foreach (Address a in person.AddressCol.Collection)
                    {
                        if (!String.IsNullOrEmpty(a.AddressLine1)) addressLine1 = a.AddressLine1;
                        if (!String.IsNullOrEmpty(a.AddressLine2)) addressLine2 = a.AddressLine2;
                        if (!String.IsNullOrEmpty(a.AddressLine3)) addressLine3 = a.AddressLine3;
                        if (!String.IsNullOrEmpty(a.AddressLine4)) addressLine4 = a.AddressLine4;
                        if (!String.IsNullOrEmpty(a.AddressLine5)) addressLine5 = a.AddressLine5;
                    }
                    bool addressMatched = false;
                    foreach (Address a in matchingPerson.AddressCol.Collection)
                    {
                        if (addressMatched == false)
                        {
                            if (addressLine1 == a.AddressLine1.Trim())
                            {
                                matchPercent += 20;
                                addressMatched = true; //Exit as we have match - dont look at other matches of address or > 100
                            }

                            if (addressLine2 == a.AddressLine2.Trim())
                                matchPercent += 10;

                            if (addressLine3 == a.AddressLine3.Trim())
                                matchPercent += 5;

                            if (addressLine4 == a.AddressLine4.Trim())
                                matchPercent += 5;

                            if (addressLine5 == a.AddressLine5.Trim())
                                matchPercent += 5;
                        }
                    }
                    matchingPerson.OnlineMatchPerecnt = matchPercent;
                }
            }
            catch (WebError e) { e.AdditionalInformation.Add("Error in Person Search Data.Load"); e.AdditionalInformation.Add(sqlString); throw e; }
            catch (Exception e) { if (sqlString != string.Empty) sqlString.LogErrorMessage(); throw e; }
            finally { if (closeConnection && con.State == ConnectionState.Open) con.Close(); }
        }

    }
}
