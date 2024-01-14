using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucArticleCategories : System.Web.UI.UserControl
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
        var query = from m in db.ArticleMainCategories
                    where m.Status == true
                    orderby m.Position
                    select new
                    {
                        ID = m.ArticleMainCategoryID,
                        m.Title
                    };
        Repeater_ArticleCategories.DataSource = query.ToList();
        Repeater_ArticleCategories.DataBind();

    }
}