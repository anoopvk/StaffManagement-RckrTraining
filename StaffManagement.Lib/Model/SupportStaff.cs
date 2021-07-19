using System;

namespace StaffManagement.Lib.Model
{
    class SupportStaff : Staff
    {
        //string[] subjectsHandled = new string[100];
        String building;

        public SupportStaff(int sId, string sName,String mybuilding):base(sId, sName)
        {
            building = mybuilding;
        }

    }
}