using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;

public partial class Admin_ProductEdit : System.Web.UI.Page
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
        var item = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
        if (item == null)
        {
            ucMessage.ShowError("Tên tài khoản không hợp lệ");
            return;
        }
        DropDownList_Category.SelectedValue = item.ProductCategoryID.ToSafetyString();
        input_ID.Value = item.ProductID.ToSafetyString();
        input_Code.Value = item.Code;
        input_Title.Value = item.Title;
        textarea_Decription.Value = item.Description;
        input_Position.Value = item.Position.ToSafetyString();
        input_Viewtime.Value = item.ViewTime.ToSafetyString();
        input_CreateTime.Value = item.CreateTime.ToString("dd/MM/yyyy");
        input_CreateBy.Value = item.CreateBy;

        textarea_Specification.Value = item.Specifications;
        input_Quantity.Value = item.Quantity.ToSafetyString();
        input_Price.Value = item.Price.ToSafetyString();
        input_OldPrice.Value = item.OldPrice.ToSafetyString();
        input_Promotion.Value = item.Promotions;
        input_WarrantyPolicy.Value = item.WarrantyPolicy;
        input_Accessories.Value = item.Accessories;
        input_SourcePage.Value = item.SourcePage;
        input_SourchLink.Value = item.SourceLink;
        textarea_Content.Value = item.Content;

        a_Avatar.HRef = item.Avatar;
        img_Avatar.Src = item.Avatar;

        //Load danh sách hình ImageList
        Repeater_ImageList.DataSource = GetImageListArray(item.ImageList);
        Repeater_ImageList.DataBind();


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

        string specification = textarea_Specification.Value.Trim();
        int quantity = input_Quantity.Value.ToInt();
        double price = input_Price.Value.ToDouble();
        double oldPrice = input_OldPrice.Value.ToDouble();
        string promotion = input_Promotion.Value.Trim();
        string warrantyPolicy = input_WarrantyPolicy.Value.Trim();
        string accessories = input_Accessories.Value.Trim();
        string sourcePage = input_SourcePage.Value.Trim();
        string sourceLink = input_SourchLink.Value.Trim();
        string content = textarea_Content.Value;

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
            uploadUtility.FolderSave = "~/fileuploads/Product";
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
            var item = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            //Nếu không có thì báo lỗi , kết thúc
            if (item == null)
            {
                ucMessage.ShowError("Dữ liệu không tồn tại");
                Response.Redirect("~/");
            }
            //cập nhật giá trị mới
            //Tạo một item mới có kiểu là bảng cần thêm
            item.ProductCategoryID = catID;
            item.Code = code;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;

            item.Specifications = specification;
            item.Promotions = promotion;
            item.WarrantyPolicy = warrantyPolicy;
            item.Accessories = accessories;
            item.SourcePage = sourcePage;
            item.SourceLink = sourceLink;
            item.Content = content;

            //Gán bộ sưu tập hình
            item.ImageList = hidden_ImageLists.Value;

            if (quantity > 0)
                item.Quantity = quantity;
            else if (input_Quantity.Value == string.Empty)
                item.Quantity = null;
            if (price > 0)
                item.Price = price;
            else if (input_Price.Value == string.Empty)
            {
                item.Price = null;
            }
            if (oldPrice > 0)
                item.OldPrice = oldPrice;
            else if (input_OldPrice.Value == string.Empty)
            {
                item.OldPrice = null;
            }
            //Khi cập nhật không thay đổi viewtime

            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            else if (input_Position.Value == string.Empty)
                item.Position = null;

            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }

            //Update dữ liệu
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/ProductList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Cập nhật sản phẩm thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
        //Ngược lại thì Insert (Add)
        else
        {
            //Kiểm tra ID trùng lắp
            DBEntities db = new DBEntities();
            //Tạo một item mới có kiểu là bảng cần thêm
            Product item = new Product();

            item.ProductCategoryID = catID;
            item.Code = code;
            item.Content = content;
            item.Title = title;
            item.Description = description;
            item.Status = status;
            item.CreateBy = SessionUtility.AdminUsername;
            item.CreateTime = DateTime.Now;
            item.ViewTime = RandomUtility.RandomNumber(1, 1000);

            item.Specifications = specification;
            item.Promotions = promotion;
            item.WarrantyPolicy = warrantyPolicy;
            item.Accessories = accessories;
            item.SourcePage = sourcePage;
            item.SourceLink = sourceLink;

            //Gán bộ sưu tập hình
            item.ImageList = hidden_ImageLists.Value;

            if (quantity > 0)
                item.Quantity = quantity;
            else if (input_Quantity.Value == string.Empty)
                item.Quantity = null;
            if (price > 0)
                item.Price = price;
            else if (input_Price.Value == string.Empty)
                item.Price = null;
            if (oldPrice > 0)
                item.OldPrice = oldPrice;
            else if (input_OldPrice.Value == string.Empty)
                item.OldPrice = null;
            //Khi cập nhật không thay đổi viewtime

            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }
            //Nếu có nhập vị trí thì lưu
            if (position > 0)
                item.Position = position;
            else if (input_Position.Value == string.Empty)
                item.Position = null;

            if (avatar != string.Empty)
            {
                item.Avatar = avatar;
                item.Thumb = thumb;
            }
            //Thêm mới
            db.Products.Add(item);
            db.SaveChanges();

            //Tạo url trang hiện tại kèm theo điều kiện search
            string url = "~/Admin/ProductList.aspx?messagetype={0}&message={1}";
            url = url.StringFormat("success", "Đã thêm mới sản phẩm thành công");
            //Chuyển về trang đích
            Response.Redirect(url);
        }
    }

    public void LoadCategory()
    {
        DBEntities db = new DBEntities();

        var query = db.ProductCategories.Select(x => new
        {
            ID = x.ProductCategoryID,
            x.Title
        });

        DropDownList_Category.DataValueField = "ID";
        DropDownList_Category.DataTextField = "Title";
        DropDownList_Category.DataTextFormatString = "-{0}-";

        DropDownList_Category.DataSource = query.ToList();
        DropDownList_Category.DataBind();
    }

    protected string[] GetImageListArray(object ImageList)
    {
        var arr = ImageList.ToSafetyString().Split(';').ToList();
        if (arr.Count>0 &&  arr.LastOrDefault().IsNullOrEmpty() )
        {
            arr.RemoveAt(arr.Count - 1);
        }
       
        return arr.ToArray();
    }
}