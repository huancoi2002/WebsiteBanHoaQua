using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Admin_ProductDetail : System.Web.UI.Page
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
        var item = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

        if (item !=null)
        {
            div_Content.InnerHtml = item.Content;
        }
    }
}