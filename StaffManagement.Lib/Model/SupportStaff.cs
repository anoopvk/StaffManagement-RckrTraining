using System.Collections.Generic;

namespace StaffManagement.Lib.Model
{
    public class SupportStaff : Staff
    {
        public string building;

        public SupportStaff(int sId, string sName, string sBuilding ) : base(sId, sName)
        {
            building = sBuilding;
        }

        public override string ToString()
        {
            return $"(Support staff----------) {base.ToString()} | BUILDING : {building}";
        }

    }
} 