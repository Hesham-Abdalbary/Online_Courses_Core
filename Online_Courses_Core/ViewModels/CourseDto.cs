using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.ViewModels
{
    public class CourseDto
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public decimal TotalCourseHours { get; set; }
        public string MobileNumber { get; set; }
        public string ImagePath { get; set; }
        public string DemoUrl { get; set; }
        public string CertificateTemplatePath { get; set; }
        public string Description { get; set; }
        public decimal PassPercentage { get; set; }
        public int? CourseStatusID { get; set; }
        public CourseStatusDto CourseStatus { get; set; }
        public int? CourseLanguageID { get; set; }
        public CourseLanguageDto CourseLanguage { get; set; }
        public virtual List<CourseFileDto> Files { get; set; }
        public virtual List<UserCourseDto> UserCourse { get; set; }
    }


    public class PagingModel
    {
        public int pageSize { get; set; }

        public int pageIndex { get; set; }
    }
}
