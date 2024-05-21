using BeStreet.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.DbContexts
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=BeStreet") { }
        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
