using CommunityToolkit.Mvvm.ComponentModel;

namespace BergerDb.Controls.TableView;

public partial class TableValuePair<TKey, TValue>(TKey key, TValue value) : ObservableObject
{
    private TKey _key = key;

    private TValue _value = value;

    public TKey Key
    {
        get => _key;
        set => SetProperty(ref _key, value);
    }

    public TValue Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }
}
