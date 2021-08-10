using StaffManagement.Lib.Model;
using System.Collections.Generic;

namespace StaffManagement.Interface
{
    public interface IStaffRepository
    {
        void AddStaff(Staff s);

        void AddStaffInBulk(List<Staff> staffs);

        bool UpdateStaff(int id,Staff s);

        bool UpdateStaffInBulk(List<Staff> staffs);

        bool DeleteStaff(int id);

        Staff GetStaff(int staffId);

        List<Staff> GetAllStaff();
    }
}
