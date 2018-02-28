﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintsModel
{
    public class ReportingModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DOB { get; set; }
        public string Status { get; set; }
        public string Insurance { get; set; }
        public string ColumnName { get; set; }
        public string CenterName { get; set; }
        public string ClassroomName { get; set; }
        public int reporttype { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Guardian { get; set; }
        public string ProgramType { get; set; }
        public string DaysEnrolled { get; set; }
        public List<ReportingModel> Reportlst { get; set; }
    }
}