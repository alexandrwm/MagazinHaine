using MagazinHaine.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazinHaine.BusinessLogic
{
    public class BusinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
