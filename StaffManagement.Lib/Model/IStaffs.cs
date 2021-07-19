using StaffManagement.Lib.Model;


namespace Staffmanagement
{
    public interface IStaffs
    {
        public void AddStaff(Staff s);

        public string ViewStaff(int searchingStaffId = -1);

        public bool UpdateStaff(int id,Staff s);

        public bool DeleteStaff(int id);


    }
}
