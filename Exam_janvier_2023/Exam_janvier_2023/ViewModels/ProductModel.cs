using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Exam_janvier_2023.Models;


namespace Exam_janvier_2023.ViewModels
{
    public class ProductModel
    {
        // Wrapping the Product object
        private readonly Product _product;

        // Expose the original Product object for advanced use cases
        public Product Product
        {
            get
            {
                return _product;
            }
        }

        // Constructor to initialize the wrapper with a Product instance
        public ProductModel(Product current)
        {
            _product = current ?? throw new ArgumentNullException(nameof(current));
        }

        // Display properties
        public int DisplayProductID => _product.ProductId;

        public string DisplayProductName => _product.ProductName ?? "Unnamed Product";

        public string DisplayCategory => _product.Category?.CategoryName ?? "No Category";

        public decimal DisplayUnitPrice
        {
            get
            {
                // Safely return the unit price
                return _product.UnitPrice ?? 0;
            }
        }

        public bool IsDiscontinued => _product.Discontinued;

        public string DisplayStockStatus
        {
            get
            {
                // Provide a status based on stock quantity
                if (_product.UnitsInStock == null || _product.UnitsInStock <= 0)
                {
                    return "Out of Stock";
                }

                return $"{_product.UnitsInStock} units available";
            }
        }
        public string DisplaySupplier => _product.Supplier?.CompanyName ?? "No Supplier";

    }
}