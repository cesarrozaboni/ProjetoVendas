using Domain.Enums;
using UtilExtensionMethods;

namespace Domain.Sales
{
    public class SalesStatus
    {
        ICollection<ListStatus> SalesSatus { get; set; }

        public ICollection<ListStatus> GetEnumSales()
        {
            SalesSatus = new List<ListStatus>();

            for (int index = 0; index < Enum.GetValues(typeof(SaleStatusModel)).Length; index++)
            {
                SalesSatus.Add(new ListStatus { 
                                                 Id     = index,
                                                 Status = ((SaleStatusModel)index).GetEnumDescription()
                                              });
            }
            return SalesSatus;
        }
    }
    
    public class ListStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    
}

