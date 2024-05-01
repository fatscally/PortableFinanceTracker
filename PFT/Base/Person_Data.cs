#region Using Section
using System;
using System.Data;
using CLPSI.Web.BusinessObjects.Base.Models;
using Sybase.Data.AseClient;
using CLPSI.Web.Libraries.WcfUtils;
using CLPSI.Web.BusinessObjects.BaseData.Factory;
#endregion

namespace CLPSI.Web.BusinessObjects.BaseData.Data
{
    public class PersonData
    {
        /// <summary>Save the business object to the database when no database connection is specified.</summary>
        /// <returns></returns>
        public void Save(Person person, out int clientId)
        {
            clientId = 0;
            //person.ClientId = clientId;
            string saveType;

            AseConnection con = null;
            string sqlString = string.Empty;
            bool closeConnection = false;

            try
            {
                con = DatabaseUtil.GetCon();
                if (con.Transaction == null)
                    closeConnection = true;

                if (person.ClientId <= 0)
                    saveType = "I";
                else
                {
                    saveType = "U";
                }

                //Inserts or updates  CLIENT and PERSON
                using (AseCommand sqlCmd = new AseCommand("RON_SavePerson", con))
                {
                    sqlCmd.Parameters.Add("@saveType", AseDbType.VarChar).Value = saveType;
                    sqlCmd.Parameters.Add("@clientID", AseDbType.Integer).Value = person.ClientId;
                    if (person.Title != string.Empty) sqlCmd.Parameters.Add("@title", AseDbType.VarChar).Value = person.Title;
                    if (person.Forenames != string.Empty) sqlCmd.Parameters.Add("@forenames", AseDbType.VarChar).Value = person.Forenames;
                    if (person.Surname != string.Empty) sqlCmd.Parameters.Add("@surname", AseDbType.VarChar).Value = person.Surname;
                    if (person.DateOfBirth != null && person.DateOfBirth != DateTime.MinValue) sqlCmd.Parameters.Add("@dateOfBirth", AseDbType.DateTime).Value = Convert.ToDateTime(person.DateOfBirth);
                    if (person.MaritalStatus != string.Empty) sqlCmd.Parameters.Add("@maritalStatus", AseDbType.VarChar).Value = person.MaritalStatus;
                    if (person.Gender != string.Empty) sqlCmd.Parameters.Add("@gender", AseDbType.VarChar).Value = person.Gender;
                    if (person.CurrentCountryOfResidence != string.Empty) sqlCmd.Parameters.Add("@currentCountryOfResidence", AseDbType.VarChar).Value = person.CurrentCountryOfResidence;
                    if (person.NationalityCode != string.Empty) sqlCmd.Parameters.Add("@nationalityCode", AseDbType.VarChar).Value = person.NationalityCode;
                    if (person.Language != string.Empty) sqlCmd.Parameters.Add("@languageCode", AseDbType.VarChar).Value = person.Language;
                    if (person.NormalCountryOfResidence != string.Empty) sqlCmd.Parameters.Add("@normalCountryOfResidence", AseDbType.VarChar).Value = person.NormalCountryOfResidence;
                    if (person.CountryOfBirth != string.Empty) sqlCmd.Parameters.Add("@countryOfBirth", AseDbType.VarChar).Value = person.CountryOfBirth;
                    if (person.ProfessionCode != string.Empty) sqlCmd.Parameters.Add("@professionCode", AseDbType.VarChar).Value = person.ProfessionCode;
                    if (person.ProfessionalDescription != string.Empty) sqlCmd.Parameters.Add("@professionDescription", AseDbType.VarChar).Value = person.ProfessionalDescription;
                    if (person.AnnualIncome != null) sqlCmd.Parameters.Add("@annualIncomeCode", AseDbType.VarChar).Value = person.AnnualIncome;
                    if (person.ProofOfIdentity == true) sqlCmd.Parameters.Add("@proofOfIdentity", AseDbType.VarChar).Value = "T";
                    sqlCmd.Parameters.Add("@personTimestamp", AseDbType.TimeStamp).Value = (!string.IsNullOrEmpty(person.Timestamp)) ? StringUtil.ToByteArray(person.Timestamp) : null;
                    sqlCmd.Parameters.Add("@proofOfAge", AseDbType.VarChar).Value = "T";
                    if (person.MiddleName != null) sqlCmd.Parameters.Add("@middleName", AseDbType.VarChar).Value = person.MiddleName;
                    if (person.PlaceOfBirth != null) sqlCmd.Parameters.Add("@town", AseDbType.VarChar).Value = person.PlaceOfBirth;
                    if (person.OccupationOth != string.Empty) sqlCmd.Parameters.Add("@employStatusDesc", AseDbType.VarChar).Value = person.OccupationOth;
                    if (person.Occupation != string.Empty) sqlCmd.Parameters.Add("@employStatus", AseDbType.VarChar).Value = person.Occupation;
                    if (person.ClientBankID != string.Empty) sqlCmd.Parameters.Add("@clientBnkNo", AseDbType.VarChar).Value = person.ClientBankID;
                    if (person.RiskCode != string.Empty) sqlCmd.Parameters.Add("@riskProfileResult", AseDbType.VarChar).Value = person.RiskCode;
                    if (person.MatchedOnline != string.Empty) sqlCmd.Parameters.Add("@matchedOnline", AseDbType.VarChar).Value = person.MatchedOnline;
                    if (person.ClientIdentityAlt_1 != string.Empty) sqlCmd.Parameters.Add("@clientIdentityAlt_1", AseDbType.VarChar).Value = person.ClientIdentityAlt_1;
                    if (person.ClientIdentityAlt_2 != string.Empty) sqlCmd.Parameters.Add("@clientIdentityAlt_2", AseDbType.VarChar).Value = person.ClientIdentityAlt_2;
                    if (person.DateOfDeath != null) sqlCmd.Parameters.Add("@dateOfDeath", AseDbType.DateTime).Value = Convert.ToDateTime(person.DateOfDeath);
                    if (person.OtherTitle != string.Empty) sqlCmd.Parameters.Add("@otherTitle", AseDbType.VarChar).Value = person.OtherTitle;

                    DataSet dataset = new DataSet();

                    sqlString = DatabaseUtil.GetProcAndArgs(sqlCmd);

                    DatabaseUtil.FetchData(sqlCmd, out dataset, con);
                    if (dataset.Tables.Count > 0)
                    {
                        foreach (DataRow tmpRow in dataset.Tables[0].Rows)
                        {
                            if (tmpRow[0] != System.DBNull.Value && person.ClientId <= 0)
                            {
                                clientId = Convert.ToInt32(tmpRow["CLIENT ID"].ToString()); //Convert.ToInt32(tmpRow[0].ToString());
                                person.Timestamp = StringUtil.ToHexString((byte[])tmpRow["PERSON timestamp"]);
                                person.ClientId = clientId;
                            }
                            else
                            {
                                if (tmpRow[0] != System.DBNull.Value && person.ClientId > 0)
                                {
                                    person.Timestamp = StringUtil.ToHexString((byte[])tmpRow["PERSON timestamp"]);
                                }
                            }
                        }
                    }
                    if (person.NationalityCol != null && person.NationalityCol.Collection.Count > 0)
                    {
                        person.NationalityCol.ClientId = person.ClientId;
                        NationalityColData nationalityColData = new NationalityColData();
                        nationalityColData.Save(person.NationalityCol);
                    }
                    if (person.CitizenShipCol != null && person.CitizenShipCol.Collection.Count > 0)
                    {
                        (new CitizenshipColData()).Save(person.CitizenShipCol);
                    }
                    if (person.AddressCol != null && person.AddressCol.Collection.Count > 0)
                    {
                        (new AddressColData()).Save(person.AddressCol);
                    }

                    if (person.PhoneCol != null && person.PhoneCol.Collection.Count > 0)
                    {
                        person.PhoneCol.ClientId = person.ClientId;
                        PhoneColData phoneColData = new PhoneColData();
                        phoneColData.Save(person.PhoneCol);
                        //person.PhoneCol.Save();
                    }

                    if (person.WebContactCol != null && person.WebContactCol.Collection.Count > 0)
                    {
                        //person.WebContactCol.ClientId = clientId;
                        WebContactColData webContactColData = new WebContactColData();
                        webContactColData.Save(person.WebContactCol);

                    }

                    if (person.IdentificationCol != null && person.IdentificationCol.Collection.Count > 0)
                    {
                        person.IdentificationCol.ClientId = person.ClientId;
                        IdentificationColData identificationColData = new IdentificationColData();
                        identificationColData.Save(person.IdentificationCol);
                    }

                    if (person.Employer != null)
                    {
                        int employerId;
                        OrganisationData organisationData = new OrganisationData();
                        organisationData.Save(person.Employer, out employerId);

                        SaveClientRelationship saveRelationship = new SaveClientRelationship();
                        saveRelationship.SaveClientEmployerRelationship(person.ClientId, employerId);
                    }
                }
            }
            catch (WebError e) { e.AdditionalInformation.Add("Error in PersonData.Save"); e.AdditionalInformation.Add(sqlString); throw e; }
            catch (Exception e) { if (sqlString != string.Empty) sqlString.LogErrorMessage(); throw e; }
            finally { if (closeConnection && con.State == ConnectionState.Open) con.Close(); }

        }

