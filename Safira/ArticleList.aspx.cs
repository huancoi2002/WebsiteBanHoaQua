using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
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
            QuantityProduct = db.Articles.Count(z => z.ArticleCategory.ArticleMainCategoryID == x.ArticleMainCategoryID)
        }).ToList();

        Repeater_Category.DataSource = data;
        Repeater_Category.DataBind();

        Repeater_Tag.DataSource = data;
        Repeater_Tag.DataBind();
    }

    public void LoadData()
    {
        int mid = Request.QueryString["mid"].ToInt();
        int cid = Request.QueryString["cid"].ToInt();
        DBEntities db = new DBEntities();
        var query = from a in db.Articles
                    from ac in db.ArticleCategories
                    where a.Status == true
                    && a.ArticleCategoryID == ac.ArticleCategoryID
                    orderby a.Position
                    select new
                    {
                        ID = a.ArticleID,
                        a.Title,
                        a.Avatar,
                        a.CreateBy,
                        a.ViewTime,
                        a.CreateTime,
                        a.Description,
                        ac.ArticleMainCategoryID,
                        a.ArticleCategoryID
                    };
        if (mid > 0 )
        {
            query = query.Where(q => q.ArticleMainCategoryID == mid);
        }
        if (cid > 0)
        {
            query = query.Where(q => q.ArticleCategoryID == cid);
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
        string url = "~/ArticleList.aspx?mid={0}&cid={1}&page={2}".StringFormat(mid, cid, "{0}");
        ucPagging.TotalItems = totalItems;
        ucPagging.CurrentPage = page;
        ucPagging.PageSize = pageSize;
        ucPagging.MaxPage = maxPage;
        ucPagging.URL = url;
        ucPagging.DataBind();
        Repeater_ArticleList.DataSource = data;
        Repeater_ArticleList.DataBind();

    }
}