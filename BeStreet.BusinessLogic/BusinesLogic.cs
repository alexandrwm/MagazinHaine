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

        public IMgmtCus GetMgmtCusBL()
        {
            return new MgmtCusBL();
        }

        public ICart GetCartBL()
        {
            return new CartBL();
        }
    }
}
