namespace OSComboBoxIssue
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Xml;

    public sealed partial class Physician : ObservableObject
    {

        private int _physicianKey;
        public int PhysicianKey
        {
            get
            {
                return this._physicianKey;
            }
            set
            {
                Set(() => PhysicianKey, ref _physicianKey, value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                Set(() => FirstName, ref _firstName, value);
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                Set(() => LastName, ref _lastName, value);
            }
        }

        public string PhysicianName
        {
            get
            {
                string name = String.Format("{0} {1}", this.FirstName, this.LastName).Trim();
                if (name == null) return " ";
                if (name.Trim() == "") return " ";
                return name.Trim();
            }
        }


    }
}