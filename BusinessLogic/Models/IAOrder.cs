using System;

namespace BusinessLogic.Models
{
    public interface IAOrder
    {
        int OrderNo { get; set; }
        int StoreNo { get; set; }
        int AccountNo { get; set; }
        DateTime OrderDate { get; set; }
        decimal Subtotal { get; set; }
        decimal Tax { get; set; }
        decimal Total { get; set; }
    }
}