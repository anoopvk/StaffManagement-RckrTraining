using System;

namespace StaffManagement.Lib.Model
{
    [Serializable]
    public class AdministrativeStaff : Staff
    {
        //string[] subjectsHandled = new string[100];
        public String Section
        {
            get;
            set;
        }
        public AdministrativeStaff(int sId, string sName, String sect) : base(sId, sName)
        {
            Section = sect;
        }
        private AdministrativeStaff() : base() { }


        public override string ToString()
        {
            return $"(Administrative staff---) {base.ToString()} | SECTION: {Section}" ;
        }

    }
}