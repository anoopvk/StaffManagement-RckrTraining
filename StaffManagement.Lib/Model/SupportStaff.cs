using System;
using System.Collections.Generic;

namespace StaffManagement.Lib.Model
{
    [Serializable]
    public class SupportStaff : Staff
    {
        public string Building
        {
            get;
            set;
        }
        private SupportStaff() : base() { }
        public SupportStaff(int sId, string sName, string sBuilding ) : base(sId, sName)
        {
            Building = sBuilding;
        }

        public override string ToString()
        {
            return $"(Support staffe staff---) {base.ToString()} | BUILDING : {Building}";
        }

    }
} 