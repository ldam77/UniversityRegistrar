using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityRegistrar.Models;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Controllers
{
  public class StudentsController : Controller
  {
    [HttpGet("/Students")]
    public ActionResult Index()
    {
      List<Student> newStudents = Student.GetAll();
      return View(newStudents);
    }
    [HttpGet("/Students/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/Students")]
    public ActionResult Create(string studentName, string enrollmentDate)
    {
      Student addStudent = new Student(studentName, enrollmentDate);
      addStudent.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/Students/{ID}")]
    public ActionResult Detail(int ID)
    {
      return View(Student.FindById(ID));
    }
  }
}
