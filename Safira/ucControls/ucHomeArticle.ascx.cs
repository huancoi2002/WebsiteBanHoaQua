using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucHomeArticle : System.Web.UI.UserControl
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
                    orderby a.CreateTime descending
                    select new
                    {
                        ID = a.ArticleID,
                        a.Title,
                        a.Avatar,
                        a.CreateBy,
                        a.ViewTime,
                        a.CreateTime,
                        a.Description,
                    };
        Repeater_HomeArticle.DataSource = query.Take(10).ToList();
        Repeater_HomeArticle.DataBind();
    }
}