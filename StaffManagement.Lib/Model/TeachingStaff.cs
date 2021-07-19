using System.Collections;

namespace StaffManagement.Lib.Model
{
    class TeachingStaff : Staff
    {
        //string[] subjectsHandled = new string[100];
        public ArrayList subjectsHandled = new ArrayList();
        public TeachingStaff(int sId, string sName, ArrayList subjects):base( sId,  sName)
        {
            subjectsHandled.AddRange(subjects);
        }

    }
}