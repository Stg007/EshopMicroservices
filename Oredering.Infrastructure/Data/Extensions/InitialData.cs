using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Oredering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>()
        {
            Customer.Create(CustomerId.Of(new Guid("9d558091-22f3-499c-bce8-33876837b937")),"taha","toto"),
            Customer.Create(CustomerId.Of(new Guid("3b4fadec-e42a-46b7-8d0d-edd7428dd7cc")),"tahi","toti")
        };
    public static IEnumerable<Product> Products => new List<Product>()
        {
            Product.Create(ProductId.Of(new Guid("22018ed1-f9c1-4954-9464-fa071a5f9e82")),"Iphone",100),
            Product.Create(ProductId.Of(new Guid("47e7c317-5602-4d72-9c56-b0b6a40fdd38")),"Samsung",101)
        };
    public static IEnumerable<Order> OrdersWithItems 
    {
        get
        {

            var order = Order.Create(OrderId.Of(new Guid("b5327080-c13a-4b8e-ad1f-4b40b1f77859")), CustomerId.Of(new Guid("9d558091-22f3-499c-bce8-33876837b937")), OrderName.Of("Taha"), Address.Of("taha", "taha", "taha", "taha", "taha"
                , "taha", "taha"), Address.Of("taha", "taha", "taha", "taha", "taha"
                , "taha", "taha"), Payment.Of("taha", "taha", "taha", "123", 1));

            var order2 = Order.Create(OrderId.Of(new Guid("d11b795c-1404-4e0f-8cb2-c78b3545942e")), CustomerId.Of(new Guid("3b4fadec-e42a-46b7-8d0d-edd7428dd7cc")), OrderName.Of("Taha"), Address.Of("taha", "taha", "taha", "taha", "taha"
                , "taha", "taha"), Address.Of("taha", "taha", "taha", "taha", "taha"
                , "taha", "taha"), Payment.Of("taha", "taha", "taha", "123", 1));

            order.Add(Products.First().Id, 1, 100);
            order2.Add(Products.Last().Id, 12, 120);

            return new List<Order>() { order,order2};

        }
    }
}
