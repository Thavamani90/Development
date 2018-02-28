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

namespace FingerprintsData
{
    public class YakkrData
    {
        SqlConnection Connection = connection.returnConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader dataReader = null;
        SqlTransaction tranSaction = null;
        SqlDataAdapter DataAdapter = null;
        DataTable yakkrdataTable = null;
        DataSet _dataset = null;
        public Yakkr GetData_YakkrData(String AgencyID)
        {
            Yakkr _staff = new Yakkr();
            try
            {
                DataSet ds = null;
                using (SqlConnection Connection = connection.returnConnection())
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        ds = new DataSet();
                        command.Connection = Connection;
                        command.CommandText = "Sp_Sel_YakkrDetails";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@agencyID", AgencyID);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(ds);
                    }
                }
                // yAKKR CODES
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Yakkr.YakkrCode> _YakkrList = new List<Yakkr.YakkrCode>();
                    Yakkr.YakkrCode obj = null;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        obj = new Yakkr.YakkrCode();
                        obj._YakkrID = dr["Yakkrid"].ToString();
                        obj._YakkrCode = dr["YakkrCode"].ToString();
                        _YakkrList.Add(obj);
                    }
                    _staff.YakkrList = _YakkrList;
                }
                // roles 
                if (ds.Tables[1].Rows.Count > 0)
                {
                    List<Yakkr.YakkrRoles> _YakkrList = new List<Yakkr.YakkrRoles>();
                    Yakkr.YakkrRoles obj = null;
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        obj = new Yakkr.YakkrRoles();
                        obj._RoleID = dr["Id"].ToString();
                        obj._RoleName = dr["RoleName"].ToString();
                        _YakkrList.Add(obj);
                    }
                    _staff._YakkrRolesList = _YakkrList;
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    List<Yakkr.YakkrAgencyCodes> _YakkrList = new List<Yakkr.YakkrAgencyCodes>();
                    Yakkr.YakkrAgencyCodes obj = null;
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        obj = new Yakkr.YakkrAgencyCodes();
                        obj.YakkrRoleID = Convert.ToInt32(dr["YakkrRoleID"]);
                        obj.AgencyID = dr["AgencyID"].ToString();
                        obj.YakkrID = Convert.ToInt32(dr["YakkrID"]);
                        obj.YakkrCode = dr["YakkrCode"].ToString();
                        obj.StaffRoleID = dr["StaffRoleID"].ToString();
                        obj.StaffRoleName = dr["StaffRoleName"].ToString();
                        obj.Status = dr["Status"].ToString();
                        obj.DateEntered = Convert.ToDateTime(dr["DateEntered"].ToString());
                        obj.Value = dr["Value"].ToString();
                        obj.Description = dr["Description"].ToString();
                        _YakkrList.Add(obj);
                    }
                    _staff._YakkrAgencyCodes = _YakkrList;
                }

            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            return _staff;
        }
        public string AddYakkrInfo(Yakkr info, int mode, Guid userId, string AgencyId)
        {
            try
            {
                command.Connection = Connection;
                command.CommandText = "SP_addyakkrinfo";
                command.Parameters.AddWithValue("@mode", mode);
                command.Parameters.AddWithValue("@YakkrRoleID", info.YakkrRoleID);
                command.Parameters.AddWithValue("@YakkrID", info.YakkrID);
                command.Parameters.AddWithValue("@AgencyId", AgencyId);
                command.Parameters.AddWithValue("@Value", (info.Value));
                command.Parameters.AddWithValue("@Description", info.Description == null ? null : info.Description.Trim());
                command.Parameters.AddWithValue("@StaffRoleID", info.StaffRoleID);
                command.Parameters.AddWithValue("@SecondaryRoleID", info.SecondaryRoleID);
                command.Parameters.AddWithValue("@CreatedBy", userId);
                command.Parameters.AddWithValue("@result", "").Direction = ParameterDirection.Output;
                command.CommandType = CommandType.StoredProcedure;
                Connection.Open();
                command.ExecuteNonQuery();
                Connection.Close();
                return command.Parameters["@result"].Value.ToString();
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
                return "";
            }
            //return command.Parameters["@result"].Value.ToString();
        }
        public List<Yakkr> YakkrInfoDetails(out string totalrecord, string sortOrder, string sortDirection, string search, int skip, int pageSize, string userid)
        {
            List<Yakkr> _yakkrlist = new List<Yakkr>();
            try
            {
                totalrecord = string.Empty;
                string searchAgency = string.Empty;
                if (string.IsNullOrEmpty(search.Trim()))
                    searchAgency = string.Empty;
                else
                    searchAgency = search;
                command.Parameters.Add(new SqlParameter("@Search", searchAgency));
                command.Parameters.Add(new SqlParameter("@take", pageSize));
                command.Parameters.Add(new SqlParameter("@skip", skip));
                command.Parameters.Add(new SqlParameter("@sortcolumn", sortOrder));
                command.Parameters.Add(new SqlParameter("@sortorder", sortDirection));
                command.Parameters.Add(new SqlParameter("@agencyid", userid));
                command.Parameters.Add(new SqlParameter("@totalRecord", 0)).Direction = ParameterDirection.Output;
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_yakkrInfo";
                DataAdapter = new SqlDataAdapter(command);
                yakkrdataTable = new DataTable();
                DataAdapter.Fill(yakkrdataTable);
                if (yakkrdataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < yakkrdataTable.Rows.Count; i++)
                    {
                        Yakkr addYakkrRow = new Yakkr();
                        addYakkrRow.YakkrRoleID = Convert.ToInt32(yakkrdataTable.Rows[i]["YakkrRoleID"]);
                        //addYakkrRow.YakkrID = Convert.ToString(yakkrdataTable.Rows[i]["YakkrID"]);
                        addYakkrRow.YakkrID = Convert.ToString(yakkrdataTable.Rows[i]["YakkrCode"]);
                        addYakkrRow.Description = Convert.ToString(yakkrdataTable.Rows[i]["Description"]);
                        addYakkrRow.Value = Convert.ToString(yakkrdataTable.Rows[i]["Value"]);
                        addYakkrRow.StaffRoleID = Convert.ToString(yakkrdataTable.Rows[i]["StaffRoleID"]);
                        addYakkrRow.SecondaryRoleID = Convert.ToString(yakkrdataTable.Rows[i]["OptionalRoleID"]);
                        addYakkrRow.StaffRoleName = Convert.ToString(yakkrdataTable.Rows[i]["StaffRoleName"]);
                        if (Convert.ToString(yakkrdataTable.Rows[i]["OptionalRole"]) != null)
                            addYakkrRow.OptionalRoleName = Convert.ToString(yakkrdataTable.Rows[i]["OptionalRole"]);
                        else
                            addYakkrRow.OptionalRoleName = string.Empty;
                        addYakkrRow.DateEntered = Convert.ToDateTime(yakkrdataTable.Rows[i]["DateEntered"]).ToString("MM/dd/yyyy");

                        _yakkrlist.Add(addYakkrRow);
                    }
                    totalrecord = command.Parameters["@totalRecord"].Value.ToString();
                }
                return _yakkrlist;
            }
            catch (Exception ex)
            {
                totalrecord = string.Empty;
                clsError.WriteException(ex);
                return _yakkrlist;
            }
            finally
            {
                DataAdapter.Dispose();
                command.Dispose();
                yakkrdataTable.Dispose();
            }
        }
        public Yakkr Getyakkrinfo(string YakkrRoleID, string AgencyId)
        {
            Yakkr obj = new Yakkr();
            try
            {
                command.Parameters.Add(new SqlParameter("@YakkrRoleID", YakkrRoleID));
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_yakkedetailsinfo";
                DataAdapter = new SqlDataAdapter(command);
                yakkrdataTable = new DataTable();
                DataAdapter.Fill(yakkrdataTable);
                if (yakkrdataTable != null && yakkrdataTable.Rows.Count > 0)
                {
                    obj.YakkrRoleID = Convert.ToInt32(yakkrdataTable.Rows[0]["YakkrRoleID"]);
                    obj.Value = yakkrdataTable.Rows[0]["Value"].ToString();
                    obj.StaffRoleID = yakkrdataTable.Rows[0]["StaffRoleID"].ToString();
                    obj.Description = yakkrdataTable.Rows[0]["Description"].ToString();
                    obj.DateEntered = Convert.ToDateTime(yakkrdataTable.Rows[0]["DateEntered"]).ToString("MM/dd/yyyy");
                    obj.SecondaryRoleID = yakkrdataTable.Rows[0]["OptionalRoleID"].ToString();
                    obj.YakkrID = yakkrdataTable.Rows[0]["YakkrID"].ToString();
                    // obj.FormalAgreement = Convert.ToBoolean(yakkrdataTable.Rows[0]["FormalAgreement"].ToString());



                }
                DataAdapter.Dispose();
                command.Dispose();
                yakkrdataTable.Dispose();
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
                yakkrdataTable.Dispose();
            }
        }

        //Changes
        public string Deleteyakkrinfo(string YakkrId)
        {

            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_deleteYakkrDetails";
                command.Parameters.Add(new SqlParameter("@YakkrId", YakkrId));
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
        public Yakkr Getyakkrdetailinfo(string YakkrID, string AgencyId)
        {
            Yakkr obj = new Yakkr();
            try
            {
                command.Parameters.Add(new SqlParameter("@YakkrID", YakkrID));
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_getyakkedetailsinfo";
                DataAdapter = new SqlDataAdapter(command);
                yakkrdataTable = new DataTable();
                DataAdapter.Fill(yakkrdataTable);
                if (yakkrdataTable != null && yakkrdataTable.Rows.Count > 0)
                {
                    if (!DBNull.Value.Equals(yakkrdataTable.Rows[0]["YakkrRoleID"]))
                    {
                        obj.YakkrRoleID = Convert.ToInt32(yakkrdataTable.Rows[0]["YakkrRoleID"]);
                    }
                    else
                    {
                        obj.YakkrRoleID = 0;
                    }
                    obj.Value = yakkrdataTable.Rows[0]["Value"].ToString();
                    obj.StaffRoleID = yakkrdataTable.Rows[0]["StaffRoleID"].ToString();
                    obj.Description = yakkrdataTable.Rows[0]["Description"].ToString();
                    // obj.DateEntered = Convert.ToDateTime(yakkrdataTable.Rows[0]["DateEntered"]).ToString("MM/dd/yyyy");
                    obj.SecondaryRoleID = yakkrdataTable.Rows[0]["OptionalRoleID"].ToString();
                    //   obj.YakkrID = yakkrdataTable.Rows[0]["YakkrCode"].ToString();
                    // obj.FormalAgreement = Convert.ToBoolean(yakkrdataTable.Rows[0]["FormalAgreement"].ToString());



                }
                DataAdapter.Dispose();
                command.Dispose();
                yakkrdataTable.Dispose();
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
                yakkrdataTable.Dispose();
            }
        }

        public List<YakkrDetails> YakkrDetail(ref int yakkrcount, string Agencyid, string userid)
        {
            List<YakkrDetails> YakkrDetailsList = new List<YakkrDetails>();
            try
            {
                command.Parameters.Add(new SqlParameter("@Agencyid", Agencyid));
                command.Parameters.Add(new SqlParameter("@userid", userid));
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_FSwYakkr";
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null && _dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _dataset.Tables[0].Rows)
                    {
                        YakkrDetails info = new YakkrDetails();
                        info.Date = Convert.ToDateTime(dr["DateEntered"]).ToString("MM/dd/yyyy");
                        info.StaffName = dr["staffname"].ToString();
                        info.Status = dr["Status"].ToString();
                        info.Description = dr["Description"].ToString();
                        info.Yakkrid = FingerprintsModel.EncryptDecrypt.Encrypt64(dr["yakkrid"].ToString());
                        info.StaffId = dr["staffid"].ToString();
                        info.HouseholdId = FingerprintsModel.EncryptDecrypt.Encrypt64(dr["HouseholdId"].ToString());
                        yakkrcount = Convert.ToInt32(dr["yakkrcount"]);
                        YakkrDetailsList.Add(info);
                    }

                }

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
            return YakkrDetailsList;
        }


        public int GetYakkrCountByUserId(Guid AgencyId, Guid UserId, string Status)
        {
            int YakkrCount = 0;
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetYakkrDetailsByUserId";
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                command.Parameters.Add(new SqlParameter("@Status", Status));
                command.Parameters.Add(new SqlParameter("@Command", "YakkrCount"));
                Object objCount = command.ExecuteScalar();
                Connection.Close();
                if (objCount != null)
                    YakkrCount = Convert.ToInt32(objCount);
            }
            catch (Exception ex)
            {
                clsError.WriteException(ex);
            }
            finally
            {
                command.Dispose();
                Connection.Close();
            }
            return YakkrCount;
        }

        public List<YakkrDetail> GetYakkrDetail(Guid AgencyId, Guid UserId, string Status)
        {
            List<YakkrDetail> listDetail = new List<YakkrDetail>();
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetYakkrDetailsByUserId";
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                command.Parameters.Add(new SqlParameter("@Status", Status));
                command.Parameters.Add(new SqlParameter("@Command", "YakkrDetail"));
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            listDetail.Add(new YakkrDetail
                            {
                                YakkrCode = dr["YakkrCode"].ToString(),
                                Description = dr["Description"].ToString(),
                                Number = Convert.ToInt32(dr["Number"].ToString())
                            });
                        }
                    }
                }
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
            return listDetail;
        }
        

        public List<YakkrClientDetail> GetYakkrListByCode(Guid AgencyId, Guid UserId, string YakkrCode,string Status)
        {
            List<YakkrClientDetail> listDetail = new List<YakkrClientDetail>();
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetYakkrDetailsByUserId";
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@AgencyId", AgencyId));
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                command.Parameters.Add(new SqlParameter("@YakkrCode", YakkrCode));
                command.Parameters.Add(new SqlParameter("@Status", Status));
                command.Parameters.Add(new SqlParameter("@Command", "YakkrDetailByCode"));
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0 && YakkrCode!="750")
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            listDetail.Add(new YakkrClientDetail
                            {
                                YakkrCode = !string.IsNullOrEmpty(dr["YakkrCode"].ToString()) ? dr["YakkrCode"].ToString() : "",
                                ClientName = !string.IsNullOrEmpty(dr["ClientName"].ToString()) ? dr["ClientName"].ToString() : "",
                                DOB = !string.IsNullOrEmpty(dr["DOB"].ToString()) ? Convert.ToDateTime(dr["DOB"]).ToString("MM/dd/yyyy") : "N/A",
                                CenterName = !string.IsNullOrEmpty(dr["CenterName"].ToString()) ? dr["CenterName"].ToString() : "",
                                FromUser = !string.IsNullOrEmpty(dr["FromUser"].ToString()) ? dr["FromUser"].ToString() : "",
                                Date = !string.IsNullOrEmpty(dr["Date"].ToString()) ? Convert.ToDateTime(dr["Date"]).ToString("MM/dd/yyyy") : "N/A",
                                HouseHoldId = !string.IsNullOrEmpty(dr["HouseHoldId"].ToString()) ? dr["HouseHoldId"].ToString() : "",
                                ClientId = !string.IsNullOrEmpty(dr["ClientId"].ToString()) ? dr["ClientId"].ToString() : "",
                                FromUserID = !string.IsNullOrEmpty(dr["FromUserID"].ToString()) ? dr["FromUserID"].ToString() : "",
                                YakkrID = !string.IsNullOrEmpty(dr["YakkrId"].ToString()) ? dr["YakkrId"].ToString() : "",
                                CenterId = !string.IsNullOrEmpty(dr["CenterId"].ToString()) ? dr["CenterId"].ToString() : "",
                                _EncCenterId= !string.IsNullOrEmpty(dr["CenterId"].ToString()) ? EncryptDecrypt.Encrypt64(dr["CenterId"].ToString()) : "",

                            });
                        }
                    }
                    else if(_dataset.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            listDetail.Add(new YakkrClientDetail
                            {
                                YakkrCode = !string.IsNullOrEmpty(dr["YakkrCode"].ToString()) ? dr["YakkrCode"].ToString() : "",
                                YakkrID = !string.IsNullOrEmpty(dr["YakkrId"].ToString()) ? dr["YakkrId"].ToString() : "",
                               Slots= !string.IsNullOrEmpty(dr["Slots"].ToString()) ? dr["Slots"].ToString() : "",
                                Date = !string.IsNullOrEmpty(dr["Date"].ToString()) ? Convert.ToDateTime(dr["Date"]).ToString("MM/dd/yyyy") : "N/A",

                            });
                        }
                    }
                }
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
            return listDetail;
        }

        //public Dictionary<String, String> SenMailToParentsAndTeachers(Guid UserId,string yakkrId,string RecordType)
        //{
        //    Dictionary<String, String> dictEmail = new Dictionary<string, string>();
        //    try
        //    {
        //        if (Connection.State == ConnectionState.Open)
        //            Connection.Close();
        //        Connection.Open();
        //        command.Parameters.Clear();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "SP_GetTeachersAndParentsEmail";
        //        command.Parameters.Add(new SqlParameter("@UserId", UserId));
        //        command.Parameters.Add(new SqlParameter("@YakkrId", yakkrId));
        //        command.Parameters.Add(new SqlParameter("@RecordType", RecordType));

        //        DataAdapter = new SqlDataAdapter(command);
        //        _dataset = new DataSet();
        //        DataAdapter.Fill(_dataset);
        //        if (_dataset != null)
        //        {
        //            if (_dataset.Tables[0].Rows.Count > 0)
        //            {
        //                int i = 0;
        //                foreach (DataRow dr in _dataset.Tables[0].Rows)
        //                {
        //                    dictEmail.Add("Parent" + i.ToString(), !string.IsNullOrEmpty(dr["Email"].ToString()) ? dr["Email"].ToString() : "");
        //                    i++;
        //                }

        //            }
        //            if (_dataset.Tables[1].Rows.Count > 0)
        //            {
        //                int i = 0;
        //                foreach (DataRow dr in _dataset.Tables[1].Rows)
        //                {
        //                    dictEmail.Add("Teacher" + i.ToString(), !string.IsNullOrEmpty(dr["Email"].ToString()) ? dr["Email"].ToString() : "");
        //                    i++;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsError.WriteException(ex);
        //    }
        //    finally
        //    {
        //        command.Dispose();
        //        Connection.Close();
        //    }
        //    return dictEmail;
        //}



        public Dictionary<String, String> SenMailToParentsAndTeachers(Guid UserId)
        {
            Dictionary<String, String> dictEmail = new Dictionary<string, string>();
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
                Connection.Open();
                command.Parameters.Clear();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetTeachersAndParentsEmail";
                command.Parameters.Add(new SqlParameter("@UserId", UserId));
                DataAdapter = new SqlDataAdapter(command);
                _dataset = new DataSet();
                DataAdapter.Fill(_dataset);
                if (_dataset != null)
                {
                    if (_dataset.Tables[0].Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in _dataset.Tables[0].Rows)
                        {
                            dictEmail.Add("Parent" + i.ToString(), !string.IsNullOrEmpty(dr["Email"].ToString()) ? dr["Email"].ToString() : "");
                            i++;
                        }

                    }
                    if (_dataset.Tables[1].Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in _dataset.Tables[1].Rows)
                        {
                            dictEmail.Add("Teacher" + i.ToString(), !string.IsNullOrEmpty(dr["Email"].ToString()) ? dr["Email"].ToString() : "");
                            i++;
                        }
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
                Connection.Close();
            }
            return dictEmail;
        }
    }
}
