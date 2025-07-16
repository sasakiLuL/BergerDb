using AutoMapper;
using BergerDb.Application.Customers.Get;
using BergerDb.Controls.TableView;
using BergerDb.Domain.Customers;
using BergerDb.UI.Abstractions;
using BergerDb.UI.Models.Customers;
using BergerDb.Shared.Errors;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using System.Collections.ObjectModel;

namespace BergerDb.UI.Pages.Customers;

public partial class CustomerPageModel : ViewModel
{
    //private string id = string.Empty;

    //private long personalId = 0;

    //private string prefix = string.Empty;

    //private string firstName = string.Empty;

    //private string lastName = string.Empty;

    //private Sex sex = Sex.Male;

    //private string emailAddress = "sashakrivko75@gmail.com";

    //private DateTime registeredOnUtc = DateTime.UtcNow;

    //private string notation = string.Empty;

    //private string street = "Westadts. Garten 2";

    //private string city = "Luneburg";

    //private string zipCode = "21335";

    //private PaymentType paymentType = PaymentType.Billing;

    //private MemberType memberType = MemberType.Apothecary;

    //private EntryType entryType = EntryType.GE;

    //private decimal subscriptionCost = 20;

    //private string institution = "KPI";

    private readonly ISender _sender;

    private readonly IMapper _mapper;

    [ObservableProperty]
    private ObservableCollection<Column> _columns = [];

    [ObservableProperty]
    private ObservableCollection<CustomerModel> _customers = [];

    [ObservableProperty]
    public ObservableCollection<string> _errors = [];

    public CustomerPageModel(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;

        Columns = [
            new Column()
            {
                Header = new Label()
                {
                    Text = "Id",
                    FontFamily = "OpenSansSemibold",
                    FontSize = 16,
                    Padding = 16,
                },
                RowTemplate = (item) => new Label()
                {
                    Text = (item as CustomerModel)!.Id.ToString() ?? "Empty",
                    FontSize = 14,
                    Padding = 16,
                }
            },
            new Column()
            {
                Header = new Label()
                {
                    Text = "Prefix",
                    FontFamily = "OpenSansSemibold",
                    FontSize = 16,
                    Padding = 16,
                },
                RowTemplate = (item) => new Label()
                {
                    Text = (item as CustomerModel)!.Prefix ?? "Empty",
                    FontSize = 14,
                    Padding = 16,
                }
            },
            new Column()
            {
                Header = new Label()
                {
                    Text = "Vorname",
                    FontFamily = "OpenSansSemibold",
                    FontSize = 16,
                    Padding = 16,
                },
                RowTemplate = (item) => new Label()
                {
                    Text = (item as CustomerModel)!.FirstName ?? "Empty",
                    FontSize = 14,
                    Padding = 16,
                }
            },
            new Column()
            {
                Header = new Label()
                {
                    Text = "Nachname",
                    FontFamily = "OpenSansSemibold",
                    FontSize = 16,
                    Padding = 16,
                },
                RowTemplate = (item) => new Label()
                {
                    Text = (item as CustomerModel)!.LastName ?? "Empty",
                    FontSize = 14,
                    Padding = 16,
                }
            }
        ];

        var customersResult = _sender.Send(new GetCustomersQuery(new GetCustomersQueryFilters(0, 10))).Result;

        if (customersResult.IsFailure)
        {
            SetErrors(customersResult.Errors);
            return;
        }

        foreach (var customer in customersResult.Value)
        {
            Customers.Add(_mapper.Map<CustomerModel>(customer));
        }
    }

    public Func<int, int, Task> OnPageChanged 
    { 
        get => async (pageSize, page) =>
        {
            var result = await _sender.Send(new GetCustomersQuery(new GetCustomersQueryFilters(page, pageSize)));

            if (result.IsFailure)
            {
                SetErrors(result.Errors);
                return;
            }

            Customers.Clear();

            foreach (var customer in result.Value)
            {
                Customers.Add(_mapper.Map<CustomerModel>(customer));
            }
        }; 
    }

    private void SetErrors(Error[] errors)
    {
        Errors.Clear();

        foreach (var error in errors)
        {
            Errors.Add($"Code: {error.Code}, Message: {error.Message}");
        }
    }
}
