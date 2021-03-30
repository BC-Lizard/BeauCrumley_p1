namespace BusinessLogic.Models
{
    public interface IAState
    {
        string StateCode { get; set; }
        string StateName { get; set; }
        int StateNo { get; set; }
        decimal TaxRate { get; set; }
    }
}