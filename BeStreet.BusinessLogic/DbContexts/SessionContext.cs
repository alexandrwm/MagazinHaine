using BeStreet.Domain.Entities.User;
using System.Data.Entity;

namespace BeStreet.BusinessLogic.DbContexts
{
    public class SessionContext : DbContext
    {
        public virtual DbSet<Session> Sessions { get; set; }
        public SessionContext() : base("name=BeStreet") { }
    }
}
