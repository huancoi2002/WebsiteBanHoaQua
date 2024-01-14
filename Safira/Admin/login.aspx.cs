using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Admin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SessionUtility.AdminUsername != string.Empty)
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            ucMessage.HideAll();

            ucMessage.ShowInfo("Vui lòng đăng nhâp");
        }
    }

    protected void Button_Login_Click(object sender, EventArgs e)
    {
        //Lấy thông tin từ form
        string userName = input_Username.Value.Trim();
        string passWord = input_Password.Value.Trim();

        //Mã hóa pasword với salt
        passWord = passWord.Encrypt(Commons.AdminShalt);

        //Kết nối DB
        DBEntities db = new DBEntities();
        //Query lấy dữ liệu theo điều kiện
        var query = db.Accounts.Where(q => q.Username == userName && q.Password == passWord && q.Status == true).FirstOrDefault();

        //Kiểm tra nêu hợp lệ, thì lưu session và chuyển đén trang Admin Defautl
        if (query != null)
        {
            SessionUtility.AdminAvatar = query.Avatar;
            SessionUtility.AdminFullName = query.FullName;
            SessionUtility.AdminUsername = query.Username;
            SessionUtility.AdminCategoryID = query.AccountCategoryID;

            //Lấy Url trang trước đó
            //string goBackUrl = Request.QueryString["backurl"].ToSafetyString().ToUrlEncode();
            //if (goBackUrl==string.Empty)
            //{
            //    //Nếu không có thì về trang chủ
            //    Response.Redirect("~/Admin/Default.aspx");
            //}
            //Nếu không có thì về trang chủ

            //Response.Redirect(goBackUrl);
            Response.Redirect("~/Admin/Default.aspx");

            return;
        }
        ucMessage.ShowError("Tài khoản không hợp lệ");
    }
}