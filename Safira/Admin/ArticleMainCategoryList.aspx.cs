using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_ArticleMainCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ShowPreviousMessage();
            LoadCategoryData();
            LoadData();
        }
    }

    public void LoadData()
    {
        //Lấy id từ url
        int ID = Request.QueryString["id"].ToInt();

        if (ID >= 0)
        {
            ListBox_Category.SelectedValue = ID.ToSafetyString();
        }

        DBEntities db = new DBEntities();
        var item = db.ArticleMainCategories.Where(x => x.ArticleMainCategoryID == ID).Select(x => x).FirstOrDefault();

        if (item == null)
        {
            return;
        }

        input_ID.Value = item.ArticleMainCategoryID.ToSafetyString();
        input_Position.Value = item.Position.ToSafetyString();
        input_Code.Value = item.Code;
        input_Title.Value = item.Title;
        textarea_Description.Value = item.Description;
        a_Avatar.HRef = item.Avatar;
        img_Avatar.Src = item.Avatar;

        if (item.Status == true)
        {
            radio_Lock.Checked = false;
            radio_Active.Checked = true;
        }
        else
        {
            radio_Lock.Checked = true;
            radio_Active.Checked = false;
        }
    }

    public void SearchData(string messageType = "", string message = "")
    {
        //Lấy các tiêu chí search
        int ID = ListBox_Category.SelectedValue.ToInt();

        //Tạo url mới từ trang hiện tại + tiêu chí search
        string url = "~/Admin/ArticleMainCategoryList.aspx?id={0}&messagetype={1}&message={2}";
        url = url.StringFormat(ID, messageType, message);

        //Chuyển đến url mới
        Response.Redirect(url);
    }

    public void LoadCategoryData()
    {
        DBEntities db = new DBEntities();

        var query = db.ArticleMainCategories.Select(x => new
        {
            x.ArticleMainCategoryID,
            x.Title
        });

        var data = query.ToList();

        //Cấu hình hiển thị
        ListBox_Category.DataValueField = "ArticleMainCategoryID";
        ListBox_Category.DataTextField = "Title";

        ListBox_Category.DataSource = data;
        ListBox_Category.DataBind();


        //Tạo 1 item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".: Chọn 1 danh mục :.";

        //Thêm vào đầu danh sách
        ListBox_Category.Items.Insert(0, item);
    }

    protected void ListBox_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_Add_Click(object sender, EventArgs e)
    {
        //Chọn phần tử rỗng trong list box để gỡ id hiện tại
        ListBox_Category.SelectedIndex = 0;

        //Load lại
        SearchData("info", "Xin mời nhập dữ liệu để tạo danh mục mới");
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        //Bóc tách ID từ nó ra
        int ID = Request.QueryString["id"].ToInt();

        //Kết nối db
        DBEntities db = new DBEntities();

        //vào db tìm 1 item có id phù hợp
        var item = db.ArticleMainCategories.Where(x => x.ArticleMainCategoryID == ID).FirstOrDefault();

        //Nếu không có, thì load lại trang
        if (item == null)
        {
            SearchData("error", "Dữ liệu không tồn tại");
            return;
        }

        //Xóa khỏi bảng
        db.ArticleMainCategories.Remove(item);

        //Lưu DB
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            ucMessage.ShowError("Không thể xóa tài khoản");
            return;
        }

        //Bỏ chọn phần tử trong Listbox
        ListBox_Category.SelectedIndex = 0;

        //Load lại trang
        SearchData("success", "Đã xóa dữ liệu");
    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();

        //Lấy id từ Url
        int ID = Request.QueryString["id"].ToInt();

        //Lấy các giá trị đã nhập trên form
        int position = input_Position.Value.ToInt();
        string code = input_Code.Value.Trim();
        string title = input_Title.Value.Trim();
        string description = textarea_Description.Value.Trim();
        bool status = radio_Active.Checked;

        //Kiểm tra đuôi hình hợp lệ
        string validExtension = ".jpg.jpeg.png.gif.bmp.ico";
        string fileExtension = Path.GetExtension(FileUpload_Avatar.FileName).ToLower();

        if (!validExtension.Contains(fileExtension))
        {
            ucMessage.ShowError("Loại hình không hỗ trợ. Hãy chọn hình có đuôi: .jpg, .png, .gif, .bmp, .ico");
            return;
        }

        //Kiểm tra dung lượng <= 4mb
        int validSize = 1024 * 1024 * 4;
        int fileSize = FileUpload_Avatar.FileBytes.Length;
        if (fileSize > validSize)
        {
            ucMessage.ShowError("Dung lượng hình cần phải nhỏ hơn 4mb");
            return;
        }

        //Upload hình lên sever
        string avatar = string.Empty;
        string thumb = string.Empty;
        UploadUtility upload = new UploadUtility();
        upload.FileUpload = FileUpload_Avatar;
        upload.FolderSave = "~/fileuploads/ArticleMainCategory/";
        upload.FullMaxWidth = 1000;
        upload.ThumbMaxWidth = 400;
        upload.MaxFileSize = 1024 * 1024 * 3; //3Mb
        upload.AutoGenerateFileName = true;

        Exception ex = null; //Biến chứa lỗi nếu có
        try
        {
            upload.UploadImage(ref avatar, ref thumb, ref ex);
        }
        catch (Exception error)
        {
            ex = error;
        }


        //--------------------//Kết thúc kiểm tra\\-----------------

        if (title == string.Empty)
        {
            ucMessage.ShowError("Dữ liệu này không còn tồn tại");
            return;
        }

        //Nếu có id, thì tiến hành cập nhật
        if (ID > 0)
        {
            //tìm 1 item có id thích hợp
            DBEntities db = new DBEntities();
            var item = db.ArticleMainCategories.Where(x => x.ArticleMainCategoryID == ID).FirstOrDefault();

            //Nếu ko có, thì báo lỗi, kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu này không còn tồn tại");
                return;
            }

            //cập nhật giá trị mới
            if (position > 0)
                item.Position = position;

            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername; ;
            item.CreateTime = DateTime.Now;

            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Lưu db
            db.SaveChanges();

            //Tạo url mới từ trang hiện tại + tiêu chí search
            string url = "~/Admin/ArticleMainCategoryList.aspx?messagetype={0}&message={1}&id={2}";
            url = url.StringFormat("success", "Đã lưu dữ liệu", item.ArticleMainCategoryID);

            //Chuyển đến url mới
            Response.Redirect(url);

        }

        //Ngược lại, thì tiến hành thêm mới
        else
        {
            DBEntities db = new DBEntities();

            //Tạo 1 item mới
            ArticleMainCategory item = new ArticleMainCategory();

            //Nhập từng giá trị vào từng cột
            if (position > 0)
                item.Position = position;

            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername; ;
            item.CreateTime = DateTime.Now;

            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Thêm vào bảng và lưu vào db
            db.ArticleMainCategories.Add(item);

            //LưuDB
            db.SaveChanges();

            //Tạo url mới từ trang hiện tại + tiêu chí search
            string url = "~/Admin/ArticleMainCategoryList.aspx?messagetype={0}&message={1}&id={2}";
            url = url.StringFormat("success", "Đã thêm mới dữ liệu", item.ArticleMainCategoryID);

            //Chuyển đến url mới
            Response.Redirect(url);

        }
    }

    private void ShowPreviousMessage()
    {
        string messageType = Request.QueryString["messagetype"].ToSafetyString();
        string message = Request.QueryString["message"].ToSafetyString();

        if (messageType == "success")
        {
            ucMessage.ShowSuccess(message);
        }
        else if (messageType == "error")
        {
            ucMessage.ShowError(message);
        }
        else if (messageType == "warning")
        {
            ucMessage.ShowWarning(message);
        }
        else if (messageType == "info")
        {
            ucMessage.ShowInfo(message);
        }
    }
}