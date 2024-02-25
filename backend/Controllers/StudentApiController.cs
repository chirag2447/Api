using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentApiController : ControllerBase
    {
        private readonly ILogger<StudentApiController> _logger;
        private readonly IStudentRepository _studentRepository;
        public StudentApiController(ILogger<StudentApiController> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var students = _studentRepository.GetStudents();
            return Ok(students);
        }

        // public IActionResult AddStudent()
        // {
        //     var courses = _studentRepository.GetCourses();
        //     ViewBag.Courses = new SelectList(courses, "c_id", "c_name");
        //     return View();
        // }

        [HttpPost]
        public IActionResult AddStudent([FromForm] StudentModel student)
        {
            if (student.Photo != null)
            {
                student.c_profile = student.Photo.FileName;
                var path = "../frontend/wwwroot/images/" + student.Photo.FileName;
                using (var stream = System.IO.File.Create(path))
                {
                    student.Photo.CopyTo(stream);
                }

            }

            _studentRepository.AddStudent(student);
            return Ok("Student added");
        }

        [HttpGet("{id}")]
        public IActionResult EditStudent(int id)
        {
            var student = _studentRepository.GetStudent(id);
            var course = _studentRepository.GetCourses();
            // ViewBag.Courses = new SelectList(course, "c_id", "c_name");
            return Ok(student);
        }

        [HttpPut]
        public IActionResult EditStudent([FromForm] StudentModel student)
        {
            if (student.Photo != null)
            {
                student.c_profile = student.Photo.FileName;
                var path = "../frontend/wwwroot/images/" + student.Photo.FileName;
                using (var stream = System.IO.File.Create(path))
                {
                    student.Photo.CopyTo(stream);
                }

            }
            else
            {
                if (student.c_id.HasValue)
                {

                    var old = _studentRepository.GetStudent(student.c_id.Value);
                    student.c_profile = old.c_profile;
                }
            }


            _studentRepository.UpdateStudent(student);
            return Ok("Index");
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            _studentRepository.DeleteStudent(id);
            return Ok("Index");
        }

        [HttpGet]
        [Route("/GetCourses")]
        public IActionResult GetCourses()
        {
            var courses = _studentRepository.GetCourses();
            return Ok(courses);
        }


        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}