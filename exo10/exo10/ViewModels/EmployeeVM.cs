using exo9.Models;
using exo9.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class EmployeeVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private NorthwindContext dc = new NorthwindContext();
    private List<EmployeeModel> _EmployeesList;
    private List<string> _listTitle;
    private List<OrderModel> _RecentOrders;

    private EmployeeModel _selectedEmployee;

    public EmployeeModel SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            if (_selectedEmployee != value)
            {
                _selectedEmployee = value;
                OnPropertyChanged();
                LoadRecentOrders();
            }
        }
    }

    public List<OrderModel> RecentOrders
    {
        get => _RecentOrders;
        private set
        {
            _RecentOrders = value;
            OnPropertyChanged();
        }
    }

    public List<EmployeeModel> EmployeesList
    {
        get => _EmployeesList = _EmployeesList ?? LoadEmployees();
    }

    public List<string> ListTitle
    {
        get => _listTitle = _listTitle ?? LoadTitleOfCourtesy();
    }

    private List<EmployeeModel> LoadEmployees()
    {
        return dc.Employees.Select(e => new EmployeeModel(e)).ToList();
    }

    private List<string> LoadTitleOfCourtesy()
    {
        return dc.Employees.Select(e => e.TitleOfCourtesy).Distinct().ToList();
    }

    public void LoadRecentOrders()
    {
        if (SelectedEmployee != null)
        {
            RecentOrders = dc.Orders
                .Where(o => o.EmployeeId == SelectedEmployee.MonEmployee.EmployeeId)
                .OrderByDescending(o => o.OrderDate)
                .Take(3)
                .Select(o => new OrderModel(o))
                .ToList();
        }
        else
        {
            RecentOrders = new List<OrderModel>();
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
