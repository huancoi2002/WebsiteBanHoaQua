using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

using System.Web.Services;

public partial class ProductDetail : System.Web.UI.Page
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
        int id = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();
        var query = from p in db.Products
                    from c in db.ProductCategories
                    where p.Status == true
                    && p.ProductID == id
                    && p.ProductCategoryID == c.ProductCategoryID
                    select new
                    {
                        ID = p.ProductID,
                        p.Title,
                        p.Avatar,
                        p.Thumb,
                        p.Description,
                        p.Content,
                        p.Price,
                        p.OldPrice,
                        p.ImageList,
                        p.ViewTime,
                        p.Quantity,
                        catID = p.ProductCategoryID,
                        MID = c.ProductMainCategoryID
                    };

        Repeater_Product.DataSource = query.ToList();
        Repeater_Product.DataBind();


        var data = query.FirstOrDefault();
        if (data != null)
        {
            //ActiveMenu
            int mid = data.MID.ToInt();
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                DateTime.Now.ToString("ddMMyyyyHHmmss"),
                "activeProductMainMenu('{0}');".StringFormat(mid),
                true);

            //Update lượt xem
            var item = db.Products.Where(q => q.ProductID == id).FirstOrDefault();
            if (item.ViewTime < 0)
            {
                item.ViewTime = 0;
            }
            item.ViewTime += 1;
            db.SaveChanges();

            int catID = data.catID.ToInt();
            LoadRelated(catID);
        }


    }

    public void LoadRelated(int catID)
    {
        int id = Request.QueryString["id"].ToInt();
        DBEntities db = new DBEntities();
        var query = from p in db.Products
                    from c in db.ProductCategories
                    where p.Status == true
                    && p.ProductID != id
                    && p.ProductCategoryID == c.ProductCategoryID
                    && p.ProductCategoryID== catID
                    orderby p.Position
                    select new
                    {
                        ID = p.ProductID,
                        p.Title,
                        p.Avatar,
                        p.Price,
                        p.OldPrice,
                    };
        Repeater_ProductRelated.DataSource = query.Take(10).ToList();
        Repeater_ProductRelated.DataBind();
    }

   
    public static bool GetVisible()
    {
        if (SessionUtility.Client == null)
        {
            return false;
        }
        return true;
    }

    protected void LinkButton_AddCart_Click(object sender, EventArgs e)
    {
        //Khai báo button hiện tại đã được nhấn
        Button Button_AddCart = sender as Button;

        //Lấy ID đang lưu trữ trong thuộc tính
        int id = Button_AddCart.CommandArgument.ToInt();

        //Vào Db lấy ra món hàng
        DBEntities db = new DBEntities();
        var item = db.Products.Where(q => q.ProductID == id).FirstOrDefault();

        if (item == null)
        {
            LoadData();
        }
        var quantity = Request.Form["quantity"].ToInt();
        //Kiểm tra món hàng hiện tại đã có trong giỏ chưa
        CartItem cartItem;

        if (!SessionUtility.Cart.CartItems.ContainsKey(id))
        {

            cartItem = new CartItem();
            cartItem.Quantity = quantity;

            //Gán thông tin khác
            cartItem.ID = id;
            cartItem.Title = item.Title;
            cartItem.Avatar = item.Avatar;
            cartItem.Description = item.Description;
            cartItem.Price = item.Price.ToInt();
            cartItem.OldPrice = item.OldPrice.ToInt();

            //Cho vào giỏ
            SessionUtility.Cart.CartItems.Add(id, cartItem);
        }
        //Nếu chưa có thì tạo mới;
        else
        {
            //Món hàng đã có sẵn
            cartItem = SessionUtility.Cart.CartItems[id];
            cartItem.Quantity += quantity;

        }

    }


    protected void LinkButton_ToCart_Click(object sender, EventArgs e)
    {
        //Khai báo button hiện tại đã được nhấn
        Button LinkButton_ToCart = sender as Button;

        //Lấy ID đang lưu trữ trong thuộc tính
        int id = LinkButton_ToCart.CommandArgument.ToInt();

        //Vào Db lấy ra món hàng
        DBEntities db = new DBEntities();
        var item = db.Products.Where(q => q.ProductID == id).FirstOrDefault();

        if (item == null)
        {
            LoadData();
        }
        //Lấy Số lượng

        var quantity = Request.Form["quantity"].ToInt();

        //Kiểm tra món hàng hiện tại đã có trong giỏ chưa
        CartItem cartItem;

        if (!SessionUtility.Cart.CartItems.ContainsKey(id))
        {

            cartItem = new CartItem();
            cartItem.Quantity = quantity;

            //Gán thông tin khác
            cartItem.ID = id;
            cartItem.Title = item.Title;
            cartItem.Avatar = item.Avatar;
            cartItem.Description = item.Description;
            cartItem.Price = item.Price.ToInt();
            cartItem.OldPrice = item.OldPrice.ToInt();

            //Cho vào giỏ
            SessionUtility.Cart.CartItems.Add(id, cartItem);
        }
        //Nếu chưa có thì tạo mới;
        else
        {
            //Món hàng đã có sẵn
            cartItem = SessionUtility.Cart.CartItems[id];
            cartItem.Quantity += quantity;

        }

        Response.Redirect("~/ProductShoppingCart.aspx");

    }

    protected void Button_AddCart_Click(object sender, EventArgs e)
    {
        //Khai báo button hiện tại đã được nhấn
        LinkButton Button_AddCart = sender as LinkButton;

        //Lấy ID đang lưu trữ trong thuộc tính
        int id = Button_AddCart.CommandArgument.ToInt();

        //Vào Db lấy ra món hàng
        DBEntities db = new DBEntities();
        var item = db.Products.Where(q => q.ProductID == id).FirstOrDefault();

        if (item == null)
        {
            LoadData();
        }

        //Kiểm tra món hàng hiện tại đã có trong giỏ chưa
        CartItem cartItem;

        if (!SessionUtility.Cart.CartItems.ContainsKey(id))
        {

            cartItem = new CartItem();
            cartItem.Quantity = 1;

            //Gán thông tin khác
            cartItem.ID = id;
            cartItem.Title = item.Title;
            cartItem.Avatar = item.Avatar;
            cartItem.Description = item.Description;
            cartItem.Price = item.Price.ToInt();
            cartItem.OldPrice = item.OldPrice.ToInt();

            //Cho vào giỏ
            SessionUtility.Cart.CartItems.Add(id, cartItem);
        }
        //Nếu chưa có thì tạo mới;
        else
        {
            //Món hàng đã có sẵn
            cartItem = SessionUtility.Cart.CartItems[id];
            cartItem.Quantity += 1;

        }

    }
}