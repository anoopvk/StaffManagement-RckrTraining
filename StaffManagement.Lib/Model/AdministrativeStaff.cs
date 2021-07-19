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
        public void PrintDetails()
        {
            Console.WriteLine("| ID:" + id + "| NAME: " + name + "| SECTION: " + section + "|");
        }


    }
}