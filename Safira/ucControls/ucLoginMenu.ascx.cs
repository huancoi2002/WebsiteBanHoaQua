using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using CodeUtility;


public partial class ucLoginMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        ucMessage.HideAll();
    }

    public void LoadData()
    {
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {

        string email = input_Email.Value.Trim();
        string passwordEncript = "khongaibiet";
        string password = input_Password.Value.Trim();

        password = password.Encrypt(passwordEncript);

        if (email.IsNullOrEmpty() || !email.IsEmailFormat())
        {
            ucMessage.ShowError("Email không đúng định dạng");
            return;
        }

        if (password.IsNullOrEmpty())
        {
            ucMessage.ShowError("Mật khẩu không được đê trống");
            return;
        }

        DBEntities db = new DBEntities();
        var item = db.Clients.Where(q => q.Email == email && q.Password == password).FirstOrDefault();
        if (item == null)
        {
            ucMessage.ShowError("Tài khoản hoặc mật khẩu không tồn tại!");
            return;
        }
        if (item.Status == false)
        {
            ucMessage.ShowError("Vui lòng kích hoạt tài khoản trước khi đăng nhập");
            return;
        }
        Thread.Sleep(1000);

        SessionUtility.Client = item;
            Response.Redirect("/");
    }
}