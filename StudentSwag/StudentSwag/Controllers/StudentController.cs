using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace StudentSwag.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public List<Student> Get()
        {
            List<Student> students = new List<Student>();
            SqlConnection dbConnection = new SqlConnection("server=(local);Integrated Security=true;Initial Catalog=Practice");
            dbConnection.Open();
            SqlCommand dbCommand = new SqlCommand("Select * from Student", dbConnection);
            SqlDataReader sqlDataReader = dbCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Student student = new Student();
                student.Id = sqlDataReader.GetInt32(0);
                student.Name = sqlDataReader.GetString(1);
                student.Gender = sqlDataReader.GetString(2);
                student.Subject = 0;
                students.Add(student);
            }

            dbConnection.Close();
            return students;
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            SqlConnection dbConnection = new SqlConnection("server=(local);Integrated Security=true;Initial Catalog=Practice");
            dbConnection.Open();
            string insertStatement = " insert into student(name,subject,gender) values (" + "'" + student.Name + "'," + "'" + student.Subject + "'," + "'" + student.Gender + "')";
            SqlCommand dbCommand = new SqlCommand(insertStatement, dbConnection);
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            return Ok();
        }


        [HttpPut]
        public ActionResult UpdateStudent(Student student)
        {
            SqlConnection dbConnection = new SqlConnection("server=(local);Integrated Security=true;Initial Catalog=Practice");
            dbConnection.Open();
            string updateStatement = " update student set name = '" + student.Name + "', gender = '" + student.Gender + "' where id = " + student.Id;
            SqlCommand dbCommand = new SqlCommand(updateStatement, dbConnection);
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            return Ok();
        }


        [HttpDelete]
        public ActionResult DeleteStudent(int studentId)
        {
            SqlConnection dbConnection = new SqlConnection("server=(local);Integrated Security=true;Initial Catalog=Practice");
            dbConnection.Open();
            string deleteStatement = " delete student where id = " + studentId;
            SqlCommand dbCommand = new SqlCommand(deleteStatement, dbConnection);
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            return Ok();
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Subject { get; set; }
    }
}