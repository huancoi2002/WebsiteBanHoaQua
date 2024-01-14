using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucUpperHeader : System.Web.UI.UserControl
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
        if (SessionUtility.Client != null)
        {
            span_FullName.InnerHtml = SessionUtility.Client.FullName;

            a_Login.Visible = false;
            a_Register.Visible = false;
            LinkButton_Logout.Visible = true;
            span_FullNames.InnerHtml = SessionUtility.Client.FullName;
        }
    }

    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Remove("Client");
        Response.Redirect("/");
    }
}