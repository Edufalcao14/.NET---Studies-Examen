using exo9.ViewModels;
using System.Windows;

namespace exo9.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = new EmployeeVM();
    }
}