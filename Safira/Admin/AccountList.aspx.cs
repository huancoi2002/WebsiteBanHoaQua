using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_Pages_AccountList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            //Load dữ liệu trong dropdow
            LoadCategoryData();
            LoadData();


        }
    }

    public void LoadData()
    {

        string catID = Request.QueryString["catid"].ToSafetyString();
        string title = Request.QueryString["title"].ToSafetyString();

        DBEntities db = new DBEntities();
        var query = from a in db.Accounts
                    from m in db.AccountCategories
                    where a.AccountCategoryID == m.AccountCategoryID
                    orderby a.CreateTime
                    select new
                    {
                        ID = a.Username,
                        a.Avatar,
                        a.Address,
                        a.Status,
                        a.Email,
                        a.CreateTime,
                        a.Mobi,
                        a.FullName,
                        a.Gender,

                        MainTitle = m.Title,
                        CatID = m.AccountCategoryID

                    };

        if (catID != string.Empty)
        {
            query = query.Where(x => x.CatID == catID);
            //Đổ lại Dropdown
            DropDownList_Category.SelectedValue = catID;
        }

        if (title != string.Empty)
        {
            query = query.Where(x => x.FullName.Contains(title) || x.Mobi.Contains(title) || x.Email.Contains(title) || x.ID.Contains(title));
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
        string url = "~/Admin/AccountList.aspx?catid={0}&title={1}&page={2}";
        url = url.StringFormat(catID, title, "{0}");

        //Đỗ dữ liệu vào ucPaging
        ucPagging.CurrentPage = curuntPage;
        ucPagging.PageSize = pageSize;
        ucPagging.MaxPage = maxPage;
        ucPagging.URL = url;
        ucPagging.TotalItems = totalItem;
        ucPagging.DataBind();

        var data = query.Skip(curuntPage).Take(pageSize).ToList();

        Repeater_Account.DataSource = data;
        Repeater_Account.DataBind();

        //Hiển thị thông báo từ trước
        ShowPreviousMessage();
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

    public void LoadCategoryData()
    {
        DBEntities db = new DBEntities();

        //Nếu select mà tất cả thì dùng Select(x=>) ngược lại thì Giống dưới
        var query = db.AccountCategories.Select(x => new { x.AccountCategoryID, x.Title });
        var data = query.ToList();

        //Cấu hình hiển thị
        DropDownList_Category.DataValueField = "AccountCategoryID";
        DropDownList_Category.DataTextField = "Title";

        DropDownList_Category.DataSource = data;
        DropDownList_Category.DataBind();

        //Tạo thêm một Item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".:Chọn loại tài khoản:.";

        //Chèn Item mặc định vào đầu danh sách Dropdown
        DropDownList_Category.Items.Insert(0, item);

    }

    protected void DropDownList_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_Search_Click(object sender, EventArgs e)
    {
        SearchData();
    }

    public void SearchData(string type = "", string message = "")
    {
        //Lấy các giá trị tìm kiếm
        string catID = DropDownList_Category.SelectedValue;
        string title = input_Title.Value.Trim();

        //Tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/AccountList.aspx?catid={0}&title={1}&messagetype={2}&message={3}";
        url = url.StringFormat(catID, title, type, message);
        //Chuyển về trang đích
        Response.Redirect(url);
    }

    protected void LinkButton_ClearSearch_Click(object sender, EventArgs e)
    {
        //Tạo url cảu trang hiện tại bằng cách loại bỏ các điều kiện lọc
        DropDownList_Category.SelectedIndex = 0;
        input_Title.Value = string.Empty;

        //Gọi lại hàm search
        SearchData();
    }

    protected void LinkButton_SaveAvatar_Click(object sender, EventArgs e)
    {

        //Ép kiểu sender về dạng LinkButton
        LinkButton linkButton = sender as LinkButton;
        //Lấy ID của tài khoản cần đổi hình
        string ID = linkButton.CommandArgument.ToSafetyString();

        //tìm một tài khoản có ID như trên
        DBEntities db = new DBEntities();
        var item = db.Accounts.Where(x => x.Username == ID).FirstOrDefault();
        //Nêu không tìm thấy, thì load lại dữ liệu
        if (item == null)
        {
            SearchData("error", "Tài khoản cần đổi hình không tồn tại");
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
        uploadUtility.FolderSave = "~/fileuploads/Account";
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
        SearchData("success", "Đã upload hình thành công");
        return;
    }

    protected void LinkButton_CancelAvatar_Click(object sender, EventArgs e)
    {

    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        string ID = linkButton.CommandArgument.ToString();

        DBEntities db = new DBEntities();
        var item = db.Accounts.Where(x => x.Username == ID).FirstOrDefault();

        if (ID == SessionUtility.AdminUsername)
        {
            ucMessage.ShowError("Bạn không thể xóa chính mình");
            return;

        }

        if (SessionUtility.AdminCategoryID != "SupperAdmin")
        {
            ucMessage.ShowError("Bạn không đủ quyền để xóa tài khoản");
            return;
        }

        if (item == null)
        {
            SearchData("error", "Tài khoản không còn tồn tại");
            return;
        }

        db.Accounts.Remove(item);
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            ucMessage.ShowError("Chưa xóa được, vui lòng thử lại");
        }
        SearchData("success", "Đã xóa dữ liệu");
        return;
    }

    protected void LinkButton_Active_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        string ID = linkButton.CommandArgument.ToSafetyString();

        DBEntities db = new DBEntities();
        var item = db.Accounts.Where(x => x.Username == ID).FirstOrDefault();

        if (ID == SessionUtility.AdminUsername)
        {
            ucMessage.ShowError("Bạn không thể Active chính mình");
            return;

        }

        if (SessionUtility.AdminCategoryID != "SupperAdmin")
        {
            ucMessage.ShowError("Bạn không đủ quyền để xóa tài khoản");
            return;
        }
        if (item == null)
        {
            SearchData("error", "Tài khoản không còn tồn tại");
            return;
        }
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
        SearchData("success", "Đã cập nhật trạng thái thành công");
        return;

    }

}