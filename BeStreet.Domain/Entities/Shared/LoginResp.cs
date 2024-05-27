using BeStreet.Domain.Enums;

namespace BeStreet.Domain.Entities.Shared
{
    public class LoginResp
    {
        public bool Status { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public URole Role { get; set; }
    }
}
