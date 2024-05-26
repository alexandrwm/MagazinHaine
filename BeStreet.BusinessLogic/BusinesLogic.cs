using BeStreet.BusinessLogic.BLs;
using BeStreet.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic
{
    public class BusinesLogic
    {
        public IProd GetProdBL()
        {
            return new ProdBL();
        }

        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IMgmtSup GetMgmtSupBL()
        {
            return new MgmtSupBL();
        }
    }
}
