using System;
using System.Xml.Serialization;

namespace StaffManagement.Lib.Model
{
    [Serializable]
    [XmlInclude(typeof(AdministrativeStaff)), XmlInclude(typeof(TeachingStaff)), XmlInclude(typeof(SupportStaff))]
    public abstract class Staff
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;

        }

        public enum TypesOfStaff
        {
            AdministrativeStaff = 1,
            SupportStaff=2,
            TeachingStaff = 3
        }
        public TypesOfStaff StaffType { get; set; }


        public Staff() { }
        //private Staff() { }

        


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