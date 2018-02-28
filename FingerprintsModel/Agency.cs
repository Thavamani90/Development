﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Web;
namespace FingerprintsModel
{
    public class Agency
    {
        public Nullable<Boolean> Transportation { get; set; }
        public string agencyId { get; set; }
        public string agencyCode { get; set; }
        public string agencyName { get; set; }
        public string primaryEmail { get; set; }
        public string userName { get; set; }
        public string programName { get; set; }
        public string nameGranteeDelegate { get; set; }
        public string grantNo { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string State { get; set; }
        public string zipCode { get; set; }
        public string SpeedZip { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string programstartDate { get; set; }
        public string programendDate { get; set; }
        public string maximumcapacityforalldayClassrooms { get; set; }
        public string maximumcapacityforhalfdayClassrooms { get; set; }
        public Guid createdBy { get; set; }
        public Guid updatedBy { get; set; }
        public char status { get; set; }
        public string agencyIdnonreadable { get; set; }
        public string createdDate { get; set; }
        public string nationality { get; set; }
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string AccessStartDate { get; set; }
        public string AccessDays { get; set; }
        public string AccessStart { get; set; }
        public string AccessStop { get; set; }
        public List<TimeZoneinfo> TimeZonelist = new List<TimeZoneinfo>();
        public string TimeZoneID { get; set; }
        public string ProgramStartTime { get; set; }
        public string DocsStorage { get; set; }
        public string ActiveProgYear { get; set; }
        public string ProgramEndTime { get; set; }
        public string FSWYearlyVisit { get; set; }
        public string Yakkr600 { get; set; }
        public string Yakkr601 { get; set; }
        public string Areabreakdown { get; set; }
        public HttpPostedFileBase logo { get; set; }
        public string logoFileName { get; set; }
        public string logoFileExtension { get; set; }
        public string logourl { get; set; }
        public int Slots { get; set; }
        public int SlotId { get; set; }
        public string LastLogin { get; set; }
        public List<FundSource> FundSourcedata = new List<FundSource>();
        public class FundSource
        {
            public Int32 FundID { get; set; }
            public string ProgramYear { get; set; }
            public string Acronym { get; set; }
            public string Description { get; set; }
            public int Amount { get; set; }
            public string Date { get; set; }
            public string Duration { get; set; }
            public string ServiceQty { get; set; }
            public string fundingtype { get; set; }
            public string nameGranteeDelegate { get; set; }
            public string grantNo { get; set; }
            public string OldFund { get; set; }
            public string ProgramStartTime { get; set; }
            public string fundsource { get; set; }
            public string ProgramEndTime { get; set; }
            public int FundStatus { get; set; }
            public List<ProgramType> progtypelist { get; set; }

            ////Fund question
            public string FundQ1 { get; set; }
            public string FundQ2 { get; set; }
            public string FundQ3 { get; set; }
            public string FundQ4 { get; set; }
            public string FundQ5 { get; set; }
            public string FundQ6 { get; set; }
            public string FundQ7 { get; set; }
            public string FundQ8 { get; set; }
            public string FundQ9 { get; set; }
            public string FundQ10 { get; set; }
            public string FundQ11 { get; set; }
            public string FundQ12 { get; set; }
            public string FundQ13 { get; set; }
            public string FundQ14 { get; set; }
            public string FundQ15 { get; set; }
            public string FundQ16 { get; set; }



        }

        public class ProgramType
        {
            public Int32 ProgramID { get; set; }
            public Int32 FundID { get; set; }
            public string OldFund { get; set; }
            public string AgencyId { get; set; }
            public string ProgramTypes { get; set; }
            public string Description { get; set; }
            public bool PIRReport { get; set; }
            public string Slots { get; set; }
            public string GranteeNumber { get; set; }
            public string Area { get; set; }
            public string StartTime { get; set; }
            public string StopTime { get; set; }
            public bool HealthReview { get; set; }
            public int MinAge { get; set; }
            public int MaxAge { get; set; }
            public int ProgStatus { get; set; }
            public int ProgYear { get; set; }
            public int ProgEndYear { get; set; }
            public string ReferenceProg { get; set; }
            public string programstartDate { get; set; }
            public string programendDate { get; set; }
            public int WorkingDays { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public List<ReferenceProgInfo> refList = new List<ReferenceProgInfo>();

            public class ReferenceProgInfo
            {
                public string Id { get; set; }
                public string Name { get; set; }
            }
        }

    }
    public class Agencystaffreport
    {
        public string Contractor { get; set; }
        public string AssociatedProgram { get; set; }
        public string totalAssociatedProgram { get; set; }


    }
    public class Agencyreport
    {
        public List<Agencystaffreport> Agencystaffreport { get; set; }
        public string terminationdate { get; set; }
        public string hiredate { get; set; }
        public string totalhdstarterlyhdstart { get; set; }
        public string totalcontracterhdstarterlyhdstart { get; set; }
        public string totalreplaced { get; set; }
        public string totalreplacedcontrator { get; set; }
        public string Contractortotalhired { get; set; }
        public string Contractortotalterminated { get; set; }
    }

    public class AgencySlots
    {

        public List<SelectListItem> RefProgram { get; set; }
        public DataTable _CenterTable { get; set; }
        public DataTable _Centerprogram { get; set; }
        public string ProgramType { get; set; }
        public string SlotPurchased { get; set; }
        public string SlotAllocated { get; set; }
        public string Slots { get; set; }
        public bool MenuEnabled { get; set; }
    }

   

    public class AgencyAdditionalSlots
    {
        public int SlotId { get; set; }
        public int SlotsCount { get; set; }
       public string ProgramType { get; set; }
        public string Slot { get; set; }
        public string ExistingSlot { get; set; }
        public string AgencyId { get; set; }
        public int CenterId { get; set; }
        public int ClassroomId { get; set; }
        public int Seats { get; set; }
    }

    public class HomeBased
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public bool Absent { get; set; }
        public bool Present { get; set; }
    }


}
