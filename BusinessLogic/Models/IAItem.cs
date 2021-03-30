namespace BusinessLogic.Models
{
    public interface IAItem
    {
        string PartDescription { get; set; }
        string PartImage { get; set; }
        string PartName { get; set; }
        int PartNo { get; set; }
        decimal PartPrice { get; set; }
        decimal PartSale { get; set; }
    }
}