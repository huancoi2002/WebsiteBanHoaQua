using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CodeUtility;

public partial class Admin_PictureList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            LoadCategoryData();
            LoadData();

        }
    }

    public void LoadData()
    {

        int catID = Request.QueryString["catid"].ToInt();
        string title = Request.QueryString["title"].ToSafetyString();

        DBEntities db = new DBEntities();
        //
        var query = db.Pictures.OrderByDescending(x => x.CreateTime).Select(x => new
        {
            ID = x.PictureID,
            x.Title,
            x.Avatar,
            x.Description,
            x.Status,
            x.ViewTime,
            x.CreateBy,
            x.CreateTime,
            catID = x.PictureCategoryID,

            CatTitle = x.PictureCategory.Title,
            MainCatTitle = x.PictureCategory.PictureMainCategory.Title,
            //AccountUsername = x.PictureCategory.PictureMainCategory.Account.Username

        });
        //

        if (catID != 0)
        {
            query = query.Where(x => x.catID == catID);
            //Đổ lại Dropdown
            DropDownList_Category.SelectedValue = catID.ToString();
        }

        if (title != string.Empty)
        {
            query = query.Where(x => x.Title.Contains(title));
            //Đổ lại vào ô input
            input_Title.Value = title;
        }
        //Phân trang dữ liệu
        int totalItem = query.Count();
        int maxPage = 10;
        int pageSize = 10;
        int curuntPage = Request.QueryString["page"].ToInt();
        if (curuntPage <= 0)
        {
            curuntPage = 1;
        }
        string url = "~/Admin/PictureList.aspx?catid={0}&title={1}&page={2}";
        url = url.StringFormat(catID, title, "{0}");

        //Đỗ dữ liệu vào ucPaging
        ucPagging.CurrentPage = curuntPage;
        ucPagging.PageSize = pageSize;
        ucPagging.MaxPage = maxPage;
        ucPagging.URL = url;
        ucPagging.TotalItems = totalItem;
        ucPagging.DataBind();

        int skip = (curuntPage - 1) * pageSize;
        var data = query.Skip(skip).Take(pageSize).ToList();

        Repeater_Picture.DataSource = data;
        Repeater_Picture.DataBind();

        //Hiển thị thông báo từ trước
        ShowPreviousMessage();
    }

    public void LoadCategoryData()
    {
        DBEntities db = new DBEntities();

        //Nếu select mà tất cả thì dùng Select(x=>) ngược lại thì Giống dưới
        var query = db.PictureCategories.Select(x => new { x.PictureCategoryID, x.Title });
        var data = query.ToList();

        //Cấu hình hiển thị
        DropDownList_Category.DataValueField = "PictureCategoryID";
        DropDownList_Category.DataTextField = "Title";

        DropDownList_Category.DataSource = data;
        DropDownList_Category.DataBind();

        //Tạo thêm một Item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".:Chọn loại hình ảnh:.";

        //Chèn Item mặc định vào đầu danh sách Dropdown
        DropDownList_Category.Items.Insert(0, item);

    }

    public void SearchData(string type = "", string message = "", bool isKeepPage = false)
    {
        //Lấy các giá trị tìm kiếm
        string catID = DropDownList_Category.SelectedValue;
        string title = input_Title.Value.Trim();

        int page = 1;
        if (isKeepPage == true)
        {
            page = Request.QueryString["page"].ToInt();
            if (page <= 0)
            {
                page = 1;
            }
        }
        //Tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/PictureList.aspx?catid={0}&title={1}&messagetype={2}&message={3}&page={4}";
        url = url.StringFormat(catID, title, type, message, page);
        //Chuyển về trang đích
        Response.Redirect(url);
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

    protected void DropDownList_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_Search_Click(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_ClearSearch_Click(object sender, EventArgs e)
    {
        DropDownList_Category.SelectedIndex = 0;
        input_Title.Value = string.Empty;

        SearchData();
    }

    protected void LinkButton_SaveAvatar_Click(object sender, EventArgs e)
    {

        //Ép kiểu sender về dạng LinkButton
        LinkButton linkButton = sender as LinkButton;
        //Lấy ID của tài khoản cần đổi hình
        int ID = linkButton.CommandArgument.ToInt();

        //tìm một hình ảnh có ID như trên
        DBEntities db = new DBEntities();
        var item = db.Pictures.Where(x => x.PictureID == ID).FirstOrDefault();
        //Nêu không tìm thấy, thì load lại dữ liệu
        if (item == null)
        {
            SearchData("error", "Hình ảnh cần đổi hình không tồn tại");
            return;
        }

        //Up hình ảnh lên server
        FileUpload FileUpload_Avatar = linkButton.NamingContainer.FindControl("FileUpload_Avatar") as FileUpload;

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
        //Cách 1: FileUpload_Avatar.SaveAs("URL");
        //Cách 2
        Exception error = null;
        string avatar = string.Empty;
        string thumb = string.Empty;
        UploadUtility uploadUtility = new UploadUtility();
        uploadUtility.FileUpload = FileUpload_Avatar;
        uploadUtility.FolderSave = "~/fileuploads/Picture/";
        uploadUtility.FullMaxWidth = 1000;
        uploadUtility.ThumbMaxWidth = 400;
        uploadUtility.MaxFileSize = 1024 * 1024 * 3;
        uploadUtility.AutoGenerateFileName = true;
        uploadUtility.UploadImage(ref avatar, ref thumb, ref error);

        //Kiểm tra nếu có lỗi khi upload thì kết thúc
        if (error != null || avatar == string.Empty)
        {
            SearchData("error", "Chưa upload được hình do lỗi hệ thống");
            return;
        }
        //Cập nhật link mới vào tài khoản kể trên
        item.Avatar = avatar;
        item.Thumb = thumb;
        //lưu lại
        db.SaveChanges();
        //Load lại dữ liệu để thấy hình được thay mới
        SearchData("success", "Đã upload hình thành công", true);
        return;
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        int ID = linkButton.CommandArgument.ToInt();

        DBEntities db = new DBEntities();
        var item = db.Pictures.Where(x => x.PictureID == ID).FirstOrDefault();

        if (item == null)
        {
            SearchData("error", "Hình ảnh không còn tồn tại");
            return;
        }

        db.Pictures.Remove(item);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            ucMessage.ShowError("Chưa xóa được, vui lòng thử lại");
        }
        SearchData("success", "Đã xóa dữ liệu", true);
        return;
    }

    protected void LinkButton_Active_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        int ID = linkButton.CommandArgument.ToInt();

        DBEntities db = new DBEntities();
        var item = db.Pictures.Where(x => x.PictureID == ID).FirstOrDefault();

        item.Status = !item.Status;
        //Lưu db
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ucMessage.ShowError("Chưa lưu được, vui lòng thử lại");
        }
        SearchData("success", "Đã cập nhật trạng thái thành công", true);
        return;

    }
}