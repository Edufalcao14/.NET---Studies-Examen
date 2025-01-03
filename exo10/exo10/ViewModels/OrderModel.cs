using exo9.Models;
using System;
using System.Linq;

namespace exo9.ViewModels;

public class OrderModel
{
    // Wrapping the Order object
    private readonly Order _monOrder;

    // Expose the original Order object for advanced use cases
    public Order MonOrder
    {
        get
        {
            return _monOrder;
        }
    }

    // Constructor to initialize the wrapper with an Order instance
    public OrderModel(Order current)
    {
        _monOrder = current ?? throw new ArgumentNullException(nameof(current));
    }

    // Display properties
    public int DisplayOrderID => _monOrder.OrderId;

    public decimal DisplayOrderTotal
    {
        get
        {
            // Safely calculate the total order amount
            if (_monOrder.OrderDetails == null || !_monOrder.OrderDetails.Any())
            {
                return 0;
            }

            return _monOrder.OrderDetails.Sum(item =>
                    ((decimal)item.Quantity) * item.UnitPrice);
        }
    }

    public string DisplayOrderDate => _monOrder.OrderDate?.ToShortDateString() ?? "N/A";

}
