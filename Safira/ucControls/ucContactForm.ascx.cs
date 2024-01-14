using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO;
using CodeUtility;

public partial class ucContactForm : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

            LoadCategories();
            ucMessage.HideAll();
        }
    }

    public void LoadData()
    {

    }

    protected void LinkButton_SendMessage_Click(object sender, EventArgs e)
    {
        string fullName = input_Name.Value.Trim();
        string email = input_Email.Value.Trim();
        string mobi = input_Phone.Value.Trim();
        int category = DropDownList_Categories.SelectedValue.ToInt();
        string content = textarea_Message.Value.Trim();

        if (fullName.IsNullOrEmpty())
        {
            ucMessage.ShowError("Vui lòng nhập đầy đủ họ tên");
            return;
        }

        if (!email.IsEmailFormat())
        {
            ucMessage.ShowError("Vui lòng nhập đúng định dạng email");
            return;
        }

        if (mobi.IsNullOrEmpty())
        {
            ucMessage.ShowError("Vui lòng nhập số điện thoại");
            return;
        }
        if (content.IsNullOrEmpty())
        {
            ucMessage.ShowError("Vui lòng nhập nội dung");
            return;
        }

        //Gán biến
        Contact item = new Contact();
        item.FullName = fullName;
        item.Mobi = mobi;
        item.Email = email;
        item.ContactCategoryID = category;
        item.Content = content;

        item.CreateTime = DateTime.Now;
        item.Status = false;

        //Lưu Db
        DBEntities db = new DBEntities();
        db.Contacts.Add(item);
        db.SaveChanges();


        //Thông báo thành công
        ucMessage.ShowSuccess("Gửi thư liên hệ thành công! Chúng tôi sẽ sớm liên hệ lại với bạn");


    }

    public void LoadCategories()
    {
        DBEntities db = new DBEntities();
        var query = from c in db.ContactCategories
                    where c.Status == true
                    orderby c.Position
                    select new
                    {
                        ID = c.ContactCategoryID,
                        c.Title
                    };
        DropDownList_Categories.DataValueField = "ID";
        DropDownList_Categories.DataTextField = "Title";

        DropDownList_Categories.DataSource = query.ToList();
        DropDownList_Categories.DataBind();
    }

    protected void LinkButton_Clear_Click(object sender, EventArgs e)
    {
        input_Email.Value = string.Empty;
        input_Name.Value = string.Empty;
        input_Phone.Value = string.Empty;
        textarea_Message.Value = string.Empty;

        ucMessage.ShowSuccess("Xóa trắng Form thành công");
    }
}