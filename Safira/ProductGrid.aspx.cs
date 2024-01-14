using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using CodeUtility;

public partial class ProductGrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        LoadData();
        LoadCategory();
    }

    public void LoadData()
    {
        int mid = Request.QueryString["mid"].ToInt();
        int cid = Request.QueryString["cid"].ToInt();

        string keyword = Request.QueryString["keyword"].ToSafetyString();

        //Lấy giá trị từ dropdown sắp xếp, hiển thị
        //int valueShow = DropDownList_LimitProduct.SelectedValue.ToInt();
        int valueSort = DropDownList_Filter.SelectedValue.ToInt();

        DBEntities db = new DBEntities();
        var query = from p in db.Products
                    from c in db.ProductCategories
                    from m in db.ProductMainCategories
                    where p.Status == true
                    && p.ProductCategoryID == c.ProductCategoryID
                    && c.ProductMainCategoryID == m.ProductMainCategoryID
                    select new
                    {
                        ID = p.ProductID,
                        p.Title,
                        p.Thumb,
                        p.Price,
                        p.OldPrice,
                        p.Avatar,
                        CatID = c.ProductCategoryID,
                        MainCatID = c.ProductMainCategoryID,

                        CatTitle = c.Title,
                        MainCatTitle = m.Title
                    };

        if (mid > 0)
        {
            query = query.Where(q => q.MainCatID == mid);

            // //hiển thị theo dạng lưới hoặc danh sách
            // a_ProductGrid.HRef = "ProductGrid.aspx?mid={0}".StringFormat(mid);
            //// a_ProductList.HRef = "ProductList.aspx?mid={0}".StringFormat(mid);


        }

        if (cid > 0)
        {
            query = query.Where(q => q.CatID == cid);

            ////hiển thị theo dạng lưới hoặc danh sách
            //a_ProductGrid.HRef = "ProductGrid.aspx?cid={0}".StringFormat(cid);
            ////a_ProductList.HRef = "ProductList.aspx?cid={0}".StringFormat(cid);
        }

        //Sắp xếp
        if (valueSort == 1)
            query = query.OrderBy(q => q.Price);
        else if (valueSort == 2)
            query = query.OrderByDescending(q => q.Price);
        else if (valueSort == 3)
        {
            query = query.Where(q => q.Price < 1000000);
            query = query.OrderByDescending(q => q.Price);
        }
        else if (valueSort == 4)
        {
            query = query.Where(q => q.Price >= 1000000 && q.Price <= 3000000);
            query = query.OrderByDescending(q => q.Price);
        }
        else if (valueSort == 5)
        {
            query = query.Where(q => q.Price >= 3000000 && q.Price <= 7000000);
            query = query.OrderByDescending(q => q.Price);
        }
        else if (valueSort == 6)
        {
            query = query.Where(q => q.Price > 7000000);
            query = query.OrderByDescending(q => q.Price);
        }
        if (keyword != string.Empty)
        {
            query = query.Where(q => q.Title.Contains(keyword));
        }

        //Đổ vào Repeater
        int pageSize = 12; //10 là số phần tử trên mỗi trang
        int maxPage = 5; //5 là số trang tối đã sẽ hiển thị, còn lại là ...
        int page = Request.QueryString["page"].ToInt(); // Trang hiện tại
        if (page <= 0)
            page = 1;
        int totalItems = query.Count();



        int skip = (page - 1) * pageSize;
        var data = query.Skip(skip).Take(pageSize).ToList();
        // .: Lưu ý sửa lại link cho đúng với trang và điều kiện thực tế của mỗi trang :. \\
        string url = "~/ProductGrid.aspx?mid={0}&cid={1}&page={2}&keyword={3}&view={4}&sort={5}".StringFormat(mid, cid, "{0}", keyword, pageSize, valueSort);
        ucPagging.TotalItems = totalItems;
        ucPagging.CurrentPage = page;
        ucPagging.PageSize = pageSize;
        ucPagging.MaxPage = maxPage;
        ucPagging.URL = url;
        ucPagging.DataBind();
        Repeater_Product.DataSource = data;
        Repeater_Product.DataBind();


        var firstItem = data.FirstOrDefault();
        if (firstItem != null)
        {
            //Active Menu
            int mainID = firstItem.MainCatID.ToInt();
            Page.ClientScript.RegisterStartupScript(
               this.GetType(),
               DateTime.Now.ToString("ddMMyyyyHHmmss"),
               "activeProductMainMenu('{0}');".StringFormat(mainID),
               true);

            //Hiển thị danh mục đầu Trang danh sách
            if (mid > 0)
                span_Categories.InnerHtml = firstItem.MainCatTitle;
            else if (cid > 0)
                span_Categories.InnerHtml = firstItem.CatTitle;
            else
                span_Categories.InnerHtml = "sản phẩm";

        }
    }


    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var data = db.ProductMainCategories.Where(x => x.Status == true).Select(x => new
        {
            ID = x.ProductMainCategoryID,
            Title = x.Title,
            QuantityProduct = db.Products.Count(z => z.ProductCategory.ProductMainCategoryID == x.ProductMainCategoryID)
        }).ToList();

        Repeater_Category.DataSource = data;
        Repeater_Category.DataBind();

        Repeater_Tag.DataSource = data;
        Repeater_Tag.DataBind();
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

    protected void UpdatePanel_Main_PreRender(object sender, EventArgs e)
    {
        LoadData();
    }
}