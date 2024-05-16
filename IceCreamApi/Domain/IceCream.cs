﻿namespace IceCreamApi.Domain;

public class IceCream
{
  public Guid? Id { get; set; }
  public string Name { get; set; }
  public decimal Price { get; set; }
  public bool OnSale { get; set; }
  public int SaleDiscount { get; set; }

  internal IceCream(){}
  public IceCream(string name, decimal price, bool onSale, int saleDiscount)
  {
    Name = name;
    Price = price;
    OnSale = onSale;
    SaleDiscount = saleDiscount;
  }
}