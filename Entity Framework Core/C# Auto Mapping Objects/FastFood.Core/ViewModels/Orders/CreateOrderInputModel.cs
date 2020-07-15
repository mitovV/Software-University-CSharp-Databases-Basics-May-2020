namespace FastFood.Core.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using FastFood.Models.Enums;

    public class CreateOrderInputModel
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public OrderType Type { get; set; }
    }
}
