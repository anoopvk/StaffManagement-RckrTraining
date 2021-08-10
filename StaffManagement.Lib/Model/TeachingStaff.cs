using System;
using System.Collections.Generic;

namespace StaffManagement.Lib.Model
{
    [Serializable]
    public class TeachingStaff : Staff
    {
        public string SubjectHandled
        {
            get;     
            set;
        }
        private TeachingStaff() : base() { }
        public TeachingStaff(int sId, string sName, string subjectsHandledInput) : base(sId, sName)
        {
            SubjectHandled = subjectsHandledInput;
        }

        public override string ToString()
        {
            return $"(Teaching staff---------) {base.ToString()} |SUBJECTS HANDLED : {SubjectHandled}" ;
        }

    }
    
}