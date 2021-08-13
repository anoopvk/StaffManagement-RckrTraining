using StaffManagement.Interface;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace StaffManagement.Data.DbStorage
{
    public class DbStaffRepository : IStaffRepository
    {
        public string connectionString;
        public DbStaffRepository()
        {
            //connectionString = "Server=LAPTOP-NOOBIE\\SQLEXPRESS;Integrated security=SSPI;database=StaffManagementDb";
            //connectionString = "Data Source=LAPTOP-NOOBIE\\SQLEXPRESS;Integrated security=SSPI;Initial Catalog=StaffManagementDb";

            connectionString = (string)ConfigurationManager.AppSettings.Get("DbConnectionString");
        }



        private void _addAdmminStaffToDB(Staff s)
        {
            AdministrativeStaff administrativeStaff = (AdministrativeStaff)s;

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"AddAdminStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@name", administrativeStaff.Name);
            sqlCommand.Parameters.AddWithValue("@section", administrativeStaff.Section);
            sqlCommand.ExecuteNonQuery();
        }
        private void _addSupportStaffToDb(Staff s)
        {
            SupportStaff supportStaff = (SupportStaff)s;

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"AddSupportStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@name", supportStaff.Name);
            sqlCommand.Parameters.AddWithValue("@building", supportStaff.Building);
            sqlCommand.ExecuteNonQuery();

        }
        private void _addTeachingStaffToDb(Staff s)
        {
            TeachingStaff teachingStaff = (TeachingStaff)s;

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"AddTeachingStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@name", teachingStaff.Name);
            sqlCommand.Parameters.AddWithValue("@subjectHandled", teachingStaff.SubjectHandled);
            sqlCommand.ExecuteNonQuery();

        }

        public void AddStaff(Staff s)
        {


            if (s.GetType() == typeof(AdministrativeStaff))
            {
                _addAdmminStaffToDB(s);
            }
            else if (s.GetType() == typeof(SupportStaff))
            {
                _addSupportStaffToDb(s);
            }
            else if (s.GetType() == typeof(TeachingStaff))
            {
                _addTeachingStaffToDb(s);
            }

        }



        private DataTable _convertStaffListToDataTable(List<Staff> staffList)
        {
            DataTable staffTable = new DataTable();
            staffTable.Columns.Add("Id");
            staffTable.Columns.Add("Name");
            staffTable.Columns.Add("StaffTypeId");
            staffTable.Columns.Add("Section");
            staffTable.Columns.Add("Building");
            staffTable.Columns.Add("SubjectName");
            DataRow row;
            int newId = 0;
            foreach (var staff in staffList)
            {
                row = staffTable.NewRow();
                newId++;
                row["Id"] = staff.Id;
                row["Name"] = staff.Name;

                row["StaffTypeId"] = DBNull.Value;
                row["Section"] = DBNull.Value;
                row["Building"] = DBNull.Value;
                row["SubjectName"] = DBNull.Value;

                if (staff.GetType() == typeof(AdministrativeStaff))
                {
                    row["StaffTypeId"] = 1;
                    row["Section"] = ((AdministrativeStaff)staff).Section;
                }
                else if (staff.GetType() == typeof(SupportStaff))
                {
                    row["StaffTypeId"] = 2;
                    row["Building"] = ((SupportStaff)staff).Building;
                }
                else if (staff.GetType() == typeof(TeachingStaff))
                {
                    row["StaffTypeId"] = 3;
                    row["SubjectName"] = ((TeachingStaff)staff).SubjectHandled;

                }
                staffTable.Rows.Add(row);
            }


            return staffTable;

        }

        public void AddStaffInBulk(List<Staff> staffList)
        {
            DataTable data = _convertStaffListToDataTable(staffList);
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"AddStaffInBulk ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@StaffDetailsInBulk", data);
            sqlCommand.ExecuteNonQuery();
        }





        private bool _updateAdmminStaffToDB(Staff updatedStaff)
        {
            AdministrativeStaff administrativeStaff = (AdministrativeStaff)updatedStaff;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateAdminStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", administrativeStaff.Id);
            sqlCommand.Parameters.AddWithValue("@name", administrativeStaff.Name);
            sqlCommand.Parameters.AddWithValue("@section", administrativeStaff.Section);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        private bool _updateSupportStaffToDb(Staff updatedStaff)
        {
            SupportStaff supportStaff = (SupportStaff)updatedStaff;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateSupportStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", supportStaff.Id);
            sqlCommand.Parameters.AddWithValue("@name", supportStaff.Name);
            sqlCommand.Parameters.AddWithValue("@building", supportStaff.Building);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        private bool _updateTeachingStaffToDb(Staff updatedStaff)
        {
            TeachingStaff teachingStaff = (TeachingStaff)updatedStaff;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateTeachingStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", teachingStaff.Id);
            sqlCommand.Parameters.AddWithValue("@name", teachingStaff.Name);
            sqlCommand.Parameters.AddWithValue("@subjectHandled", teachingStaff.SubjectHandled);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateStaff(int id, Staff updatedStaff)
        {
            if (updatedStaff.GetType() == typeof(AdministrativeStaff))
            {
                return _updateAdmminStaffToDB(updatedStaff);
            }
            else if (updatedStaff.GetType() == typeof(SupportStaff))
            {
                return _updateSupportStaffToDb(updatedStaff);
            }
            else if (updatedStaff.GetType() == typeof(TeachingStaff))
            {
                return _updateTeachingStaffToDb(updatedStaff);
            }

            return false;
        }



        public bool UpdateStaffInBulk(List<Staff> staffList)
        {
            DataTable data = _convertStaffListToDataTable(staffList);




            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"UpdateStaffInBulk ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@StaffDetailsInBulk", data);
            sqlCommand.ExecuteNonQuery();


            return true;
        }


        public bool DeleteStaff(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_DeleteStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", id);


            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;

        }






        private Staff _createAdminStaffObject(SqlDataReader dataReader)
        {
            return new AdministrativeStaff((int)dataReader["Id"], (string)dataReader["Name"], (string)dataReader["Section"]);
        }
        private Staff _createSupportStaffObject(SqlDataReader dataReader)
        {
            return new SupportStaff((int)dataReader["Id"], (string)dataReader["Name"], (string)dataReader["Building"]);
        }
        private Staff _createTeachingStaffObject(SqlDataReader dataReader)
        {
            return new TeachingStaff((int)dataReader["Id"], (string)dataReader["Name"], (string)dataReader["SubjectHandled"]);



        }


        public Staff GetStaff(int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_GetStaffWithId ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", staffId);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                switch ((int)dataReader["StaffTypeId"])
                {
                    case 1:
                        return _createAdminStaffObject(dataReader);
                    case 2:
                        return _createSupportStaffObject(dataReader);
                    case 3:
                        return _createTeachingStaffObject(dataReader);
                    default:
                        return null;
                }
            }
            return null;
        }

        public List<Staff> GetAllStaff()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_GetAllStaff", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            List<Staff> staffList = new List<Staff>();
            bool flag = dataReader.Read();




            while (flag)
            {
                switch ((int)dataReader["StaffTypeId"])
                {
                    case 1:
                        staffList.Add(_createAdminStaffObject(dataReader));
                        flag = dataReader.Read();
                        break;

                    case 2:
                        staffList.Add(_createSupportStaffObject(dataReader));
                        flag = dataReader.Read();
                        break;

                    case 3:
                        staffList.Add(_createTeachingStaffObject(dataReader));
                        flag = dataReader.Read();
                        break;

                    default:
                        return staffList;
                }
            }
            return staffList;

        }

    }
}
