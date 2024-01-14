using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class ucSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();

            string keyword = Request.QueryString["keyword"].ToSafetyString();
            int mid = Request.QueryString["mid"].ToInt();

            input_Search.Value = keyword;
            DropDownList_Categories.SelectedValue = mid.ToString();

            input_Search.Focus();
        }
    }

    public void LoadCategories()
    {
        DBEntities db = new DBEntities();
        var query = from m in db.ProductMainCategories
                    where m.Status == true
                    orderby m.Position
                    select new
                    {
                        ID = m.ProductMainCategoryID,
                        m.Title
                    };

        DropDownList_Categories.DataValueField = "ID";
        DropDownList_Categories.DataTextField = "Title";

        var data = query.ToList();
        DropDownList_Categories.DataSource = data;
        DropDownList_Categories.DataBind();



    }

    protected void LinkButton_Search_Click(object sender, EventArgs e)
    {
        //Lấy giá trị trong ô search
        string keyword = input_Search.Value.Trim();//Trim loại bỏ khoảng trắng 2 đầu
        int mid = DropDownList_Categories.SelectedValue.ToInt();
        //Khái báo Link trang đích
        string url = "~/ProductGrid.aspx?mid={0}&keyword={1}";
        //Định dạng Link trang đích
        url = url.StringFormat(mid,keyword);
        //Chuyển đến trang đích
        Response.Redirect(url);
    }
}