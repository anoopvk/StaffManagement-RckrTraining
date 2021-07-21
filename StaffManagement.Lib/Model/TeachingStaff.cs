using System.Collections.Generic;

namespace StaffManagement.Lib.Model
{
    public class TeachingStaff : Staff
    {
        public List<string> subjectsHandled;

        public TeachingStaff(int sId, string sName, List<string> subjectsHandledInput) : base(sId, sName)
        {
            subjectsHandled = subjectsHandledInput;
        }

        public override string ToString()
        {
            return $"(Teaching staff---------) {base.ToString()} |SUBJECTS HANDLED : {string.Join((" , "), subjectsHandled.ToArray()) }" ;
        }

    }
}