using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagement.Interface;
using StaffManagement.Lib.Model;

namespace StaffManagement.Data
{
    public class InMemoryStaffRepository: IStaffRepository
    {

        List<Staff> staffList;
        public InMemoryStaffRepository()
        {
            staffList = new List<Staff>();
        }

        public void AddStaff(Staff s)
        {
            if (s != null)
            {
                staffList.Add(s);
            }
        }
        public Staff GetStaff(int staffId)
        {
            return staffList.Find(x => x.Id == staffId);
        }
        public List<Staff> GetAllStaff()
        {
            return staffList;
        }

        
        public bool UpdateStaff(int id,Staff updatedStaff) 
        {
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
            return true;
        }

        public bool DeleteStaff(int idToDelete) 
        {
            if (staffList.Count == 0)
            {
                return false;
            }
            int staffIndexToDelete  = staffList.FindIndex(x => x.Id == idToDelete);
            if (staffIndexToDelete == -1)
            {
                return false;
            }
            staffList.RemoveAt(staffIndexToDelete);
            return true;
        }
    }
}
