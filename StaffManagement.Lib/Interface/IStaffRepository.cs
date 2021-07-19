using StaffManagement.Lib.Model;
using System.Collections.Generic;

namespace StaffManagement.Interface
{
    public interface IStaffRepository
    {
        void AddStaff(Staff s);


        bool UpdateStaff(int id,Staff s);

        bool DeleteStaff(int id);

        Staff GetStaff(int staffId);

        List<Staff> GetAllStaff();
    }
}
