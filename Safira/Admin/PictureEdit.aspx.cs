using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_PictureEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ucMessage.ShowInfo("Xin mời nhập thông tin sau đó nhấn lưu");
            LoadCategory();
            int id = Request.QueryString["id"].ToInt();
            if (id > 0)
            {
                LoadData();
            }
        }
    }

    public void LoadData()
    {
        int id = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();
        var item = db.Pictures.Where(x => x.PictureID == id).FirstOrDefault();
        if (item == null)
        {
            ucMessage.ShowError("Tên tài khoản không hợp lệ");
            return;
        }
        DropDownList_Category.SelectedValue = item.PictureCategoryID.ToSafetyString();
        input_ID.Value = item.PictureID.ToSafetyString();
        input_Code.Value = item.Code;
        input_Title.Value = item.Title;
        textarea_Decription.Value = item.Description;
        input_Position.Value = item.Position.ToSafetyString();
        input_Viewtime.Value = item.ViewTime.ToSafetyString();
        input_CreateTime.Value = item.CreateTime.ToString("dd/MM/yyyy");
        input_CreateBy.Value = item.CreateBy;

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

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        //Lấy ID từ url xuống
        int id = Request.QueryString["id"].ToInt();

        //Lấy giá trị các ô nhập
        int catID = DropDownList_Category.SelectedValue.ToInt();
        int position = input_Position.Value.ToInt();
        string code = input_Code.Value.Trim();
        string title = input_Title.Value.Trim();
        string description = textarea_Decription.Value.Trim();
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
            uploadUtility.FolderSave = "~/fileuploads/Picture";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        //Kiểm tra title hợp lêk
        if (title==string.Empty)
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
            var item = db.Pictures.Where(x => x.PictureID == id).FirstOrDefault();

            //Nếu không có thì báo lỗi , kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu không tồn tại");
                Response.Redirect("~/");
            }
            //cập nhật giá trị mới
            //Tạo một item mới có kiểu là bảng cần thêm
            item.PictureCategoryID = catID;
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
            string url = "~/Admin/PictureList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Cập nhật hình ảnh thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
        //Ngược lại thì Insert (Add)
        else
        {
            //Kiểm tra ID trùng lắp
            DBEntities db = new DBEntities();
            //Tạo một item mới có kiểu là bảng cần thêm
            Picture item = new Picture();

            item.PictureCategoryID = catID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;
            item.ViewTime = RandomUtility.RandomNumber(1, 1000);

            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }
            //Thêm mới
            db.Pictures.Add(item);
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/PictureList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Đã thêm mới hình ảnh thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
    }

    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var query = db.PictureCategories.Select(x => new
        {
            ID = x.PictureCategoryID,
            x.Title
        });

        DropDownList_Category.DataValueField = "ID";
        DropDownList_Category.DataTextField = "Title";
        DropDownList_Category.DataTextFormatString = "-{0}-";

        DropDownList_Category.DataSource = query.ToList();
        DropDownList_Category.DataBind();
    }
}