using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Frameworks.Abstracts;
using OnlineShop.Domain.Frameworks.Bases;

namespace OnlineShop.Domain.Aggregates.SaleAggregates;

public class OrderHeader : MainEntityBase , ICreatedEntity
{
    // Keys
    public IdentityUser Seller { get; set; }
    public IdentityUser Buyer { get; set; }

    // Properties
    public DateTime CreatedDateGregorian { get; set; }
    public string CreatedDatePersian { get; set; }
}
