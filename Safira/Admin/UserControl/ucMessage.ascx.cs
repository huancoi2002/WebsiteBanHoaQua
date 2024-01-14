using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    public void ShowError(string messsage)
    {
        HideAll();
        ErrorBox.InnerHtml= messsage;
        ErrorBox.Visible = true;
      
    }

    public void ShowSuccess(string messsage)
    {
        HideAll();
        SuccessBox.InnerHtml = messsage;
        SuccessBox.Visible = true;
    }

    public void ShowWarning(string messsage)
    {
        HideAll();
        WarningBox.InnerHtml = messsage;
        WarningBox.Visible = true;
    }

    public void ShowInfo(string messsage)
    {
        HideAll();
        InfoBox.InnerHtml = messsage;
        InfoBox.Visible = true;
    }

    public void HideAll()
    {
        ErrorBox.Visible = false;
        SuccessBox.Visible = false;
        InfoBox.Visible = false;
        WarningBox.Visible = false;
    }
}