using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Admin_Pages_ContactList : System.Web.UI.Page
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

        int catID = Request.QueryString["catid"].ToInt();
        string title = Request.QueryString["title"].ToSafetyString();

        DBEntities db = new DBEntities();
        var query = db.Contacts.OrderByDescending(x => x.CreateTime).Select(x => new
        {
            ID = x.ContactID,
            x.FullName,
            x.Content,
            x.Address,
            x.Email,
            x.CreateTime,
            x.Mobi,
            x.Status,
            CatID = x.ContactCategoryID,
            x.ApproveBy
        });
        if (catID > 0)
        {
            query = query.Where(x => x.CatID == catID);
            //Đổ lại Dropdown
            DropDownList_Category.SelectedValue = catID.ToSafetyString();
        }

        if (title != string.Empty)
        {
            query = query.Where(x => x.FullName.Contains(title) || x.Mobi.Contains(title) || x.Email.Contains(title));
            //Đổ lại vào ô input
            input_Title.Value = title;
        }
        //Phân trang dữ liệu
        int totalItem = query.Count();
        if (totalItem <= 0)
        {
            tr_DataEmpty.Visible = true;
        }
        int maxPage = 10;
        int pageSize = 10;
        int curuntPage = Request.QueryString["page"].ToInt();
        if (curuntPage <= 0)
        {
            curuntPage = 1;
        }
        string url = "~/Admin/ContactList.aspx?catid={0}&title={1}&page={2}";
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

        Repeater_Main.DataSource = data;
        Repeater_Main.DataBind();

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
        var query = db.ContactCategories.Select(x => new { x.ContactCategoryID, x.Title });
        var data = query.ToList();

        //Cấu hình hiển thị
        DropDownList_Category.DataValueField = "ContactCategoryID";
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
        string url = "~/Admin/ContactList.aspx?catid={0}&title={1}&messagetype={2}&message={3}";
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


    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = sender as LinkButton;
        int ID = linkButton.CommandArgument.ToInt();

        DBEntities db = new DBEntities();
        var item = db.Contacts.Where(x => x.ContactID == ID).FirstOrDefault();

        if (item == null)
        {
            SearchData("error", "Thư liên hệ không còn tồn tại");
            return;
        }

        db.Contacts.Remove(item);
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
        int ID = linkButton.CommandArgument.ToInt();

        DBEntities db = new DBEntities();
        var item = db.Contacts.Where(x => x.ContactID == ID).FirstOrDefault();

        if (item == null)
        {
            SearchData("error", "Thư liên hệ không còn tồn tại");
            return;
        }
        item.Status = !item.Status;
        item.ApproveBy = SessionUtility.AdminUsername;
        //Lưu db
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            ucMessage.ShowError("Chưa lưu được, vui lòng thử lại");
            return;
        }
        SearchData("success", "Đã cập nhật trạng thái thành công");
        return;

    }
}