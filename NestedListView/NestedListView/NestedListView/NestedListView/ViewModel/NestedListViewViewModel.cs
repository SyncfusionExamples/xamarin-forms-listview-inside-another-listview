using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewSample
{
    public class NestedListViewViewModel : INotifyPropertyChanged
    {

        #region Fields

        private string grouping;
        private SfListView list1;

        #endregion

        #region Properties

        public ObservableCollection<ContactInfo_NestedListView> ContactInfo { get; set; }

        public string Grouping
        {
            get { return grouping; }
            set
            {
                this.grouping = value;
                RaisedOnPropertyChanged("Grouping");
            }
        }


        #endregion

        #region Constructor
        public NestedListViewViewModel()
        {
            GenerateDetails();
        }

        #endregion

        #region Private Methods

        private void GenerateDetails()
        {
            grouping = "ContactName";
            ContactInfo = new ObservableCollection<ContactInfo_NestedListView>
            {
                new ContactInfo_NestedListView(3) {Location="Boston"},
                new ContactInfo_NestedListView(1){Location="Italy"},
                new ContactInfo_NestedListView(2){Location="Chicago"},
                new ContactInfo_NestedListView(1) {Location="Denmark"},
                new ContactInfo_NestedListView(1) {Location="Dallas"},
                new ContactInfo_NestedListView(2) {Location="Tunisia"},
                new ContactInfo_NestedListView(1) {Location="Bangkok"},
                new ContactInfo_NestedListView(1) {Location="Tokyo"}

            };
        }

        #endregion

        #region Interface Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion

    }
}
