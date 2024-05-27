using BeStreet.Domain.Enums;
using System;

namespace BeStreet.Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public URole Role { get; set; }
    }
}