        /// <summary>
        /// Loads a person from the database
        /// </summary>
        /// <returns>The popualetd person object</returns>
        public void Load(Person person)
        {
            AseConnection con = null;
            string sqlString = string.Empty;
            bool closeConnection = false;

            try
            {
                con = DatabaseUtil.GetCon();
                if (con.Transaction == null)
                    closeConnection = true;


                //Person person = null; // default to null incase we cant find this person

                using (AseCommand sqlCmd = new AseCommand("RON_GetPerson", con))
                {
                    sqlCmd.Parameters.Add("@clientID", AseDbType.Integer).Value = person.ClientId;

                    DataSet dataset = new DataSet();

                    DatabaseUtil.FetchData(sqlCmd, out dataset, con);
                    if (dataset.Tables.Count > 0)
                    {
                        //foreach (DataRow tmpRow in dataset.Tables[0].Rows)
                        if (dataset.Tables[0].Rows.Count > 0)
                        {
                            DataRow tmpRow = dataset.Tables[0].Rows[0];

                            if (tmpRow["DATE_OF_BIRTH"] != System.DBNull.Value) person.DateOfBirth = Convert.ToDateTime(tmpRow["DATE_OF_BIRTH"].ToString());
                            if (tmpRow["CURRENT_COUNTRY_OF_RESIDENCE"] != System.DBNull.Value) person.CurrentCountryOfResidence = tmpRow["CURRENT_COUNTRY_OF_RESIDENCE"].ToString();
                            if (tmpRow["FORENAMES"] != System.DBNull.Value) person.Forenames = tmpRow["FORENAMES"].ToString();
                            if (tmpRow["INITIALS"] != System.DBNull.Value) person.Initials = tmpRow["INITIALS"].ToString();
                            if (tmpRow["NORMAL_COUNTRY_OF_RESIDENCE"] != System.DBNull.Value) person.NormalCountryOfResidence = tmpRow["NORMAL_COUNTRY_OF_RESIDENCE"].ToString();
                            if (tmpRow["SURNAME"] != System.DBNull.Value) person.Surname = tmpRow["SURNAME"].ToString();
                            if (tmpRow["TITLE"] != System.DBNull.Value) person.Title = tmpRow["TITLE"].ToString();
                            if (tmpRow["MARITAL_STATUS"] != System.DBNull.Value) person.MaritalStatus = tmpRow["MARITAL_STATUS"].ToString();
                            if (tmpRow["NATIONALITY_CODE"] != System.DBNull.Value) person.NationalityCode = tmpRow["NATIONALITY_CODE"].ToString();
                            if (tmpRow["GENDER"] != System.DBNull.Value) person.Gender = tmpRow["GENDER"].ToString();
                            if (tmpRow["COUNTRY_OF_BIRTH"] != System.DBNull.Value) person.CountryOfBirth = tmpRow["COUNTRY_OF_BIRTH"].ToString();
                            if (tmpRow["COUNTRY_OF_FISCAL_RESIDENCE"] != System.DBNull.Value) person.CountryCode = tmpRow["COUNTRY_OF_FISCAL_RESIDENCE"].ToString();
                            if (tmpRow["PROFESSION_CODE"] != System.DBNull.Value) person.ProfessionCode = tmpRow["PROFESSION_CODE"].ToString();
                            if (tmpRow["PROFESSION_DESCRIPTION"] != System.DBNull.Value) person.ProfessionalDescription = tmpRow["PROFESSION_DESCRIPTION"].ToString();
                            if (tmpRow["MARITAL_REGIME"] != System.DBNull.Value) person.MaritalRegime = tmpRow["MARITAL_REGIME"].ToString();
                            if (tmpRow["ANNUAL_INCOME_CODE"] != System.DBNull.Value) person.AnnualIncome = tmpRow["ANNUAL_INCOME_CODE"].ToString();
                            if (tmpRow["OTHER_TITLE"] != System.DBNull.Value) person.OtherTitle = tmpRow["OTHER_TITLE"].ToString();
                            if (tmpRow["ONLINE_MATCH_PERCENT"] != System.DBNull.Value) person.OnlineMatchPerecnt = Convert.ToInt32(tmpRow["ONLINE_MATCH_PERCENT"]);
                            if (tmpRow["MATCHED_ONLINE"] != System.DBNull.Value) person.MatchedOnline = tmpRow["MATCHED_ONLINE"].ToString();
                            if (tmpRow["PROOF_OF_IDENTITY"] != DBNull.Value && tmpRow["PROOF_OF_IDENTITY"].ToString() == "T")
                                person.ProofOfIdentity = true;
                            if (tmpRow["MIDDLE_NAME"] != System.DBNull.Value) person.MiddleName = tmpRow["MIDDLE_NAME"].ToString();
                            if (tmpRow["TOWN"] != System.DBNull.Value) person.PlaceOfBirth = tmpRow["TOWN"].ToString();
                            if (tmpRow["EMPLOYMENT_STATUS"] != System.DBNull.Value) person.Occupation = tmpRow["EMPLOYMENT_STATUS"].ToString();
                            if (tmpRow["EMPLOYMENT_STATUS_DESC"] != System.DBNull.Value) person.OccupationOth = tmpRow["EMPLOYMENT_STATUS_DESC"].ToString();
                            if (tmpRow["RISK_PROFILE_RESULT"] != System.DBNull.Value) person.RiskCode = tmpRow["RISK_PROFILE_RESULT"].ToString();
                            if (tmpRow["CLIENT_BNK_ID"] != System.DBNull.Value) person.ClientBankID = tmpRow["CLIENT_BNK_ID"].ToString();
                            if (tmpRow["SOURCE1_CLIENT_ID"] != System.DBNull.Value) person.ClientIdentityAlt_1 = tmpRow["SOURCE1_CLIENT_ID"].ToString();
                            if (tmpRow["SOURCE2_CLIENT_ID"] != System.DBNull.Value) person.ClientIdentityAlt_2 = tmpRow["SOURCE2_CLIENT_ID"].ToString();
                            if (tmpRow["DEATH_DATE"] != System.DBNull.Value) person.DateOfDeath = Convert.ToDateTime(tmpRow["DEATH_DATE"].ToString());

                            if (tmpRow["timestamp"] != System.DBNull.Value)
                                person.Timestamp = StringUtil.ToHexString((byte[])tmpRow["timestamp"]);

                            person.AddressCol.ClientId = person.ClientId;
                            AddressColData addressColData = new AddressColData();
                            addressColData.Load(person.AddressCol.ClientId, person.AddressCol);
                            //person.AddressCol.Load();

                            person.PhoneCol.ClientId = person.ClientId;
                            PhoneColData phoneColData = new PhoneColData();
                            phoneColData.Load(person.PhoneCol.ClientId, person.PhoneCol);
                            //person.PhoneCol.Load();

                            person.WebContactCol.ClientId = person.ClientId;
                            WebContactColData webContactColData = new WebContactColData();
                            webContactColData.Load(person.WebContactCol.ClientId, person.WebContactCol);
                            //person.WebContactCol.Load();

                            person.CitizenShipCol.ClientId = person.ClientId;
                            CitizenshipColData citizenshipColData = new CitizenshipColData();
                            citizenshipColData.Load(person.CitizenShipCol.ClientId, person.CitizenShipCol);

                            person.NationalityCol.ClientId = person.ClientId;
                            NationalityColData nationalityColData = new NationalityColData();
                            nationalityColData.Load(person.NationalityCol.ClientId, person.NationalityCol);

                            person.IdentificationCol.ClientId = person.ClientId;
                            IdentificationColData idColData = new IdentificationColData();
                            idColData.Load(person.IdentificationCol.ClientId, person.IdentificationCol);

                            person.RiskProfileCol.ClientId = person.ClientId;
                            RiskProfileColData riskProfileColData = new RiskProfileColData();
                            riskProfileColData.Load(person.RiskProfileCol.ClientId, person.RiskProfileCol);
                            //person.CitizenShipCol.Load();

                            SaveClientRelationship saveClientRelationship = new SaveClientRelationship();
                            int employerClientId = 0;
                            saveClientRelationship.GetEmployerClientId(person.ClientId, out employerClientId);

                            if (employerClientId != 0)
                            {
                                person.Employer = new Organisation();
                                OrganisationData employerData = new OrganisationData();
                                employerData.Load(employerClientId, person.Employer);
                            }
                        }
                    }
                }

            }
            catch (WebError e) { e.AdditionalInformation.Add("Error in PersonData.Load"); e.AdditionalInformation.Add(sqlString); throw e; }
            catch (Exception e) { if (sqlString != string.Empty) sqlString.LogErrorMessage(); throw e; }
            finally { if (closeConnection && con.State == ConnectionState.Open) con.Close(); }
        }

    }
}
