﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FingerprintsModel
{
    public class RosterNew
    {
        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class Users
        {
            public List<User> Clientlist { get; set; }
            public List<User> UserList { get; set; }
        }

        public class CaseNote
        {
            public string ClientId { get; set; }
            public string CenterId { get; set; }
            public string HouseHoldId { get; set; }
            public string CaseNoteid { get; set; }
            public string ProgramId { get; set; }
            public string ClientIds { get; set; }
            public string StaffIds { get; set; }
            public string CaseNoteDate { get; set; }
            public string CaseNotetitle { get; set; }
            public string CaseNotetags { get; set; }
            public string Note { get; set; }
            public bool CaseNoteSecurity { get; set; }
            public string Classroomid { get; set; }
            public bool IsLateArrival { get; set; }
            public string NewReason { get; set; }

            public int ReasonID { get; set; }
 public string DateOfTransition { get; set; }
        }

        public class ClientUsers
        {
            public string[] IDS { get; set; }
        }
        public class Attachment
        {

            public HttpPostedFileBase file { get; set; }
        }
    }
    public class REF
    {
        public int? ServiceID { get; set; }
        public long? ClientID { get; set; }
        public string Services { get; set; }
        public string Description { get; set; }
        public string ParentName { get; set; }
        public string AgencyID { get; set; }
        public bool? IsFamilyCheckStatus { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool? Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? ParentRole { get; set; }
        public long? CategoryID { get; set; }
        public bool? IsChild { get; set; }
        public bool? IsFamily { get; set; }
        public long? HouseHoldId { get; set; }
        public List<REF> refListData { get; set; }

        public long? ReferralClientServiceId { get; set; }
        public int? Step { get; set; }
        public long? CommunityId { get; set; }
        public string OrganizationName { get; set; }
        public List<MatchProviderModel> MPMList { get; set; }
        public List<SelectListItem> OrganizationList { get; set; }

        public List<ServiceReferences> ServiceRefernceList { get; set; }
    }

    public class REF_ParentList
    {

        public string ParentName { get; set; }
        public List<REF_ParentList> refListData { get; set; }
    }

    public class ServiceReferences
    {

        public string  ServicesName { get; set; }

        public int  ServiceId { get; set; }
    }
 public class ListRoster
    {
        public string id { get; set; }
        public string ServiceId { get; set; }
        public string AgencyId { get; set; }
        public string CommonClientId { get; set; }
        public string HouseHoldId { get; set; }
        public string ClientId { get; set; }
        public long referralClientId { get; set; }
      //  public long? ReferralClientId { get; set; }
      //  public int? Step { get; set; }
        public string clientName { get; set; }
        public string parentName { get; set; }
        public string ReferralDate { get; set; }
        public string Description { get; set; }
        public int ServiceResourceId { get; set; }
        public long CommunityId { get; set; }
        public string CommunityIds { get; set; }
        public long ReferralClientServiceId { get; set; }
    }
    public class ReferralList
    {
        public string id { get; set; }
        public long? ReferralClientId { get; set; }
        public int? Step { get; set; }
        public string clientName { get; set; }
        public string parentName { get; set; }
    }
    public class AttendenceDetailsByDate
    {
        public string id { get; set; }
     
        public string ClientName { get; set; }
        public string ParentName { get; set; }
        public DateTime? AttendenceDate { get; set; }
        public string Center { get; set; }
        public string Class { get; set; }
        public string AttendenceStatus { get; set; }
        public string Meals { get; set; }
        public string SignedInBy { get; set; }
        public string SignedOutBy { get; set; }
        public string StaffName { get; set; }
        public int? Breakfast { get; set; }
        public int? Lunch { get; set; }
        public int? Snacks { get; set; }
        public string SignedInTime { get; set; }
        public string SignedOutTime { get; set; }

    }
	public class CaseNoteNew
    {
        public string ClientId { get; set; }
        public string CenterId { get; set; }
        public string HouseHoldId { get; set; }
        public string CaseNoteid { get; set; }
        public string ProgramId { get; set; }
        public string ClientIds { get; set; }
        public string StaffIds { get; set; }
        public string CaseNoteDate { get; set; }
        public string CaseNotetitle { get; set; }
        public string CaseNotetags { get; set; }
        public string Note { get; set; }
        public bool CaseNoteSecurity { get; set; }
        public string Classroomid { get; set; }

        public string DateOfTransition { get; set; }
    }
}
