namespace BergerDb.UI.Pages.Customers;

public partial class CustomerPage : ContentPage
{
    protected readonly CustomerPageModel _customerPageModel;

    public CustomerPage(CustomerPageModel customerPageModel)
	{
        InitializeComponent();
        _customerPageModel = customerPageModel;
        BindingContext = _customerPageModel;
    }
}