using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewSample
{
   public class OuterListViewBehavior : Behavior<SfListView>
    {
        SfListView ListView;
        protected override void OnAttachedTo(SfListView bindable)
        {
            ListView = bindable;
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Location",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as ContactInfo_NestedListView);
                    return item.Location.ToString();
                },
            });
            ListView.Loaded += ListView_Loaded;
            base.OnAttachedTo(bindable);
        }

        private async void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            await Task.Delay(2000);
            ListView.RefreshView();
        }
    }
}
