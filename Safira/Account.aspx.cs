using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ucMessage_Register.HideAll();
        }
    }

    protected void LinkButton_Login_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        ucMessage_Register.HideAll();

        string email = input_emailLongin.Value.ToSafetyString();
        string password = input_passwordLogin.Value.ToSafetyString();

        password = password.Encrypt(Commons.AdminShalt);

        if (!email.IsEmailFormat())
        {
            ucMessage.ShowError("Email không đúng định dạng");
            return;
        }

        if (password.IsNullOrEmpty())
        {
            ucMessage.ShowError("Vui lòng nhập mật khẩu");
            return;
        }

        DBEntities db = new DBEntities();
        var item = db.Clients.Where(q => q.Email == email && q.Password == password).FirstOrDefault();
        if (item == null)
        {
            ucMessage.ShowError("Tài khoản không tồn tại! Vui lòng đăng kí mới");
            return;
        }
        if (item.Status == false)
        {
            ucMessage.ShowError("Vui lòng kích hoạt tài khoản trước khi đăng nhập");
            return;
        }

        SessionUtility.Client = item;

        Response.Redirect("/");
    }

    protected void LinkButton_Register_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        ucMessage_Register.HideAll();
        string fullName = input_fullName.Value.Trim();
        string email = input_Email.Value.Trim();
        string password = input_Password.Value.Trim();
        string rePassword = input_RePassword.Value.Trim();


        if (fullName.IsNullOrEmpty())
        {
            ucMessage_Register.ShowError("Vui lòng nhập họ tên");
            return;
        }
        if (!email.IsEmailFormat())
        {
            ucMessage_Register.ShowError("Email không đúng định dạng");
            return;
        }
        if (password.IsNullOrEmpty())
        {
            ucMessage_Register.ShowError("Vui lòng nhập mật khẩu");
            return;
        }
        if (rePassword.IsNullOrEmpty())
        {
            ucMessage_Register.ShowError("Vui lòng nhập lại mật khẩu");
            return;
        }
        if (password != rePassword)
        {
            ucMessage_Register.ShowError("Mật khẩu bạn nhập không khớp");
            return;
        }

        DBEntities db = new DBEntities();
        if (db.Clients.Where(q => q.Email == email).FirstOrDefault() != null)
        {
            ucMessage_Register.ShowError("Tài khoản đã tồn tại! Vui lòng đăng kí mới");
            return;
        }
        else
        {
            Client client = new Client();

            client.FullName = fullName;
            client.Email = email;
            client.Password = password.Encrypt(Commons.AdminShalt);
            client.Status = true;
            client.CreateTime = DateTime.Now;

            //Gữi email thông báo và yêu cầu kích hoạt
            
            //Lưu db
            db.Clients.Add(client);
            db.SaveChanges();

            ucMessage_Register.ShowSuccess("Đăng kí thành công! Vui lòng đăng nhập");
        }
    }
}