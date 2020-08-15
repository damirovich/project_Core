using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SUBDCORE.Models;


namespace SUBDCORE.Repository
{
    public class Employeerepository : ICrudable<Employees>
    {
        SQLSpAdapter sqladapter;
        List<Employees> employees = new List<Employees>();

        public IEnumerable<Employees> GetList()
        {
            Employees st;
            sqladapter = new SQLSpAdapter("GetEmployee"); //Указываем имя ХП
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                st = new Employees();
                st.PositionNavigation = new Position();
                st.IdEmployees = Convert.ToInt16(row[0]);
                st.FullName = row[1].ToString();
                st.Position = Convert.ToByte(row[2]);
                st.Salary = Convert.ToDecimal(row[3]);
                st.Address = row[4].ToString();
                st.PhoneNumber = row[5].ToString();
                st.PositionNavigation.Position1 = row[6].ToString();
                employees.Add(st);
            }
            return employees;
        }
        public void Create(Employees employees)
        {
            sqladapter = new SQLSpAdapter("AddEmployee");
            sqladapter.AddSqlParameter("@FullName", employees.FullName);
            sqladapter.AddSqlParameter("@Position", employees.Position);
            sqladapter.AddSqlParameter("@Salary", employees.Salary);
            sqladapter.AddSqlParameter("@Address", employees.Address);
            sqladapter.AddSqlParameter("@Phonenumber", employees.PhoneNumber);
            sqladapter.ExecNonQuery();
        }
        public void Update(Employees employees)
        {
            sqladapter = new SQLSpAdapter("UpEmployee");
            sqladapter.AddSqlParameter("@idemployees", employees.IdEmployees);
            sqladapter.AddSqlParameter("@FullName", employees.FullName);
            sqladapter.AddSqlParameter("@Position", employees.Position);
            sqladapter.AddSqlParameter("@Salary", employees.Salary);
            sqladapter.AddSqlParameter("@Address", employees.Address);
            sqladapter.AddSqlParameter("@Phonenumber", employees.PhoneNumber);
            sqladapter.ExecNonQuery();
        }
        public void Delete(int id)
        {
            sqladapter = new SQLSpAdapter("DelEmployee");
            sqladapter.AddSqlParameter("@idemployees", id);
            sqladapter.ExecNonQuery();
        }
      
    }
}
