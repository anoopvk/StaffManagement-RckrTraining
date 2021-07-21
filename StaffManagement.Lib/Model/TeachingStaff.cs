using System.Collections.Generic;

namespace StaffManagement.Lib.Model
{
    public class TeachingStaff : Staff
    {
        public List<string> SubjectsHandled
        {
            get;
        }

        public TeachingStaff(int sId, string sName, List<string> subjectsHandledInput) : base(sId, sName)
        {
            SubjectsHandled = subjectsHandledInput;
        }

        public override string ToString()
        {
            return $"(Teaching staff---------) {base.ToString()} |SUBJECTS HANDLED : {string.Join((" , "), SubjectsHandled.ToArray()) }" ;
        }

    }
    
}