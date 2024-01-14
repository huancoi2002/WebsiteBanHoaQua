using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CodeUtility;

public partial class ucMenuArea : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            LoadDataArticle();
            if (SessionUtility.Client != null)
            {
                span_FullName.InnerHtml = SessionUtility.Client.FullName;
                a_Login.Visible = false;
                div_Logout.Visible = true;
            }
            else
            {
                span_FullName.Visible = false;
                a_wellcome.Visible = false;
                div_Logout.Visible = false;
            }
        }
    }

    public void LoadData()
    {
        DBEntities db = new DBEntities();
        var query = from p in db.ProductMainCategories
                    where p.Status == true
                    orderby p.Position
                    select new
                    {
                        ID = p.ProductMainCategoryID,
                        p.Title,
                        SubMenu =
                        (
                            from c in db.ProductCategories
                            where c.Status == true
                            && c.ProductMainCategoryID == p.ProductMainCategoryID
                            orderby c.Position
                            select new
                            {
                                ID = c.ProductCategoryID,
                                c.Title,
                            }
                        )
                    };
        Repeater_MenuArea.DataSource = query.ToList();
        Repeater_MenuArea.DataBind();


        
    }

    public void LoadDataArticle()
    {
        DBEntities db = new DBEntities();
        var query = from p in db.ArticleMainCategories
                    where p.Status == true
                    orderby p.Position
                    select new
                    {
                        ID = p.ArticleMainCategoryID,
                        p.Title
                    };
        Repeater_MenuArticle.DataSource = query.ToList();
        Repeater_MenuArticle.DataBind();
    }
    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/");
    }

    protected void LinkButton_Search_Click(object sender, EventArgs e)
    {
        string keyword = input_Search.Value.Trim();

        //string searchMain = DropDownList_Search.SelectedValue;
        string url = "~/ProductGrid.aspx?keyword=" + keyword;
        Response.Redirect(url);

        //string url;
        //if (searchMain == "search-product")
        //{
        //    url = "~/ProductGrid.aspx?keyword=" + keyword;
        //    Response.Redirect(url);
        //}
        //else if (searchMain == "search-article")
        //{
        //    url = "~/ProductGrid.aspx?keyword=" + keyword;
        //    Response.Redirect(url);
        //}


    }
}