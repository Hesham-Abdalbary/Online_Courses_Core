﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Models
{
    public class CompanyContactInfo
    {
        [Key]
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? StartWorkingHour { get; set; }
        public DateTime? EndWorkingHour { get; set; }
        public string MobileNumber { get; set; }
        public string SecondLandLine { get; set; }
        public string ImagePath { get; set; }
        public string CompanyUrl { get; set; }
        public string Address { get; set; }
    }
}
