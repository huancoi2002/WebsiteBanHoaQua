using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class ActiveAcount : System.Web.UI.Page
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
        string id = Request.QueryString["id"].ToSafetyString();

        string passwordEncript = "khongaibiet";

        string email = id.Decrypt(passwordEncript);

        if (email.IsEmpty() || !email.IsEmailFormat())
        {
            div_Alert.InnerHtml = "Lỗi rồi!";
            a_login.Visible = false;
            div_Alert.Attributes.Add("class", "title-error");
            div_Content.InnerHtml = "Email không hợp lệ";
            return;
        }

        DBEntities db = new DBEntities();
        var item = db.Clients.Where(q => q.Email == email).FirstOrDefault();

        if (item == null)
        {
            div_Alert.InnerHtml = "Lỗi rồi!";
            a_login.Visible = false;
            div_Alert.Attributes.Add("class", "title-error");
            div_Content.InnerHtml = "Tài khoản không tồn tại! Vui lòng đăng kí mới";
            return;
        }
        if (item.Status == true)
        {
            div_Alert.InnerHtml = "Cảnh báo!";
            div_Alert.Attributes.Add("class", "title-warning");
            div_Content.InnerHtml = "Tài khoản đã được kích hoạt trước đó! Vui lòng đăng nhập";
            return;
        }

        item.Status = true;
        db.SaveChanges();

        div_Alert.InnerHtml = "Xin chúc mừng";
        div_Alert.Attributes.Add("class", "title-success");
        div_Content.InnerHtml = "Tài khoản đã được kích hoạt thành công! Vui lòng đăng nhập";
        return;
    }
}