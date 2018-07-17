using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UniversityRegistrar;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    private int id;
    private string courseName;
    private string courseNumber;

    public Student (string newCourseName, string newCourseNumber, int newId = 0)
    {
      id = newId;
      courseName = newCourseName;
      courseNumber = newCourseNumber;
    }
    public int GetId()
    {
      return id;
    }
    public int GetCourseName()
    {
      return courseName;
    }
    public int GetCourseNumber()
    {
      return courseNumber;
    }
    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (this.GetId() == newCourse.GetId());
        bool courseNameEquality = (this.GetCourseName() == newCourse.GetCourseName());
        bool courseNumberEquality = (this.GetCourseNumber() == newCourse.GetCourseNumber());
        return (idEquality && courseNameEquality && courseNumberEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses (course_name, course_number) VALUES (@inputName, @inputNumber);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newCourse = new MySqlParameter();
      newCourse.ParameterName = "@inputCourse";
      newCourse.Value = this.Course;
      cmd.Parameters.Add(newCourse);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
