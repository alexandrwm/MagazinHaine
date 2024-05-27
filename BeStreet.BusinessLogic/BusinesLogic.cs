using BeStreet.BusinessLogic.BLs;
using BeStreet.BusinessLogic.Interfaces;

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

        public IMgmtCus GetMgmtCusBL()
        {
            return new MgmtCusBL();
        }
    }
}
