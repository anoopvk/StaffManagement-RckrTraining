namespace Staffmanagement
{
    public interface IStaffs
    {
        public void AddStaff(int newid);

        public void ViewStaff(int searchingStaffId = -1);

        public void UpdateStaff(int id);

        public void DeleteStaff(int id);


    }
}
