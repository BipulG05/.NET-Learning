using ASPCoreCRUDUsingADO.Models;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ASPCoreCRUDUsingADO
{
    public class EmployeesDataAccessLayer
    {
        string cs = ConnectionString.dbcs;

        public List<Employees> getAllEmployees()
        {
            List<Employees> emplist = new List<Employees>();
            using(SqlConnection conn = new SqlConnection(cs) )
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                { 
                    Employees employee = new Employees();
                    employee.Id = Convert.ToInt32(reader["ID"]);
                    employee.Name = reader["Name"].ToString()??"";
                    employee.Gender = reader["Gender"].ToString() ?? "";
                    employee.Age = Convert.ToInt32(reader["Age"]);
                    employee.Designation = reader["Designation"].ToString() ?? "";
                    employee.City = reader["City"].ToString() ?? "";
                    emplist.Add( employee );
                }
            }
            return emplist;
        }
        public Employees getEmployeeById(int? id)
        {
            Employees emp = new Employees();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from employees where id = @id",conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employees employee = new Employees();
                    employee.Id = Convert.ToInt32(reader["ID"]);
                    employee.Name = reader["Name"].ToString() ?? "";
                    employee.Gender = reader["Gender"].ToString() ?? "";
                    employee.Age = Convert.ToInt32(reader["Age"]);
                    employee.Designation = reader["Designation"].ToString() ?? "";
                    employee.City = reader["City"].ToString() ?? "";
                    emp = employee;
                }
            }
            return emp;
        }
        public void AddEmployee(Employees employee )
        {
            using(SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name",employee.Name);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@age", employee.Age);
                cmd.Parameters.AddWithValue("@designation", employee.Designation);
                cmd.Parameters.AddWithValue("@city", employee.City);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void UpdateEmployee(Employees employee)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", employee.Id);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@age", employee.Age);
                cmd.Parameters.AddWithValue("@designation", employee.Designation);
                cmd.Parameters.AddWithValue("@city", employee.City);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public void DeleteEmployee(int? id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

    }
}
