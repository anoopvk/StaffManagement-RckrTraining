using System;

namespace StaffManagement.Lib.Model
{
    public class AdministrativeStaff : Staff
    {
        //string[] subjectsHandled = new string[100];
        String Section
        {
            get;
        }

        public AdministrativeStaff(int sId, string sName,String sect): base(sId, sName)
        {
            Section = sect;
        }


        public override string ToString()
        {
            return $"(Administrative staff---) {base.ToString()} | SECTION: {Section}" ;
        }

    }
}