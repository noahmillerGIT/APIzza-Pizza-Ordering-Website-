using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ICreateItems
    {
        BeverageItem AddBeverageItem(BeverageItem beverageItem, int userId);
        SideItem AddSideItem(SideItem sideItem, int userId);
        List<BeverageItem> GetBeverageItems();
        List<SideItem> GetSideItems();
    }
}
