using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Controllers
{
  public class CoursesController : Controller
  {
    [HttpGet("/Courses")]
    public ActionResult Index()
    {
      List<Course> newCourses = Course.GetAll();
      return View(newCourses);
    }
    [HttpGet("/Courses/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/Courses")]
    public ActionResult Create(string courseName, string courseNumber)
    {
      Course addCourse = new Course(courseName, courseNumber);
      addCourse.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/Courses/{ID}")]
    public ActionResult Detail(int ID)
    {
      return View(Course.FindById(ID));
    }
    [HttpPost("/Courses/{CourseID}")]
    public ActionResult AddStudent(string studentName, int CourseID)
    {
      StudentCourse newPair = new StudentCourse(Student.FindByName(studentName).GetId(), CourseID);
      newPair.Save();
      return RedirectToAction("Detail", new {ID = CourseID});
    }
  }
}
