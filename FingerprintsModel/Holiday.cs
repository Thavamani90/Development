﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerprintsModel
{
    public class Holiday
    {
        public Int32 HolidayID { get; set; }
        public string HolidayName { get; set; }
        public string Description { get; set; }
        public string HolidayDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
