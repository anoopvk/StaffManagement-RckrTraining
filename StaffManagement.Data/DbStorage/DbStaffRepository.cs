using StaffManagement.Interface;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StaffManagement.Data.DbStorage
{
    public class DbStaffRepository : IStaffRepository
    {
        public string connectionString;
        public DbStaffRepository()
        {
            connectionString = "Server=LAPTOP-NOOBIE\\SQLEXPRESS;Integrated security=SSPI;database=StaffManagementDb";
        }
        private int _executeSqlQuery(string query)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int response = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return response;
        }
        private int _executeSqlQueryAndReturnId(string query)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader response = sqlCommand.ExecuteReader();
            int id = -1;
            response.Read();
            if (response.GetValue(0)!=null)
            {
                id = (int)response.GetValue(0);
            }
            sqlConnection.Close();
            return id;
        }
        private void _addAdmminStaffToDB(Staff s)
        {
            AdministrativeStaff administrativeStaff = (AdministrativeStaff)s;
            _executeSqlQuery($"EXEC AddAdminStaff {administrativeStaff.Name}, {administrativeStaff.Section} ");
        }
        private void _addSupportStaffToDb(Staff s)
        {
            SupportStaff supportStaff = (SupportStaff)s;
            _executeSqlQuery($"EXEC AddSupportStaff {supportStaff.Name}, {supportStaff.Building} ");

        }
        private void _addTeachingStaffToDb(Staff s)
        {
            TeachingStaff teachingStaff = (TeachingStaff)s;
            int teachingStaffId =_executeSqlQueryAndReturnId($"EXEC AddTeachingStaff { teachingStaff.Name}");

            if (teachingStaffId == -1)
            {
                return;
            }

            foreach (var item in teachingStaff.SubjectsHandled)
            {
                _executeSqlQuery($"EXEC AddSubjectForTeacher {teachingStaffId} , {item} ");
            }

        }



        public void AddStaff(Staff s)
        {
            

            if (s.GetType() == typeof(AdministrativeStaff))
            {
                Console.WriteLine(typeof(AdministrativeStaff));
                _addAdmminStaffToDB(s);
            }
            else if(s.GetType() == typeof(SupportStaff))
            {
                _addSupportStaffToDb(s);
            }
            else if (s.GetType() == typeof(TeachingStaff))
            {
                _addTeachingStaffToDb(s);
            }

        }



        public bool UpdateStaff(int id, Staff s)
        {

            return false;
        }

        public bool DeleteStaff(int id)
        {
            return false;

        }

        public Staff GetStaff(int staffId)
        {

            return new AdministrativeStaff(5,"asd","asd");
        }

        public List<Staff> GetAllStaff()
        {
            return new List<Staff>();
        }

    }
}
