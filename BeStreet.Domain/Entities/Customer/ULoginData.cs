using System;

namespace BeStreet.Domain.Entities.User
{
    public class ULoginData
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
