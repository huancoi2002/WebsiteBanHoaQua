using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Admin_ArticleDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    public void LoadData()
    {
        int id = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();

        var query = db.Articles.Where(x => x.ArticleID==id).Select(x => new
        {
            ID=x.ArticleID,
            x.Content
        });

        Repeater_Detail.DataSource = query.ToList();
        Repeater_Detail.DataBind();
    }
}