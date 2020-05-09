using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Online_Courses_Core.Models;
using Online_Courses_Core.Repository;
using Online_Courses_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Service
{
    public class CourseService
    {
        private CourseRepository _repo;
        private readonly IHostEnvironment _hostingEnvironment;
        public CourseService(IHostEnvironment hostingEnvironment, CourseRepository repo)
        {
            _repo = repo;
            _hostingEnvironment = hostingEnvironment;
        }

        //public List<CourseDto> GetAll(string rootPath)
        //{
        //    var courses = _repo.GetAll();
        //    var coursesModels = Mapper.Map<List<Course>, List<CourseDto>>(courses);
        //    coursesModels = coursesModels.Select(p => { p.ImagePath = rootPath + p.ImagePath; return p; }).ToList();
        //    return coursesModels;
        //}

        public List<CourseDto> GetAll(string rootPath, PagingModel model)
        {
            var courses = _repo.GetAll();
            var coursesModels = Mapper.Map<List<Course>, List<CourseDto>>(courses);
            coursesModels = coursesModels.Select(p => { p.ImagePath = rootPath + p.ImagePath; return p; })
                    .Skip((model.pageIndex - 1) * model.pageSize)
                    .Take(model.pageSize).ToList();
            return coursesModels;
        }

        public List<CourseDto> GetAllByCatId(string rootPath, int catId)
        {
            var courses = _repo.GetAllByCatId(catId);
            var coursesModels = Mapper.Map<List<Course>, List<CourseDto>>(courses);
            coursesModels = coursesModels.Select(p => { p.ImagePath = rootPath + p.ImagePath; return p; }).ToList();
            return coursesModels;
        }

        public CourseDto GetByID(int courseID, string rootPath)
        {
            var courseEntity = _repo.GetByID(courseID);
            var courseModel = Mapper.Map<Course, CourseDto>(courseEntity);
            courseModel.ImagePath = rootPath + courseModel.ImagePath;
            return courseModel;
        }

        public int Add(CourseDto courseModel, List<IFormFile> files)
        {
            if (files != null)
            {
                var file = files.FirstOrDefault();
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\" + file.FileName);
                using (FileStream filestream = File.Create(path))
                {
                    file.CopyTo(filestream);
                    filestream.Flush();
                    courseModel.ImagePath = "//" + file.FileName;
                }
            }
            var courseEntity = Mapper.Map<CourseDto, Course>(courseModel);
            var addCourseResult = _repo.Add(courseEntity);
            return addCourseResult;
        }

        public int Update(CourseDto courseModel, List<IFormFile> files, string rootPath)
        {
            if (files != null)
            {
                var file = files.FirstOrDefault();
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\" + file.FileName);
                using (FileStream filestream = File.Create(path))
                {
                    file.CopyToAsync(filestream);
                    filestream.FlushAsync();
                    courseModel.ImagePath = "//" + file.FileName;
                }
            }
            else
            {
                var course = GetByID(courseModel.CourseID, rootPath);
                courseModel.ImagePath = course.ImagePath;
            }
            var courseEntity = Mapper.Map<CourseDto, Course>(courseModel);
            var updateCourseResult = _repo.Update(courseEntity);
            return updateCourseResult;
        }

        public int Delete(int courseID)
        {
            var deleteResult = _repo.Delete(courseID);
            return deleteResult;
        }
    }
}
