using Microsoft.EntityFrameworkCore;
using Online_Courses_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Repository
{
    public class CourseRepository
    {
        Context _context;
        public CourseRepository(Context context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            var courses = _context.Courses
                .Include(c => c.CourseLanguage)
                .Include(c => c.UserCourse)
                .ToList();
            return courses;
        }

        public List<Course> GetAllByCatId(int catId)
        {
            var courses = _context.Courses.Include( c => c.CourseLanguage).Where(c => c.CourseLanguageID == catId).ToList();
            return courses;
        }

        public Course GetByID(int courseID)
        {
            var courses = _context.Courses.Find(courseID);
            return courses;
        }

        public int Add(Course course)
        {
            _context.Courses.Add(course);
            var addCourseResult = _context.SaveChanges();
            return addCourseResult;
        }

        public int Update(Course course)
        {
            _context.Courses.Update(course);
            return _context.SaveChanges();
        }

        public int Delete(int courseID)
        {
            _context.Courses.Remove(new Course() { CourseID = courseID });
            return _context.SaveChanges();
        }
    }
}
