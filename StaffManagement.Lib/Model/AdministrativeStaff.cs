using System;

namespace StaffManagement.Lib.Model
{
    public class AdministrativeStaff : Staff
    {
        //string[] subjectsHandled = new string[100];
        public String section;

        public AdministrativeStaff(int sId, string sName,String sect): base(sId, sName)
        {
            section = sect;
        }
        public string GetDetails()
        {
            return ("| ID:" + id + "| NAME: " + name + "| SECTION: " + section + "|");
        }
        public override string ToString()
        {
            return $" {base.ToString()} | SECTION: {section}" ;
        }

    }
}