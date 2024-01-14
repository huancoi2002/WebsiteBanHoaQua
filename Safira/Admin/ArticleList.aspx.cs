using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_ArticleList : System.Web.UI.Page
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
        DBEntities db = new DBEntities();
        var query = from x in db.Articles
                    from y in db.ArticleCategories
                    from z in db.ArticleMainCategories
                    where
                     x.ArticleCategoryID == y.ArticleCategoryID
                    && y.ArticleMainCategoryID == z.ArticleMainCategoryID
                    orderby x.CreateTime
                    select new
                    {
                        ID = x.ArticleID,
                        x.Title,
                        CatTitle = y.Title,
                        x.Description,
                        x.ViewTime,
                        x.CreateBy,
                        x.CreateTime,
                        x.Avatar,
                        x.Thumb,
                        x.Status,
                        y.ArticleCategoryID
                    };



        ///lấy các tiêu chí search
        //lấy catid
        int catID = Request.QueryString["catid"].ToInt();

        //nếu có lọc theo  catid
        if (catID != 0)
        {
            DropDownList_Category.SelectedValue = catID.ToString();
            query = query.Where(x => x.ArticleCategoryID == catID);
        }
        //lấy title
        string title = Request.QueryString["title"].ToSafetyString();

        //nếu có lọc theo title
        if (title != string.Empty)
        {
            input_Title.Value = title;
            query = query.Where(x => x.Title.Contains(title) ||
            x.CatTitle.Contains(title) || x.Description.Contains(title)
            );
        }

        //phân trang dữ liệu
        int totalItems = query.Count();
        int maxPage = 10;
        int pageSize = 5;
        int currentpPage = Request.QueryString["page"].ToInt();

        if (currentpPage <= 0)
            currentpPage = 1;

        string url = "~/Admin/ArticleList.aspx?page={0}&catid={1}&title={2}";
        url = url.StringFormat("{0}", catID, title);

        //đổ vào ucpagging
        ucPagging.CurrentPage = currentpPage;
        ucPagging.PageSize = pageSize;
        ucPagging.MaxPage = maxPage;
        ucPagging.TotalItems = totalItems;
        ucPagging.URL = url;
        ucPagging.DataBind();

        //lấy dữ liệu
        int skip = (currentpPage - 1) * pageSize;
        var data = query.Skip(skip).Take(pageSize).ToList();

        //đổ vào bộ lặp
        Repeater_Main.DataSource = data;

        //hiển thị lên
        Repeater_Main.DataBind();

        //Hiển thị thông báo từ lần trước
        ShowPreviousMessage();
    }

    private void ShowPreviousMessage()

    {
        //Hiển thị thông báo từ lần trước
        string messagetype = Request.QueryString["messagetype"];
        string message = Request.QueryString["message"];
        if (messagetype == "success")
        {
            ucMessage.ShowSuccess(message);
        }
        else if (messagetype == "warning")
        {
            ucMessage.ShowSuccess(message);

        }
        else if (messagetype == "info")
        {
            ucMessage.ShowSuccess(message);

        }
        else if (messagetype == "error")
        {
            ucMessage.ShowSuccess(message);
        }
    }

    public void LoadCategoryData()
    {
        DBEntities db = new DBEntities();
        var query = db.ArticleCategories.Select(x => new { x.ArticleCategoryID, x.Title });

        var data = query.ToList();

        //Cấu hình hiển thị theo vị trí tương ứng
        DropDownList_Category.DataValueField = "ArticleCategoryID";//khóa value
       DropDownList_Category.DataTextField = "Title";// nhãn
        DropDownList_Category.DataSource = data;// đổ dữ liệu
        DropDownList_Category.DataBind();// hiện lên

        //tạo thêm một item mặc định
        ListItem item = new ListItem();
        item.Value = string.Empty;
        item.Text = ".:Chọn loại tin tức:.";

        //Chèn item mặc định vào đầu danh sách dropdown
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

    protected void LinkButton_ClearSearch_Click(object sender, EventArgs e)
    {
        //xóa sạch tiêu chí search
        DropDownList_Category.SelectedIndex = 0;
        input_Title.Value = string.Empty;
        //search lại
        SearchData();
    }

    protected void LinkButton_SaveAvatar_Click(object sender, EventArgs e)
    {
        //ép kiểu sender về dạng linkbutton
        LinkButton LinkButton_SaveAvatar = sender as LinkButton;

        //Lấy ID của tài khoản cần đổi hình
        int ID = LinkButton_SaveAvatar.CommandArgument.ToInt();

        //Tìm 1 hình ảnh có  ID như  trên
        DBEntities db = new DBEntities();
        var item = db.Articles.Where(x => x.ArticleID == ID).FirstOrDefault(); //tìm 1 phần tử

        //nếu không tìm thấy tài khoản thì load lại dữ liệu
        if (item == null)
        {
            SearchData("error", "Hình cần đổi không tồn tại");
            return;
        }

        //Upload hình ảnh lên server

        FileUpload FileUpload_Avatar = LinkButton_SaveAvatar.NamingContainer.FindControl("FileUpload_Avatar") as FileUpload;//tìm control upload

        ////kiểm tra đuôi lệ rồi mới upload
        string validExtension = ".jpg.jpeg.png.gif.bmp.ico";
        //Bóc tách đuôi
        string fileExtension = Path.GetExtension(FileUpload_Avatar.FileName.ToLower());//hàm Path chuyên xử lý tên file
        if (!validExtension.Contains(fileExtension))
        {
            ucMessage.ShowError("Hình không hợp lệ, Loại hình hỗ trợ chỉ gồm: jpg,jpeg,png,gif,bmp,ico ");
            return;
        }

        //kiểm tra dung lượng file <=3MB
        int validSize = 1024 * 1024 * 3;
        int fileSize = FileUpload_Avatar.FileBytes.Length;
        if (fileSize > validSize)
        {
            ucMessage.ShowError(" Dung lượng hình phải dưới 3MB");
            return;
        }

        Exception error = null;
        string avatar = string.Empty;
        string thumb = string.Empty;
        UploadUtility uploadUtility = new UploadUtility();
        uploadUtility.FileUpload = FileUpload_Avatar;
        uploadUtility.FolderSave = "~/fileuploads/Article";
        uploadUtility.FullMaxWidth = 1000;
        uploadUtility.ThumbMaxWidth = 400;
        uploadUtility.MaxFileSize = 1024 * 1024 * 3;
        uploadUtility.AutoGenerateFileName = true;
        uploadUtility.UploadImage(ref avatar, ref thumb, ref error);

        //kiểm tra, nếu có lỗi khi upload thì kết thúc
        if (error != null || avatar == string.Empty)
        {
            SearchData("error", "Chưa upload được hình do hệ thống lỗi");
            return;
        }

        //cập nhật link mới vào tài khoản đó
        item.Avatar = avatar;
        item.Thumb = thumb;
        //lưu DB
        db.SaveChanges();
        //load lại dữ liệu để thấy hình đc thay mới
        SearchData("success", "Đã đổi hình thành công", true);
        return;
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        //ép kiểu sender thành linkbutoon
        LinkButton LinkButton_Delete = sender as LinkButton;

        //Bóc tách lấy phần id của nó
        int ID = LinkButton_Delete.CommandArgument.ToInt();

        //kết nôi db
        DBEntities db = new DBEntities();

        //tìm 1 item phù hợp
        var item = db.Articles.Where(x => x.ArticleID == ID).FirstOrDefault();

        //Kiểm tra nếu item k tồn tại thì báo lỗi
        if (item == null)
        {
            SearchData("error", "Tin tức này không còn tồn tại");
            return;
        }

        //Kiểm tra quyền, có đủ để thực hiện hay ko
        if (SessionUtility.AdminCategoryID != "SupperAdmin")
        {
            ucMessage.ShowError("Bạn ko đủ quyền để xóa hình. Hãy liên hệ Administrator");
            return;
        }


        //Xóa khỏi DB
        db.Articles.Remove(item);

        //Lưu db lại
        db.SaveChanges();

        //load lại trang
        SearchData("success", "Đã xóa tin tức", true);
        return;
    }

    protected void LinkButton_Active_Click(object sender, EventArgs e)
    {
        //ép kiểu sender thành LinkButton. control là gì thì ép ra kiểu đó
        LinkButton LinkButton_Active = sender as LinkButton;
        //bóc tách id từ linkbutton ra
        int ID = LinkButton_Active.CommandArgument.ToInt();

        //kết nối db
        DBEntities db = new DBEntities();
        //tìm 1 item có ID phù hợp tromg bảng Account
        var item = db.Articles.Where(x => x.ArticleID == ID).FirstOrDefault();
        //Nếu ko có item phù họp thì kết thúc
        if (item == null)
        {
            SearchData("error", "Tin tức không còn tồn tại");
            return;
        }

        //Cập nhật giá trị status mới
        //cách 1:( item.Status = (item.Status == true ? false : true);)
        //cách 2:if (item.Status == true)
        //  item.Status = false;
        //else
        //    item.Status = true;

        item.Status = !item.Status;
        //Lưu DB,Lưu ko đc thì báo lỗi và kết thúc
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ucMessage.ShowError("Chưa lưu  được, vui lòng thử lại");
            return;
        }


        //Load lại dữ liệu với các tiêu chí search
        SearchData("success", "Đã cập nhật trạng thái", true);
        return;
    }

    public void SearchData(string type = "", string message = "", bool isKeepPage = false)
    {
        //Lấy các giá trị tìm kiếm
        string catID = DropDownList_Category.SelectedValue;//lấy giá trị ẩn bên dưới dropdown
        string title = input_Title.Value.Trim();

        int page = 1;
        if (isKeepPage == true)
        {
            page = Request.QueryString["page"].ToInt();
            if (page <= 0)
                page = 1;
        }
        //tạo url trang hiện tại kèm theo điều kiện search
        string url = "~/Admin/ArticleList.aspx?catid={0}&title={1}&messagetype={2}&message={3}&page={4}";
        url = url.StringFormat(catID, title, type, message, page); //truyền vào đường dẫn

        //chuyển về trang đích
        Response.Redirect(url);

    }

}
