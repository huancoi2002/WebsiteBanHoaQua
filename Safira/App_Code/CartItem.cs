using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem
{
    public int ID
    {
        get;
        set;
    }

    public string Title
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public string Avatar
    {
        get;
        set;
    }

    public double Price
    {
        get;
        set;
    }

    public double OldPrice
    {
        get;
        set;
    }

    public int Quantity
    {
        get;
        set;
    }

    public double TotalPrice
    {
        get
        {
            double total = 0;
            total = Price * Quantity;
            return total;
        }
    }
}