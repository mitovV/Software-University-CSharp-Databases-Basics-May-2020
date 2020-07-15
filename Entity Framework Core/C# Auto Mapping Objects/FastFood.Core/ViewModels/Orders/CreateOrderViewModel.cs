namespace FastFood.Core.ViewModels.Orders
{
    using System.Collections.Generic;

    using FastFood.Models.Enums;

    public class CreateOrderViewModel
    {
        public List<ItemOrderViewModel> Items { get; set; }

        public List<EmployeeOrderViewModel> Employees { get; set; }

        public OrderType Type { get; set; }
    }
}
