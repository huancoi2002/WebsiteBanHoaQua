using CodeUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArticleDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            LoadCategory();
        }

    }
    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var data = db.ArticleMainCategories.Where(x => x.Status == true).Select(x => new
        {
            ID = x.ArticleMainCategoryID,
            Title = x.Title,
            QuantityProduct = db.Products.Count(z => z.ProductCategory.ProductMainCategoryID == x.ArticleMainCategoryID)
        }).ToList();

        Repeater_Category.DataSource = data;
        Repeater_Category.DataBind();

        Repeater_Tag.DataSource = data;
        Repeater_Tag.DataBind();
    }
    public void LoadData()
    {
        int id = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();
        var query = from a in db.Articles
                    where a.Status == true
                    && a.ArticleID == id
                    select new
                    {
                        ID = a.ArticleID,
                        a.Title,
                        a.Description,
                        a.Avatar,
                        a.Content,
                        a.CreateBy,
                        a.CreateTime,
                        a.ViewTime,
                        a.ArticleCategoryID
                    };
        Repeater_ArticleDetail.DataSource = query.ToList();
        Repeater_ArticleDetail.DataBind();
        var data = query.FirstOrDefault();
        if (data != null)
        {
            int catID = data.ArticleCategoryID.ToInt();
            LoadRelated(catID);
        }
        else
        {
            Response.Redirect("~/404error.aspx");
        }
    }

    public void LoadRelated(int catID)
    {
        int id = Request.QueryString["id"].ToInt();
        DBEntities db = new DBEntities();
        var query = from a in db.Articles
                    where a.Status == true
                    && a.ArticleCategoryID == catID
                    && a.ArticleID != id

                    select new
                    {
                        ID = a.ArticleID,
                        a.Title,
                        a.Avatar,
                        a.CreateBy,
                        a.CreateTime,
                        a.ViewTime
                    };
        Repeater_ArticleRelated.DataSource = query.Take(10).ToList();
        Repeater_ArticleRelated.DataBind();


    }
}