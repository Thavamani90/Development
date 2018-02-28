﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FingerprintsModel;
using System.Web.Mvc;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Drawing;

namespace FingerprintsData
{

    public class FamilyData : Controller
    {
        SqlConnection Connection = connection.returnConnection();
        SqlCommand command = new SqlCommand();
        //SqlDataReader dataReader = null;
        SqlTransaction tranSaction = null;
        SqlDataAdapter DataAdapter = null;
        DataTable familydataTable = null;
        DataSet _dataset = null;
        DataTable _dataTable = null;
        public string addStreetInfo(out int householdgenerated, FamilyHousehold obj, int mode, Guid userId)
        {
            string result = string.Empty;
            householdgenerated = 0;
            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Street", obj.Street));
                command.Parameters.Add(new SqlParameter("@StreetName", obj.StreetName));
                command.Parameters.Add(new SqlParameter("@Apartmentno", obj.Apartmentno));
                command.Parameters.Add(new SqlParameter("@ZipCode", obj.ZipCode));
                command.Parameters.Add(new SqlParameter("@State", obj.State));
                command.Parameters.Add(new SqlParameter("@City", obj.City));
                command.Parameters.Add(new SqlParameter("@nationality", obj.County));
                command.Parameters.Add(new SqlParameter("@fileinbyte", obj.HImageByte));
                command.Parameters.Add(new SqlParameter("@filename", obj.HFileName));
                command.Parameters.Add(new SqlParameter("@fileextension", obj.HFileExtension));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@CreatedBy", userId));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@HouseholdIdgenerated", Convert.ToInt32(0)));
                command.Parameters["@HouseholdIdgenerated"].Direction = ParameterDirection.Output;
                command.Parameters["@HouseholdIdgenerated"].Size = 10;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_StreetHousehold";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
                householdgenerated = Convert.ToInt32(command.Parameters["@HouseholdIdgenerated"].Value);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string addFamilyInfo(FamilyHousehold obj, int mode, Guid ID)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@FamilyHouseholdID", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@TANF", obj.TANF));
                command.Parameters.Add(new SqlParameter("@SSI", obj.SSI));
                command.Parameters.Add(new SqlParameter("@SNAP", obj.SNAP));
                command.Parameters.Add(new SqlParameter("@WIC", obj.WIC));
                command.Parameters.Add(new SqlParameter("@HomeType", obj.HomeType));
                command.Parameters.Add(new SqlParameter("@PrimaryLanguauge", obj.PrimaryLanguauge));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@RentType", obj.RentType));
                command.Parameters.Add(new SqlParameter("@FamilyType", obj.FamilyType));
                command.Parameters.Add(new SqlParameter("@Interpretor", obj.Interpretor));
                command.Parameters.Add(new SqlParameter("@InsuranceOption", obj.InsuranceOption));
                command.Parameters.Add(new SqlParameter("@MedicalNotice", obj.MedicalNote));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_FamilyDetails";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string addChildInfo(FamilyHousehold obj, int mode, Guid ID)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@ChildId", obj.ChildId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@Cfirstname", obj.Cfirstname));
                command.Parameters.Add(new SqlParameter("@Clastname", obj.Clastname));
                command.Parameters.Add(new SqlParameter("@Cmiddlename", obj.Cmiddlename));
                command.Parameters.Add(new SqlParameter("@CprogramType", obj.CProgramType));
                command.Parameters.Add(new SqlParameter("@CDOB", obj.CDOB));
                command.Parameters.Add(new SqlParameter("@CDOBverifiedby", obj.DOBverifiedBy));
                command.Parameters.Add(new SqlParameter("@CSSN", obj.CSSN));
                command.Parameters.Add(new SqlParameter("@CGender", obj.CGender));
                command.Parameters.Add(new SqlParameter("@CRace", obj.CRace));
                command.Parameters.Add(new SqlParameter("@CRaceSubCategory", obj.CRaceSubCategory));
                command.Parameters.Add(new SqlParameter("@CEthnicity", obj.CEthnicity));
                command.Parameters.Add(new SqlParameter("@CMedicalhome", obj.Medicalhome));
                command.Parameters.Add(new SqlParameter("@Dentalhome", obj.CDentalhome));
                command.Parameters.Add(new SqlParameter("@ImmunizationService", obj.ImmunizationService));
                command.Parameters.Add(new SqlParameter("@medicalservice", obj.MedicalService));
                command.Parameters.Add(new SqlParameter("@Parentdisable", obj.CParentdisable));
                command.Parameters.Add(new SqlParameter("@Bmistatus", obj.BMIStatus));

                command.Parameters.Add(new SqlParameter("@FileName", obj.CFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.CFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.CImageByte));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                //

                //
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_ChildDetails";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public FamilyHousehold GetData_AllDropdown(string HouseHoldid, int parentid, string agencyid, string userid, int i = 0, FamilyHousehold familyInfo = null)
        {
            //  List<AgencyStaff> _agencyStafflist = new List<AgencyStaff>();
            FamilyHousehold Info = new FamilyHousehold();

            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "Sp_Sel_FamilyInfo_Dropdowndata";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@HouseHoldid", HouseHoldid));
                        command.Parameters.Add(new SqlParameter("@parentid", parentid));
                        command.Parameters.Add(new SqlParameter("@AgencyId", agencyid));
                        command.Parameters.Add(new SqlParameter("@userid", userid));
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                    try
                    {
                        List<FingerprintsModel.FamilyHousehold.PrimarylangInfo> listlang = new List<FingerprintsModel.FamilyHousehold.PrimarylangInfo>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            FingerprintsModel.FamilyHousehold.PrimarylangInfo obj = new FingerprintsModel.FamilyHousehold.PrimarylangInfo();
                            obj.LangId = Convert.ToString(dr["Id"].ToString());
                            obj.Name = dr["Name"].ToString();
                            listlang.Add(obj);
                        }

                        Info.langList = listlang;
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.RaceSubCategory> _racelist = new List<FingerprintsModel.FamilyHousehold.RaceSubCategory>();
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.RaceSubCategory obj = new FingerprintsModel.FamilyHousehold.RaceSubCategory();
                                obj.RaceSubCategoryID = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _racelist.Add(obj);
                            }
                            Info.raceCategory = _racelist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.Relationship> _relationlist = new List<FingerprintsModel.FamilyHousehold.Relationship>();
                            foreach (DataRow dr in ds.Tables[2].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.Relationship obj = new FingerprintsModel.FamilyHousehold.Relationship();
                                obj.Id = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _relationlist.Add(obj);
                            }
                            //  _rolelist.Insert(0, new Role() { RoleId = "0", RoleName = "Select" });
                            Info.relationship = _relationlist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        try
                        {
                            List<FamilyHousehold.RaceInfo> _racelist = new List<FamilyHousehold.RaceInfo>();
                            //_staff.myList
                            foreach (DataRow dr in ds.Tables[3].Rows)
                            {
                                FamilyHousehold.RaceInfo obj = new FamilyHousehold.RaceInfo();
                                obj.RaceId = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _racelist.Add(obj);

                            }
                            //_racelist.Insert(0, new RaceInfo() { RaceId = "0", Name = "Select" });
                            Info.raceList = _racelist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }

                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    {
                        List<FamilyHousehold.Programdetail> Programs = new List<FamilyHousehold.Programdetail>();
                        foreach (DataRow dr in ds.Tables[4].Rows)
                        {
                            FamilyHousehold.Programdetail obj = new FamilyHousehold.Programdetail();
                            obj.Id = Convert.ToInt32(dr["programtypeid"]);
                            obj.Name = dr["programtype"].ToString();
                            obj.ReferenceId = dr["ReferenceId"].ToString();
                            Programs.Add(obj);
                        }
                        Info.AvailableProgram = Programs;
                    }
                    if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                        foreach (DataRow dr in ds.Tables[5].Rows)
                        {
                            FamilyHousehold.ImmunizationRecord obj = new FamilyHousehold.ImmunizationRecord();
                            obj.Dose = dr["Immunization"].ToString();
                            obj.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationid"]);
                            ImmunizationRecords.Add(obj);
                        }

                        Info.ImmunizationRecords = ImmunizationRecords;
                    }
                    if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDirectBloodRelative> ChildHealth = new List<FamilyHousehold.ChildDirectBloodRelative>();
                        foreach (DataRow dr in ds.Tables[6].Rows)
                        {
                            FamilyHousehold.ChildDirectBloodRelative obj = new FamilyHousehold.ChildDirectBloodRelative();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildHealth.Add(obj);
                        }
                        Info.AvailableDisease = ChildHealth;
                    }
                    if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDiagnosedDisease> ChildDiagnosedHealth = new List<FamilyHousehold.ChildDiagnosedDisease>();
                        foreach (DataRow dr in ds.Tables[7].Rows)
                        {
                            FamilyHousehold.ChildDiagnosedDisease obj = new FamilyHousehold.ChildDiagnosedDisease();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDiagnosedHealth.Add(obj);
                        }
                        Info.AvailableDiagnosedDisease = ChildDiagnosedHealth;
                    }
                    if (ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDental> ChildDental = new List<FamilyHousehold.ChildDental>();
                        foreach (DataRow dr in ds.Tables[8].Rows)
                        {
                            FamilyHousehold.ChildDental obj = new FamilyHousehold.ChildDental();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDental.Add(obj);
                        }
                        Info.AvailableDental = ChildDental;
                    }
                    if (ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDental> ChildDental = new List<FamilyHousehold.ChildDental>();
                        foreach (DataRow dr in ds.Tables[8].Rows)
                        {
                            FamilyHousehold.ChildDental obj = new FamilyHousehold.ChildDental();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDental.Add(obj);
                        }
                        Info.AvailableDental = ChildDental;
                    }
                    if (ds.Tables[9] != null && ds.Tables[9].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildEHS> ChildEHSInfo = new List<FamilyHousehold.ChildEHS>();
                        foreach (DataRow dr in ds.Tables[9].Rows)
                        {
                            FamilyHousehold.ChildEHS obj = new FamilyHousehold.ChildEHS();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildEHSInfo.Add(obj);
                        }
                        Info.AvailableEHS = ChildEHSInfo;
                    }
                    if (ds.Tables[10] != null && ds.Tables[10].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDietInfo> ChildDietDetails = new List<FamilyHousehold.ChildDietInfo>();
                        foreach (DataRow dr in ds.Tables[10].Rows)
                        {
                            FamilyHousehold.ChildDietInfo obj = new FamilyHousehold.ChildDietInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["DietInfo"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDietDetails.Add(obj);
                        }
                        Info.dietList = ChildDietDetails;
                    }
                    if (ds.Tables[11] != null && ds.Tables[11].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDrink> ChildDrinkDetails = new List<FamilyHousehold.ChildDrink>();
                        foreach (DataRow dr in ds.Tables[11].Rows)
                        {
                            FamilyHousehold.ChildDrink obj = new FamilyHousehold.ChildDrink();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDrinkDetails.Add(obj);
                        }
                        Info.AvailableChildDrink = ChildDrinkDetails;
                    }
                    if (ds.Tables[12] != null && ds.Tables[12].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildFoodInfo> ChildFoodDetails = new List<FamilyHousehold.ChildFoodInfo>();
                        foreach (DataRow dr in ds.Tables[12].Rows)
                        {
                            FamilyHousehold.ChildFoodInfo obj = new FamilyHousehold.ChildFoodInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Category"].ToString();
                            ChildFoodDetails.Add(obj);
                        }
                        Info.foodList = ChildFoodDetails;
                    }



                    if (ds.Tables[13] != null && ds.Tables[13].Rows.Count > 0)
                    {
                        List<SchoolDistrict> schooldistrict = new List<SchoolDistrict>();
                        foreach (DataRow dr in ds.Tables[13].Rows)
                        {
                            SchoolDistrict info = new SchoolDistrict();
                            info.SchoolDistrictID = Convert.ToInt32(dr["SchoolDistrictID"]);
                            info.Acronym = dr["Acronym"].ToString();
                            schooldistrict.Add(info);
                        }
                        Info.SchoolList = schooldistrict;
                    }
                    if (ds.Tables[14] != null && ds.Tables[14].Rows.Count > 0)
                    {
                        List<Nurse.ChildFeedCerealInfo> ChildCerealDetails = new List<Nurse.ChildFeedCerealInfo>();
                        foreach (DataRow dr in ds.Tables[14].Rows)
                        {
                            Nurse.ChildFeedCerealInfo obj = new Nurse.ChildFeedCerealInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildCerealDetails.Add(obj);
                        }
                        Info.CFeedCerealList = ChildCerealDetails;
                    }
                    if (ds.Tables[15] != null && ds.Tables[15].Rows.Count > 0)
                    {
                        List<Nurse.ChildReferalCriteriaInfo> ChildReferalDetails = new List<Nurse.ChildReferalCriteriaInfo>();
                        foreach (DataRow dr in ds.Tables[15].Rows)
                        {
                            Nurse.ChildReferalCriteriaInfo obj = new Nurse.ChildReferalCriteriaInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildReferalDetails.Add(obj);
                        }
                        Info.CReferalCriteriaList = ChildReferalDetails;
                    }
                    if (ds.Tables[16] != null && ds.Tables[16].Rows.Count > 0)
                    {
                        List<Nurse.ChildFormulaInfo> ChildFormulaDetails = new List<Nurse.ChildFormulaInfo>();
                        foreach (DataRow dr in ds.Tables[16].Rows)
                        {
                            Nurse.ChildFormulaInfo obj = new Nurse.ChildFormulaInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildFormulaDetails.Add(obj);
                        }
                        Info.CFormulaList = ChildFormulaDetails;
                    }
                    if (ds.Tables[17] != null && ds.Tables[17].Rows.Count > 0)
                    {
                        List<Nurse.ChildFeedInfo> ChildFedDetails = new List<Nurse.ChildFeedInfo>();
                        foreach (DataRow dr in ds.Tables[17].Rows)
                        {
                            Nurse.ChildFeedInfo obj = new Nurse.ChildFeedInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildFedDetails.Add(obj);
                        }
                        Info.CFeedList = ChildFedDetails;
                    }
                    if (ds.Tables[18] != null && ds.Tables[18].Rows.Count > 0)
                    {
                        List<Nurse.ChildDietFull> ChildDietFullDetails = new List<Nurse.ChildDietFull>();
                        foreach (DataRow dr in ds.Tables[18].Rows)
                        {
                            Nurse.ChildDietFull obj = new Nurse.ChildDietFull();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            ChildDietFullDetails.Add(obj);
                        }
                        Info.AvailableChildDietFull = ChildDietFullDetails;
                    }
                    if (ds.Tables[19] != null && ds.Tables[19].Rows.Count > 0)
                    {
                        List<Nurse.ChildVitamin> ChildVitaminDetails = new List<Nurse.ChildVitamin>();
                        foreach (DataRow dr in ds.Tables[19].Rows)
                        {
                            Nurse.ChildVitamin obj = new Nurse.ChildVitamin();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            ChildVitaminDetails.Add(obj);
                        }
                        Info.AvailableChildVitamin = ChildVitaminDetails;
                    }
                    if (ds.Tables[20] != null && ds.Tables[20].Rows.Count > 0)
                    {
                        List<Nurse.ChildHungryInfo> ChildHungryDetails = new List<Nurse.ChildHungryInfo>();
                        foreach (DataRow dr in ds.Tables[20].Rows)
                        {
                            Nurse.ChildHungryInfo obj = new Nurse.ChildHungryInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildHungryDetails.Add(obj);
                        }
                        Info.ChungryList = ChildHungryDetails;
                    }
                    if (ds.Tables[21] != null && ds.Tables[21].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMConditionsInfo> PMCondtnDetails = new List<FamilyHousehold.PMConditionsInfo>();
                        foreach (DataRow dr in ds.Tables[21].Rows)
                        {
                            FamilyHousehold.PMConditionsInfo obj = new FamilyHousehold.PMConditionsInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMCondtnDetails.Add(obj);
                        }
                        Info.PMCondtnList = PMCondtnDetails;
                    }
                    if (ds.Tables[22] != null && ds.Tables[22].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMProblems> PMPrblmDetails = new List<FamilyHousehold.PMProblems>();
                        foreach (DataRow dr in ds.Tables[22].Rows)
                        {
                            FamilyHousehold.PMProblems obj = new FamilyHousehold.PMProblems();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMPrblmDetails.Add(obj);
                        }
                        Info.AvailablePrblms = PMPrblmDetails;
                    }
                    if (ds.Tables[23] != null && ds.Tables[23].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMService> PMServiceDetails = new List<FamilyHousehold.PMService>();
                        foreach (DataRow dr in ds.Tables[23].Rows)
                        {
                            FamilyHousehold.PMService obj = new FamilyHousehold.PMService();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMServiceDetails.Add(obj);
                        }
                        Info.AvailableService = PMServiceDetails;
                    }

                    if (ds.Tables[24] != null && ds.Tables[24].Rows.Count > 0)
                    {
                        List<FamilyHousehold.AssignedTo> _clientlist = new List<FamilyHousehold.AssignedTo>();
                        foreach (DataRow dr in ds.Tables[24].Rows)
                        {
                            FamilyHousehold.AssignedTo obj = new FamilyHousehold.AssignedTo();
                            obj.Id = dr["UserId"].ToString() == "" ? "" : dr["UserId"].ToString().ToUpper();
                            obj.Name = dr["Name"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            _clientlist.Add(obj);
                        }
                        Info.ClientAssignedTo = _clientlist;
                    }


                    if (ds.Tables[25] != null && ds.Tables[25].Rows.Count > 0)
                    {

                        foreach (DataRow dr in ds.Tables[25].Rows)
                        {
                            Info.CTransportNeeded = Convert.ToBoolean(dr["ChildTransport"]);
                        }

                    }
                    if (parentid != 0 && HouseHoldid != "0") //Changes
                    {

                        if (ds.Tables[26] != null && ds.Tables[26].Rows.Count > 0)
                        {
                            Info.Pfirstname = ds.Tables[26].Rows[0]["Pfirstname"].ToString();
                            Info.Plastname = ds.Tables[26].Rows[0]["PLastName"].ToString();
                            Info.Pmidddlename = ds.Tables[26].Rows[0]["PMiddlename"].ToString();
                            if (ds.Tables[26].Rows[0]["PDOB"].ToString() != "")
                                Info.PDOB = Convert.ToDateTime(ds.Tables[26].Rows[0]["PDOB"]).ToString("MM/dd/yyyy");
                            Info.PGender = ds.Tables[26].Rows[0]["PGender"].ToString();
                            Info.Pemailid = ds.Tables[26].Rows[0]["PEmailid"].ToString();
                            if (ds.Tables[26].Rows[0]["PMilitaryStatus"].ToString() != "")
                                Info.PMilitaryStatus = Convert.ToInt32(ds.Tables[26].Rows[0]["PMilitaryStatus"]);
                            Info.PEnrollment = ds.Tables[26].Rows[0]["PEnrollment"].ToString();
                            if (ds.Tables[26].Rows[0]["PDegreeEarned"].ToString() != "")
                                Info.PDegreeEarned = Convert.ToString(ds.Tables[26].Rows[0]["PDegreeEarned"]);
                            Info.Pnotesother = ds.Tables[26].Rows[0]["PNotes"].ToString();
                            Info.PRole = ds.Tables[26].Rows[0]["ParentRole"].ToString();
                            if (ds.Tables[26].Rows[0]["PCurrentlyWorking"].ToString() != "")
                                Info.PCurrentlyWorking = ds.Tables[26].Rows[0]["PCurrentlyWorking"].ToString();
                            if (ds.Tables[26].Rows[0]["PPolicyCouncil"].ToString() != "")
                                Info.PPolicyCouncil = ds.Tables[26].Rows[0]["PPolicyCouncil"].ToString();
                            if (ds.Tables[26].Rows[0]["ParentId"].ToString() != "")
                                Info.ParentID = Convert.ToInt32(ds.Tables[26].Rows[0]["ParentId"]);
                            if (ds.Tables[26].Rows[0]["IsPreg"].ToString() != "")
                                Info.PQuestion = ds.Tables[26].Rows[0]["IsPreg"].ToString();
                            if (ds.Tables[26].Rows[0]["EnrollforPregnant"].ToString() != "")
                                Info.Pregnantmotherenrolled = Convert.ToBoolean(ds.Tables[26].Rows[0]["EnrollforPregnant"]);
                            if (ds.Tables[26].Rows[0]["motherinsurance"].ToString() != "")
                                Info.Pregnantmotherprimaryinsurance = Convert.ToInt32(ds.Tables[26].Rows[0]["motherinsurance"]);
                            if (ds.Tables[26].Rows[0]["insurancenotemother"].ToString() != "")
                                Info.Pregnantmotherprimaryinsurancenotes = ds.Tables[26].Rows[0]["insurancenotemother"].ToString();
                            if (ds.Tables[26].Rows[0]["TrimesterEnrolled"].ToString() != "")
                                Info.TrimesterEnrolled = Convert.ToInt32(ds.Tables[26].Rows[0]["TrimesterEnrolled"]);
                            try
                            {
                                Info.ParentSSN1 = ds.Tables[26].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(ds.Tables[26].Rows[0]["SSN"].ToString());
                            }
                            catch (Exception ex)
                            {
                                clsError.WriteException(ex);
                                Info.ParentSSN1 = ds.Tables[26].Rows[0]["SSN"].ToString();
                            }
                            if (ds.Tables[26].Rows[0]["Medicalhome"].ToString() != "")
                                Info.MedicalhomePreg1 = Convert.ToInt32(ds.Tables[26].Rows[0]["Medicalhome"]);
                            if (ds.Tables[26].Rows[0]["Doctorvalue"].ToString() != "")
                                Info.P1Doctor = Convert.ToInt32(ds.Tables[26].Rows[0]["Doctorvalue"]);
                            Info.CDoctorP1 = ds.Tables[26].Rows[0]["doctorname"].ToString();
                            if (ds.Tables[26].Rows[0]["NoEmail"].ToString() != "")
                                Info.Noemail1 = Convert.ToBoolean(ds.Tables[26].Rows[0]["NoEmail"]);
                            Info.PMVisitDoc = ds.Tables[26].Rows[0]["PMVisitDoc"].ToString();
                            if (ds.Tables[26].Rows[0]["PMProblmID"].ToString() != "")
                                Info.PMProblem = Convert.ToInt32(ds.Tables[26].Rows[0]["PMProblmID"]);
                            Info.PMOtherIssues = ds.Tables[26].Rows[0]["PMIssues"].ToString();
                            Info.PMConditions = ds.Tables[26].Rows[0]["PMConditionID"].ToString();
                            Info.PMCondtnDesc = ds.Tables[26].Rows[0]["PMConditionDescID"].ToString();
                            if (ds.Tables[26].Rows[0]["PMRisk"].ToString() != "")
                                Info.PMRisk = Convert.ToBoolean(ds.Tables[26].Rows[0]["PMRisk"]);
                            if (ds.Tables[26].Rows[0]["PMDentalExam"].ToString() != "")
                                Info.PMDentalExam = Convert.ToInt32(ds.Tables[26].Rows[0]["PMDentalExam"]);
                            if (ds.Tables[26].Rows[0]["PMDentalDate"].ToString() != "")
                                Info.PMDentalExamDate = Convert.ToDateTime(ds.Tables[26].Rows[0]["PMDentalDate"]).ToString("MM/dd/yyyy");
                            if (ds.Tables[26].Rows[0]["PMNeedDental"].ToString() != "")
                                Info.PMNeedDental = Convert.ToInt32(ds.Tables[26].Rows[0]["PMNeedDental"]);
                            if (ds.Tables[26].Rows[0]["PMRecieveDental"].ToString() != "")
                                Info.PMRecieveDental = Convert.ToInt32(ds.Tables[26].Rows[0]["PMRecieveDental"]);
                        }
                        if (ds.Tables[27] != null && ds.Tables[27].Rows.Count > 0)
                        {
                            List<FamilyHousehold.calculateincome> IncomeList = new List<FamilyHousehold.calculateincome>();
                            foreach (DataRow dr1 in ds.Tables[27].Rows)
                            {
                                FamilyHousehold.calculateincome _income = new FamilyHousehold.calculateincome();
                                _income.newincomeid = Convert.ToInt32(dr1["IncomeId"]);
                                if (dr1["Income"].ToString() != "")
                                    _income.Income = Convert.ToInt32(dr1["Income"]);
                                if (dr1["IncomeSource1"].ToString() != "")
                                    _income.IncomeSource1 = dr1["IncomeSource1"].ToString();
                                if (dr1["IncomeAmt1"].ToString() != "")
                                    _income.AmountVocher1 = Convert.ToDecimal(dr1["IncomeAmt1"]);
                                if (dr1["IncomeAmt2"].ToString() != "")
                                    _income.AmountVocher2 = Convert.ToDecimal(dr1["IncomeAmt2"]);
                                if (dr1["IncomeAmt3"].ToString() != "")
                                    _income.AmountVocher3 = Convert.ToDecimal(dr1["IncomeAmt3"]);
                                if (dr1["IncomeAmt4"].ToString() != "")
                                    _income.AmountVocher4 = Convert.ToDecimal(dr1["IncomeAmt4"]);
                                if (dr1["PayFrequency"].ToString() != "")
                                    _income.Payfrequency = Convert.ToInt32(dr1["PayFrequency"]);
                                if (dr1["WorkingPeriod"].ToString() != "")
                                    _income.Working = Convert.ToInt32(dr1["WorkingPeriod"]);
                                if (dr1["TotalIncome"].ToString() != "")
                                    _income.IncomeCalculated = Convert.ToDecimal(dr1["TotalIncome"]);
                                if (dr1["Image1"].ToString() != "")
                                    _income.SalaryAvatar1bytes = (byte[])dr1["Image1"];
                                if (dr1["Image2"].ToString() != "")
                                    _income.SalaryAvatar2bytes = (byte[])dr1["Image2"];
                                if (dr1["Image3"].ToString() != "")
                                    _income.SalaryAvatar3bytes = (byte[])dr1["Image3"];
                                if (dr1["Image4"].ToString() != "")
                                    _income.SalaryAvatar1bytes = (byte[])dr1["Image4"];
                                if (dr1["Image1FileName"].ToString() != "")
                                    _income.SalaryAvatarFilename1 = dr1["Image1FileName"].ToString();
                                if (dr1["Image2FileName"].ToString() != "")
                                    _income.SalaryAvatarFilename2 = dr1["Image2FileName"].ToString();
                                if (dr1["Image3FileName"].ToString() != "")
                                    _income.SalaryAvatarFilename3 = dr1["Image3FileName"].ToString();
                                if (dr1["Image4FileName"].ToString() != "")
                                    _income.SalaryAvatarFilename4 = dr1["Image4FileName"].ToString();
                                if (dr1["Image1FileExt"].ToString() != "")
                                    _income.SalaryAvatarFileExtension1 = dr1["Image1FileExt"].ToString();
                                if (dr1["Image2FileExt"].ToString() != "")
                                    _income.SalaryAvatarFileExtension2 = dr1["Image2FileExt"].ToString();
                                if (dr1["Image3FileExt"].ToString() != "")
                                    _income.SalaryAvatarFileExtension3 = dr1["Image3FileExt"].ToString();
                                if (dr1["Image4FileExt"].ToString() != "")
                                    _income.SalaryAvatarFileExtension4 = dr1["Image4FileExt"].ToString();
                                if (dr1["NoIncomeImage"].ToString() != "")
                                    _income.NoIncomeAvatarbytes = (byte[])dr1["NoIncomeImage"];
                                if (dr1["NoIncomeImageFileName"].ToString() != "")
                                    _income.NoIncomeFilename4 = dr1["NoIncomeImageFileName"].ToString();
                                if (dr1["NoIncomeImageFileExt"].ToString() != "")
                                    _income.NoIncomeFileExtension4 = dr1["NoIncomeImageFileExt"].ToString();
                                if (dr1["IncomePaper1"].ToString() != "")
                                    _income.incomePaper1 = Convert.ToBoolean(dr1["IncomePaper1"]);
                                if (dr1["IncomePaper2"].ToString() != "")
                                    _income.incomePaper2 = Convert.ToBoolean(dr1["IncomePaper2"]);
                                if (dr1["IncomePaper3"].ToString() != "")
                                    _income.incomePaper3 = Convert.ToBoolean(dr1["IncomePaper3"]);
                                if (dr1["IncomePaper4"].ToString() != "")
                                    _income.incomePaper4 = Convert.ToBoolean(dr1["IncomePaper4"]);
                                if (dr1["NoincomePaper"].ToString() != "")
                                    _income.noincomepaper = Convert.ToBoolean(dr1["NoincomePaper"]);

                                IncomeList.Add(_income);
                            }
                            Info.Income1 = IncomeList;

                        }
                        if (ds.Tables[28] != null && ds.Tables[28].Rows.Count > 0)
                        {
                            List<FamilyHousehold.Parentphone1> _Phonelist = new List<FamilyHousehold.Parentphone1>();
                            FamilyHousehold.Parentphone1 _phoneadd = null;
                            foreach (DataRow row in ds.Tables[28].Rows)
                            {
                                _phoneadd = new FamilyHousehold.Parentphone1();
                                _phoneadd.PPhoneId = Convert.ToInt32(row["Id"]);
                                _phoneadd.PhoneTypeP = row["PhoneType"].ToString();
                                _phoneadd.phonenoP = row["Phoneno"].ToString();
                                _phoneadd.StateP = row["IsPrimaryContact"].ToString() == "" ? false : Convert.ToBoolean(row["IsPrimaryContact"]);
                                _phoneadd.SmsP = row["Sms"].ToString() == "" ? false : Convert.ToBoolean(row["Sms"]);
                                _phoneadd.notesP = row["Notes"].ToString();
                                _Phonelist.Add(_phoneadd);
                            }
                            Info.phoneListParent = _Phonelist;
                            Info.Phonecount = ds.Tables[28].Rows.Count;
                        }
                        if (ds.Tables[29] != null && ds.Tables[29].Rows.Count > 0)
                        {
                            List<FamilyHousehold.PMproblemandservices> _PMproblemandservicesList = new List<FamilyHousehold.PMproblemandservices>();
                            FamilyHousehold.PMproblemandservices info = null;
                            foreach (DataRow dr in ds.Tables[29].Rows)
                            {
                                info = new FamilyHousehold.PMproblemandservices();
                                info.Id = dr["ID"].ToString();
                                info.MasterId = dr["PMPrblmID"].ToString();
                                info.Description = dr["PmDecription"].ToString();
                                info.Parentid = dr["ParentID"].ToString();
                                _PMproblemandservicesList.Add(info);
                            }
                            Info._PMproblem = _PMproblemandservicesList;
                        }
                        if (ds.Tables[30] != null && ds.Tables[30].Rows.Count > 0)
                        {
                            List<FamilyHousehold.PMproblemandservices> _PMproblemandservicesList = new List<FamilyHousehold.PMproblemandservices>();
                            FamilyHousehold.PMproblemandservices info = null;
                            foreach (DataRow dr in ds.Tables[30].Rows)
                            {
                                info = new FamilyHousehold.PMproblemandservices();
                                info.Id = dr["ID"].ToString();
                                info.MasterId = dr["PMServiceID"].ToString();
                                info.Description = dr["PMDescription"].ToString();
                                info.Parentid = dr["ParentID"].ToString();
                                _PMproblemandservicesList.Add(info);
                            }
                            Info._PMservices = _PMproblemandservicesList;
                        }



                    }
                    //Changes
                    if (ds.Tables[26] != null && ds.Tables[26].Rows.Count > 0 && parentid == 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.CenterData> _centerlist = new List<FingerprintsModel.FamilyHousehold.CenterData>();
                            foreach (DataRow dr in ds.Tables[26].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.CenterData obj = new FingerprintsModel.FamilyHousehold.CenterData();
                                obj.Id = dr["center"].ToString();
                                obj.Name = dr["centername"].ToString();
                                _centerlist.Add(obj);
                            }
                            Info.Centers = _centerlist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }
                    if (ds.Tables[27] != null && ds.Tables[27].Rows.Count > 0 && parentid == 0)
                    {
                        List<FamilyHousehold.WorkshopDetails> WorkshopDetails = new List<FamilyHousehold.WorkshopDetails>();
                        foreach (DataRow dr in ds.Tables[27].Rows)
                        {
                            FamilyHousehold.WorkshopDetails obj = new FamilyHousehold.WorkshopDetails();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["WorkshopName"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            WorkshopDetails.Add(obj);
                        }
                        Info.AvailableWorkshop = WorkshopDetails;
                    }
                    if (ds.Tables[28] != null && ds.Tables[28].Rows.Count > 0 && parentid == 0)
                    {

                        Info.WorkshopId = Convert.ToInt32(ds.Tables[28].Rows[0]["ID"]);
                        Info.WorkshopDate = Convert.ToString(ds.Tables[28].Rows[0]["Date"]);//.ToString("MM/dd/yyyy");
                        Info.CenterDetails = Convert.ToString(ds.Tables[28].Rows[0]["Center"]);

                        // Info.EditAllowed = Convert.ToInt32(_dataset.Tables[2].Rows[0]["EditAllowed"]);
                        List<FamilyHousehold.WorkshopDetails> _workshopinfo = new List<FamilyHousehold.WorkshopDetails>();
                        FamilyHousehold.WorkshopDetails obj = null;
                        foreach (DataRow dr in ds.Tables[28].Rows)
                        {
                            obj = new FamilyHousehold.WorkshopDetails();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["WorkshopName"].ToString();
                            obj.IsSelected = true;

                            _workshopinfo.Add(obj);
                        }
                        Info.AvailableWorkshopDetails = _workshopinfo;
                    }

                    //End



                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return Info;
        }
        public DataTable addChildInfoNew(FamilyHousehold obj, int mode, Guid ID)
        {
            string result = string.Empty;
            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Cfirstname", obj.Cfirstname));
                command.Parameters.Add(new SqlParameter("@Clastname", obj.Clastname));
                command.Parameters.Add(new SqlParameter("@Cmiddlename", obj.Cmiddlename));
                command.Parameters.Add(new SqlParameter("@CDOB", obj.CDOB));
                command.Parameters.Add(new SqlParameter("@CGender", obj.CGender));
                command.Parameters.Add(new SqlParameter("@CRace", obj.CRace));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@CNationality", obj.CEthnicity));
                command.Parameters.Add(new SqlParameter("@CAvtr", obj.CAvatarUrl));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_ChildDetails";
                command.ExecuteNonQuery();
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable != null && familydataTable.Rows.Count > 0)
                {
                    obj.Cfirstname = Convert.ToString(familydataTable.Rows[0]["Cfirstname"]);
                    obj.Clastname = Convert.ToString(familydataTable.Rows[0]["Clastname"]);
                    obj.Cmiddlename = Convert.ToString(familydataTable.Rows[0]["Cmiddlename"]);
                    obj.CDOB = Convert.ToString(familydataTable.Rows[0]["CDOB"]);
                    obj.CGender = Convert.ToString(familydataTable.Rows[0]["CGender"]);
                }
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
                return familydataTable;
                // result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return familydataTable;

        }
        public string addEmergencyInfo(FamilyHousehold obj, int mode, Guid ID, List<FamilyHousehold.phone> PhoneNos, string agencyid, string roleid)
        {
            string result = string.Empty;
            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                tranSaction = Connection.BeginTransaction();
                command.Transaction = tranSaction;
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@EmergencyId", obj.EmegencyId));
                command.Parameters.Add(new SqlParameter("@Efirstname", obj.Efirstname));
                command.Parameters.Add(new SqlParameter("@Elastname", obj.Elastname));
                command.Parameters.Add(new SqlParameter("@Emiddlename", obj.Emiddlename));
                command.Parameters.Add(new SqlParameter("@EDOB", obj.EDOB));
                command.Parameters.Add(new SqlParameter("@EGender", obj.EGender));
                command.Parameters.Add(new SqlParameter("@EEmail", obj.EEmail));
                command.Parameters.Add(new SqlParameter("@relationship", obj.ERelationwithchild));
                command.Parameters.Add(new SqlParameter("@Enotes", obj.Enotes));
                command.Parameters.Add(new SqlParameter("@FileName", obj.EFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.EFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.EImageByte));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));

                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                if (PhoneNos != null && PhoneNos.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string)),
                    });
                    foreach (FamilyHousehold.phone phone in PhoneNos)
                    {
                        if (phone.PhoneNo != null && phone.PhoneType != null)
                        {
                            dt.Rows.Add(phone.PhoneType, phone.PhoneNo, phone.IsPrimary, phone.IsSms, phone.Notes, phone.PhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt));

                }
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_EmegencyDetails";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();

                tranSaction.Commit();

            }
            catch (Exception ex)
            {
                if (tranSaction != null)
                    tranSaction.Rollback();
                clsError.WriteException(ex);
                result = "";

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string addRestrictedInfo(FamilyHousehold obj, int mode, Guid ID, string agencyid, string roleid)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@RestrictedId", obj.RestrictedId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Rfirstname", obj.Rfirstname));
                command.Parameters.Add(new SqlParameter("@Rlastname", obj.Rlastname));
                command.Parameters.Add(new SqlParameter("@Rmiddlename", obj.Rmiddlename));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@RDescription", obj.RDescription));
                command.Parameters.Add(new SqlParameter("@FileName", obj.RFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.RFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.RImageByte));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_RestrictedDetails";
                //command.ExecuteNonQuery();
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string addParentInfo(ref FamilyHousehold obj, int mode, Guid ID, List<FamilyHousehold.Parentphone1> ParentPhoneNos,
           List<FamilyHousehold.Parentphone2> ParentPhoneNos1, List<FamilyHousehold.calculateincome> Income, List<FamilyHousehold.calculateincome1> Income1,
            List<FamilyHousehold.ImmunizationRecord> Imminization, List<FamilyHousehold.phone> PhoneNos, Screening _screen, string Roleid, FormCollection collection, HttpFileCollectionBase Files)
        {

            //string pol = obj.PPolicyCouncil;
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                bool IEPForm = false;
                if (collection.Get("IsIEP") == "true,false")
                {
                    IEPForm = true;
                }
                else
                {
                    IEPForm = false;
                }
                bool IFSPForm = false;
                if (collection.Get("IsISFP") == "true,false")
                {
                    IFSPForm = true;
                }
                else
                {
                    IFSPForm = false;
                }

                bool IsExpired = false;
                if (collection.Get("IsExpired") == "true,false")
                {
                    IsExpired = true;
                }
                else
                {
                    IsExpired = false;
                }
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Street", obj.Street));
                command.Parameters.Add(new SqlParameter("@StreetName", obj.StreetName));
                command.Parameters.Add(new SqlParameter("@Apartmentno", obj.Apartmentno));
                command.Parameters.Add(new SqlParameter("@ZipCode", obj.ZipCode));
                command.Parameters.Add(new SqlParameter("@State", obj.State));
                command.Parameters.Add(new SqlParameter("@City", obj.City));
                command.Parameters.Add(new SqlParameter("@nationality", obj.County));
                command.Parameters.Add(new SqlParameter("@fileinbyte1", obj.HImageByte));
                command.Parameters.Add(new SqlParameter("@filename1", obj.HFileName));
                command.Parameters.Add(new SqlParameter("@fileextension1", obj.HFileExtension));
                command.Parameters.Add(new SqlParameter("@AdresssverificationinPaper", obj.AdresssverificationinPaper));
                command.Parameters.Add(new SqlParameter("@TANF", obj.TANF));
                command.Parameters.Add(new SqlParameter("@SSI", obj.SSI));
                command.Parameters.Add(new SqlParameter("@SNAP", obj.SNAP));
                command.Parameters.Add(new SqlParameter("@WIC", obj.WIC));
                command.Parameters.Add(new SqlParameter("@NONE", obj.NONE));//None
                command.Parameters.Add(new SqlParameter("@HomeType", obj.HomeType));
                command.Parameters.Add(new SqlParameter("@PrimaryLanguauge", obj.PrimaryLanguauge));
                command.Parameters.Add(new SqlParameter("@RentType", obj.RentType));
                command.Parameters.Add(new SqlParameter("@FamilyType", obj.FamilyType));
                command.Parameters.Add(new SqlParameter("@Interpretor", obj.Interpretor));
                command.Parameters.Add(new SqlParameter("@ParentRelatioship", obj.ParentRelatioship));
                command.Parameters.Add(new SqlParameter("@ParentRelatioshipOther", obj.ParentRelatioshipOther));
                command.Parameters.Add(new SqlParameter("@OtherLanguageDetail", obj.OtherLanguageDetail));
                command.Parameters.Add(new SqlParameter("@Married", obj.Married));
                //child paremeter
                command.Parameters.Add(new SqlParameter("@ChildId", obj.ChildId));
                command.Parameters.Add(new SqlParameter("@Cfirstname", obj.Cfirstname));
                command.Parameters.Add(new SqlParameter("@Clastname", obj.Clastname));
                command.Parameters.Add(new SqlParameter("@Cmiddlename", obj.Cmiddlename));
                command.Parameters.Add(new SqlParameter("@CprogramType", obj.CProgramType));
                command.Parameters.Add(new SqlParameter("@CDOB", obj.CDOB));
                //Changes
                command.Parameters.Add(new SqlParameter("@CTransportNeeded", obj.CTransportNeeded));
                //End
                command.Parameters.Add(new SqlParameter("@CDOBverifiedby", obj.DOBverifiedBy));
                command.Parameters.Add(new SqlParameter("@CSSN", obj.CSSN == null ? null : EncryptDecrypt.Encrypt(obj.CSSN)));
                command.Parameters.Add(new SqlParameter("@CGender", obj.CGender));
                command.Parameters.Add(new SqlParameter("@CRace", obj.CRace));
                command.Parameters.Add(new SqlParameter("@CRaceSubCategory", obj.CRaceSubCategory));
                command.Parameters.Add(new SqlParameter("@CEthnicity", obj.CEthnicity));
                command.Parameters.Add(new SqlParameter("@CMedicalhome", obj.Medicalhome));
                command.Parameters.Add(new SqlParameter("@Dentalhome", obj.CDentalhome));
                command.Parameters.Add(new SqlParameter("@ImmunizationService", obj.ImmunizationService));
                command.Parameters.Add(new SqlParameter("@medicalservice", obj.MedicalService));
                command.Parameters.Add(new SqlParameter("@Parentdisable", obj.CParentdisable));
                command.Parameters.Add(new SqlParameter("@IsIEP", IEPForm));
                command.Parameters.Add(new SqlParameter("@IsIFSP", IFSPForm));
                command.Parameters.Add(new SqlParameter("@IsExpired", IsExpired));
                //end 
                command.Parameters.Add(new SqlParameter("@Bmistatus", obj.BMIStatus2));
                command.Parameters.Add(new SqlParameter("@FileName2", obj.CFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension2", obj.CFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes2", obj.CImageByte));
                command.Parameters.Add(new SqlParameter("@Dobaddressform", obj.Dobaddressform));
                command.Parameters.Add(new SqlParameter("@DobFileName", obj.DobFileName));
                command.Parameters.Add(new SqlParameter("@DobFileExtension", obj.DobFileExtension));
                command.Parameters.Add(new SqlParameter("@Doctor", obj.Doctor));
                command.Parameters.Add(new SqlParameter("@Dentist", obj.Dentist));
                command.Parameters.Add(new SqlParameter("@Dobpaper", obj.DobverificationinPaper));
                command.Parameters.Add(new SqlParameter("@SchoolDistrict", obj.SchoolDistrict));
                command.Parameters.Add(new SqlParameter("@InsuranceOption", obj.InsuranceOption));
                command.Parameters.Add(new SqlParameter("@MedicalNotice", obj.MedicalNote));
                command.Parameters.Add(new SqlParameter("@IsFoster", obj.IsFoster));
                command.Parameters.Add(new SqlParameter("@Inwalfareagency", obj.Inwalfareagency));
                command.Parameters.Add(new SqlParameter("@InDualcustody", obj.InDualcustody));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileName", obj.ImmunizationFileName));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileExtension", obj.ImmunizationFileExtension));
                command.Parameters.Add(new SqlParameter("@Immunizationfileinbytes", obj.Immunizationfileinbytes));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileName", obj.ReleaseformFileName));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileExtension", obj.ReleaseformFileExtension));
                command.Parameters.Add(new SqlParameter("@Releaseformfileinbytes", obj.Releaseformfileinbytes));
                command.Parameters.Add(new SqlParameter("@ImmunizationinPaper", obj.ImmunizationinPaper));
                command.Parameters.Add(new SqlParameter("@OtherRace", obj.Raceother));
                command.Parameters.Add(new SqlParameter("@HWInput", obj.HWInput));
                command.Parameters.Add(new SqlParameter("@AssessmentDate", obj.AssessmentDate));
                command.Parameters.Add(new SqlParameter("@BHeight", obj.AHeight));
                command.Parameters.Add(new SqlParameter("@bWeight", obj.AWeight));
                command.Parameters.Add(new SqlParameter("@HeadCircle", obj.HeadCircle));








                //Others details parameter
                #region
                //command.Parameters.Add(new SqlParameter("@OthersId", obj.OthersId));
                //command.Parameters.Add(new SqlParameter("@Ofirstname", obj.Ofirstname));
                //command.Parameters.Add(new SqlParameter("@Olastname", obj.Olastname));
                //command.Parameters.Add(new SqlParameter("@Omiddlename", obj.Omiddlename));
                //command.Parameters.Add(new SqlParameter("@ODOB", obj.ODOB));
                //if (obj.Oemergencycontact)
                //    command.Parameters.Add(new SqlParameter("@Isemergency", obj.Oemergencycontact));
                //else
                //    command.Parameters.Add(new SqlParameter("@Isemergency", DBNull.Value));
                //command.Parameters.Add(new SqlParameter("@OGender", obj.OGender));
                //command.Parameters.Add(new SqlParameter("@ParentSSN3", obj.ParentSSN3 == null ? null : EncryptDecrypt.Encrypt(obj.ParentSSN3)));
                //command.Parameters.Add(new SqlParameter("@Othersidgenereated", string.Empty));
                //command.Parameters["@Othersidgenereated"].Direction = ParameterDirection.Output;
                //command.Parameters["@Othersidgenereated"].Size = 10;
                #endregion
                //Emergency details Parameter
                #region
                //command.Parameters.Add(new SqlParameter("@EmergencyId", obj.EmegencyId));
                //command.Parameters.Add(new SqlParameter("@Efirstname", obj.Efirstname));
                //command.Parameters.Add(new SqlParameter("@Elastname", obj.Elastname));
                //command.Parameters.Add(new SqlParameter("@Emiddlename", obj.Emiddlename));
                //command.Parameters.Add(new SqlParameter("@EDOB", obj.EDOB));
                //command.Parameters.Add(new SqlParameter("@EGender", obj.EGender));
                //command.Parameters.Add(new SqlParameter("@EEmail", obj.EEmail));
                //command.Parameters.Add(new SqlParameter("@relationship", obj.ERelationwithchild));
                //command.Parameters.Add(new SqlParameter("@Enotes", obj.Enotes));
                //command.Parameters.Add(new SqlParameter("@EFileName", obj.EFileName));
                //command.Parameters.Add(new SqlParameter("@EFileExtension", obj.EFileExtension));
                //command.Parameters.Add(new SqlParameter("@EFileInBytes", obj.EImageByte));
                //if (PhoneNos != null && PhoneNos.Count > 0)
                //{
                //    DataTable dtphone = new DataTable();
                //    dtphone.Columns.AddRange(new DataColumn[6] { 
                //    new DataColumn("PhoneType", typeof(string)),
                //    new DataColumn("Phoneno",typeof(string)), 
                //    new DataColumn("IsPrimaryContact",typeof(bool)), 
                //    new DataColumn("Sms",typeof(bool)), 
                //    new DataColumn("Notes",typeof(string)), 
                //    new DataColumn("PhoneID",typeof(string)), 
                //    });
                //    foreach (FamilyHousehold.phone phone in PhoneNos)
                //    {
                //        if (phone.PhoneNo != null && phone.PhoneType != null)
                //        {
                //            dtphone.Rows.Add(phone.PhoneType, phone.PhoneNo, phone.IsPrimary, phone.IsSms, phone.Notes, phone.PhoneId);
                //        }
                //    }
                //    command.Parameters.Add(new SqlParameter("@tblphone", dtphone));

                //}
                #endregion
                //restricted Parameter
                #region
                //command.Parameters.Add(new SqlParameter("@RestrictedId", obj.RestrictedId));
                //command.Parameters.Add(new SqlParameter("@Rfirstname", obj.Rfirstname));
                //command.Parameters.Add(new SqlParameter("@Rlastname", obj.Rlastname));
                //command.Parameters.Add(new SqlParameter("@Rmiddlename", obj.Rmiddlename));
                //command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                //command.Parameters.Add(new SqlParameter("@RDescription", obj.RDescription));
                //command.Parameters.Add(new SqlParameter("@RFileName", obj.RFileName));
                //command.Parameters.Add(new SqlParameter("@RFileExtension", obj.RFileExtension));
                //command.Parameters.Add(new SqlParameter("@RFileInBytes", obj.RImageByte));
                #endregion
                if ((mode == 0 && obj.Parentsecondexist == 0) || (mode == 1 && obj.Parentsecondexist == 0))
                {

                    command.Parameters.Add(new SqlParameter("@ParentID", obj.ParentID));
                    command.Parameters.Add(new SqlParameter("@Parentsecondexist", obj.Parentsecondexist));
                    command.Parameters.Add(new SqlParameter("@Pfirstname", obj.Pfirstname));
                    command.Parameters.Add(new SqlParameter("@Plastname", obj.Plastname));
                    command.Parameters.Add(new SqlParameter("@Pmiddlename", obj.Pmidddlename));
                    command.Parameters.Add(new SqlParameter("@Pnotes", obj.Pnotes));
                    command.Parameters.Add(new SqlParameter("@Pnotesother", obj.Pnotesother));
                    command.Parameters.Add(new SqlParameter("@Pemailid", obj.Pemailid));
                    command.Parameters.Add(new SqlParameter("@PDOB", obj.PDOB));
                    command.Parameters.Add(new SqlParameter("@PRole", obj.PRole));
                    command.Parameters.Add(new SqlParameter("@PGender", obj.PGender));
                    command.Parameters.Add(new SqlParameter("@PMilitaryStatus", obj.PMilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@PCurrentlyWorking", obj.PCurrentlyWorking));
                    //command.Parameters.Add(new SqlParameter("@PPolicyCouncil", obj.PPolicyCouncil));
                    command.Parameters.Add(new SqlParameter("@PEnrollment", obj.PEnrollment));
                    command.Parameters.Add(new SqlParameter("@PDegreeEarned", obj.PDegreeEarned));
                    command.Parameters.Add(new SqlParameter("@PGuardiannotes", obj.PGuardiannotes));
                    command.Parameters.Add(new SqlParameter("@FileName3", obj.PFileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension3", obj.PFileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes3", obj.PImageByte));
                    command.Parameters.Add(new SqlParameter("@PQuestion", obj.PQuestion));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherenrolled", obj.Pregnantmotherenrolled));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance", obj.Pregnantmotherprimaryinsurance));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes", obj.Pregnantmotherprimaryinsurancenotes));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled", obj.TrimesterEnrolled));
                    if (string.IsNullOrEmpty(obj.ParentSSN1))
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", EncryptDecrypt.Encrypt(obj.ParentSSN1)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg1", obj.MedicalhomePreg1));
                    command.Parameters.Add(new SqlParameter("@P1Doctor", obj.P1Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail", obj.Noemail1));
                    //
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[33] {
                    new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    DataTable dt1 = new DataTable();
                    dt1.Columns.AddRange(new DataColumn[33] {
                    new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    foreach (FamilyHousehold.calculateincome parentincome in Income)
                    {
                        if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                        {
                            dt.Rows.Add(parentincome.newincomeid, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                  parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                  parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                   parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                   parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                   parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                   parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                   parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                   parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                   );
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblincome", dt));
                    command.Parameters.Add(new SqlParameter("@tblincome1", dt1));


                    DataTable dt2 = new DataTable();
                    dt2.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    DataTable dt3 = new DataTable();
                    dt3.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone1 phone in ParentPhoneNos)
                    {
                        if (phone.phonenoP != null && phone.PhoneTypeP != null)
                        {
                            dt2.Rows.Add(phone.PhoneTypeP, phone.phonenoP, phone.StateP, phone.SmsP, phone.notesP, phone.PPhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt2));
                    command.Parameters.Add(new SqlParameter("@tblphone1", dt3));


                }
                else if (((mode == 0) && (obj.Parentsecondexist != 0)) || ((mode == 1) && (obj.Parentsecondexist != 0)))
                {
                    //Parent1 Parameter
                    command.Parameters.Add(new SqlParameter("@ParentID", obj.ParentID));
                    command.Parameters.Add(new SqlParameter("@ParentID1", obj.ParentID1));
                    command.Parameters.Add(new SqlParameter("@Parentsecondexist", obj.Parentsecondexist));
                    command.Parameters.Add(new SqlParameter("@Pfirstname", obj.Pfirstname));
                    command.Parameters.Add(new SqlParameter("@Plastname", obj.Plastname));
                    command.Parameters.Add(new SqlParameter("@Pmiddlename", obj.Pmidddlename));
                    command.Parameters.Add(new SqlParameter("@Pnotes", obj.Pnotes));
                    command.Parameters.Add(new SqlParameter("@Pnotesother", obj.Pnotesother));
                    command.Parameters.Add(new SqlParameter("@Pemailid", obj.Pemailid));
                    command.Parameters.Add(new SqlParameter("@PDOB", obj.PDOB));
                    command.Parameters.Add(new SqlParameter("@PRole", obj.PRole));
                    command.Parameters.Add(new SqlParameter("@PGender", obj.PGender));
                    command.Parameters.Add(new SqlParameter("@PMilitaryStatus", obj.PMilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@PCurrentlyWorking", obj.PCurrentlyWorking));
                    //command.Parameters.Add(new SqlParameter("@PPolicyCouncil", obj.PPolicyCouncil));
                    command.Parameters.Add(new SqlParameter("@PEnrollment", obj.PEnrollment));
                    command.Parameters.Add(new SqlParameter("@PDegreeEarned", obj.PDegreeEarned));
                    command.Parameters.Add(new SqlParameter("@PphoneType", obj.PphoneType));
                    command.Parameters.Add(new SqlParameter("@PState", obj.PState));
                    command.Parameters.Add(new SqlParameter("@FileName3", obj.PFileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension3", obj.PFileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes3", obj.PImageByte));
                    command.Parameters.Add(new SqlParameter("@PSms", obj.PSms));
                    command.Parameters.Add(new SqlParameter("@PGuardiannotes", obj.PGuardiannotes));
                    command.Parameters.Add(new SqlParameter("@PQuestion", obj.PQuestion));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherenrolled", obj.Pregnantmotherenrolled));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance", obj.Pregnantmotherprimaryinsurance));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes", obj.Pregnantmotherprimaryinsurancenotes));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled", obj.TrimesterEnrolled));
                    if (string.IsNullOrEmpty(obj.ParentSSN1))
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", EncryptDecrypt.Encrypt(obj.ParentSSN1)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg1", obj.MedicalhomePreg1));
                    command.Parameters.Add(new SqlParameter("@P1Doctor", obj.P1Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail", obj.Noemail1));
                    //


                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[33] {
                 new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    foreach (FamilyHousehold.calculateincome parentincome in Income)
                    {
                        if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                        {
                            dt.Rows.Add(parentincome.newincomeid, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                  parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                  parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                   parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                   parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                   parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                   parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                   parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                   parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                   );
                        }
                    }
                    DataTable dt1 = new DataTable();
                    dt1.Columns.AddRange(new DataColumn[33] {
                   new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                     new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    if (Income1 != null)
                    {
                        foreach (FamilyHousehold.calculateincome1 parentincome in Income1)
                        {
                            if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                            {
                                dt1.Rows.Add(parentincome.IncomeID, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                      parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                      parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                       parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                       parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                       parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                       parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                       parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                       parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                       );
                            }
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblincome", dt));
                    command.Parameters.Add(new SqlParameter("@tblincome1", dt1));
                    //Parent2 Parameter
                    command.Parameters.Add(new SqlParameter("@P1firstname", obj.P1firstname));
                    command.Parameters.Add(new SqlParameter("@P1lastname", obj.P1lastname));
                    command.Parameters.Add(new SqlParameter("@P1middlename", obj.P1midddlename));
                    command.Parameters.Add(new SqlParameter("@P1phoneno", obj.P1phoneno));
                    command.Parameters.Add(new SqlParameter("@P1Avtr", obj.P1AvatarUrl));
                    command.Parameters.Add(new SqlParameter("@P1notes", obj.P1notes));
                    command.Parameters.Add(new SqlParameter("@P1notesother", obj.P1notesother));
                    command.Parameters.Add(new SqlParameter("@P1emailid", obj.P1emailid));
                    command.Parameters.Add(new SqlParameter("@P1DOB", obj.P1DOB));
                    command.Parameters.Add(new SqlParameter("@P1Role", obj.P1Role));
                    command.Parameters.Add(new SqlParameter("@P1Gender", obj.P1Gender));
                    command.Parameters.Add(new SqlParameter("@P1MilitaryStatus", obj.P1MilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@P1CurrentlyWorking", obj.P1CurrentlyWorking));
                    command.Parameters.Add(new SqlParameter("@P1Enrollment", obj.P1Enrollment));
                    command.Parameters.Add(new SqlParameter("@P1DegreeEarned", obj.P1DegreeEarned));
                    command.Parameters.Add(new SqlParameter("@P1phoneType", obj.P1phoneType));
                    command.Parameters.Add(new SqlParameter("@P1State", obj.P1State));
                    command.Parameters.Add(new SqlParameter("@P1Sms", obj.P1Sms));
                    command.Parameters.Add(new SqlParameter("@P1Guardiannotes", obj.P1Guardiannotes));
                    command.Parameters.Add(new SqlParameter("@FileName4", obj.P1FileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension4", obj.P1FileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes4", obj.P1ImageByte));
                    command.Parameters.Add(new SqlParameter("@P1Question", obj.P1Question));
                    command.Parameters.Add(new SqlParameter("@PregnantmotherenrolledP1", obj.PregnantmotherenrolledP1));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance1", obj.Pregnantmotherprimaryinsurance1));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes1", obj.Pregnantmotherprimaryinsurancenotes1));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled1", obj.TrimesterEnrolled1));
                    if (string.IsNullOrEmpty(obj.ParentSSN2))
                        command.Parameters.Add(new SqlParameter("@ParentSSN2", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN2", EncryptDecrypt.Encrypt(obj.ParentSSN2)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg2", obj.MedicalhomePreg2));
                    command.Parameters.Add(new SqlParameter("@P2Doctor", obj.P2Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail1", obj.Noemail2));
                    //for phone
                    DataTable dt2 = new DataTable();
                    dt2.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone1 phone in ParentPhoneNos)
                    {
                        if (phone.phonenoP != null && phone.PhoneTypeP != null)
                        {
                            dt2.Rows.Add(phone.PhoneTypeP, phone.phonenoP, phone.StateP, phone.SmsP, phone.notesP, phone.PPhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt2));
                    DataTable dt3 = new DataTable();
                    dt3.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone2 phone in ParentPhoneNos1)
                    {
                        if (phone.phonenoP1 != null && phone.PhoneTypeP1 != null)
                        {
                            dt3.Rows.Add(phone.PhoneTypeP1, phone.phonenoP1, phone.StateP1, phone.SmsP1, phone.notesP1, phone.PPhoneId1);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone1", dt3));
                }



                DataTable dt5 = new DataTable();
                dt5.Columns.AddRange(new DataColumn[18] {
                    new DataColumn("Immunizationmasterid", typeof(Int32)),
                    new DataColumn("ImmunizationId", typeof(Int32)),
                    new DataColumn("Dose",typeof(string)),
                    new DataColumn("Dose1",typeof(string)),
                    new DataColumn("Preempt1",typeof(bool)),
                    new DataColumn("Exempt1",typeof(bool)),
                    new DataColumn("Dose2",typeof(string)),
                    new DataColumn("Preempt2",typeof(bool)),
                    new DataColumn("Exempt2",typeof(bool)),
                    new DataColumn("Dose3",typeof(string)),
                    new DataColumn("Preempt3",typeof(bool)),
                    new DataColumn("Exempt3",typeof(bool)),
                     new DataColumn("Dose4",typeof(string)),
                    new DataColumn("Preempt4",typeof(bool)),
                    new DataColumn("Exempt4",typeof(bool)),
                     new DataColumn("Dose5",typeof(string)),
                    new DataColumn("Preempt5",typeof(bool)),
                    new DataColumn("Exempt5",typeof(bool)),

                    });
                if (Imminization != null)
                {
                    foreach (FamilyHousehold.ImmunizationRecord _Imminization in Imminization)
                    {
                        dt5.Rows.Add(_Imminization.ImmunizationmasterId, _Imminization.ImmunizationId, _Imminization.Dose, _Imminization.Dose1, _Imminization.Preempt1, _Imminization.Exempt1
                            , _Imminization.Dose2, _Imminization.Preempt2, _Imminization.Exempt2, _Imminization.Dose3, _Imminization.Preempt3, _Imminization.Exempt3
                            , _Imminization.Dose4, _Imminization.Preempt4, _Imminization.Exempt4, _Imminization.Dose5, _Imminization.Preempt5, _Imminization.Exempt5);
                    }
                }

                #region screening
                DataTable dt6 = new DataTable();
                dt6.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),
                    });
                #endregion
                foreach (var s in _screen.GetType().GetProperties())
                {
                    int screeningid = 0;
                    int questionid = 0;
                    if (s.Name.Substring(0, 1) == "F")
                        screeningid = 1;
                    if (s.Name.Substring(0, 1) == "v")
                        screeningid = 2;
                    if (s.Name.Substring(0, 1) == "h")
                        screeningid = 3;
                    if (s.Name.Substring(0, 1) == "d")
                        screeningid = 4;
                    if (s.Name.Substring(0, 1) == "E")
                        screeningid = 5;
                    if (s.Name.Substring(0, 1) == "s")
                        screeningid = 6;
                    if (screeningid == 1 || screeningid == 2 || screeningid == 3 || screeningid == 4 || screeningid == 5 || screeningid == 6)
                    {
                        questionid = Convert.ToInt32(s.Name.Substring(1, 3));
                        dt6.Rows.Add(screeningid, questionid, s.GetValue(_screen));
                    }
                }
                command.Parameters.Add(new SqlParameter("@tblImminization", dt5));
                command.Parameters.Add(new SqlParameter("@tblscreening", dt6));
                //Changes
                command.Parameters.Add(new SqlParameter("@Physical", _screen.AddPhysical));
                command.Parameters.Add(new SqlParameter("@Vision", _screen.AddVision));
                command.Parameters.Add(new SqlParameter("@Dental", _screen.AddDental));
                command.Parameters.Add(new SqlParameter("@Hearing", _screen.AddHearing));
                command.Parameters.Add(new SqlParameter("@Develop", _screen.AddDevelop));
                command.Parameters.Add(new SqlParameter("@Speech", _screen.AddSpeech));
                //    command.Parameters.Add(new SqlParameter("@ScreeningAccept", _screen.ScreeningAccept));
                command.Parameters.Add(new SqlParameter("@PhysicalFileName", _screen.PhysicalFileName));
                command.Parameters.Add(new SqlParameter("@PhysicalFileExtension", _screen.PhysicalFileExtension));
                command.Parameters.Add(new SqlParameter("@PhysicalImageByte", _screen.PhysicalImageByte));
                command.Parameters.Add(new SqlParameter("@VisionFileName", _screen.VisionFileName));
                command.Parameters.Add(new SqlParameter("@VisionFileExtension", _screen.VisionFileExtension));
                command.Parameters.Add(new SqlParameter("@VisionImageByte", _screen.VisionImageByte));
                command.Parameters.Add(new SqlParameter("@DevelopFileName", _screen.DevelopFileName));
                command.Parameters.Add(new SqlParameter("@DevelopFileExtension", _screen.DevelopFileExtension));
                command.Parameters.Add(new SqlParameter("@DevelopImageByte", _screen.DevelopImageByte));
                command.Parameters.Add(new SqlParameter("@DentalFileExtension", _screen.DentalFileExtension));
                command.Parameters.Add(new SqlParameter("@DentalFileName", _screen.DentalFileName));
                command.Parameters.Add(new SqlParameter("@DentalImageByte", _screen.DentalImageByte));
                command.Parameters.Add(new SqlParameter("@HearingFileName", _screen.HearingFileName));
                command.Parameters.Add(new SqlParameter("@HearingFileExtension", _screen.HearingFileExtension));
                command.Parameters.Add(new SqlParameter("@HearingImageByte", _screen.HearingImageByte));
                command.Parameters.Add(new SqlParameter("@SpeechFileName", _screen.SpeechFileName));
                command.Parameters.Add(new SqlParameter("@SpeechFileExtension", _screen.SpeechFileExtension));
                command.Parameters.Add(new SqlParameter("@SpeechImageByte", _screen.SpeechImageByte));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileExtension", _screen.ScreeningAcceptFileExtension));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileName", _screen.ScreeningAcceptFileName));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptImageByte", _screen.ScreeningAcceptImageByte));
                //Changes
                command.Parameters.Add(new SqlParameter("@Consolidated", _screen.Consolidated));
                command.Parameters.Add(new SqlParameter("@ParentName", _screen.Parentname));

                #region Parent1,Parent2 health question
                command.Parameters.Add(new SqlParameter("@PMVisitDoc", obj.PMVisitDoc));
                command.Parameters.Add(new SqlParameter("@PMProblem", obj.PMProblem));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues", obj.PMOtherIssues));
                command.Parameters.Add(new SqlParameter("@PMConditions", obj.PMConditions));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc", obj.PMCondtnDesc));
                command.Parameters.Add(new SqlParameter("@PMRisk", obj.PMRisk));
                command.Parameters.Add(new SqlParameter("@PMDentalExam", obj.PMDentalExam));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate", obj.PMDentalExamDate));
                command.Parameters.Add(new SqlParameter("@PMNeedDental", obj.PMNeedDental));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental", obj.PMRecieveDental));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices", obj._Pregnantmotherpmservices));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem", obj._Pregnantmotherproblem));
                command.Parameters.Add(new SqlParameter("@PMVisitDoc1", obj.PMVisitDoc1));
                command.Parameters.Add(new SqlParameter("@PMProblem1", obj.PMProblem1));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues1", obj.PMOtherIssues1));
                command.Parameters.Add(new SqlParameter("@PMConditions1", obj.PMConditions1));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc1", obj.PMCondtnDesc1));
                command.Parameters.Add(new SqlParameter("@PMRisk1", obj.PMRisk1));
                command.Parameters.Add(new SqlParameter("@PMDentalExam1", obj.PMDentalExam1));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate1", obj.PMDentalExamDate1));
                command.Parameters.Add(new SqlParameter("@PMNeedDental1", obj.PMNeedDental1));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental1", obj.PMRecieveDental1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices1", obj._Pregnantmotherpmservices1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem1", obj._Pregnantmotherproblem1));
                #endregion
                #region child health Ehs
                command.Parameters.Add(new SqlParameter("@EHsChildBorn", obj.EhsChildBorn));
                command.Parameters.Add(new SqlParameter("@EhsChildBirthWt", obj.EhsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@EhsChildLength", obj.EhsChildLength));
                command.Parameters.Add(new SqlParameter("@EhsChildProblm", obj.EhsChildProblm));
                command.Parameters.Add(new SqlParameter("@EhsMedication", obj.EhsMedication));
                command.Parameters.Add(new SqlParameter("@EHSmpplan", obj.EHSmpplan));
                command.Parameters.Add(new SqlParameter("@EHSmpplanComment", obj.EHSmpplancomment));
                //10082016
                command.Parameters.Add(new SqlParameter("@EHSBabyOrMotherProblems", obj.EHSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@EHSChildMedication", obj.EHSChildMedication));

                command.Parameters.Add(new SqlParameter("@EHSAllergy", obj.EHSAllergy));
                command.Parameters.Add(new SqlParameter("@EHSEpiPen", obj.EHSEpiPen));


                //
                command.Parameters.Add(new SqlParameter("@EhsComment", obj.EhsComment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeEhs", obj._ChildDirectBloodRelativeEhs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsEhs", obj._ChildDiagnosedConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsEhs", obj._ChildChronicHealthConditions2Ehs));
                command.Parameters.Add(new SqlParameter("@ChildreceivedChronicHealthConditionsEhs", obj._ChildChronicHealthConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceivingChronicHealthConditionsEhs", obj._ChildChronicHealthConditions1Ehs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentEhs", obj._ChildMedicalTreatmentEhs));
                #endregion
                #region child health Hs
                command.Parameters.Add(new SqlParameter("@HsChildBorn", obj.HsChildBorn));
                command.Parameters.Add(new SqlParameter("@HsChildBirthWt", obj.HsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@HsChildLength", obj.HsChildLength));
                command.Parameters.Add(new SqlParameter("@HsChildProblm", obj.HsChildProblm));
                command.Parameters.Add(new SqlParameter("@HsMedication", obj.HsMedication));
                command.Parameters.Add(new SqlParameter("@HSmpplan", obj.HSmpplan));
                command.Parameters.Add(new SqlParameter("@HSmpplanComment", obj.HSmpplanComment));
                command.Parameters.Add(new SqlParameter("@HsDentalExam", obj.HsDentalExam));
                command.Parameters.Add(new SqlParameter("@HsComment", obj.HsComment));
                command.Parameters.Add(new SqlParameter("@HsChildDentalCare", obj.HsChildDentalCare));
                command.Parameters.Add(new SqlParameter("@HsRecentDentalExam", obj.HsRecentDentalExam));




                command.Parameters.Add(new SqlParameter("@HsChildNeedDentalTreatment", obj.HsChildNeedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HsChildRecievedDentalTreatment", obj.HsChildRecievedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeHs", obj._ChildDirectBloodRelativeHs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsHs", obj._ChildDiagnosedConditionsHs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentHs", obj._ChildMedicalTreatmentHs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsHs", obj._ChildChronicHealthConditionsHs));
                //10082016
                command.Parameters.Add(new SqlParameter("@HSBabyOrMotherProblems", obj.HSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@HsMedicationName", obj.HsMedicationName));
                command.Parameters.Add(new SqlParameter("@HsDosage", obj.HsDosage));
                command.Parameters.Add(new SqlParameter("@HSChildMedication", obj.HSChildMedication));
                command.Parameters.Add(new SqlParameter("@HSPreventativeDentalCare", obj.HSPreventativeDentalCare));
                command.Parameters.Add(new SqlParameter("@HSProfessionalDentalExam", obj.HSProfessionalDentalExam));
                command.Parameters.Add(new SqlParameter("@HSNeedingDentalTreatment", obj.HSNeedingDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HSChildReceivedDentalTreatment", obj.HSChildReceivedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@ChildProfessionalDentalExam", obj.ChildProfessionalDentalExam));//new ques added
                //
                #endregion
                //#region child nutrition
                //command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                //command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                //command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                //command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                //command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                //command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                //command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                //command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                //command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                //command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                //command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                //command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                //command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                //command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                //command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                //command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                //command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                //command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                //command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                //command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                //command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                //command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                //command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                //command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                //command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                //command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                //command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                //command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                //command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                //command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                //command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                //command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                //command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                //command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                //command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                //command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                //command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                //command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                //command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                //command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                //command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                //command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                //command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                //command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                //command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                //command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                //command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                //command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                //command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                //command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                //command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                //#endregion


                //Added by Akansha on 19Dec2016
                // child nutrition with HS/Ehs
                if (obj._childprogrefid == "1")  //Ehs Questions
                {
                    #region child nutrition

                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.EhsChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.EhsPersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.EhsPersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.EhsPersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.EhsDramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.EhsRecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.EhsRecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.EhsChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.EhsFoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.EhsNutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.EhsFoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.EhschildTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.Ehsspoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.Ehsfeedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.EhschildThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.EhsTakebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.Ehschewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.EhsChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.EhsChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.EhsMilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));//Differ
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));//End
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.EhsChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.EhsChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.EhsBreakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.EhsLunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.EhsSnack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.EhsDinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.EhsNA));
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.EhsRestrictFood));//New ques added

                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.EhsNauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.EhsDiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.EhsConstipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.EhsDramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.EhsRecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.EhsRecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.EhsSpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.EhsFoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.EhsNutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.EhsChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.EhsSpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.EhsSpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.EhsBottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EhsEatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                if (obj._childprogrefid == "2")  //hs Questions
                {
                    #region child nutrition
                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.ChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.RestrictFood));//New ques added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                //End

                //custom screenig save


                #region custom screening
                DataTable screeningquestion = new DataTable();
                screeningquestion.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),
                    new DataColumn("OptionID",typeof(Int32)),
                    new DataColumn("ScreeningDate",typeof(string))
                    });
                #endregion
                #region allowed screening
                DataTable screeningallowed = new DataTable();
                screeningallowed.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("Allowed",typeof(Int32)),
                    new DataColumn("FileName",typeof(string)),
                    new DataColumn("FileExtension",typeof(string)),
                    new DataColumn("FileBytes",typeof(byte[]))
                    });
                #endregion



                if (collection != null)
                {
                    foreach (var radio in collection.AllKeys.Where(P => P.Contains("_allowchildcustomscreening")))
                    {
                        if (collection[radio].ToString() == "1")
                        {
                            foreach (var question in collection.AllKeys.Where(P => P.Contains("_custscreeningquestin") && P.Split('k')[1] == radio.Split('@')[0]))
                            {

                                string questionid = string.Empty;
                                string optionid = string.Empty;
                                string screeningdate = "";
                                if (question.ToString().Contains("o") || question.ToString().Contains("k"))
                                    questionid = question.ToString().Split('k', 'k')[2];
                                if (question.ToString().Contains("o"))
                                {
                                    optionid = question.ToString().Split('o', 'o')[1];
                                    questionid = question.ToString().Split('k', 'k')[2];
                                }
                                if (question.ToString().Contains("_custrad"))
                                {
                                    optionid = collection[question].ToString().Split('o', 'o')[1];
                                    questionid = collection[question].ToString().Split('k', 'k')[2];
                                }
                                if (question.Contains("_$SD"))
                                    screeningdate = collection[question].ToString();
                                if (string.IsNullOrEmpty(optionid))
                                {
                                    if (question.ToString().Contains("select"))
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString().Replace(",", ""), DBNull.Value, screeningdate);
                                    else
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString(), DBNull.Value, screeningdate);


                                }
                                else
                                {
                                    screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, DBNull.Value, optionid, screeningdate);
                                }
                                optionid = "";
                                questionid = "";


                            }



                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }




                        }
                        else
                        {


                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }

                        }
                    }
                }
                //End

                command.Parameters.Add(new SqlParameter("@screeningquestion", screeningquestion));
                command.Parameters.Add(new SqlParameter("@screeningallowed", screeningallowed));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusChild", obj.ApplicationStatusChild));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusParent1", obj.ApplicationStatusParent1));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusParent2", obj.ApplicationStatusParent2));
                command.Parameters.Add(new SqlParameter("@Roleid", Roleid));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_FamilyDetails_info";//SP_FamilyDetails_info_TempNutritn
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                DataAdapter.Dispose();
                command.Dispose();
                result = command.Parameters["@result"].Value.ToString();
                //other paremeter
                //obj.EmegencyId = Convert.ToInt32(command.Parameters["@Othersidgenereated"].Value);

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public void GetallHouseholdinfo(FamilyHousehold obj, DataSet _dataset)
        {
            if (_dataset != null && _dataset.Tables.Count > 0)
            {
                if (_dataset.Tables[0].Rows.Count > 0)
                {
                    //house hold details
                    obj.HouseholdId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                    obj.Encrypthouseholid = EncryptDecrypt.Encrypt64(_dataset.Tables[0].Rows[0]["ID"].ToString());
                    obj.FamilyHouseholdID = Convert.ToInt32(_dataset.Tables[0].Rows[0]["familyhouseholdid"]);
                    obj.EditAllowed = Convert.ToInt32(_dataset.Tables[0].Rows[0]["EditAllowed"]);
                    obj.RequestAllowed = Convert.ToInt32(_dataset.Tables[0].Rows[0]["RequestAllowed"]);
                    obj.Street = Convert.ToString(_dataset.Tables[0].Rows[0]["Street"]);
                    obj.StreetName = Convert.ToString(_dataset.Tables[0].Rows[0]["StreetName"]);
                    obj.City = Convert.ToString(_dataset.Tables[0].Rows[0]["City"]);
                    obj.ZipCode = Convert.ToString(_dataset.Tables[0].Rows[0]["ZipCode"]);
                    obj.State = Convert.ToString(_dataset.Tables[0].Rows[0]["State"]);
                    obj.County = Convert.ToString(_dataset.Tables[0].Rows[0]["County"]);
                    obj.HFileName = Convert.ToString(_dataset.Tables[0].Rows[0]["FileName"]);
                    obj.HFileExtension = Convert.ToString(_dataset.Tables[0].Rows[0]["FileExtension"]);
                    if (_dataset.Tables[0].Rows[0]["Hpaper"].ToString() != "")
                        obj.AdresssverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Hpaper"]);
                    //Family householddetails
                    //
                    if (_dataset.Tables[0].Rows[0]["RentType"].ToString() != "")
                        obj.RentType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["RentType"]);
                    if (_dataset.Tables[0].Rows[0]["TANF"].ToString() != "")
                        obj.TANF = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TANF"]);
                    if (_dataset.Tables[0].Rows[0]["HouseType"].ToString() != "")
                        obj.HomeType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["HouseType"]);
                    if (_dataset.Tables[0].Rows[0]["FamilyType"].ToString() != "")
                        obj.FamilyType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FamilyType"]);
                    if (_dataset.Tables[0].Rows[0]["SSI"].ToString() != "")
                        obj.SSI = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SSI"]);
                    if (_dataset.Tables[0].Rows[0]["WIC"].ToString() != "")
                        obj.WIC = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WIC"]);
                    if (_dataset.Tables[0].Rows[0]["SNAP"].ToString() != "")
                        obj.SNAP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SNAP"]);
                    if (_dataset.Tables[0].Rows[0]["NONE"].ToString() != "")
                        obj.NONE = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NONE"]);
                    if (_dataset.Tables[0].Rows[0]["Interpretor"].ToString() != "")
                        obj.Interpretor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Interpretor"]);
                    if (_dataset.Tables[0].Rows[0]["ParentRelatioship"].ToString() != "")
                        obj.ParentRelatioship = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentRelatioship"]);
                    if (_dataset.Tables[0].Rows[0]["OtherRelationship"].ToString() != "")
                        obj.ParentRelatioshipOther = _dataset.Tables[0].Rows[0]["OtherRelationship"].ToString();
                    if (_dataset.Tables[0].Rows[0]["OtherDesc"].ToString() != "")
                        obj.OtherLanguageDetail = _dataset.Tables[0].Rows[0]["OtherDesc"].ToString();
                    obj.PrimaryLanguauge = Convert.ToString(_dataset.Tables[0].Rows[0]["PrimaryLanguauge"]);
                    obj.Married = _dataset.Tables[0].Rows[0]["Married"].ToString();
                    obj.docstorage = Convert.ToInt32(_dataset.Tables[0].Rows[0]["docsStorage"]);


                    //obj.DeleteAllowed = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DeleteAllowed"]);

                    //Parent1 details
                    obj.ExistPmprogram = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Pmprogram"]);
                    obj.Pfirstname = _dataset.Tables[0].Rows[0]["Pfirstname"].ToString();
                    obj.Plastname = _dataset.Tables[0].Rows[0]["PLastName"].ToString();
                    obj.Pmidddlename = _dataset.Tables[0].Rows[0]["PMiddlename"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PDOB"].ToString() != "")
                        obj.PDOB = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["PDOB"]).ToString("MM/dd/yyyy");
                    obj.PGender = _dataset.Tables[0].Rows[0]["PGender"].ToString();
                    obj.Pemailid = _dataset.Tables[0].Rows[0]["PEmailid"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PMilitaryStatus"].ToString() != "")
                        obj.PMilitaryStatus = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMilitaryStatus"]);
                    obj.PEnrollment = _dataset.Tables[0].Rows[0]["PEnrollment"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PDegreeEarned"].ToString() != "")
                        obj.PDegreeEarned = Convert.ToString(_dataset.Tables[0].Rows[0]["PDegreeEarned"]);
                    obj.Pnotesother = _dataset.Tables[0].Rows[0]["PNotes"].ToString();
                    obj.PRole = _dataset.Tables[0].Rows[0]["ParentRole"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PCurrentlyWorking"].ToString() != "")
                        obj.PCurrentlyWorking = _dataset.Tables[0].Rows[0]["PCurrentlyWorking"].ToString();
                    if (_dataset.Tables[0].Rows[0]["ParentId"].ToString() != "")
                        obj.ParentID = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentId"]);
                    if (_dataset.Tables[0].Rows[0]["IsPreg"].ToString() != "")
                        obj.PQuestion = _dataset.Tables[0].Rows[0]["IsPreg"].ToString();
                    if (_dataset.Tables[0].Rows[0]["EnrollforPregnant"].ToString() != "")
                        obj.Pregnantmotherenrolled = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["EnrollforPregnant"]);
                    if (_dataset.Tables[0].Rows[0]["motherinsurance"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurance = Convert.ToInt32(_dataset.Tables[0].Rows[0]["motherinsurance"]);
                    if (_dataset.Tables[0].Rows[0]["insurancenotemother"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurancenotes = _dataset.Tables[0].Rows[0]["insurancenotemother"].ToString();
                    if (_dataset.Tables[0].Rows[0]["TrimesterEnrolled"].ToString() != "")
                        obj.TrimesterEnrolled = Convert.ToInt32(_dataset.Tables[0].Rows[0]["TrimesterEnrolled"]);
                    try
                    {
                        obj.ParentSSN1 = _dataset.Tables[0].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[0]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.ParentSSN1 = _dataset.Tables[0].Rows[0]["SSN"].ToString();
                    }
                    if (_dataset.Tables[0].Rows[0]["Medicalhome"].ToString() != "")
                        obj.MedicalhomePreg1 = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Medicalhome"]);
                    if (_dataset.Tables[0].Rows[0]["Doctorvalue"].ToString() != "")
                        obj.P1Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Doctorvalue"]);
                    obj.CDoctorP1 = _dataset.Tables[0].Rows[0]["doctorname"].ToString();
                    if (_dataset.Tables[0].Rows[0]["NoEmail"].ToString() != "")
                        obj.Noemail1 = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoEmail"]);
                    obj.PMVisitDoc = _dataset.Tables[0].Rows[0]["PMVisitDoc"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PMProblmID"].ToString() != "")
                        obj.PMProblem = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMProblmID"]);
                    obj.PMOtherIssues = _dataset.Tables[0].Rows[0]["PMIssues"].ToString();
                    obj.PMConditions = _dataset.Tables[0].Rows[0]["PMConditionID"].ToString();
                    obj.PMCondtnDesc = _dataset.Tables[0].Rows[0]["PMConditionDescID"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PMRisk"].ToString() != "")
                        obj.PMRisk = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["PMRisk"]);
                    if (_dataset.Tables[0].Rows[0]["PMDentalExam"].ToString() != "")
                        obj.PMDentalExam = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMDentalExam"]);
                    if (_dataset.Tables[0].Rows[0]["PMDentalDate"].ToString() != "")
                        obj.PMDentalExamDate = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["PMDentalDate"]).ToString("MM/dd/yyyy");
                    if (_dataset.Tables[0].Rows[0]["PMNeedDental"].ToString() != "")
                        obj.PMNeedDental = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMNeedDental"]);
                    if (_dataset.Tables[0].Rows[0]["PMRecieveDental"].ToString() != "")
                        obj.PMRecieveDental = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMRecieveDental"]);
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.calculateincome> IncomeList = new List<FamilyHousehold.calculateincome>();
                        DataRow[] dr = _dataset.Tables[1].Select("ParentId=" + Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentId"]));
                        foreach (DataRow dr1 in dr)
                        {
                            FamilyHousehold.calculateincome _income = new FamilyHousehold.calculateincome();
                            _income.newincomeid = Convert.ToInt32(dr1["IncomeId"]);
                            if (dr1["Income"].ToString() != "")
                                _income.Income = Convert.ToInt32(dr1["Income"]);
                            if (dr1["IncomeSource1"].ToString() != "")
                                _income.IncomeSource1 = dr1["IncomeSource1"].ToString();
                            if (dr1["IncomeAmt1"].ToString() != "")
                                _income.AmountVocher1 = Convert.ToDecimal(dr1["IncomeAmt1"]);
                            if (dr1["IncomeAmt2"].ToString() != "")
                                _income.AmountVocher2 = Convert.ToDecimal(dr1["IncomeAmt2"]);
                            if (dr1["IncomeAmt3"].ToString() != "")
                                _income.AmountVocher3 = Convert.ToDecimal(dr1["IncomeAmt3"]);
                            if (dr1["IncomeAmt4"].ToString() != "")
                                _income.AmountVocher4 = Convert.ToDecimal(dr1["IncomeAmt4"]);
                            if (dr1["PayFrequency"].ToString() != "")
                                _income.Payfrequency = Convert.ToInt32(dr1["PayFrequency"]);
                            if (dr1["WorkingPeriod"].ToString() != "")
                                _income.Working = Convert.ToInt32(dr1["WorkingPeriod"]);
                            if (dr1["TotalIncome"].ToString() != "")
                                _income.IncomeCalculated = Convert.ToDecimal(dr1["TotalIncome"]);
                            //if (dr1["Image1"].ToString() != "")
                            //    _income.SalaryAvatar1bytes = (byte[])dr1["Image1"];
                            //if (dr1["Image2"].ToString() != "")
                            //    _income.SalaryAvatar2bytes = (byte[])dr1["Image2"];
                            //if (dr1["Image3"].ToString() != "")
                            //    _income.SalaryAvatar3bytes = (byte[])dr1["Image3"];
                            //if (dr1["Image4"].ToString() != "")
                            //    _income.SalaryAvatar1bytes = (byte[])dr1["Image4"];
                            if (dr1["Image1FileName"].ToString() != "")
                                _income.SalaryAvatarFilename1 = dr1["Image1FileName"].ToString();
                            if (dr1["Image2FileName"].ToString() != "")
                                _income.SalaryAvatarFilename2 = dr1["Image2FileName"].ToString();
                            if (dr1["Image3FileName"].ToString() != "")
                                _income.SalaryAvatarFilename3 = dr1["Image3FileName"].ToString();
                            if (dr1["Image4FileName"].ToString() != "")
                                _income.SalaryAvatarFilename4 = dr1["Image4FileName"].ToString();
                            if (dr1["Image1FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension1 = dr1["Image1FileExt"].ToString();
                            if (dr1["Image2FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension2 = dr1["Image2FileExt"].ToString();
                            if (dr1["Image3FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension3 = dr1["Image3FileExt"].ToString();
                            if (dr1["Image4FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension4 = dr1["Image4FileExt"].ToString();
                            //if (dr1["NoIncomeImage"].ToString() != "")
                            //    _income.NoIncomeAvatarbytes = (byte[])dr1["NoIncomeImage"];
                            if (dr1["NoIncomeImageFileName"].ToString() != "")
                                _income.NoIncomeFilename4 = dr1["NoIncomeImageFileName"].ToString();
                            if (dr1["NoIncomeImageFileExt"].ToString() != "")
                                _income.NoIncomeFileExtension4 = dr1["NoIncomeImageFileExt"].ToString();
                            if (dr1["IncomePaper1"].ToString() != "")
                                _income.incomePaper1 = Convert.ToBoolean(dr1["IncomePaper1"]);
                            if (dr1["IncomePaper2"].ToString() != "")
                                _income.incomePaper2 = Convert.ToBoolean(dr1["IncomePaper2"]);
                            if (dr1["IncomePaper3"].ToString() != "")
                                _income.incomePaper3 = Convert.ToBoolean(dr1["IncomePaper3"]);
                            if (dr1["IncomePaper4"].ToString() != "")
                                _income.incomePaper4 = Convert.ToBoolean(dr1["IncomePaper4"]);
                            if (dr1["NoincomePaper"].ToString() != "")
                                _income.noincomepaper = Convert.ToBoolean(dr1["NoincomePaper"]);
                            IncomeList.Add(_income);
                        }
                        obj.Income1 = IncomeList;
                    }
                }

                //Parent2 detail
                if (_dataset.Tables[0].Rows.Count == 2)
                {
                    obj.P1firstname = _dataset.Tables[0].Rows[1]["Pfirstname"].ToString();
                    obj.P1lastname = _dataset.Tables[0].Rows[1]["PLastName"].ToString();
                    obj.P1midddlename = _dataset.Tables[0].Rows[1]["PMiddlename"].ToString();
                    obj.P1DOB = _dataset.Tables[0].Rows[1]["PDOB"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[0].Rows[1]["PDOB"]).ToString("MM/dd/yyyy");
                    if (_dataset.Tables[0].Rows[1]["PGender"].ToString() != "")
                        obj.P1Gender = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PGender"]);
                    obj.P1emailid = _dataset.Tables[0].Rows[1]["PEmailid"].ToString();
                    if (_dataset.Tables[0].Rows[1]["PMilitaryStatus"].ToString() != "")
                        obj.P1MilitaryStatus = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMilitaryStatus"]);
                    if (_dataset.Tables[0].Rows[1]["PEnrollment"].ToString() != "")
                        obj.P1Enrollment = Convert.ToString(_dataset.Tables[0].Rows[1]["PEnrollment"]);
                    if (_dataset.Tables[0].Rows[1]["PDegreeEarned"].ToString() != "")
                        obj.P1DegreeEarned = Convert.ToString(_dataset.Tables[0].Rows[1]["PDegreeEarned"]);
                    if (_dataset.Tables[0].Rows[1]["PCurrentlyWorking"].ToString() != "")
                        obj.P1CurrentlyWorking = _dataset.Tables[0].Rows[1]["PCurrentlyWorking"].ToString();
                    obj.P1Role = _dataset.Tables[0].Rows[1]["ParentRole"].ToString();
                    obj.P1notesother = _dataset.Tables[0].Rows[1]["PNotes"].ToString();
                    if (_dataset.Tables[0].Rows[1]["ParentId"].ToString() != "")
                        obj.ParentID1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["ParentId"]);
                    if (_dataset.Tables[0].Rows[1]["IsPreg"].ToString() != "")
                        obj.P1Question = _dataset.Tables[0].Rows[1]["IsPreg"].ToString();
                    if (_dataset.Tables[0].Rows[1]["EnrollforPregnant"].ToString() != "")
                        obj.PregnantmotherenrolledP1 = Convert.ToBoolean(_dataset.Tables[0].Rows[1]["EnrollforPregnant"]);
                    if (_dataset.Tables[0].Rows[1]["motherinsurance"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurance1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["motherinsurance"]);
                    if (_dataset.Tables[0].Rows[1]["insurancenotemother"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurancenotes1 = _dataset.Tables[0].Rows[1]["insurancenotemother"].ToString();
                    if (_dataset.Tables[0].Rows[1]["TrimesterEnrolled"].ToString() != "")
                        obj.TrimesterEnrolled1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["TrimesterEnrolled"]);
                    try
                    {
                        obj.ParentSSN2 = _dataset.Tables[0].Rows[1]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[1]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.ParentSSN2 = _dataset.Tables[0].Rows[1]["SSN"].ToString();
                    }
                    if (_dataset.Tables[0].Rows[1]["Medicalhome"].ToString() != "")
                        obj.MedicalhomePreg2 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["Medicalhome"]);
                    if (_dataset.Tables[0].Rows[1]["Doctorvalue"].ToString() != "")
                        obj.P2Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[1]["Doctorvalue"]);
                    obj.CDoctorP2 = _dataset.Tables[0].Rows[1]["doctorname"].ToString();
                    if (_dataset.Tables[0].Rows[1]["NoEmail"].ToString() != "")
                        obj.Noemail2 = Convert.ToBoolean(_dataset.Tables[0].Rows[1]["NoEmail"]);
                    obj.PMVisitDoc1 = _dataset.Tables[0].Rows[1]["PMVisitDoc"].ToString();
                    if (_dataset.Tables[0].Rows[1]["PMProblmID"].ToString() != "")
                        obj.PMProblem1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMProblmID"]);
                    obj.PMOtherIssues1 = _dataset.Tables[0].Rows[1]["PMIssues"].ToString();
                    obj.PMConditions1 = _dataset.Tables[0].Rows[1]["PMConditionID"].ToString();
                    obj.PMCondtnDesc1 = _dataset.Tables[0].Rows[1]["PMConditionDescID"].ToString();
                    if (_dataset.Tables[0].Rows[1]["PMRisk"].ToString() != "")
                        obj.PMRisk1 = Convert.ToBoolean(_dataset.Tables[0].Rows[1]["PMRisk"]);
                    if (_dataset.Tables[0].Rows[1]["PMDentalExam"].ToString() != "")
                        obj.PMDentalExam1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMDentalExam"]);
                    if (_dataset.Tables[0].Rows[1]["PMDentalDate"].ToString() != "")
                        obj.PMDentalExamDate1 = Convert.ToDateTime(_dataset.Tables[0].Rows[1]["PMDentalDate"]).ToString("MM/dd/yyyy");
                    if (_dataset.Tables[0].Rows[1]["PMNeedDental"].ToString() != "")
                        obj.PMNeedDental1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMNeedDental"]);
                    if (_dataset.Tables[0].Rows[1]["PMRecieveDental"].ToString() != "")
                        obj.PMRecieveDental1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMRecieveDental"]);
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.calculateincome1> IncomeList = new List<FamilyHousehold.calculateincome1>();

                        DataRow[] dr = _dataset.Tables[1].Select("ParentId=" + Convert.ToInt32(_dataset.Tables[0].Rows[1]["ParentId"]));
                        foreach (DataRow dr1 in dr)
                        {
                            FamilyHousehold.calculateincome1 _income = new FamilyHousehold.calculateincome1();
                            _income.IncomeID = Convert.ToInt32(dr1["IncomeId"]);
                            if (dr1["Income"].ToString() != "")
                                _income.Income = Convert.ToInt32(dr1["Income"]);
                            if (dr1["IncomeSource1"].ToString() != "")
                                _income.IncomeSource1 = dr1["IncomeSource1"].ToString();
                            if (dr1["IncomeAmt1"].ToString() != "")
                                _income.AmountVocher1 = Convert.ToDecimal(dr1["IncomeAmt1"]);
                            if (dr1["IncomeAmt2"].ToString() != "")
                                _income.AmountVocher2 = Convert.ToDecimal(dr1["IncomeAmt2"]);
                            if (dr1["IncomeAmt3"].ToString() != "")
                                _income.AmountVocher3 = Convert.ToDecimal(dr1["IncomeAmt3"]);
                            if (dr1["IncomeAmt4"].ToString() != "")
                                _income.AmountVocher4 = Convert.ToDecimal(dr1["IncomeAmt4"]);
                            if (dr1["PayFrequency"].ToString() != "")
                                _income.Payfrequency = Convert.ToInt32(dr1["PayFrequency"]);
                            if (dr1["WorkingPeriod"].ToString() != "")
                                _income.Working = Convert.ToInt32(dr1["WorkingPeriod"]);
                            if (dr1["TotalIncome"].ToString() != "")
                                _income.IncomeCalculated = Convert.ToDecimal(dr1["TotalIncome"]);
                            //if (dr1["Image1"].ToString() != "")
                            //    _income.SalaryAvatar1bytes = (byte[])dr1["Image1"];
                            //if (dr1["Image2"].ToString() != "")
                            //    _income.SalaryAvatar2bytes = (byte[])dr1["Image2"];
                            //if (dr1["Image3"].ToString() != "")
                            //    _income.SalaryAvatar3bytes = (byte[])dr1["Image3"];
                            //if (dr1["Image4"].ToString() != "")
                            //    _income.SalaryAvatar1bytes = (byte[])dr1["Image4"];
                            if (dr1["Image1FileName"].ToString() != "")
                                _income.SalaryAvatarFilename1 = dr1["Image1FileName"].ToString();
                            if (dr1["Image2FileName"].ToString() != "")
                                _income.SalaryAvatarFilename2 = dr1["Image2FileName"].ToString();
                            if (dr1["Image3FileName"].ToString() != "")
                                _income.SalaryAvatarFilename3 = dr1["Image3FileName"].ToString();
                            if (dr1["Image4FileName"].ToString() != "")
                                _income.SalaryAvatarFilename4 = dr1["Image4FileName"].ToString();
                            if (dr1["Image1FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension1 = dr1["Image1FileExt"].ToString();
                            if (dr1["Image2FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension2 = dr1["Image2FileExt"].ToString();
                            if (dr1["Image3FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension3 = dr1["Image3FileExt"].ToString();
                            if (dr1["Image4FileExt"].ToString() != "")
                                _income.SalaryAvatarFileExtension4 = dr1["Image4FileExt"].ToString();
                            //if (dr1["NoIncomeImage"].ToString() != "")
                            //    _income.NoIncomeAvatarbytes = (byte[])dr1["NoIncomeImage"];
                            if (dr1["NoIncomeImageFileName"].ToString() != "")
                                _income.NoIncomeFilename4 = dr1["NoIncomeImageFileName"].ToString();
                            if (dr1["NoIncomeImageFileExt"].ToString() != "")
                                _income.NoIncomeFileExtension4 = dr1["NoIncomeImageFileExt"].ToString();
                            if (dr1["IncomePaper1"].ToString() != "")
                                _income.incomePaper1 = Convert.ToBoolean(dr1["IncomePaper1"]);
                            if (dr1["IncomePaper2"].ToString() != "")
                                _income.incomePaper2 = Convert.ToBoolean(dr1["IncomePaper2"]);
                            if (dr1["IncomePaper3"].ToString() != "")
                                _income.incomePaper3 = Convert.ToBoolean(dr1["IncomePaper3"]);
                            if (dr1["IncomePaper4"].ToString() != "")
                                _income.incomePaper4 = Convert.ToBoolean(dr1["IncomePaper4"]);
                            if (dr1["NoincomePaper"].ToString() != "")
                                _income.noincomepaper = Convert.ToBoolean(dr1["NoincomePaper"]);

                            IncomeList.Add(_income);
                        }
                        obj.Income2 = IncomeList;

                    }
                }
                if (_dataset.Tables[2].Rows.Count > 0)
                {
                    List<FamilyHousehold.Qualifier> Qualifier = new List<FamilyHousehold.Qualifier>();
                    FamilyHousehold.Qualifier _Qualifier = null;
                    foreach (DataRow dr in _dataset.Tables[2].Rows)
                    {
                        _Qualifier = new FamilyHousehold.Qualifier();
                        _Qualifier.Name = dr["name"].ToString();
                        _Qualifier.Programtype = dr["ProgramType"].ToString();
                        _Qualifier.Dob = dr["DOB"].ToString() == "" ? "" : Convert.ToDateTime(dr["DOB"]).ToString("MM/dd/yyyy");
                        _Qualifier.clientid = Convert.ToInt32(dr["clientid"]);
                        _Qualifier.Programid = Convert.ToInt32(dr["programid"]);
                        _Qualifier.PovertyPercentage = dr["PovertyPercentage"].ToString();
                        _Qualifier.Selectionpoint = dr["Selectionpoint"].ToString();
                        _Qualifier.ClientType = dr["Clienttype"].ToString();
                        obj.totalhousehold = Convert.ToInt32(dr["totalhousehold"]);
                        _Qualifier.IsAccepted = dr["IsAccepted"].ToString() == "" ? false : Convert.ToBoolean(dr["IsAccepted"]);
                        _Qualifier.Iswaiting = dr["iswaiting"].ToString() == "" ? false : Convert.ToBoolean(dr["iswaiting"]);
                        _Qualifier.ApplicationStatus = Convert.ToInt32(dr["ApplicationStatus"]);
                        _Qualifier.HealthReview = Convert.ToInt32(dr["HealthReview"]);
                        _Qualifier.HealthReviewAllowed = Convert.ToInt32(dr["Healthreviewallowed"]);
                        _Qualifier.HealthReviewPm = dr["HealthReviewPm"].ToString() == "" ? false : Convert.ToBoolean(dr["HealthReviewPm"]);
                        Qualifier.Add(_Qualifier);
                        _Qualifier = null;
                    }
                    obj.QualifierRecords = Qualifier;
                }
                if (_dataset.Tables[3] != null && _dataset.Tables[3].Rows.Count > 0)
                {
                    List<HrCenterInfo> centerList = new List<HrCenterInfo>();
                    foreach (DataRow dr in _dataset.Tables[3].Rows)
                    {
                        HrCenterInfo info = new HrCenterInfo();
                        info.CenterId = dr["center"].ToString();
                        info.Name = dr["centername"].ToString();
                        info.Address = dr["address"].ToString();
                        info.Zip = dr["Zip"].ToString();
                        info.SeatsAvailable = dr["AvailSeats"].ToString();
                        centerList.Add(info);
                    }
                    obj.HrcenterList = centerList;
                }
                if (_dataset.Tables[5] != null && _dataset.Tables[5].Rows.Count > 0)
                {
                    List<FamilyHousehold.PMproblemandservices> _PMproblemandservicesList = new List<FamilyHousehold.PMproblemandservices>();
                    FamilyHousehold.PMproblemandservices info = null;
                    foreach (DataRow dr in _dataset.Tables[5].Rows)
                    {
                        info = new FamilyHousehold.PMproblemandservices();
                        info.Id = dr["ID"].ToString();
                        info.MasterId = dr["PMPrblmID"].ToString();
                        info.Description = dr["PmDecription"].ToString();
                        info.Parentid = dr["ParentID"].ToString();
                        _PMproblemandservicesList.Add(info);
                    }
                    obj._PMproblem = _PMproblemandservicesList;
                }
                if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
                {
                    List<FamilyHousehold.PMproblemandservices> _PMproblemandservicesList = new List<FamilyHousehold.PMproblemandservices>();
                    FamilyHousehold.PMproblemandservices info = null;
                    foreach (DataRow dr in _dataset.Tables[6].Rows)
                    {
                        info = new FamilyHousehold.PMproblemandservices();
                        info.Id = dr["ID"].ToString();
                        info.MasterId = dr["PMServiceID"].ToString();
                        info.Description = dr["PMDescription"].ToString();
                        info.Parentid = dr["ParentID"].ToString();
                        _PMproblemandservicesList.Add(info);
                    }
                    obj._PMservices = _PMproblemandservicesList;
                }
                //26082016  child list
                if (_dataset.Tables[7] != null && _dataset.Tables[7].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Childlist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[7].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.Cfirstname = Convert.ToString(_dataset.Tables[7].Rows[i]["Firstname"]);
                        familyinfo.Clastname = Convert.ToString(_dataset.Tables[7].Rows[i]["Lastname"]);
                        familyinfo.Cmiddlename = Convert.ToString(_dataset.Tables[7].Rows[i]["Middlename"]);
                        familyinfo.CDOB = Convert.ToDateTime(_dataset.Tables[7].Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.CRace = Convert.ToString(_dataset.Tables[7].Rows[i]["Name"]);
                        if (_dataset.Tables[7].Rows[i]["DentalHome"].ToString() != "")
                            familyinfo.CDentalhome = Convert.ToInt32(_dataset.Tables[7].Rows[i]["DentalHome"]);
                        familyinfo.ChildId = Convert.ToInt32(_dataset.Tables[7].Rows[i]["ID"]);
                        familyinfo.CreatedOn = Convert.ToDateTime(_dataset.Tables[7].Rows[i]["DateEntered"]).ToString("MM/dd/yyyy");
                        familyinfo.Imagejson = _dataset.Tables[7].Rows[i]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[7].Rows[i]["ProfilePic"]);
                        if ((Convert.ToString(_dataset.Tables[7].Rows[i]["Gender"]) == "1"))
                        {
                            familyinfo.CGender = "Male";
                        }
                        if ((Convert.ToString(_dataset.Tables[7].Rows[i]["Gender"]) == "2"))
                        {
                            familyinfo.CGender = "Female";
                        }
                        if ((Convert.ToString(_dataset.Tables[7].Rows[i]["Gender"]) == "3"))
                        {
                            familyinfo.CGender = "Other";
                        }
                        _Childlist.Add(familyinfo);
                    }
                    obj._Clist = _Childlist;
                }
                //EmergencyList
                if (_dataset.Tables[8] != null && _dataset.Tables[8].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Elist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[8].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.EmegencyId = Convert.ToInt32(_dataset.Tables[8].Rows[i]["ID"]);
                        familyinfo.Efirstname = Convert.ToString(_dataset.Tables[8].Rows[i]["Name"]);
                        familyinfo.EDOB = _dataset.Tables[8].Rows[i]["DOB"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[8].Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.ERelationwithchild = Convert.ToString(_dataset.Tables[8].Rows[i]["RelationName"]);
                        familyinfo.EImagejson = _dataset.Tables[8].Rows[i]["DocumentFile"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[8].Rows[i]["DocumentFile"]);
                        _Elist.Add(familyinfo);
                    }
                    obj._Elist = _Elist;
                }
                //restrcited list
                if (_dataset.Tables[9] != null && _dataset.Tables[9].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Rlist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[9].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.RestrictedId = Convert.ToInt32(_dataset.Tables[9].Rows[i]["ID"]);
                        familyinfo.Rfirstname = Convert.ToString(_dataset.Tables[9].Rows[i]["Firstname"]);
                        familyinfo.Rlastname = Convert.ToString(_dataset.Tables[9].Rows[i]["Lastname"]);
                        familyinfo.RDescription = Convert.ToString(_dataset.Tables[9].Rows[i]["Notes"]);
                        familyinfo.RImagejson = _dataset.Tables[9].Rows[i]["FileAttachment"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[9].Rows[i]["FileAttachment"]);
                        _Rlist.Add(familyinfo);
                    }
                    obj._Rlist = _Rlist;
                }
                //others list
                if (_dataset.Tables[10] != null && _dataset.Tables[10].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Olist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[10].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.Ofirstname = Convert.ToString(_dataset.Tables[10].Rows[i]["Name"]);

                        familyinfo.ODOB = Convert.ToDateTime(_dataset.Tables[10].Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.OthersId = Convert.ToInt32(_dataset.Tables[10].Rows[i]["ID"]);
                        familyinfo.CSSN = _dataset.Tables[10].Rows[i]["ssn"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[10].Rows[i]["ssn"].ToString());
                        familyinfo.HouseHoldImagejson = _dataset.Tables[10].Rows[i]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[10].Rows[i]["ProfilePic"]);


                        if ((Convert.ToString(_dataset.Tables[10].Rows[i]["Gender"]) == "1"))
                        {
                            familyinfo.OGender = "Male";
                        }
                        else if ((Convert.ToString(_dataset.Tables[10].Rows[i]["Gender"]) == "2"))
                        {
                            familyinfo.OGender = "Female";
                        }
                        else
                        {
                            familyinfo.OGender = "Other";
                        }
                        _Olist.Add(familyinfo);
                    }
                    obj._Olist = _Olist;
                }
                //Application notes list
                if (_dataset.Tables[11] != null && _dataset.Tables[11].Rows.Count > 0)
                {
                    List<FamilyHousehold.Applicationnotes> _Nlist = new List<FamilyHousehold.Applicationnotes>();
                    FamilyHousehold.Applicationnotes _Applicationnotes = null;
                    foreach (DataRow dr in _dataset.Tables[11].Rows)
                    {
                        _Applicationnotes = new FamilyHousehold.Applicationnotes();
                        _Applicationnotes.Name = dr["name"].ToString();
                        _Applicationnotes.CreatedOn = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                        _Applicationnotes.notes = dr["Notes"].ToString();
                        _Nlist.Add(_Applicationnotes);
                    }
                    obj._Nlist = _Nlist;
                }
                //Parent1 phone list
                if (_dataset.Tables[12] != null && _dataset.Tables[12].Rows.Count > 0)
                {
                    List<FamilyHousehold.Parentphone1> _Phonelist = new List<FamilyHousehold.Parentphone1>();
                    FamilyHousehold.Parentphone1 _phoneadd = null;
                    foreach (DataRow row in _dataset.Tables[12].Rows)
                    {
                        _phoneadd = new FamilyHousehold.Parentphone1();
                        _phoneadd.PPhoneId = Convert.ToInt32(row["Id"]);
                        _phoneadd.PhoneTypeP = row["PhoneType"].ToString();
                        _phoneadd.phonenoP = row["Phoneno"].ToString();
                        _phoneadd.StateP = row["IsPrimaryContact"].ToString() == "" ? false : Convert.ToBoolean(row["IsPrimaryContact"]);
                        _phoneadd.SmsP = row["Sms"].ToString() == "" ? false : Convert.ToBoolean(row["Sms"]);
                        _phoneadd.notesP = row["Notes"].ToString();
                        _Phonelist.Add(_phoneadd);
                    }
                    obj.phoneListParent = _Phonelist;
                }
                //Parent2 phone list
                if (_dataset.Tables[13] != null && _dataset.Tables[13].Rows.Count > 0)
                {
                    List<FamilyHousehold.Parentphone2> _Phonelist = new List<FamilyHousehold.Parentphone2>();
                    FamilyHousehold.Parentphone2 _phoneadd = null;
                    foreach (DataRow row in _dataset.Tables[13].Rows)
                    {
                        _phoneadd = new FamilyHousehold.Parentphone2();
                        _phoneadd.PPhoneId1 = Convert.ToInt32(row["Id"]);
                        _phoneadd.PhoneTypeP1 = row["PhoneType"].ToString();
                        _phoneadd.phonenoP1 = row["Phoneno"].ToString();
                        _phoneadd.StateP1 = row["IsPrimaryContact"].ToString() == "" ? false : Convert.ToBoolean(row["IsPrimaryContact"]);
                        _phoneadd.SmsP1 = row["Sms"].ToString() == "" ? false : Convert.ToBoolean(row["Sms"]);
                        _phoneadd.notesP1 = row["Notes"].ToString();
                        _Phonelist.Add(_phoneadd);
                    }
                    obj.phoneListParent1 = _Phonelist;
                }

                if (_dataset.Tables[14] != null && _dataset.Tables[14].Rows.Count > 0)
                {
                    obj.customscreening = _dataset.Tables[14];

                }
                if (_dataset.Tables[15] != null && _dataset.Tables[15].Rows.Count > 0)
                {
                    List<FamilyHousehold.WorkshopDetails> _workshopinfo = new List<FamilyHousehold.WorkshopDetails>();
                    FamilyHousehold.WorkshopDetails WorkshopDetails = null;
                    foreach (DataRow dr in _dataset.Tables[15].Rows)
                    {
                        WorkshopDetails = new FamilyHousehold.WorkshopDetails();
                        WorkshopDetails.Id = Convert.ToInt32(dr["WorkshopId"]);
                        WorkshopDetails.Name = dr["WorkshopName"].ToString();
                        WorkshopDetails.IsSelected = true;
                        obj.timepreference = dr["timepreference"] != DBNull.Value ? Convert.ToInt32(dr["timepreference"].ToString()) : 0;  //by atul 28-3-2017
                        obj.daypreference = dr["daypreference"] != DBNull.Value ? Convert.ToBoolean(dr["daypreference"].ToString()) : false; //by atul 28-3-2017
                        _workshopinfo.Add(WorkshopDetails);
                    }
                    obj.AvailableWorkshopDetails = _workshopinfo;

                }







            }


        }
        public List<FamilyHousehold> childDetails(string Householdid, string agencyid)
        {
            List<FamilyHousehold> _familyinfo = new List<FamilyHousehold>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Householdid", Householdid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Sel_childcodelist";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < familydataTable.Rows.Count; i++)
                    {
                        FamilyHousehold familyinfo = new FamilyHousehold();
                        familyinfo.Cfirstname = Convert.ToString(familydataTable.Rows[i]["Firstname"]);
                        familyinfo.Clastname = Convert.ToString(familydataTable.Rows[i]["Lastname"]);
                        familyinfo.Cmiddlename = Convert.ToString(familydataTable.Rows[i]["Middlename"]);
                        familyinfo.CDOB = Convert.ToDateTime(familydataTable.Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.CRace = Convert.ToString(familydataTable.Rows[i]["Name"]);
                        if (familydataTable.Rows[i]["DentalHome"].ToString() != "")
                            familyinfo.CDentalhome = Convert.ToInt32(familydataTable.Rows[i]["DentalHome"]);
                        familyinfo.ChildId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                        familyinfo.CreatedOn = Convert.ToDateTime(familydataTable.Rows[i]["DateEntered"]).ToString("MM/dd/yyyy");
                        familyinfo.Imagejson = familydataTable.Rows[i]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])familydataTable.Rows[i]["ProfilePic"]);
                        if ((Convert.ToString(familydataTable.Rows[i]["Gender"]) == "1"))
                        {
                            familyinfo.CGender = "Male";
                        }
                        if ((Convert.ToString(familydataTable.Rows[i]["Gender"]) == "2"))
                        {
                            familyinfo.CGender = "Female";
                        }
                        if ((Convert.ToString(familydataTable.Rows[i]["Gender"]) == "3"))
                        {
                            familyinfo.CGender = "Other";
                        }
                        _familyinfo.Add(familyinfo);
                    }
                }
                return _familyinfo;
            }
            catch (Exception ex)
            {
                //  totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _familyinfo;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public List<FamilyHousehold> EmergencyContactDetails(string householdid, string agencyid)
        {
            List<FamilyHousehold> _familyinfo = new List<FamilyHousehold>();
            try
            {
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Sel_emergencycodelist";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < familydataTable.Rows.Count; i++)
                    {
                        FamilyHousehold familyinfo = new FamilyHousehold();
                        familyinfo.EmegencyId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                        familyinfo.Efirstname = Convert.ToString(familydataTable.Rows[i]["Name"]);
                        familyinfo.EDOB = familydataTable.Rows[i]["DOB"].ToString() == "" ? "" : Convert.ToDateTime(familydataTable.Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.ERelationwithchild = Convert.ToString(familydataTable.Rows[i]["RelationName"]);
                        familyinfo.EImagejson = familydataTable.Rows[i]["DocumentFile"].ToString() == "" ? "" : Convert.ToBase64String((byte[])familydataTable.Rows[i]["DocumentFile"]);

                        _familyinfo.Add(familyinfo);
                    }
                    //totalrecord = command.Parameters["@totalRecord"].Value.ToString();
                }
                return _familyinfo;
            }
            catch (Exception ex)
            {
                //  totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _familyinfo;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public List<FamilyHousehold> RestrictedDetails(string Householdid, string agencyid)
        {
            List<FamilyHousehold> _familyinfo = new List<FamilyHousehold>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Householdid", Householdid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Sel_restrictedlist";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < familydataTable.Rows.Count; i++)
                    {
                        FamilyHousehold familyinfo = new FamilyHousehold();
                        familyinfo.RestrictedId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                        familyinfo.Rfirstname = Convert.ToString(familydataTable.Rows[i]["Firstname"]);
                        familyinfo.Rlastname = Convert.ToString(familydataTable.Rows[i]["Lastname"]);
                        familyinfo.RDescription = Convert.ToString(familydataTable.Rows[i]["Notes"]);
                        familyinfo.RImagejson = familydataTable.Rows[i]["FileAttachment"].ToString() == "" ? "" : Convert.ToBase64String((byte[])familydataTable.Rows[i]["FileAttachment"]);
                        _familyinfo.Add(familyinfo);
                    }
                    //totalrecord = command.Parameters["@totalRecord"].Value.ToString();
                }
                return _familyinfo;
            }
            catch (Exception ex)
            {
                //  totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _familyinfo;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public FamilyHousehold GetRestricted(string RestrictedId, string HouseHoldId, string agencyid)//string RestrictedId,
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@RestrictedId", RestrictedId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_restrictedinfo";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable != null && familydataTable.Rows.Count > 0)
                {
                    obj.RestrictedId = Convert.ToInt32(familydataTable.Rows[0]["ID"]);
                    obj.Rfirstname = familydataTable.Rows[0]["Firstname"].ToString();
                    obj.Rlastname = familydataTable.Rows[0]["Lastname"].ToString();
                    obj.RDescription = familydataTable.Rows[0]["Notes"].ToString();
                }
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public string DeleteRestricted(string RestrictedId, string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleterestrictedinfo";
                command.Parameters.Add(new SqlParameter("@RestrictedId", RestrictedId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public List<FamilyHousehold> getHouseholdList(out string totalrecord, out string IncompleteApplication, string sortOrder, string sortDirection, string search, string search1, int skip, int pageSize, string userid, int centerid, int option, int housholdid, bool Applicationstatus)
        {
            List<FamilyHousehold> _householdlist = new List<FamilyHousehold>();
            try
            {
                totalrecord = string.Empty;
                IncompleteApplication = string.Empty;
                string searchAgency = string.Empty;
                if (string.IsNullOrEmpty(search.Trim()))
                    searchAgency = string.Empty;
                else
                    searchAgency = search;

                string searchAgency1 = string.Empty;
                if (string.IsNullOrEmpty(search1.Trim()))
                    searchAgency1 = string.Empty;
                else
                    searchAgency1 = search1;
                command.Parameters.Add(new SqlParameter("@Search", searchAgency));
                command.Parameters.Add(new SqlParameter("@Search1", searchAgency1));
                command.Parameters.Add(new SqlParameter("@take", pageSize));
                command.Parameters.Add(new SqlParameter("@skip", skip));
                command.Parameters.Add(new SqlParameter("@sortcolumn", sortOrder));
                command.Parameters.Add(new SqlParameter("@sortorder", sortDirection));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Parameters.Add(new SqlParameter("@option", option));
                command.Parameters.Add(new SqlParameter("@housholdid", housholdid));
                command.Parameters.Add(new SqlParameter("@Applicationstatus", Applicationstatus));
                command.Parameters.Add(new SqlParameter("@totalRecord", 0)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@IncompleteApplication", 0)).Direction = ParameterDirection.Output;
                command.Parameters["@IncompleteApplication"].Size = 10;
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_household_list";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < familydataTable.Rows.Count; i++)
                    {
                        FamilyHousehold addhouseholdRow = new FamilyHousehold();
                        addhouseholdRow.HouseholdId = Convert.ToInt32(familydataTable.Rows[i]["HouseholdID"]);
                        addhouseholdRow.Street = familydataTable.Rows[i]["Street"].ToString();
                        addhouseholdRow.clientIdnew = EncryptDecrypt.Encrypt64(familydataTable.Rows[i]["ClientId"].ToString());
                        addhouseholdRow.ClientFname = familydataTable.Rows[i]["name"].ToString();
                        addhouseholdRow.RPhoneno = familydataTable.Rows[i]["PHONENO"].ToString();
                        addhouseholdRow.CreatedOn = Convert.ToDateTime(familydataTable.Rows[i]["DateEntered"]).ToString("MM/dd/yyyy");
                        addhouseholdRow.Encrypthouseholid = EncryptDecrypt.Encrypt64(familydataTable.Rows[i]["HouseholdID"].ToString());
                        addhouseholdRow.ApplicationStatusChild = familydataTable.Rows[i]["ApplicationStatus"].ToString();
                        _householdlist.Add(addhouseholdRow);
                    }
                    totalrecord = command.Parameters["@totalRecord"].Value.ToString();
                    IncompleteApplication = command.Parameters["@IncompleteApplication"].Value.ToString();
                }
                return _householdlist;
            }
            catch (Exception ex)
            {
                totalrecord = string.Empty;
                IncompleteApplication = string.Empty;
                clsError.WriteException(ex);
                return _householdlist;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public FamilyHousehold EditFamilyInfo(string id, string Agencyid, string userid, string Roleid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@Roleid", Roleid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_gethouseholdinfo";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        //Changes by Akansha on 19Dec2016
        public FamilyHousehold Getchild(string ChildId, string HouseHoldId, string agencyid, string serverpath, string roleid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ChildId", ChildId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_chidinfo";//SP_chidinfo   
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        obj.ChildId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                        obj.Cfirstname = _dataset.Tables[0].Rows[0]["Firstname"].ToString();
                        obj.Cmiddlename = _dataset.Tables[0].Rows[0]["Middlename"].ToString();
                        obj.Clastname = _dataset.Tables[0].Rows[0]["Lastname"].ToString();
                        if (_dataset.Tables[0].Rows[0]["DOB"].ToString() != "")
                            obj.CDOB = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                        if (_dataset.Tables[0].Rows[0]["DateOfEnrollment"].ToString() != "")
                            obj.DateOfEnrollment = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["DateOfEnrollment"]).ToString("MM/dd/yyyy");

                        obj.DOBverifiedBy = _dataset.Tables[0].Rows[0]["Dobverifiedby"].ToString();
                        try
                        {
                            obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[0]["SSN"].ToString());
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                            obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString();
                        }

                        obj.CProgramType = _dataset.Tables[0].Rows[0]["Programtype"].ToString();
                        obj.CGender = _dataset.Tables[0].Rows[0]["Gender"].ToString();
                        obj.CRace = _dataset.Tables[0].Rows[0]["RaceID"].ToString();
                        obj.CRaceSubCategory = _dataset.Tables[0].Rows[0]["RaceSubCategoryID"].ToString();
                        if (_dataset.Tables[0].Rows[0]["ImmunizationServiceType"].ToString() != "")
                            obj.ImmunizationService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ImmunizationServiceType"]);
                        if (_dataset.Tables[0].Rows[0]["MedicalService"].ToString() != "")
                            obj.MedicalService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["MedicalService"]);
                        if (_dataset.Tables[0].Rows[0]["Medicalhome"].ToString() != "")
                            obj.Medicalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Medicalhome"]);
                        if (_dataset.Tables[0].Rows[0]["ParentDisable"].ToString() != "")
                            obj.CParentdisable = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentDisable"]);
                        //Added by Santosh For IsIEP IsFSP
                        if (_dataset.Tables[0].Rows[0]["IEP"].ToString() != "")
                            obj.IsIEP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IEP"].ToString());
                        if (_dataset.Tables[0].Rows[0]["IFSP"].ToString() != "")
                            obj.IsIFSP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IFSP"].ToString());
                        if (_dataset.Tables[0].Rows[0]["IsExpired"].ToString() != "")
                            obj.IsExpired = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IsExpired"].ToString());
                        //
                        if (_dataset.Tables[0].Rows[0]["BMIStatus"].ToString() != "")
                            obj.BMIStatus = Convert.ToInt32(_dataset.Tables[0].Rows[0]["BMIStatus"]);
                        if (_dataset.Tables[0].Rows[0]["DentalHome"].ToString() != "")
                            obj.CDentalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DentalHome"]);
                        if (_dataset.Tables[0].Rows[0]["Ethnicity"].ToString() != "")
                            obj.CEthnicity = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Ethnicity"]);
                        obj.CFileName = _dataset.Tables[0].Rows[0]["FileNameul"].ToString();
                        obj.CFileExtension = _dataset.Tables[0].Rows[0]["FileExtension"].ToString();
                        obj.Imagejson = _dataset.Tables[0].Rows[0]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[0].Rows[0]["ProfilePic"]);
                        obj.DobFileName = _dataset.Tables[0].Rows[0]["Dobfilename"].ToString();
                        obj.FosterFileName = _dataset.Tables[0].Rows[0]["FosterFileName"].ToString();
                        obj.CDoctor = _dataset.Tables[0].Rows[0]["doctorname"].ToString();
                        obj.CDentist = _dataset.Tables[0].Rows[0]["dentistname"].ToString();
                        if (_dataset.Tables[0].Rows[0]["Doctorvalue"].ToString() != "")
                            obj.Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Doctorvalue"]);
                        if (_dataset.Tables[0].Rows[0]["Dentistvalue"].ToString() != "")
                            obj.Dentist = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Dentistvalue"]);
                        if (_dataset.Tables[0].Rows[0]["SchoolDistrict"].ToString() != "")
                            obj.SchoolDistrict = Convert.ToInt32(_dataset.Tables[0].Rows[0]["SchoolDistrict"]);
                        if (_dataset.Tables[0].Rows[0]["DobPaper"].ToString() != "")
                            obj.DobverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["DobPaper"]);
                        if (_dataset.Tables[0].Rows[0]["FosterChild"].ToString() != "")
                            obj.IsFoster = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FosterChild"]);
                        if (_dataset.Tables[0].Rows[0]["WelfareAgency"].ToString() != "")
                            obj.Inwalfareagency = Convert.ToInt32(_dataset.Tables[0].Rows[0]["WelfareAgency"]);
                        if (_dataset.Tables[0].Rows[0]["DualCustodyChild"].ToString() != "")
                            obj.InDualcustody = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DualCustodyChild"]);
                        if (_dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString() != "")
                            obj.InsuranceOption = _dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString();
                        if (_dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString() != "")
                            obj.MedicalNote = _dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString();
                        if (_dataset.Tables[0].Rows[0]["ImmunizationinPaper"].ToString() != "")
                            obj.ImmunizationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ImmunizationinPaper"]);
                        obj.ImmunizationFileName = _dataset.Tables[0].Rows[0]["ImmunizationFileName"].ToString();
                        obj.Raceother = _dataset.Tables[0].Rows[0]["OtherRace"].ToString();
                        //ChildTransport

                        obj.CTransport = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ChildTransport"].ToString());

                        if (!string.IsNullOrEmpty((_dataset.Tables[0].Rows[0]["TransportNeeded"]).ToString()))
                        {
                            obj.CTransportNeeded = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TransportNeeded"]);
                        }
                        else
                        {
                            obj.CTransportNeeded = false;
                        }

                        //End
                        //Ehs Health question
                        obj.EhsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                        obj.EhsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                        obj.EhsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                        obj.EhsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                        obj.EhsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                        obj.EhsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();
                        obj.EHSmpplan = _dataset.Tables[0].Rows[0]["M_PCarePlan"].ToString();
                        obj.EHSmpplancomment = _dataset.Tables[0].Rows[0]["M_PCarePlanComment"].ToString();
                        obj.EHSAllergy = _dataset.Tables[0].Rows[0]["EHSAllergy"].ToString();
                        obj.EHSEpiPen = Convert.ToInt32(_dataset.Tables[0].Rows[0]["EHSEpiPen"]);



                        //HS Health question
                        obj.HsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                        obj.HsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                        obj.HsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                        obj.HsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                        obj.HsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                        obj.HSmpplan = _dataset.Tables[0].Rows[0]["M_PCarePlan"].ToString();
                        obj.HSmpplanComment = _dataset.Tables[0].Rows[0]["M_PCarePlanComment"].ToString();
                        obj.HsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();
                        obj.HsChildDentalCare = _dataset.Tables[0].Rows[0]["DentalCare"].ToString();
                        obj.HsDentalExam = _dataset.Tables[0].Rows[0]["CurrentDentalexam"].ToString();
                        obj.HsRecentDentalExam = _dataset.Tables[0].Rows[0]["RecentDentalExam"].ToString();
                        obj.HsChildNeedDentalTreatment = _dataset.Tables[0].Rows[0]["NeedDentalTreatment"].ToString();
                        obj.HsChildRecievedDentalTreatment = _dataset.Tables[0].Rows[0]["RecievedDentalTreatment"].ToString();
                        obj.ChildProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ChildEverHadProfExam"].ToString();

                        //Nutrition Question without HS/EHS

                        //obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        //obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        //obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        //obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        //obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        //obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        //obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //    obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //    obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        //obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        //obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        //obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        //obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        //obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        //obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        //obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        //obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        //obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        //obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        //obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        //obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        //obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        //obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        //obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        //obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        //obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        //obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        //if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                        //    obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        //if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                        //    obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        //if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                        //    obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        //if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                        //    obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        //if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                        //    obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        //obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        //obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        //obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        //obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        //obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        //obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        //obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        //obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        //obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        //obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        //obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        //obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        //obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        //obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        //obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();

                        //obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        //obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        //obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        //obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                        //obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                        //obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        //obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                        //obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                        //obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                        //obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                        //obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                        //


                        //Nutrition question
                        //Changes on 19Dec2016
                        if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "1")
                        {
                            obj.EhsRestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                            obj.EhsChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                            obj.EhsPersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                            obj.EhsPersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                            obj.EhsPersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                            obj.EhsDramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                            obj.EhsRecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                            obj.EhsChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                            obj.EhsFoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                            obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.EhsRecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                            //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                            //    obj.EhsWICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                            //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                            //    obj.EhsFoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                            //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                            //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                            obj.EhsFoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                            obj.EhschildTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                            //obj.EhsChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                            obj.Ehsspoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                            obj.Ehsfeedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                            obj.EhschildThin = Convert.ToInt32(_dataset.Tables[0].Rows[0]["childhealth"].ToString());
                            obj.EhsTakebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                            obj.Ehschewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                            obj.EhsChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                            obj.EhsChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                            obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                            obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                            obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                            obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                            obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                            obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                            obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                            obj.EhsChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                            obj.EhsChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                            if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                                obj.EhsBreakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                            if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                                obj.EhsLunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                            if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                                obj.EhsSnack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                            if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                                obj.EhsDinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                            if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                                obj.EhsNA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                            // obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                            obj.EhsNauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                            obj.EhsDiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                            obj.EhsConstipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                            obj.EhsDramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                            obj.EhsRecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                            obj.EhsRecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                            obj.EhsSpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                            obj.EhsFoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                            obj.EhsNutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                            obj.EhsChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                            obj.EhsSpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                            obj.EhsSpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                            obj.EhsBottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                            obj.EhsEatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        }
                        else if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "2")
                        {
                            obj.RestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                            obj.ChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                            obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                            obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                            obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                            obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                            obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                            obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                            obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                            obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                            if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                                obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                            if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                                obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                            if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                                obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                            obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                            obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                            obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                            obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                            obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                            obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                            obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                            obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                            obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                            obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                            obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                            obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                            obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                            obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                            obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                            obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                            obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                            obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                            obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                            if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                                obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                            if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                                obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                            if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                                obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                            if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                                obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                            if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                                obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                            obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                            obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                            obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                            obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                            obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                            obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                            obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                            obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                            obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                            obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                            obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                            obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                            obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                            obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                            obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        }
                        else
                        {

                            obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                            obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                            obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                            obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                            obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                            obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                            obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                            obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                            obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                            if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                                obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                            if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                                obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                            if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                                obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                            obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                            obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                            obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                            obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                            obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                            obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                            obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                            obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                            obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                            obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                            obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                            obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                            obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                            obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                            obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                            obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                            obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                            obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                            obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                            if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                                obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                            if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                                obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                            if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                                obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                            if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                                obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                            if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                                obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                            obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                            obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                            obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                            obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                            obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                            obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                            obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                            obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                            obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                            obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                            obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                            obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                            obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                            obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                            obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        }


                        //End



                        obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                        obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                        obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                        obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                        obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                        obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                        obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                        obj.HWInput = _dataset.Tables[0].Rows[0]["HWInput"].ToString();
                        obj.AssessmentDate = _dataset.Tables[0].Rows[0]["AssessmentDate"].ToString() != "" ? Convert.ToDateTime(_dataset.Tables[0].Rows[0]["AssessmentDate"]).ToString("MM/dd/yyyy") : "";
                        obj.AHeight = _dataset.Tables[0].Rows[0]["BHeight"].ToString();
                        obj.AWeight = _dataset.Tables[0].Rows[0]["BWeight"].ToString();
                        obj.HeadCircle = _dataset.Tables[0].Rows[0]["HeadCirc"].ToString();



                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                        FamilyHousehold.ImmunizationRecord obj1;
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            obj1 = new FamilyHousehold.ImmunizationRecord();
                            obj1.ImmunizationId = Convert.ToInt32(dr["Immunization_ID"]);
                            obj1.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationmasterid"]);
                            obj1.Dose = dr["Dose"].ToString();
                            if (dr["Dose1"].ToString() != "")
                                obj1.Dose1 = Convert.ToDateTime(dr["Dose1"]).ToString("MM/dd/yyyy");
                            else
                                obj1.Dose1 = dr["Dose1"].ToString();
                            if (dr["Dose2"].ToString() != "")
                                obj1.Dose2 = Convert.ToDateTime(dr["Dose2"]).ToString("MM/dd/yyyy");
                            else
                                obj1.Dose2 = dr["Dose2"].ToString();
                            if (dr["Dose3"].ToString() != "")
                                obj1.Dose3 = Convert.ToDateTime(dr["Dose3"]).ToString("MM/dd/yyyy");
                            else
                                obj1.Dose3 = dr["Dose3"].ToString();
                            if (dr["Dose4"].ToString() != "")
                                obj1.Dose4 = Convert.ToDateTime(dr["Dose4"]).ToString("MM/dd/yyyy");
                            else
                                obj1.Dose4 = dr["Dose4"].ToString();
                            if (dr["Dose5"].ToString() != "")
                                obj1.Dose5 = Convert.ToDateTime(dr["Dose5"]).ToString("MM/dd/yyyy");
                            else
                                obj1.Dose5 = dr["Dose5"].ToString();
                            if (dr["Exempt1"].ToString() != "")
                                obj1.Exempt1 = Convert.ToBoolean(dr["Exempt1"]);
                            if (dr["Exempt2"].ToString() != "")
                                obj1.Exempt2 = Convert.ToBoolean(dr["Exempt2"]);
                            if (dr["Exempt3"].ToString() != "")
                                obj1.Exempt3 = Convert.ToBoolean(dr["Exempt3"]);
                            if (dr["Exempt4"].ToString() != "")
                                obj1.Exempt4 = Convert.ToBoolean(dr["Exempt4"]);
                            if (dr["Exempt5"].ToString() != "")
                                obj1.Exempt5 = Convert.ToBoolean(dr["Exempt5"]);
                            if (dr["Preemptive1"].ToString() != "")
                                obj1.Preempt1 = Convert.ToBoolean(dr["Preemptive1"]);
                            if (dr["Preemptive2"].ToString() != "")
                                obj1.Preempt2 = Convert.ToBoolean(dr["Preemptive2"]);
                            if (dr["Preemptive3"].ToString() != "")
                                obj1.Preempt3 = Convert.ToBoolean(dr["Preemptive3"]);
                            if (dr["Preemptive4"].ToString() != "")
                                obj1.Preempt4 = Convert.ToBoolean(dr["Preemptive4"]);
                            if (dr["Preemptive5"].ToString() != "")
                                obj1.Preempt5 = Convert.ToBoolean(dr["Preemptive5"]);
                            ImmunizationRecords.Add(obj1);
                            obj1 = null;
                        }
                        obj.ImmunizationRecords = ImmunizationRecords;
                    }
                    if (_dataset.Tables[2].Rows.Count > 0)
                    {
                        List<FamilyHousehold.Programdetail> ProgramdetailRecords = new List<FamilyHousehold.Programdetail>();
                        FamilyHousehold.Programdetail obj1;
                        foreach (DataRow dr in _dataset.Tables[2].Rows)
                        {
                            obj1 = new FamilyHousehold.Programdetail();
                            obj1.Id = Convert.ToInt32(dr["programid"]);
                            obj1.ReferenceId = dr["ReferenceId"].ToString();
                            ProgramdetailRecords.Add(obj1);
                        }
                        obj.AvailableProgram = ProgramdetailRecords;
                    }
                    if (_dataset.Tables[3].Rows.Count > 0)
                    {
                        Screening _Screening = new Screening();
                        foreach (DataRow dr in _dataset.Tables[3].Rows)
                        {
                            _Screening.F001physicalDate = dr["F001physicalDate"].ToString();
                            _Screening.F002physicalResults = dr["F002physicalResults"].ToString();
                            _Screening.F003physicallFOReason = dr["F003physicallFOReason"].ToString();
                            _Screening.F004medFollowup = dr["F004medFollowup"].ToString();
                            _Screening.F005MedFOComments = dr["F005MedFOComments"].ToString();
                            _Screening.F006bpResults = dr["F006bpResults"].ToString();
                            _Screening.F007hgDate = dr["F007hgDate"].ToString();
                            _Screening.F008hgStatus = dr["F008hgStatus"].ToString();
                            _Screening.F009hgResults = dr["F009hgResults"].ToString();
                            _Screening.F010hgReferralDate = dr["F010hgReferralDate"].ToString();
                            _Screening.F011hgComments = dr["F011hgComments"].ToString();
                            _Screening.F012hgDate2 = dr["F012hgDate2"].ToString();
                            _Screening.F013hgResults2 = dr["F013hgResults2"].ToString();
                            _Screening.F014hgFOStatus = dr["F014hgFOStatus"].ToString();
                            _Screening.F015leadDate = dr["F015leadDate"].ToString();
                            _Screening.F016leadResults = dr["F016leadResults"].ToString();
                            _Screening.F017leadReferDate = dr["F017leadReferDate"].ToString();
                            _Screening.F018leadComments = dr["F018leadComments"].ToString();
                            _Screening.F019leadDate2 = dr["F019leadDate2"].ToString();
                            _Screening.F020leadResults2 = dr["F020leadResults2"].ToString();
                            _Screening.F021leadFOStatus = dr["F021leadFOStatus"].ToString();
                            _Screening.v022date = dr["v022date"].ToString();
                            _Screening.v023results = dr["v023results"].ToString();
                            _Screening.v024comments = dr["v024comments"].ToString();
                            _Screening.v025dateR1 = dr["v025dateR1"].ToString();
                            _Screening.v026resultsR1 = dr["v026resultsR1"].ToString();
                            _Screening.v027commentsR1 = dr["v027commentsR1"].ToString();
                            _Screening.v028dateR2 = dr["v028dateR2"].ToString();
                            _Screening.v029resultsR2 = dr["v029resultsR2"].ToString();
                            _Screening.v030commentsR2 = dr["v030commentsR2"].ToString();
                            _Screening.v031ReferralDate = dr["v031ReferralDate"].ToString();
                            _Screening.v032Treatment = dr["v032Treatment"].ToString();
                            _Screening.v033TreatmentComments = dr["v033TreatmentComments"].ToString();
                            _Screening.v034Completedate = dr["v034Completedate"].ToString();
                            _Screening.v035ExamStatus = dr["v035ExamStatus"].ToString();
                            _Screening.h036Date = dr["h036Date"].ToString();
                            _Screening.h037Results = dr["h037Results"].ToString();
                            _Screening.h038Comments = dr["h038Comments"].ToString();
                            _Screening.h039DateR1 = dr["h039DateR1"].ToString();
                            _Screening.h040ResultsR1 = dr["h040ResultsR1"].ToString();
                            _Screening.h041CommentsR1 = dr["h041CommentsR1"].ToString();
                            _Screening.h042DateR2 = dr["h042DateR2"].ToString();
                            _Screening.h043ResultsR2 = dr["h043ResultsR2"].ToString();
                            _Screening.h044CommentsR2 = dr["h044CommentsR2"].ToString();
                            _Screening.h045ReferralDate = dr["h045ReferralDate"].ToString();
                            _Screening.h046Treatment = dr["h046Treatment"].ToString();
                            _Screening.h047TreatmentComments = dr["h047TreatmentComments"].ToString();
                            _Screening.h048CompleteDate = dr["h048CompleteDate"].ToString();
                            _Screening.h049ExamStatus = dr["h049ExamStatus"].ToString();
                            _Screening.d050evDate = dr["d050evDate"].ToString();
                            _Screening.d051NameDEV = dr["d051NameDEV"].ToString();
                            _Screening.d052evResults = dr["d052evResults"].ToString();
                            _Screening.d053evResultsDetails = dr["d053evResultsDetails"].ToString();
                            _Screening.d054evDate2 = dr["d054evDate2"].ToString();
                            _Screening.d055evResults2 = dr["d055evResults2"].ToString();
                            _Screening.d056evReferral = dr["d056evReferral"].ToString();
                            _Screening.d057evFOStatus = dr["d057evFOStatus"].ToString();
                            _Screening.d058evComments = dr["d058evComments"].ToString();
                            _Screening.d059evTool = dr["d059evTool"].ToString();
                            _Screening.E060denDate = dr["E060denDate"].ToString();
                            _Screening.E061denResults = dr["E061denResults"].ToString();
                            _Screening.E062denPrevent = dr["E062denPrevent"].ToString();
                            _Screening.E063denReferralDate = dr["E063denReferralDate"].ToString();
                            _Screening.E064denTreatment = dr["E064denTreatment"].ToString();
                            _Screening.E065denTreatmentComments = dr["E065denTreatmentComments"].ToString();
                            _Screening.E066denTreatmentReceive = dr["E066denTreatmentReceive"].ToString();
                            _Screening.s067Date = dr["s067Date"].ToString();
                            _Screening.s068NameTCR = dr["s068NameTCR"].ToString();
                            _Screening.s069Details = dr["s069Details"].ToString();
                            _Screening.s070Results = dr["s070Results"].ToString();
                            _Screening.s071RescreenTCR = dr["s071RescreenTCR"].ToString();
                            _Screening.s072RescreenTCRDate = dr["s072RescreenTCRDate"].ToString();
                            _Screening.s073RescreenTCRResults = dr["s073RescreenTCRResults"].ToString();
                            _Screening.s074ReferralDC = dr["s074ReferralDC"].ToString();
                            _Screening.s075ReferDate = dr["s075ReferDate"].ToString();
                            _Screening.s076DCDate = dr["s076DCDate"].ToString();
                            _Screening.s077NameDC = dr["s077NameDC"].ToString();
                            _Screening.s078DetailDC = dr["s078DetailDC"].ToString();
                            _Screening.s079DCDate2 = dr["s079DCDate2"].ToString();
                            _Screening.s080DetailDC2 = dr["s080DetailDC2"].ToString();
                            _Screening.s081FOStatus = dr["s081FOStatus"].ToString();
                        }
                        //Screening changes
                        if (_dataset.Tables[5].Rows.Count > 0)
                        {
                            _Screening.AddPhysical = _dataset.Tables[5].Rows[0]["PhysicalScreening"].ToString();
                            _Screening.AddVision = _dataset.Tables[5].Rows[0]["Vision"].ToString();
                            _Screening.AddHearing = _dataset.Tables[5].Rows[0]["Hearing"].ToString();
                            _Screening.AddDental = _dataset.Tables[5].Rows[0]["Dental"].ToString();
                            _Screening.AddDevelop = _dataset.Tables[5].Rows[0]["Developmental"].ToString();
                            _Screening.AddSpeech = _dataset.Tables[5].Rows[0]["Speech"].ToString();
                            _Screening.ScreeningAcceptFileName = _dataset.Tables[5].Rows[0]["AcceptFileUl"].ToString();
                            _Screening.PhysicalFileName = _dataset.Tables[5].Rows[0]["PhyImageUl"].ToString();
                            _Screening.HearingFileName = _dataset.Tables[5].Rows[0]["HearingPicUl"].ToString();
                            _Screening.DentalFileName = _dataset.Tables[5].Rows[0]["DentalPicUl"].ToString();
                            _Screening.DevelopFileName = _dataset.Tables[5].Rows[0]["DevePicUl"].ToString();
                            _Screening.VisionFileName = _dataset.Tables[5].Rows[0]["VisionPicUl"].ToString();
                            _Screening.SpeechFileName = _dataset.Tables[5].Rows[0]["SpeechPicUl"].ToString();
                            _Screening.ParentAppID = Convert.ToInt32(_dataset.Tables[5].Rows[0]["ID"].ToString());
                            _Screening.Parentname = _dataset.Tables[5].Rows[0]["ParentName"].ToString();
                            _Screening.Consolidated = (_dataset.Tables[5].Rows[0]["Consolidated"] != DBNull.Value) ? Convert.ToInt32(_dataset.Tables[5].Rows[0]["Consolidated"]) : 0;

                            //Get screening scan document
                            _Screening.PhysicalImagejson = _dataset.Tables[5].Rows[0]["PhyImage"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]);
                            _Screening.PhysicalFileExtension = _dataset.Tables[5].Rows[0]["PhyFileExtension"].ToString();
                            string Url = Guid.NewGuid().ToString();
                            if (_Screening.PhysicalFileName != "" && _Screening.PhysicalFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["PhyImage"], 0, ((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]).Length);
                                file.Close();
                                _Screening.PhysicalImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            Url = "";
                            _Screening.VisionImagejson = _dataset.Tables[5].Rows[0]["VisionPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]);
                            _Screening.VisionFileExtension = _dataset.Tables[5].Rows[0]["VisionFileExtension"].ToString();
                            Url = Guid.NewGuid().ToString();
                            if (_Screening.VisionFileName != "" && _Screening.VisionFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["VisionPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]).Length);
                                file.Close();
                                _Screening.VisionImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            Url = "";
                            _Screening.HearingImagejson = _dataset.Tables[5].Rows[0]["HearingPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]);
                            _Screening.HearingFileExtension = _dataset.Tables[5].Rows[0]["HearingFileExtension"].ToString();
                            Url = Guid.NewGuid().ToString();
                            if (_Screening.HearingFileName != "" && _Screening.HearingFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["HearingPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]).Length);
                                file.Close();
                                _Screening.HearingImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            Url = "";
                            _Screening.DevelopImagejson = _dataset.Tables[5].Rows[0]["DevePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DevePic"]);
                            _Screening.DevelopFileExtension = _dataset.Tables[5].Rows[0]["DeveFileExtension"].ToString();
                            Url = Guid.NewGuid().ToString();
                            if (_Screening.DevelopFileName != "" && _Screening.DevelopFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["DevePic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DevePic"]).Length);
                                file.Close();
                                _Screening.DevelopImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            Url = "";
                            _Screening.DentalImagejson = _dataset.Tables[5].Rows[0]["DentalPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]);
                            _Screening.DentalFileExtension = _dataset.Tables[5].Rows[0]["DentalPicExtension"].ToString();
                            Url = Guid.NewGuid().ToString();
                            if (_Screening.DentalFileName != "" && _Screening.DentalFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["DentalPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]).Length);
                                file.Close();
                                _Screening.DentalImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            Url = "";
                            _Screening.SpeechImagejson = _dataset.Tables[5].Rows[0]["SpeechPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]);
                            _Screening.SpeechFileExtension = _dataset.Tables[5].Rows[0]["SpeechFileExtension"].ToString();

                            Url = Guid.NewGuid().ToString();
                            if (_Screening.SpeechFileName != "" && _Screening.SpeechFileExtension == ".pdf")
                            {
                                System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                                file.Write((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]).Length);
                                file.Close();
                                _Screening.SpeechImagejson = "/TempAttachment/" + Url + ".pdf";

                            }
                            //END
                        }
                        obj._Screening = _Screening;
                    }
                }
                if (_dataset.Tables[4].Rows.Count > 0)
                {
                    List<FamilyHousehold.Childhealthnutrition> _childhealthnutrition = new List<FamilyHousehold.Childhealthnutrition>();
                    FamilyHousehold.Childhealthnutrition info = null;
                    foreach (DataRow dr in _dataset.Tables[4].Rows)
                    {
                        info = new FamilyHousehold.Childhealthnutrition();
                        info.Id = dr["ID"].ToString();
                        info.MasterId = dr["ChildRecieveTreatment"].ToString();
                        info.Description = dr["Description"].ToString();
                        info.Questionid = dr["Questionid"].ToString();
                        info.Programid = dr["Programid"].ToString();
                        _childhealthnutrition.Add(info);
                    }
                    obj._Childhealthnutrition = _childhealthnutrition;
                }

                if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
                {
                    List<FamilyHousehold.Childcustomscreening> _Childcustomscreenings = new List<FamilyHousehold.Childcustomscreening>();
                    FamilyHousehold.Childcustomscreening info = null;
                    foreach (DataRow dr in _dataset.Tables[6].Rows)
                    {
                        info = new FamilyHousehold.Childcustomscreening();
                        info.QuestionID = dr["QuestionID"].ToString();
                        info.Screeningid = dr["Screeningid"].ToString();
                        info.Value = dr["Value"].ToString();
                        info.QuestionAcronym = dr["QuestionAcronym"].ToString();
                        info.optionid = dr["optionid"].ToString();
                        info.ScreeningDate = dr["ScreeningDate"].ToString() != "" ? Convert.ToDateTime(dr["ScreeningDate"]).ToString("MM/dd/yyyy") : "";
                        string Url = Guid.NewGuid().ToString();
                        if (dr["DocumentName"].ToString() != "" && dr["DocumentExtension"].ToString() == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])dr["Documentdata"], 0, ((byte[])dr["Documentdata"]).Length);
                            file.Close();
                            info.pdfpath = "/TempAttachment/" + Url + ".pdf";
                        }
                        else
                        {
                            info.pdfpath = "";
                            info.Documentdata = dr["Documentdata"].ToString() == "" ? "" : Convert.ToBase64String((byte[])dr["Documentdata"]);
                        }
                        _Childcustomscreenings.Add(info);
                    }
                    obj._childscreenings = _Childcustomscreenings;
                }

                if (_dataset.Tables[7] != null && _dataset.Tables[7].Rows.Count > 0)
                {
                    List<CustomScreeningAllowed> _CustomScreeningAllowed = new List<CustomScreeningAllowed>();
                    CustomScreeningAllowed info = null;
                    foreach (DataRow dr in _dataset.Tables[7].Rows)
                    {
                        info = new CustomScreeningAllowed();
                        info.ScreeningAllowed = dr["Screeningallowed"].ToString();
                        info.Screeningid = dr["Screeningid"].ToString();
                        info.ScreeningName = dr["screeningname"].ToString();
                        _CustomScreeningAllowed.Add(info);
                    }
                    obj._CustomScreeningAlloweds = _CustomScreeningAllowed;
                }
                if (_dataset.Tables[8] != null && _dataset.Tables[8].Rows.Count > 0)
                {
                    List<WellBabyExamModel> _WellBabyExamModel = new List<WellBabyExamModel>();
                    WellBabyExamModel info = null;
                    foreach (DataRow dr in _dataset.Tables[8].Rows)
                    {
                        info = new WellBabyExamModel();
                        info.ExaminedDate = dr["ExamDate"].ToString();
                        info.Month = dr["WellBabyExamMonth"].ToString();
                        info.EnrollmentDate = dr["EnrollmentDate"].ToString();
                        _WellBabyExamModel.Add(info);
                    }
                    obj.WellBabyExamModelList = _WellBabyExamModel;
                }

                DataAdapter.Dispose();
                command.Dispose();
                _dataset.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                _dataset.Dispose();
            }
        }


        private List<List<string>> PhysicalExamDatesInScreening(string dob)
        {
            List<List<string>> listOfDates = new List<List<string>>();
            DateTime dob1 = DateTime.Parse(dob);
            List<string> data = EachDate(dob1, 7, false);
            listOfDates.Add(EachDate(dob1, 7, false));
            listOfDates.Add(EachDate(dob1, 2, true));
            listOfDates.Add(EachDate(dob1, 4, true));
            listOfDates.Add(EachDate(dob1, 6, true));
            listOfDates.Add(EachDate(dob1, 12, true));
            listOfDates.Add(EachDate(dob1, 15, true));
            listOfDates.Add(EachDate(dob1, 18, true));
            listOfDates.Add(EachDate(dob1, 24, true));
            listOfDates.Add(EachDate(dob1, 30, true));
            listOfDates.Add(EachDate(dob1, 36, true));

            return listOfDates;
        }
        private List<string> EachDate(DateTime dob1, int monthOrDays, bool isMonth)
        {
            List<string> data = new List<string>();
            DateTime newDate = new DateTime();
            newDate = (isMonth) ? dob1.AddMonths(monthOrDays) : dob1.AddDays(monthOrDays);
            data.Add((isMonth) ? (monthOrDays + " Months") : ("1 Week"));
            if (newDate < DateTime.Now)
            {
                data.Add(newDate.ToShortDateString());
                data.Add("Expired");

            }
            else
            {
                data.Add(newDate.ToShortDateString());
                string futuredate = Convert.ToString((newDate - DateTime.Now).TotalDays);
                data.Add(futuredate.Split('.')[0] + " days");

            }

            return data;
        }
        public FamilyHousehold Getchild1(string ChildId, string HouseHoldId, string agencyid, string serverpath, string roleid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@ChildId", ChildId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_chidinfo";//SP_chidinfo
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        obj.ChildId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                        obj.Cfirstname = _dataset.Tables[0].Rows[0]["Firstname"].ToString();
                        obj.Cmiddlename = _dataset.Tables[0].Rows[0]["Middlename"].ToString();
                        obj.Clastname = _dataset.Tables[0].Rows[0]["Lastname"].ToString();
                        if (_dataset.Tables[0].Rows[0]["DOB"].ToString() != "")
                            obj.CDOB = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                        obj.DOBverifiedBy = _dataset.Tables[0].Rows[0]["Dobverifiedby"].ToString();
                        try
                        {
                            obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[0]["SSN"].ToString());
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                            obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString();
                        }

                        obj.CProgramType = _dataset.Tables[0].Rows[0]["Programtype"].ToString();
                        obj.CGender = _dataset.Tables[0].Rows[0]["Gender"].ToString();
                        obj.CRace = _dataset.Tables[0].Rows[0]["RaceID"].ToString();
                        obj.CRaceSubCategory = _dataset.Tables[0].Rows[0]["RaceSubCategoryID"].ToString();
                        if (_dataset.Tables[0].Rows[0]["ImmunizationServiceType"].ToString() != "")
                            obj.ImmunizationService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ImmunizationServiceType"]);
                        if (_dataset.Tables[0].Rows[0]["MedicalService"].ToString() != "")
                            obj.MedicalService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["MedicalService"]);
                        if (_dataset.Tables[0].Rows[0]["Medicalhome"].ToString() != "")
                            obj.Medicalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Medicalhome"]);
                        if (_dataset.Tables[0].Rows[0]["ParentDisable"].ToString() != "")
                            obj.CParentdisable = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentDisable"]);
                        //Added by Santosh For IsIEP IsFSP
                        if (_dataset.Tables[0].Rows[0]["IEP"].ToString() != "")
                            obj.IsIEP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IEP"].ToString());
                        if (_dataset.Tables[0].Rows[0]["IFSP"].ToString() != "")
                            obj.IsIFSP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IFSP"].ToString());
                        if (_dataset.Tables[0].Rows[0]["IsExpired"].ToString() != "")
                            obj.IsExpired = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["IsExpired"].ToString());
                        //
                        if (_dataset.Tables[0].Rows[0]["BMIStatus"].ToString() != "")
                            obj.BMIStatus = Convert.ToInt32(_dataset.Tables[0].Rows[0]["BMIStatus"]);
                        if (_dataset.Tables[0].Rows[0]["DentalHome"].ToString() != "")
                            obj.CDentalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DentalHome"]);
                        if (_dataset.Tables[0].Rows[0]["Ethnicity"].ToString() != "")
                            obj.CEthnicity = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Ethnicity"]);
                        obj.CFileName = _dataset.Tables[0].Rows[0]["FileNameul"].ToString();
                        obj.CFileExtension = _dataset.Tables[0].Rows[0]["FileExtension"].ToString();
                        obj.DobFileName = _dataset.Tables[0].Rows[0]["Dobfilename"].ToString();
                        obj.CDoctor = _dataset.Tables[0].Rows[0]["doctorname"].ToString();
                        obj.CDentist = _dataset.Tables[0].Rows[0]["dentistname"].ToString();
                        if (_dataset.Tables[0].Rows[0]["Doctorvalue"].ToString() != "")
                            obj.Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Doctorvalue"]);
                        if (_dataset.Tables[0].Rows[0]["Dentistvalue"].ToString() != "")
                            obj.Dentist = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Dentistvalue"]);
                        if (_dataset.Tables[0].Rows[0]["SchoolDistrict"].ToString() != "")
                            obj.SchoolDistrict = Convert.ToInt32(_dataset.Tables[0].Rows[0]["SchoolDistrict"]);
                        if (_dataset.Tables[0].Rows[0]["DobPaper"].ToString() != "")
                            obj.DobverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["DobPaper"]);
                        if (_dataset.Tables[0].Rows[0]["FosterChild"].ToString() != "")
                            obj.IsFoster = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FosterChild"]);
                        if (_dataset.Tables[0].Rows[0]["WelfareAgency"].ToString() != "")
                            obj.Inwalfareagency = Convert.ToInt32(_dataset.Tables[0].Rows[0]["WelfareAgency"]);
                        if (_dataset.Tables[0].Rows[0]["DualCustodyChild"].ToString() != "")
                            obj.InDualcustody = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DualCustodyChild"]);
                        if (_dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString() != "")
                            obj.InsuranceOption = _dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString();
                        if (_dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString() != "")
                            obj.MedicalNote = _dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString();
                        if (_dataset.Tables[0].Rows[0]["ImmunizationinPaper"].ToString() != "")
                            obj.ImmunizationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ImmunizationinPaper"]);
                        obj.ImmunizationFileName = _dataset.Tables[0].Rows[0]["ImmunizationFileName"].ToString();
                        obj.Raceother = _dataset.Tables[0].Rows[0]["OtherRace"].ToString();
                        //ChildTransport

                        obj.CTransport = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ChildTransport"].ToString());

                        if (!string.IsNullOrEmpty((_dataset.Tables[0].Rows[0]["TransportNeeded"]).ToString()))
                        {
                            obj.CTransportNeeded = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TransportNeeded"]);
                        }
                        else
                        {
                            obj.CTransportNeeded = false;
                        }

                        //End
                        //Ehs Health question
                        obj.EhsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                        obj.EhsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                        obj.EhsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                        obj.EhsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                        obj.EhsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                        obj.EhsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();
                        obj.EHSmpplan = _dataset.Tables[0].Rows[0]["M_PCarePlan"].ToString();
                        obj.EHSmpplancomment = _dataset.Tables[0].Rows[0]["M_PCarePlanComment"].ToString();
                        obj.EHSAllergy = _dataset.Tables[0].Rows[0]["EHSAllergy"].ToString();
                        obj.EHSEpiPen = Convert.ToInt32(_dataset.Tables[0].Rows[0]["EHSEpiPen"]);



                        //HS Health question
                        obj.HsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                        obj.HsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                        obj.HsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                        obj.HsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                        obj.HsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                        obj.HSmpplan = _dataset.Tables[0].Rows[0]["M_PCarePlan"].ToString();
                        obj.HSmpplanComment = _dataset.Tables[0].Rows[0]["M_PCarePlanComment"].ToString();
                        obj.HsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();
                        obj.HsChildDentalCare = _dataset.Tables[0].Rows[0]["DentalCare"].ToString();
                        obj.HsDentalExam = _dataset.Tables[0].Rows[0]["CurrentDentalexam"].ToString();
                        obj.HsRecentDentalExam = _dataset.Tables[0].Rows[0]["RecentDentalExam"].ToString();
                        obj.HsChildNeedDentalTreatment = _dataset.Tables[0].Rows[0]["NeedDentalTreatment"].ToString();
                        obj.HsChildRecievedDentalTreatment = _dataset.Tables[0].Rows[0]["RecievedDentalTreatment"].ToString();
                        obj.ChildProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ChildEverHadProfExam"].ToString();

                        //Nutrition Question without HS/EHS

                        //obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        //obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        //obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        //obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        //obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        //obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        //obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //    obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //    obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        //obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        //obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        //obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        //obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        //obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        //obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        //obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        //obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        //obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        //obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        //obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        //obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        //obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        //obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        //obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        //obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        //obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        //obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        //if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                        //    obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        //if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                        //    obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        //if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                        //    obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        //if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                        //    obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        //if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                        //    obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        //obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        //obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        //obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        //obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        //obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        //obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        //obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        //obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        //obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        //obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        //obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        //obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        //obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        //obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        //obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();

                        //obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        //obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        //obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        //obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                        //obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                        //obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        //obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                        //obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                        //obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                        //obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                        //obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                        //


                        //Nutrition question
                        //Changes on 19Dec2016
                        //if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "1")
                        //{
                        //    obj.EhsRestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                        //    obj.EhsChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                        //    obj.EhsPersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        //    obj.EhsPersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        //    obj.EhsPersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        //    obj.EhsDramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        //    obj.EhsRecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        //    obj.EhsChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        //    obj.EhsFoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        //    obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.EhsRecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //    //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //    //    obj.EhsWICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //    //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //    //    obj.EhsFoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //    //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //    //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        //    obj.EhsFoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        //    obj.EhschildTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //    //obj.EhsChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        //    obj.Ehsspoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        //    obj.Ehsfeedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        //    obj.EhschildThin = Convert.ToInt32(_dataset.Tables[0].Rows[0]["childhealth"].ToString());
                        //    obj.EhsTakebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        //    obj.Ehschewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        //    obj.EhsChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        //    obj.EhsChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        //    obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        //    obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        //    obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        //    obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        //    obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        //    obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        //    obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        //    obj.EhsChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        //    obj.EhsChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        //    if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                        //        obj.EhsBreakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        //    if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                        //        obj.EhsLunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        //    if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                        //        obj.EhsSnack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        //    if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                        //        obj.EhsDinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        //    if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                        //        obj.EhsNA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        //    // obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        //    obj.EhsNauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        //    obj.EhsDiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        //    obj.EhsConstipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        //    obj.EhsDramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        //    obj.EhsRecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        //    obj.EhsRecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        //    obj.EhsSpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        //    obj.EhsFoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        //    obj.EhsNutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        //    obj.EhsChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        //    obj.EhsSpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        //    obj.EhsSpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        //    obj.EhsBottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        //    obj.EhsEatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        //}
                        //else if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "2")
                        //{
                        //    obj.RestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                        //    obj.ChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                        //    obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        //    obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        //    obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        //    obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        //    obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        //    obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        //    obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        //    obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //    if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //        obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //    if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //        obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //    if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //        obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        //    obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        //    obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //    obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        //    obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        //    obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        //    obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        //    obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        //    obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        //    obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        //    obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        //    obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        //    obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        //    obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        //    obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        //    obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        //    obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        //    obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        //    obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        //    obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        //    if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                        //        obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        //    if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                        //        obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        //    if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                        //        obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        //    if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                        //        obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        //    if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                        //        obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        //    obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        //    obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        //    obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        //    obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        //    obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        //    obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        //    obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        //    obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        //    obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        //    obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        //    obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        //    obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        //    obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        //    obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        //    obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        //}
                        //else
                        //{

                        //    obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        //    obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        //    obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        //    obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        //    obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        //    obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        //    obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        //    obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        //    obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //    if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //        obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //    if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //        obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //    if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //        obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        //    obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        //    obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //    obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        //    obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        //    obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        //    obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        //    obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        //    obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        //    obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        //    obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        //    obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        //    obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        //    obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        //    obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        //    obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        //    obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        //    obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        //    obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        //    obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        //    if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                        //        obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        //    if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                        //        obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        //    if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                        //        obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        //    if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                        //        obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        //    if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                        //        obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        //    obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        //    obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        //    obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        //    obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        //    obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        //    obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        //    obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        //    obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        //    obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        //    obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        //    obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        //    obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        //    obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        //    obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        //    obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                        //}


                        //End



                        obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                        obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                        obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                        obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                        obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                        obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                        obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                        obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                        obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                        obj.HWInput = _dataset.Tables[0].Rows[0]["HWInput"].ToString();
                        obj.AssessmentDate = _dataset.Tables[0].Rows[0]["AssessmentDate"].ToString() != "" ? Convert.ToDateTime(_dataset.Tables[0].Rows[0]["AssessmentDate"]).ToString("MM/dd/yyyy") : "";
                        obj.AHeight = _dataset.Tables[0].Rows[0]["BHeight"].ToString();
                        obj.AWeight = _dataset.Tables[0].Rows[0]["BWeight"].ToString();
                        obj.HeadCircle = _dataset.Tables[0].Rows[0]["HeadCirc"].ToString();



                    }
                    //if (_dataset.Tables[1].Rows.Count > 0)
                    //{
                    //    List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                    //    FamilyHousehold.ImmunizationRecord obj1;
                    //    foreach (DataRow dr in _dataset.Tables[1].Rows)
                    //    {
                    //        obj1 = new FamilyHousehold.ImmunizationRecord();
                    //        obj1.ImmunizationId = Convert.ToInt32(dr["Immunization_ID"]);
                    //        obj1.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationmasterid"]);
                    //        obj1.Dose = dr["Dose"].ToString();
                    //        if (dr["Dose1"].ToString() != "")
                    //            obj1.Dose1 = Convert.ToDateTime(dr["Dose1"]).ToString("MM/dd/yyyy");
                    //        else
                    //            obj1.Dose1 = dr["Dose1"].ToString();
                    //        if (dr["Dose2"].ToString() != "")
                    //            obj1.Dose2 = Convert.ToDateTime(dr["Dose2"]).ToString("MM/dd/yyyy");
                    //        else
                    //            obj1.Dose2 = dr["Dose2"].ToString();
                    //        if (dr["Dose3"].ToString() != "")
                    //            obj1.Dose3 = Convert.ToDateTime(dr["Dose3"]).ToString("MM/dd/yyyy");
                    //        else
                    //            obj1.Dose3 = dr["Dose3"].ToString();
                    //        if (dr["Dose4"].ToString() != "")
                    //            obj1.Dose4 = Convert.ToDateTime(dr["Dose4"]).ToString("MM/dd/yyyy");
                    //        else
                    //            obj1.Dose4 = dr["Dose4"].ToString();
                    //        if (dr["Dose5"].ToString() != "")
                    //            obj1.Dose5 = Convert.ToDateTime(dr["Dose5"]).ToString("MM/dd/yyyy");
                    //        else
                    //            obj1.Dose5 = dr["Dose5"].ToString();
                    //        if (dr["Exempt1"].ToString() != "")
                    //            obj1.Exempt1 = Convert.ToBoolean(dr["Exempt1"]);
                    //        if (dr["Exempt2"].ToString() != "")
                    //            obj1.Exempt2 = Convert.ToBoolean(dr["Exempt2"]);
                    //        if (dr["Exempt3"].ToString() != "")
                    //            obj1.Exempt3 = Convert.ToBoolean(dr["Exempt3"]);
                    //        if (dr["Exempt4"].ToString() != "")
                    //            obj1.Exempt4 = Convert.ToBoolean(dr["Exempt4"]);
                    //        if (dr["Exempt5"].ToString() != "")
                    //            obj1.Exempt5 = Convert.ToBoolean(dr["Exempt5"]);
                    //        if (dr["Preemptive1"].ToString() != "")
                    //            obj1.Preempt1 = Convert.ToBoolean(dr["Preemptive1"]);
                    //        if (dr["Preemptive2"].ToString() != "")
                    //            obj1.Preempt2 = Convert.ToBoolean(dr["Preemptive2"]);
                    //        if (dr["Preemptive3"].ToString() != "")
                    //            obj1.Preempt3 = Convert.ToBoolean(dr["Preemptive3"]);
                    //        if (dr["Preemptive4"].ToString() != "")
                    //            obj1.Preempt4 = Convert.ToBoolean(dr["Preemptive4"]);
                    //        if (dr["Preemptive5"].ToString() != "")
                    //            obj1.Preempt5 = Convert.ToBoolean(dr["Preemptive5"]);
                    //        ImmunizationRecords.Add(obj1);
                    //        obj1 = null;
                    //    }
                    //    obj.ImmunizationRecords = ImmunizationRecords;
                    //}
                    if (_dataset.Tables[2].Rows.Count > 0)
                    {
                        List<FamilyHousehold.Programdetail> ProgramdetailRecords = new List<FamilyHousehold.Programdetail>();
                        FamilyHousehold.Programdetail obj1;
                        foreach (DataRow dr in _dataset.Tables[2].Rows)
                        {
                            obj1 = new FamilyHousehold.Programdetail();
                            obj1.Id = Convert.ToInt32(dr["programid"]);
                            obj1.ReferenceId = dr["ReferenceId"].ToString();
                            ProgramdetailRecords.Add(obj1);
                        }
                        obj.AvailableProgram = ProgramdetailRecords;
                    }
                    //if (_dataset.Tables[3].Rows.Count > 0)
                    //{
                    //    Screening _Screening = new Screening();
                    //    foreach (DataRow dr in _dataset.Tables[3].Rows)
                    //    {
                    //        _Screening.F001physicalDate = dr["F001physicalDate"].ToString();
                    //        _Screening.F002physicalResults = dr["F002physicalResults"].ToString();
                    //        _Screening.F003physicallFOReason = dr["F003physicallFOReason"].ToString();
                    //        _Screening.F004medFollowup = dr["F004medFollowup"].ToString();
                    //        _Screening.F005MedFOComments = dr["F005MedFOComments"].ToString();
                    //        _Screening.F006bpResults = dr["F006bpResults"].ToString();
                    //        _Screening.F007hgDate = dr["F007hgDate"].ToString();
                    //        _Screening.F008hgStatus = dr["F008hgStatus"].ToString();
                    //        _Screening.F009hgResults = dr["F009hgResults"].ToString();
                    //        _Screening.F010hgReferralDate = dr["F010hgReferralDate"].ToString();
                    //        _Screening.F011hgComments = dr["F011hgComments"].ToString();
                    //        _Screening.F012hgDate2 = dr["F012hgDate2"].ToString();
                    //        _Screening.F013hgResults2 = dr["F013hgResults2"].ToString();
                    //        _Screening.F014hgFOStatus = dr["F014hgFOStatus"].ToString();
                    //        _Screening.F015leadDate = dr["F015leadDate"].ToString();
                    //        _Screening.F016leadResults = dr["F016leadResults"].ToString();
                    //        _Screening.F017leadReferDate = dr["F017leadReferDate"].ToString();
                    //        _Screening.F018leadComments = dr["F018leadComments"].ToString();
                    //        _Screening.F019leadDate2 = dr["F019leadDate2"].ToString();
                    //        _Screening.F020leadResults2 = dr["F020leadResults2"].ToString();
                    //        _Screening.F021leadFOStatus = dr["F021leadFOStatus"].ToString();
                    //        _Screening.v022date = dr["v022date"].ToString();
                    //        _Screening.v023results = dr["v023results"].ToString();
                    //        _Screening.v024comments = dr["v024comments"].ToString();
                    //        _Screening.v025dateR1 = dr["v025dateR1"].ToString();
                    //        _Screening.v026resultsR1 = dr["v026resultsR1"].ToString();
                    //        _Screening.v027commentsR1 = dr["v027commentsR1"].ToString();
                    //        _Screening.v028dateR2 = dr["v028dateR2"].ToString();
                    //        _Screening.v029resultsR2 = dr["v029resultsR2"].ToString();
                    //        _Screening.v030commentsR2 = dr["v030commentsR2"].ToString();
                    //        _Screening.v031ReferralDate = dr["v031ReferralDate"].ToString();
                    //        _Screening.v032Treatment = dr["v032Treatment"].ToString();
                    //        _Screening.v033TreatmentComments = dr["v033TreatmentComments"].ToString();
                    //        _Screening.v034Completedate = dr["v034Completedate"].ToString();
                    //        _Screening.v035ExamStatus = dr["v035ExamStatus"].ToString();
                    //        _Screening.h036Date = dr["h036Date"].ToString();
                    //        _Screening.h037Results = dr["h037Results"].ToString();
                    //        _Screening.h038Comments = dr["h038Comments"].ToString();
                    //        _Screening.h039DateR1 = dr["h039DateR1"].ToString();
                    //        _Screening.h040ResultsR1 = dr["h040ResultsR1"].ToString();
                    //        _Screening.h041CommentsR1 = dr["h041CommentsR1"].ToString();
                    //        _Screening.h042DateR2 = dr["h042DateR2"].ToString();
                    //        _Screening.h043ResultsR2 = dr["h043ResultsR2"].ToString();
                    //        _Screening.h044CommentsR2 = dr["h044CommentsR2"].ToString();
                    //        _Screening.h045ReferralDate = dr["h045ReferralDate"].ToString();
                    //        _Screening.h046Treatment = dr["h046Treatment"].ToString();
                    //        _Screening.h047TreatmentComments = dr["h047TreatmentComments"].ToString();
                    //        _Screening.h048CompleteDate = dr["h048CompleteDate"].ToString();
                    //        _Screening.h049ExamStatus = dr["h049ExamStatus"].ToString();
                    //        _Screening.d050evDate = dr["d050evDate"].ToString();
                    //        _Screening.d051NameDEV = dr["d051NameDEV"].ToString();
                    //        _Screening.d052evResults = dr["d052evResults"].ToString();
                    //        _Screening.d053evResultsDetails = dr["d053evResultsDetails"].ToString();
                    //        _Screening.d054evDate2 = dr["d054evDate2"].ToString();
                    //        _Screening.d055evResults2 = dr["d055evResults2"].ToString();
                    //        _Screening.d056evReferral = dr["d056evReferral"].ToString();
                    //        _Screening.d057evFOStatus = dr["d057evFOStatus"].ToString();
                    //        _Screening.d058evComments = dr["d058evComments"].ToString();
                    //        _Screening.d059evTool = dr["d059evTool"].ToString();
                    //        _Screening.E060denDate = dr["E060denDate"].ToString();
                    //        _Screening.E061denResults = dr["E061denResults"].ToString();
                    //        _Screening.E062denPrevent = dr["E062denPrevent"].ToString();
                    //        _Screening.E063denReferralDate = dr["E063denReferralDate"].ToString();
                    //        _Screening.E064denTreatment = dr["E064denTreatment"].ToString();
                    //        _Screening.E065denTreatmentComments = dr["E065denTreatmentComments"].ToString();
                    //        _Screening.E066denTreatmentReceive = dr["E066denTreatmentReceive"].ToString();
                    //        _Screening.s067Date = dr["s067Date"].ToString();
                    //        _Screening.s068NameTCR = dr["s068NameTCR"].ToString();
                    //        _Screening.s069Details = dr["s069Details"].ToString();
                    //        _Screening.s070Results = dr["s070Results"].ToString();
                    //        _Screening.s071RescreenTCR = dr["s071RescreenTCR"].ToString();
                    //        _Screening.s072RescreenTCRDate = dr["s072RescreenTCRDate"].ToString();
                    //        _Screening.s073RescreenTCRResults = dr["s073RescreenTCRResults"].ToString();
                    //        _Screening.s074ReferralDC = dr["s074ReferralDC"].ToString();
                    //        _Screening.s075ReferDate = dr["s075ReferDate"].ToString();
                    //        _Screening.s076DCDate = dr["s076DCDate"].ToString();
                    //        _Screening.s077NameDC = dr["s077NameDC"].ToString();
                    //        _Screening.s078DetailDC = dr["s078DetailDC"].ToString();
                    //        _Screening.s079DCDate2 = dr["s079DCDate2"].ToString();
                    //        _Screening.s080DetailDC2 = dr["s080DetailDC2"].ToString();
                    //        _Screening.s081FOStatus = dr["s081FOStatus"].ToString();
                    //    }
                    //    //Screening changes
                    //if (_dataset.Tables[5].Rows.Count > 0)
                    //{
                    //    _Screening.AddPhysical = _dataset.Tables[5].Rows[0]["PhysicalScreening"].ToString();
                    //    _Screening.AddVision = _dataset.Tables[5].Rows[0]["Vision"].ToString();
                    //    _Screening.AddHearing = _dataset.Tables[5].Rows[0]["Hearing"].ToString();
                    //    _Screening.AddDental = _dataset.Tables[5].Rows[0]["Dental"].ToString();
                    //    _Screening.AddDevelop = _dataset.Tables[5].Rows[0]["Developmental"].ToString();
                    //    _Screening.AddSpeech = _dataset.Tables[5].Rows[0]["Speech"].ToString();
                    //    _Screening.ScreeningAcceptFileName = _dataset.Tables[5].Rows[0]["AcceptFileUl"].ToString();
                    //    _Screening.PhysicalFileName = _dataset.Tables[5].Rows[0]["PhyImageUl"].ToString();
                    //    _Screening.HearingFileName = _dataset.Tables[5].Rows[0]["HearingPicUl"].ToString();
                    //    _Screening.DentalFileName = _dataset.Tables[5].Rows[0]["DentalPicUl"].ToString();
                    //    _Screening.DevelopFileName = _dataset.Tables[5].Rows[0]["DevePicUl"].ToString();
                    //    _Screening.VisionFileName = _dataset.Tables[5].Rows[0]["VisionPicUl"].ToString();
                    //    _Screening.SpeechFileName = _dataset.Tables[5].Rows[0]["SpeechPicUl"].ToString();
                    //    _Screening.ParentAppID = Convert.ToInt32(_dataset.Tables[5].Rows[0]["ID"].ToString());
                    //    _Screening.Parentname = _dataset.Tables[5].Rows[0]["ParentName"].ToString();
                    //    _Screening.Consolidated = Convert.ToInt32(_dataset.Tables[5].Rows[0]["Consolidated"].ToString());

                    //    //Get screening scan document
                    //    _Screening.PhysicalImagejson = _dataset.Tables[5].Rows[0]["PhyImage"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]);
                    //    _Screening.PhysicalFileExtension = _dataset.Tables[5].Rows[0]["PhyFileExtension"].ToString();
                    //    string Url = Guid.NewGuid().ToString();
                    //    if (_Screening.PhysicalFileName != "" && _Screening.PhysicalFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["PhyImage"], 0, ((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]).Length);
                    //        file.Close();
                    //        _Screening.PhysicalImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    Url = "";
                    //    _Screening.VisionImagejson = _dataset.Tables[5].Rows[0]["VisionPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]);
                    //    _Screening.VisionFileExtension = _dataset.Tables[5].Rows[0]["VisionFileExtension"].ToString();
                    //    Url = Guid.NewGuid().ToString();
                    //    if (_Screening.VisionFileName != "" && _Screening.VisionFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["VisionPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]).Length);
                    //        file.Close();
                    //        _Screening.VisionImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    Url = "";
                    //    _Screening.HearingImagejson = _dataset.Tables[5].Rows[0]["HearingPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]);
                    //    _Screening.HearingFileExtension = _dataset.Tables[5].Rows[0]["HearingFileExtension"].ToString();
                    //    Url = Guid.NewGuid().ToString();
                    //    if (_Screening.HearingFileName != "" && _Screening.HearingFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["HearingPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]).Length);
                    //        file.Close();
                    //        _Screening.HearingImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    Url = "";
                    //    _Screening.DevelopImagejson = _dataset.Tables[5].Rows[0]["DevePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DevePic"]);
                    //    _Screening.DevelopFileExtension = _dataset.Tables[5].Rows[0]["DeveFileExtension"].ToString();
                    //    Url = Guid.NewGuid().ToString();
                    //    if (_Screening.DevelopFileName != "" && _Screening.DevelopFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["DevePic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DevePic"]).Length);
                    //        file.Close();
                    //        _Screening.DevelopImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    Url = "";
                    //    _Screening.DentalImagejson = _dataset.Tables[5].Rows[0]["DentalPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]);
                    //    _Screening.DentalFileExtension = _dataset.Tables[5].Rows[0]["DentalPicExtension"].ToString();
                    //    Url = Guid.NewGuid().ToString();
                    //    if (_Screening.DentalFileName != "" && _Screening.DentalFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["DentalPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]).Length);
                    //        file.Close();
                    //        _Screening.DentalImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    Url = "";
                    //    _Screening.SpeechImagejson = _dataset.Tables[5].Rows[0]["SpeechPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]);
                    //    _Screening.SpeechFileExtension = _dataset.Tables[5].Rows[0]["SpeechFileExtension"].ToString();

                    //    Url = Guid.NewGuid().ToString();
                    //    if (_Screening.SpeechFileName != "" && _Screening.SpeechFileExtension == ".pdf")
                    //    {
                    //        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //        file.Write((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]).Length);
                    //        file.Close();
                    //        _Screening.SpeechImagejson = "/TempAttachment/" + Url + ".pdf";

                    //    }
                    //    //END
                    //}
                    //obj._Screening = _Screening;
                    //}

                    //if (_dataset.Tables[4].Rows.Count > 0)
                    //{
                    //    List<FamilyHousehold.Childhealthnutrition> _childhealthnutrition = new List<FamilyHousehold.Childhealthnutrition>();
                    //    FamilyHousehold.Childhealthnutrition info = null;
                    //    foreach (DataRow dr in _dataset.Tables[4].Rows)
                    //    {
                    //        info = new FamilyHousehold.Childhealthnutrition();
                    //        info.Id = dr["ID"].ToString();
                    //        info.MasterId = dr["ChildRecieveTreatment"].ToString();
                    //        info.Description = dr["Description"].ToString();
                    //        info.Questionid = dr["Questionid"].ToString();
                    //        info.Programid = dr["Programid"].ToString();
                    //        _childhealthnutrition.Add(info);
                    //    }
                    //    obj._Childhealthnutrition = _childhealthnutrition;
                    //}

                    //if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
                    //{
                    //    List<FamilyHousehold.Childcustomscreening> _Childcustomscreenings = new List<FamilyHousehold.Childcustomscreening>();
                    //    FamilyHousehold.Childcustomscreening info = null;
                    //    foreach (DataRow dr in _dataset.Tables[6].Rows)
                    //    {
                    //        info = new FamilyHousehold.Childcustomscreening();
                    //        info.QuestionID = dr["QuestionID"].ToString();
                    //        info.Screeningid = dr["Screeningid"].ToString();
                    //        info.Value = dr["Value"].ToString();
                    //        info.QuestionAcronym = dr["QuestionAcronym"].ToString();
                    //        info.optionid = dr["optionid"].ToString();
                    //        info.ScreeningDate = dr["ScreeningDate"].ToString() != "" ? Convert.ToDateTime(dr["ScreeningDate"]).ToString("MM/dd/yyyy") : "";
                    //        string Url = Guid.NewGuid().ToString();
                    //        if (dr["DocumentName"].ToString() != "" && dr["DocumentExtension"].ToString() == ".pdf")
                    //        {
                    //            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                    //            file.Write((byte[])dr["Documentdata"], 0, ((byte[])dr["Documentdata"]).Length);
                    //            file.Close();
                    //            info.pdfpath = "/TempAttachment/" + Url + ".pdf";
                    //        }
                    //        else
                    //        {
                    //            info.pdfpath = "";
                    //            info.Documentdata = dr["Documentdata"].ToString() == "" ? "" : Convert.ToBase64String((byte[])dr["Documentdata"]);
                    //        }
                    //        _Childcustomscreenings.Add(info);
                    //    }
                    //    obj._childscreenings = _Childcustomscreenings;
                    //}

                    //if (_dataset.Tables[7] != null && _dataset.Tables[7].Rows.Count > 0)
                    //{
                    //    List<CustomScreeningAllowed> _CustomScreeningAllowed = new List<CustomScreeningAllowed>();
                    //    CustomScreeningAllowed info = null;
                    //    foreach (DataRow dr in _dataset.Tables[7].Rows)
                    //    {
                    //        info = new CustomScreeningAllowed();
                    //        info.ScreeningAllowed = dr["Screeningallowed"].ToString();
                    //        info.Screeningid = dr["Screeningid"].ToString();
                    //        info.ScreeningName = dr["screeningname"].ToString();
                    //        _CustomScreeningAllowed.Add(info);
                    //    }
                    //    obj._CustomScreeningAlloweds = _CustomScreeningAllowed;
                    //}

                    DataAdapter.Dispose();
                    command.Dispose();
                    _dataset.Dispose();

                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                _dataset.Dispose();
            }
            return obj;
        }
        public string Deletechild(string ChildId, string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deletechidinfo";
                command.Parameters.Add(new SqlParameter("@ChildId", ChildId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public List<FamilyHousehold.Parentphone1> PhoneDetails(string householdid, string Parentid, string Agencyid)
        {
            List<FamilyHousehold.Parentphone1> _Parentphone1phone = new List<FamilyHousehold.Parentphone1>();
            try
            {
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@Parentid", Parentid));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Sel_phonelist";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in familydataTable.Rows)
                    {
                        FamilyHousehold.Parentphone1 _phoneadd = new FamilyHousehold.Parentphone1();
                        _phoneadd.PPhoneId = Convert.ToInt32(row["Id"]);
                        _phoneadd.PhoneTypeP = row["PhoneType"].ToString();
                        _phoneadd.phonenoP = row["Phoneno"].ToString();
                        _phoneadd.StateP = row["IsPrimaryContact"].ToString() == "" ? false : Convert.ToBoolean(row["IsPrimaryContact"]);
                        _phoneadd.SmsP = row["Sms"].ToString() == "" ? false : Convert.ToBoolean(row["Sms"]);
                        _phoneadd.notesP = row["Notes"].ToString();
                        _Parentphone1phone.Add(_phoneadd);
                    }
                }
                return _Parentphone1phone;
            }
            catch (Exception ex)
            {
                //  totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _Parentphone1phone;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public FamilyHousehold Getother(string OthersId, string HouseHoldId, string agencyid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@OthersId", OthersId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_otherinfo";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable != null && familydataTable.Rows.Count > 0)
                {
                    obj.OthersId = Convert.ToInt32(familydataTable.Rows[0]["ID"]);
                    obj.Ofirstname = familydataTable.Rows[0]["Firstname"].ToString();
                    obj.Omiddlename = familydataTable.Rows[0]["Middlename"].ToString();
                    obj.Olastname = familydataTable.Rows[0]["Lastname"].ToString();
                    obj.ODOB = Convert.ToDateTime(familydataTable.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                    obj.OGender = familydataTable.Rows[0]["Gender"].ToString();
                    obj.Oemergencycontact = familydataTable.Rows[0]["isemergency"].ToString() == "" ? false : Convert.ToBoolean(familydataTable.Rows[0]["isemergency"]);
                    obj.HouseHoldImagejson = familydataTable.Rows[0]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])familydataTable.Rows[0]["ProfilePic"]);

                    try
                    {
                        obj.ParentSSN3 = familydataTable.Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(familydataTable.Rows[0]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.ParentSSN3 = familydataTable.Rows[0]["SSN"].ToString();
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public string Deleteother(string OthersId, string HouseHoldId, string agnecyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteotherinfo";
                command.Parameters.Add(new SqlParameter("@OthersId", OthersId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agnecyid", agnecyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public List<FamilyHousehold> OtherDetails(string Householdid, string agencyid)
        {
            List<FamilyHousehold> _familyinfo = new List<FamilyHousehold>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Householdid", Householdid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Sel_otherlist";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < familydataTable.Rows.Count; i++)
                    {
                        FamilyHousehold familyinfo = new FamilyHousehold();
                        familyinfo.Ofirstname = Convert.ToString(familydataTable.Rows[i]["Firstname"]);
                        familyinfo.Olastname = Convert.ToString(familydataTable.Rows[i]["Lastname"]);
                        familyinfo.Omiddlename = Convert.ToString(familydataTable.Rows[i]["Middlename"]);
                        familyinfo.ODOB = Convert.ToDateTime(familydataTable.Rows[i]["DOB"]).ToString("MM/dd/yyyy");
                        familyinfo.OthersId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                        familyinfo.CSSN = familydataTable.Rows[i]["SSN"].ToString() != "" ? EncryptDecrypt.Decrypt(familydataTable.Rows[i]["SSN"].ToString()) : "";
                        if ((Convert.ToString(familydataTable.Rows[i]["Gender"]) == "1"))
                        {
                            familyinfo.OGender = "Male";
                        }
                        else if ((Convert.ToString(familydataTable.Rows[i]["Gender"]) == "2"))
                        {
                            familyinfo.OGender = "Female";
                        }
                        else
                        {
                            familyinfo.OGender = "Other";
                        }
                        _familyinfo.Add(familyinfo);
                    }
                }
                return _familyinfo;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return _familyinfo;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public string addOthersInfo(FamilyHousehold obj, int mode, Guid ID, string agencyid, string roleid)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@OthersId", obj.OthersId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Ofirstname", obj.Ofirstname));
                command.Parameters.Add(new SqlParameter("@Olastname", obj.Olastname));
                command.Parameters.Add(new SqlParameter("@Omiddlename", obj.Omiddlename));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@ODOB", obj.ODOB));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));

                if (obj.Oemergencycontact)
                    command.Parameters.Add(new SqlParameter("@Isemergency", obj.Oemergencycontact));
                else
                    command.Parameters.Add(new SqlParameter("@Isemergency", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OGender", obj.OGender));
                command.Parameters.Add(new SqlParameter("@ParentSSN3", obj.ParentSSN3 == null ? null : EncryptDecrypt.Encrypt(obj.ParentSSN3)));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@Othersidgenereated", string.Empty));
                command.Parameters["@Othersidgenereated"].Direction = ParameterDirection.Output;
                command.Parameters["@Othersidgenereated"].Size = 10;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_OthersDetails";
                //command.ExecuteNonQuery();
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
                obj.EmegencyId = Convert.ToInt32(command.Parameters["@Othersidgenereated"].Value);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public FamilyHousehold Getemergencycontact(string Emergencyid, string HouseHoldId, string agencyid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@Emergencyid", Emergencyid));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Emergencyinfo";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        obj.EmegencyId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                        obj.Efirstname = Convert.ToString(_dataset.Tables[0].Rows[0]["Firstname"]);
                        obj.Elastname = Convert.ToString(_dataset.Tables[0].Rows[0]["Lastname"]);
                        obj.Emiddlename = Convert.ToString(_dataset.Tables[0].Rows[0]["Middlename"]);
                        //obj.EAddress1 = Convert.ToString(_dataset.Tables[0].Rows[0]["Address"]);
                        //obj.EAddress2 = Convert.ToString(_dataset.Tables[0].Rows[0]["Apartmentno"]);
                        //obj.ECity = Convert.ToString(_dataset.Tables[0].Rows[0]["City"]);
                        //obj.EState = Convert.ToString(_dataset.Tables[0].Rows[0]["State"]);
                        //obj.EZipcode = Convert.ToString(_dataset.Tables[0].Rows[0]["ZipCode"]);
                        obj.EDOB = _dataset.Tables[0].Rows[0]["DOB"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[0].Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                        obj.EGender = Convert.ToString(_dataset.Tables[0].Rows[0]["gender"]);
                        obj.EEmail = Convert.ToString(_dataset.Tables[0].Rows[0]["Email"]);
                        obj.ERelationwithchild = Convert.ToString(_dataset.Tables[0].Rows[0]["Relationwithchild"]);
                        obj.Enotes = Convert.ToString(_dataset.Tables[0].Rows[0]["Notes"]);
                        obj.EFileName = Convert.ToString(_dataset.Tables[0].Rows[0]["FileNameul"]);
                        obj.EFileExtension = Convert.ToString(_dataset.Tables[0].Rows[0]["FileExtension"]);
                        obj.EImagejson = _dataset.Tables[0].Rows[0]["DocumentFile"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[0].Rows[0]["DocumentFile"]);
                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.phone> _phone = new List<FamilyHousehold.phone>();
                        foreach (DataRow row in _dataset.Tables[1].Rows)
                        {
                            FamilyHousehold.phone _phoneadd = new FamilyHousehold.phone();
                            _phoneadd.PhoneId = Convert.ToInt32(row["Id"]);
                            _phoneadd.PhoneType = row["PhoneType"].ToString();
                            _phoneadd.PhoneNo = row["Phoneno"].ToString();
                            _phoneadd.IsPrimary = row["IsPrimaryContact"].ToString() == "" ? false : Convert.ToBoolean(row["IsPrimaryContact"]);
                            _phoneadd.IsSms = row["Sms"].ToString() == "" ? false : Convert.ToBoolean(row["Sms"]);
                            _phoneadd.Notes = row["Notes"].ToString();
                            _phone.Add(_phoneadd);
                        }
                        obj.phoneList = _phone;
                    }

                }
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        public string Deleteemergencycontact(string EmergencyId, string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteemergencycontactinfo";
                command.Parameters.Add(new SqlParameter("@EmergencyId", EmergencyId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string Deletecontact(string phoneId, string HouseHoldId, string EmergencyId, string Agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deletecontact";
                command.Parameters.Add(new SqlParameter("@phoneId", phoneId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@EmergencyId", EmergencyId));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string DeleteParent1(string ParentId, string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteparent1info";
                command.Parameters.Add(new SqlParameter("@ParentId", ParentId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@modifiedBy", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Size = 1;
                command.Parameters["@result"].Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string DeleteParent2(string ParentId, string HouseHoldId)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteparent2info";
                command.Parameters.Add(new SqlParameter("@ParentId", ParentId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string DeleteParentContact(string phoneId, string HouseHoldId, string Parentid, string Agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteParentcontact";
                command.Parameters.Add(new SqlParameter("@phoneId", phoneId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Parentid", Parentid));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public List<CommunityResource> AutoCompleteDoctr(string term, string agencyid, string active = "0")
        {
            List<CommunityResource> DoctorList = new List<CommunityResource>();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "AutoComplete_MedicalCenterList";// "AutoComplete_DoctorList";//Changes by Akansha on 15Dec2016
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DoctorName", term);
                        command.Parameters.AddWithValue("@IsDeleted", active);
                        command.Parameters.AddWithValue("@agencyid", agencyid);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CommunityResource obj = new CommunityResource();
                            obj.CommunityID = Convert.ToInt32(dr["Id"].ToString());
                            obj.Title = Convert.ToString(dr["Title"].ToString());
                            obj.Fname = dr["Fname"].ToString();
                            obj.MedicalCenter = dr["DoctorName"].ToString();//Added by Akansha
                            obj.Lname = dr["Lname"].ToString();
                            obj.CreatedDate = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                            obj.Community = dr["Community"].ToString();
                            // obj.AgencyId = (dr["AgencyId"].ToString());
                            //  obj.TimeZoneID = dr["TimeZone_ID"].ToString();

                            DoctorList.Add(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            return DoctorList;
        }
        public List<CommunityResource> AutoCompleteDentst(string term, string agencyid, string active = "0")
        {
            List<CommunityResource> DentistList = new List<CommunityResource>();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "AutoComplete_DentalCenterList";//AutoComplete_DentistList//Changes by Akansha on 15Dec2016
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DentistName", term);
                        command.Parameters.AddWithValue("@IsDeleted", active);
                        command.Parameters.AddWithValue("@AgencyId", agencyid);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CommunityResource obj = new CommunityResource();
                            obj.CommunityID = Convert.ToInt32(dr["Id"].ToString());
                            obj.Title = Convert.ToString(dr["Title"].ToString());
                            obj.Fname = dr["Fname"].ToString();
                            obj.DentalCenter = dr["DentistName"].ToString();//Added by Akansha
                            obj.Lname = dr["Lname"].ToString();
                            obj.CreatedDate = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                            obj.Community = dr["Community"].ToString();
                            // obj.AgencyId = (dr["AgencyId"].ToString());
                            //  obj.TimeZoneID = dr["TimeZone_ID"].ToString();

                            DentistList.Add(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            return DentistList;
        }
        public List<Zipcodes> Checkaddress(out string Result, string Address, string HouseHoldId, int Zipcode)
        {
            List<Zipcodes> ZipcodesList = new List<Zipcodes>();
            Result = "0";
            try
            {

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_CheckAddress";
                command.Parameters.Add(new SqlParameter("@Address", Address));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Zipcode", Zipcode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                Result = command.Parameters["@result"].Value.ToString();
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            Zipcodes info = new Zipcodes();
                            info.Zipcode = dr["zipcode"].ToString();
                            info.City = dr["City"].ToString();
                            info.State = dr["state"].ToString();
                            info.County = dr["county"].ToString();
                            ZipcodesList.Add(info);
                        }
                    }
                }
                return ZipcodesList;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return ZipcodesList;
            }
            finally
            {
                command.Dispose();

            }
        }
        public string DeleteParentincome(string incomeId, string HouseHoldId, string Parentid, string Agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteParentincome";
                command.Parameters.Add(new SqlParameter("@incomeId", incomeId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Parentid", Parentid));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public FamilyHousehold getpdfimage(string Agencyid, string column, string incomeid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            //obj.income1 = new FamilyHousehold.calculateincome();

            try
            {
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@incomeid", incomeid));
                command.Parameters.Add(new SqlParameter("@column", column));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getincomeimage";
                DataAdapter = new SqlDataAdapter(command);
                //Due to Phone Type
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    obj.EImageByte = (byte[])_dataset.Tables[0].Rows[0]["imagebyte"];
                    obj.EFileExtension = _dataset.Tables[0].Rows[0]["imageExt"].ToString();
                    obj.EFileName = _dataset.Tables[0].Rows[0]["imagename"].ToString();

                }
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        public List<FingerprintsModel.FamilyHousehold.RaceSubCategory> getsubcategory(string Raceid, string Agencyid)
        {
            List<FingerprintsModel.FamilyHousehold.RaceSubCategory> _racelist = new List<FingerprintsModel.FamilyHousehold.RaceSubCategory>();
            if (!String.IsNullOrWhiteSpace(Raceid))
            {
                try
                {
                    command.Parameters.Add(new SqlParameter("@Raceid", Raceid));
                    command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                    command.Connection = Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Sp_getracesubcategory";
                    DataAdapter = new SqlDataAdapter(command);
                    _dataset = new DataSet();
                    DataAdapter.Fill(_dataset);
                    if (_dataset != null && _dataset.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            FingerprintsModel.FamilyHousehold.RaceSubCategory obj = new FingerprintsModel.FamilyHousehold.RaceSubCategory();
                            obj.RaceSubCategoryID = dr["Id"].ToString();
                            obj.Name = dr["Name"].ToString();
                            _racelist.Add(obj);
                        }
                    }
                    DataAdapter.Dispose();
                    command.Dispose();
                }
                catch (Exception ex)
                {
                    clsError.WriteException(ex);
                }
                finally
                {
                    DataAdapter.Dispose();
                    command.Dispose();
                }

            }
            return _racelist;
        }
        public string AddClientAjax(string HouseholdId, string Street, string StreetName, string ZipCode, string City, string State, string County, string Pfirstname, string Plastname, string Cfirstname, string Clastname, string CDOB, string CGender, string userId, string AgencyId, string mode, bool Enrollpregnantmother, string Roleid)
        {
            try
            {
                FamilyHousehold Info = new FamilyHousehold();
                command.Connection = Connection;
                command.CommandText = "SP_addclient";
                command.Parameters.AddWithValue("@mode", mode);
                command.Parameters.AddWithValue("@HouseholdId", HouseholdId);
                command.Parameters.AddWithValue("@Street", Street);
                command.Parameters.AddWithValue("@AgencyId", AgencyId);
                command.Parameters.AddWithValue("@StreetName", StreetName);
                command.Parameters.AddWithValue("@ZipCode", ZipCode);
                command.Parameters.AddWithValue("@City", City);
                command.Parameters.AddWithValue("@State", State);
                command.Parameters.AddWithValue("@Pfirstname", Pfirstname);
                command.Parameters.AddWithValue("@Plastname", Plastname);
                command.Parameters.AddWithValue("@Cfirstname", Cfirstname);
                command.Parameters.AddWithValue("@Clastname", Clastname);
                command.Parameters.AddWithValue("@CDOB", CDOB);
                command.Parameters.AddWithValue("@CGender", CGender);
                command.Parameters.AddWithValue("@County", County);
                command.Parameters.AddWithValue("@Enrollpregnantmother", Enrollpregnantmother);
                command.Parameters.AddWithValue("@CreatedBy", userId);
                command.Parameters.AddWithValue("@Roleid", Roleid);
                command.Parameters.AddWithValue("@result", "").Direction = ParameterDirection.Output;
                command.Parameters["@result"].Size = 10;
                command.CommandType = CommandType.StoredProcedure;
                Connection.Open();

                command.ExecuteNonQuery();
                Connection.Close();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "0";
            }
        }
        public string Getassociatefamily(string Firstname, string Lastname, string Address, string ZipCode, string City, string State, string Agencyid, string mode, string userid)
        {

            string result = "";
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Add(new SqlParameter("@Firstname", Firstname));
                command.Parameters.Add(new SqlParameter("@Lastname", Lastname));
                command.Parameters.Add(new SqlParameter("@Address", Address));
                command.Parameters.Add(new SqlParameter("@ZipCode", ZipCode));
                command.Parameters.Add(new SqlParameter("@City", City));
                command.Parameters.Add(new SqlParameter("@State", State));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.AddWithValue("@result", "").Direction = ParameterDirection.Output;
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getasociatefamily";

                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
                //DataAdapter = new SqlDataAdapter(command);
                //_dataset = new DataSet();
                //DataAdapter.Fill(_dataset);
                //if (_dataset != null && _dataset.Tables.Count > 0)
                //{
                //    foreach (DataRow dr in _dataset.Tables[0].Rows)
                //    {
                //        FingerprintsModel.FamilyHousehold.RaceSubCategory obj = new FingerprintsModel.FamilyHousehold.RaceSubCategory();
                //        obj.RaceSubCategoryID = dr["Id"].ToString();
                //        obj.Name = dr["Name"].ToString();
                //        _racelist.Add(obj);
                //    }
                //}
                //DataAdapter.Dispose();

                command.Dispose();
            }
            catch (Exception ex)
            {
                Connection.Close();
                clsError.WriteException(ex);
            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;
        }
        public List<FamilyHousehold.Applicationnotes> SaveNotes(ref string result, string agencyid, string userid, string HouseHoldId, string Notes, string mode)
        {
            List<FamilyHousehold.Applicationnotes> Applicationnotes = new List<FamilyHousehold.Applicationnotes>();
            try
            {

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_savenotes";
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Notes", Notes));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);

                if (_dataset != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        FamilyHousehold.Applicationnotes _Applicationnotes = new FamilyHousehold.Applicationnotes();
                        _Applicationnotes.Name = dr["name"].ToString();
                        _Applicationnotes.CreatedOn = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                        _Applicationnotes.notes = dr["Notes"].ToString();
                        Applicationnotes.Add(_Applicationnotes);
                    }
                }
                result = command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                command.Dispose();

            }
            return Applicationnotes;
        }
        public string GetPovertyCalculation(string HouseHoldId, string PovertyPercentage, string Totalhousehold)
        {

            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_PovertyPercentage";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@PovertyPercentage", PovertyPercentage));
                command.Parameters.Add(new SqlParameter("@Totalhousehold", Totalhousehold));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.Parameters["@result"].Size = 5;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }

        }
        public FamilyHousehold.Povertymodel SavePovertyCalculation(ref string result, string agencyid, string userid, string HouseHoldId, string Parentid1, string Parentid2, string Percentage1, string Percentage2, string Amount1, string Amount2, string ChildIncome, string PovertyPercentage, string mode, string clientid)
        {
            FamilyHousehold.Povertymodel Povertymodel = new FamilyHousehold.Povertymodel();
            try
            {

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SavePovertyPercentage";
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@clientid", clientid));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Parentid1", Parentid1));
                command.Parameters.Add(new SqlParameter("@Parentid2", Parentid2));
                command.Parameters.Add(new SqlParameter("@Percentage1", Percentage1));
                command.Parameters.Add(new SqlParameter("@Percentage2", Percentage2));
                command.Parameters.Add(new SqlParameter("@Amount1", Amount1));
                command.Parameters.Add(new SqlParameter("@Amount2", Amount2));
                command.Parameters.Add(new SqlParameter("@ChildIncome", ChildIncome));
                command.Parameters.Add(new SqlParameter("@PovertyPercentage", PovertyPercentage));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                //command.Parameters.Add(new SqlParameter("@Totalhousehold", Totalhousehold));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {

                        Povertymodel.Amount1 = dr["Amount1"].ToString();
                        Povertymodel.Amount2 = dr["Amount2"].ToString();
                        Povertymodel.ChildIncome = dr["ChildIncome"].ToString();
                        Povertymodel.Percentage1 = dr["Percentage1"].ToString();
                        Povertymodel.Percentage2 = dr["Percentage2"].ToString();
                        Povertymodel.PovertyCalculated = dr["PovertyCalculated"].ToString();
                        Povertymodel.Totalinhousehold = dr["Totalinhousehold"].ToString();
                    }
                }
                result = command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                command.Dispose();

            }
            return Povertymodel;
        }
        public FamilyHousehold getselectionpoints(string Programid, string agencyid, string clientid, string householdid)
        {
            FamilyHousehold _household = new FamilyHousehold();
            SelectPoints _selectionpoints = new SelectPoints();
            _household._Selectionpoints = _selectionpoints;
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getselectionpoints";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@clientid", clientid));
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            _selectionpoints.SingleParent = Convert.ToInt32(dr["SingleParent"]);
                            _selectionpoints.TwoParent = Convert.ToInt32(dr["Twoparent"]);
                            _selectionpoints.English = Convert.ToInt32(dr["PrimaryLangEnglish"]);
                            _selectionpoints.Other = Convert.ToInt32(dr["PrimaryLangOther"]);
                            _selectionpoints.HomelessYes = Convert.ToInt32(dr["HomelessYes"]);
                            _selectionpoints.HomelessNo = Convert.ToInt32(dr["HomelessNo"]);
                            _selectionpoints.TANF = Convert.ToInt32(dr["TANF"]);
                            _selectionpoints.SSI = Convert.ToInt32(dr["SSI"]);
                            _selectionpoints.SNAP = Convert.ToInt32(dr["SNAP"]);
                            _selectionpoints.WIC = Convert.ToInt32(dr["WIC"]);
                            _selectionpoints.None = Convert.ToInt32(dr["NONE"]);
                            _selectionpoints.Teenager = Convert.ToInt32(dr["G1Teenager"]);
                            _selectionpoints.Age20 = Convert.ToInt32(dr["G1Age20"]);
                            _selectionpoints.Age30over = Convert.ToInt32(dr["G1Age30over"]);
                            _selectionpoints.MilitaryStatusNone = Convert.ToInt32(dr["G1MilitaryStatusNone"]);
                            _selectionpoints.MilitaryStatusActive = Convert.ToInt32(dr["G1MilitaryStatusActive"]);
                            _selectionpoints.MilitaryStatusVeteran = Convert.ToInt32(dr["G1MilitaryStatusVeteran"]);
                            _selectionpoints.CurrentlyWorkYes = Convert.ToInt32(dr["G1CurrentlyWorkYes"]);
                            _selectionpoints.CurrentlyWorkNo = Convert.ToInt32(dr["G1CurrentlyWorkNo"]);
                            _selectionpoints.JobTrainingyes = Convert.ToInt32(dr["G1JobTrainingyes"]);
                            _selectionpoints.JobTrainingno = Convert.ToInt32(dr["G1JobTrainingno"]);
                            _selectionpoints.G2Teenager = Convert.ToInt32(dr["G2Teenager"]);
                            _selectionpoints.G2Age20 = Convert.ToInt32(dr["G2Age20"]);
                            _selectionpoints.G2Age30over = Convert.ToInt32(dr["G2Age30over"]);
                            _selectionpoints.G2MilitaryStatusNone = Convert.ToInt32(dr["G2MilitaryStatusNone"]);
                            _selectionpoints.G2MilitaryStatusActive = Convert.ToInt32(dr["G2MilitaryStatusActive"]);
                            _selectionpoints.G2MilitaryStatusVeteran = Convert.ToInt32(dr["G2MilitaryStatusVeteran"]);
                            _selectionpoints.G2CurrentlyWorkYes = Convert.ToInt32(dr["G2CurrentlyWorkYes"]);
                            _selectionpoints.G2CurrentlyWorkNo = Convert.ToInt32(dr["G2CurrentlyWorkNo"]);
                            _selectionpoints.G2JobTrainingyes = Convert.ToInt32(dr["G2JobTrainingyes"]);
                            _selectionpoints.G2JobTrainingno = Convert.ToInt32(dr["G2JobTrainingno"]);
                            _selectionpoints.MedicalHomeYes = Convert.ToInt32(dr["MedicalHomeYes"]);
                            _selectionpoints.MedicalHomeNo = Convert.ToInt32(dr["MedicalHomeNo"]);
                            _selectionpoints.DentalHomeYes = Convert.ToInt32(dr["DentalHomeYes"]);
                            _selectionpoints.DentalHomeNo = Convert.ToInt32(dr["DentalHomeNo"]);
                            _selectionpoints.InsuranceYes = Convert.ToInt32(dr["InsuranceYes"]);
                            _selectionpoints.InsuranceNo = Convert.ToInt32(dr["InsuranceNo"]);
                            //_selectionpoints.SuspecteddocumentofdisabiltyYes = Convert.ToInt32(dr["SuspecteddocsYes"]);
                            //_selectionpoints.SuspecteddocumentofdisabiltyNo = Convert.ToInt32(dr["SuspecteddocsNo"]);

                            _selectionpoints.SuspecteddisabiltyNo = dr["SuspecteddocsNo"] != DBNull.Value ? Convert.ToInt32(dr["SuspecteddocsNo"]) : 0;
                            _selectionpoints.SuspecteddisabiltyYes = dr["SuspecteddocsYes"] != DBNull.Value ? Convert.ToInt32(dr["SuspecteddocsYes"]) : 0;
                            _selectionpoints.DocumentofdisabiltyNo = dr["DisabilitydocNo"] != DBNull.Value ? Convert.ToInt32(dr["DisabilitydocNo"]) : 0;
                            _selectionpoints.DocumentofdisabiltyYes = dr["DisabilitydocYes"] != DBNull.Value ? Convert.ToInt32(dr["DisabilitydocYes"]) : 0;

                            _selectionpoints.ChildWlfareYes = Convert.ToInt32(dr["ChildWlfareYes"]);
                            _selectionpoints.ChildWlfareNo = Convert.ToInt32(dr["ChildWlfareNo"]);
                            _selectionpoints.FosterChildYes = Convert.ToInt32(dr["FosterChildYes"]);
                            _selectionpoints.FosterChildNo = Convert.ToInt32(dr["FosterChildNo"]);
                            _selectionpoints.DualCustYes = Convert.ToInt32(dr["DualCustYes"]);
                            _selectionpoints.DualCustNo = Convert.ToInt32(dr["DualCustNo"]);
                            _selectionpoints.PMInsuranceYes = Convert.ToInt32(dr["PMInsuranceYes"]);
                            _selectionpoints.PMInsuranceNo = Convert.ToInt32(dr["PMInsuranceNo"]);
                            _selectionpoints.PMMedicalHomeYes = Convert.ToInt32(dr["PMMedicalHomeYes"]);
                            _selectionpoints.PMMedicalHomeNo = Convert.ToInt32(dr["PMMedicalHomeNo"]);
                            _selectionpoints.Trimester1 = Convert.ToInt32(dr["Trimester1"]);
                            _selectionpoints.Trimester2 = Convert.ToInt32(dr["Trimester2"]);
                            _selectionpoints.Trimester3 = Convert.ToInt32(dr["Trimester3"]);
                            _selectionpoints.Age3Months = Convert.ToInt32(dr["ChildAge3months"]);
                            _selectionpoints.Age6Months = Convert.ToInt32(dr["ChildAge6months"]);
                            _selectionpoints.Age1yr = Convert.ToInt32(dr["ChildAge1yr"]);
                            _selectionpoints.Age2yr = Convert.ToInt32(dr["ChildAge2yr"]);
                            _selectionpoints.Age3yr = Convert.ToInt32(dr["ChildAge3yr"]);
                            _selectionpoints.Age4yr = Convert.ToInt32(dr["ChildAge4yr"]);
                            _selectionpoints.Age5yr = Convert.ToInt32(dr["ChildAge5yr"]);
                            _selectionpoints.Age6yr = Convert.ToInt32(dr["ChildAge6yr"]);
                            _selectionpoints.Age6yrorgreater = Convert.ToInt32(dr["ChildAge6yrorgreater"]);
                            _selectionpoints.poverty0to25 = Convert.ToInt32(dr["Poverty0to25"]);
                            _selectionpoints.poverty26to50 = Convert.ToInt32(dr["Poverty26to50"]);
                            _selectionpoints.poverty51to75 = Convert.ToInt32(dr["Poverty51to75"]);
                            _selectionpoints.poverty76to100 = Convert.ToInt32(dr["Poverty76to100"]);
                            _selectionpoints.poverty100to130 = Convert.ToInt32(dr["Poverty100to130"]);
                            _selectionpoints.povertygreater130 = Convert.ToInt32(dr["Povertygreater130"]);
                        }
                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            _household.IsFoster = Convert.ToInt32(dr["FosterChild"]);
                            _household.Inwalfareagency = Convert.ToInt32(dr["WelfareAgency"]);
                            _household.InDualcustody = Convert.ToInt32(dr["DualCustodyChild"]);
                            _household.CParentdisable = Convert.ToInt32(dr["ParentDisable"]);
                            _household.Medicalhome = Convert.ToInt32(dr["Medicalhome"]);
                            _household.CDentalhome = Convert.ToInt32(dr["DentalHome"]);
                            _household.InsuranceOption = dr["PrimaryInsurance"].ToString();
                            _household.Pregnantmotherprimaryinsurance = Convert.ToInt32(dr["PrimaryInsurancepm"]);
                            _household.TrimesterEnrolled = Convert.ToInt32(dr["TrimesterEnrolled"]);
                            _household.CDOB = dr["Dob"].ToString() != "" ? Convert.ToDateTime(dr["Dob"]).ToString("MM/dd/yyyy") : "";
                            _household.Povertypercentage = dr["povertypercentage"].ToString();
                            if (dr["IEP"] != DBNull.Value)
                                _household.IsIEP = Convert.ToBoolean(dr["IEP"].ToString());
                            if (dr["IFSP"] != DBNull.Value)
                                _household.IsIEP = Convert.ToBoolean(dr["IFSP"].ToString());
                            if (dr["IsExpired"] != DBNull.Value)
                                _household.IsExpired = Convert.ToBoolean(dr["IsExpired"].ToString());


                        }
                    }
                    if (_dataset.Tables[2].Rows.Count > 0)
                    {

                        List<SelectPoints.CustomQuestion> CustomQues = new List<SelectPoints.CustomQuestion>();
                        SelectPoints.CustomQuestion _customquestion = null;
                        foreach (DataRow dr in _dataset.Tables[2].Rows)
                        {
                            _customquestion = new SelectPoints.CustomQuestion();
                            _customquestion.CQID = Convert.ToInt32(dr["customquestionid"]);
                            _customquestion.Question = dr["Question"].ToString();
                            _customquestion.QuesYes = dr["QuesYes"].ToString();
                            _customquestion.QuesNo = dr["QuesNo"].ToString();
                            _customquestion.point = dr["point"].ToString();
                            _customquestion.totalcustompoint = dr["totalcustompoint"].ToString();
                            CustomQues.Add(_customquestion);
                        }
                        _household.CustomQues = CustomQues;
                    }

                }


            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                command.Dispose();
            }
            return _household;
        }
        public string Deletepmmother(string ParentId, string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deletpmmotherprogram";
                command.Parameters.Add(new SqlParameter("@ParentId", ParentId));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string SaveSelectionPoint(List<SelectPoints.CustomQuestion> custompoints, string Totalselectionpoint, string Totalcustompoint, string Grandpoint, string clientid, string householdid, string agencyid, string userid)
        {

            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SaveselectionPoint";
                if (custompoints != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("CQID", typeof(int)),
                    new DataColumn("ProgramType",typeof(string)),
                    new DataColumn("QuesYes",typeof(int)),
                    new DataColumn("Question",typeof(string)),
                    new DataColumn("QuesNo",typeof(int)),
                    new DataColumn("point",typeof(int)),
                    });
                    foreach (SelectPoints.CustomQuestion _custompoints in custompoints)
                    {
                        if (_custompoints.CQID != 0)
                        {
                            dt.Rows.Add(_custompoints.CQID, _custompoints.ProgramType, _custompoints.QuesYes, _custompoints.Question, _custompoints.QuesNo, _custompoints.point);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@custompoints", dt));
                }
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@clientid", clientid));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", householdid));
                command.Parameters.Add(new SqlParameter("@Totalselectionpoint", Totalselectionpoint));
                command.Parameters.Add(new SqlParameter("@Totalcustompoint", Totalcustompoint));
                command.Parameters.Add(new SqlParameter("@Grandpoint", Grandpoint));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";

            }
            finally
            {
                command.Dispose();
                Connection.Dispose();
            }

        }
        public List<FingerprintsModel.FamilyHousehold> GetFileCabinet(string tabName, string Agencyid, int houseHoldId)
        {
            List<FingerprintsModel.FamilyHousehold> _list = new List<FingerprintsModel.FamilyHousehold>();
            try
            {
                command.Parameters.Add(new SqlParameter("@tabName", tabName));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@houseHoldId", @houseHoldId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Get_FamilyCabinet";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        FingerprintsModel.FamilyHousehold obj = new FingerprintsModel.FamilyHousehold();
                        obj.FamilyHouseholdID = Convert.ToInt32(dr["ID"]);
                        obj.HFileName = dr["FileName"].ToString();
                        obj.DocumentDesc = dr["Description"].ToString();
                        obj.AmountNo = dr["IncomeAmount"].ToString();
                        obj.CreatedOn = dr["DateEntered"].ToString();
                        _list.Add(obj);
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return _list;
        }
        public FamilyHousehold getpdfimage1(string Agencyid, int rowId, string TabName, string AmountNo)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@rowId", rowId));
                command.Parameters.Add(new SqlParameter("@TabName", TabName));
                command.Parameters.Add(new SqlParameter("@AmountNo", AmountNo));

                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dp_getVerificationdocuments";
                DataAdapter = new SqlDataAdapter(command);
                //Due to Phone Type
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    obj.EImageByte = (byte[])_dataset.Tables[0].Rows[0]["DocumentFile"];
                    obj.HFileName = _dataset.Tables[0].Rows[0]["FileName"].ToString();
                    obj.EFileExtension = _dataset.Tables[0].Rows[0]["FileExtension"].ToString();

                }
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        public List<HrCenterInfo> getagencyid(string agencyid, string roleid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            try
            {

                command.Parameters.Add(new SqlParameter("@Agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_getcenter";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        HrCenterInfo obj = new HrCenterInfo();
                        obj.CenterId = dr["CenterId"].ToString();
                        obj.Name = dr["CenterName"].ToString();
                        centerList.Add(obj);
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public List<UserInfo> GetNurse(string Centerid, string Agencyid, string userid)
        {
            List<UserInfo> _userlist = new List<UserInfo>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Centerid", Centerid));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_getnurse";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        UserInfo obj = new UserInfo();
                        obj.userId = dr["Userid"].ToString();
                        obj.Name = dr["Name"].ToString();
                        _userlist.Add(obj);
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return _userlist;
        }
        public string SaveAcceptanceprocess(string Clientid, string Usernurseid, string householdid, string centerid, string agencyid, string userid, string Programid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_saveacceptanceprocess";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Clientid", Clientid));
                command.Parameters.Add(new SqlParameter("@Usernurseid", Usernurseid));
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                command.Dispose();
            }
            return "0";
        }

        // changes by shambhu 4 march
        public string SaveMultipleAcceptanceprocess(List<string> ClientList, string Usernurseid, string householdid, string centerid, string agencyid, string userid, string Programid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_saveMultipleacceptanceprocess";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Usernurseid", Usernurseid));
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                DataTable dt = new DataTable();
                if (ClientList.Count > 0)
                {
                    dt.Columns.AddRange(new DataColumn[2] {
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("ClientId", typeof(string))
                    });
                }
                int i = 0;
                foreach (string str in ClientList)
                {
                    i++;
                    dt.Rows.Add(i, str);
                }
                command.Parameters.Add(new SqlParameter("@Clientid", dt));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                command.Dispose();
            }
            return "0";
        }


        public List<HrCenterInfo> Getcenters(ref int yakkrcount, ref int appointment, string Agencyid, string userid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            List<HrCenterInfo> centerList1 = new List<HrCenterInfo>();

            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getuserwaitinglist";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            HrCenterInfo info = new HrCenterInfo();
                            info.CenterId = EncryptDecrypt.Encrypt64(dr["center"].ToString());
                            info.Name = dr["centername"].ToString();
                            info.SeatsAvailable = dr["AvailSeats"].ToString();
                            info.option1 = dr["1"].ToString();
                            info.option2 = dr["2"].ToString();
                            info.option3 = dr["3"].ToString();
                            info.Routecode100 = dr["100"].ToString();
                            info.Routecode101 = dr["101"].ToString();
                            info.Routecode102 = dr["102"].ToString();
                            info.Attendance = dr["Attendance"].ToString();
                            info.TotalWaitingList = dr["TotalWaitinglist"].ToString();
                            yakkrcount = Convert.ToInt32(dr["yakkrcount"]);
                            appointment = Convert.ToInt32(dr["Appointment"]);
                            centerList.Add(info);
                        }
                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            HrCenterInfo info = new HrCenterInfo();
                            info.CenterId = dr["center"].ToString();
                            info.Name = dr["centername"].ToString();
                            info.Address = dr["address"].ToString();
                            info.Zip = dr["Zip"].ToString();
                            info.SeatsAvailable = dr["AvailSeats"].ToString();
                            centerList1.Add(info);
                        }
                        if (centerList.Count > 0 && centerList1.Count > 0)
                        {
                            centerList.FirstOrDefault().AllCentersList = centerList1;
                        }
                    }



                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public List<HrCenterInfo> GetApplicationApprovalDashboard(ref int yakkrcount, ref DataTable Screeninglist, string Agencyid, string userid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            Screeninglist = new DataTable();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getAppicationdashboard";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        HrCenterInfo info = new HrCenterInfo();
                        info.CenterId = dr["center"].ToString();
                        info.Name = dr["centername"].ToString();
                        info.Routecode100 = dr["Pending"].ToString();
                        info.Routecode101 = dr["Accepted"].ToString();
                        info.Routecode102 = dr["Rejected"].ToString();
                        centerList.Add(info);
                    }

                }
                if (_dataset.Tables[1] != null && _dataset.Tables[1].Rows.Count > 0)
                {
                    yakkrcount = Convert.ToInt32(_dataset.Tables[1].Rows[0]["YakkrCountPending"]);
                }
                if (_dataset.Tables[2] != null && _dataset.Tables[2].Rows.Count > 0)
                {
                    Screeninglist = _dataset.Tables[2];
                }

                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public List<HrCenterInfo> GetcentersFSW(ref int yakkrcount, string Agencyid, string userid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getcenterslistFsw";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        HrCenterInfo info = new HrCenterInfo();
                        info.CenterId = dr["center"].ToString();
                        info.Name = dr["centername"].ToString();
                        info.yakkrcount = dr["yakkrcount"].ToString();
                        //  yakkrcount = Convert.ToInt32(dr["yakkrcount"]);
                        centerList.Add(info);
                    }

                }
                if (_dataset.Tables[1] != null && _dataset.Tables[1].Rows.Count > 0)
                {
                    yakkrcount = Convert.ToInt32(_dataset.Tables[1].Rows[0]["YakkrCountPending"]);

                    //obj.RejectDesc = Convert.ToString(_dataset.Tables[8].Rows[0]["Notes"]);
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public List<Fswuserapproval> Getallclients(string centerid, string Agencyid, string userid)
        {
            List<Fswuserapproval> FswuserapprovalList = new List<Fswuserapproval>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getcentersclientfsw";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        Fswuserapproval info = new Fswuserapproval();
                        info.ClientId = dr["ClientId"].ToString();
                        info.ClientName = dr["Name"].ToString();
                        info.Date = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                        info.CenterName = dr["centername"].ToString();
                        info.StaffName = dr["staffname"].ToString();
                        info.routecode = dr["RouteCode"].ToString();
                        info.Status = dr["Status"].ToString();
                        info.Yakkrid = dr["yakkrid"].ToString();
                        info.CenterId = centerid;
                        FswuserapprovalList.Add(info);
                    }

                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return FswuserapprovalList;
        }
        public List<HrCenterInfo> Getallcenter(string mode, string RoleId, string Agencyid, string userid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            try
            {
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getallcnterforfsw";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        HrCenterInfo info = new HrCenterInfo();
                        info.CenterId = dr["center"].ToString();
                        info.Name = dr["centername"].ToString();
                        info.Address = dr["address"].ToString();
                        info.Zip = dr["Zip"].ToString();
                        info.SeatsAvailable = dr["AvailSeats"].ToString();
                        centerList.Add(info);
                    }

                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public string SaveWaitingclient(List<Waitinginfo> waitinglist, string agencyid, string userid)
        {

            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SavesWaitinglist";
                if (waitinglist != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("CenterId", typeof(int)),
                    new DataColumn("Clientid",typeof(int)),
                    new DataColumn("Householid",typeof(int)),
                    new DataColumn("Programid",typeof(int)),
                    new DataColumn("Options",typeof(int)),
                    new DataColumn("Notes",typeof(string)),
                    });
                    foreach (Waitinginfo _waiting in waitinglist)
                    {
                        if (_waiting.CenterId != "0")
                        {
                            dt.Rows.Add(_waiting.CenterId, _waiting.Clientid, _waiting.Householid, _waiting.Programid, _waiting.Options, _waiting.Notes);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@Waitinglist", dt));
                }
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";

            }
            finally
            {
                command.Dispose();
                Connection.Dispose();
            }

        }
        public List<FamilyHousehold> AutoCompletefamilyList(string term, string agencyid, string userid, string active = "0")
        {
            List<FamilyHousehold> _householdlist = new List<FamilyHousehold>();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "AutoComplete_familyList";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", term);
                        command.Parameters.AddWithValue("@Active", active);
                        command.Parameters.AddWithValue("@agencyid", agencyid);
                        command.Parameters.AddWithValue("@userid", userid);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            FamilyHousehold obj = new FamilyHousehold();
                            obj.HouseholdId = Convert.ToInt32(dr["HouseholdID"].ToString());
                            obj.Pfirstname = dr["name"].ToString();
                            obj.Encrypthouseholid = EncryptDecrypt.Encrypt64(dr["HouseholdID"].ToString());
                            _householdlist.Add(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            return _householdlist;
        }
        public List<Fswuserapproval> Getallyakkrclients(int centerid, int option, string Agencyid, string userid)
        {
            List<Fswuserapproval> FswuserapprovalList = new List<Fswuserapproval>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Parameters.Add(new SqlParameter("@option", option));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_gettyakkrclientfsw";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        Fswuserapproval info = new Fswuserapproval();
                        info.ClientId = dr["ClientId"].ToString();
                        info.ClientName = dr["Name"].ToString();
                        info.Date = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                        info.CenterName = dr["centername"].ToString();
                        info.StaffName = dr["staffname"].ToString();
                        info.routecode = dr["RouteCode"].ToString();
                        info.Status = dr["status"].ToString();
                        FswuserapprovalList.Add(info);
                    }

                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return FswuserapprovalList;
        }
        public List<HrCenterInfo> Getyakkraccepted(string Agencyid, string userid)
        {
            List<HrCenterInfo> centerList = new List<HrCenterInfo>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getcenterslistFsw";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        HrCenterInfo info = new HrCenterInfo();
                        info.CenterId = dr["center"].ToString();
                        info.Name = dr["centername"].ToString();
                        info.yakkrcount = dr["yakkrcount"].ToString();
                        centerList.Add(info);
                    }

                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return centerList;
        }
        public void GetallHouseholdall(FamilyHousehold obj, DataSet _dataset)
        {
            if (_dataset != null && _dataset.Tables.Count > 0)
            {
                if (_dataset.Tables[0].Rows.Count > 0)
                {
                    obj.HouseholdId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                    obj.FamilyHouseholdID = Convert.ToInt32(_dataset.Tables[0].Rows[0]["familyhouseholdid"]);
                    obj.Street = Convert.ToString(_dataset.Tables[0].Rows[0]["Street"]);
                    obj.StreetName = Convert.ToString(_dataset.Tables[0].Rows[0]["StreetName"]);
                    obj.City = Convert.ToString(_dataset.Tables[0].Rows[0]["City"]);
                    obj.ZipCode = Convert.ToString(_dataset.Tables[0].Rows[0]["ZipCode"]);
                    obj.State = Convert.ToString(_dataset.Tables[0].Rows[0]["State"]);
                    obj.County = Convert.ToString(_dataset.Tables[0].Rows[0]["County"]);
                    obj.HFileName = Convert.ToString(_dataset.Tables[0].Rows[0]["FileName"]);
                    obj.HFileExtension = Convert.ToString(_dataset.Tables[0].Rows[0]["FileExtension"]);
                    if (_dataset.Tables[0].Rows[0]["Hpaper"].ToString() != "")
                        obj.AdresssverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Hpaper"]);
                    if (_dataset.Tables[0].Rows[0]["RentType"].ToString() != "")
                        obj.RentType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["RentType"]);
                    if (_dataset.Tables[0].Rows[0]["TANF"].ToString() != "")
                        obj.TANF = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TANF"]);
                    if (_dataset.Tables[0].Rows[0]["HouseType"].ToString() != "")
                        obj.HomeType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["HouseType"]);
                    if (_dataset.Tables[0].Rows[0]["FamilyType"].ToString() != "")
                        obj.FamilyType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FamilyType"]);
                    if (_dataset.Tables[0].Rows[0]["SSI"].ToString() != "")
                        obj.SSI = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SSI"]);
                    if (_dataset.Tables[0].Rows[0]["WIC"].ToString() != "")
                        obj.WIC = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WIC"]);
                    if (_dataset.Tables[0].Rows[0]["SNAP"].ToString() != "")
                        obj.SNAP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SNAP"]);
                    if (_dataset.Tables[0].Rows[0]["NONE"].ToString() != "")
                        obj.NONE = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NONE"]);
                    if (_dataset.Tables[0].Rows[0]["Interpretor"].ToString() != "")
                        obj.Interpretor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Interpretor"]);
                    if (_dataset.Tables[0].Rows[0]["ParentRelatioship"].ToString() != "")
                        obj.ParentRelatioship = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentRelatioship"]);
                    if (_dataset.Tables[0].Rows[0]["OtherRelationship"].ToString() != "")
                        obj.ParentRelatioshipOther = _dataset.Tables[0].Rows[0]["OtherRelationship"].ToString();
                    if (_dataset.Tables[0].Rows[0]["OtherDesc"].ToString() != "")
                        obj.OtherLanguageDetail = _dataset.Tables[0].Rows[0]["OtherDesc"].ToString();
                    obj.PrimaryLanguauge = Convert.ToString(_dataset.Tables[0].Rows[0]["PrimaryLanguauge"]);
                    obj.Married = _dataset.Tables[0].Rows[0]["Married"].ToString();
                    obj.docstorage = Convert.ToInt32(_dataset.Tables[0].Rows[0]["docsStorage"]);
                    //Parent1 details
                    obj.ExistPmprogram = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Pmprogram"]);
                    obj.Pfirstname = _dataset.Tables[0].Rows[0]["Pfirstname"].ToString();
                    obj.Plastname = _dataset.Tables[0].Rows[0]["PLastName"].ToString();
                    obj.Pmidddlename = _dataset.Tables[0].Rows[0]["PMiddlename"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PDOB"].ToString() != "")
                        obj.PDOB = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["PDOB"]).ToString("MM/dd/yyyy");
                    obj.PGender = _dataset.Tables[0].Rows[0]["PGender"].ToString();
                    obj.Pemailid = _dataset.Tables[0].Rows[0]["PEmailid"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PMilitaryStatus"].ToString() != "")
                        obj.PMilitaryStatus = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMilitaryStatus"]);
                    obj.PEnrollment = _dataset.Tables[0].Rows[0]["PEnrollment"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PDegreeEarned"].ToString() != "")
                        obj.PDegreeEarned = Convert.ToString(_dataset.Tables[0].Rows[0]["PDegreeEarned"]);
                    obj.Pnotesother = _dataset.Tables[0].Rows[0]["PNotes"].ToString();
                    obj.PRole = _dataset.Tables[0].Rows[0]["ParentRole"].ToString();
                    if (_dataset.Tables[0].Rows[0]["PCurrentlyWorking"].ToString() != "")
                        obj.PCurrentlyWorking = _dataset.Tables[0].Rows[0]["PCurrentlyWorking"].ToString();
                    if (_dataset.Tables[0].Rows[0]["ParentId"].ToString() != "")
                        obj.ParentID = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentId"]);
                    if (_dataset.Tables[0].Rows[0]["IsPreg"].ToString() != "")
                        obj.PQuestion = _dataset.Tables[0].Rows[0]["IsPreg"].ToString();
                    if (_dataset.Tables[0].Rows[0]["EnrollforPregnant"].ToString() != "")
                        obj.Pregnantmotherenrolled = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["EnrollforPregnant"]);
                    if (_dataset.Tables[0].Rows[0]["motherinsurance"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurance = Convert.ToInt32(_dataset.Tables[0].Rows[0]["motherinsurance"]);
                    if (_dataset.Tables[0].Rows[0]["insurancenotemother"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurancenotes = _dataset.Tables[0].Rows[0]["insurancenotemother"].ToString();
                    if (_dataset.Tables[0].Rows[0]["TrimesterEnrolled"].ToString() != "")
                        obj.TrimesterEnrolled = Convert.ToInt32(_dataset.Tables[0].Rows[0]["TrimesterEnrolled"]);
                    if (_dataset.Tables[0].Rows[0]["PMProblmID"].ToString() != "")
                        obj.PMProblem = Convert.ToInt32(_dataset.Tables[0].Rows[0]["PMProblmID"]);
                    try
                    {
                        obj.ParentSSN1 = _dataset.Tables[0].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[0]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.ParentSSN1 = _dataset.Tables[0].Rows[0]["SSN"].ToString();
                    }
                    if (_dataset.Tables[0].Rows[0]["Medicalhome"].ToString() != "")
                        obj.MedicalhomePreg1 = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Medicalhome"]);
                    if (_dataset.Tables[0].Rows[0]["Doctorvalue"].ToString() != "")
                        obj.P1Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Doctorvalue"]);
                    obj.CDoctorP1 = _dataset.Tables[0].Rows[0]["doctorname"].ToString();
                    if (_dataset.Tables[0].Rows[0]["NoEmail"].ToString() != "")
                        obj.Noemail1 = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoEmail"]);
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.calculateincome> IncomeList = new List<FamilyHousehold.calculateincome>();
                        DataRow[] dr = _dataset.Tables[1].Select("ParentId=" + Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentId"]));
                        foreach (DataRow dr1 in dr)
                        {
                            FamilyHousehold.calculateincome _income = new FamilyHousehold.calculateincome();
                            _income.newincomeid = Convert.ToInt32(dr1["IncomeId"]);
                            if (dr1["Income"].ToString() != "")
                                _income.Income = Convert.ToInt32(dr1["Income"]);
                            if (dr1["IncomeSource1"].ToString() != "")
                                if (dr1["IncomeAmt1"].ToString() != "")
                                    _income.AmountVocher1 = Convert.ToDecimal(dr1["IncomeAmt1"]);
                            if (dr1["IncomeAmt2"].ToString() != "")
                                _income.AmountVocher2 = Convert.ToDecimal(dr1["IncomeAmt2"]);
                            if (dr1["IncomeAmt3"].ToString() != "")
                                _income.AmountVocher3 = Convert.ToDecimal(dr1["IncomeAmt3"]);
                            if (dr1["IncomeAmt4"].ToString() != "")
                                _income.AmountVocher4 = Convert.ToDecimal(dr1["IncomeAmt4"]);
                            if (dr1["PayFrequency"].ToString() != "")
                                _income.Payfrequency = Convert.ToInt32(dr1["PayFrequency"]);
                            if (dr1["WorkingPeriod"].ToString() != "")
                                _income.Working = Convert.ToInt32(dr1["WorkingPeriod"]);
                            if (dr1["TotalIncome"].ToString() != "")
                                _income.IncomeCalculated = Convert.ToDecimal(dr1["TotalIncome"]);
                            if (dr1["IncomePaper1"].ToString() != "")
                                _income.incomePaper1 = Convert.ToBoolean(dr1["IncomePaper1"]);
                            if (dr1["IncomePaper2"].ToString() != "")
                                _income.incomePaper2 = Convert.ToBoolean(dr1["IncomePaper2"]);
                            if (dr1["IncomePaper3"].ToString() != "")
                                _income.incomePaper3 = Convert.ToBoolean(dr1["IncomePaper3"]);
                            if (dr1["IncomePaper4"].ToString() != "")
                                _income.incomePaper4 = Convert.ToBoolean(dr1["IncomePaper4"]);
                            if (dr1["NoincomePaper"].ToString() != "")
                                _income.noincomepaper = Convert.ToBoolean(dr1["NoincomePaper"]);
                            IncomeList.Add(_income);
                        }
                        obj.Income1 = IncomeList;
                    }
                }

                //Parent2 detail
                if (_dataset.Tables[0].Rows.Count == 2)
                {
                    obj.P1firstname = _dataset.Tables[0].Rows[1]["Pfirstname"].ToString();
                    obj.P1lastname = _dataset.Tables[0].Rows[1]["PLastName"].ToString();
                    obj.P1midddlename = _dataset.Tables[0].Rows[1]["PMiddlename"].ToString();
                    obj.P1DOB = _dataset.Tables[0].Rows[1]["PDOB"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[0].Rows[1]["PDOB"]).ToString("MM/dd/yyyy");
                    if (_dataset.Tables[0].Rows[1]["PGender"].ToString() != "")
                        obj.P1Gender = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PGender"]);
                    obj.P1emailid = _dataset.Tables[0].Rows[1]["PEmailid"].ToString();
                    if (_dataset.Tables[0].Rows[1]["PMilitaryStatus"].ToString() != "")
                        obj.P1MilitaryStatus = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMilitaryStatus"]);
                    if (_dataset.Tables[0].Rows[1]["PEnrollment"].ToString() != "")
                        obj.P1Enrollment = Convert.ToString(_dataset.Tables[0].Rows[1]["PEnrollment"]);
                    if (_dataset.Tables[0].Rows[1]["PDegreeEarned"].ToString() != "")
                        obj.P1DegreeEarned = Convert.ToString(_dataset.Tables[0].Rows[1]["PDegreeEarned"]);
                    if (_dataset.Tables[0].Rows[1]["PCurrentlyWorking"].ToString() != "")
                        obj.P1CurrentlyWorking = _dataset.Tables[0].Rows[1]["PCurrentlyWorking"].ToString();
                    obj.P1Role = _dataset.Tables[0].Rows[1]["ParentRole"].ToString();
                    obj.P1notesother = _dataset.Tables[0].Rows[1]["PNotes"].ToString();
                    if (_dataset.Tables[0].Rows[1]["ParentId"].ToString() != "")
                        obj.ParentID1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["ParentId"]);
                    if (_dataset.Tables[0].Rows[1]["IsPreg"].ToString() != "")
                        obj.P1Question = _dataset.Tables[0].Rows[1]["IsPreg"].ToString();
                    if (_dataset.Tables[0].Rows[1]["EnrollforPregnant"].ToString() != "")
                        obj.PregnantmotherenrolledP1 = Convert.ToBoolean(_dataset.Tables[0].Rows[1]["EnrollforPregnant"]);
                    if (_dataset.Tables[0].Rows[1]["motherinsurance"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurance1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["motherinsurance"]);
                    if (_dataset.Tables[0].Rows[1]["insurancenotemother"].ToString() != "")
                        obj.Pregnantmotherprimaryinsurancenotes1 = _dataset.Tables[0].Rows[1]["insurancenotemother"].ToString();
                    if (_dataset.Tables[0].Rows[1]["TrimesterEnrolled"].ToString() != "")
                        obj.TrimesterEnrolled1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["TrimesterEnrolled"]);
                    if (_dataset.Tables[0].Rows[1]["PMProblmID"].ToString() != "")
                        obj.PMProblem1 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["PMProblmID"]);
                    try
                    {
                        obj.ParentSSN2 = _dataset.Tables[0].Rows[1]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[1]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.ParentSSN2 = _dataset.Tables[0].Rows[1]["SSN"].ToString();
                    }
                    if (_dataset.Tables[0].Rows[1]["Medicalhome"].ToString() != "")
                        obj.MedicalhomePreg2 = Convert.ToInt32(_dataset.Tables[0].Rows[1]["Medicalhome"]);
                    if (_dataset.Tables[0].Rows[1]["Doctorvalue"].ToString() != "")
                        obj.P2Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[1]["Doctorvalue"]);
                    obj.CDoctorP2 = _dataset.Tables[0].Rows[1]["doctorname"].ToString();
                    if (_dataset.Tables[0].Rows[1]["NoEmail"].ToString() != "")
                        obj.Noemail2 = Convert.ToBoolean(_dataset.Tables[0].Rows[1]["NoEmail"]);
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.calculateincome1> IncomeList = new List<FamilyHousehold.calculateincome1>();
                        DataRow[] dr = _dataset.Tables[1].Select("ParentId=" + Convert.ToInt32(_dataset.Tables[0].Rows[1]["ParentId"]));
                        foreach (DataRow dr1 in dr)
                        {
                            FamilyHousehold.calculateincome1 _income = new FamilyHousehold.calculateincome1();
                            _income.IncomeID = Convert.ToInt32(dr1["IncomeId"]);
                            if (dr1["Income"].ToString() != "")
                                _income.Income = Convert.ToInt32(dr1["Income"]);
                            if (dr1["IncomeSource1"].ToString() != "")
                                _income.IncomeSource1 = dr1["IncomeSource1"].ToString();
                            if (dr1["IncomeAmt1"].ToString() != "")
                                _income.AmountVocher1 = Convert.ToDecimal(dr1["IncomeAmt1"]);
                            if (dr1["IncomeAmt2"].ToString() != "")
                                _income.AmountVocher2 = Convert.ToDecimal(dr1["IncomeAmt2"]);
                            if (dr1["IncomeAmt3"].ToString() != "")
                                _income.AmountVocher3 = Convert.ToDecimal(dr1["IncomeAmt3"]);
                            if (dr1["IncomeAmt4"].ToString() != "")
                                _income.AmountVocher4 = Convert.ToDecimal(dr1["IncomeAmt4"]);
                            if (dr1["PayFrequency"].ToString() != "")
                                _income.Payfrequency = Convert.ToInt32(dr1["PayFrequency"]);
                            if (dr1["WorkingPeriod"].ToString() != "")
                                _income.Working = Convert.ToInt32(dr1["WorkingPeriod"]);
                            if (dr1["TotalIncome"].ToString() != "")
                                _income.IncomeCalculated = Convert.ToDecimal(dr1["TotalIncome"]);

                            if (dr1["IncomePaper1"].ToString() != "")
                                _income.incomePaper1 = Convert.ToBoolean(dr1["IncomePaper1"]);
                            if (dr1["IncomePaper2"].ToString() != "")
                                _income.incomePaper2 = Convert.ToBoolean(dr1["IncomePaper2"]);
                            if (dr1["IncomePaper3"].ToString() != "")
                                _income.incomePaper3 = Convert.ToBoolean(dr1["IncomePaper3"]);
                            if (dr1["IncomePaper4"].ToString() != "")
                                _income.incomePaper4 = Convert.ToBoolean(dr1["IncomePaper4"]);
                            if (dr1["NoincomePaper"].ToString() != "")
                                _income.noincomepaper = Convert.ToBoolean(dr1["NoincomePaper"]);
                            IncomeList.Add(_income);
                        }
                        obj.Income2 = IncomeList;
                    }
                }
                if (_dataset.Tables[2].Rows.Count > 0)
                {
                    obj.ChildId = Convert.ToInt32(_dataset.Tables[2].Rows[0]["ID"]);
                    obj.Cfirstname = _dataset.Tables[2].Rows[0]["Firstname"].ToString();
                    obj.Cmiddlename = _dataset.Tables[2].Rows[0]["Middlename"].ToString();
                    obj.Clastname = _dataset.Tables[2].Rows[0]["Lastname"].ToString();
                    if (_dataset.Tables[2].Rows[0]["DOB"].ToString() != "")
                        obj.CDOB = Convert.ToDateTime(_dataset.Tables[2].Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                    obj.DOBverifiedBy = _dataset.Tables[2].Rows[0]["Dobverifiedby"].ToString();
                    try
                    {
                        obj.CSSN = _dataset.Tables[2].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[2].Rows[0]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.CSSN = _dataset.Tables[2].Rows[0]["SSN"].ToString();
                    }
                    obj.CProgramType = _dataset.Tables[2].Rows[0]["Programtype"].ToString();
                    obj.CGender = _dataset.Tables[2].Rows[0]["Gender"].ToString();
                    obj.CRace = _dataset.Tables[2].Rows[0]["RaceID"].ToString();
                    obj.CRaceSubCategory = _dataset.Tables[2].Rows[0]["RaceSubCategoryID"].ToString();
                    if (_dataset.Tables[2].Rows[0]["ImmunizationServiceType"].ToString() != "")
                        obj.ImmunizationService = Convert.ToInt32(_dataset.Tables[2].Rows[0]["ImmunizationServiceType"]);
                    if (_dataset.Tables[2].Rows[0]["MedicalService"].ToString() != "")
                        obj.MedicalService = Convert.ToInt32(_dataset.Tables[2].Rows[0]["MedicalService"]);
                    if (_dataset.Tables[2].Rows[0]["Medicalhome"].ToString() != "")
                        obj.Medicalhome = Convert.ToInt32(_dataset.Tables[2].Rows[0]["Medicalhome"]);
                    if (_dataset.Tables[2].Rows[0]["ParentDisable"].ToString() != "")
                        obj.CParentdisable = Convert.ToInt32(_dataset.Tables[2].Rows[0]["ParentDisable"]);
                    if (_dataset.Tables[2].Rows[0]["BMIStatus"].ToString() != "")
                        obj.BMIStatus = Convert.ToInt32(_dataset.Tables[2].Rows[0]["BMIStatus"]);
                    if (_dataset.Tables[2].Rows[0]["DentalHome"].ToString() != "")
                        obj.CDentalhome = Convert.ToInt32(_dataset.Tables[2].Rows[0]["DentalHome"]);
                    if (_dataset.Tables[2].Rows[0]["Ethnicity"].ToString() != "")
                        obj.CEthnicity = Convert.ToInt32(_dataset.Tables[2].Rows[0]["Ethnicity"]);
                    obj.CFileName = _dataset.Tables[2].Rows[0]["FileNameul"].ToString();
                    obj.CFileExtension = _dataset.Tables[2].Rows[0]["FileExtension"].ToString();
                    obj.DobFileName = _dataset.Tables[2].Rows[0]["Dobfilename"].ToString();
                    obj.CDoctor = _dataset.Tables[2].Rows[0]["doctorname"].ToString();
                    obj.CDentist = _dataset.Tables[2].Rows[0]["dentistname"].ToString();
                    if (_dataset.Tables[2].Rows[0]["Doctorvalue"].ToString() != "")
                        obj.Doctor = Convert.ToInt32(_dataset.Tables[2].Rows[0]["Doctorvalue"]);
                    if (_dataset.Tables[2].Rows[0]["Dentistvalue"].ToString() != "")
                        obj.Dentist = Convert.ToInt32(_dataset.Tables[2].Rows[0]["Dentistvalue"]);
                    if (_dataset.Tables[2].Rows[0]["SchoolDistrict"].ToString() != "")
                        obj.SchoolDistrict = Convert.ToInt32(_dataset.Tables[2].Rows[0]["SchoolDistrict"]);
                    if (_dataset.Tables[2].Rows[0]["DobPaper"].ToString() != "")
                        obj.DobverificationinPaper = Convert.ToBoolean(_dataset.Tables[2].Rows[0]["DobPaper"]);
                    if (_dataset.Tables[2].Rows[0]["FosterChild"].ToString() != "")
                        obj.IsFoster = Convert.ToInt32(_dataset.Tables[2].Rows[0]["FosterChild"]);
                    if (_dataset.Tables[2].Rows[0]["WelfareAgency"].ToString() != "")
                        obj.Inwalfareagency = Convert.ToInt32(_dataset.Tables[2].Rows[0]["WelfareAgency"]);
                    if (_dataset.Tables[2].Rows[0]["DualCustodyChild"].ToString() != "")
                        obj.InDualcustody = Convert.ToInt32(_dataset.Tables[2].Rows[0]["DualCustodyChild"]);
                    if (_dataset.Tables[2].Rows[0]["PrimaryInsurance"].ToString() != "")
                        obj.InsuranceOption = _dataset.Tables[2].Rows[0]["PrimaryInsurance"].ToString();
                    if (_dataset.Tables[2].Rows[0]["InsuranceNotes"].ToString() != "")
                        obj.MedicalNote = _dataset.Tables[2].Rows[0]["InsuranceNotes"].ToString();
                    if (_dataset.Tables[2].Rows[0]["ImmunizationinPaper"].ToString() != "")
                        obj.ImmunizationinPaper = Convert.ToBoolean(_dataset.Tables[2].Rows[0]["ImmunizationinPaper"]);
                    obj.ImmunizationFileName = _dataset.Tables[2].Rows[0]["ImmunizationFileName"].ToString();
                    obj.Raceother = _dataset.Tables[2].Rows[0]["OtherRace"].ToString();
                    obj.CTransport = Convert.ToBoolean(_dataset.Tables[2].Rows[0]["ChildTransport"].ToString());
                    if (!string.IsNullOrEmpty((_dataset.Tables[2].Rows[0]["TransportNeeded"]).ToString()))
                    {
                        obj.CTransportNeeded = Convert.ToBoolean(_dataset.Tables[2].Rows[0]["TransportNeeded"]);
                    }
                    else
                    {
                        obj.CTransportNeeded = false;

                    }
                }
                //if (_dataset.Tables[3].Rows.Count > 0)
                //{
                //    List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                //    FamilyHousehold.ImmunizationRecord obj1;
                //    foreach (DataRow dr in _dataset.Tables[3].Rows)
                //    {
                //        obj1 = new FamilyHousehold.ImmunizationRecord();
                //        obj1.ImmunizationId = Convert.ToInt32(dr["Immunization_ID"]);
                //        obj1.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationmasterid"]);
                //        obj1.Dose = dr["Dose"].ToString();
                //        if (dr["Dose1"].ToString() != "")
                //            obj1.Dose1 = Convert.ToDateTime(dr["Dose1"]).ToString("MM/dd/yyyy");
                //        else
                //            obj1.Dose1 = dr["Dose1"].ToString();
                //        if (dr["Dose2"].ToString() != "")
                //            obj1.Dose2 = Convert.ToDateTime(dr["Dose2"]).ToString("MM/dd/yyyy");
                //        else
                //            obj1.Dose2 = dr["Dose2"].ToString();
                //        if (dr["Dose3"].ToString() != "")
                //            obj1.Dose3 = Convert.ToDateTime(dr["Dose3"]).ToString("MM/dd/yyyy");
                //        else
                //            obj1.Dose3 = dr["Dose3"].ToString();
                //        if (dr["Dose4"].ToString() != "")
                //            obj1.Dose4 = Convert.ToDateTime(dr["Dose4"]).ToString("MM/dd/yyyy");
                //        else
                //            obj1.Dose4 = dr["Dose4"].ToString();
                //        if (dr["Dose5"].ToString() != "")
                //            obj1.Dose5 = Convert.ToDateTime(dr["Dose5"]).ToString("MM/dd/yyyy");
                //        else
                //            obj1.Dose5 = dr["Dose5"].ToString();
                //        if (dr["Exempt1"].ToString() != "")
                //            obj1.Exempt1 = Convert.ToBoolean(dr["Exempt1"]);
                //        if (dr["Exempt2"].ToString() != "")
                //            obj1.Exempt2 = Convert.ToBoolean(dr["Exempt2"]);
                //        if (dr["Exempt3"].ToString() != "")
                //            obj1.Exempt3 = Convert.ToBoolean(dr["Exempt3"]);
                //        if (dr["Exempt4"].ToString() != "")
                //            obj1.Exempt4 = Convert.ToBoolean(dr["Exempt4"]);
                //        if (dr["Exempt5"].ToString() != "")
                //            obj1.Exempt5 = Convert.ToBoolean(dr["Exempt5"]);
                //        if (dr["Preemptive1"].ToString() != "")
                //            obj1.Preempt1 = Convert.ToBoolean(dr["Preemptive1"]);
                //        if (dr["Preemptive2"].ToString() != "")
                //            obj1.Preempt2 = Convert.ToBoolean(dr["Preemptive2"]);
                //        if (dr["Preemptive3"].ToString() != "")
                //            obj1.Preempt3 = Convert.ToBoolean(dr["Preemptive3"]);
                //        if (dr["Preemptive4"].ToString() != "")
                //            obj1.Preempt4 = Convert.ToBoolean(dr["Preemptive4"]);
                //        if (dr["Preemptive5"].ToString() != "")
                //            obj1.Preempt5 = Convert.ToBoolean(dr["Preemptive5"]);
                //        ImmunizationRecords.Add(obj1);
                //        obj1 = null;
                //    }
                //    obj.ImmunizationRecords = ImmunizationRecords;
                //}
                if (_dataset.Tables[4].Rows.Count > 0)
                {
                    List<FamilyHousehold.Programdetail> ProgramdetailRecords = new List<FamilyHousehold.Programdetail>();
                    FamilyHousehold.Programdetail obj1;
                    foreach (DataRow dr in _dataset.Tables[4].Rows)
                    {
                        obj1 = new FamilyHousehold.Programdetail();
                        obj1.Id = Convert.ToInt32(dr["programid"]);
                        obj1.ReferenceId = dr["ReferenceId"].ToString();
                        ProgramdetailRecords.Add(obj1);
                    }
                    obj.AvailableProgram = ProgramdetailRecords;
                }
                //if (_dataset.Tables[5].Rows.Count > 0)
                //{
                //    Screening _Screening = new Screening();
                //    foreach (DataRow dr in _dataset.Tables[5].Rows)
                //    {
                //        _Screening.F001physicalDate = dr["F001physicalDate"].ToString() != "" ? Convert.ToDateTime(dr["F001physicalDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F002physicalResults = dr["F002physicalResults"].ToString();
                //        _Screening.F003physicallFOReason = dr["F003physicallFOReason"].ToString();
                //        _Screening.F004medFollowup = dr["F004medFollowup"].ToString();
                //        _Screening.F005MedFOComments = dr["F005MedFOComments"].ToString();
                //        _Screening.F006bpResults = dr["F006bpResults"].ToString();
                //        _Screening.F007hgDate = dr["F007hgDate"].ToString() != "" ? Convert.ToDateTime(dr["F007hgDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F008hgStatus = dr["F008hgStatus"].ToString();
                //        _Screening.F009hgResults = dr["F009hgResults"].ToString();
                //        _Screening.F010hgReferralDate = dr["F010hgReferralDate"].ToString() != "" ? Convert.ToDateTime(dr["F010hgReferralDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F011hgComments = dr["F011hgComments"].ToString();
                //        _Screening.F012hgDate2 = dr["F012hgDate2"].ToString() != "" ? Convert.ToDateTime(dr["F012hgDate2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F013hgResults2 = dr["F013hgResults2"].ToString();
                //        _Screening.F014hgFOStatus = dr["F014hgFOStatus"].ToString();
                //        _Screening.F015leadDate = dr["F015leadDate"].ToString() != "" ? Convert.ToDateTime(dr["F015leadDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F016leadResults = dr["F016leadResults"].ToString();
                //        _Screening.F017leadReferDate = dr["F017leadReferDate"].ToString() != "" ? Convert.ToDateTime(dr["F017leadReferDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F018leadComments = dr["F018leadComments"].ToString();
                //        _Screening.F019leadDate2 = dr["F019leadDate2"].ToString() != "" ? Convert.ToDateTime(dr["F019leadDate2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.F020leadResults2 = dr["F020leadResults2"].ToString();
                //        _Screening.F021leadFOStatus = dr["F021leadFOStatus"].ToString();
                //        _Screening.v022date = dr["v022date"].ToString() != "" ? Convert.ToDateTime(dr["v022date"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.v023results = dr["v023results"].ToString();
                //        _Screening.v024comments = dr["v024comments"].ToString();
                //        _Screening.v025dateR1 = dr["v025dateR1"].ToString() != "" ? Convert.ToDateTime(dr["v025dateR1"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.v026resultsR1 = dr["v026resultsR1"].ToString();
                //        _Screening.v027commentsR1 = dr["v027commentsR1"].ToString();
                //        _Screening.v028dateR2 = dr["v028dateR2"].ToString() != "" ? Convert.ToDateTime(dr["v028dateR2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.v029resultsR2 = dr["v029resultsR2"].ToString();
                //        _Screening.v030commentsR2 = dr["v030commentsR2"].ToString();
                //        _Screening.v031ReferralDate = dr["v031ReferralDate"].ToString() != "" ? Convert.ToDateTime(dr["v031ReferralDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.v032Treatment = dr["v032Treatment"].ToString();
                //        _Screening.v033TreatmentComments = dr["v033TreatmentComments"].ToString();
                //        _Screening.v034Completedate = dr["v034Completedate"].ToString() != "" ? Convert.ToDateTime(dr["v034Completedate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.v035ExamStatus = dr["v035ExamStatus"].ToString();
                //        _Screening.h036Date = dr["h036Date"].ToString() != "" ? Convert.ToDateTime(dr["h036Date"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.h037Results = dr["h037Results"].ToString();
                //        _Screening.h038Comments = dr["h038Comments"].ToString();
                //        _Screening.h039DateR1 = dr["h039DateR1"].ToString() != "" ? Convert.ToDateTime(dr["h039DateR1"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.h040ResultsR1 = dr["h040ResultsR1"].ToString();
                //        _Screening.h041CommentsR1 = dr["h041CommentsR1"].ToString();
                //        _Screening.h042DateR2 = dr["h042DateR2"].ToString() != "" ? Convert.ToDateTime(dr["h042DateR2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.h043ResultsR2 = dr["h043ResultsR2"].ToString();
                //        _Screening.h044CommentsR2 = dr["h044CommentsR2"].ToString();
                //        _Screening.h045ReferralDate = dr["h045ReferralDate"].ToString() != "" ? Convert.ToDateTime(dr["h045ReferralDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.h046Treatment = dr["h046Treatment"].ToString();
                //        _Screening.h047TreatmentComments = dr["h047TreatmentComments"].ToString();
                //        _Screening.h048CompleteDate = dr["h048CompleteDate"].ToString() != "" ? Convert.ToDateTime(dr["h048CompleteDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.h049ExamStatus = dr["h049ExamStatus"].ToString();
                //        _Screening.d050evDate = dr["d050evDate"].ToString() != "" ? Convert.ToDateTime(dr["d050evDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.d051NameDEV = dr["d051NameDEV"].ToString();
                //        _Screening.d052evResults = dr["d052evResults"].ToString();
                //        _Screening.d053evResultsDetails = dr["d053evResultsDetails"].ToString();
                //        _Screening.d054evDate2 = dr["d054evDate2"].ToString() != "" ? Convert.ToDateTime(dr["d054evDate2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.d055evResults2 = dr["d055evResults2"].ToString();
                //        _Screening.d056evReferral = dr["d056evReferral"].ToString();
                //        _Screening.d057evFOStatus = dr["d057evFOStatus"].ToString();
                //        _Screening.d058evComments = dr["d058evComments"].ToString();
                //        _Screening.d059evTool = dr["d059evTool"].ToString();
                //        _Screening.E060denDate = dr["E060denDate"].ToString() != "" ? Convert.ToDateTime(dr["E060denDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.E061denResults = dr["E061denResults"].ToString();
                //        _Screening.E062denPrevent = dr["E062denPrevent"].ToString();
                //        _Screening.E063denReferralDate = dr["E063denReferralDate"].ToString() != "" ? Convert.ToDateTime(dr["E063denReferralDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.E064denTreatment = dr["E064denTreatment"].ToString();
                //        _Screening.E065denTreatmentComments = dr["E065denTreatmentComments"].ToString();
                //        _Screening.E066denTreatmentReceive = dr["E066denTreatmentReceive"].ToString();
                //        _Screening.s067Date = dr["s067Date"].ToString() != "" ? Convert.ToDateTime(dr["s067Date"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.s068NameTCR = dr["s068NameTCR"].ToString();
                //        _Screening.s069Details = dr["s069Details"].ToString();
                //        _Screening.s070Results = dr["s070Results"].ToString();
                //        _Screening.s071RescreenTCR = dr["s071RescreenTCR"].ToString();
                //        _Screening.s072RescreenTCRDate = dr["s072RescreenTCRDate"].ToString() != "" ? Convert.ToDateTime(dr["s072RescreenTCRDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.s073RescreenTCRResults = dr["s073RescreenTCRResults"].ToString();
                //        _Screening.s074ReferralDC = dr["s074ReferralDC"].ToString();
                //        _Screening.s075ReferDate = dr["s075ReferDate"].ToString() != "" ? Convert.ToDateTime(dr["s075ReferDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.s076DCDate = dr["s076DCDate"].ToString() != "" ? Convert.ToDateTime(dr["s076DCDate"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.s077NameDC = dr["s077NameDC"].ToString();
                //        _Screening.s078DetailDC = dr["s078DetailDC"].ToString();
                //        _Screening.s079DCDate2 = dr["s079DCDate2"].ToString() != "" ? Convert.ToDateTime(dr["s079DCDate2"]).ToString("MM/dd/yyyy") : "";
                //        _Screening.s080DetailDC2 = dr["s080DetailDC2"].ToString();
                //        _Screening.s081FOStatus = dr["s081FOStatus"].ToString();
                //    }
                //    obj._Screening = _Screening;
                //}
                if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
                {
                    List<FamilyHousehold.Parentphone1> _Parentphone = new List<FamilyHousehold.Parentphone1>();
                    FamilyHousehold.Parentphone1 _phoneadd = null;
                    foreach (DataRow row in _dataset.Tables[6].Rows)
                    {
                        _phoneadd = new FamilyHousehold.Parentphone1();
                        _phoneadd.PPhoneId = Convert.ToInt32(row["Id"]);
                        _phoneadd.Parents = Convert.ToInt32(row["ParentID"]);
                        _phoneadd.PhoneTypeP = row["PhoneType"].ToString();
                        _phoneadd.phonenoP = row["Phoneno"].ToString();
                        if (row["IsPrimaryContact"].ToString() != "")
                            _phoneadd.StateP = Convert.ToBoolean(row["IsPrimaryContact"]);
                        _Parentphone.Add(_phoneadd);
                    }
                    obj.phoneListParent = _Parentphone;
                }


                //if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
                //{
                //    List<FamilyHousehold.PMproblemandservices> _PMproblemandservicesList = new List<FamilyHousehold.PMproblemandservices>();
                //    FamilyHousehold.PMproblemandservices info = null;
                //    foreach (DataRow dr in _dataset.Tables[6].Rows)
                //    {
                //        info = new FamilyHousehold.PMproblemandservices();
                //        info.Id = dr["ID"].ToString();
                //        info.MasterId = dr["PMServiceID"].ToString();
                //        info.Description = dr["PMDescription"].ToString();
                //        info.Parentid = dr["ParentID"].ToString();
                //        _PMproblemandservicesList.Add(info);
                //    }
                //    obj._PMservices = _PMproblemandservicesList;
                //}
                //if (_dataset.Tables[7].Rows.Count > 0)
                //{
                //    List<FamilyHousehold.Childhealthnutrition> _childhealthnutrition = new List<FamilyHousehold.Childhealthnutrition>();
                //    FamilyHousehold.Childhealthnutrition info = null;
                //    foreach (DataRow dr in _dataset.Tables[7].Rows)
                //    {
                //        info = new FamilyHousehold.Childhealthnutrition();
                //        info.Id = dr["ID"].ToString();
                //        info.MasterId = dr["ChildRecieveTreatment"].ToString();
                //        info.Description = dr["Description"].ToString();
                //        info.Questionid = dr["Questionid"].ToString();
                //        info.Programid = dr["Programid"].ToString();
                //        _childhealthnutrition.Add(info);
                //    }
                //    obj._Childhealthnutrition = _childhealthnutrition;
                //}
            }
        }
        public FamilyHousehold Checkallmanadatoryfield(string Clientid, string Householid, string Agencyid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@Clientid", Clientid));
                command.Parameters.Add(new SqlParameter("@Householdid", Householid));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getallfamilyintake";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdall(obj, _dataset);
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        public string SaveClientAssigned(ref string result, string agencyid, string userid, string HouseHoldId, string Staffid, string yakkrid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_AssignedtoClient";
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@Staffid", Staffid));
                command.Parameters.Add(new SqlParameter("@yakkrid", yakkrid));
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                command.Dispose();

            }
            return result;
        }
        public FamilyHousehold getpdfimageScreen(string Agencyid, string column, string incomeid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@incomeid", incomeid));
                command.Parameters.Add(new SqlParameter("@column", column));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getincomeimage";
                DataAdapter = new SqlDataAdapter(command);
                //Due to Phone Type
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        obj.EImageByte = (byte[])_dataset.Tables[1].Rows[0]["imagebyte"];
                        obj.EFileExtension = _dataset.Tables[1].Rows[0]["imageExt"].ToString();
                        obj.EFileName = _dataset.Tables[1].Rows[0]["imagename"].ToString();
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        public List<ClientWaitingList> GetclientWaitingList(string CenterId, string Option, string ProgramType, string AgencyId, string UserId)
        {
            List<ClientWaitingList> ClientList = new List<ClientWaitingList>();
            List<UserInfo> _userlist = new List<UserInfo>();
            List<FamilyHousehold.Programdetail> Programs = new List<FamilyHousehold.Programdetail>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@userid", UserId));
                command.Parameters.Add(new SqlParameter("@CenterId", EncryptDecrypt.Decrypt64(CenterId)));
                command.Parameters.Add(new SqlParameter("@Option", Option));
                command.Parameters.Add(new SqlParameter("@ProgramType", ProgramType));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetClientWaitingList";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            ClientWaitingList info = new ClientWaitingList();
                            info.ClientId = dr["Clientid"].ToString();
                            info.HouseholdId = dr["Householdid"].ToString();
                            info.Programid = dr["Programid"].ToString();
                            info.CenterId = dr["centerid"].ToString();
                            info.Name = dr["name"].ToString();
                            info.Choice = dr["centerchoice"].ToString();
                            info.Option = dr["Option"].ToString();
                            info.DateOnList = dr["dateentered"].ToString() != "" ? Convert.ToDateTime(dr["dateentered"]).ToString("MM/dd/yyyy") : "";
                            info.DOB = dr["dob"].ToString() != "" ? Convert.ToDateTime(dr["dob"]).ToString("MM/dd/yyyy") : "";
                            info.Gender = dr["gender"].ToString();
                            info.ProgramType = dr["programtype"].ToString();
                            info.SelectionPoints = dr["Selectionpoint"].ToString();
                            info.TotalChoice = dr["SumChoice"].ToString();
                            ClientList.Add(info);
                        }
                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        UserInfo obj = null;
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            obj = new UserInfo();
                            obj.userId = dr["Userid"].ToString();
                            obj.Name = dr["Name"].ToString();
                            _userlist.Add(obj);
                        }
                        ClientList.FirstOrDefault().UserList = _userlist;
                    }
                    if (_dataset.Tables[2].Rows.Count > 0)
                    {
                        FamilyHousehold.Programdetail obj = null;
                        foreach (DataRow dr in _dataset.Tables[2].Rows)
                        {
                            obj = new FamilyHousehold.Programdetail();
                            obj.Id = Convert.ToInt32(dr["programtypeid"]);
                            obj.Name = dr["programtype"].ToString();
                            obj.ReferenceId = dr["ReferenceId"].ToString();
                            Programs.Add(obj);
                        }
                        ClientList.FirstOrDefault().ProgramsList = Programs;
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return ClientList;
        }
        public List<ClientWaitingList> LoadClientPendinglist(string CenterId, string Type, string AgencyId, string UserId)
        {
            List<ClientWaitingList> ClientList = new List<ClientWaitingList>();
            List<UserInfo> _userlist = new List<UserInfo>();
            List<FamilyHousehold.Programdetail> Programs = new List<FamilyHousehold.Programdetail>();
            try
            {
                string centerif = EncryptDecrypt.Decrypt64(CenterId);
                command.Parameters.Add(new SqlParameter("@Agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@userid", UserId));
                command.Parameters.Add(new SqlParameter("@Type", Type));
                command.Parameters.Add(new SqlParameter("@CenterId", EncryptDecrypt.Decrypt64(CenterId)));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetClientPendinglist";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            ClientWaitingList info = new ClientWaitingList();
                            info.Id = dr["YakkrId"].ToString();
                            info.ClientId = dr["Clientid"].ToString();
                            info.HouseholdId = dr["Householdid"].ToString();
                            info.HouseholdIdencrypted = EncryptDecrypt.Encrypt64(dr["Householdid"].ToString());
                            info.Programid = dr["Programid"].ToString();
                            info.CenterId = dr["centerid"].ToString();
                            info.Name = dr["name"].ToString();
                            info.DateOnList = dr["dateentered"].ToString() != "" ? Convert.ToDateTime(dr["dateentered"]).ToString("MM/dd/yyyy") : "";
                            info.DOB = dr["dob"].ToString() != "" ? Convert.ToDateTime(dr["dob"]).ToString("MM/dd/yyyy") : "";
                            info.Gender = dr["gender"].ToString();
                            info.ProgramType = dr["programtype"].ToString();
                            info.SelectionPoints = dr["Selectionpoint"].ToString();
                            info.Notes = dr["notes"].ToString();
                            ClientList.Add(info);
                        }
                    }

                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
            return ClientList;
        }
        public string DeletePendingClient(string Id, string centerid, string Clientid, string householdid, string Programid, string AgencyId, string UserId)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Add(new SqlParameter("@Agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@userid", UserId));
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@CenterId", centerid));
                command.Parameters.Add(new SqlParameter("@Clientid", Clientid));
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeletePendingClient";
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();

            }

        }
        //Changes
        public List<ClientAcceptList> GetclientAcceptList(string CenterId, string Option, string AgencyId, string UserId)
        {
            List<ClientAcceptList> ClientList = new List<ClientAcceptList>();
            List<ClassRoom> _userlist = new List<ClassRoom>();

            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@userid", UserId));
                command.Parameters.Add(new SqlParameter("@CenterId", EncryptDecrypt.Decrypt64(CenterId)));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetClientAcceptedList";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            ClientAcceptList info = new ClientAcceptList();
                            info.ClientId = dr["Clientid"].ToString();
                            info.HouseholdId = dr["Householdid"].ToString();
                            info.ProgramType = dr["programid"].ToString();
                            info.CenterId = CenterId;
                            info.Name = dr["name"].ToString();
                            info.DateOnList = dr["ModifiedDate"].ToString() != "" ? Convert.ToDateTime(dr["ModifiedDate"]).ToString("MM/dd/yyyy") : "";
                            info.DOB = dr["dob"].ToString() != "" ? Convert.ToDateTime(dr["dob"]).ToString("MM/dd/yyyy") : "";
                            info.Gender = dr["gender"].ToString();
                            if (!DBNull.Value.Equals(dr["ChildTransport"]))
                                info.CTransport = Convert.ToString(dr["ChildTransport"]);
                            else
                                info.CTransport = string.Empty;
                            if (!DBNull.Value.Equals(dr["BMIStatus"]))
                                info.ChildWeight = dr["BMIStatus"].ToString();
                            else
                                info.ChildWeight = dr["BMIStatus"].ToString();
                            if (!DBNull.Value.Equals(dr["ParentDisable"]))
                                info.CParentDisable = dr["ParentDisable"].ToString();
                            else
                                info.CParentDisable = dr["ParentDisable"].ToString();
                            if (!DBNull.Value.Equals(dr["FoodAllergies"]))
                                info.FoodAllergies = dr["FoodAllergies"].ToString();
                            else
                                info.FoodAllergies = dr["FoodAllergies"].ToString();
                            if (dr["IEP"] != DBNull.Value && dr["IEP"].ToString() != "")
                                info.IsIEP = Convert.ToBoolean(dr["IEP"].ToString());
                            if (dr["IFSP"] != DBNull.Value && dr["IFSP"].ToString() != "")
                                info.IsIFSP = Convert.ToBoolean(dr["IFSP"].ToString());
                            if (dr["IsExpired"] != DBNull.Value && dr["IsExpired"].ToString() != "")
                                info.IsExpired = Convert.ToBoolean(dr["IsExpired"].ToString());
                            ClientList.Add(info);
                        }
                    }
                    if (_dataset.Tables[1] != null && _dataset.Tables[1].Rows.Count > 0)
                    {
                        ClassRoom obj = null;
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            obj = new ClassRoom();
                            obj.ClassroomID = Convert.ToInt32(dr["ClassroomId"]);
                            obj.ClassName = dr["ClassroomName"].ToString();
                            obj.ActualSeats = Convert.ToInt32(dr["ActualSeats"]);
                            _userlist.Add(obj);
                        }

                        if (ClientList.Count() > 0)
                        {
                            ClientList.FirstOrDefault().Classroom = _userlist;
                        }

                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                DataAdapter.Dispose();
                if (Connection != null && Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    command.Dispose();
                }

            }
            return ClientList;
        }
        //End

        //22Aug2016
        public string SaveAcceptanceenrollinfo(string Clientid, string ClassroomID, string centerid, string Reason
           , string StartDate, string agencyid, string userid, string Programid, string ChildTran, string ChildBMI, string ChildDis, string ChildFood)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_saveacceptance_enrollprocess";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Clientid", Clientid));
                command.Parameters.Add(new SqlParameter("@ClassroomID", ClassroomID));
                command.Parameters.Add(new SqlParameter("@centerid", EncryptDecrypt.Decrypt64(centerid)));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@Reason", Reason));
                command.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@ChildTrans", ChildTran));
                command.Parameters.Add(new SqlParameter("@ChildDis", ChildDis));
                command.Parameters.Add(new SqlParameter("@ChildBMI", ChildBMI));
                command.Parameters.Add(new SqlParameter("@ChildFood", ChildFood));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {

                if (Connection != null && Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    command.Dispose();
                }
            }
            return "0";
        }
        //end    //end  
        public string DeleteRejectedRecord(string Id, string ClientId, string HouseholdId, string userid, string Agencyid)
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", HouseholdId));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeleteClientYakkr";
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();

            }

        }
        public string SaveHealthreview(string Clientid, string Usernurseid, string householdid, string centerid, string agencyid, string userid, string Programid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_HealthReview";
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@Clientid", Clientid));
                command.Parameters.Add(new SqlParameter("@Usernurseid", Usernurseid));
                command.Parameters.Add(new SqlParameter("@householdid", householdid));
                command.Parameters.Add(new SqlParameter("@centerid", centerid));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@Programid", Programid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                command.Dispose();
            }
            return "0";
        }
        public void FamilySummary(FamilyHousehold FamilyObject, string id, string Agencyid, string userid)
        {

            try
            {
                command.Parameters.Add(new SqlParameter("@Householdid", EncryptDecrypt.Decrypt64(id)));
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@UserId", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Getfamilysummarry";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                FamilySummaryinfo(FamilyObject, _dataset);

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }

        }
        public void FamilySummaryinfo(FamilyHousehold obj, DataSet _dataset)
        {

            if (_dataset != null && _dataset.Tables.Count > 0)
            {
                if (_dataset.Tables[0].Rows.Count > 0)
                {
                    obj.HouseholdId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                    obj.Encrypthouseholid = EncryptDecrypt.Encrypt64(_dataset.Tables[0].Rows[0]["ID"].ToString());
                    obj.Street = _dataset.Tables[0].Rows[0]["Street"].ToString();
                    obj.StreetName = _dataset.Tables[0].Rows[0]["StreetName"].ToString();
                    obj.City = _dataset.Tables[0].Rows[0]["City"].ToString();
                    obj.ZipCode = _dataset.Tables[0].Rows[0]["ZipCode"].ToString();
                    obj.State = _dataset.Tables[0].Rows[0]["State"].ToString();
                    obj.County = _dataset.Tables[0].Rows[0]["County"].ToString();
                    obj.RentTypetext = _dataset.Tables[0].Rows[0]["RentType"].ToString();
                    if (_dataset.Tables[0].Rows[0]["Hpaper"].ToString() != "")
                        obj.AdresssverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Hpaper"]);
                    if (_dataset.Tables[0].Rows[0]["RentTypes"].ToString() != "")
                        obj.RentType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["RentTypes"]);
                    if (_dataset.Tables[0].Rows[0]["TANF"].ToString() != "")
                        obj.TANF = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TANF"]);
                    if (_dataset.Tables[0].Rows[0]["HouseType"].ToString() != "")
                        obj.HomeType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["HouseType"]);
                    if (_dataset.Tables[0].Rows[0]["FamilyType"].ToString() != "")
                        obj.FamilyType = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FamilyType"]);
                    if (_dataset.Tables[0].Rows[0]["SSI"].ToString() != "")
                        obj.SSI = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SSI"]);
                    if (_dataset.Tables[0].Rows[0]["WIC"].ToString() != "")
                        obj.WIC = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WIC"]);
                    if (_dataset.Tables[0].Rows[0]["SNAP"].ToString() != "")
                        obj.SNAP = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["SNAP"]);
                    if (_dataset.Tables[0].Rows[0]["NONE"].ToString() != "")
                        obj.NONE = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NONE"]);
                    if (_dataset.Tables[0].Rows[0]["Interpretor"].ToString() != "")
                        obj.Interpretor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Interpretor"]);
                    if (_dataset.Tables[0].Rows[0]["ParentRelatioship"].ToString() != "")
                        obj.ParentRelatioship = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentRelatioship"]);
                    if (_dataset.Tables[0].Rows[0]["OtherRelationship"].ToString() != "")
                        obj.ParentRelatioshipOther = _dataset.Tables[0].Rows[0]["OtherRelationship"].ToString();
                    if (_dataset.Tables[0].Rows[0]["OtherDesc"].ToString() != "")
                        obj.OtherLanguageDetail = _dataset.Tables[0].Rows[0]["OtherDesc"].ToString();
                    obj.PrimaryLanguauge = Convert.ToString(_dataset.Tables[0].Rows[0]["PrimaryLanguauge"]);
                    obj.Married = _dataset.Tables[0].Rows[0]["Married"].ToString();
                    obj.docstorage = Convert.ToInt32(_dataset.Tables[0].Rows[0]["docsStorage"]);
                    obj.CTransportNeeded = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ChildTransport"]);
                    obj.ExistPmprogram = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Pmprogram"]);
                }
                if (_dataset.Tables[1] != null && _dataset.Tables[1].Rows.Count > 0)
                {
                    //Parent1 
                    obj.ParentID = Convert.ToInt32(_dataset.Tables[1].Rows[0]["clientid"]);
                    obj.Pfirstname = _dataset.Tables[1].Rows[0]["name"].ToString();
                    obj.EncryptedName = EncryptDecrypt.Encrypt64(_dataset.Tables[1].Rows[0]["name"].ToString());
                    obj.GenderParent1 = _dataset.Tables[1].Rows[0]["GenderText"].ToString();
                    obj.PGender = _dataset.Tables[1].Rows[0]["gender"].ToString();
                    obj.IsPreg = Convert.ToInt32(_dataset.Tables[1].Rows[0]["IsPreg"]);
                    obj.PDOB = _dataset.Tables[1].Rows[0]["dob"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[1].Rows[0]["dob"]).ToString("MM/dd/yyy");
                    obj.PImagejson = _dataset.Tables[1].Rows[0]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[1].Rows[0]["ProfilePic"]);
                    obj.RPhoneno = _dataset.Tables[1].Rows[0]["ParentPhoneNo"].ToString() == "" ? "" : Convert.ToString(_dataset.Tables[1].Rows[0]["ParentPhoneNo"]);
                    obj.PPhoneList = obj.RPhoneno.Split(',').ToList();
                    if (_dataset.Tables[1].Rows.Count > 1)
                    {
                        obj.ParentID1 = Convert.ToInt32(_dataset.Tables[1].Rows[1]["clientid"]);
                        obj.P1firstname = _dataset.Tables[1].Rows[1]["name"].ToString();
                        obj.IsPreg1 = Convert.ToInt32(_dataset.Tables[1].Rows[1]["IsPreg"]);
                        obj.GenderParent2 = _dataset.Tables[1].Rows[1]["GenderText"].ToString();
                        obj.P1Gender = _dataset.Tables[1].Rows[1]["gender"].ToString() == "0" ? 0 : Convert.ToInt32(_dataset.Tables[1].Rows[1]["gender"]);
                        obj.P1DOB = _dataset.Tables[1].Rows[1]["dob"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[1].Rows[1]["dob"]).ToString("MM/dd/yyy");
                        obj.P1Imagejson = _dataset.Tables[1].Rows[1]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[1].Rows[1]["ProfilePic"]);
                        obj.P1phoneno = _dataset.Tables[1].Rows[1]["ParentPhoneNo"].ToString() == "" ? "" : Convert.ToString(_dataset.Tables[1].Rows[1]["ParentPhoneNo"]);
                        obj.P1PhoneList = obj.P1phoneno.Split(',').ToList();
                    }
                }
                //Child list
                if (_dataset.Tables[2] != null && _dataset.Tables[2].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Childlist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[2].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.ChildId = Convert.ToInt32(_dataset.Tables[2].Rows[i]["clientid"]);
                        familyinfo.Cfirstname = _dataset.Tables[2].Rows[i]["name"].ToString();
                        familyinfo.CDOB = Convert.ToDateTime(_dataset.Tables[2].Rows[i]["dob"]).ToString("MM/dd/yyyy");
                        familyinfo.Gender = _dataset.Tables[2].Rows[i]["GenderText"].ToString();
                        familyinfo.CGender = _dataset.Tables[2].Rows[i]["gender"].ToString();
                        familyinfo.Imagejson = _dataset.Tables[2].Rows[i]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[2].Rows[i]["ProfilePic"]);
                        familyinfo.Yakkr = _dataset.Tables[2].Rows[i]["yakkr"].ToString();
                        familyinfo.ClassRoomName = _dataset.Tables[2].Rows[i]["ClassRoomName"].ToString();
                        familyinfo.ClassRoomId = Convert.ToInt64(_dataset.Tables[2].Rows[i]["ClassRoomId"]);
                        familyinfo.CenterName = _dataset.Tables[2].Rows[i]["CenterName"].ToString();
                        familyinfo.CenterId = Convert.ToInt64(_dataset.Tables[2].Rows[i]["CenterId"].ToString());
                        familyinfo.CProgramType = Convert.ToString(_dataset.Tables[2].Rows[i]["ProgramType"].ToString());
                        _Childlist.Add(familyinfo);
                    }
                    obj._Clist = _Childlist;
                }
                //others list
                if (_dataset.Tables[3] != null && _dataset.Tables[3].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Olist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[3].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.OthersId = Convert.ToInt32(_dataset.Tables[3].Rows[i]["clientid"]);
                        familyinfo.Ofirstname = _dataset.Tables[3].Rows[i]["name"].ToString();
                        familyinfo.ODOB = _dataset.Tables[3].Rows[i]["dob"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[3].Rows[i]["dob"]).ToString("MM/dd/yyy");
                        familyinfo.Gender = _dataset.Tables[3].Rows[i]["GenderText"].ToString();
                        familyinfo.OGender = _dataset.Tables[3].Rows[i]["gender"].ToString();
                        familyinfo.OtherEligible = Convert.ToBoolean(_dataset.Tables[3].Rows[i]["age"]);
                        familyinfo.HouseHoldImagejson = _dataset.Tables[3].Rows[i]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[3].Rows[i]["ProfilePic"]);

                        _Olist.Add(familyinfo);
                    }
                    obj._Olist = _Olist;
                }
                //EmergencyList
                if (_dataset.Tables[4] != null && _dataset.Tables[4].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Elist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[4].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.EmegencyId = Convert.ToInt32(_dataset.Tables[4].Rows[i]["ClientID"]);
                        familyinfo.Efirstname = _dataset.Tables[4].Rows[i]["name"].ToString();
                        familyinfo.EDOB = _dataset.Tables[4].Rows[i]["dob"].ToString() == "" ? "" : Convert.ToDateTime(_dataset.Tables[4].Rows[i]["dob"]).ToString("MM/dd/yyy");
                        familyinfo.Gender = _dataset.Tables[4].Rows[i]["GenderText"].ToString();
                        familyinfo.EGender = _dataset.Tables[4].Rows[i]["gender"].ToString();
                        familyinfo.EImagejson = _dataset.Tables[4].Rows[i]["FileAttachment"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[4].Rows[i]["FileAttachment"]);
                        _Elist.Add(familyinfo);
                    }
                    obj._Elist = _Elist;
                }
                //restrcited list
                if (_dataset.Tables[5] != null && _dataset.Tables[5].Rows.Count > 0)
                {
                    List<FamilyHousehold> _Rlist = new List<FamilyHousehold>();
                    FamilyHousehold familyinfo = null;
                    for (int i = 0; i < _dataset.Tables[5].Rows.Count; i++)
                    {
                        familyinfo = new FamilyHousehold();
                        familyinfo.RestrictedId = Convert.ToInt32(_dataset.Tables[5].Rows[i]["ClientID"]);
                        familyinfo.Rfirstname = _dataset.Tables[5].Rows[i]["name"].ToString();
                        familyinfo.RImagejson = _dataset.Tables[5].Rows[i]["FileAttachment"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[i]["FileAttachment"]);
                        _Rlist.Add(familyinfo);
                    }
                    obj._Rlist = _Rlist;
                }
                //Bind Dropdown

                List<FingerprintsModel.FamilyHousehold.Relationship> _relationlist = new List<FingerprintsModel.FamilyHousehold.Relationship>();
                foreach (DataRow dr in _dataset.Tables[6].Rows)
                {
                    FingerprintsModel.FamilyHousehold.Relationship Relationship = new FingerprintsModel.FamilyHousehold.Relationship();
                    Relationship.Id = dr["Id"].ToString();
                    Relationship.Name = dr["Name"].ToString();
                    _relationlist.Add(Relationship);
                }
                obj.relationship = _relationlist;
                List<FingerprintsModel.FamilyHousehold.PrimarylangInfo> listlang = new List<FingerprintsModel.FamilyHousehold.PrimarylangInfo>();
                foreach (DataRow dr in _dataset.Tables[7].Rows)
                {
                    FingerprintsModel.FamilyHousehold.PrimarylangInfo _primarylanguage = new FingerprintsModel.FamilyHousehold.PrimarylangInfo();
                    _primarylanguage.LangId = Convert.ToString(dr["Id"].ToString());
                    _primarylanguage.Name = dr["Name"].ToString();
                    listlang.Add(_primarylanguage);
                }
                obj.langList = listlang;
                List<FamilyHousehold.RaceInfo> _racelist = new List<FamilyHousehold.RaceInfo>();
                foreach (DataRow dr in _dataset.Tables[8].Rows)
                {
                    FamilyHousehold.RaceInfo RaceInfo = new FamilyHousehold.RaceInfo();
                    RaceInfo.RaceId = dr["Id"].ToString();
                    RaceInfo.Name = dr["Name"].ToString();
                    _racelist.Add(RaceInfo);
                }
                obj.raceList = _racelist;
                List<SchoolDistrict> schooldistrict = new List<SchoolDistrict>();
                foreach (DataRow dr in _dataset.Tables[9].Rows)
                {
                    SchoolDistrict info = new SchoolDistrict();
                    info.SchoolDistrictID = Convert.ToInt32(dr["SchoolDistrictID"]);
                    info.Acronym = dr["Acronym"].ToString();
                    schooldistrict.Add(info);
                }
                obj.SchoolList = schooldistrict;

                //Reasons List
                if (_dataset.Tables[10] != null && _dataset.Tables[10].Rows.Count > 0)
                {
                    List<FamilyHousehold.ClassroomChangeReason> _reasonList = new List<FamilyHousehold.ClassroomChangeReason>();
                    FamilyHousehold.ClassroomChangeReason reason = null;
                    for (int i = 0; i < _dataset.Tables[10].Rows.Count; i++)
                    {
                        reason = new FamilyHousehold.ClassroomChangeReason();
                        reason.Value = Convert.ToString(_dataset.Tables[10].Rows[i]["ChangeReasonId"]);
                        reason.Reason = Convert.ToString(_dataset.Tables[10].Rows[i]["ChangeReason"]);
                        _reasonList.Add(reason);
                    }
                    obj._ChangeReasonList = _reasonList;
                }


            }
        }
        public string SaveFamilySummary(FamilyHousehold info, string Agenyid, string UserId)
        {
            string result = string.Empty;
            try
            {
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", info.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Street", info.Street));
                command.Parameters.Add(new SqlParameter("@StreetName", info.StreetName));
                command.Parameters.Add(new SqlParameter("@Apartmentno", info.Apartmentno));
                command.Parameters.Add(new SqlParameter("@ZipCode", info.ZipCode));
                command.Parameters.Add(new SqlParameter("@State", info.State));
                command.Parameters.Add(new SqlParameter("@City", info.City));
                command.Parameters.Add(new SqlParameter("@nationality", info.County));
                command.Parameters.Add(new SqlParameter("@fileinbyte1", info.HImageByte));
                command.Parameters.Add(new SqlParameter("@filename1", info.HFileName));
                command.Parameters.Add(new SqlParameter("@fileextension1", info.HFileExtension));
                command.Parameters.Add(new SqlParameter("@AdresssverificationinPaper", info.AdresssverificationinPaper));
                command.Parameters.Add(new SqlParameter("@TANF", info.TANF));
                command.Parameters.Add(new SqlParameter("@SSI", info.SSI));
                command.Parameters.Add(new SqlParameter("@SNAP", info.SNAP));
                command.Parameters.Add(new SqlParameter("@WIC", info.WIC));
                command.Parameters.Add(new SqlParameter("@NONE", info.NONE));
                command.Parameters.Add(new SqlParameter("@HomeType", info.HomeType));
                command.Parameters.Add(new SqlParameter("@PrimaryLanguauge", info.PrimaryLanguauge));
                command.Parameters.Add(new SqlParameter("@RentType", info.RentType));
                command.Parameters.Add(new SqlParameter("@Interpretor", info.Interpretor));
                command.Parameters.Add(new SqlParameter("@OtherLanguageDetail", info.OtherLanguageDetail));
                command.Parameters.Add(new SqlParameter("@CreatedBy", UserId));
                command.Parameters.Add(new SqlParameter("@AgencyID", Agenyid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SaveFamilysummary";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                FamilySummaryinfo(info, _dataset);
                result = command.Parameters["@result"].Value.ToString();

            }
            catch (Exception Ex)
            {
                clsError.WriteException(Ex);
            }

            return result;
        }

        public string AddWellBabyDetails(Screening _screen, string agencyid, string userid)
        {
            string result = string.Empty;
            try
            {

                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", EncryptDecrypt.Decrypt64(_screen.Householdid)));
                command.Parameters.Add(new SqlParameter("@ChildId", EncryptDecrypt.Decrypt64(_screen.Childid)));
                command.Parameters.Add(new SqlParameter("@AgencyId", agencyid));
                command.Parameters.Add(new SqlParameter("@CreatedBy", userid));
                command.Parameters.Add(new SqlParameter("@PhysicalFileName", _screen.PhysicalFileName));
                command.Parameters.Add(new SqlParameter("@PhysicalFileExtension", _screen.PhysicalFileExtension));
                command.Parameters.Add(new SqlParameter("@PhysicalImageByte", _screen.PhysicalImageByte));
                command.Parameters.Add(new SqlParameter("@WellBabyExamMonth", _screen.WellBabyExamMonth));

                #region screening
                DataTable dt2 = new DataTable();
                dt2.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),

                    });
                #endregion
                foreach (var s in _screen.GetType().GetProperties())
                {
                    int screeningid = 0;
                    int questionid = 0;
                    string month = "";
                    if (s.Name.Substring(0, 1) == "F")
                    {
                        screeningid = 1;

                    }
                    if (screeningid == 1)
                    {
                        questionid = Convert.ToInt32(s.Name.Substring(1, 3));
                        dt2.Rows.Add(screeningid, questionid, s.GetValue(_screen));
                    }
                }

                command.Parameters.Add(new SqlParameter("@tblscreening", dt2));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddWellBabyExamDetails";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string Addnewchild(ref FamilyHousehold obj, int mode, Guid ID, List<FamilyHousehold.ImmunizationRecord> Imminization, Screening _screen, FormCollection collection, HttpFileCollectionBase Files)
        {
            string result = string.Empty;
            try
            {

                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@OthersId", obj.OthersId));
                command.Parameters.Add(new SqlParameter("@ChildId", obj.ChildId));
                //child paremeter
                command.Parameters.Add(new SqlParameter("@Cfirstname", obj.Cfirstname));
                command.Parameters.Add(new SqlParameter("@Clastname", obj.Clastname));
                command.Parameters.Add(new SqlParameter("@Cmiddlename", obj.Cmiddlename));
                command.Parameters.Add(new SqlParameter("@CprogramType", obj.CProgramType));
                command.Parameters.Add(new SqlParameter("@CDOB", obj.CDOB));
                command.Parameters.Add(new SqlParameter("@CTransportNeeded", obj.CTransportNeeded));
                command.Parameters.Add(new SqlParameter("@CDOBverifiedby", obj.DOBverifiedBy));
                command.Parameters.Add(new SqlParameter("@CSSN", obj.CSSN == null ? null : EncryptDecrypt.Encrypt(obj.CSSN)));
                command.Parameters.Add(new SqlParameter("@CGender", obj.CGender));
                command.Parameters.Add(new SqlParameter("@CRace", obj.CRace));
                command.Parameters.Add(new SqlParameter("@CRaceSubCategory", obj.CRaceSubCategory));
                command.Parameters.Add(new SqlParameter("@CEthnicity", obj.CEthnicity));
                command.Parameters.Add(new SqlParameter("@CMedicalhome", obj.Medicalhome));
                command.Parameters.Add(new SqlParameter("@Dentalhome", obj.CDentalhome));
                command.Parameters.Add(new SqlParameter("@ImmunizationService", obj.ImmunizationService));
                command.Parameters.Add(new SqlParameter("@medicalservice", obj.MedicalService));
                command.Parameters.Add(new SqlParameter("@Parentdisable", obj.CParentdisable));
                command.Parameters.Add(new SqlParameter("@Bmistatus", obj.BMIStatus2));
                command.Parameters.Add(new SqlParameter("@FileName2", obj.CFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension2", obj.CFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes2", obj.CImageByte));
                command.Parameters.Add(new SqlParameter("@Dobaddressform", obj.Dobaddressform));
                command.Parameters.Add(new SqlParameter("@DobFileName", obj.DobFileName));
                command.Parameters.Add(new SqlParameter("@DobFileExtension", obj.DobFileExtension));

                command.Parameters.Add(new SqlParameter("@FosterAttachment", obj.Fosterfileinbytes));
                command.Parameters.Add(new SqlParameter("@FosterFileName", obj.FosterFileName));
                command.Parameters.Add(new SqlParameter("@FosterFileExtension", obj.FosterFileExtension));
                command.Parameters.Add(new SqlParameter("@FosterPaper", obj.FosterVerificationPaper));

                command.Parameters.Add(new SqlParameter("@Doctor", obj.Doctor));
                command.Parameters.Add(new SqlParameter("@Dentist", obj.Dentist));
                command.Parameters.Add(new SqlParameter("@Dobpaper", obj.DobverificationinPaper));
                command.Parameters.Add(new SqlParameter("@SchoolDistrict", obj.SchoolDistrict));
                command.Parameters.Add(new SqlParameter("@InsuranceOption", obj.InsuranceOption));
                command.Parameters.Add(new SqlParameter("@MedicalNotice", obj.MedicalNote));
                command.Parameters.Add(new SqlParameter("@IsFoster", obj.IsFoster));
                command.Parameters.Add(new SqlParameter("@Inwalfareagency", obj.Inwalfareagency));
                command.Parameters.Add(new SqlParameter("@InDualcustody", obj.InDualcustody));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileName", obj.ImmunizationFileName));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileExtension", obj.ImmunizationFileExtension));
                command.Parameters.Add(new SqlParameter("@Immunizationfileinbytes", obj.Immunizationfileinbytes));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileName", obj.ReleaseformFileName));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileExtension", obj.ReleaseformFileExtension));
                command.Parameters.Add(new SqlParameter("@Releaseformfileinbytes", obj.Releaseformfileinbytes));
                command.Parameters.Add(new SqlParameter("@ImmunizationinPaper", obj.ImmunizationinPaper));
                command.Parameters.Add(new SqlParameter("@HWInput", obj.HWInput));
                command.Parameters.Add(new SqlParameter("@AssessmentDate", obj.AssessmentDate));
                command.Parameters.Add(new SqlParameter("@BHeight", obj.AHeight));
                command.Parameters.Add(new SqlParameter("@bWeight", obj.AWeight));
                command.Parameters.Add(new SqlParameter("@HeadCircle", obj.HeadCircle));
                command.Parameters.Add(new SqlParameter("@OtherRace", obj.Raceother));

                DataTable dt1 = new DataTable();
                dt1.Columns.AddRange(new DataColumn[18] {
                    new DataColumn("Immunizationmasterid", typeof(Int32)),
                    new DataColumn("ImmunizationId", typeof(Int32)),
                    new DataColumn("Dose",typeof(string)),
                    new DataColumn("Dose1",typeof(string)),
                    new DataColumn("Preempt1",typeof(bool)),
                    new DataColumn("Exempt1",typeof(bool)),
                    new DataColumn("Dose2",typeof(string)),
                    new DataColumn("Preempt2",typeof(bool)),
                    new DataColumn("Exempt2",typeof(bool)),
                    new DataColumn("Dose3",typeof(string)),
                    new DataColumn("Preempt3",typeof(bool)),
                    new DataColumn("Exempt3",typeof(bool)),
                     new DataColumn("Dose4",typeof(string)),
                    new DataColumn("Preempt4",typeof(bool)),
                    new DataColumn("Exempt4",typeof(bool)),
                     new DataColumn("Dose5",typeof(string)),
                    new DataColumn("Preempt5",typeof(bool)),
                    new DataColumn("Exempt5",typeof(bool)),

                    });
                if (Imminization != null)
                {
                    foreach (FamilyHousehold.ImmunizationRecord _Imminization in Imminization)
                    {
                        dt1.Rows.Add(_Imminization.ImmunizationmasterId, _Imminization.ImmunizationId, _Imminization.Dose, _Imminization.Dose1, _Imminization.Preempt1, _Imminization.Exempt1
                            , _Imminization.Dose2, _Imminization.Preempt2, _Imminization.Exempt2, _Imminization.Dose3, _Imminization.Preempt3, _Imminization.Exempt3
                            , _Imminization.Dose4, _Imminization.Preempt4, _Imminization.Exempt4, _Imminization.Dose5, _Imminization.Preempt5, _Imminization.Exempt5);
                    }
                }

                #region screening
                DataTable dt2 = new DataTable();
                dt2.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string))
                     //new DataColumn("WellBabyExamMonth",typeof(string)),
                    });
                #endregion
                foreach (var s in _screen.GetType().GetProperties())
                {
                    int screeningid = 0;
                    int questionid = 0;
                    string month = "";
                    if (s.Name.Substring(0, 1) == "F")
                    {
                        screeningid = 1;
                        //month = _screen.WellBabyExamMonth;
                    }

                    if (s.Name.Substring(0, 1) == "v")
                        screeningid = 2;
                    if (s.Name.Substring(0, 1) == "h")
                        screeningid = 3;
                    if (s.Name.Substring(0, 1) == "d")
                        screeningid = 4;
                    if (s.Name.Substring(0, 1) == "E")
                        screeningid = 5;
                    if (s.Name.Substring(0, 1) == "s")
                        screeningid = 6;
                    if (screeningid == 1 || screeningid == 2 || screeningid == 3 || screeningid == 4 || screeningid == 5 || screeningid == 6)
                    {
                        questionid = Convert.ToInt32(s.Name.Substring(1, 3));
                        dt2.Rows.Add(screeningid, questionid, s.GetValue(_screen));
                    }
                }
                command.Parameters.Add(new SqlParameter("@tblImminization", dt1));
                command.Parameters.Add(new SqlParameter("@tblscreening", dt2));
                //Changes
                command.Parameters.Add(new SqlParameter("@Physical", _screen.AddPhysical));
                command.Parameters.Add(new SqlParameter("@Vision", _screen.AddVision));
                command.Parameters.Add(new SqlParameter("@Dental", _screen.AddDental));
                command.Parameters.Add(new SqlParameter("@Hearing", _screen.AddHearing));
                command.Parameters.Add(new SqlParameter("@Develop", _screen.AddDevelop));
                command.Parameters.Add(new SqlParameter("@Speech", _screen.AddSpeech));
                //    command.Parameters.Add(new SqlParameter("@ScreeningAccept", _screen.ScreeningAccept));
                command.Parameters.Add(new SqlParameter("@PhysicalFileName", _screen.PhysicalFileName));
                command.Parameters.Add(new SqlParameter("@PhysicalFileExtension", _screen.PhysicalFileExtension));
                command.Parameters.Add(new SqlParameter("@PhysicalImageByte", _screen.PhysicalImageByte));
                command.Parameters.Add(new SqlParameter("@VisionFileName", _screen.VisionFileName));
                command.Parameters.Add(new SqlParameter("@VisionFileExtension", _screen.VisionFileExtension));
                command.Parameters.Add(new SqlParameter("@VisionImageByte", _screen.VisionImageByte));
                command.Parameters.Add(new SqlParameter("@DevelopFileName", _screen.DevelopFileName));
                command.Parameters.Add(new SqlParameter("@DevelopFileExtension", _screen.DevelopFileExtension));
                command.Parameters.Add(new SqlParameter("@DevelopImageByte", _screen.DevelopImageByte));
                command.Parameters.Add(new SqlParameter("@DentalFileExtension", _screen.DentalFileExtension));
                command.Parameters.Add(new SqlParameter("@DentalFileName", _screen.DentalFileName));
                command.Parameters.Add(new SqlParameter("@DentalImageByte", _screen.DentalImageByte));
                command.Parameters.Add(new SqlParameter("@HearingFileName", _screen.HearingFileName));
                command.Parameters.Add(new SqlParameter("@HearingFileExtension", _screen.HearingFileExtension));
                command.Parameters.Add(new SqlParameter("@HearingImageByte", _screen.HearingImageByte));
                command.Parameters.Add(new SqlParameter("@SpeechFileName", _screen.SpeechFileName));
                command.Parameters.Add(new SqlParameter("@SpeechFileExtension", _screen.SpeechFileExtension));
                command.Parameters.Add(new SqlParameter("@SpeechImageByte", _screen.SpeechImageByte));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileExtension", _screen.ScreeningAcceptFileExtension));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileName", _screen.ScreeningAcceptFileName));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptImageByte", _screen.ScreeningAcceptImageByte));
                //Changes
                command.Parameters.Add(new SqlParameter("@Consolidated", _screen.Consolidated));
                command.Parameters.Add(new SqlParameter("@ParentName", _screen.Parentname));
                #region Parent1,Parent2 health question
                command.Parameters.Add(new SqlParameter("@PMVisitDoc", obj.PMVisitDoc));
                command.Parameters.Add(new SqlParameter("@PMProblem", obj.PMProblem));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues", obj.PMOtherIssues));
                command.Parameters.Add(new SqlParameter("@PMConditions", obj.PMConditions));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc", obj.PMCondtnDesc));
                command.Parameters.Add(new SqlParameter("@PMRisk", obj.PMRisk));
                command.Parameters.Add(new SqlParameter("@PMDentalExam", obj.PMDentalExam));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate", obj.PMDentalExamDate));
                command.Parameters.Add(new SqlParameter("@PMNeedDental", obj.PMNeedDental));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental", obj.PMRecieveDental));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices", obj._Pregnantmotherpmservices));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem", obj._Pregnantmotherproblem));
                command.Parameters.Add(new SqlParameter("@PMVisitDoc1", obj.PMVisitDoc1));
                command.Parameters.Add(new SqlParameter("@PMProblem1", obj.PMProblem1));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues1", obj.PMOtherIssues1));
                command.Parameters.Add(new SqlParameter("@PMConditions1", obj.PMConditions1));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc1", obj.PMCondtnDesc1));
                command.Parameters.Add(new SqlParameter("@PMRisk1", obj.PMRisk1));
                command.Parameters.Add(new SqlParameter("@PMDentalExam1", obj.PMDentalExam1));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate1", obj.PMDentalExamDate1));
                command.Parameters.Add(new SqlParameter("@PMNeedDental1", obj.PMNeedDental1));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental1", obj.PMRecieveDental1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices1", obj._Pregnantmotherpmservices1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem1", obj._Pregnantmotherproblem1));
                #endregion
                #region child health Ehs
                command.Parameters.Add(new SqlParameter("@EHsChildBorn", obj.EhsChildBorn));
                command.Parameters.Add(new SqlParameter("@EhsChildBirthWt", obj.EhsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@EhsChildLength", obj.EhsChildLength));
                command.Parameters.Add(new SqlParameter("@EhsChildProblm", obj.EhsChildProblm));
                command.Parameters.Add(new SqlParameter("@EhsMedication", obj.EhsMedication));
                //10082016
                command.Parameters.Add(new SqlParameter("@EHSBabyOrMotherProblems", obj.EHSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@EHSChildMedication", obj.EHSChildMedication));
                //
                command.Parameters.Add(new SqlParameter("@EhsComment", obj.EhsComment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeEhs", obj._ChildDirectBloodRelativeEhs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsEhs", obj._ChildDiagnosedConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsEhs", obj._ChildChronicHealthConditions2Ehs));
                command.Parameters.Add(new SqlParameter("@ChildreceivedChronicHealthConditionsEhs", obj._ChildChronicHealthConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceivingChronicHealthConditionsEhs", obj._ChildChronicHealthConditions1Ehs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentEhs", obj._ChildMedicalTreatmentEhs));


                command.Parameters.Add(new SqlParameter("@EHSAllergy", obj.EHSAllergy));
                command.Parameters.Add(new SqlParameter("@EHSEpiPen", obj.EHSEpiPen));
                #endregion
                #region child health Hs
                command.Parameters.Add(new SqlParameter("@HsChildBorn", obj.HsChildBorn));
                command.Parameters.Add(new SqlParameter("@HsChildBirthWt", obj.HsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@HsChildLength", obj.HsChildLength));
                command.Parameters.Add(new SqlParameter("@HsChildProblm", obj.HsChildProblm));
                command.Parameters.Add(new SqlParameter("@HsMedication", obj.HsMedication));
                command.Parameters.Add(new SqlParameter("@HsDentalExam", obj.HsDentalExam));
                command.Parameters.Add(new SqlParameter("@HsComment", obj.HsComment));
                command.Parameters.Add(new SqlParameter("@HsChildDentalCare", obj.HsChildDentalCare));
                command.Parameters.Add(new SqlParameter("@HsRecentDentalExam", obj.HsRecentDentalExam));
                command.Parameters.Add(new SqlParameter("@HsChildNeedDentalTreatment", obj.HsChildNeedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HsChildRecievedDentalTreatment", obj.HsChildRecievedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeHs", obj._ChildDirectBloodRelativeHs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsHs", obj._ChildDiagnosedConditionsHs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentHs", obj._ChildMedicalTreatmentHs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsHs", obj._ChildChronicHealthConditionsHs));
                //10082016
                command.Parameters.Add(new SqlParameter("@HSBabyOrMotherProblems", obj.HSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@HsMedicationName", obj.HsMedicationName));
                command.Parameters.Add(new SqlParameter("@HsDosage", obj.HsDosage));
                command.Parameters.Add(new SqlParameter("@HSChildMedication", obj.HSChildMedication));
                command.Parameters.Add(new SqlParameter("@HSPreventativeDentalCare", obj.HSPreventativeDentalCare));
                command.Parameters.Add(new SqlParameter("@HSProfessionalDentalExam", obj.HSProfessionalDentalExam));
                command.Parameters.Add(new SqlParameter("@ChildProfessionalDentalExam", obj.ChildProfessionalDentalExam));//new ques added


                command.Parameters.Add(new SqlParameter("@HSNeedingDentalTreatment", obj.HSNeedingDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HSChildReceivedDentalTreatment", obj.HSChildReceivedDentalTreatment));
                //
                #endregion
                //#region child nutrition
                //command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                //command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                //command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                //command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                //command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                //command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                //command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                //command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                //command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                //command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                //command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                //command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                //command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                //command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                //command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                //command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                //command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                //command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                //command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                //command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                //command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                //command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                //command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                //command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                //command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                //command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                //command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                //command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                //command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                //command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                //command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                //command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                //command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                //command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                //command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                //command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));
                //command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                //command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                //command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                //command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                //command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                //command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                //command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                //command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                //command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                //command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                //command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                //command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                //command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                //command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));

                //command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));
                //#endregion
                //Added by Akansha on 19Dec2016
                // child nutrition with HS/Ehs
                if (obj._childprogrefid == "1")  //Ehs Questions
                {
                    #region child nutrition

                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.EhsChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.EhsPersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.EhsPersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.EhsPersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.EhsDramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.EhsRecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.EhsRecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.EhsChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.EhsFoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.EhsNutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.EhsFoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.EhschildTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.Ehsspoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.Ehsfeedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.EhschildThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.EhsTakebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.Ehschewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.EhsChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.EhsChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.EhsMilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));//Differ
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));//End
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.EhsChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.EhsChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.EhsBreakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.EhsLunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.EhsSnack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.EhsDinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.EhsNA));
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.EhsRestrictFood));//New ques added

                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.EhsNauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.EhsDiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.EhsConstipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.EhsDramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.EhsRecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.EhsRecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.EhsSpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.EhsFoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.EhsNutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.EhsChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.EhsSpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.EhsSpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.EhsBottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EhsEatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                if (obj._childprogrefid == "2")  //hs Questions
                {
                    #region child nutrition
                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.ChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.RestrictFood));//New ques added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                //custom screenig save
                #region custom screening
                DataTable screeningquestion = new DataTable();
                screeningquestion.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),
                    new DataColumn("OptionID",typeof(Int32)),
                    new DataColumn("ScreeningDate",typeof(string))
                    });
                #endregion
                #region allowed screening
                DataTable screeningallowed = new DataTable();
                screeningallowed.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("Allowed",typeof(Int32)),
                    new DataColumn("FileName",typeof(string)),
                    new DataColumn("FileExtension",typeof(string)),
                    new DataColumn("FileBytes",typeof(byte[]))
                    });
                #endregion
                if (collection != null)
                {
                    foreach (var radio in collection.AllKeys.Where(P => P.Contains("_allowchildcustomscreening")))
                    {
                        if (collection[radio].ToString() == "1")
                        {
                            foreach (var question in collection.AllKeys.Where(P => P.Contains("_custscreeningquestin") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                string questionid = string.Empty;
                                string optionid = string.Empty;
                                string screeningdate = "";
                                if (question.ToString().Contains("o") || question.ToString().Contains("k"))
                                    questionid = question.ToString().Split('k', 'k')[2];
                                if (question.ToString().Contains("o"))
                                {
                                    optionid = question.ToString().Split('o', 'o')[1];
                                    questionid = question.ToString().Split('k', 'k')[2];
                                }
                                if (question.ToString().Contains("_custrad"))
                                {
                                    optionid = collection[question].ToString().Split('o', 'o')[1];
                                    questionid = collection[question].ToString().Split('k', 'k')[2];
                                }
                                if (question.Contains("_$SD"))
                                    screeningdate = collection[question].ToString();
                                if (string.IsNullOrEmpty(optionid))
                                {
                                    if (question.ToString().Contains("select"))
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString().Replace(",", ""), DBNull.Value, screeningdate);
                                    else
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString(), DBNull.Value, screeningdate);
                                }

                                else
                                    screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, DBNull.Value, optionid, screeningdate);
                                optionid = "";
                                questionid = "";
                            }
                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }
                        }
                        else
                        {
                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }
                        }
                    }
                }
                //End
                command.Parameters.Add(new SqlParameter("@screeningquestion", screeningquestion));
                command.Parameters.Add(new SqlParameter("@screeningallowed", screeningallowed));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_ChildSummary";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string AddParent(ref FamilyHousehold obj, int mode, Guid ID, List<FamilyHousehold.Parentphone1> ParentPhoneNos, List<FamilyHousehold.calculateincome> Income)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@ParentID", obj.ParentID));
                command.Parameters.Add(new SqlParameter("@Pfirstname", obj.Pfirstname));
                command.Parameters.Add(new SqlParameter("@Plastname", obj.Plastname));
                command.Parameters.Add(new SqlParameter("@Pmiddlename", obj.Pmidddlename));
                command.Parameters.Add(new SqlParameter("@Pnotes", obj.Pnotes));
                command.Parameters.Add(new SqlParameter("@Pnotesother", obj.Pnotesother));
                command.Parameters.Add(new SqlParameter("@Pemailid", obj.Pemailid));
                command.Parameters.Add(new SqlParameter("@PDOB", obj.PDOB));
                command.Parameters.Add(new SqlParameter("@PRole", obj.PRole));
                command.Parameters.Add(new SqlParameter("@PGender", obj.PGender));
                command.Parameters.Add(new SqlParameter("@PMilitaryStatus", obj.PMilitaryStatus));
                command.Parameters.Add(new SqlParameter("@PCurrentlyWorking", obj.PCurrentlyWorking));
                command.Parameters.Add(new SqlParameter("@PPolicyCouncil", obj.PPolicyCouncil));
                command.Parameters.Add(new SqlParameter("@PEnrollment", obj.PEnrollment));
                command.Parameters.Add(new SqlParameter("@PDegreeEarned", obj.PDegreeEarned));
                command.Parameters.Add(new SqlParameter("@PGuardiannotes", obj.PGuardiannotes));
                command.Parameters.Add(new SqlParameter("@FileName4", obj.PFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension4", obj.PFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes4", obj.PImageByte));
                command.Parameters.Add(new SqlParameter("@PQuestion", obj.PQuestion));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherenrolled", obj.Pregnantmotherenrolled));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance", obj.Pregnantmotherprimaryinsurance));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes", obj.Pregnantmotherprimaryinsurancenotes));
                command.Parameters.Add(new SqlParameter("@TrimesterEnrolled", obj.TrimesterEnrolled));
                if (string.IsNullOrEmpty(obj.ParentSSN1))
                    command.Parameters.Add(new SqlParameter("@ParentSSN1", DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@ParentSSN1", EncryptDecrypt.Encrypt(obj.ParentSSN1)));
                command.Parameters.Add(new SqlParameter("@MedicalhomePreg1", obj.MedicalhomePreg1));
                command.Parameters.Add(new SqlParameter("@P1Doctor", obj.P1Doctor));
                command.Parameters.Add(new SqlParameter("@Noemail", obj.Noemail1));
                //
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[33] {
                    new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });

                foreach (FamilyHousehold.calculateincome parentincome in Income)
                {
                    if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                    {
                        dt.Rows.Add(parentincome.newincomeid, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                              parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                              parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                               parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                               parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                               parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                               parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                               parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                               parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                               );
                    }
                }
                command.Parameters.Add(new SqlParameter("@tblincome", dt));
                DataTable dt2 = new DataTable();
                dt2.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });

                foreach (FamilyHousehold.Parentphone1 phone in ParentPhoneNos)
                {
                    if (phone.phonenoP != null && phone.PhoneTypeP != null)
                    {
                        dt2.Rows.Add(phone.PhoneTypeP, phone.phonenoP, phone.StateP, phone.SmsP, phone.notesP, phone.PPhoneId);
                    }
                }
                command.Parameters.Add(new SqlParameter("@tblphone", dt2));

                command.Parameters.Add(new SqlParameter("@PMVisitDoc", obj.PMVisitDoc));
                command.Parameters.Add(new SqlParameter("@PMProblem", obj.PMProblem));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues", obj.PMOtherIssues));
                command.Parameters.Add(new SqlParameter("@PMConditions", obj.PMConditions));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc", obj.PMCondtnDesc));
                command.Parameters.Add(new SqlParameter("@PMRisk", obj.PMRisk));
                command.Parameters.Add(new SqlParameter("@PMDentalExam", obj.PMDentalExam));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate", obj.PMDentalExamDate));
                command.Parameters.Add(new SqlParameter("@PMNeedDental", obj.PMNeedDental));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental", obj.PMRecieveDental));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices", obj._Pregnantmotherpmservices));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem", obj._Pregnantmotherproblem));

                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_AddParent";
                command.ExecuteNonQuery();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }

        //public void CheckByClient(string GetParameter, int Mode)
        //{
        //    //   DataTable dt = new DataTable();   
        //    //string Parameter = "INSERT";  
        //    try
        //    {
        //        if (Connection.State == ConnectionState.Open)
        //            Connection.Close();
        //        Connection.Open();
        //        command.Connection = Connection;
        //        command.Parameters.Clear();
        //        command.Parameters.AddWithValue("@Command", GetParameter);
        //        command.Parameters.AddWithValue("@Mode", Mode);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "USP_CheckByClient";
        //        int RowsAffected = command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsError.WriteException(ex);
        //    }
        //    finally
        //    {
        //        if (Connection != null)
        //            Connection.Close();
        //        command.Dispose();
        //    }

        //}
        public string AddOthersSummary(FamilyHousehold obj, string agencyid, string ID)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@OthersId", obj.OthersId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Ofirstname", obj.Ofirstname));
                command.Parameters.Add(new SqlParameter("@Olastname", obj.Olastname));
                command.Parameters.Add(new SqlParameter("@Omiddlename", obj.Omiddlename));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@ODOB", obj.ODOB));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@FileName", obj.HouseHoldFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.HouseHoldFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.HouseHoldImageByte));
                if (obj.Oemergencycontact)
                    command.Parameters.Add(new SqlParameter("@Isemergency", obj.Oemergencycontact));
                else
                    command.Parameters.Add(new SqlParameter("@Isemergency", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OGender", obj.OGender));
                command.Parameters.Add(new SqlParameter("@ParentSSN3", obj.ParentSSN3 == null ? null : EncryptDecrypt.Encrypt(obj.ParentSSN3)));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_Othersinhousehold";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                FamilySummaryinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string AddeContacts(FamilyHousehold obj, Guid ID, List<FamilyHousehold.phone> PhoneNos, string agencyid)
        {
            string result = string.Empty;
            try
            {

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@EmergencyId", obj.EmegencyId));
                command.Parameters.Add(new SqlParameter("@Efirstname", obj.Efirstname));
                command.Parameters.Add(new SqlParameter("@Elastname", obj.Elastname));
                command.Parameters.Add(new SqlParameter("@Emiddlename", obj.Emiddlename));
                command.Parameters.Add(new SqlParameter("@EDOB", obj.EDOB));
                command.Parameters.Add(new SqlParameter("@EGender", obj.EGender));
                command.Parameters.Add(new SqlParameter("@EEmail", obj.EEmail));
                command.Parameters.Add(new SqlParameter("@relationship", obj.ERelationwithchild));
                command.Parameters.Add(new SqlParameter("@Enotes", obj.Enotes));
                command.Parameters.Add(new SqlParameter("@FileName", obj.EFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.EFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.EImageByte));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                if (PhoneNos != null && PhoneNos.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string)),
                    });
                    foreach (FamilyHousehold.phone phone in PhoneNos)
                    {
                        if (phone.PhoneNo != null && phone.PhoneType != null)
                        {
                            dt.Rows.Add(phone.PhoneType, phone.PhoneNo, phone.IsPrimary, phone.IsSms, phone.Notes, phone.PhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt));

                }
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_EContacts";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                FamilySummaryinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {

                clsError.WriteException(ex);
                result = "";

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public string addRestricted(FamilyHousehold obj, Guid ID, string agencyid)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@RestrictedId", obj.RestrictedId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Rfirstname", obj.Rfirstname));
                command.Parameters.Add(new SqlParameter("@Rlastname", obj.Rlastname));
                command.Parameters.Add(new SqlParameter("@Rmiddlename", obj.Rmiddlename));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@RDescription", obj.RDescription));
                command.Parameters.Add(new SqlParameter("@FileName", obj.RFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.RFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.RImageByte));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_Restrictedclient";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                FamilySummaryinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }
        public FamilyHousehold GetData_AllDropdownChild(string agencyid, string userid, int OthersId, string roleid)
        {
            //  List<AgencyStaff> _agencyStafflist = new List<AgencyStaff>();
            FamilyHousehold Info = new FamilyHousehold();

            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "Sp_Dropdownfolrchild";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@AgencyId", agencyid));
                        command.Parameters.Add(new SqlParameter("@userid", userid));
                        command.Parameters.Add(new SqlParameter("@OthersId", OthersId));
                        command.Parameters.Add(new SqlParameter("@roleid", roleid));
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                    try
                    {
                        List<FingerprintsModel.FamilyHousehold.PrimarylangInfo> listlang = new List<FingerprintsModel.FamilyHousehold.PrimarylangInfo>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            FingerprintsModel.FamilyHousehold.PrimarylangInfo obj = new FingerprintsModel.FamilyHousehold.PrimarylangInfo();
                            obj.LangId = Convert.ToString(dr["Id"].ToString());
                            obj.Name = dr["Name"].ToString();
                            listlang.Add(obj);
                        }

                        Info.langList = listlang;
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.RaceSubCategory> _racelist = new List<FingerprintsModel.FamilyHousehold.RaceSubCategory>();
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.RaceSubCategory obj = new FingerprintsModel.FamilyHousehold.RaceSubCategory();
                                obj.RaceSubCategoryID = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _racelist.Add(obj);
                            }
                            Info.raceCategory = _racelist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.Relationship> _relationlist = new List<FingerprintsModel.FamilyHousehold.Relationship>();
                            foreach (DataRow dr in ds.Tables[2].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.Relationship obj = new FingerprintsModel.FamilyHousehold.Relationship();
                                obj.Id = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _relationlist.Add(obj);
                            }
                            //  _rolelist.Insert(0, new Role() { RoleId = "0", RoleName = "Select" });
                            Info.relationship = _relationlist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        try
                        {
                            List<FamilyHousehold.RaceInfo> _racelist = new List<FamilyHousehold.RaceInfo>();
                            //_staff.myList
                            foreach (DataRow dr in ds.Tables[3].Rows)
                            {
                                FamilyHousehold.RaceInfo obj = new FamilyHousehold.RaceInfo();
                                obj.RaceId = dr["Id"].ToString();
                                obj.Name = dr["Name"].ToString();
                                _racelist.Add(obj);

                            }
                            //_racelist.Insert(0, new RaceInfo() { RaceId = "0", Name = "Select" });
                            Info.raceList = _racelist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }

                    if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    {
                        List<FamilyHousehold.Programdetail> Programs = new List<FamilyHousehold.Programdetail>();
                        foreach (DataRow dr in ds.Tables[4].Rows)
                        {
                            FamilyHousehold.Programdetail obj = new FamilyHousehold.Programdetail();
                            obj.Id = Convert.ToInt32(dr["programtypeid"]);
                            obj.Name = dr["programtype"].ToString();
                            obj.ReferenceId = dr["ReferenceId"].ToString();
                            Programs.Add(obj);
                        }
                        Info.AvailableProgram = Programs;
                    }
                    if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                        foreach (DataRow dr in ds.Tables[5].Rows)
                        {
                            FamilyHousehold.ImmunizationRecord obj = new FamilyHousehold.ImmunizationRecord();
                            obj.Dose = dr["Immunization"].ToString();
                            obj.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationid"]);
                            ImmunizationRecords.Add(obj);
                        }

                        Info.ImmunizationRecords = ImmunizationRecords;
                    }
                    if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDirectBloodRelative> ChildHealth = new List<FamilyHousehold.ChildDirectBloodRelative>();
                        foreach (DataRow dr in ds.Tables[6].Rows)
                        {
                            FamilyHousehold.ChildDirectBloodRelative obj = new FamilyHousehold.ChildDirectBloodRelative();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildHealth.Add(obj);
                        }
                        Info.AvailableDisease = ChildHealth;
                    }
                    if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDiagnosedDisease> ChildDiagnosedHealth = new List<FamilyHousehold.ChildDiagnosedDisease>();
                        foreach (DataRow dr in ds.Tables[7].Rows)
                        {
                            FamilyHousehold.ChildDiagnosedDisease obj = new FamilyHousehold.ChildDiagnosedDisease();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDiagnosedHealth.Add(obj);
                        }
                        Info.AvailableDiagnosedDisease = ChildDiagnosedHealth;
                    }
                    if (ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDental> ChildDental = new List<FamilyHousehold.ChildDental>();
                        foreach (DataRow dr in ds.Tables[8].Rows)
                        {
                            FamilyHousehold.ChildDental obj = new FamilyHousehold.ChildDental();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDental.Add(obj);
                        }
                        Info.AvailableDental = ChildDental;
                    }
                    if (ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDental> ChildDental = new List<FamilyHousehold.ChildDental>();
                        foreach (DataRow dr in ds.Tables[8].Rows)
                        {
                            FamilyHousehold.ChildDental obj = new FamilyHousehold.ChildDental();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDental.Add(obj);
                        }
                        Info.AvailableDental = ChildDental;
                    }
                    if (ds.Tables[9] != null && ds.Tables[9].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildEHS> ChildEHSInfo = new List<FamilyHousehold.ChildEHS>();
                        foreach (DataRow dr in ds.Tables[9].Rows)
                        {
                            FamilyHousehold.ChildEHS obj = new FamilyHousehold.ChildEHS();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildEHSInfo.Add(obj);
                        }
                        Info.AvailableEHS = ChildEHSInfo;
                    }
                    if (ds.Tables[10] != null && ds.Tables[10].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDietInfo> ChildDietDetails = new List<FamilyHousehold.ChildDietInfo>();
                        foreach (DataRow dr in ds.Tables[10].Rows)
                        {
                            FamilyHousehold.ChildDietInfo obj = new FamilyHousehold.ChildDietInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["DietInfo"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDietDetails.Add(obj);
                        }
                        Info.dietList = ChildDietDetails;
                    }
                    if (ds.Tables[11] != null && ds.Tables[11].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildDrink> ChildDrinkDetails = new List<FamilyHousehold.ChildDrink>();
                        foreach (DataRow dr in ds.Tables[11].Rows)
                        {
                            FamilyHousehold.ChildDrink obj = new FamilyHousehold.ChildDrink();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            ChildDrinkDetails.Add(obj);
                        }
                        Info.AvailableChildDrink = ChildDrinkDetails;
                    }
                    if (ds.Tables[12] != null && ds.Tables[12].Rows.Count > 0)
                    {
                        List<FamilyHousehold.ChildFoodInfo> ChildFoodDetails = new List<FamilyHousehold.ChildFoodInfo>();
                        foreach (DataRow dr in ds.Tables[12].Rows)
                        {
                            FamilyHousehold.ChildFoodInfo obj = new FamilyHousehold.ChildFoodInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Category"].ToString();
                            ChildFoodDetails.Add(obj);
                        }
                        Info.foodList = ChildFoodDetails;
                    }



                    if (ds.Tables[13] != null && ds.Tables[13].Rows.Count > 0)
                    {
                        List<SchoolDistrict> schooldistrict = new List<SchoolDistrict>();
                        foreach (DataRow dr in ds.Tables[13].Rows)
                        {
                            SchoolDistrict info = new SchoolDistrict();
                            info.SchoolDistrictID = Convert.ToInt32(dr["SchoolDistrictID"]);
                            info.Acronym = dr["Acronym"].ToString();
                            schooldistrict.Add(info);
                        }
                        Info.SchoolList = schooldistrict;
                    }
                    if (ds.Tables[14] != null && ds.Tables[14].Rows.Count > 0)
                    {
                        List<Nurse.ChildFeedCerealInfo> ChildCerealDetails = new List<Nurse.ChildFeedCerealInfo>();
                        foreach (DataRow dr in ds.Tables[14].Rows)
                        {
                            Nurse.ChildFeedCerealInfo obj = new Nurse.ChildFeedCerealInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildCerealDetails.Add(obj);
                        }
                        Info.CFeedCerealList = ChildCerealDetails;
                    }
                    if (ds.Tables[15] != null && ds.Tables[15].Rows.Count > 0)
                    {
                        List<Nurse.ChildReferalCriteriaInfo> ChildReferalDetails = new List<Nurse.ChildReferalCriteriaInfo>();
                        foreach (DataRow dr in ds.Tables[15].Rows)
                        {
                            Nurse.ChildReferalCriteriaInfo obj = new Nurse.ChildReferalCriteriaInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildReferalDetails.Add(obj);
                        }
                        Info.CReferalCriteriaList = ChildReferalDetails;
                    }
                    if (ds.Tables[16] != null && ds.Tables[16].Rows.Count > 0)
                    {
                        List<Nurse.ChildFormulaInfo> ChildFormulaDetails = new List<Nurse.ChildFormulaInfo>();
                        foreach (DataRow dr in ds.Tables[16].Rows)
                        {
                            Nurse.ChildFormulaInfo obj = new Nurse.ChildFormulaInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildFormulaDetails.Add(obj);
                        }
                        Info.CFormulaList = ChildFormulaDetails;
                    }
                    if (ds.Tables[17] != null && ds.Tables[17].Rows.Count > 0)
                    {
                        List<Nurse.ChildFeedInfo> ChildFedDetails = new List<Nurse.ChildFeedInfo>();
                        foreach (DataRow dr in ds.Tables[17].Rows)
                        {
                            Nurse.ChildFeedInfo obj = new Nurse.ChildFeedInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildFedDetails.Add(obj);
                        }
                        Info.CFeedList = ChildFedDetails;
                    }
                    if (ds.Tables[18] != null && ds.Tables[18].Rows.Count > 0)
                    {
                        List<Nurse.ChildDietFull> ChildDietFullDetails = new List<Nurse.ChildDietFull>();
                        foreach (DataRow dr in ds.Tables[18].Rows)
                        {
                            Nurse.ChildDietFull obj = new Nurse.ChildDietFull();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            ChildDietFullDetails.Add(obj);
                        }
                        Info.AvailableChildDietFull = ChildDietFullDetails;
                    }
                    if (ds.Tables[19] != null && ds.Tables[19].Rows.Count > 0)
                    {
                        List<Nurse.ChildVitamin> ChildVitaminDetails = new List<Nurse.ChildVitamin>();
                        foreach (DataRow dr in ds.Tables[19].Rows)
                        {
                            Nurse.ChildVitamin obj = new Nurse.ChildVitamin();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            ChildVitaminDetails.Add(obj);
                        }
                        Info.AvailableChildVitamin = ChildVitaminDetails;
                    }
                    if (ds.Tables[20] != null && ds.Tables[20].Rows.Count > 0)
                    {
                        List<Nurse.ChildHungryInfo> ChildHungryDetails = new List<Nurse.ChildHungryInfo>();
                        foreach (DataRow dr in ds.Tables[20].Rows)
                        {
                            Nurse.ChildHungryInfo obj = new Nurse.ChildHungryInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            ChildHungryDetails.Add(obj);
                        }
                        Info.ChungryList = ChildHungryDetails;
                    }
                    if (ds.Tables[21] != null && ds.Tables[21].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMConditionsInfo> PMCondtnDetails = new List<FamilyHousehold.PMConditionsInfo>();
                        foreach (DataRow dr in ds.Tables[21].Rows)
                        {
                            FamilyHousehold.PMConditionsInfo obj = new FamilyHousehold.PMConditionsInfo();
                            obj.Id = (dr["ID"]).ToString();
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMCondtnDetails.Add(obj);
                        }
                        Info.PMCondtnList = PMCondtnDetails;
                    }
                    if (ds.Tables[22] != null && ds.Tables[22].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMProblems> PMPrblmDetails = new List<FamilyHousehold.PMProblems>();
                        foreach (DataRow dr in ds.Tables[22].Rows)
                        {
                            FamilyHousehold.PMProblems obj = new FamilyHousehold.PMProblems();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMPrblmDetails.Add(obj);
                        }
                        Info.AvailablePrblms = PMPrblmDetails;
                    }
                    if (ds.Tables[23] != null && ds.Tables[23].Rows.Count > 0)
                    {
                        List<FamilyHousehold.PMService> PMServiceDetails = new List<FamilyHousehold.PMService>();
                        foreach (DataRow dr in ds.Tables[23].Rows)
                        {
                            FamilyHousehold.PMService obj = new FamilyHousehold.PMService();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["Description"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            PMServiceDetails.Add(obj);
                        }
                        Info.AvailableService = PMServiceDetails;
                    }

                    if (ds.Tables[24] != null && ds.Tables[24].Rows.Count > 0)
                    {
                        List<FamilyHousehold.AssignedTo> _clientlist = new List<FamilyHousehold.AssignedTo>();
                        foreach (DataRow dr in ds.Tables[24].Rows)
                        {
                            FamilyHousehold.AssignedTo obj = new FamilyHousehold.AssignedTo();
                            obj.Id = (dr["UserId"]).ToString();
                            obj.Name = dr["Name"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            _clientlist.Add(obj);
                        }
                        Info.ClientAssignedTo = _clientlist;
                    }


                    if (ds.Tables[25] != null && ds.Tables[25].Rows.Count > 0)
                    {

                        foreach (DataRow dr in ds.Tables[25].Rows)
                        {
                            Info.CTransportNeeded = Convert.ToBoolean(dr["ChildTransport"]);
                        }

                    }
                    if (ds.Tables[26] != null && ds.Tables[26].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[26].Rows)
                        {

                            Info.Cfirstname = dr["Firstname"].ToString();
                            Info.Cmiddlename = dr["Middlename"].ToString();
                            Info.Clastname = dr["Lastname"].ToString();
                            Info.CGender = dr["Gender"].ToString();
                            if (dr["DOB"].ToString() != "")
                                Info.CDOB = Convert.ToDateTime(dr["DOB"]).ToString("MM/dd/yyyy");
                            try
                            {
                                Info.CSSN = dr["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(dr["SSN"].ToString());
                            }
                            catch (Exception ex)
                            {
                                clsError.WriteException(ex);
                                Info.CSSN = dr["SSN"].ToString();
                            }
                        }
                    }

                    if (ds.Tables[27] != null && ds.Tables[27].Rows.Count > 0)
                    {
                        Info.customscreening = ds.Tables[27];

                    }


                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return Info;
        }
        public string DropClient(string ClientId, string HouseholdId, string status, string Reason, string StatusText, string ddlreason, string ddlreasontext, string userid, string Agencyid, string IsWaiting)
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", HouseholdId));
                command.Parameters.Add(new SqlParameter("@status", status));
                command.Parameters.Add(new SqlParameter("@Reason", Reason));
                command.Parameters.Add(new SqlParameter("@StatusText", StatusText));
                command.Parameters.Add(new SqlParameter("@ddlreason", ddlreason));
                command.Parameters.Add(new SqlParameter("@ddlreasontext", ddlreasontext));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@IsWaiting", string.IsNullOrEmpty(IsWaiting) ? false : IsWaiting == "1" ? true : false));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_enrolldropchild";
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();

            }

        }
        public List<FingerprintsModel.FamilyHousehold.EnrollmentChangeReason> GetEnrollReason(string Status, string UserId, string Agencyid)
        {
            List<FingerprintsModel.FamilyHousehold.EnrollmentChangeReason> _EnrollmentChangeReasonlist = new List<FingerprintsModel.FamilyHousehold.EnrollmentChangeReason>();
            if (!String.IsNullOrWhiteSpace(Status))
            {
                try
                {
                    command.Parameters.Add(new SqlParameter("@Status", Status));
                    command.Parameters.Add(new SqlParameter("@UserId", UserId));
                    command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                    command.Connection = Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "Sp_GetEnrollmentReason";
                    DataAdapter = new SqlDataAdapter(command);
                    _dataset = new DataSet();
                    DataAdapter.Fill(_dataset);
                    if (_dataset != null && _dataset.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            FingerprintsModel.FamilyHousehold.EnrollmentChangeReason obj = new FingerprintsModel.FamilyHousehold.EnrollmentChangeReason();
                            obj.ReasonID = dr["TagKey"].ToString();
                            obj.ReasonText = dr["Description"].ToString();
                            _EnrollmentChangeReasonlist.Add(obj);
                        }
                    }
                    DataAdapter.Dispose();
                    command.Dispose();
                }
                catch (Exception ex)
                {
                    clsError.WriteException(ex);
                }
                finally
                {
                    DataAdapter.Dispose();
                    command.Dispose();
                }

            }
            return _EnrollmentChangeReasonlist;
        }
        public string GeneratehouseholdeditRequest(string HouseHoldId, string agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GenerateRequest";
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public string CalculateBmi(string Gender, string Input, string Dob, string AssessmentDate, string Height, string Weight, string Headcir, string agencyid, string userid)
        {

            try
            {
                command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetBmi";
                command.Parameters.Add(new SqlParameter("@Gender", Gender));
                command.Parameters.Add(new SqlParameter("@Input", Input));
                command.Parameters.Add(new SqlParameter("@Dob", Dob));
                command.Parameters.Add(new SqlParameter("@AssessmentDate", AssessmentDate));
                command.Parameters.Add(new SqlParameter("@Height", Height));
                command.Parameters.Add(new SqlParameter("@Weight", Weight));
                int circ = 0;
                if (int.TryParse(Headcir, out circ))
                {
                    command.Parameters.Add(new SqlParameter("@Headcir", Headcir));
                }
                else
                {
                }
                //command.Parameters.Add(new SqlParameter("@Headcir", Headcir));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.Parameters["@result"].Size = 10;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public DataTable GetnursescreeningDashboard(string Agencyid, string userid)
        {
            try
            {
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_NurseClientcenterList";
                DataAdapter = new SqlDataAdapter(command);
                _dataTable = new DataTable();
                DataAdapter.Fill(_dataTable);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return _dataTable;

            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
            return _dataTable;

        }
        public string Deletefamily(string HouseHoldId, string Agencyid, string userid)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deletefamily";
                command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseHoldId));
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        public FamilyHousehold GetCustompdfimageScreen(string Agencyid, string screeningid, string clientid)
        {
            FamilyHousehold obj = new FamilyHousehold();
            try
            {
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@clientid", clientid));
                command.Parameters.Add(new SqlParameter("@screeningid", screeningid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getcustomimagescreening";
                DataAdapter = new SqlDataAdapter(command);
                //Due to Phone Type
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null && _dataset.Tables.Count > 0)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        obj.EImageByte = (byte[])_dataset.Tables[0].Rows[0]["imagebyte"];
                        obj.EFileExtension = _dataset.Tables[0].Rows[0]["imageExt"].ToString();
                        obj.EFileName = _dataset.Tables[0].Rows[0]["imagename"].ToString();
                    }
                }
                DataAdapter.Dispose();
                command.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return obj;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
            }
        }
        //Added on 26Dec2016
        public FamilyHousehold GetCenterData(string agencyid, string Id)
        {
            //  List<AgencyStaff> _agencyStafflist = new List<AgencyStaff>();
            FamilyHousehold Info = new FamilyHousehold();

            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "Sp_Sel_Center_Dropdowndata";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@AgencyId", agencyid));
                        command.Parameters.Add(new SqlParameter("@HouseHoldId", Id));

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            List<FingerprintsModel.FamilyHousehold.CenterData> _centerlist = new List<FingerprintsModel.FamilyHousehold.CenterData>();
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                FingerprintsModel.FamilyHousehold.CenterData obj = new FingerprintsModel.FamilyHousehold.CenterData();
                                obj.Id = dr["center"].ToString();
                                obj.Name = dr["centername"].ToString();
                                _centerlist.Add(obj);
                            }
                            Info.Centers = _centerlist;
                        }
                        catch (Exception ex)
                        {
                            clsError.WriteException(ex);
                        }
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        List<FamilyHousehold.WorkshopDetails> WorkshopDetails = new List<FamilyHousehold.WorkshopDetails>();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            FamilyHousehold.WorkshopDetails obj = new FamilyHousehold.WorkshopDetails();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["WorkshopName"].ToString();
                            // obj.ReferenceId = dr["Description"].ToString();
                            WorkshopDetails.Add(obj);
                        }
                        Info.AvailableWorkshop = WorkshopDetails;
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {

                        Info.WorkshopId = Convert.ToInt32(ds.Tables[2].Rows[0]["ID"]);
                        Info.WorkshopDate = Convert.ToString(ds.Tables[2].Rows[0]["Date"]);//.ToString("MM/dd/yyyy");
                        Info.CenterDetails = Convert.ToString(ds.Tables[2].Rows[0]["Center"]);

                        // Info.EditAllowed = Convert.ToInt32(_dataset.Tables[2].Rows[0]["EditAllowed"]);
                        List<FamilyHousehold.WorkshopDetails> _workshopinfo = new List<FamilyHousehold.WorkshopDetails>();
                        FamilyHousehold.WorkshopDetails obj = null;
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            obj = new FamilyHousehold.WorkshopDetails();
                            obj.Id = Convert.ToInt32(dr["ID"]);
                            obj.Name = dr["WorkshopName"].ToString();
                            obj.IsSelected = true;

                            _workshopinfo.Add(obj);
                        }
                        Info.AvailableWorkshopDetails = _workshopinfo;
                    }



                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return Info;
        }


        public List<string> GetWellBabyDetail(string HouseholdId, string Agencyid, string ChildId, string WellBabyMonth, string serverpath)
        {

            List<string> PhyValues = new List<string>();
            Screening _Screening = new Screening();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "SP_GetWellBabyDetails";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@AgencyId", Agencyid));
                        command.Parameters.Add(new SqlParameter("@HouseHoldId", EncryptDecrypt.Decrypt64(HouseholdId)));
                        command.Parameters.Add(new SqlParameter("@ChildId", EncryptDecrypt.Decrypt64(ChildId)));
                        command.Parameters.Add(new SqlParameter("@WellBabyMonth", WellBabyMonth));
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                    string PhysicalFileName = "", PhysicalImagejson = "", PhysicalFileExtension = "", imageDataURL = "";
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        PhysicalFileName = ds.Tables[1].Rows[0]["PhyImageUl"].ToString();
                        PhysicalImagejson = ds.Tables[1].Rows[0]["PhyImage"].ToString() == "" ? "" : Convert.ToBase64String((byte[])ds.Tables[1].Rows[0]["PhyImage"]);
                        imageDataURL = PhysicalImagejson;
                        PhysicalFileExtension = ds.Tables[1].Rows[0]["PhyFileExtension"].ToString();
                        string Url = Guid.NewGuid().ToString();
                        if (PhysicalFileName != "" && PhysicalFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])ds.Tables[1].Rows[0]["PhyImage"], 0, ((byte[])ds.Tables[1].Rows[0]["PhyImage"]).Length);
                            file.Close();
                            PhysicalImagejson = "/TempAttachment/" + Url + ".pdf";

                        }

                    }
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string val = Convert.ToString(dr["Value"]);

                            PhyValues.Add(val);
                        }

                    }
                    PhyValues.Add(PhysicalFileName);
                    PhyValues.Add(imageDataURL);
                    PhyValues.Add(PhysicalFileExtension);

                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return PhyValues;
        }
        //Added on 26Dec2016 //update by atul 27-3-2017
        public string AddWorkshopInfo(string Center, FamilyHousehold info, string HouseholdId, string WorkshopId, string daypreference, string timepreference, string userId, string AgencyId)//, , string Date,string Community, string DentalCenter, string DentalNotes, string MedicalNotes, string MedicalCenter, string CompanyNameden)
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandText = "SP_addWorkshopClient";
                if (WorkshopId != "0")
                {
                    command.Parameters.AddWithValue("@mode", 1);
                }
                else
                {
                    command.Parameters.AddWithValue("@mode", 0);
                }
                command.Parameters.AddWithValue("@ID", WorkshopId);
                command.Parameters.AddWithValue("@WorkshopInfo", info.WorkshopInfo);
                command.Parameters.AddWithValue("@AgencyId", AgencyId);
                command.Parameters.AddWithValue("@Center", Center);
                command.Parameters.AddWithValue("@daypreference", daypreference);
                command.Parameters.AddWithValue("@timepreference", timepreference);
                //  command.Parameters.AddWithValue("@Date", Date);//Changes on 28Dec2016
                command.Parameters.AddWithValue("@HouseholdId", HouseholdId);


                command.Parameters.AddWithValue("@CreatedBy", userId);

                command.Parameters.AddWithValue("@result", "").Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }







            return command.Parameters["@result"].Value.ToString();
        }
        //End
        //Added on 27Dec2016
        public FamilyHousehold getWorkshopInfo(string HouseholdId, string Agencyid)
        {
            FamilyHousehold Info = new FamilyHousehold();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "Sp_Sel_Center_Dropdowndata";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@AgencyId", Agencyid));
                        command.Parameters.Add(new SqlParameter("@HouseHoldId", HouseholdId));
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        List<FamilyHousehold.WorkshopDetails> _workshopinfo = new List<FamilyHousehold.WorkshopDetails>();
                        FamilyHousehold.WorkshopDetails obj = null;
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            obj = new FamilyHousehold.WorkshopDetails();
                            obj.Id = Convert.ToInt32(dr["WorkshopId"]);
                            obj.Name = dr["WorkshopName"].ToString();
                            obj.IsSelected = true;

                            _workshopinfo.Add(obj);
                        }
                        Info.AvailableWorkshopDetails = _workshopinfo;
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return Info;
        }
        public DataSet GetCenterCaseNote(string Agencyid, string userid)
        {
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Getcentercasenotes";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
            return _dataset;
        }
        public string SaveGroupCaseNotes(RosterNew.CaseNote CaseNote, List<RosterNew.Attachment> Attachments, string Agencyid, string UserID, string Roleid)
        {
            string result = string.Empty;

            try
            {
                string HouseholdId = string.Empty;
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@CenterId", CaseNote.CenterId));
                command.Parameters.Add(new SqlParameter("@Classroomid", CaseNote.Classroomid));
                command.Parameters.Add(new SqlParameter("@CNoteid", CaseNote.CaseNoteid));
                command.Parameters.Add(new SqlParameter("@Note", CaseNote.Note));
                command.Parameters.Add(new SqlParameter("@CaseNoteDate", CaseNote.CaseNoteDate));
                command.Parameters.Add(new SqlParameter("@CaseNoteSecurity", CaseNote.CaseNoteSecurity));
                command.Parameters.Add(new SqlParameter("@CaseNotetags", CaseNote.CaseNotetags));
                command.Parameters.Add(new SqlParameter("@CaseNotetitle", CaseNote.CaseNotetitle));
                command.Parameters.Add(new SqlParameter("@ClientIds", CaseNote.ClientIds));
                command.Parameters.Add(new SqlParameter("@StaffIds", CaseNote.StaffIds));
                command.Parameters.Add(new SqlParameter("@userid", UserID));
                command.Parameters.Add(new SqlParameter("@RoleId", Roleid));
                command.Parameters.Add(new SqlParameter("@agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("Attachment", typeof(byte[])),
                      new DataColumn("AttachmentName",typeof(string)),
                        new DataColumn("Attachmentextension",typeof(string))
                    });
                foreach (RosterNew.Attachment Attachment in Attachments)
                {
                    if (Attachment != null && Attachment.file != null)
                    {
                        dt.Rows.Add(new BinaryReader(Attachment.file.InputStream).ReadBytes(Attachment.file.ContentLength), Attachment.file.FileName, Path.GetExtension(Attachment.file.FileName));

                    }
                }
                command.Parameters.Add(new SqlParameter("@Attachments", dt));
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SaveGroupCaseNote";
                command.ExecuteNonQuery();

                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                if (Connection != null)
                    Connection.Close();

            }
            return result;





        }
        //Added on 13 jan2017
        public string AddSchedulerDetails(string Description, string Date, string StartTime, string Duration, string EndTime, string Days, string ClientId, string notes, string MeetingId, string userId, string AgencyId, bool isyakkr600601, bool updateEnroll = false, bool isRecurring = false)//, , string Date,string Community, string DentalCenter, string DentalNotes, string MedicalNotes, string MedicalCenter, string CompanyNameden)
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandText = "SP_addSchedularDetails";

                command.Parameters.AddWithValue("@ID", MeetingId);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@Date", Date);
                command.Parameters.AddWithValue("@StartTime", StartTime);
                command.Parameters.AddWithValue("@EndTime", EndTime);
                command.Parameters.AddWithValue("@Duration", Duration);
                command.Parameters.AddWithValue("@ClientId", ClientId);
                command.Parameters.AddWithValue("@AgencyId", AgencyId);
                command.Parameters.AddWithValue("@Day", Days);
                command.Parameters.AddWithValue("@Notes", notes);
                //  command.Parameters.AddWithValue("@Date", Date);//Changes on 28Dec2016
                // command.Parameters.AddWithValue("@HouseholdId", HouseholdId);
                command.Parameters.AddWithValue("@IsUpdateEnroll", updateEnroll);
                command.Parameters.AddWithValue("@IsRecurring", isRecurring);
                command.Parameters.AddWithValue("@isyakkr600601", isyakkr600601);
                command.Parameters.AddWithValue("@CreatedBy", userId);
                command.Parameters.AddWithValue("@result", "").Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }







            return command.Parameters["@result"].Value.ToString();
        }
        public List<Scheduler> ScheduleInfo(out string totalrecord, out int todayAppointmentCount, string sortOrder, string sortDirection, string ClientId, string Day, int skip, int pageSize, string agencyId, string userid)
        {
            List<Scheduler> _Schedulerlist = new List<Scheduler>();
            todayAppointmentCount = 0;
            try
            {
                totalrecord = string.Empty;

                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                if (Day != "")
                {
                    command.Parameters.Add(new SqlParameter("@Days", Day));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@Days", null));
                }

                command.Parameters.Add(new SqlParameter("@take", pageSize));
                command.Parameters.Add(new SqlParameter("@skip", skip));
                command.Parameters.Add(new SqlParameter("@sortcolumn", sortOrder));
                command.Parameters.Add(new SqlParameter("@sortorder", sortDirection));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyId));
                command.Parameters.Add(new SqlParameter("@totalRecord", 0)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@TodayRecordCount", 0)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@userId", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Schedule_list";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    Scheduler addSchedulerRow = new Scheduler();

                    _Schedulerlist = (from DataRow dr in familydataTable.Rows
                                      select new Scheduler
                                      {
                                          MeetingId = Convert.ToInt32(dr["ID"]),
                                          MeetingDescription = Convert.ToString(dr["Description"]),
                                          StartTime = Convert.ToString(dr["StartTime"]),
                                          EndTime = Convert.ToString(dr["EndTime"]),
                                          MeetingDate = string.IsNullOrEmpty(dr["Date"].ToString()) ? "" : Convert.ToDateTime(dr["Date"]).ToString("MM/dd/yyyy")
                                      }
                                      ).ToList();
                    //for (int i = 0; i < familydataTable.Rows.Count; i++)
                    //{

                    //    addSchedulerRow.MeetingId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                    //    addSchedulerRow.MeetingDescription = Convert.ToString(familydataTable.Rows[i]["Description"]);
                    //    addSchedulerRow.StartTime = (Convert.ToString(familydataTable.Rows[i]["StartTime"]));
                    //    addSchedulerRow.EndTime = (Convert.ToString(familydataTable.Rows[i]["EndTime"]));
                    //    //int idx = Convert.ToString(familydataTable.Rows[i]["StartTime"]).LastIndexOf(':');

                    //    //addSchedulerRow.StartTime = (Convert.ToString(familydataTable.Rows[i]["StartTime"]).Substring(0, idx));

                    //    //int idxend = Convert.ToString(familydataTable.Rows[i]["EndTime"]).LastIndexOf(':');//Changes on 19Jan2017

                    //    //addSchedulerRow.EndTime = (Convert.ToString(familydataTable.Rows[i]["EndTime"]).Substring(0, idxend));


                    //    addSchedulerRow.MeetingDate =string.IsNullOrEmpty(familydataTable.Rows[i]["Date"].ToString())?"": Convert.ToDateTime(familydataTable.Rows[i]["Date"]).ToString("MM/dd/yyyy");

                    //    _Schedulerlist.Add(addSchedulerRow);
                    //}


                    totalrecord = command.Parameters["@totalRecord"].Value.ToString();

                    todayAppointmentCount = Convert.ToInt32(command.Parameters["@TodayRecordCount"].Value.ToString());
                }

                var list = _Schedulerlist.Where(x => Convert.ToDateTime(x.MeetingDate).ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).ToList();

                list.AddRange(_Schedulerlist.Where(x => Convert.ToDateTime(x.MeetingDate).ToString("MM/dd/yyyy") != DateTime.Now.ToString("MM/dd/yyyy")).OrderByDescending(x => Convert.ToDateTime(x.MeetingDate)).ToList());

                _Schedulerlist = list;

                return _Schedulerlist;
            }
            catch (Exception ex)
            {
                totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _Schedulerlist;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        public string Deletescheduleinfo(string ID, string AgencyId)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deletescheduleinfo";
                command.Parameters.Add(new SqlParameter("@ID", ID));
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            finally
            {
                command.Dispose();

            }
        }
        //Changes on 25Jan2017
        public Scheduler getscheduleinfo(string Id, string userId, string agencyid)
        {
            try
            {
                Scheduler obj = new Scheduler();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_getSchedularInfo";
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@ID", Id));
                command.Parameters.Add(new SqlParameter("@UserId", userId));
                command.Parameters.Add(new SqlParameter("@AgencyId", agencyid));
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);

                if (_dataset != null)
                {
                    familydataTable = new DataTable();
                    familydataTable = _dataset.Tables[0];
                    obj.ParentDetailsList = new List<ParentDetails>();


                    if (familydataTable.Rows.Count > 0)
                    {

                        obj.ParentDetailsList = new List<ParentDetails>();

                        obj.MeetingId = Convert.ToInt32(familydataTable.Rows[0]["ID"]);
                        obj.ClientId = Convert.ToInt32(familydataTable.Rows[0]["ClientId"]);
                        obj.Enc_ClientId = EncryptDecrypt.Encrypt64(familydataTable.Rows[0]["ClientId"].ToString());
                        obj.MeetingDescription = Convert.ToString(familydataTable.Rows[0]["Description"]);
                        int idx = Convert.ToString(familydataTable.Rows[0]["StartTime"]).LastIndexOf(':');
                        obj.StartTime = Convert.ToString(familydataTable.Rows[0]["StartTime"]);
                        obj.ClientName = Convert.ToString(familydataTable.Rows[0]["ClientName"]);//added on 25Jan2017
                        obj.EndTime = Convert.ToString(familydataTable.Rows[0]["EndTime"]);
                        obj.Duration = Convert.ToString(familydataTable.Rows[0]["Duration"]);
                        obj.Day = Convert.ToString(familydataTable.Rows[0]["Day"]);
                        obj.MeetingDate = Convert.ToDateTime(familydataTable.Rows[0]["Date"]).ToString("MM/dd/yyyy");
                        obj.MeetingNotes = Convert.ToString(familydataTable.Rows[0]["Notes"]);
                        obj.CenterId = Convert.ToInt64(familydataTable.Rows[0]["CenterId"]);
                        obj.CenterId = Convert.ToInt64(familydataTable.Rows[0]["CenterId"]);
                        obj.Enc_CenterId = EncryptDecrypt.Encrypt64(familydataTable.Rows[0]["CenterId"].ToString());
                        obj.ClassRoomId = Convert.ToInt64(familydataTable.Rows[0]["ClassRoomId"]);
                        obj.Enc_ClassRoomId = EncryptDecrypt.Encrypt64(familydataTable.Rows[0]["ClassRoomId"].ToString());
                        obj.ProgramTypeId = Convert.ToInt64(familydataTable.Rows[0]["ProgramID"]);
                        obj.Enc_ProgramTypeId = EncryptDecrypt.Encrypt64(familydataTable.Rows[0]["ProgramID"].ToString());
                        obj.HouseholdId = Convert.ToInt64(familydataTable.Rows[0]["HouseholdId"]);
                        obj.Enc_HouseholdId = EncryptDecrypt.Encrypt64(familydataTable.Rows[0]["HouseholdId"].ToString());
                        obj.ParentDetailsList = (from DataRow dr in _dataset.Tables[1].Rows
                                                 select new ParentDetails
                                                 {
                                                     ParentName = dr["ParentName"].ToString(),
                                                     ParentRole = dr["ParentRole"].ToString(),
                                                     ClientId = dr["ClientId"].ToString(),
                                                     ProfilePicture = dr["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])dr["ProfilePic"])
                                                 }
                                               ).ToList();
                        obj.RosterYakkr = familydataTable.Rows[0]["yakkr"].ToString();

                    }

                    if (_dataset.Tables[2].Rows.Count > 0)
                    {
                        obj.TimeZone = Convert.ToInt32(_dataset.Tables[2].Rows[0]["UTCMINUTEDIFFERENC"]).ToString();
                    }
                    else
                    {
                        obj.TimeZone = "";
                    }
                    obj.AttendanceTypeList = new List<AttendanceType>();
                    if (_dataset.Tables[3].Rows.Count > 0)
                    {
                        obj.AttendanceTypeList = (from DataRow dr4 in _dataset.Tables[3].Rows

                                                  select new AttendanceType
                                                  {
                                                      AttendanceTypeId = Convert.ToInt64(dr4["AttendanceTypeId"]),
                                                      Description = dr4["Description"].ToString(),
                                                      Acronym = dr4["Acronym"].ToString()
                                                  }
                                                ).ToList();
                    }

                }

                return obj;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return null;
            }
            finally
            {
                command.Dispose();

            }
        }
        //Changes on 23Jan2017
        public List<Scheduler> AppointmentInfo(out string totalrecord, out string timeZoneDiff, string sortOrder, string sortDirection, string search, int skip, int pageSize, string agencyId, string userId)
        {
            List<Scheduler> _Schedulerlist = new List<Scheduler>();
            try
            {
                totalrecord = string.Empty;
                timeZoneDiff = string.Empty;
                string searchDetail = string.Empty;
                if (string.IsNullOrEmpty(search.Trim()))
                    searchDetail = string.Empty;
                else
                    searchDetail = search;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@Search", searchDetail));
                command.Parameters.Add(new SqlParameter("@take", pageSize));
                command.Parameters.Add(new SqlParameter("@skip", skip));
                command.Parameters.Add(new SqlParameter("@sortcolumn", sortOrder));
                command.Parameters.Add(new SqlParameter("@sortorder", sortDirection));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyId));
                command.Parameters.Add(new SqlParameter("@userId", userId));
                command.Parameters.Add(new SqlParameter("@totalRecord", 0)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@timeZoneDiff", 0)).Direction = ParameterDirection.Output;
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Appointment_list";
                DataAdapter = new SqlDataAdapter(command);
                familydataTable = new DataTable();
                DataAdapter.Fill(familydataTable);
                if (familydataTable.Rows.Count > 0)
                {
                    //for (int i = 0; i < familydataTable.Rows.Count; i++)
                    //{
                    //    Scheduler addSchedulerRow = new Scheduler();

                    //    addSchedulerRow.MeetingId = Convert.ToInt32(familydataTable.Rows[i]["ID"]);
                    //    addSchedulerRow.MeetingDescription = Convert.ToString(familydataTable.Rows[i]["Description"]);
                    //    addSchedulerRow.ClientName = Convert.ToString(familydataTable.Rows[i]["ClientName"]);
                    //    addSchedulerRow.ClientId = Convert.ToInt32(familydataTable.Rows[i]["ClientID"]);
                    //    addSchedulerRow.StartTime = Convert.ToString(familydataTable.Rows[i]["StartTime"]);//Changes on 23Jan2017
                    //    addSchedulerRow.EndTime = Convert.ToString(familydataTable.Rows[i]["EndTime"]);//Changes on 23Jan2017
                    //    //int idx = Convert.ToString(familydataTable.Rows[i]["StartTime"]).LastIndexOf(':');

                    //    //addSchedulerRow.StartTime = (Convert.ToString(familydataTable.Rows[i]["StartTime"]).Substring(0, idx));

                    //    //int idxend = Convert.ToString(familydataTable.Rows[i]["EndTime"]).LastIndexOf(':');

                    //    //addSchedulerRow.EndTime = (Convert.ToString(familydataTable.Rows[i]["EndTime"]).Substring(0, idxend));
                    //    addSchedulerRow.MeetingNotes = Convert.ToString(familydataTable.Rows[i]["Notes"]);

                    //    addSchedulerRow.MeetingDate = Convert.ToDateTime(familydataTable.Rows[i]["Date"]).ToString("MM/dd/yyyy");

                    //    addSchedulerRow.HouseholdAddress = familydataTable.Rows[i]["HouseholdAddress"].ToString();
                    //    addSchedulerRow.HouseholdPhoneNo = familydataTable.Rows[i]["HouseholdPhoneNo"].ToString().Replace(",", "<br>");
                    //    _Schedulerlist.Add(addSchedulerRow);
                    //}

                    _Schedulerlist = (from DataRow dr in familydataTable.Rows

                                      select new Scheduler
                                      {
                                          MeetingId = Convert.ToInt32(dr["ID"]),
                                          MeetingDescription = Convert.ToString(dr["Description"]),
                                          ClientName = Convert.ToString(dr["ClientName"]),
                                          ClientId = Convert.ToInt32(dr["clientID"]),
                                          StartTime = Convert.ToString(dr["StartTime"]),
                                          EndTime = Convert.ToString(dr["EndTime"]),
                                          MeetingNotes = Convert.ToString(dr["Notes"]),
                                          MeetingDate = Convert.ToString(dr["Date"]),
                                          HouseholdAddress = Convert.ToString(dr["HouseholdAddress"]),
                                          HouseholdPhoneNo = Convert.ToString(dr["HouseholdPhoneNo"]).Replace(",", "<br>")
                                      }

                                    ).ToList();
                    totalrecord = command.Parameters["@totalRecord"].Value.ToString();

                    timeZoneDiff = Convert.ToInt32(command.Parameters["@timeZoneDiff"].Value).ToString();
                }
                return _Schedulerlist;
            }
            catch (Exception ex)
            {
                totalrecord = string.Empty;
                timeZoneDiff = string.Empty;
                clsError.WriteException(ex);
                return _Schedulerlist;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                familydataTable.Dispose();
            }
        }
        //Changes on 1Feb2017
        public string AddOthersHouseholdInfo(FamilyHousehold obj,string userId, string AgencyId, string roleId)
        {
            string result = string.Empty;
            try
            {

              
                //FamilyHousehold obj = new FamilyHousehold();
                int mode;

                command.Connection = Connection;
                command.CommandText = "SP_Add_OthersDetails";
                if (obj.OthersId != 0)
                {
                    mode = 1;
                }
                else
                {
                    mode = 0;
                }
                command.Parameters.AddWithValue("@mode", mode);
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.AddWithValue("@Ofirstname", obj.Ofirstname);
                command.Parameters.AddWithValue("@Omiddlename", obj.Omiddlename);
                command.Parameters.AddWithValue("@Olastname", obj.Olastname);
                command.Parameters.AddWithValue("@ParentSSN3", (obj.CSSN == null ? null : EncryptDecrypt.Encrypt(obj.CSSN)));
                command.Parameters.AddWithValue("@OGender", obj.OGender);
                command.Parameters.AddWithValue("@ODOB", obj.ODOB);
                command.Parameters.AddWithValue("@OthersId", obj.OthersId);
                command.Parameters.Add(new SqlParameter("@FileName", obj.HouseHoldFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.HouseHoldFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.HouseHoldImageByte));
                // command.Parameters.AddWithValue("@Isemergency", IsEmer);
                if (obj.IsEmergency != null)
                    command.Parameters.Add(new SqlParameter("@Isemergency", obj.IsEmergency));
                else
                    command.Parameters.Add(new SqlParameter("@Isemergency", DBNull.Value));
                command.Parameters.AddWithValue("@CreatedBy", userId);
                command.Parameters.Add(new SqlParameter("@agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@roleid", roleId));
                //End

                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("@Othersidgenereated", string.Empty));
                command.Parameters["@Othersidgenereated"].Direction = ParameterDirection.Output;
                command.Parameters["@Othersidgenereated"].Size = 10;
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                // obj.EmegencyId = Convert.ToInt32(command.Parameters["@Othersidgenereated"].Value);
                result = command.Parameters["@result"].Value.ToString();
                obj.EmegencyId = Convert.ToInt32(command.Parameters["@Othersidgenereated"].Value);
                //return result;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;
        }
        public string AddEmergencyInfo(FamilyHousehold obj, string Fname, string Mname, string Lname, string Gender, string DOB, string Email, string Relationwithchild, string Enotes, string HouseholdId, string EmegencyId, List<FamilyHousehold.phone> PhoneNos, string userId, string AgencyId, string roleId)
        {
            string result = string.Empty;
            try
            {
                //FamilyHousehold obj = new FamilyHousehold();
                int mode;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                tranSaction = Connection.BeginTransaction();
                command.Transaction = tranSaction;
                command.Connection = Connection;
                if (EmegencyId != "0")
                {
                    mode = 1;
                }
                else
                {
                    mode = 0;
                }

                command.Parameters.Add(new SqlParameter("@HouseholdId", HouseholdId));

                command.Parameters.Add(new SqlParameter("@EmergencyId", EmegencyId));
                command.Parameters.Add(new SqlParameter("@Efirstname", Fname));
                command.Parameters.Add(new SqlParameter("@Elastname", Lname));
                command.Parameters.Add(new SqlParameter("@Emiddlename", Mname));
                command.Parameters.Add(new SqlParameter("@EDOB", DOB));
                command.Parameters.Add(new SqlParameter("@EGender", Gender));
                command.Parameters.Add(new SqlParameter("@EEmail", Email));
                command.Parameters.Add(new SqlParameter("@relationship", Relationwithchild));
                command.Parameters.Add(new SqlParameter("@Enotes", Enotes));
                command.Parameters.Add(new SqlParameter("@FileName", obj.EFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.EFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.EImageByte));

                command.Parameters.Add(new SqlParameter("@CreatedBy", userId));
                command.Parameters.Add(new SqlParameter("@agencyid", AgencyId));
                command.Parameters.Add(new SqlParameter("@roleid", roleId));

                // command.Parameters.Add(new SqlParameter("@agencyid", AgencyId));

                //End
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                if (PhoneNos != null && PhoneNos.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string)),
                    });
                    foreach (FamilyHousehold.phone phone in PhoneNos)
                    {
                        if (phone.PhoneNo != null && phone.PhoneType != null)
                        {
                            dt.Rows.Add(phone.PhoneType, phone.PhoneNo, phone.IsPrimary, phone.IsSms, phone.Notes, phone.PhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt));

                }
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_EmegencyDetails";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();

                tranSaction.Commit();

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;
        }
        public string AddRestrictedInfo(FamilyHousehold obj, string Fname, string Lname, string Desc, string RAvatar, string RestrictedId, string HouseholdId, string userId, string agencyid, string roleid)
        {
            string result = string.Empty;
            try
            {
                int mode;
                if (RestrictedId != "0")
                {
                    mode = 1;
                }
                else
                {
                    mode = 0;
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@RestrictedId", RestrictedId));
                command.Parameters.Add(new SqlParameter("@HouseholdId", HouseholdId));
                command.Parameters.Add(new SqlParameter("@Rfirstname", Fname));
                command.Parameters.Add(new SqlParameter("@Rlastname", Lname));
                command.Parameters.Add(new SqlParameter("@Rmiddlename", obj.Rmiddlename));
                command.Parameters.Add(new SqlParameter("@CreatedBy", userId));
                command.Parameters.Add(new SqlParameter("@RDescription", Desc));
                command.Parameters.Add(new SqlParameter("@FileName", obj.RFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension", obj.RFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes", obj.RImageByte));
                command.Parameters.Add(new SqlParameter("@agencyid", agencyid));
                command.Parameters.Add(new SqlParameter("@roleid", roleid));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_Add_RestrictedDetails";
                //command.ExecuteNonQuery();
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                GetallHouseholdinfo(obj, _dataset);
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }


        public string addScreeningInfo(ref FamilyHousehold obj, int mode, Guid ID, List<FamilyHousehold.Parentphone1> ParentPhoneNos,
        List<FamilyHousehold.Parentphone2> ParentPhoneNos1, List<FamilyHousehold.calculateincome> Income, List<FamilyHousehold.calculateincome1> Income1,
         List<FamilyHousehold.ImmunizationRecord> Imminization, List<FamilyHousehold.phone> PhoneNos, Screening _screen, string Roleid, FormCollection collection, HttpFileCollectionBase Files, string serverpath)
        {
            string result = string.Empty;
            try
            {
                string HouseholdId = string.Empty;
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.Parameters.Add(new SqlParameter("@HouseholdId", obj.HouseholdId));
                command.Parameters.Add(new SqlParameter("@Street", obj.Street));
                command.Parameters.Add(new SqlParameter("@StreetName", obj.StreetName));
                command.Parameters.Add(new SqlParameter("@Apartmentno", obj.Apartmentno));
                command.Parameters.Add(new SqlParameter("@ZipCode", obj.ZipCode));
                command.Parameters.Add(new SqlParameter("@State", obj.State));
                command.Parameters.Add(new SqlParameter("@City", obj.City));
                command.Parameters.Add(new SqlParameter("@nationality", obj.County));
                command.Parameters.Add(new SqlParameter("@fileinbyte1", obj.HImageByte));
                command.Parameters.Add(new SqlParameter("@filename1", obj.HFileName));
                command.Parameters.Add(new SqlParameter("@fileextension1", obj.HFileExtension));
                command.Parameters.Add(new SqlParameter("@AdresssverificationinPaper", obj.AdresssverificationinPaper));
                command.Parameters.Add(new SqlParameter("@TANF", obj.TANF));
                command.Parameters.Add(new SqlParameter("@SSI", obj.SSI));
                command.Parameters.Add(new SqlParameter("@SNAP", obj.SNAP));
                command.Parameters.Add(new SqlParameter("@WIC", obj.WIC));
                command.Parameters.Add(new SqlParameter("@NONE", obj.NONE));//None
                command.Parameters.Add(new SqlParameter("@HomeType", obj.HomeType));
                command.Parameters.Add(new SqlParameter("@PrimaryLanguauge", obj.PrimaryLanguauge));
                command.Parameters.Add(new SqlParameter("@RentType", obj.RentType));
                command.Parameters.Add(new SqlParameter("@FamilyType", obj.FamilyType));
                command.Parameters.Add(new SqlParameter("@Interpretor", obj.Interpretor));
                command.Parameters.Add(new SqlParameter("@ParentRelatioship", obj.ParentRelatioship));
                command.Parameters.Add(new SqlParameter("@ParentRelatioshipOther", obj.ParentRelatioshipOther));
                command.Parameters.Add(new SqlParameter("@OtherLanguageDetail", obj.OtherLanguageDetail));
                command.Parameters.Add(new SqlParameter("@Married", obj.Married));
                //child paremeter
                command.Parameters.Add(new SqlParameter("@ChildId", obj.ChildId));
                command.Parameters.Add(new SqlParameter("@Cfirstname", obj.Cfirstname));
                command.Parameters.Add(new SqlParameter("@Clastname", obj.Clastname));
                command.Parameters.Add(new SqlParameter("@Cmiddlename", obj.Cmiddlename));
                command.Parameters.Add(new SqlParameter("@CprogramType", obj.CProgramType));
                command.Parameters.Add(new SqlParameter("@CDOB", obj.CDOB));
                //Changes
                command.Parameters.Add(new SqlParameter("@CTransportNeeded", obj.CTransportNeeded));
                //End
                command.Parameters.Add(new SqlParameter("@CDOBverifiedby", obj.DOBverifiedBy));
                command.Parameters.Add(new SqlParameter("@CSSN", obj.CSSN == null ? null : EncryptDecrypt.Encrypt(obj.CSSN)));
                command.Parameters.Add(new SqlParameter("@CGender", obj.CGender));
                command.Parameters.Add(new SqlParameter("@CRace", obj.CRace));
                command.Parameters.Add(new SqlParameter("@CRaceSubCategory", obj.CRaceSubCategory));
                command.Parameters.Add(new SqlParameter("@CEthnicity", obj.CEthnicity));
                command.Parameters.Add(new SqlParameter("@CMedicalhome", obj.Medicalhome));
                command.Parameters.Add(new SqlParameter("@Dentalhome", obj.CDentalhome));
                command.Parameters.Add(new SqlParameter("@ImmunizationService", obj.ImmunizationService));
                command.Parameters.Add(new SqlParameter("@medicalservice", obj.MedicalService));
                command.Parameters.Add(new SqlParameter("@Parentdisable", obj.CParentdisable));
                command.Parameters.Add(new SqlParameter("@Bmistatus", obj.BMIStatus2));
                command.Parameters.Add(new SqlParameter("@FileName2", obj.CFileName));
                command.Parameters.Add(new SqlParameter("@FileExtension2", obj.CFileExtension));
                command.Parameters.Add(new SqlParameter("@FileInBytes2", obj.CImageByte));
                command.Parameters.Add(new SqlParameter("@Dobaddressform", obj.Dobaddressform));
                command.Parameters.Add(new SqlParameter("@DobFileName", obj.DobFileName));
                command.Parameters.Add(new SqlParameter("@DobFileExtension", obj.DobFileExtension));
                command.Parameters.Add(new SqlParameter("@Doctor", obj.Doctor));
                command.Parameters.Add(new SqlParameter("@Dentist", obj.Dentist));
                command.Parameters.Add(new SqlParameter("@Dobpaper", obj.DobverificationinPaper));
                command.Parameters.Add(new SqlParameter("@SchoolDistrict", obj.SchoolDistrict));
                command.Parameters.Add(new SqlParameter("@InsuranceOption", obj.InsuranceOption));
                command.Parameters.Add(new SqlParameter("@MedicalNotice", obj.MedicalNote));
                command.Parameters.Add(new SqlParameter("@IsFoster", obj.IsFoster));
                command.Parameters.Add(new SqlParameter("@Inwalfareagency", obj.Inwalfareagency));
                command.Parameters.Add(new SqlParameter("@InDualcustody", obj.InDualcustody));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileName", obj.ImmunizationFileName));
                command.Parameters.Add(new SqlParameter("@ImmunizationFileExtension", obj.ImmunizationFileExtension));
                command.Parameters.Add(new SqlParameter("@Immunizationfileinbytes", obj.Immunizationfileinbytes));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileName", obj.ReleaseformFileName));
                command.Parameters.Add(new SqlParameter("@ReleaseformFileExtension", obj.ReleaseformFileExtension));
                command.Parameters.Add(new SqlParameter("@Releaseformfileinbytes", obj.Releaseformfileinbytes));
                command.Parameters.Add(new SqlParameter("@ImmunizationinPaper", obj.ImmunizationinPaper));
                command.Parameters.Add(new SqlParameter("@OtherRace", obj.Raceother));
                command.Parameters.Add(new SqlParameter("@HWInput", obj.HWInput));
                command.Parameters.Add(new SqlParameter("@AssessmentDate", obj.AssessmentDate));
                command.Parameters.Add(new SqlParameter("@BHeight", obj.AHeight));
                command.Parameters.Add(new SqlParameter("@bWeight", obj.AWeight));
                command.Parameters.Add(new SqlParameter("@HeadCircle", obj.HeadCircle));








                //Others details parameter
                #region
                //command.Parameters.Add(new SqlParameter("@OthersId", obj.OthersId));
                //command.Parameters.Add(new SqlParameter("@Ofirstname", obj.Ofirstname));
                //command.Parameters.Add(new SqlParameter("@Olastname", obj.Olastname));
                //command.Parameters.Add(new SqlParameter("@Omiddlename", obj.Omiddlename));
                //command.Parameters.Add(new SqlParameter("@ODOB", obj.ODOB));
                //if (obj.Oemergencycontact)
                //    command.Parameters.Add(new SqlParameter("@Isemergency", obj.Oemergencycontact));
                //else
                //    command.Parameters.Add(new SqlParameter("@Isemergency", DBNull.Value));
                //command.Parameters.Add(new SqlParameter("@OGender", obj.OGender));
                //command.Parameters.Add(new SqlParameter("@ParentSSN3", obj.ParentSSN3 == null ? null : EncryptDecrypt.Encrypt(obj.ParentSSN3)));
                //command.Parameters.Add(new SqlParameter("@Othersidgenereated", string.Empty));
                //command.Parameters["@Othersidgenereated"].Direction = ParameterDirection.Output;
                //command.Parameters["@Othersidgenereated"].Size = 10;
                #endregion
                //Emergency details Parameter
                #region
                //command.Parameters.Add(new SqlParameter("@EmergencyId", obj.EmegencyId));
                //command.Parameters.Add(new SqlParameter("@Efirstname", obj.Efirstname));
                //command.Parameters.Add(new SqlParameter("@Elastname", obj.Elastname));
                //command.Parameters.Add(new SqlParameter("@Emiddlename", obj.Emiddlename));
                //command.Parameters.Add(new SqlParameter("@EDOB", obj.EDOB));
                //command.Parameters.Add(new SqlParameter("@EGender", obj.EGender));
                //command.Parameters.Add(new SqlParameter("@EEmail", obj.EEmail));
                //command.Parameters.Add(new SqlParameter("@relationship", obj.ERelationwithchild));
                //command.Parameters.Add(new SqlParameter("@Enotes", obj.Enotes));
                //command.Parameters.Add(new SqlParameter("@EFileName", obj.EFileName));
                //command.Parameters.Add(new SqlParameter("@EFileExtension", obj.EFileExtension));
                //command.Parameters.Add(new SqlParameter("@EFileInBytes", obj.EImageByte));
                //if (PhoneNos != null && PhoneNos.Count > 0)
                //{
                //    DataTable dtphone = new DataTable();
                //    dtphone.Columns.AddRange(new DataColumn[6] { 
                //    new DataColumn("PhoneType", typeof(string)),
                //    new DataColumn("Phoneno",typeof(string)), 
                //    new DataColumn("IsPrimaryContact",typeof(bool)), 
                //    new DataColumn("Sms",typeof(bool)), 
                //    new DataColumn("Notes",typeof(string)), 
                //    new DataColumn("PhoneID",typeof(string)), 
                //    });
                //    foreach (FamilyHousehold.phone phone in PhoneNos)
                //    {
                //        if (phone.PhoneNo != null && phone.PhoneType != null)
                //        {
                //            dtphone.Rows.Add(phone.PhoneType, phone.PhoneNo, phone.IsPrimary, phone.IsSms, phone.Notes, phone.PhoneId);
                //        }
                //    }
                //    command.Parameters.Add(new SqlParameter("@tblphone", dtphone));

                //}
                #endregion
                //restricted Parameter
                #region
                //command.Parameters.Add(new SqlParameter("@RestrictedId", obj.RestrictedId));
                //command.Parameters.Add(new SqlParameter("@Rfirstname", obj.Rfirstname));
                //command.Parameters.Add(new SqlParameter("@Rlastname", obj.Rlastname));
                //command.Parameters.Add(new SqlParameter("@Rmiddlename", obj.Rmiddlename));
                //command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                //command.Parameters.Add(new SqlParameter("@RDescription", obj.RDescription));
                //command.Parameters.Add(new SqlParameter("@RFileName", obj.RFileName));
                //command.Parameters.Add(new SqlParameter("@RFileExtension", obj.RFileExtension));
                //command.Parameters.Add(new SqlParameter("@RFileInBytes", obj.RImageByte));
                #endregion
                if ((mode == 0 && obj.Parentsecondexist == 0) || (mode == 1 && obj.Parentsecondexist == 0))
                {

                    command.Parameters.Add(new SqlParameter("@ParentID", obj.ParentID));
                    command.Parameters.Add(new SqlParameter("@Parentsecondexist", obj.Parentsecondexist));
                    command.Parameters.Add(new SqlParameter("@Pfirstname", obj.Pfirstname));
                    command.Parameters.Add(new SqlParameter("@Plastname", obj.Plastname));
                    command.Parameters.Add(new SqlParameter("@Pmiddlename", obj.Pmidddlename));
                    command.Parameters.Add(new SqlParameter("@Pnotes", obj.Pnotes));
                    command.Parameters.Add(new SqlParameter("@Pnotesother", obj.Pnotesother));
                    command.Parameters.Add(new SqlParameter("@Pemailid", obj.Pemailid));
                    command.Parameters.Add(new SqlParameter("@PDOB", obj.PDOB));
                    command.Parameters.Add(new SqlParameter("@PRole", obj.PRole));
                    command.Parameters.Add(new SqlParameter("@PGender", obj.PGender));
                    command.Parameters.Add(new SqlParameter("@PMilitaryStatus", obj.PMilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@PCurrentlyWorking", obj.PCurrentlyWorking));
                    command.Parameters.Add(new SqlParameter("@PEnrollment", obj.PEnrollment));
                    command.Parameters.Add(new SqlParameter("@PDegreeEarned", obj.PDegreeEarned));
                    command.Parameters.Add(new SqlParameter("@PGuardiannotes", obj.PGuardiannotes));
                    command.Parameters.Add(new SqlParameter("@FileName3", obj.PFileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension3", obj.PFileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes3", obj.PImageByte));
                    command.Parameters.Add(new SqlParameter("@PQuestion", obj.PQuestion));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherenrolled", obj.Pregnantmotherenrolled));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance", obj.Pregnantmotherprimaryinsurance));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes", obj.Pregnantmotherprimaryinsurancenotes));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled", obj.TrimesterEnrolled));
                    if (string.IsNullOrEmpty(obj.ParentSSN1))
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", EncryptDecrypt.Encrypt(obj.ParentSSN1)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg1", obj.MedicalhomePreg1));
                    command.Parameters.Add(new SqlParameter("@P1Doctor", obj.P1Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail", obj.Noemail1));
                    //
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[33] {
                    new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    DataTable dt1 = new DataTable();
                    dt1.Columns.AddRange(new DataColumn[33] {
                    new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    foreach (FamilyHousehold.calculateincome parentincome in Income)
                    {
                        if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                        {
                            dt.Rows.Add(parentincome.newincomeid, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                  parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                  parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                   parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                   parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                   parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                   parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                   parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                   parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                   );
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblincome", dt));
                    command.Parameters.Add(new SqlParameter("@tblincome1", dt1));


                    DataTable dt2 = new DataTable();
                    dt2.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    DataTable dt3 = new DataTable();
                    dt3.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone1 phone in ParentPhoneNos)
                    {
                        if (phone.phonenoP != null && phone.PhoneTypeP != null)
                        {
                            dt2.Rows.Add(phone.PhoneTypeP, phone.phonenoP, phone.StateP, phone.SmsP, phone.notesP, phone.PPhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt2));
                    command.Parameters.Add(new SqlParameter("@tblphone1", dt3));


                }
                else if (((mode == 0) && (obj.Parentsecondexist != 0)) || ((mode == 1) && (obj.Parentsecondexist != 0)))
                {
                    //Parent1 Parameter
                    command.Parameters.Add(new SqlParameter("@ParentID", obj.ParentID));
                    command.Parameters.Add(new SqlParameter("@ParentID1", obj.ParentID1));
                    command.Parameters.Add(new SqlParameter("@Parentsecondexist", obj.Parentsecondexist));
                    command.Parameters.Add(new SqlParameter("@Pfirstname", obj.Pfirstname));
                    command.Parameters.Add(new SqlParameter("@Plastname", obj.Plastname));
                    command.Parameters.Add(new SqlParameter("@Pmiddlename", obj.Pmidddlename));
                    command.Parameters.Add(new SqlParameter("@Pnotes", obj.Pnotes));
                    command.Parameters.Add(new SqlParameter("@Pnotesother", obj.Pnotesother));
                    command.Parameters.Add(new SqlParameter("@Pemailid", obj.Pemailid));
                    command.Parameters.Add(new SqlParameter("@PDOB", obj.PDOB));
                    command.Parameters.Add(new SqlParameter("@PRole", obj.PRole));
                    command.Parameters.Add(new SqlParameter("@PGender", obj.PGender));
                    command.Parameters.Add(new SqlParameter("@PMilitaryStatus", obj.PMilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@PCurrentlyWorking", obj.PCurrentlyWorking));
                    command.Parameters.Add(new SqlParameter("@PEnrollment", obj.PEnrollment));
                    command.Parameters.Add(new SqlParameter("@PDegreeEarned", obj.PDegreeEarned));
                    command.Parameters.Add(new SqlParameter("@PphoneType", obj.PphoneType));
                    command.Parameters.Add(new SqlParameter("@PState", obj.PState));
                    command.Parameters.Add(new SqlParameter("@FileName3", obj.PFileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension3", obj.PFileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes3", obj.PImageByte));
                    command.Parameters.Add(new SqlParameter("@PSms", obj.PSms));
                    command.Parameters.Add(new SqlParameter("@PGuardiannotes", obj.PGuardiannotes));
                    command.Parameters.Add(new SqlParameter("@PQuestion", obj.PQuestion));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherenrolled", obj.Pregnantmotherenrolled));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance", obj.Pregnantmotherprimaryinsurance));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes", obj.Pregnantmotherprimaryinsurancenotes));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled", obj.TrimesterEnrolled));
                    if (string.IsNullOrEmpty(obj.ParentSSN1))
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN1", EncryptDecrypt.Encrypt(obj.ParentSSN1)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg1", obj.MedicalhomePreg1));
                    command.Parameters.Add(new SqlParameter("@P1Doctor", obj.P1Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail", obj.Noemail1));
                    //


                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[33] {
                 new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                    new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    foreach (FamilyHousehold.calculateincome parentincome in Income)
                    {
                        if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                        {
                            dt.Rows.Add(parentincome.newincomeid, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                  parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                  parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                   parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                   parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                   parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                   parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                   parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                   parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                   );
                        }
                    }
                    DataTable dt1 = new DataTable();
                    dt1.Columns.AddRange(new DataColumn[33] {
                   new DataColumn("IncomeID",typeof(string)),
                    new DataColumn("Income",typeof(string)),
                    new DataColumn("IncomeSource1",typeof(string)),
                    new DataColumn("IncomeSource2",typeof(string)),
                    new DataColumn("IncomeSource3",typeof(string)),
                    new DataColumn("IncomeSource4",typeof(string)),
                    new DataColumn("IncomeAmt1",typeof(string)),
                    new DataColumn("IncomeAmt2",typeof(string)),
                    new DataColumn("IncomeAmt3",typeof(string)),
                    new DataColumn("IncomeAmt4",typeof(string)),
                    new DataColumn("PayFrequency",typeof(string)),
                    new DataColumn("WorkingPeriod",typeof(string)),
                    new DataColumn("TotalIncome",typeof(string)),
                    new DataColumn("Image1",typeof(byte[])),
                    new DataColumn("Image2",typeof(byte[])),
                    new DataColumn("Image3",typeof(byte[])),
                    new DataColumn("Image4",typeof(byte[])),
                    new DataColumn("Image1FileName",typeof(string)),
                    new DataColumn("Image2FileName",typeof(string)),
                    new DataColumn("Image3FileName",typeof(string)),
                    new DataColumn("Image4FileName",typeof(string)),
                    new DataColumn("Image1FileExt",typeof(string)),
                    new DataColumn("Image2FileExt",typeof(string)),
                    new DataColumn("Image3FileExt",typeof(string)),
                    new DataColumn("Image4FileExt",typeof(string)),
                    new DataColumn("NoIncomeImage",typeof(byte[])),
                    new DataColumn("NoIncomeImageFileName",typeof(string)),
                    new DataColumn("NoIncomeImageFileExt",typeof(string)),
                     new DataColumn("NoincomePaper",typeof(bool)),
                    new DataColumn("IncomePaper1",typeof(bool)),
                    new DataColumn("IncomePaper2",typeof(bool)),
                    new DataColumn("IncomePaper3",typeof(bool)),
                    new DataColumn("IncomePaper4",typeof(bool))
                    });
                    if (Income1 != null)
                    {
                        foreach (FamilyHousehold.calculateincome1 parentincome in Income1)
                        {
                            if (parentincome != null && (!string.IsNullOrEmpty(parentincome.IncomeSource1) || parentincome.Income != 0))
                            {
                                dt1.Rows.Add(parentincome.IncomeID, parentincome.Income, parentincome.IncomeSource1, parentincome.IncomeSource2, parentincome.IncomeSource3,
                                      parentincome.IncomeSource4, parentincome.AmountVocher1, parentincome.AmountVocher2, parentincome.AmountVocher3,
                                      parentincome.AmountVocher4, parentincome.Payfrequency, parentincome.Working, parentincome.IncomeCalculated,
                                       parentincome.SalaryAvatar1bytes, parentincome.SalaryAvatar2bytes, parentincome.SalaryAvatar3bytes,
                                       parentincome.SalaryAvatar4bytes, parentincome.SalaryAvatarFilename1, parentincome.SalaryAvatarFilename2,
                                       parentincome.SalaryAvatarFilename3, parentincome.SalaryAvatarFilename4,
                                       parentincome.SalaryAvatarFileExtension1, parentincome.SalaryAvatarFileExtension2, parentincome.SalaryAvatarFileExtension3,
                                       parentincome.SalaryAvatarFileExtension4, parentincome.NoIncomeAvatarbytes, parentincome.NoIncomeFilename4, parentincome.NoIncomeFileExtension4,
                                       parentincome.noincomepaper, parentincome.incomePaper1, parentincome.incomePaper2, parentincome.incomePaper3, parentincome.incomePaper4
                                       );
                            }
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblincome", dt));
                    command.Parameters.Add(new SqlParameter("@tblincome1", dt1));
                    //Parent2 Parameter
                    command.Parameters.Add(new SqlParameter("@P1firstname", obj.P1firstname));
                    command.Parameters.Add(new SqlParameter("@P1lastname", obj.P1lastname));
                    command.Parameters.Add(new SqlParameter("@P1middlename", obj.P1midddlename));
                    command.Parameters.Add(new SqlParameter("@P1phoneno", obj.P1phoneno));
                    command.Parameters.Add(new SqlParameter("@P1Avtr", obj.P1AvatarUrl));
                    command.Parameters.Add(new SqlParameter("@P1notes", obj.P1notes));
                    command.Parameters.Add(new SqlParameter("@P1notesother", obj.P1notesother));
                    command.Parameters.Add(new SqlParameter("@P1emailid", obj.P1emailid));
                    command.Parameters.Add(new SqlParameter("@P1DOB", obj.P1DOB));
                    command.Parameters.Add(new SqlParameter("@P1Role", obj.P1Role));
                    command.Parameters.Add(new SqlParameter("@P1Gender", obj.P1Gender));
                    command.Parameters.Add(new SqlParameter("@P1MilitaryStatus", obj.P1MilitaryStatus));
                    command.Parameters.Add(new SqlParameter("@P1CurrentlyWorking", obj.P1CurrentlyWorking));
                    command.Parameters.Add(new SqlParameter("@P1Enrollment", obj.P1Enrollment));
                    command.Parameters.Add(new SqlParameter("@P1DegreeEarned", obj.P1DegreeEarned));
                    command.Parameters.Add(new SqlParameter("@P1phoneType", obj.P1phoneType));
                    command.Parameters.Add(new SqlParameter("@P1State", obj.P1State));
                    command.Parameters.Add(new SqlParameter("@P1Sms", obj.P1Sms));
                    command.Parameters.Add(new SqlParameter("@P1Guardiannotes", obj.P1Guardiannotes));
                    command.Parameters.Add(new SqlParameter("@FileName4", obj.P1FileName));
                    command.Parameters.Add(new SqlParameter("@FileExtension4", obj.P1FileExtension));
                    command.Parameters.Add(new SqlParameter("@FileInBytes4", obj.P1ImageByte));
                    command.Parameters.Add(new SqlParameter("@P1Question", obj.P1Question));
                    command.Parameters.Add(new SqlParameter("@PregnantmotherenrolledP1", obj.PregnantmotherenrolledP1));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurance1", obj.Pregnantmotherprimaryinsurance1));
                    command.Parameters.Add(new SqlParameter("@Pregnantmotherprimaryinsurancenotes1", obj.Pregnantmotherprimaryinsurancenotes1));
                    command.Parameters.Add(new SqlParameter("@TrimesterEnrolled1", obj.TrimesterEnrolled1));
                    if (string.IsNullOrEmpty(obj.ParentSSN2))
                        command.Parameters.Add(new SqlParameter("@ParentSSN2", DBNull.Value));
                    else
                        command.Parameters.Add(new SqlParameter("@ParentSSN2", EncryptDecrypt.Encrypt(obj.ParentSSN2)));
                    command.Parameters.Add(new SqlParameter("@MedicalhomePreg2", obj.MedicalhomePreg2));
                    command.Parameters.Add(new SqlParameter("@P2Doctor", obj.P2Doctor));
                    command.Parameters.Add(new SqlParameter("@Noemail1", obj.Noemail2));
                    //for phone
                    DataTable dt2 = new DataTable();
                    dt2.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone1 phone in ParentPhoneNos)
                    {
                        if (phone.phonenoP != null && phone.PhoneTypeP != null)
                        {
                            dt2.Rows.Add(phone.PhoneTypeP, phone.phonenoP, phone.StateP, phone.SmsP, phone.notesP, phone.PPhoneId);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone", dt2));
                    DataTable dt3 = new DataTable();
                    dt3.Columns.AddRange(new DataColumn[6] {
                    new DataColumn("PhoneType", typeof(string)),
                    new DataColumn("Phoneno",typeof(string)),
                    new DataColumn("IsPrimaryContact",typeof(bool)),
                    new DataColumn("Sms",typeof(bool)),
                    new DataColumn("Notes",typeof(string)),
                    new DataColumn("PhoneID",typeof(string))
                    });
                    foreach (FamilyHousehold.Parentphone2 phone in ParentPhoneNos1)
                    {
                        if (phone.phonenoP1 != null && phone.PhoneTypeP1 != null)
                        {
                            dt3.Rows.Add(phone.PhoneTypeP1, phone.phonenoP1, phone.StateP1, phone.SmsP1, phone.notesP1, phone.PPhoneId1);
                        }
                    }
                    command.Parameters.Add(new SqlParameter("@tblphone1", dt3));
                }



                DataTable dt5 = new DataTable();
                dt5.Columns.AddRange(new DataColumn[18] {
                    new DataColumn("Immunizationmasterid", typeof(Int32)),
                    new DataColumn("ImmunizationId", typeof(Int32)),
                    new DataColumn("Dose",typeof(string)),
                    new DataColumn("Dose1",typeof(string)),
                    new DataColumn("Preempt1",typeof(bool)),
                    new DataColumn("Exempt1",typeof(bool)),
                    new DataColumn("Dose2",typeof(string)),
                    new DataColumn("Preempt2",typeof(bool)),
                    new DataColumn("Exempt2",typeof(bool)),
                    new DataColumn("Dose3",typeof(string)),
                    new DataColumn("Preempt3",typeof(bool)),
                    new DataColumn("Exempt3",typeof(bool)),
                     new DataColumn("Dose4",typeof(string)),
                    new DataColumn("Preempt4",typeof(bool)),
                    new DataColumn("Exempt4",typeof(bool)),
                     new DataColumn("Dose5",typeof(string)),
                    new DataColumn("Preempt5",typeof(bool)),
                    new DataColumn("Exempt5",typeof(bool)),

                    });
                if (Imminization != null)
                {
                    foreach (FamilyHousehold.ImmunizationRecord _Imminization in Imminization)
                    {
                        dt5.Rows.Add(_Imminization.ImmunizationmasterId, _Imminization.ImmunizationId, _Imminization.Dose, _Imminization.Dose1, _Imminization.Preempt1, _Imminization.Exempt1
                            , _Imminization.Dose2, _Imminization.Preempt2, _Imminization.Exempt2, _Imminization.Dose3, _Imminization.Preempt3, _Imminization.Exempt3
                            , _Imminization.Dose4, _Imminization.Preempt4, _Imminization.Exempt4, _Imminization.Dose5, _Imminization.Preempt5, _Imminization.Exempt5);
                    }
                }

                #region screening
                DataTable dt6 = new DataTable();
                dt6.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),
                    });
                #endregion
                foreach (var s in _screen.GetType().GetProperties())
                {
                    int screeningid = 0;
                    int questionid = 0;
                    if (s.Name.Substring(0, 1) == "F")
                        screeningid = 1;
                    if (s.Name.Substring(0, 1) == "v")
                        screeningid = 2;
                    if (s.Name.Substring(0, 1) == "h")
                        screeningid = 3;
                    if (s.Name.Substring(0, 1) == "d")
                        screeningid = 4;
                    if (s.Name.Substring(0, 1) == "E")
                        screeningid = 5;
                    if (s.Name.Substring(0, 1) == "s")
                        screeningid = 6;
                    if (screeningid == 1 || screeningid == 2 || screeningid == 3 || screeningid == 4 || screeningid == 5 || screeningid == 6)
                    {
                        questionid = Convert.ToInt32(s.Name.Substring(1, 3));
                        dt6.Rows.Add(screeningid, questionid, s.GetValue(_screen));
                    }
                }
                command.Parameters.Add(new SqlParameter("@tblImminization", dt5));
                command.Parameters.Add(new SqlParameter("@tblscreening", dt6));
                //Changes
                command.Parameters.Add(new SqlParameter("@Physical", _screen.AddPhysical));
                command.Parameters.Add(new SqlParameter("@Vision", _screen.AddVision));
                command.Parameters.Add(new SqlParameter("@Dental", _screen.AddDental));
                command.Parameters.Add(new SqlParameter("@Hearing", _screen.AddHearing));
                command.Parameters.Add(new SqlParameter("@Develop", _screen.AddDevelop));
                command.Parameters.Add(new SqlParameter("@Speech", _screen.AddSpeech));
                //    command.Parameters.Add(new SqlParameter("@ScreeningAccept", _screen.ScreeningAccept));
                command.Parameters.Add(new SqlParameter("@PhysicalFileName", _screen.PhysicalFileName));
                command.Parameters.Add(new SqlParameter("@PhysicalFileExtension", _screen.PhysicalFileExtension));
                command.Parameters.Add(new SqlParameter("@PhysicalImageByte", _screen.PhysicalImageByte));
                command.Parameters.Add(new SqlParameter("@VisionFileName", _screen.VisionFileName));
                command.Parameters.Add(new SqlParameter("@VisionFileExtension", _screen.VisionFileExtension));
                command.Parameters.Add(new SqlParameter("@VisionImageByte", _screen.VisionImageByte));
                command.Parameters.Add(new SqlParameter("@DevelopFileName", _screen.DevelopFileName));
                command.Parameters.Add(new SqlParameter("@DevelopFileExtension", _screen.DevelopFileExtension));
                command.Parameters.Add(new SqlParameter("@DevelopImageByte", _screen.DevelopImageByte));
                command.Parameters.Add(new SqlParameter("@DentalFileExtension", _screen.DentalFileExtension));
                command.Parameters.Add(new SqlParameter("@DentalFileName", _screen.DentalFileName));
                command.Parameters.Add(new SqlParameter("@DentalImageByte", _screen.DentalImageByte));
                command.Parameters.Add(new SqlParameter("@HearingFileName", _screen.HearingFileName));
                command.Parameters.Add(new SqlParameter("@HearingFileExtension", _screen.HearingFileExtension));
                command.Parameters.Add(new SqlParameter("@HearingImageByte", _screen.HearingImageByte));
                command.Parameters.Add(new SqlParameter("@SpeechFileName", _screen.SpeechFileName));
                command.Parameters.Add(new SqlParameter("@SpeechFileExtension", _screen.SpeechFileExtension));
                command.Parameters.Add(new SqlParameter("@SpeechImageByte", _screen.SpeechImageByte));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileExtension", _screen.ScreeningAcceptFileExtension));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptFileName", _screen.ScreeningAcceptFileName));
                command.Parameters.Add(new SqlParameter("@ScreeningAcceptImageByte", _screen.ScreeningAcceptImageByte));
                //Changes
                command.Parameters.Add(new SqlParameter("@Consolidated", _screen.Consolidated));
                command.Parameters.Add(new SqlParameter("@ParentName", _screen.Parentname));

                #region Parent1,Parent2 health question
                command.Parameters.Add(new SqlParameter("@PMVisitDoc", obj.PMVisitDoc));
                command.Parameters.Add(new SqlParameter("@PMProblem", obj.PMProblem));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues", obj.PMOtherIssues));
                command.Parameters.Add(new SqlParameter("@PMConditions", obj.PMConditions));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc", obj.PMCondtnDesc));
                command.Parameters.Add(new SqlParameter("@PMRisk", obj.PMRisk));
                command.Parameters.Add(new SqlParameter("@PMDentalExam", obj.PMDentalExam));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate", obj.PMDentalExamDate));
                command.Parameters.Add(new SqlParameter("@PMNeedDental", obj.PMNeedDental));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental", obj.PMRecieveDental));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices", obj._Pregnantmotherpmservices));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem", obj._Pregnantmotherproblem));
                command.Parameters.Add(new SqlParameter("@PMVisitDoc1", obj.PMVisitDoc1));
                command.Parameters.Add(new SqlParameter("@PMProblem1", obj.PMProblem1));
                command.Parameters.Add(new SqlParameter("@PMOtherIssues1", obj.PMOtherIssues1));
                command.Parameters.Add(new SqlParameter("@PMConditions1", obj.PMConditions1));
                command.Parameters.Add(new SqlParameter("@PMCondtnDesc1", obj.PMCondtnDesc1));
                command.Parameters.Add(new SqlParameter("@PMRisk1", obj.PMRisk1));
                command.Parameters.Add(new SqlParameter("@PMDentalExam1", obj.PMDentalExam1));
                command.Parameters.Add(new SqlParameter("@PMDentalExamDate1", obj.PMDentalExamDate1));
                command.Parameters.Add(new SqlParameter("@PMNeedDental1", obj.PMNeedDental1));
                command.Parameters.Add(new SqlParameter("@PMRecieveDental1", obj.PMRecieveDental1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherpmservices1", obj._Pregnantmotherpmservices1));
                command.Parameters.Add(new SqlParameter("@Pregnantmotherproblem1", obj._Pregnantmotherproblem1));
                #endregion
                #region child health Ehs
                command.Parameters.Add(new SqlParameter("@EHsChildBorn", obj.EhsChildBorn));
                command.Parameters.Add(new SqlParameter("@EhsChildBirthWt", obj.EhsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@EhsChildLength", obj.EhsChildLength));
                command.Parameters.Add(new SqlParameter("@EhsChildProblm", obj.EhsChildProblm));
                command.Parameters.Add(new SqlParameter("@EhsMedication", obj.EhsMedication));
                //10082016
                command.Parameters.Add(new SqlParameter("@EHSBabyOrMotherProblems", obj.EHSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@EHSChildMedication", obj.EHSChildMedication));

                command.Parameters.Add(new SqlParameter("@EHSAllergy", obj.EHSAllergy));
                command.Parameters.Add(new SqlParameter("@EHSEpiPen", obj.EHSEpiPen));


                //
                command.Parameters.Add(new SqlParameter("@EhsComment", obj.EhsComment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeEhs", obj._ChildDirectBloodRelativeEhs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsEhs", obj._ChildDiagnosedConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsEhs", obj._ChildChronicHealthConditions2Ehs));
                command.Parameters.Add(new SqlParameter("@ChildreceivedChronicHealthConditionsEhs", obj._ChildChronicHealthConditionsEhs));
                command.Parameters.Add(new SqlParameter("@ChildreceivingChronicHealthConditionsEhs", obj._ChildChronicHealthConditions1Ehs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentEhs", obj._ChildMedicalTreatmentEhs));
                #endregion
                #region child health Hs
                command.Parameters.Add(new SqlParameter("@HsChildBorn", obj.HsChildBorn));
                command.Parameters.Add(new SqlParameter("@HsChildBirthWt", obj.HsChildBirthWt));
                command.Parameters.Add(new SqlParameter("@HsChildLength", obj.HsChildLength));
                command.Parameters.Add(new SqlParameter("@HsChildProblm", obj.HsChildProblm));
                command.Parameters.Add(new SqlParameter("@HsMedication", obj.HsMedication));
                command.Parameters.Add(new SqlParameter("@HsDentalExam", obj.HsDentalExam));
                command.Parameters.Add(new SqlParameter("@HsComment", obj.HsComment));
                command.Parameters.Add(new SqlParameter("@HsChildDentalCare", obj.HsChildDentalCare));
                command.Parameters.Add(new SqlParameter("@HsRecentDentalExam", obj.HsRecentDentalExam));




                command.Parameters.Add(new SqlParameter("@HsChildNeedDentalTreatment", obj.HsChildNeedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HsChildRecievedDentalTreatment", obj.HsChildRecievedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@ChildDirectBloodRelativeHs", obj._ChildDirectBloodRelativeHs));
                command.Parameters.Add(new SqlParameter("@ChildDiagnosedConditionsHs", obj._ChildDiagnosedConditionsHs));
                command.Parameters.Add(new SqlParameter("@ChildMedicalTreatmentHs", obj._ChildMedicalTreatmentHs));
                command.Parameters.Add(new SqlParameter("@ChildreceiveChronicHealthConditionsHs", obj._ChildChronicHealthConditionsHs));
                //10082016
                command.Parameters.Add(new SqlParameter("@HSBabyOrMotherProblems", obj.HSBabyOrMotherProblems));
                command.Parameters.Add(new SqlParameter("@HsMedicationName", obj.HsMedicationName));
                command.Parameters.Add(new SqlParameter("@HsDosage", obj.HsDosage));
                command.Parameters.Add(new SqlParameter("@HSChildMedication", obj.HSChildMedication));
                command.Parameters.Add(new SqlParameter("@HSPreventativeDentalCare", obj.HSPreventativeDentalCare));
                command.Parameters.Add(new SqlParameter("@HSProfessionalDentalExam", obj.HSProfessionalDentalExam));
                command.Parameters.Add(new SqlParameter("@HSNeedingDentalTreatment", obj.HSNeedingDentalTreatment));
                command.Parameters.Add(new SqlParameter("@HSChildReceivedDentalTreatment", obj.HSChildReceivedDentalTreatment));
                command.Parameters.Add(new SqlParameter("@ChildProfessionalDentalExam", obj.ChildProfessionalDentalExam));//new ques added
                //
                #endregion
                //#region child nutrition
                //command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                //command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                //command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                //command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                //command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                //command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                //command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                //command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                //command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                //command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                //command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                //command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                //command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                //command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                //command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                //command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                //command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                //command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                //command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                //command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                //command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                //command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                //command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                //command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                //command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                //command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                //command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                //command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                //command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                //command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                //command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                //command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                //command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                //command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                //command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                //command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                //command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                //command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                //command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                //command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                //command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                //command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                //command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                //command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                //command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                //command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                //command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                //command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                //command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                //command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                //command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                //command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                //command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                //command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                //#endregion


                //Added by Akansha on 19Dec2016
                // child nutrition with HS/Ehs
                if (obj._childprogrefid == "1")  //Ehs Questions
                {
                    #region child nutrition

                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.EhsChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.EhsPersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.EhsPersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.EhsPersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.EhsDramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.EhsRecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.EhsRecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.EhsChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.EhsFoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.EhsNutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.EhsFoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.EhschildTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.Ehsspoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.Ehsfeedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.EhschildThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.EhsTakebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.Ehschewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.EhsChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.EhsChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.EhsMilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));//Differ
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));//End
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.EhsChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.EhsChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.EhsBreakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.EhsLunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.EhsSnack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.EhsDinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.EhsNA));
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.EhsRestrictFood));//New ques added

                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.EhsNauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.EhsDiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.EhsConstipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.EhsDramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.EhsRecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.EhsRecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.EhsSpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.EhsFoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.EhsNutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.EhsChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.EhsSpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.EhsSpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.EhsBottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EhsEatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                if (obj._childprogrefid == "2")  //hs Questions
                {
                    #region child nutrition
                    command.Parameters.Add(new SqlParameter("@ChildVitaminSupplment", obj.ChildVitaminSupplment));//new field added
                    command.Parameters.Add(new SqlParameter("@RestrictFood", obj.RestrictFood));//New ques added
                    command.Parameters.Add(new SqlParameter("@PersistentNausea", obj.PersistentNausea));
                    command.Parameters.Add(new SqlParameter("@PersistentDiarrhea", obj.PersistentDiarrhea));
                    command.Parameters.Add(new SqlParameter("@PersistentConstipation", obj.PersistentConstipation));
                    command.Parameters.Add(new SqlParameter("@DramaticWeight", obj.DramaticWeight));
                    command.Parameters.Add(new SqlParameter("@RecentSurgery", obj.RecentSurgery));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalization", obj.RecentHospitalization));
                    command.Parameters.Add(new SqlParameter("@ChildSpecialDiet", obj.ChildSpecialDiet));
                    command.Parameters.Add(new SqlParameter("@FoodAllergies", obj.FoodAllergies));
                    command.Parameters.Add(new SqlParameter("@NutritionalConcern", obj.NutritionalConcern));
                    command.Parameters.Add(new SqlParameter("@WICNutrition", obj.WICNutrition));
                    command.Parameters.Add(new SqlParameter("@FoodStamps", obj.FoodStamps));
                    command.Parameters.Add(new SqlParameter("@NoNutritionProg", obj.NoNutritionProg));
                    command.Parameters.Add(new SqlParameter("@foodpantry", obj.FoodPantory));
                    command.Parameters.Add(new SqlParameter("@childTrouble", obj.childTrouble));
                    command.Parameters.Add(new SqlParameter("@spoon", obj.spoon));
                    command.Parameters.Add(new SqlParameter("@feedingtube", obj.feedingtube));
                    command.Parameters.Add(new SqlParameter("@childThin", obj.childThin));
                    command.Parameters.Add(new SqlParameter("@Takebottle", obj.Takebottle));
                    command.Parameters.Add(new SqlParameter("@chewanything", obj.chewanything));
                    command.Parameters.Add(new SqlParameter("@ChangeinAppetite", obj.ChangeinAppetite));
                    command.Parameters.Add(new SqlParameter("@ChildHungry", obj.ChildHungry));
                    command.Parameters.Add(new SqlParameter("@MilkComment", obj.MilkComment));
                    command.Parameters.Add(new SqlParameter("@ChildFeed", obj.ChildFeed));
                    command.Parameters.Add(new SqlParameter("@ChildFormula", obj.ChildFormula));
                    command.Parameters.Add(new SqlParameter("@ChildFeedCereal", obj.ChildFeedCereal));
                    command.Parameters.Add(new SqlParameter("@ChildFeedMarshfood", obj.ChildFeedMarshfood));
                    command.Parameters.Add(new SqlParameter("@ChildFeedChopedfood", obj.ChildFeedChopedfood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFood", obj.ChildFingerFood));
                    command.Parameters.Add(new SqlParameter("@ChildFingerFEDFood", obj.ChildFingerFEDFood));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuice", obj.ChildFruitJuice));
                    command.Parameters.Add(new SqlParameter("@ChildFruitJuicevitaminc", obj.ChildFruitJuicevitaminc));
                    command.Parameters.Add(new SqlParameter("@ChildWater", obj.ChildWater));
                    command.Parameters.Add(new SqlParameter("@Breakfast", obj.Breakfast));
                    command.Parameters.Add(new SqlParameter("@Lunch", obj.Lunch));
                    command.Parameters.Add(new SqlParameter("@Snack", obj.Snack));
                    command.Parameters.Add(new SqlParameter("@Dinner", obj.Dinner));
                    command.Parameters.Add(new SqlParameter("@NA", obj.NA));
                    command.Parameters.Add(new SqlParameter("@ChildReferalCriteria", obj.ChildReferalCriteria));
                    command.Parameters.Add(new SqlParameter("@ChildChildVitaminSupplement", obj._ChildChildVitaminSupplement));
                    command.Parameters.Add(new SqlParameter("@ChildDiet", obj._ChildDiet));
                    command.Parameters.Add(new SqlParameter("@ChildDrink", obj._ChildDrink));

                    command.Parameters.Add(new SqlParameter("@NotHealthStaff", obj.NotHealthStaff));




                    command.Parameters.Add(new SqlParameter("@NauseaorVomitingcomment", obj.NauseaorVomitingcomment));
                    command.Parameters.Add(new SqlParameter("@DiarrheaComment", obj.DiarrheaComment));
                    command.Parameters.Add(new SqlParameter("@Constipationcomment", obj.Constipationcomment));
                    command.Parameters.Add(new SqlParameter("@DramaticWeightchangecomment", obj.DramaticWeightchangecomment));
                    command.Parameters.Add(new SqlParameter("@RecentSurgerycomment", obj.RecentSurgerycomment));
                    command.Parameters.Add(new SqlParameter("@RecentHospitalizationComment", obj.RecentHospitalizationComment));
                    command.Parameters.Add(new SqlParameter("@SpecialDietComment", obj.SpecialDietComment));
                    command.Parameters.Add(new SqlParameter("@FoodAllergiesComment", obj.FoodAllergiesComment));
                    command.Parameters.Add(new SqlParameter("@NutritionAlconcernsComment", obj.NutritionAlconcernsComment));
                    command.Parameters.Add(new SqlParameter("@ChewingorSwallowingcomment", obj.ChewingorSwallowingcomment));
                    command.Parameters.Add(new SqlParameter("@SpoonorForkComment", obj.SpoonorForkComment));
                    command.Parameters.Add(new SqlParameter("@SpecialFeedingComment", obj.SpecialFeedingComment));
                    command.Parameters.Add(new SqlParameter("@BottleComment", obj.BottleComment));
                    command.Parameters.Add(new SqlParameter("@EatOrChewComment", obj.EatOrChewComment));
                    command.Parameters.Add(new SqlParameter("@childprogrefid", obj._childprogrefid));
                    #endregion
                }
                //End

                //custom screenig save


                #region custom screening
                DataTable screeningquestion = new DataTable();
                screeningquestion.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("QuestionID",typeof(Int32)),
                    new DataColumn("Value",typeof(string)),
                    new DataColumn("OptionID",typeof(Int32)),
                    new DataColumn("ScreeningDate",typeof(string))
                    });
                #endregion
                #region allowed screening
                DataTable screeningallowed = new DataTable();
                screeningallowed.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("ScreeningID",typeof(Int32)),
                    new DataColumn("Allowed",typeof(Int32)),
                    new DataColumn("FileName",typeof(string)),
                    new DataColumn("FileExtension",typeof(string)),
                    new DataColumn("FileBytes",typeof(byte[]))
                    });
                #endregion



                if (collection != null)
                {
                    foreach (var radio in collection.AllKeys.Where(P => P.Contains("_allowchildcustomscreening")))
                    {
                        if (collection[radio].ToString() == "1")
                        {
                            foreach (var question in collection.AllKeys.Where(P => P.Contains("_custscreeningquestin") && P.Split('k')[1] == radio.Split('@')[0]))
                            {

                                string questionid = string.Empty;
                                string optionid = string.Empty;
                                string screeningdate = "";
                                if (question.ToString().Contains("o") || question.ToString().Contains("k"))
                                    questionid = question.ToString().Split('k', 'k')[2];
                                if (question.ToString().Contains("o"))
                                {
                                    optionid = question.ToString().Split('o', 'o')[1];
                                    questionid = question.ToString().Split('k', 'k')[2];
                                }
                                if (question.ToString().Contains("_custrad"))
                                {
                                    optionid = collection[question].ToString().Split('o', 'o')[1];
                                    questionid = collection[question].ToString().Split('k', 'k')[2];
                                }
                                if (question.Contains("_$SD"))
                                    screeningdate = collection[question].ToString();
                                if (string.IsNullOrEmpty(optionid))
                                {
                                    if (question.ToString().Contains("select"))
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString().Replace(",", ""), DBNull.Value, screeningdate);
                                    else
                                        screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, collection[question].ToString(), DBNull.Value, screeningdate);


                                }
                                else
                                {
                                    screeningquestion.Rows.Add(question.ToString().Split('k')[1], questionid, DBNull.Value, optionid, screeningdate);
                                }
                                optionid = "";
                                questionid = "";


                            }



                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }




                        }
                        else
                        {


                            foreach (var file in Files.AllKeys.Where(P => P.Contains("_customscreeningdocument") && P.Split('k')[1] == radio.Split('@')[0]))
                            {
                                HttpPostedFileBase _file = Files[file];
                                string filename = null;
                                string fileextension = null;
                                byte[] filedata = null;
                                if (_file != null && _file.FileName != "")
                                {
                                    filename = _file.FileName;
                                    fileextension = Path.GetExtension(_file.FileName);
                                    filedata = new BinaryReader(_file.InputStream).ReadBytes(_file.ContentLength);
                                }
                                screeningallowed.Rows.Add(radio.Split('@')[0], collection[radio].ToString(), filename, fileextension, filedata);
                            }

                        }
                    }
                }
                //End

                command.Parameters.Add(new SqlParameter("@screeningquestion", screeningquestion));
                command.Parameters.Add(new SqlParameter("@screeningallowed", screeningallowed));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusChild", obj.ApplicationStatusChild));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusParent1", obj.ApplicationStatusParent1));
                command.Parameters.Add(new SqlParameter("@ApplicationStatusParent2", obj.ApplicationStatusParent2));
                command.Parameters.Add(new SqlParameter("@Roleid", Roleid));
                command.Parameters.Add(new SqlParameter("@CreatedBy", ID));
                command.Parameters.Add(new SqlParameter("@mode", mode));
                command.Parameters.Add(new SqlParameter("@result", string.Empty));
                command.Parameters["@result"].Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_SaveScreeningfamilyinfo_info";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                Getallchildinfo(obj, _dataset, serverpath);
                DataAdapter.Dispose();
                command.Dispose();
                result = command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);

            }
            finally
            {
                Connection.Close();
                command.Dispose();
            }
            return result;

        }


        private void Getallchildinfo(FamilyHousehold obj, DataSet _dataset, string serverpath)
        {
            if (_dataset != null)
            {
                if (_dataset.Tables[0].Rows.Count > 0)
                {
                    obj.ChildId = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ID"]);
                    obj.Cfirstname = _dataset.Tables[0].Rows[0]["Firstname"].ToString();
                    obj.Cmiddlename = _dataset.Tables[0].Rows[0]["Middlename"].ToString();
                    obj.Clastname = _dataset.Tables[0].Rows[0]["Lastname"].ToString();
                    if (_dataset.Tables[0].Rows[0]["DOB"].ToString() != "")
                        obj.CDOB = Convert.ToDateTime(_dataset.Tables[0].Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                    obj.DOBverifiedBy = _dataset.Tables[0].Rows[0]["Dobverifiedby"].ToString();
                    try
                    {
                        obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString() == "" ? "" : EncryptDecrypt.Decrypt(_dataset.Tables[0].Rows[0]["SSN"].ToString());
                    }
                    catch (Exception ex)
                    {
                        clsError.WriteException(ex);
                        obj.CSSN = _dataset.Tables[0].Rows[0]["SSN"].ToString();
                    }

                    obj.CProgramType = _dataset.Tables[0].Rows[0]["Programtype"].ToString();
                    obj.CGender = _dataset.Tables[0].Rows[0]["Gender"].ToString();
                    obj.CRace = _dataset.Tables[0].Rows[0]["RaceID"].ToString();
                    obj.CRaceSubCategory = _dataset.Tables[0].Rows[0]["RaceSubCategoryID"].ToString();
                    if (_dataset.Tables[0].Rows[0]["ImmunizationServiceType"].ToString() != "")
                        obj.ImmunizationService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ImmunizationServiceType"]);
                    if (_dataset.Tables[0].Rows[0]["MedicalService"].ToString() != "")
                        obj.MedicalService = Convert.ToInt32(_dataset.Tables[0].Rows[0]["MedicalService"]);
                    if (_dataset.Tables[0].Rows[0]["Medicalhome"].ToString() != "")
                        obj.Medicalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Medicalhome"]);
                    if (_dataset.Tables[0].Rows[0]["ParentDisable"].ToString() != "")
                        obj.CParentdisable = Convert.ToInt32(_dataset.Tables[0].Rows[0]["ParentDisable"]);
                    if (_dataset.Tables[0].Rows[0]["BMIStatus"].ToString() != "")
                        obj.BMIStatus = Convert.ToInt32(_dataset.Tables[0].Rows[0]["BMIStatus"]);
                    if (_dataset.Tables[0].Rows[0]["DentalHome"].ToString() != "")
                        obj.CDentalhome = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DentalHome"]);
                    if (_dataset.Tables[0].Rows[0]["Ethnicity"].ToString() != "")
                        obj.CEthnicity = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Ethnicity"]);
                    obj.CFileName = _dataset.Tables[0].Rows[0]["FileNameul"].ToString();
                    obj.CFileExtension = _dataset.Tables[0].Rows[0]["FileExtension"].ToString();
                    obj.Imagejson = _dataset.Tables[0].Rows[0]["ProfilePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[0].Rows[0]["ProfilePic"]);
                    obj.DobFileName = _dataset.Tables[0].Rows[0]["Dobfilename"].ToString();
                    obj.CDoctor = _dataset.Tables[0].Rows[0]["doctorname"].ToString();
                    obj.CDentist = _dataset.Tables[0].Rows[0]["dentistname"].ToString();
                    if (_dataset.Tables[0].Rows[0]["Doctorvalue"].ToString() != "")
                        obj.Doctor = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Doctorvalue"]);
                    if (_dataset.Tables[0].Rows[0]["Dentistvalue"].ToString() != "")
                        obj.Dentist = Convert.ToInt32(_dataset.Tables[0].Rows[0]["Dentistvalue"]);
                    if (_dataset.Tables[0].Rows[0]["SchoolDistrict"].ToString() != "")
                        obj.SchoolDistrict = Convert.ToInt32(_dataset.Tables[0].Rows[0]["SchoolDistrict"]);
                    if (_dataset.Tables[0].Rows[0]["DobPaper"].ToString() != "")
                        obj.DobverificationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["DobPaper"]);
                    if (_dataset.Tables[0].Rows[0]["FosterChild"].ToString() != "")
                        obj.IsFoster = Convert.ToInt32(_dataset.Tables[0].Rows[0]["FosterChild"]);
                    if (_dataset.Tables[0].Rows[0]["WelfareAgency"].ToString() != "")
                        obj.Inwalfareagency = Convert.ToInt32(_dataset.Tables[0].Rows[0]["WelfareAgency"]);
                    if (_dataset.Tables[0].Rows[0]["DualCustodyChild"].ToString() != "")
                        obj.InDualcustody = Convert.ToInt32(_dataset.Tables[0].Rows[0]["DualCustodyChild"]);
                    if (_dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString() != "")
                        obj.InsuranceOption = _dataset.Tables[0].Rows[0]["PrimaryInsurance"].ToString();
                    if (_dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString() != "")
                        obj.MedicalNote = _dataset.Tables[0].Rows[0]["InsuranceNotes"].ToString();
                    if (_dataset.Tables[0].Rows[0]["ImmunizationinPaper"].ToString() != "")
                        obj.ImmunizationinPaper = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ImmunizationinPaper"]);
                    obj.ImmunizationFileName = _dataset.Tables[0].Rows[0]["ImmunizationFileName"].ToString();
                    obj.Raceother = _dataset.Tables[0].Rows[0]["OtherRace"].ToString();
                    //ChildTransport

                    obj.CTransport = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["ChildTransport"].ToString());

                    if (!string.IsNullOrEmpty((_dataset.Tables[0].Rows[0]["TransportNeeded"]).ToString()))
                    {
                        obj.CTransportNeeded = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["TransportNeeded"]);
                    }
                    else
                    {
                        obj.CTransportNeeded = false;
                    }

                    //End
                    //Ehs Health question
                    obj.EhsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                    obj.EhsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                    obj.EhsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                    obj.EhsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                    obj.EhsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                    obj.EhsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();

                    obj.EHSAllergy = _dataset.Tables[0].Rows[0]["EHSAllergy"].ToString();
                    obj.EHSEpiPen = Convert.ToInt32(_dataset.Tables[0].Rows[0]["EHSEpiPen"]);



                    //HS Health question
                    obj.HsChildBorn = _dataset.Tables[0].Rows[0]["ChildBorn"].ToString();
                    obj.HsChildBirthWt = _dataset.Tables[0].Rows[0]["ChildBirthWt"].ToString();
                    obj.HsChildLength = _dataset.Tables[0].Rows[0]["ChildLength"].ToString();
                    obj.HsChildProblm = _dataset.Tables[0].Rows[0]["ChildPrblm"].ToString();
                    obj.HsMedication = _dataset.Tables[0].Rows[0]["Medication"].ToString();
                    obj.HsComment = _dataset.Tables[0].Rows[0]["Comment"].ToString();
                    obj.HsChildDentalCare = _dataset.Tables[0].Rows[0]["DentalCare"].ToString();
                    obj.HsDentalExam = _dataset.Tables[0].Rows[0]["CurrentDentalexam"].ToString();
                    obj.HsRecentDentalExam = _dataset.Tables[0].Rows[0]["RecentDentalExam"].ToString();
                    obj.HsChildNeedDentalTreatment = _dataset.Tables[0].Rows[0]["NeedDentalTreatment"].ToString();
                    obj.HsChildRecievedDentalTreatment = _dataset.Tables[0].Rows[0]["RecievedDentalTreatment"].ToString();
                    obj.ChildProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ChildEverHadProfExam"].ToString();

                    //Nutrition Question without HS/EHS

                    //obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                    //obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                    //obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                    //obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                    //obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                    //obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                    //obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                    //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                    //obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                    //obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                    //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                    //    obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                    //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                    //    obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                    //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                    //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                    //obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                    //obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                    //obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                    //obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                    //obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                    //obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                    //obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                    //obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                    //obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                    //obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                    //obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                    //obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                    //obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                    //obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                    //obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                    //obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                    //obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                    //obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                    //obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                    //if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                    //    obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                    //if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                    //    obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                    //if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                    //    obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                    //if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                    //    obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                    //if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                    //    obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                    //obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                    //obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                    //obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                    //obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                    //obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                    //obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                    //obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                    //obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                    //obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                    //obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                    //obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                    //obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                    //obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                    //obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                    //obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();

                    //obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                    //obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                    //obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                    //obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                    //obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                    //obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                    //obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                    //obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                    //obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                    //obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                    //obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                    //


                    //Nutrition question
                    //Changes on 19Dec2016
                    if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "1")
                    {
                        obj.EhsRestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                        obj.EhsChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                        obj.EhsPersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        obj.EhsPersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        obj.EhsPersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        obj.EhsDramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        obj.EhsRecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        obj.EhsChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        obj.EhsFoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.EhsNutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.EhsRecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        //if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                        //    obj.EhsWICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        //if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                        //    obj.EhsFoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        //if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                        //    obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        obj.EhsFoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        obj.EhschildTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        //obj.EhsChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        obj.Ehsspoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        obj.Ehsfeedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        obj.EhschildThin = Convert.ToInt32(_dataset.Tables[0].Rows[0]["childhealth"].ToString());
                        obj.EhsTakebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        obj.Ehschewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        obj.EhsChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        obj.EhsChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        obj.EhsChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        obj.EhsChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                            obj.EhsBreakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                            obj.EhsLunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                            obj.EhsSnack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                            obj.EhsDinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                            obj.EhsNA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        // obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        obj.EhsNauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        obj.EhsDiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        obj.EhsConstipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        obj.EhsDramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        obj.EhsRecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        obj.EhsRecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        obj.EhsSpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        obj.EhsFoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        obj.EhsNutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        obj.EhsChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        obj.EhsSpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        obj.EhsSpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        obj.EhsBottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        obj.EhsEatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                    }
                    else if (_dataset.Tables[0].Rows[0]["NutirionProgramID"].ToString() == "2")
                    {
                        obj.RestrictFood = _dataset.Tables[0].Rows[0]["ChildRestrictFood"].ToString();
                        obj.ChildVitaminSupplment = _dataset.Tables[0].Rows[0]["ChildVitaminSupplement"].ToString();
                        obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                            obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                            obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                            obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                            obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                            obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                            obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                            obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                            obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                    }
                    else
                    {

                        obj.PersistentNausea = _dataset.Tables[0].Rows[0]["CurrentNausea"].ToString();
                        obj.PersistentDiarrhea = _dataset.Tables[0].Rows[0]["Currentdiarrhea"].ToString();
                        obj.PersistentConstipation = _dataset.Tables[0].Rows[0]["CurrentConstipation"].ToString();
                        obj.DramaticWeight = _dataset.Tables[0].Rows[0]["weightchange"].ToString();
                        obj.RecentSurgery = _dataset.Tables[0].Rows[0]["Recentsurgery"].ToString();
                        obj.ChildSpecialDiet = _dataset.Tables[0].Rows[0]["specialdiet"].ToString();
                        obj.FoodAllergies = _dataset.Tables[0].Rows[0]["foodallergies"].ToString();
                        obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.NutritionalConcern = _dataset.Tables[0].Rows[0]["nutritionalconcerns"].ToString();
                        obj.RecentHospitalization = _dataset.Tables[0].Rows[0]["Recenthospitalization"].ToString();
                        if (_dataset.Tables[0].Rows[0]["WICNutrition"].ToString() != "")
                            obj.WICNutrition = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["WICNutrition"]);
                        if (_dataset.Tables[0].Rows[0]["FoodStamps"].ToString() != "")
                            obj.FoodStamps = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["FoodStamps"]);
                        if (_dataset.Tables[0].Rows[0]["NoNutritionProg"].ToString() != "")
                            obj.NoNutritionProg = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NoNutritionProg"]);
                        obj.FoodPantory = _dataset.Tables[0].Rows[0]["foodpantry"].ToString();
                        obj.childTrouble = _dataset.Tables[0].Rows[0]["troublechewing"].ToString();
                        obj.ChildFormula = _dataset.Tables[0].Rows[0]["ChildFormula"].ToString();
                        obj.spoon = _dataset.Tables[0].Rows[0]["childusespoon"].ToString();
                        obj.feedingtube = _dataset.Tables[0].Rows[0]["childusefeedingtube"].ToString();
                        obj.childThin = _dataset.Tables[0].Rows[0]["childhealth"].ToString();
                        obj.Takebottle = _dataset.Tables[0].Rows[0]["childtakebottle"].ToString();
                        obj.chewanything = _dataset.Tables[0].Rows[0]["Childeatchew"].ToString();
                        obj.ChangeinAppetite = _dataset.Tables[0].Rows[0]["childappetite"].ToString();
                        obj.ChildHungry = _dataset.Tables[0].Rows[0]["childhungry"].ToString();
                        obj.ChildFeed = _dataset.Tables[0].Rows[0]["ChildFeed"].ToString();
                        obj.ChildFeedCereal = _dataset.Tables[0].Rows[0]["childcereal"].ToString();
                        obj.ChildFeedMarshfood = _dataset.Tables[0].Rows[0]["childmashedfoods"].ToString();
                        obj.ChildFeedChopedfood = _dataset.Tables[0].Rows[0]["childchoppedfoods"].ToString();
                        obj.ChildFingerFood = _dataset.Tables[0].Rows[0]["childfingerfoods"].ToString();
                        obj.ChildFingerFEDFood = _dataset.Tables[0].Rows[0]["childfedfingerfoods"].ToString();
                        obj.ChildFruitJuice = _dataset.Tables[0].Rows[0]["childfruitjiuce"].ToString();
                        obj.ChildFruitJuicevitaminc = _dataset.Tables[0].Rows[0]["childfedVitamin"].ToString();
                        obj.ChildWater = _dataset.Tables[0].Rows[0]["childdrinkwater"].ToString();
                        if (_dataset.Tables[0].Rows[0]["Breakfast"].ToString() != "")
                            obj.Breakfast = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Breakfast"]);
                        if (_dataset.Tables[0].Rows[0]["lunch"].ToString() != "")
                            obj.Lunch = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["lunch"]);
                        if (_dataset.Tables[0].Rows[0]["Snack"].ToString() != "")
                            obj.Snack = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Snack"]);
                        if (_dataset.Tables[0].Rows[0]["Dinner"].ToString() != "")
                            obj.Dinner = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["Dinner"]);
                        if (_dataset.Tables[0].Rows[0]["NA"].ToString() != "")
                            obj.NA = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NA"]);
                        obj.ChildReferalCriteria = _dataset.Tables[0].Rows[0]["CriteriaforReferral"].ToString();
                        obj.NauseaorVomitingcomment = _dataset.Tables[0].Rows[0]["NauseaorVomitingcomment"].ToString();
                        obj.DiarrheaComment = _dataset.Tables[0].Rows[0]["DiarrheaComment"].ToString();
                        obj.Constipationcomment = _dataset.Tables[0].Rows[0]["Constipationcomment"].ToString();
                        obj.DramaticWeightchangecomment = _dataset.Tables[0].Rows[0]["DramaticWeightchangecomment"].ToString();
                        obj.RecentSurgerycomment = _dataset.Tables[0].Rows[0]["RecentSurgerycomment"].ToString();
                        obj.RecentHospitalizationComment = _dataset.Tables[0].Rows[0]["RecentHospitalizationComment"].ToString();
                        obj.SpecialDietComment = _dataset.Tables[0].Rows[0]["SpecialDietComment"].ToString();
                        obj.FoodAllergiesComment = _dataset.Tables[0].Rows[0]["FoodAllergiesComment"].ToString();
                        obj.NutritionAlconcernsComment = _dataset.Tables[0].Rows[0]["NutritionAlconcernsComment"].ToString();
                        obj.ChewingorSwallowingcomment = _dataset.Tables[0].Rows[0]["ChewingorSwallowingcomment"].ToString();
                        obj.SpoonorForkComment = _dataset.Tables[0].Rows[0]["SpoonorForkComment"].ToString();
                        obj.SpecialFeedingComment = _dataset.Tables[0].Rows[0]["SpecialFeedingComment"].ToString();
                        obj.BottleComment = _dataset.Tables[0].Rows[0]["BottleComment"].ToString();
                        obj.EatOrChewComment = _dataset.Tables[0].Rows[0]["EatOrChewComment"].ToString();


                    }


                    //End



                    obj.EHSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                    obj.EHSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                    obj.HSBabyOrMotherProblems = _dataset.Tables[0].Rows[0]["BabyMotherProblemComment"].ToString();
                    obj.HsMedicationName = _dataset.Tables[0].Rows[0]["HsMedicationName"].ToString();
                    obj.HsDosage = _dataset.Tables[0].Rows[0]["HsDosage"].ToString();
                    obj.HSChildMedication = _dataset.Tables[0].Rows[0]["MedicationComment"].ToString();
                    obj.HSPreventativeDentalCare = _dataset.Tables[0].Rows[0]["PreventativeDentalCareComment"].ToString();
                    obj.HSProfessionalDentalExam = _dataset.Tables[0].Rows[0]["ProfessionalDentalExamComment"].ToString();
                    obj.HSNeedingDentalTreatment = _dataset.Tables[0].Rows[0]["DiagnosedDentalTreatmentComment"].ToString();
                    obj.HSChildReceivedDentalTreatment = _dataset.Tables[0].Rows[0]["ChildReceivedDentalTreatmentComment"].ToString();
                    obj.NotHealthStaff = Convert.ToBoolean(_dataset.Tables[0].Rows[0]["NotHealthStaff"]);
                    obj.HWInput = _dataset.Tables[0].Rows[0]["HWInput"].ToString();
                    obj.AssessmentDate = _dataset.Tables[0].Rows[0]["AssessmentDate"].ToString() != "" ? Convert.ToDateTime(_dataset.Tables[0].Rows[0]["AssessmentDate"]).ToString("MM/dd/yyyy") : "";
                    obj.AHeight = _dataset.Tables[0].Rows[0]["BHeight"].ToString();
                    obj.AWeight = _dataset.Tables[0].Rows[0]["BWeight"].ToString();
                    obj.HeadCircle = _dataset.Tables[0].Rows[0]["HeadCirc"].ToString();



                }
                if (_dataset.Tables[1].Rows.Count > 0)
                {
                    List<FamilyHousehold.ImmunizationRecord> ImmunizationRecords = new List<FamilyHousehold.ImmunizationRecord>();
                    FamilyHousehold.ImmunizationRecord obj1;
                    foreach (DataRow dr in _dataset.Tables[1].Rows)
                    {
                        obj1 = new FamilyHousehold.ImmunizationRecord();
                        obj1.ImmunizationId = Convert.ToInt32(dr["Immunization_ID"]);
                        obj1.ImmunizationmasterId = Convert.ToInt32(dr["Immunizationmasterid"]);
                        obj1.Dose = dr["Dose"].ToString();
                        if (dr["Dose1"].ToString() != "")
                            obj1.Dose1 = Convert.ToDateTime(dr["Dose1"]).ToString("MM/dd/yyyy");
                        else
                            obj1.Dose1 = dr["Dose1"].ToString();
                        if (dr["Dose2"].ToString() != "")
                            obj1.Dose2 = Convert.ToDateTime(dr["Dose2"]).ToString("MM/dd/yyyy");
                        else
                            obj1.Dose2 = dr["Dose2"].ToString();
                        if (dr["Dose3"].ToString() != "")
                            obj1.Dose3 = Convert.ToDateTime(dr["Dose3"]).ToString("MM/dd/yyyy");
                        else
                            obj1.Dose3 = dr["Dose3"].ToString();
                        if (dr["Dose4"].ToString() != "")
                            obj1.Dose4 = Convert.ToDateTime(dr["Dose4"]).ToString("MM/dd/yyyy");
                        else
                            obj1.Dose4 = dr["Dose4"].ToString();
                        if (dr["Dose5"].ToString() != "")
                            obj1.Dose5 = Convert.ToDateTime(dr["Dose5"]).ToString("MM/dd/yyyy");
                        else
                            obj1.Dose5 = dr["Dose5"].ToString();
                        if (dr["Exempt1"].ToString() != "")
                            obj1.Exempt1 = Convert.ToBoolean(dr["Exempt1"]);
                        if (dr["Exempt2"].ToString() != "")
                            obj1.Exempt2 = Convert.ToBoolean(dr["Exempt2"]);
                        if (dr["Exempt3"].ToString() != "")
                            obj1.Exempt3 = Convert.ToBoolean(dr["Exempt3"]);
                        if (dr["Exempt4"].ToString() != "")
                            obj1.Exempt4 = Convert.ToBoolean(dr["Exempt4"]);
                        if (dr["Exempt5"].ToString() != "")
                            obj1.Exempt5 = Convert.ToBoolean(dr["Exempt5"]);
                        if (dr["Preemptive1"].ToString() != "")
                            obj1.Preempt1 = Convert.ToBoolean(dr["Preemptive1"]);
                        if (dr["Preemptive2"].ToString() != "")
                            obj1.Preempt2 = Convert.ToBoolean(dr["Preemptive2"]);
                        if (dr["Preemptive3"].ToString() != "")
                            obj1.Preempt3 = Convert.ToBoolean(dr["Preemptive3"]);
                        if (dr["Preemptive4"].ToString() != "")
                            obj1.Preempt4 = Convert.ToBoolean(dr["Preemptive4"]);
                        if (dr["Preemptive5"].ToString() != "")
                            obj1.Preempt5 = Convert.ToBoolean(dr["Preemptive5"]);
                        ImmunizationRecords.Add(obj1);
                        obj1 = null;
                    }
                    obj.ImmunizationRecords = ImmunizationRecords;
                }
                if (_dataset.Tables[2].Rows.Count > 0)
                {
                    List<FamilyHousehold.Programdetail> ProgramdetailRecords = new List<FamilyHousehold.Programdetail>();
                    FamilyHousehold.Programdetail obj1;
                    foreach (DataRow dr in _dataset.Tables[2].Rows)
                    {
                        obj1 = new FamilyHousehold.Programdetail();
                        obj1.Id = Convert.ToInt32(dr["programid"]);
                        obj1.ReferenceId = dr["ReferenceId"].ToString();
                        ProgramdetailRecords.Add(obj1);
                    }
                    obj.AvailableProgram = ProgramdetailRecords;
                }
                if (_dataset.Tables[3].Rows.Count > 0)
                {
                    Screening _Screening = new Screening();
                    foreach (DataRow dr in _dataset.Tables[3].Rows)
                    {
                        _Screening.F001physicalDate = dr["F001physicalDate"].ToString();
                        _Screening.F002physicalResults = dr["F002physicalResults"].ToString();
                        _Screening.F003physicallFOReason = dr["F003physicallFOReason"].ToString();
                        _Screening.F004medFollowup = dr["F004medFollowup"].ToString();
                        _Screening.F005MedFOComments = dr["F005MedFOComments"].ToString();
                        _Screening.F006bpResults = dr["F006bpResults"].ToString();
                        _Screening.F007hgDate = dr["F007hgDate"].ToString();
                        _Screening.F008hgStatus = dr["F008hgStatus"].ToString();
                        _Screening.F009hgResults = dr["F009hgResults"].ToString();
                        _Screening.F010hgReferralDate = dr["F010hgReferralDate"].ToString();
                        _Screening.F011hgComments = dr["F011hgComments"].ToString();
                        _Screening.F012hgDate2 = dr["F012hgDate2"].ToString();
                        _Screening.F013hgResults2 = dr["F013hgResults2"].ToString();
                        _Screening.F014hgFOStatus = dr["F014hgFOStatus"].ToString();
                        _Screening.F015leadDate = dr["F015leadDate"].ToString();
                        _Screening.F016leadResults = dr["F016leadResults"].ToString();
                        _Screening.F017leadReferDate = dr["F017leadReferDate"].ToString();
                        _Screening.F018leadComments = dr["F018leadComments"].ToString();
                        _Screening.F019leadDate2 = dr["F019leadDate2"].ToString();
                        _Screening.F020leadResults2 = dr["F020leadResults2"].ToString();
                        _Screening.F021leadFOStatus = dr["F021leadFOStatus"].ToString();
                        _Screening.v022date = dr["v022date"].ToString();
                        _Screening.v023results = dr["v023results"].ToString();
                        _Screening.v024comments = dr["v024comments"].ToString();
                        _Screening.v025dateR1 = dr["v025dateR1"].ToString();
                        _Screening.v026resultsR1 = dr["v026resultsR1"].ToString();
                        _Screening.v027commentsR1 = dr["v027commentsR1"].ToString();
                        _Screening.v028dateR2 = dr["v028dateR2"].ToString();
                        _Screening.v029resultsR2 = dr["v029resultsR2"].ToString();
                        _Screening.v030commentsR2 = dr["v030commentsR2"].ToString();
                        _Screening.v031ReferralDate = dr["v031ReferralDate"].ToString();
                        _Screening.v032Treatment = dr["v032Treatment"].ToString();
                        _Screening.v033TreatmentComments = dr["v033TreatmentComments"].ToString();
                        _Screening.v034Completedate = dr["v034Completedate"].ToString();
                        _Screening.v035ExamStatus = dr["v035ExamStatus"].ToString();
                        _Screening.h036Date = dr["h036Date"].ToString();
                        _Screening.h037Results = dr["h037Results"].ToString();
                        _Screening.h038Comments = dr["h038Comments"].ToString();
                        _Screening.h039DateR1 = dr["h039DateR1"].ToString();
                        _Screening.h040ResultsR1 = dr["h040ResultsR1"].ToString();
                        _Screening.h041CommentsR1 = dr["h041CommentsR1"].ToString();
                        _Screening.h042DateR2 = dr["h042DateR2"].ToString();
                        _Screening.h043ResultsR2 = dr["h043ResultsR2"].ToString();
                        _Screening.h044CommentsR2 = dr["h044CommentsR2"].ToString();
                        _Screening.h045ReferralDate = dr["h045ReferralDate"].ToString();
                        _Screening.h046Treatment = dr["h046Treatment"].ToString();
                        _Screening.h047TreatmentComments = dr["h047TreatmentComments"].ToString();
                        _Screening.h048CompleteDate = dr["h048CompleteDate"].ToString();
                        _Screening.h049ExamStatus = dr["h049ExamStatus"].ToString();
                        _Screening.d050evDate = dr["d050evDate"].ToString();
                        _Screening.d051NameDEV = dr["d051NameDEV"].ToString();
                        _Screening.d052evResults = dr["d052evResults"].ToString();
                        _Screening.d053evResultsDetails = dr["d053evResultsDetails"].ToString();
                        _Screening.d054evDate2 = dr["d054evDate2"].ToString();
                        _Screening.d055evResults2 = dr["d055evResults2"].ToString();
                        _Screening.d056evReferral = dr["d056evReferral"].ToString();
                        _Screening.d057evFOStatus = dr["d057evFOStatus"].ToString();
                        _Screening.d058evComments = dr["d058evComments"].ToString();
                        _Screening.d059evTool = dr["d059evTool"].ToString();
                        _Screening.E060denDate = dr["E060denDate"].ToString();
                        _Screening.E061denResults = dr["E061denResults"].ToString();
                        _Screening.E062denPrevent = dr["E062denPrevent"].ToString();
                        _Screening.E063denReferralDate = dr["E063denReferralDate"].ToString();
                        _Screening.E064denTreatment = dr["E064denTreatment"].ToString();
                        _Screening.E065denTreatmentComments = dr["E065denTreatmentComments"].ToString();
                        _Screening.E066denTreatmentReceive = dr["E066denTreatmentReceive"].ToString();
                        _Screening.s067Date = dr["s067Date"].ToString();
                        _Screening.s068NameTCR = dr["s068NameTCR"].ToString();
                        _Screening.s069Details = dr["s069Details"].ToString();
                        _Screening.s070Results = dr["s070Results"].ToString();
                        _Screening.s071RescreenTCR = dr["s071RescreenTCR"].ToString();
                        _Screening.s072RescreenTCRDate = dr["s072RescreenTCRDate"].ToString();
                        _Screening.s073RescreenTCRResults = dr["s073RescreenTCRResults"].ToString();
                        _Screening.s074ReferralDC = dr["s074ReferralDC"].ToString();
                        _Screening.s075ReferDate = dr["s075ReferDate"].ToString();
                        _Screening.s076DCDate = dr["s076DCDate"].ToString();
                        _Screening.s077NameDC = dr["s077NameDC"].ToString();
                        _Screening.s078DetailDC = dr["s078DetailDC"].ToString();
                        _Screening.s079DCDate2 = dr["s079DCDate2"].ToString();
                        _Screening.s080DetailDC2 = dr["s080DetailDC2"].ToString();
                        _Screening.s081FOStatus = dr["s081FOStatus"].ToString();
                    }
                    //Screening changes
                    if (_dataset.Tables[5].Rows.Count > 0)
                    {
                        _Screening.AddPhysical = _dataset.Tables[5].Rows[0]["PhysicalScreening"].ToString();
                        _Screening.AddVision = _dataset.Tables[5].Rows[0]["Vision"].ToString();
                        _Screening.AddHearing = _dataset.Tables[5].Rows[0]["Hearing"].ToString();
                        _Screening.AddDental = _dataset.Tables[5].Rows[0]["Dental"].ToString();
                        _Screening.AddDevelop = _dataset.Tables[5].Rows[0]["Developmental"].ToString();
                        _Screening.AddSpeech = _dataset.Tables[5].Rows[0]["Speech"].ToString();
                        _Screening.ScreeningAcceptFileName = _dataset.Tables[5].Rows[0]["AcceptFileUl"].ToString();
                        _Screening.PhysicalFileName = _dataset.Tables[5].Rows[0]["PhyImageUl"].ToString();
                        _Screening.HearingFileName = _dataset.Tables[5].Rows[0]["HearingPicUl"].ToString();
                        _Screening.DentalFileName = _dataset.Tables[5].Rows[0]["DentalPicUl"].ToString();
                        _Screening.DevelopFileName = _dataset.Tables[5].Rows[0]["DevePicUl"].ToString();
                        _Screening.VisionFileName = _dataset.Tables[5].Rows[0]["VisionPicUl"].ToString();
                        _Screening.SpeechFileName = _dataset.Tables[5].Rows[0]["SpeechPicUl"].ToString();
                        _Screening.ParentAppID = Convert.ToInt32(_dataset.Tables[5].Rows[0]["ID"].ToString());
                        _Screening.Parentname = _dataset.Tables[5].Rows[0]["ParentName"].ToString();
                        _Screening.Consolidated = Convert.ToInt32(_dataset.Tables[5].Rows[0]["Consolidated"].ToString());

                        //Get screening scan document
                        _Screening.PhysicalImagejson = _dataset.Tables[5].Rows[0]["PhyImage"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]);
                        _Screening.PhysicalFileExtension = _dataset.Tables[5].Rows[0]["PhyFileExtension"].ToString();
                        string Url = Guid.NewGuid().ToString();
                        if (_Screening.PhysicalFileName != "" && _Screening.PhysicalFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["PhyImage"], 0, ((byte[])_dataset.Tables[5].Rows[0]["PhyImage"]).Length);
                            file.Close();
                            _Screening.PhysicalImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        Url = "";
                        _Screening.VisionImagejson = _dataset.Tables[5].Rows[0]["VisionPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]);
                        _Screening.VisionFileExtension = _dataset.Tables[5].Rows[0]["VisionFileExtension"].ToString();
                        Url = Guid.NewGuid().ToString();
                        if (_Screening.VisionFileName != "" && _Screening.VisionFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["VisionPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["VisionPic"]).Length);
                            file.Close();
                            _Screening.VisionImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        Url = "";
                        _Screening.HearingImagejson = _dataset.Tables[5].Rows[0]["HearingPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]);
                        _Screening.HearingFileExtension = _dataset.Tables[5].Rows[0]["HearingFileExtension"].ToString();
                        Url = Guid.NewGuid().ToString();
                        if (_Screening.HearingFileName != "" && _Screening.HearingFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["HearingPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["HearingPic"]).Length);
                            file.Close();
                            _Screening.HearingImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        Url = "";
                        _Screening.DevelopImagejson = _dataset.Tables[5].Rows[0]["DevePic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DevePic"]);
                        _Screening.DevelopFileExtension = _dataset.Tables[5].Rows[0]["DeveFileExtension"].ToString();
                        Url = Guid.NewGuid().ToString();
                        if (_Screening.DevelopFileName != "" && _Screening.DevelopFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["DevePic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DevePic"]).Length);
                            file.Close();
                            _Screening.DevelopImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        Url = "";
                        _Screening.DentalImagejson = _dataset.Tables[5].Rows[0]["DentalPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]);
                        _Screening.DentalFileExtension = _dataset.Tables[5].Rows[0]["DentalPicExtension"].ToString();
                        Url = Guid.NewGuid().ToString();
                        if (_Screening.DentalFileName != "" && _Screening.DentalFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["DentalPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["DentalPic"]).Length);
                            file.Close();
                            _Screening.DentalImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        Url = "";
                        _Screening.SpeechImagejson = _dataset.Tables[5].Rows[0]["SpeechPic"].ToString() == "" ? "" : Convert.ToBase64String((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]);
                        _Screening.SpeechFileExtension = _dataset.Tables[5].Rows[0]["SpeechFileExtension"].ToString();

                        Url = Guid.NewGuid().ToString();
                        if (_Screening.SpeechFileName != "" && _Screening.SpeechFileExtension == ".pdf")
                        {
                            System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                            file.Write((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"], 0, ((byte[])_dataset.Tables[5].Rows[0]["SpeechPic"]).Length);
                            file.Close();
                            _Screening.SpeechImagejson = "/TempAttachment/" + Url + ".pdf";

                        }
                        //END
                    }
                    obj._Screening = _Screening;
                }
            }
            if (_dataset.Tables[4].Rows.Count > 0)
            {
                List<FamilyHousehold.Childhealthnutrition> _childhealthnutrition = new List<FamilyHousehold.Childhealthnutrition>();
                FamilyHousehold.Childhealthnutrition info = null;
                foreach (DataRow dr in _dataset.Tables[4].Rows)
                {
                    info = new FamilyHousehold.Childhealthnutrition();
                    info.Id = dr["ID"].ToString();
                    info.MasterId = dr["ChildRecieveTreatment"].ToString();
                    info.Description = dr["Description"].ToString();
                    info.Questionid = dr["Questionid"].ToString();
                    info.Programid = dr["Programid"].ToString();
                    _childhealthnutrition.Add(info);
                }
                obj._Childhealthnutrition = _childhealthnutrition;
            }

            if (_dataset.Tables[6] != null && _dataset.Tables[6].Rows.Count > 0)
            {
                List<FamilyHousehold.Childcustomscreening> _Childcustomscreenings = new List<FamilyHousehold.Childcustomscreening>();
                FamilyHousehold.Childcustomscreening info = null;
                foreach (DataRow dr in _dataset.Tables[6].Rows)
                {
                    info = new FamilyHousehold.Childcustomscreening();
                    info.QuestionID = dr["QuestionID"].ToString();
                    info.Screeningid = dr["Screeningid"].ToString();
                    info.Value = dr["Value"].ToString();
                    info.QuestionAcronym = dr["QuestionAcronym"].ToString();
                    info.optionid = dr["optionid"].ToString();
                    info.ScreeningDate = dr["ScreeningDate"].ToString() != "" ? Convert.ToDateTime(dr["ScreeningDate"]).ToString("MM/dd/yyyy") : "";
                    string Url = Guid.NewGuid().ToString();
                    if (dr["DocumentName"].ToString() != "" && dr["DocumentExtension"].ToString() == ".pdf")
                    {
                        System.IO.FileStream file = System.IO.File.Create(serverpath + "//" + Url + ".pdf");
                        file.Write((byte[])dr["Documentdata"], 0, ((byte[])dr["Documentdata"]).Length);
                        file.Close();
                        info.pdfpath = "/TempAttachment/" + Url + ".pdf";
                    }
                    else
                    {
                        info.pdfpath = "";
                        info.Documentdata = dr["Documentdata"].ToString() == "" ? "" : Convert.ToBase64String((byte[])dr["Documentdata"]);
                    }
                    _Childcustomscreenings.Add(info);
                }
                obj._childscreenings = _Childcustomscreenings;
            }

            if (_dataset.Tables[7] != null && _dataset.Tables[7].Rows.Count > 0)
            {
                List<CustomScreeningAllowed> _CustomScreeningAllowed = new List<CustomScreeningAllowed>();
                CustomScreeningAllowed info = null;
                foreach (DataRow dr in _dataset.Tables[7].Rows)
                {
                    info = new CustomScreeningAllowed();
                    info.ScreeningAllowed = dr["Screeningallowed"].ToString();
                    info.Screeningid = dr["Screeningid"].ToString();
                    info.ScreeningName = dr["screeningname"].ToString();
                    _CustomScreeningAllowed.Add(info);
                }
                obj._CustomScreeningAlloweds = _CustomScreeningAllowed;
            }




        }

        public bool SaveTransportation(ChildTransportation objTransport, string UserId)
        {
            bool isInserted = false;
            try
            {

                command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_SaveTransportationException";
                if (!string.IsNullOrEmpty(objTransport.Id))
                    command.Parameters.AddWithValue("@Id", objTransport.Id);
                command.Parameters.AddWithValue("@AgencyId", objTransport.AgencyId);
                command.Parameters.AddWithValue("@ClientId", objTransport.ClientId);
                command.Parameters.AddWithValue("@PickupStatus", objTransport.PickupStatus);
                command.Parameters.AddWithValue("@PickupAddress", objTransport.PickupAddress);
                command.Parameters.AddWithValue("@PickupZipcode", objTransport.PickupZipcode);
                command.Parameters.AddWithValue("@PickupCity", objTransport.PickupCity);
                command.Parameters.AddWithValue("@PickupState", objTransport.PickupState);
                command.Parameters.AddWithValue("@PickupLat", objTransport.PickupLatitude);
                command.Parameters.AddWithValue("@PickupLong", objTransport.PickupLongitude);
                command.Parameters.AddWithValue("@DropStatus", objTransport.DropStatus);
                command.Parameters.AddWithValue("@DropAddress", objTransport.DropAddress);
                command.Parameters.AddWithValue("@DropZipcode", objTransport.DropZipcode);
                command.Parameters.AddWithValue("@DropCity", objTransport.DropCity);
                command.Parameters.AddWithValue("@DropState", objTransport.DropState);
                command.Parameters.AddWithValue("@DropLat", objTransport.DropLatitude);
                command.Parameters.AddWithValue("@DropLong", objTransport.DropLongitude);
                command.Parameters.AddWithValue("@UserId", UserId);
                int Affected = command.ExecuteNonQuery();
                if (Affected > 0)
                    isInserted = true;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
            return isInserted;
        }

        public void GetTranportationDetails(ref DataTable dtTransportation, string AgencyId, string ClientId)
        {
            dtTransportation = new DataTable();
            try
            {
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GetTransportationDetails";
                DataAdapter = new SqlDataAdapter(command);
                DataAdapter.Fill(dtTransportation);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
        }
        public string GetAddressByClientId(string ClientId)
        {
            string Address = "";
            try
            {
                command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GetAddressByClientId";
                Object output = command.ExecuteScalar();
                if (output != null)
                    Address = output.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
            return Address;
        }

        public bool UpdateDateOfBirth(string DOB,string ClientId)
        {
            bool isupdated = true;
      
            try
            {
                command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Add(new SqlParameter("@ClientId", ClientId));
                command.Parameters.Add(new SqlParameter("@DOB", DOB));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_UpdateDateOfBirth";
                command.ExecuteNonQuery();
                int Affected = command.ExecuteNonQuery();
                if (Affected > 0)
                    isupdated = true;
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                if (Connection != null)
                    Connection.Close();
            }
            return isupdated;
        }

        
    }
}
