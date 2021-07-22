using System;
namespace StaffManagement.Lib.Model
{
    public abstract class Staff
    {
        public int Id
        {
            get;
        }
        public string Name
        {
            get;

        }


        public Staff(int sId, string sName)
        {
            Id = sId;
            Name = sName;

        }

        public override string ToString()
        {
            return $"| ID: {Id} | NAME: {Name}";
        }

    }
}