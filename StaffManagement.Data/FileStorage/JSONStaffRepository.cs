using StaffManagement.Interface;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace StaffManagement.Data.FileStorage
{
    public class JSONStaffRepository : IStaffRepository
    {
        string fileName;
        JsonSerializer jsonSerializer;
        public JSONStaffRepository()
        {
            string jsonFileNameFromConfig = ConfigurationManager.AppSettings["JsonFileName"];
            fileName = "DefaultFileName.json";
            if (!String.IsNullOrWhiteSpace(jsonFileNameFromConfig))
            {
                fileName = jsonFileNameFromConfig;
            }
            jsonSerializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
        }

        private List<Staff> _getDataFromJson()
        {
            if (File.Exists(fileName) == false)
            {
                return new List<Staff>();
            }
            using var streamReader = new StreamReader(fileName);
            if (streamReader.Peek() == -1)
            {
                return new List<Staff>();
            }

            using JsonReader jsonReader = new JsonTextReader(streamReader);
            List<Staff> myObjectFromJson = (List<Staff>)jsonSerializer.Deserialize(jsonReader,typeof(List<Staff>));
            return myObjectFromJson;
        }
        private void _setDataToJson(List<Staff> staffList)
        {
            

            using StreamWriter myWriter = new StreamWriter(fileName);
            using JsonWriter jsonWriter = new JsonTextWriter(myWriter);
            jsonSerializer.Serialize(myWriter ,staffList);
            
        }
        public void AddStaff(Staff staff)
        {
            if (staff != null)
            {
                List<Staff> staffList = _getDataFromJson();
                staffList.Add(staff);
                _setDataToJson(staffList);
            }


        }
        public Staff GetStaff(int staffId)
        {
            List<Staff> staffList = _getDataFromJson();
            return staffList.Find(x => x.Id == staffId);
        }
        public List<Staff> GetAllStaff()
        {
            return _getDataFromJson();
        }
        public bool UpdateStaff(int id, Staff updatedStaff)
        {
            List<Staff> staffList = _getDataFromJson();
            if (staffList.Count == 0)
            {
                return false;
            }
            int staffIndexToUpdate = staffList.FindIndex(x => x.Id == id);
            if (staffIndexToUpdate == -1)
            {
                return false;
            }
            staffList[staffIndexToUpdate] = updatedStaff;
            _setDataToJson(staffList);
            return true;
        }
        public bool DeleteStaff(int idToDelete)
        {
            List<Staff> staffList = _getDataFromJson();
            if (staffList.Count == 0)
            {
                return false;
            }
            int staffIndexToDelete = staffList.FindIndex(x => x.Id == idToDelete);
            if (staffIndexToDelete == -1)
            {
                return false;
            }
            staffList.RemoveAt(staffIndexToDelete);
            _setDataToJson(staffList);
            return true;

        }
    }

}

