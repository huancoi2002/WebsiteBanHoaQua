using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Cart
{

    public int ClientID
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }

    public string FullName
    {
        get;
        set;
    }

    public string Mobi
    {
        get;
        set;
    }


    public bool Gender
    {
        get;
        set;
    }

    public string Address
    {
        get;
        set;
    }

    public int PaymentMethod
    {
        get;
        set;
    }

    private Dictionary<int, CartItem> _CartItems;
    public Dictionary<int, CartItem> CartItems
    {
        get
        {
            if (_CartItems == null)
            {
                _CartItems = new Dictionary<int, CartItem>();
            }
            return _CartItems;
        }
    }

    public double Total
    {
        get
        {
            double total = 0;
            foreach (var item in CartItems)
            {
                total += item.Value.Quantity * item.Value.OldPrice;
            }
            return total;
        }

    }

    public double Bonus
    {
        get
        {
            double bonus = this.Total - this.Amount;
            return bonus;
        }
    }

    public double Amount
    {
        get
        {
            double amount = 0;
            foreach (var item in CartItems)
            {
                amount += item.Value.Price * item.Value.Quantity;
            }
            return amount;
        }
    }

    public int CountItems
    {
        get
        {
            int quantity = 0;
            foreach (KeyValuePair<int , CartItem> item in CartItems)
            {
                quantity += item.Value.Quantity;
            }
            return quantity;
        }

    }
}