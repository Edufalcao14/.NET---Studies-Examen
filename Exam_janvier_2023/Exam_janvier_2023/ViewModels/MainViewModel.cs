using Exam_janvier_2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Exam_janvier_2023.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly NorthwindContext _context = new NorthwindContext();

        private List<ProductModel> _productsList = new List<ProductModel>();
        private ProductModel? _selectedProduct;
        private List<string> _categoriesList = new List<string>();
        private List<CountryProductCountModel> _productCountByCountry = new List<CountryProductCountModel>();

        public MainViewModel()
        {
            // Load initial data
            LoadProductCountByCountry();
            ProductsList = LoadProducts();
        }

        public List<ProductModel> ProductsList
        {
            get => _productsList;
            private set
            {
                _productsList = value;
                OnPropertyChanged();
            }
        }

        public ProductModel? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();
                    NotifyCommands();
                }
            }
        }

        public List<string> CategoriesList
        {
            get => _categoriesList ??= LoadCategories();
        }

        public List<CountryProductCountModel> ProductCountByCountry
        {
            get => _productCountByCountry;
            private set
            {
                _productCountByCountry = value;
                OnPropertyChanged();
            }
        }

        private DelegateCommand _discontinueCommand;
        public DelegateCommand DiscontinueCommand => _discontinueCommand ??= new DelegateCommand(() =>
        {
            if (SelectedProduct != null)
            {
                // Mark product as discontinued
                SelectedProduct.Product.Discontinued = true;

                // Update database
                var product = _context.Products.Find(SelectedProduct.DisplayProductID);
                if (product != null)
                {
                    product.Discontinued = true;
                    _context.SaveChanges();

                    // Refresh product list
                    ProductsList = LoadProducts();
                }
            }
        }, () => SelectedProduct != null);

        private List<ProductModel> LoadProducts()
        {
            // Load products from the database and wrap them in ProductModel
            return _context.Products
                .Include(p => p.Supplier)
                .Where(p => !p.Discontinued)
                .Select(p => new ProductModel(p))
                .ToList();
        }

        private List<string> LoadCategories()
        {
            // Load unique categories from the database
            return _context.Categories.Select(c => c.CategoryName).Distinct().ToList();
        }

        private void LoadProductCountByCountry()
        {
            // Group products by supplier country and count them
            ProductCountByCountry = _context.Products
                .Where(p => !p.Discontinued && p.OrderDetails.Any())
                .GroupBy(p => p.Supplier.Country)
                .Select(g => new CountryProductCountModel
                {
                    Country = g.Key ?? "Unknown",
                    ProductCount = g.Count()
                })
                .OrderByDescending(c => c.ProductCount)
                .ToList();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyCommands()
        {
            _discontinueCommand?.RaiseCanExecuteChanged();
        }
    }
}
