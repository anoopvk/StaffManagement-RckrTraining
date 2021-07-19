using System;
namespace StaffManagement.Lib.Model
{
    public class Staff
    {
        public int id;
        public string name;

        public Staff(int sId, string sName)
        {
            id = sId;
            name = sName;

        }

        public override string ToString()
        {
            return $"| ID: {id} | NAME: {name}";
        }

    }
}