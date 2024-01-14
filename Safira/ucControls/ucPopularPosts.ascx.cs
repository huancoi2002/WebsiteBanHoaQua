using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucPopularPosts : System.Web.UI.UserControl
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
        DBEntities db = new DBEntities();
        var query = from a in db.Articles
                    where a.Status == true
                    orderby a.ViewTime descending
                    select new
                    {
                        ID = a.ArticleID,
                        a.Title,
                        a.Avatar,
                        a.ViewTime,
                        a.CreateTime,
                    };
        Repeater_PopularPost.DataSource = query.Take(5).ToList();
        Repeater_PopularPost.DataBind();
    }
}