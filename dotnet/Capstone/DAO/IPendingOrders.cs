using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IPendingOrders
    {
        IList<PendingOrders> ViewPendingOrders();
        IList<PendingOrders> ViewHistoricalReport();
        IList<PendingOrders> GetPizzaById(int id);
    }
}
