using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace employeesAPI.Models
{
    public class EmployeeDB
    {
        string connectionString = "Data Source=PK-KTCRA218; Initial Catalog=employees;Persist Security Info=False;User ID=sa;password=Admin!23;";

        //get all employees
        public IEnumerable<Employee> getAllEmployee()
        {
            List<Employee> empList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_getAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Department = dr["Department"].ToString();
                    emp.Username = dr["Username"].ToString();
                    emp.Password = dr["Password"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }

        //insert employee
        public void addEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Username", emp.Username);
                cmd.Parameters.AddWithValue("@Password", emp.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //updat eemployee
        public void updateEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", emp.ID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Username", emp.Username);
                cmd.Parameters.AddWithValue("@Password", emp.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //delete employee
        public void deleteEmployee(int? empId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //get employee by ID
        public Employee getEmployeeByID(int empId)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_getEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.ID = Convert.ToInt32(dr["ID"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Department = dr["Department"].ToString();
                    emp.Username = dr["Username"].ToString();
                    emp.Password = dr["Password"].ToString();
                }
                con.Close();
            }
            return emp;
        }
    }
}
