using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_PictureCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            ShowPreviousMessage();
            LoadMainCategoryData();
            LoadCategoryData();
            LoadData();
        }
    }

    public void LoadData()
    {
        //Lấy ID và mID từ url
        int ID = Request.QueryString["id"].ToInt();


        //Vào DB tìm 1 item phù hợp
        DBEntities db = new DBEntities();
        var item = db.PictureCategories.Where(x => x.PictureCategoryID == ID).FirstOrDefault();

        //Nếu ko có thì báo lỗi, kết thúc
        if (item == null)
        {
            return;
        }
        //Hiển thị từng giá trị lên các control
        DropDownList_MainCategory.SelectedValue = item.PictureMainCategoryID.ToSafetyString();
        ListBox_Category.SelectedValue = item.PictureCategoryID.ToSafetyString();

        input_ID.Value = item.PictureCategoryID.ToSafetyString();
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

    public void LoadMainCategoryData()
    {
        DBEntities db = new DBEntities();
        //Query dữ liệu
        var query = db.PictureMainCategories.OrderBy(x => x.Position).Select(x => new { x.PictureMainCategoryID, x.Title });
        //Lấy danh sách query đó
        var data = query.ToList();
        //Cấu hình hiển thị
        DropDownList_MainCategory.DataValueField = "PictureMainCategoryID";//Khóa chính
        DropDownList_MainCategory.DataTextField = "Title";//Tên cột cần hiển thị

        DropDownList_MainCategoryLeft.DataValueField = "PictureMainCategoryID";//Khóa chính
        DropDownList_MainCategoryLeft.DataTextField = "Title";//Tên cột cần hiển thị

        //Đổ dữ liệu
        DropDownList_MainCategory.DataSource = data;
        DropDownList_MainCategoryLeft.DataSource = data;
        //Hiển thị
        DropDownList_MainCategory.DataBind();
        DropDownList_MainCategoryLeft.DataBind();

        //Chọn lại phần tử đã click trước đó
        int mID = Request.QueryString["mid"].ToInt();
        if (mID > 0)
        {
            DropDownList_MainCategoryLeft.SelectedValue = mID.ToSafetyString();
            DropDownList_MainCategory.SelectedValue = mID.ToSafetyString();
        }

    }

    public void LoadCategoryData()
    {
        int mID = DropDownList_MainCategoryLeft.SelectedValue.ToInt();
        //Kết nối DB
        DBEntities db = new DBEntities();
        //Query dữ liệu
        var query = db.PictureCategories.OrderBy(x => x.Position).Where(x => x.PictureMainCategoryID == mID).Select(x => new { x.PictureCategoryID, x.Title });
        //Lấy danh sách query đó
        var data = query.ToList();
        //Cấu hình hiển thị
        ListBox_Category.DataValueField = "PictureCategoryID";//Khóa chính
        ListBox_Category.DataTextField = "Title";//Tên cột cần hiển thị
        //Đổ dữ liệu
        ListBox_Category.DataSource = data;
        //Hiển thị
        ListBox_Category.DataBind();

        //Tạo thêm một item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".:Chọn loại hình ảnh cấp con:.";

        //Chèn item mặc định vào đầu danh sách dropdown
        ListBox_Category.Items.Insert(0, item);//Vị trí và chèn thằng nào vào dropdown

    }

    public void SearchData(string type = "", string messages = "")
    {
        //Lấy các giá trị tìm kiếm
        int ID = ListBox_Category.SelectedValue.ToInt();
        int mID = DropDownList_MainCategoryLeft.SelectedValue.ToInt();
        string title = input_Title.Value.Trim();

        //Tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/PictureCategoryList.aspx?mid={0}&id={1}&messagetype={2}&message={3}";
        url = url.StringFormat(mID, ID, type, messages);

        //Chuyển về trang đích
        Response.Redirect(url);
    }

    private void ShowPreviousMessage()
    {
        //Hiển thị thông báo từ lần trước
        String messageType = Request.QueryString["messagetype"].ToSafetyString();
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

    protected void DropDownList_MainCategoryLeft_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load dữ liệu con
        LoadCategoryData();
        SearchData();
    }

    protected void DropDownList_MainCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ListBox_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_Add_Click(object sender, EventArgs e)
    {
        ListBox_Category.SelectedIndex = 0;
        SearchData();
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        //Lấy ID của tài khoản cần xóa
        int ID = Request.QueryString["id"].ToInt();

        if (ID <= 0)
        {
            ucMessage.ShowError("Vui lòng chọn một phần tử để xóa");
            return;
        }

        //Tìm một tài khoản có ID như trên
        DBEntities db = new DBEntities();
        var item = db.PictureCategories.Where(x => x.PictureCategoryID == ID).FirstOrDefault();

        //Nếu không tìm thấy thì load lại dữ liệu
        if (item == null)
        {
            SearchData("error", "Dữ liệu không còn tồn tại, vui lòng kiểm tra lại");
            return;
        }

        //Xóa dữ liệu được chọn
        db.PictureCategories.Remove(item);
        try
        {
            //Lưu lại DB
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ucMessage.ShowError("Không thể xóa do ràng buộc dữ liệu, vui lòng xóa dữ liệu liên quan trước");
            return;
        }

        //Load lại danh sách
        LoadCategoryData();
        //Load lại trang
        SearchData("success", "Đã xóa dữ liệu");
        return;

    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        //Ẩn thông báo
        ucMessage.HideAll();

        //Lấy ID từ Url xuống
        int ID = Request.QueryString["id"].ToInt();

        //Lấy giá trị các ô nhập
        int mainCatID = DropDownList_MainCategory.SelectedValue.ToInt();
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
                ucMessage.ShowError("Hình không hợp lệ, loại hình hỗ trợ: jpg, jpeg, png, gif, bmp, ico");
                return;
            }

            //Kiểm tra dung lượng file <= 3Mb
            int validSize = 1024 * 1024 * 3;
            int fileSize = FileUpload_Avatar.FileBytes.Length;
            if (fileSize > validSize)
            {
                ucMessage.ShowError("Dung lượng hình cần <= 3Mb");
                return;
            }

            Exception error = null;
            UploadUtility uploadUtility = new UploadUtility();
            uploadUtility.FileUpload = FileUpload_Avatar;
            uploadUtility.FolderSave = "~/fileuploads/PictureCategory";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        //Kiểm tra title hợp lệ
        if (title == string.Empty)
        {
            ucMessage.ShowError("Vui lòng nhập tiêu đề hình");
            return;
        }

        //Nếu có ID, thì Update (Save)
        if (ID > 0)
        {
            //Tìm 1 item thích hợp
            DBEntities db = new DBEntities();
            var item = db.PictureCategories.Where(x => x.PictureCategoryID == ID).FirstOrDefault();

            //Nếu ko có, thì báo lỗi, kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu này không còn tồn tại");
                return;
            }

            //Cập nhật giá trị mới
            item.PictureMainCategoryID = mainCatID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;

            //Nếu có nhập vị trí, thì lưu vị trí
            if (position > 0)
                item.Position = position;

            //Nếu có úp hình, thì mới cập nhật hình mới
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Lưu lại
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/PicturecategoryList.aspx?mid={0}&id={1}&messagetype={2}&message={3}";
            url = url.StringFormat(item.PictureMainCategoryID, item.PictureCategoryID, "success", "Đã cập nhật dữ liệu");

            //Chuyển về trang đích
            Response.Redirect(url);
        }

        //Ngược lại, thì Insert (Add)
        else
        {
            //Tạo 1 item mới có kiểu là bảng cần thêm
            PictureCategory item = new PictureCategory();

            //Lần lượt gán từng giá trị vào mỗi ô của item
            item.PictureMainCategoryID = mainCatID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;

            //Nếu có nhập vị trí, thì lưu vị trí
            if (position > 0)
                item.Position = position;

            //Nếu có úp hình, thì mới cập nhật hình mới
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Thêm vào bảng
            DBEntities db = new DBEntities();
            db.PictureCategories.Add(item);

            //Lưu DB
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/PicturecategoryList.aspx?mid={0}&id={1}&messagetype={2}&message={3}";
            url = url.StringFormat(item.PictureMainCategoryID, item.PictureCategoryID, "success", "Đã cập nhật dữ liệu");

            //Chuyển về trang đích
            Response.Redirect(url);
        }
    }


}