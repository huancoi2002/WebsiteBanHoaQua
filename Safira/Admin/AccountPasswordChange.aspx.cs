using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Admin_Pages_AccountPasswordChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ucMessage.ShowInfo("Vui lòng nhập mật khẩu mới");
            string ID = Request.QueryString["id"].ToSafetyString();
            if (ID==string.Empty)
            {
                ID = SessionUtility.AdminUsername;
            }

            input_Username.Value = ID;
        }
    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        //Lấy mật khẩu đã nhập
        string password = input_Password.Value.Trim();
        string rePassword = input_RePassword.Value.Trim();

        //Lấy ID cần đổi mật khẩu
        string ID = Request.QueryString["id"].ToSafetyString();

        if (ID == string.Empty)
        {
            ID = SessionUtility.AdminUsername;
        }

        //Lấy link quay về
        string goBackUrl = Request.QueryString["backurl"].ToSafetyString();
        if (goBackUrl == string.Empty)
        {
            goBackUrl = "~/Admin/Default.aspx";
        }
        //Lấy ID không hợp lệ thì về trang trước đó
        if (ID==string.Empty)
        {
            Response.Redirect(goBackUrl);
        }
        //Kiểm tra mật khẩu cũ có đúng không

        //Nếu mật khẩu không hợp lệ thì báo lỗi
        if (password == string.Empty || password != rePassword)
        {
            ucMessage.ShowError("Mật khẩu bạn nhập không khớp");
            return;
        }

        DBEntities db = new DBEntities();

        //Tìm một Acount có user gửi qua
        Account item = db.Accounts.Where(q => q.Username == ID).FirstOrDefault();

        //Nếu tài khoản không tồn tại thì thoát về trang login
        if (item == null)
        {
            Session.Abandon();
            Response.Redirect("~/Login.asspx");
        }

        //Mã hóa password mới với shalt
        //password = password.MD5Hash(Commons.AdminShalt);
        item.Password = password.Encrypt(Commons.AdminShalt); 
        db.SaveChanges();
        //Nếu có trang trước đó thì quay về
        if (goBackUrl != "~/Admin/Default.aspx") ;
        {
            Response.Redirect(goBackUrl);
        }
        //Thông báo thành công
        ucMessage.ShowSuccess("Đã đổi mật khẩu thành công");


    }
}