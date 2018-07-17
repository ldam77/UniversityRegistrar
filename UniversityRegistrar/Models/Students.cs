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
