namespace BusinessLogic.Models
{
    public interface IAUser
    {
        int AccountNo { get; set; }
        int DefaultStore { get; set; }
        string Email { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string PasswordHash { get; set; }
        string PasswordSalt { get; set; }
        int Permission { get; set; }
        string Phone { get; set; }
        string Username { get; set; }
    }
}