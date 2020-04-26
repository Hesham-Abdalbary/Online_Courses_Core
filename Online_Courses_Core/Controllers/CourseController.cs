using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Courses_Core.Service;
using Online_Courses_Core.ViewModels;

namespace Online_Courses_Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private CourseService _courseService;
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET api/values
//         [HttpGet]
//         public ActionResult<IEnumerable<CourseDto>> Get()
//         {
//             return _courseService.GetAll(ServerRootPath);
//         }
        
          [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> Get([FromQuery] int pageSize, [FromQuery]int pageIndex )
        {
            PagingModel model = new PagingModel();
            model.pageSize = pageSize;
            model.pageIndex = pageIndex;

            return _courseService.GetAll(ServerRootPath ,model );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CourseDto> Get(int id)
        {
            return _courseService.GetByID(id, ServerRootPath);
        }

        // GET api/values/5
        [HttpGet("language/{catId}")]
        public ActionResult<IEnumerable<CourseDto>> GetByCatId(int catId)
        {
            return _courseService.GetAllByCatId(ServerRootPath, catId);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<int> Post()
        {
            CourseDto course = new CourseDto();
            string jsonCourse = HttpContext.Request.Form["model"];
            course = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseDto>(jsonCourse);
            var files = Request.Form.Files.Count() > 0 ? Request.Form.Files.ToList() : null;
            return _courseService.Add(course, files);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<int> Put(int id)
        {
            CourseDto course = new CourseDto();
            string jsonCourse = HttpContext.Request.Form["model"];
            course = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseDto>(jsonCourse);
            course.CourseID = id;
            var files = Request.Form.Files.Count() > 0 ? Request.Form.Files.ToList() : null;
            return _courseService.Update(course, files, ServerRootPath);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            return _courseService.Delete(id);
        }
    }
}
