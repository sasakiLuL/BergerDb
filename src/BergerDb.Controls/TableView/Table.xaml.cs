using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BergerDb.Controls.TableView;

public partial class Table : ContentView
{
    public static readonly BindableProperty RowsProperty = BindableProperty.Create(
        nameof(Rows),
        typeof(IEnumerable<object>),
        typeof(Table),
        new List<object>(),
        propertyChanged: RowsChanged);

    public static readonly BindableProperty ColumnsProperty = BindableProperty.Create(
        nameof(Columns),
        typeof(IEnumerable<Column>),
        typeof(Table),
        new List<Column>(),
        propertyChanged: ColumnsChanged);

    public IEnumerable<object> Rows
    {
        get => (IEnumerable<object>)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    public IEnumerable<Column> Columns
    {
        get => (IEnumerable<Column>)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    private ObservableCollection<TableValuePair<object, Grid>> _rowsViews = [];

    public ObservableCollection<TableValuePair<object, Grid>> RowsViews
    {
        get => _rowsViews;
        set
        {
            _rowsViews = value;
            OnPropertyChanged(nameof(RowsViews));
        }
    }

    private static void RowsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Table table)
        {
            table.RowsViews.Clear();

            table.AddRowsToTable(table.Rows.Cast<object>());

            BindTableRowsToRows(table, oldValue, newValue);
        }
    }

    private static void ColumnsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Table table)
        {
            table.UpdateColumns();

            if (newValue is IEnumerable<Column> newColumns)
            {
                foreach (var column in newColumns)
                {
                    if (column is INotifyPropertyChanged notifyPropertyChanged)
                    {
                        notifyPropertyChanged.PropertyChanged += (sender, e) =>
                        {
                            table.UpdateColumns();

                            foreach (var row in table.Rows)
                            {
                                table.UpdateRowViewContent(row);
                            }
                        };
                    }
                }
            }

            if (oldValue is IEnumerable<Column> oldColumns)
            {
                foreach (var column in oldColumns)
                {
                    if (column is INotifyPropertyChanged notifyPropertyChanged)
                    {
                        notifyPropertyChanged.PropertyChanged -= (sender, e) =>
                        {
                            table.UpdateColumns();

                            foreach (var row in table.Rows)
                            {
                                table.UpdateRowViewContent(row);
                            }
                        };
                    }
                }
            }

            if (newValue is INotifyCollectionChanged newColumnsCollection)
            {
                newColumnsCollection.CollectionChanged += (sender, e) =>
                {
                    table.UpdateColumns();

                    table.RowsViews.Clear();

                    table.AddRowsToTable(table.Rows.Cast<object>());
                };
            }

            if (oldValue is INotifyCollectionChanged oldColumnsCollection)
            {
                oldColumnsCollection.CollectionChanged -= (sender, e) =>
                {
                    table.UpdateColumns();

                    table.RowsViews.Clear();

                    table.AddRowsToTable(table.Rows.Cast<object>());
                };
            }

            table.RowsViews.Clear();

            table.AddRowsToTable(table.Rows.Cast<object>());
        }
    }

    private static void BindTableRowsToRows(Table table, object oldValue, object newValue)
    {
        if (newValue is INotifyCollectionChanged newChangedCollection)
        {
            newChangedCollection.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        table.AddRowsToTable(e.NewItems!.Cast<object>());
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        table.RemoveRowsFromTable(e.OldItems!.Cast<object>());
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        table.RowsViews.Clear();
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        table.RowsViews.Clear();
                        table.AddRowsToTable(table.Rows.Cast<object>());
                        break;
                }
            };
        }

        if (oldValue is INotifyCollectionChanged oldChangedCollection)
        {
            oldChangedCollection.CollectionChanged -= (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        table.AddRowsToTable(e.NewItems!.Cast<object>());
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        table.RemoveRowsFromTable(e.OldItems!.Cast<object>());
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        table.RowsViews.Clear();
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        table.RowsViews.Clear();
                        table.AddRowsToTable(table.Rows.Cast<object>());
                        break;
                }
            };
        }
    }

    private void RemoveRowsFromTable(IEnumerable<object> objects)
    {
        foreach (var item in objects)
        {
            var pair = RowsViews.FirstOrDefault(x => x.Key == item);

            if (pair is not null)
            {
                if (pair.Key is INotifyPropertyChanged notifyPropertyChanged)
                {
                    notifyPropertyChanged.PropertyChanged -= (sender, e) =>
                    {
                        UpdateRowViewContent(pair.Key);
                    };
                }

                RowsViews.Remove(pair);
            }
        }
    }

    private void AddRowsToTable(IEnumerable<object> objects)
    {
        foreach (var row in objects)
        {
            Grid rowView = GenerateRowView(row);

            if (row is INotifyPropertyChanged notifyPropertyChanged)
            {
                notifyPropertyChanged.PropertyChanged += (sender, e) =>
                {
                    UpdateRowViewContent(row);
                };
            }

            RowsViews.Add(new(row, rowView));
        }
    }

    private void UpdateColumns()
    {
        ColumnsView.Clear();

        ColumnsView.ColumnDefinitions.Clear();

        foreach (var column in Columns.Index())
        {
            ColumnsView.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            
            ColumnsView.Add(column.Item.Header, column.Index, 0);
        }

    }

    private void UpdateRowViewContent(object item)
    {
        var rowView = RowsViews?.FirstOrDefault(x => x.Key == item)?.Value;

        if (rowView is not null)
        {
            rowView.Children.Clear();

            for (int i = 0; i < Columns.Count(); i++)
            {
                var column = Columns.ElementAt(i);

                var view = column.RowTemplate(item);

                rowView.Add(view, i, 0);
            }
        }
    }

    private Grid GenerateRowView(object item)
    {
        var grid = new Grid();

        foreach (var column in Columns.Index())
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            grid.Add(column.Item.RowTemplate(item), column.Index, 0);
        }

        return grid;
    }

    public Table()
    {
        InitializeComponent();
    }
}