using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UniversityRegistrar;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    private int id;
    private string name;
    private string enrollmentDate;

    public Student (string newName, string newEnrollmentDate, int newId = 0)
    {
      id = newId;
      name = newName;
      enrollment = newEnrollmentDate;
    }
    public int GetId()
    {
      return id;
    }
    public int GetName()
    {
      return name;
    }
    public int GetEnrollmentDate()
    {
      return enrollmentDate;
    }
    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (this.GetId() == newStudent.GetId());
        bool nameEquality = (this.GetName() == newStudent.GetName());
        bool enrollmentDateEquality = (this.GetEnrollmentDate() == newStudent.GetEnrollmentDate());
        return (idEquality && nameEquality && enrollmentDateEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enrollment_date) VALUES (@inputName, @inputEnrollmentDate);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.name;
      cmd.Parameters.Add(newName);
      MySqlParameter newEnrollmentDate = new MySqlParameter();
      newEnrollmentDate.ParameterName = "@inputEnrollmentDate";
      newEnrollmentDate.Value = this.EnrollmentDate;
      cmd.Parameters.Add(newEnrollmentDate);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static List<Student> GetAll()
    {
      List <Student> newStudent = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string enrollmentDate = rdr.GetString(2);
        Student newStudent = new Student(name, enrollmentDate, id);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }
    public static Student FindById(int searchId)
    {
      int id = 0;
      string name = "";
      string enrollmentDate = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        enrollmentDate = rdr.GetString(2);
      }
      Student foundStudent =  new Student(name, enrollmentDate, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStudent;
    }
    public List<Course> GetCourse()
    {
      List<Course> myCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM students JOIN student_courses ON(students.id = student_courses.student_id) JOIN courses ON (student_course.course_id = courses.id) WHERE students.id = @idParameter;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idParameter";
      parameterId.Value =this.id;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string courseName = rdr.String(1);
        string courseNumber = rdr.String(2);
        Course foundCourse = new Course(courseName, courseNumber, id);
        myCourses.Add(foundCourse);
      }
      if (conn != null)
      {
        conn.Dispose();
      }
      return myCourses;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
