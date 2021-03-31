using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class AUser : IAUser
    {
        private readonly IProcessLogger _logger;
        public AUser() { }

        public AUser(List<string> data, IProcessLogger logger)
        {
            AccountNo = int.Parse(data[0]);
            Firstname = data[1];
            Lastname = data[2];
            Username = data[3];
            PasswordSalt = data[4];
            PasswordHash = data[5];
            Phone = data[6];
            Email = data[7];
            Permission = int.Parse(data[8]);
            DefaultStore = int.Parse(data[9]);
            _logger = logger;
            _logger.Log($"USER: {Firstname.ToUpper()} {Lastname.ToUpper()} with username {Username.ToUpper()}", true);
        }

        public int AccountNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
        public int DefaultStore { get; set; }
    }
}
