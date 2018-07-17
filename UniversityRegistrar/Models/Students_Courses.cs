using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UniversityRegistrar;

namespace UniversityRegistrar.Models
{
  public class StudentCourse
  {
    private int id;
    private int studentID;
    private int courseID;

    public StudentCourse (int newStudentID, int newCourseID, int newId = 0)
    {
      id = newId;
      studentID = newStudentID;
      courseID = newCourseID;
    }
    public int GetId()
    {
      return id;
    }
    public int GetStudentID()
    {
      return studentID;
    }
    public int GetCourseID()
    {
      return courseID;
    }
    public override bool Equals(System.Object otherStudentCourse)
    {
      if (!(otherStudentCourse is StudentCourse))
      {
        return false;
      }
      else
      {
        StudentCourse newStudentCourse = (StudentCourse) otherStudentCourse;
        bool idEquality = (this.GetId() == newStudentCourse.GetId());
        bool studentIDEquality = (this.GetStudentID() == newStudentCourse.GetStudentID());
        bool courseIDEquality = (this.GetCourseID() == newStudentCourse.GetCourseID());
        return (idEquality && studentIDEquality && courseIDEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@inputStudentID, @inputCourseID);";
      MySqlParameter newStudentID = new MySqlParameter();
      newStudentID.ParameterName = "@inputStudentID";
      newStudentID.Value = this.studentID;
      cmd.Parameters.Add(newStudentID);
      MySqlParameter newCourseID = new MySqlParameter();
      newCourseID.ParameterName = "@inputCourseID";
      newCourseID.Value = this.courseID;
      cmd.Parameters.Add(newCourseID);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static StudentCourse FindById(int searchId)
    {
      int id = 0;
      int studentId = 0;
      int courseId = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students_courses WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        studentId = rdr.GetInt32(1);
        courseId = rdr.GetInt32(2);
      }
      StudentCourse foundStudentCourse =  new StudentCourse(studentId, courseId, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStudentCourse;
    }
    public static List<StudentCourse> GetAll()
    {
      List <StudentCourse> newStudentCourses = new List<StudentCourse> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students_courses;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int studentId = rdr.GetInt32(1);
        int courseId = rdr.GetInt32(2);
        StudentCourse newStudentCourse = new StudentCourse(studentId, courseId, id);
        newStudentCourses.Add(newStudentCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStudentCourses;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
