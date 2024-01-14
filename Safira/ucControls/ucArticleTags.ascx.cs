using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
public partial class ucArticleTags : System.Web.UI.UserControl
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
        var query = from t in db.Articles
                    where t.Status == true
                    && t.ArticleID == id
                    orderby t.Position
                    select new
                    {
                        ID = t.ArticleID,
                        t.Keyword
                    };
        var data = query.FirstOrDefault();
        if (data != null)
        {
            Repeater_ArticleTags.DataSource = data.Keyword.SplitToText(",");
            Repeater_ArticleTags.DataBind();
        }
    }
}