using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class ucInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Kiểm tra Session  UserName , nếu chưa có , chuyển đến trang chủ
        if (SessionUtility.AdminUsername == string.Empty)
        {
            //Lấy url của trang hiện tại
            string goBackUrl = Request.Url.ToString().ToUrlEncode();
            //Tạo Link login với trang quay về là trang hiện tại
            string loginUrl = "~/Admin/login.aspx?backurl=" + goBackUrl;
            //Chuyển đến trang login
            Response.Redirect(loginUrl);
        }
        else
        {
            img_Admin_Avatar.Src = SessionUtility.AdminAvatar;

            a_Admin_FullName.InnerHtml = SessionUtility.AdminFullName;
            a_Admin_FullName.HRef = "~/Admin/AccountEdit.aspx?id=" + SessionUtility.AdminUsername;

            b_Admin_Username.InnerHtml = SessionUtility.AdminFullName;
        }
    }


    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        //Chuyển hướng về trang chủ
        Response.Redirect("~/Admin/login.aspx");
    }

}