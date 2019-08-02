# Working with Nested Xamarin.Forms ListView

ListView allows you to load another ListView inside its [ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemTemplate.html). When the [AutoFitMode](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~AutoFitMode.html) of the outer ListView is height, the size of the inner ListView will be allocated from the maximum screen size. Since the exact size for the inner list cannot not be obtained before loading the view.

To get a fixed height for the inner ListView, define a value in its `HeightRequest`. If the items inside the inner ListView are less, allocate the total extend of the inner list to its `HeightRequest`.

Follow the steps to set the size for the outer ListView based on the extend of inner ListView:

Define a property in the Model class and bind it to the HeightRequest of inner ListView, as the height for various inner ListView has to be identified while scrolling.
Hook the container’s `PropertyChanged` event. When the height of the container changes, set the value of `TotalExtent` to the property bound to the HeightRequest.
Call the `RefreshView` method asynchronously with few seconds delay in the `Loaded` event of the outer SfListView. The height requested for each inner SfListView will be set but the outer SfListView will not get any notification regarding the size change. So, call the RefreshView method asynchronously after loading the view.

```
<listView:SfListView x:Name="listView"  ItemsSource="{Binding ContactInfo}" AutoFitMode="Height" Loaded="listView_Loaded" AllowGroupExpandCollapse="True">
    <listView:SfListView.ItemTemplate>
        <DataTemplate>
            <ViewCell>
                <ViewCell.View>
                    <Grid>
                        <local:ExtendedListView x:Name="list1" HeightRequest="{Binding InnerListHeight}" ItemsSource="{Binding ContactDetails}" ItemSize="75">
                            <local:ExtendedListView.ItemTemplate>
                                <DataTemplate>
                                    <StackLayout>
                                        <Image Source="{Binding Image}" />
                                        <Label Text="{Binding Name}" />
                                        <Label Text="{Binding Number}" />
                                    </StackLayout>
                                </DataTemplate>
                            </local:ExtendedListView.ItemTemplate>
                        </local:ExtendedListView>
                    </Grid>
                </ViewCell.View>
            </ViewCell>
        </DataTemplate>
    </listView:SfListView.ItemTemplate>
</listView:SfListView> 
```
```
public partial class NestedListView : ContentPage
{
    public NestedListView()
    {
        InitializeComponent();
    }

    private async void listView_Loaded(object sender, Syncfusion.ListView.XForms.ListViewLoadedEventArgs e)
    {
        await Task.Delay(2000);
        listView.RefreshView();
    }
}
```
```
using Syncfusion.ListView.XForms.Control.Helpers;
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
            var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(container => container.Name == "TotalExtent").GetValue(container);
            if (e.PropertyName == "Height")
                (this.BindingContext as ContactInfo_NestedListView).InnerListHeight = extent;
        });
    }
}
```

To know more about MVVM in ListView, please refer our documentation [here](https://help.syncfusion.com/xamarin/sflistview/mvvm)
