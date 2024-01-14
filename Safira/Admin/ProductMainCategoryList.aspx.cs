using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CodeUtility;

public partial class Admin_ProductMainCategoryList : System.Web.UI.Page
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

    public void LoadCategoryData()
    {
        DBEntities db = new DBEntities();

        //Nếu select mà tất cả thì dùng Select(x=>) ngược lại thì Giống dưới
        var query = db.ProductMainCategories.OrderBy(x => x.Position)
            .Select(x => new { x.ProductMainCategoryID, x.Title });
        var data = query.ToList();

        //Cấu hình hiển thị
        ListBox_Category.DataValueField = "ProductMainCategoryID";
        ListBox_Category.DataTextField = "Title";

        ListBox_Category.DataSource = data;
        ListBox_Category.DataBind();

        //Tạo thêm một Item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".:Chọn loại danh mục sản phẩm:.";

        //Chèn Item mặc định vào đầu danh sách Dropdown
        ListBox_Category.Items.Insert(0, item);

    }

    public void LoadData()
    {
        int id = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();
        var item = db.ProductMainCategories.Where(x => x.ProductMainCategoryID == id)
            .FirstOrDefault();
        if (item == null)
        {
            return;
        }

        ListBox_Category.SelectedValue = id.ToSafetyString();

        input_ID.Value = item.ProductMainCategoryID.ToSafetyString();
        input_Code.Value = item.Code;
        input_Title.Value = item.Title;
        textarea_Description.Value = item.Description;
        input_Position.Value = item.Position.ToSafetyString();

        a_Avatar.HRef = item.Avatar;
        img_Avatar.Src = item.Avatar;

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
    }

    public void SearchData(string type = "", string message = "")
    {
        //Lấy các giá trị tìm kiếm
        int ID = ListBox_Category.SelectedValue.ToInt();

        //Tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/ProductMainCategoryList.aspx?id={0}&messagetype={1}&message={2}";
        url = url.StringFormat(ID, type, message);
        //Chuyển về trang đích
        Response.Redirect(url);
    }


    protected void ListBox_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

    private void ShowPreviousMessage()
    {
        string messageType = Request.QueryString["messagetype"].ToSafetyString();
        string message = Request.QueryString["message"].ToSafetyString();
        if (messageType == "success")
        {
            ucMessage.ShowSuccess(message);
        }
        else if (messageType == "warning")
        {
            ucMessage.ShowWarning(message);
        }
        else if (messageType == "info")
        {
            ucMessage.ShowInfo(message);
        }
        else if (messageType == "error")
        {
            ucMessage.ShowError(message);
        }
    }

    protected void LinkButton_Add_Click(object sender, EventArgs e)
    {
        ListBox_Category.SelectedIndex = 0;
        SearchData();
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        int ID = Request.QueryString["id"].ToInt();
        if (ID <= 0)
        {
            ucMessage.ShowError("Vui lòng chọn 1 dữ liệu để xóa");
            return;
        }

        DBEntities db = new DBEntities();
        var item = db.ProductMainCategories.Where(x => x.ProductMainCategoryID == ID).FirstOrDefault();

        if (item == null)
        {
            SearchData("error", "Danh mục sản phẩm không còn tồn tại");
            return;
        }

        db.ProductMainCategories.Remove(item);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            ucMessage.ShowError("Chưa xóa được, vui lòng thử lại");
            return;
        }
        SearchData("success", "Đã xóa dữ liệu");
        return;
    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        //Lấy ID từ url xuống
        int id = Request.QueryString["id"].ToInt();

        //Lấy giá trị các ô nhập
        int position = input_Position.Value.ToInt();
        string code = input_Code.Value.Trim();
        string title = input_Title.Value.Trim();
        string description = textarea_Description.Value.Trim();
        bool status = radio_Active.Checked;

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
                ucMessage.ShowError("Đuôi sản phẩm không hợp lệ, Loại hình hổ trợ:.jpg .jpeg .png .gif .bmp .ico ");
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
            uploadUtility.FolderSave = "~/fileuploads/ProductMainCategory";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        //Kiểm tra title hợp lêk
        if (title == string.Empty)
        {
            ucMessage.ShowError("Vui lòng đăng nhập tiêu đề hình");
            return;
        }
        //Kiểm tra nếu như có id thì Update (Save)
        if (id > 0)
        {
            //Cập nhật
            //Tìm một item thích hợp
            DBEntities db = new DBEntities();
            var item = db.ProductMainCategories.Where(x => x.ProductMainCategoryID == id).FirstOrDefault();

            //Nếu không có thì báo lỗi , kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu không tồn tại");
                Response.Redirect("~/");
            }
            //cập nhật giá trị mới
            //Tạo một item mới có kiểu là bảng cần thêm
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;
            //Khi cập nhật không thay đổi viewtime

            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Update dữ liệu
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search

            string url = "~/Admin/ProductMainCategoryList.aspx?mid={0}&messagetype={1}&message={2}";
            url = url.StringFormat(id, "success", "Cập nhật sản phẩm thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
        //Ngược lại thì Insert (Add)
        else
        {
            //Kiểm tra ID trùng lắp
            DBEntities db = new DBEntities();
            //Tạo một item mới có kiểu là bảng cần thêm
            ProductMainCategory item = new ProductMainCategory();

            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;

            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }
            //Thêm mới
            db.ProductMainCategories.Add(item);
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search



            string url = "~/Admin/ProductMainCategoryList.aspx?messagetype={0}&message={1}&id={2}";
            url = url.StringFormat("success", "Đã thêm mới sản phẩm thành công", item.ProductMainCategoryID);
            //Chuyển về trang đích
            Response.Redirect(url);
        }
    }
}