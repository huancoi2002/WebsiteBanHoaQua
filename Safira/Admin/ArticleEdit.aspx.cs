using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_ArticleEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.ShowInfo("Xin mời nhập thông tin sau đó nhấn Lưu");
            LoadCategory();

            int ID = Request.QueryString["id"].ToInt();
            if (ID > 0)
            {
                LoadData();
            }
        }
    }
    public void LoadData()
    {
        //Lấy ID từ url
        int ID = Request.QueryString["id"].ToInt();
        //vào db tìm item phù hợp
        DBEntities db = new DBEntities();
        var item = db.Articles.Where(x => x.ArticleID == ID).FirstOrDefault();

        //nếu ko có thì báo lỗi và kết thúc
        if (item == null)
        {
            ucMessage.ShowError("Dữ liệu này không còn tồn tại");
            return;
        }
        //hiển thị từng giá trị lên control
        DropDownList_Category.SelectedValue = item.ArticleCategoryID.ToSafetyString();
        input_ID.Value = item.ArticleID.ToSafetyString();
        input_Code.Value = item.Code;
        input_Title.Value = item.Title;
        textarea_Description.Value = item.Description;
        textarea_Content.Value = item.Content;
        input_Position.Value = item.Position.ToSafetyString();

        input_ViewTime.Value = item.ViewTime.ToSafetyString();
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
    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var query = db.ArticleCategories.Select(x =>
          new
          {
              ID = x.ArticleCategoryID,
              x.Title
          });
        DropDownList_Category.DataValueField = "ID";
        DropDownList_Category.DataTextField = "Title";
        DropDownList_Category.DataTextFormatString = "- {0} -";

        DropDownList_Category.DataSource = query.ToList();
        DropDownList_Category.DataBind();
    }


    protected void LinkButton_Save_Click(object sender, EventArgs e)
    {
        ucMessage.HideAll();
        //lấy id từ url xuống
        int id = Request.QueryString["id"].ToInt();

        //Lấy giá trị các ô nhập
        int catID = DropDownList_Category.SelectedValue.ToInt();
        int position = input_Position.Value.ToInt();
        string code = input_Code.Value.Trim();
        string title = input_Title.Value.Trim();
        string description = textarea_Description.Value.Trim();
        string content = textarea_Content.Value.Trim();
        bool status = radio_Active.Checked;

        //upload hình

        string avatar = string.Empty;
        string thumb = string.Empty;
        if (FileUpload_Avatar.FileName != string.Empty)
        {

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
            UploadUtility uploadUtility = new UploadUtility();
            uploadUtility.FileUpload = FileUpload_Avatar;
            uploadUtility.FolderSave = "~/fileuploads/Article";
            uploadUtility.FullMaxWidth = 1000;
            uploadUtility.ThumbMaxWidth = 400;
            uploadUtility.MaxFileSize = 1024 * 1024 * 3;
            uploadUtility.AutoGenerateFileName = true;
            uploadUtility.UploadImage(ref avatar, ref thumb, ref error);
        }

        //kiểm tra title hợp lệ
        if (title == string.Empty)
        {
            ucMessage.ShowError("Vui lòng nhập tiêu đề hình");
            return;
        }
        //nếu có Id thì update
        if (id > 0)
        {
            //Tìm 1 item thích hợp
            DBEntities db = new DBEntities();

            var item = db.Articles.Where(x => x.ArticleID == id).FirstOrDefault();

            //nếu ko có, thì báo lỗi, kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu này không còn tồn tại");
                return;
            }

            //ngược lại, Cập nhật giá trị mới
            //lần lượt gán từng giá trị vào mỗi ô của item
            item.ArticleCategoryID = catID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Content = content;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            //khi cập nhật thì k thay đổi veiwtime

            //nếu có nahapj vị trì thì mới lưu
            if (position > 0)
                item.Position = position;

            //nếu có up hình thì thì mới cập nhật hình
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }


            //thêm vào bảng
            db.SaveChanges();

            //tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/ArticleList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Đã cập nhật dữ liệu");

            //chuyển về trang danh sách
            Response.Redirect(url);
        }
        //ngược lại ko có id thì thêm mới
        else
        {
            DBEntities db = new DBEntities();
            Article item = new Article();

            //lần lượt gán từng giá trị vào mỗi ô của item
            //lần lượt gán từng giá trị vào mỗi ô của item
            item.ArticleCategoryID = catID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Content = content;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;
            item.ViewTime = RandomUtility.RandomNumber(100, 1000);

            //nếu có nahapj vị trì thì mới lưu
            if (position > 0)
                item.Position = position;

            //nếu có up hình thì thì mới cập nhật hình
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //thêm vào bảng
            db.Articles.Add(item);
            db.SaveChanges();


            //tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/ArticleList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Đã thêm mới dữ liệu");

            //chuyển về trang đích
            Response.Redirect(url);

        }
    }
}