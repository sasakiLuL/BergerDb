using CommunityToolkit.Mvvm.ComponentModel;

namespace BergerDb.Controls.TableView;

public partial class Column : ObservableObject
{
    private View _header = new Label();

    private Func<object?, View> _rowTemplate = item => new Label();

    public View Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public Func<object?, View> RowTemplate
    {
        get => _rowTemplate;
        set => SetProperty(ref _rowTemplate, value);
    }
}
