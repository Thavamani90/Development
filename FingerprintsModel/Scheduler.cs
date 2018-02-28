﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintsModel
{
    public class Scheduler
    {
        public long MeetingId { get; set; }
        public string MeetingDescription { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        public string MeetingDate { get; set; }
        public string Day { get; set; }
        public string TimeZone { get; set; }
        public bool IsRepeat { get; set; }
        public long RecurringId { get; set; }
        public Guid AgencyId { get; set; }
        public Guid StaffId { get; set; }
        public long ClientId { get; set; }

        public Guid StaffRoleId { get; set; }
        public string Enc_ClientId { get; set; }
        public string ClientName { get; set; }
        public string Duration { get; set; }
        public string title { get; set; }
        public string MeetingNotes { get; set; }
        public string start { get; set; }
        public bool IsRecurrence { get; set; }
        public string end { get; set; }
        public Recurrence Recurrence { get; set; }
        public bool allDay { get; set; }
        public string ParentId { get; set; }
        public string EndDate { get; set; }
        public bool Status { get; set; }
        public int Mode { get; set; }
        public string CreatedDate { get; set; }
        public string InstanceId { get; set; }
        public bool IsDeletedInstance { get; set; }
        public bool IsParentEvent { get; set; }
        public string HouseholdAddress { get; set; }
        public string HouseholdPhoneNo { get; set; }

        public string Enc_CenterId { get; set; }
        public long CenterId { get; set; }

        public long ClassRoomId { get; set; }
        public string Enc_ClassRoomId { get; set; }
        public long ProgramTypeId { get; set; }
        public string Enc_ProgramTypeId { get; set; }

        public string Enc_HouseholdId { get; set; }
        public long HouseholdId { get; set; }

        public List<ParentDetails> ParentDetailsList { get; set; }

        public List<AttendanceType> AttendanceTypeList { get; set; }

        public long AttendanceTypeId { get; set; }
        public bool IsUpdateEnrollment { get; set; }
        public bool IsReSchedule { get; set; }

        public string ParentId1 { get; set; }
        public string ParentId2 { get; set; }

        public string PSignature1 { get; set; }

        public string PSignature2 { get; set; }

        public string RosterYakkr { get; set; }
        // public Center CenterDetails { get; set; }


    }

    public class ParentDetails
    {
        public string ParentName { get; set; }
        public string ClientId { get; set; }
        public string ParentRole { get; set; }

        public string ProfilePicture { get; set; }
    }



}
