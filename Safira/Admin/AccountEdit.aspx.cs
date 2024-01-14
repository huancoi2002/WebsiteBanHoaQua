using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_Pages_AccountEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ucMessage.ShowInfo("Xin mời nhập thông tin sau đó nhấn lưu");
            LoadCategory();
            string id = Request.QueryString["id"].ToSafetyString();
            if (id != string.Empty)
            {
                LoadData();
                //Khóa ô khóa chính lại
                input_Username.Attributes.Add("readonly", "readonly");
                //Thêm thuộc tính giải thích
                input_Username.Attributes.Add("title", "Thuộc tính khóa, không được xóa ");

                //Khóa radio trạng thái khi id là của tài khoản hiện tại
                if (id == SessionUtility.AdminUsername)
                {
                    radio_Active.Attributes.Add("disabled", "disabled");
                    radio_Lock.Attributes.Add("disabled", "disabled");

                    radio_Active.Attributes.Add("title", "Thuộc tính khóa, không được thay đổi ");
                    radio_Lock.Attributes.Add("title", "Thuộc tính khóa, không được thay đổi ");
                }
            }
        }
    }

    public void LoadData()
    {
        string id = Request.QueryString["id"].ToSafetyString();

        DBEntities db = new DBEntities();
        var item = db.Accounts.Where(x => x.Username == id).FirstOrDefault();
        if (item == null)
        {
            ucMessage.ShowError("Tên tài khoản không hợp lệ");
            return;
        }
        DropDownList_Category.SelectedValue = item.AccountCategoryID;
        input_Username.Value = item.Username;
        if (item.Status == true)
        {
            radio_Lock.Checked = false;
            radio_Active.Checked = true;
        }
        else
        {
            radio_Active.Checked = false;
            radio_Lock.Checked = true;
        }
        input_FullName.Value = item.FullName;
        input_Email.Value = item.Email;
        input_Mobi.Value = item.Mobi;
        textarea_Address.Value = item.Address;
        a_Avatar.HRef = item.Avatar;
        img_Avatar.Src = item.Avatar;
        if (item.Gender == true)
        {
            radio_Female.Checked = false;
            radio_Male.Checked = true;
        }
        else
        {
            radio_Male.Checked = false;
            radio_Female.Checked = true;
        }
        input_CreateTime.Value = item.CreateTime.ToString("dd/MM/yyyy");
    }

    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var query = db.AccountCategories.Select(x => new
        {
            ID = x.AccountCategoryID,
            x.Title
        });

        DropDownList_Category.DataValueField = "ID";
        DropDownList_Category.DataTextField = "Title";
        DropDownList_Category.DataTextFormatString = "-{0}-";

        DropDownList_Category.DataSource = query.ToList();
        DropDownList_Category.DataBind();
    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        //Lấy ID từ url xuống
        string id = Request.QueryString["id"].ToSafetyString();
        //Lấy giá trị các ô nhập
        string catID = DropDownList_Category.SelectedValue;

        string password = input_Password.Value;
        string repassword = input_Repassword.Value;

        // bool status = radio_Active.Checked; (Cách 1)
        //Cách 2
        bool status = radio_Active.Checked;
        string fullName = input_FullName.Value.Trim();
        string email = input_Email.Value.Trim();
        string mobi = input_Mobi.Value.Trim();
        string address = textarea_Address.Value.Trim();

        //Upload hình
        string avatar = string.Empty;
        string thumb = string.Empty;

        if (FileUpload_Avatar.FileName != string.Empty)
        {
            //Kiểm tra đuôi hình hợp lệ
            string validExtension = ".jpg.jpeg.png.gif.bmp.ico";
            string fileExtension = Path.GetExtension(FileUpload_Avatar.FileName.ToLower());
            if (!validExtension.Contains(fileExtension))
            {
                ucMessage.ShowError("Đuôi hình ảnh không hợp lệ, Loại hình hổ trợ:.jpg .jpeg .png .gif .bmp .ico ");
                return;
            }

            //Kiểm tra dung lượng file < 3MB
            int validSize = 1024 * 1024 * 3;
            int fileSize = FileUpload_Avatar.FileBytes.Length;
            if (fileSize > validSize)
            {
                ucMessage.ShowError("Dung lượng hình cần <=3Mb");
                return;
            }

            Exception error = null;
            UploadUtility uploadUtility = new UploadUtility();
            uploadUtility.FileUpload = FileUpload_Avatar;
            uploadUtility.FolderSave = "~/fileuploads/Account";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        bool gender = radio_Male.Checked;

        //kiểm tra tính hợp lệ

        if (password != repassword)
        {
            ucMessage.ShowError("Vui lòng nhập mật khẩu 2 lần giống nhau");
            return;
        }
        if (fullName == string.Empty)
        {
            ucMessage.ShowError("Vui lòng nhập họ tên");
            return;
        }
        if (email == string.Empty || !email.IsEmailFormat())
        {
            ucMessage.ShowError("Vui lòng nhập email đúng định dạng");
            return;
        }
        //Kiểm tra nếu như có id thì Update (Save)
        if (id != string.Empty)
        {

            //Cập nhật
            //Tìm một item thích hợp
            DBEntities db = new DBEntities();
            var item = db.Accounts.Where(x => x.Username == id).FirstOrDefault();

            //Nếu không có thì báo lỗi , kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu không tồn tại");
                Response.Redirect("~/");
            }
            //cập nhật giá trị mới
            //Tạo một item mới có kiểu là bảng cần thêm
            item.Username = id;
            item.Status = status;
            item.FullName = fullName;
            item.Email = email;
            item.Mobi = mobi;
            item.Address = address;
            item.Gender = gender;
            item.CreateTime = DateTime.Now;
            item.AccountCategoryID = catID;

            //Nếu có nhập mật khẩu thì mới cập nhật mật khẩu
            if (password != string.Empty)
                item.Password = password.Encrypt(Commons.AdminShalt);

            //Nếu có up hình thì mới cập nhật
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Update dữ liệu
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/AccountList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Cập nhật tài khoản thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
        //Ngược lại thì Insert (Add)
        else
        {
            //Kiểm tra mật khẩu
            if (password == string.Empty)
            {
                ucMessage.ShowError("Vui lòng nhập mật khẩu");
                return;
            }
            id = input_Username.Value.Trim();
            //Thêm mới\\
            //Kiểm tra ID trùng lắp
            DBEntities db = new DBEntities();
            var validateItem = db.Accounts.Where(x => x.Username == id).FirstOrDefault();
            if (validateItem != null)
            {
                ucMessage.ShowError("Username bạn nhập đã có sẳn. Hãy thử lại với username khác");
                return;
            }

            //Tạo một item mới có kiểu là bảng cần thêm
            Account item = new Account();
            item.Username = id;
            item.Password = password.Encrypt(Commons.AdminShalt);
            item.Status = status;
            item.FullName = fullName;
            item.Email = email;
            item.Mobi = mobi;
            item.Address = address;
            item.Avatar = avatar;
            item.Thumb = thumb;
            item.Gender = gender;
            item.CreateTime = DateTime.Now;
            item.AccountCategoryID = catID;
            //Thêm mới
            db.Accounts.Add(item);
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/AccountList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Đã thêm mới tài khoản thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
    }
}