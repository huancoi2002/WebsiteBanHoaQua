using CodeUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucMiniShoppingCart : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        LoadData();
    }

    public void LoadData()
    {
        //Load thông tin thống kê
        b_Count.InnerHtml = SessionUtility.Cart.CountItems.ToString();
        b_Counts.InnerHtml = SessionUtility.Cart.CountItems.ToString();
        b_Amount.InnerHtml = SessionUtility.Cart.Amount.ToString("0,00 đ");
        //span_Amount.InnerHtml = SessionUtility.Cart.Amount.ToString("0,00 đ");
        //hiển thị danh sách
        Repeater_Cart.DataSource = SessionUtility.Cart.CartItems.Values.ToList();
        Repeater_Cart.DataBind();
    }

    protected void LinkButton_Remove_Click(object sender, EventArgs e)
    {
        LinkButton LinkButton_Remove = sender as LinkButton;

        int id = LinkButton_Remove.CommandArgument.ToInt();

        CartItem cartItem = new CartItem();

        //TÌm sản phẩm có ID phù hợp trong giỏ
        CartItem item = SessionUtility.Cart.CartItems[id];

        //Nếu có // Thì gỡ bỏ khỏi giỏ
        if (item != null)
        {
            SessionUtility.Cart.CartItems.Remove(id);
        }
    }
}