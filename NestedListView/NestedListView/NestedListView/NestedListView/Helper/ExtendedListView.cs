using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewSample
{
    public class ExtendedListView : SfListView
    {

        VisualContainer container;
        public ExtendedListView()
        {
            container = this.GetVisualContainer();
            container.PropertyChanged += Container_PropertyChanged;
        }
        
        private void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var totalextent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(container => container.Name == "TotalExtent").GetValue(container);
                if (e.PropertyName == "Height")
                    (this.BindingContext as ContactInfo_NestedListView).InnerListHeight = totalextent;
            });
        }
    }
}
