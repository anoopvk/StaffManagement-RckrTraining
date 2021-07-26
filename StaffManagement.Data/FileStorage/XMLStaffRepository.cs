using StaffManagement.Interface;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StaffManagement.Data.FileStorage
{
    public class XMLStaffRepository: IStaffRepository
    {
        string fileName;
        XmlSerializer xmlSerializer;
        
        public XMLStaffRepository()
        {
            fileName = "MyFileForSavingXmlData.xml";
            xmlSerializer = new XmlSerializer(typeof(List<Staff>));
        }



        private List<Staff> _GetDataFromXML()
        {
            if (File.Exists(fileName)==false)
            {
                return new List<Staff>();
            }
            //use streamreader here
            using var streamReader = new StreamReader(fileName);
            if (streamReader.Peek() == -1)
            {
                return new List<Staff>();
            }
            
            var myObjectFromXml = (List<Staff>)xmlSerializer.Deserialize(streamReader);
            //fileStream.Close();
            return myObjectFromXml;
        }
        private void _SetDataIntoXML(List<Staff> staffList)
        {
            using StreamWriter myWriter = new StreamWriter(fileName);
            xmlSerializer.Serialize(myWriter, staffList);
            //myWriter.Close();
        }
        
        
        public void AddStaff(Staff staff)
        {
            if (staff != null)
            {
                List<Staff> staffList = _GetDataFromXML();
                staffList.Add(staff);
                _SetDataIntoXML(staffList);
            }


        }
        public Staff GetStaff(int staffId)
        {
            List<Staff> staffList = _GetDataFromXML();
            return staffList.Find(x => x.Id == staffId);
        }
        public List<Staff> GetAllStaff()
        {
            return _GetDataFromXML();
        }
        public bool UpdateStaff(int id, Staff updatedStaff)
        {
            List<Staff> staffList = _GetDataFromXML();
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
            _SetDataIntoXML(staffList);
            return true;
        }
        public bool DeleteStaff(int idToDelete)
        {
            List<Staff> staffList = _GetDataFromXML();
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
            _SetDataIntoXML(staffList);
            return true;

        }
    }
}
