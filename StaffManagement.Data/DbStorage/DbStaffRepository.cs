using StaffManagement.Interface;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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
            using SqlDataReader response = sqlCommand.ExecuteReader();
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

        private bool _updateAdmminStaffToDB(Staff updatedStaff)
        {
            AdministrativeStaff administrativeStaff = (AdministrativeStaff)updatedStaff;
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateAdmminStaff {administrativeStaff.Id} , {administrativeStaff.Name}, {administrativeStaff.Section}", sqlConnection);
            if (sqlCommand.ExecuteNonQuery() >= 0)
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
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateSupportStaff {supportStaff.Id} , {supportStaff.Name}, {supportStaff.Building}", sqlConnection);
            if (sqlCommand.ExecuteNonQuery() >= 0)
            {
                return true;
            }
            return false;
        }

        private bool _updateTeachingStaffToDb(Staff updatedStaff)
        {
            TeachingStaff teachingStaff = (TeachingStaff)updatedStaff;


            DataTable subjectTable = new DataTable();
            subjectTable.Columns.Add("Subjects", typeof(string));
            foreach (var subject in teachingStaff.SubjectsHandled)
            {
                subjectTable.Rows.Add(subject);
            }

            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_UpdateTeachingStaff ", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterId = new SqlParameter();
            parameterId.ParameterName = "@id";
            parameterId.Value= teachingStaff.Id;
            sqlCommand.Parameters.Add(parameterId);

            SqlParameter parameterName = new SqlParameter();
            parameterName.ParameterName = "@name";
            parameterName.Value = teachingStaff.Name;
            sqlCommand.Parameters.Add(parameterName);

            SqlParameter parameterSubjects = new SqlParameter();
            parameterSubjects.ParameterName = "@subjects";
            parameterSubjects.Value = subjectTable;
            sqlCommand.Parameters.Add(parameterSubjects);



            var response = sqlCommand.ExecuteNonQuery();
            Console.WriteLine(response);
            return true;
        }

        public bool UpdateStaff(int id, Staff updatedStaff)
        {
            if (updatedStaff.GetType() == typeof(AdministrativeStaff))
            {
                _updateAdmminStaffToDB(updatedStaff);
            }
            else if (updatedStaff.GetType() == typeof(SupportStaff))
            {
                _updateSupportStaffToDb(updatedStaff);
            }
            else if (updatedStaff.GetType() == typeof(TeachingStaff))
            {
                _updateTeachingStaffToDb(updatedStaff);
            }

            return true;
        }

        public bool DeleteStaff(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_DeleteStaff {id} ", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            return true;

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
            int currentId = (int)dataReader["Id"];
            string currentName = (string)dataReader["Name"];
            List<string>  subjectsHandled = new List<string>();
            bool flag = true;
            while ((flag) && ((int)dataReader["Id"] == currentId))
            {
                subjectsHandled.Add((string)dataReader["SubjectName"]);
                flag=dataReader.Read();

            }
            return new TeachingStaff(currentId, currentName, subjectsHandled);
        }


        public Staff GetStaff(int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Proc_GetStaffWithId {staffId}", sqlConnection);
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
                        int currentId = (int)dataReader["Id"];
                        string currentName = (string)dataReader["Name"];
                        List<string> subjectsHandled = new List<string>();
                        while ((flag) && ((int)dataReader["Id"] == currentId))
                        {
                            subjectsHandled.Add((string)dataReader["SubjectName"]);
                            flag = dataReader.Read();

                        }
                        staffList.Add(new TeachingStaff(currentId, currentName, subjectsHandled));
                        break;

                    default:
                        return staffList;
                }
            }

            return staffList;

        }

    }
}
