using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_ArticleCategoryList : System.Web.UI.Page
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

    private void ShowPreviousMessage()
    {
        //Hiển thị thông báo từ lần trước
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

    public void LoadMainCategoryData()
    {
        DBEntities db = new DBEntities();
        var query = db.ArticleMainCategories.OrderBy(x => x.Position).Select(x => new { x.ArticleMainCategoryID, x.Title });

        var data = query.ToList();

        DropDownList_MainCategory_Left.DataValueField = "ArticleMainCategoryID";
        DropDownList_MainCategory_Left.DataTextField = "Title";

        DropDownList_MainCategory.DataValueField = "ArticleMainCategoryID";
        DropDownList_MainCategory.DataTextField = "Title";

        DropDownList_MainCategory_Left.DataSource = data;
        DropDownList_MainCategory_Left.DataBind();

        DropDownList_MainCategory.DataSource = data;
        DropDownList_MainCategory.DataBind();

        //chọn phần tử đã click trước đó
        int MID = Request.QueryString["mid"].ToInt();
        if (MID > 0)
        {
            DropDownList_MainCategory_Left.SelectedValue = MID.ToSafetyString();
            DropDownList_MainCategory.SelectedValue = MID.ToSafetyString();
        }

    }

    public void LoadCategoryData()
    {
        int MID = DropDownList_MainCategory_Left.SelectedValue.ToInt();

        DBEntities db = new DBEntities();

        var query = db.ArticleCategories.OrderBy(x => x.Position).Where(x => x.ArticleMainCategoryID == MID)
            .Select(x => new { x.ArticleCategoryID, x.Title });

        var data = query.ToList();

        //Cấu hình hiển thị
        ListBox_Category.DataValueField = "ArticleCategoryID";
        ListBox_Category.DataTextField = "Title";

        //Đổ dữ liệu
        ListBox_Category.DataSource = data;

        //Hiển thị
        ListBox_Category.DataBind();

        //Tạo thêm 1 item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".: Chọn loại hình ảnh :.";

        //Chèn item mặc định vào đầu danh sách Dropdown
        ListBox_Category.Items.Insert(0, item);


    }

    public void LoadData()
    {
        //Lấy MID, ID từ url 

        int ID = Request.QueryString["id"].ToInt();

        //Chọn lại item đang chọn trước đó
        if (ID > 0)
        {
            ListBox_Category.SelectedValue = ID.ToSafetyString();
        }

        //Vào db tìm 1 item phù hợp
        DBEntities db = new DBEntities();
        var item = db.ArticleCategories.Where(x => x.ArticleCategoryID == ID).FirstOrDefault();

        //Nếu k có thì báo lỗi, kết thúc
        if (item == null)
        {
            return;
        }

        //Hiển thị từng giá trị lên các control
        DropDownList_MainCategory.SelectedValue = item.ArticleMainCategoryID.ToSafetyString();
        input_ID.Value = item.ArticleMainCategoryID.ToSafetyString();
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

        //Load dữ liệu cho ListBox
        LoadCategoryData();
    }

    public void SearchData(string type = "", string message = "")
    {
        //Lấy các giá trị tìm kiếm 
        int MID = DropDownList_MainCategory_Left.SelectedValue.ToInt();
        int ID = ListBox_Category.SelectedValue.ToInt();

        //Tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/ArticleCategoryList.aspx?id={0}&mid={1}&messagetype={2}&message={3}";

        url = url.StringFormat(ID, MID, type, message);

        Response.Redirect(url);
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
        //Tách lấy ID
        int ID = Request.QueryString["id"].ToInt();

        if (ID <= 0)
        {
            ucMessage.ShowError("Vui lòng chọn dữ liệu cần xóa");
        }

        //Kết nối DB
        DBEntities db = new DBEntities();

        //Tìm item phù hợp
        var item = db.ArticleCategories.Where(x => x.ArticleCategoryID == ID).FirstOrDefault();

        //Kiểm tra nếu item k tồn tại thì báo lỗi
        if (item == null)
        {
            SearchData("error", "Dữ liệu không tồn tại, vui lòng kiểm tra lại");
            return;
        }

        //Kiểm tra quyền có đủ để thực hiện không?
        if (SessionUtility.AdminCategoryID != "SupperAdmin")
        {
            ucMessage.ShowWarning("Bạn k đủ quyền để xóa tài khoản, vui lòng liên hệ với Administrator");
            return;
        }

        //Xóa khỏi DB
        db.ArticleCategories.Remove(item);


        try
        {
            //Lưu DB lại
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ucMessage.ShowError("Không thể xóa do ràng buộc dữ liệu, vui lòng xóa dữ liệu liên quan trước");
            return;
        }
        //Load lại dữ liệu vừa xóa để kiểm tra
        LoadCategoryData();

        //Load lại trang
        SearchData("success", "Đã xóa dữ liệu");
        return;

    }

    protected void DropDownList_MainCategory_Left_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategoryData();
        SearchData();
    }

    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        //Lấy ID từ URL xuống
        int ID = Request.QueryString["id"].ToInt();

        //Lấy giá trị các ô nhập
        int mainCatID = DropDownList_MainCategory.SelectedValue.ToInt();
        int position = input_Position.Value.ToInt();
        string code = input_Code.Value.Trim();
        string title = input_Title.Value.Trim();
        string description = textarea_Description.Value.Trim();

        string avatar = string.Empty;
        string thumb = string.Empty;

        //upload hình
        if (FileUpload_Avatar.FileName != string.Empty)
        {
            //Upload hình ảnh lên server

            //Kiểm tra đuôi hình có hợp lệ?
            string validExtention = ".jpg.png.jpeg.gif.bmp.ico";
            string fileExtention = Path.GetExtension(FileUpload_Avatar.FileName.ToLowerCase());

            if (!validExtention.Contains(fileExtention))
            {
                ucMessage.ShowError("dữ liệu k hợp lệ! lưu ý: chỉ hỗ trợ các tập tin có chứa phần mở rộng là: .jpg.png.jpeg.gif.bmp.ico");
                return;
            }

            //Kiểm tra dung lượng file
            int validSize = 1024 * 1024 * 3;
            int fileSize = FileUpload_Avatar.FileBytes.Length;
            if (fileSize > validSize)
            {
                ucMessage.ShowError("dữ liệu k hợp lệ! lưu ý: dung lượng hình k được vượt quá 3Mb");
                return;
            }


            Exception error = null;
            UploadUtility uploadUtility = new UploadUtility();
            uploadUtility.FileUpload = FileUpload_Avatar;
            uploadUtility.FolderSave = "/~fileuploads/ArticleCategory";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        bool status = radio_Active.Checked == true ? true : false;

        /////////////////Kiểm tra tính hợp lệ//////////////

        //kiểm tra tiêu đề
        if (title == string.Empty)
        {
            ucMessage.ShowError("Vui lòng nhập tiêu đề cho hình ảnh");
        }

        //Nếu có ID thì Update (save)
        if (ID > 0)
        {
            //Tìm 1 item thích hợp
            DBEntities db = new DBEntities();
            var item = db.ArticleCategories.Where(x => x.ArticleCategoryID == ID).FirstOrDefault();

            //Nếu k có thì báo lỗi, kết thúc
            if (item == null)
            {
                return;
            }

            //Cập nhật giá trị mới

            item.ArticleMainCategoryID = mainCatID;

            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;

            //Nếu có nhập vị trí thì lưu vị trí
            if (position > 0)
                item.Position = position;

            //K thêm viewtime được
            //item.ViewTime = RandomUtility.RandomInt(100);

            //nếu có up hình thì mới cập nhập hình mới
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Lưu lại, sau đó  chuyển về trang danh sách
            db.SaveChanges();

            //Tạo url trang hiện tại theo điều kiện search
            string url = "~/Admin/ArticleCategoryList.aspx?&mid={0}&id={1}&messagetype={2}&message={3}";
            url = url.StringFormat(item.ArticleMainCategoryID, item.ArticleCategoryID, "success", "Đã cập nhật dữ liệu");

            //Chuyển về trang đích
            Response.Redirect(url);


        }

        //Ngược lại, thì Insert (add)
        else
        {
            //Thêm mới

            //Tạo 1 item mới có kiểu là bảng cần thêm
            ArticleCategory item = new ArticleCategory();

            //Lần lượt gán từng giá trị vào mỗi ô của item
            //Nếu có nhập vị trí thì lưu vị trí
            item.ArticleMainCategoryID = mainCatID;

            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            if (position > 0)
                item.Position = position;

            //nếu có up hình thì mới cập nhập hình mới
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            DBEntities db = new DBEntities();

            db.ArticleCategories.Add(item);

            //Thêm vào bảng
            db.SaveChanges();

            //Tạo url trang hiện tại theo điều kiện search
            string url = "~/Admin/ArticleCategoryList.aspx?mid={0}&id={1}&messagetype={2}&message={3}";
            url = url.StringFormat(item.ArticleMainCategoryID, item.ArticleCategoryID, "success", "Đã thêm dữ liệu");

            //Chuyển về trang
            Response.Redirect(url);

        }

    }
}