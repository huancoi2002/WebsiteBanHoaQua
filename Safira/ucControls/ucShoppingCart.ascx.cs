using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class ucShoppingCart : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }

    }

    public void LoadData()
    {
        span_Amount.InnerHtml = SessionUtility.Cart.Amount.ToString("0,00 đ");
        span_Amounts.InnerHtml = SessionUtility.Cart.Amount.ToString("0,00 đ");

        Repeater_Main.DataSource = SessionUtility.Cart.CartItems.Values.ToList();
        Repeater_Main.DataBind();

    }

    protected void LinkButton_Decrease_Click(object sender, EventArgs e)
    {
        //Xác định link đang được nhấn
        LinkButton LinkButton_Decrease = sender as LinkButton;
        //Xác đinh ID của sản phẩm sẽ giảm số lượng
        int ID = LinkButton_Decrease.CommandArgument.ToInt();
        //TÌm sản phẩm có ID phù hợp trong giỏ
        CartItem item = SessionUtility.Cart.CartItems[ID];
        //Nếu tìm thấy và số lượng mà > 1 thì:    //Giảm số lượng đi 1
        if (item != null && item.Quantity > 1)
        {
            item.Quantity -= 1;
        }


        //Load Lại trang
        LoadData();
    }

    protected void LinkButton_Increase_Click(object sender, EventArgs e)
    {
        //Xác định link đang được nhấn
        LinkButton LinkButton_Increase = sender as LinkButton;

        //Xác đinh ID của sản phẩm sẽ tăng số lượng
        int ID = LinkButton_Increase.CommandArgument.ToInt();

        //TÌm sản phẩm có ID phù hợp trong giỏ
        CartItem item = SessionUtility.Cart.CartItems[ID];

        //Nếu tìm thấy , thì tăng số lượng lên 1
        if (item != null)
        {
            item.Quantity += 1;
        }
        //Load Lại trang
        LoadData();
    }

    protected void LinkButton_Remove_Click(object sender, EventArgs e)
    {
        //Xác định link đang được nhấn
        LinkButton LinkButton_Remove = sender as LinkButton;

        //Xác đinh ID của sản phẩm sẽ gỡ khỏi giỏ
        int ID = LinkButton_Remove.CommandArgument.ToInt();

        //TÌm sản phẩm có ID phù hợp trong giỏ
        CartItem item = SessionUtility.Cart.CartItems[ID];

        //Nếu có // Thì gỡ bỏ khỏi giỏ
        if (item != null)
        {
            SessionUtility.Cart.CartItems.Remove(ID);
        }


        //Load Lại trang
        LoadData();
    }

    protected void LinkButton_Checkout_Click(object sender, EventArgs e)
    {
        //Lấy url trang hiện tại
        string url = Request.Url.ToString();
        //Bóc tách tên trang hiện tại
        string fileName = Path.GetFileNameWithoutExtension(url).ToLower();


        //Kiểm tra là trang ProductShoppingCart
        if (fileName == "ProductShoppingCart".ToLower())
            // thì chuyển đến trang ProductCheckout
            Response.Redirect("~/Checkout.aspx");
        //Ngược lại gọi lệnh thanh toán
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "checkoutButtonClick();", true);


    }
}