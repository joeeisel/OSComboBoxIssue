
namespace OSComboBoxIssue
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Data;

    public class ComboBoxViewModel : ObservableObject
    {
        private CollectionViewSource _AdmissionPhysicianList = new CollectionViewSource();
        public ICollectionView PhysicianList => _AdmissionPhysicianList.View;

        private List<Physician> ActiveAdmissionPhysiciansData = new List<Physician>();

        public ComboBoxViewModel()
        {
            #region ThisWouldBeGottenFromADatabase

            ActiveAdmissionPhysiciansData.Add(new Physician { PhysicianKey = 1, FirstName = "John", LastName = "Doe" });

            // NOTE: Setting Jane Smith, the second item as the SelectedPhysician for the ItemsSource.
            //       You will see when run though that the second item is NOT selected in the UI.
            //       Also look at the output window to see the debug output - you will see that the SelectedPhysician is set twice.
            //       Once by us here int the constructor and once by the ComboBox/OpenSilver/WASM.
            this.SelectedPhysician = new Physician { PhysicianKey = 2, FirstName = "Jane", LastName = "Smith" };
            ActiveAdmissionPhysiciansData.Add(this.SelectedPhysician);

            ActiveAdmissionPhysiciansData.Add(new Physician { PhysicianKey = 3, FirstName = "Bob", LastName = "Jones" });
            ActiveAdmissionPhysiciansData.Add(new Physician { PhysicianKey = 4, FirstName = "Sue", LastName = "Johnson" });

            #endregion

            _AdmissionPhysicianList.Source = ActiveAdmissionPhysiciansData;

        }

        private int? _ovrAttendingPhysicianKey;
        public int? OvrAttendingPhysicianKey
        {
            get
            {
                return this._ovrAttendingPhysicianKey;
            }
            set
            {
                Set(() => OvrAttendingPhysicianKey, ref _ovrAttendingPhysicianKey, value);
            }
        }

        private Physician _selectedPhysician;

        public Physician SelectedPhysician
        {
            get { return _selectedPhysician; }
            set
            {
                // This setter is getting called 3 times in this demo app.  In our real app it is getting called twice.  Once with the value we set in the constructor
                // and later with the value set by something else - WASM?  The callstack in this application is different, but the problem is still reproduced in this app.

                _selectedPhysician = value;
                OvrAttendingPhysicianKey = value.PhysicianKey;

                

                //System.Diagnostics.Debug.WriteLine($"SelectedAdmissionPhysician changed.  PhysicianKey = {value.PhysicianKey}");
                Console.WriteLine($"SelectedAdmissionPhysician changed.  PhysicianKey = {value.PhysicianKey}");

                Set(() => SelectedPhysician, ref _selectedPhysician, value);

            }
        }

    }
}