using CodeUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OrderList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage.HideAll();
            showPreviousMessage();
            LoadData();
        }
    }

    private void showPreviousMessage()
    {
        string messageType = Request.QueryString["messagetype"].ToSafetyString();
        string message = Request.QueryString["message"].ToSafetyString();

        if (messageType == "success")
        {
            ucMessage.ShowSuccess(message);
        }
        if (messageType == "error")
        {
            ucMessage.ShowError(message);
        }
        if (messageType == "Warning")
        {
            ucMessage.ShowWarning(message);
        }
        if (messageType == "Info")
        {
            ucMessage.ShowInfo(message);
        }
    }

    public void LoadData()
    {
        // kết nối db
        DBEntities db = new DBEntities();

        // query dữ liệu
        var query = db.Orders.OrderByDescending(p => p.CreateTime).Select(p => new
        {
            ID = p.OrderID,
            p.Total,
            p.FullName,
            p.Email,
            p.Mobi,
            p.Mobi2,
            p.Address,
            p.Gender,
            p.OrderStatus,
            p.DeliverStatus,
            p.ChargeStatus,
            p.CreateTime
        });

        // lấy tiêu chí search ID từ url
        int ID = Request.QueryString["id"].ToInt();

        // nếu có thì search theo catID
        if (ID != 0)
        {
            //thêm điều kiện động vào query
            query = query.Where(p => p.ID == ID);
            input_ID.Value = ID.ToSafetyString();
        }
        // lấy tiêu chí search title từ url nếu có thì search theo title
        string title = Request.QueryString["title"].ToSafetyString();

        // nếu có thì search theo title
        if (title != string.Empty)
        {
            //thêm điều kiện động vào query
            query = query.Where(p => p.FullName.Contains(title)
            || p.Email.Contains(title)
            || p.Mobi.Contains(title)
            || p.Mobi2.Contains(title));

            // hiển thị tiêu chí search vao ô title
            input_Title.Value = title;
        }

        ////Phân trang dữ liệu\\\\\
        int totalItems = query.Count();
        int pageSize = 5;
        int maxPage = 5;
        int currentPage = Request.QueryString["page"].ToInt();
        if (currentPage <= 0)
            currentPage = 1;

        string url = "~/Admin/OrderList.aspx?page={0}&id={1}&title={2}";
        url = url.StringFormat("{0}", ID, title);

        ucPagging.TotalItems = totalItems;
        ucPagging.MaxPage = maxPage;
        ucPagging.PageSize = pageSize;
        ucPagging.CurrentPage =  currentPage;
        ucPagging.URL = url;
        ucPagging.DataBind();

        //lấy danh sách dữ liệu phù hợp
        int skip = (currentPage - 1) * pageSize;
        var data = query.Skip(skip).Take(pageSize).ToList();

        // đổ vào bộ lập
        Repeater_Main.DataSource = data;

        // hiển thị lên
        Repeater_Main.DataBind();


    }

    public void SearchData(string messageType = "", string message = "", bool isKeepPage = false)
    {
        // lấy các tiêu chí search
        int ID = input_ID.Value.ToInt();
        string title = input_Title.Value.Trim();
        int page = 1;
        if (isKeepPage == true)
        {
            page = Request.QueryString["page"].ToInt();
            if (page <= 0)
                page = 1;
        }

        // tạo url mới từ trang hiện tạo + tiêu chí search
        string url = "~/Admin/OrderList.aspx?id={0}&title={1}&messagetype={2}&message={3}&page={4}";
        url = url.StringFormat(ID, title, messageType, message,page);

        //chuyển đến url mới
        Response.Redirect(url);
    }

    protected void LinkButton_Search_Click(object sender, EventArgs e)
    {
        SearchData();
    }

    protected void LinkButton_ClearSearch_Click(object sender, EventArgs e)
    {
        // xóa nội dung search
        input_ID.Value = string.Empty;
        input_Title.Value = string.Empty;

        // search lại
        SearchData();
    }

    protected void LinkButton_Delete_Click(object sender, EventArgs e)
    {
        //Ép kiểu sender thành linkbutton
        LinkButton LinkButton_Delete = sender as LinkButton;

        //Bóc tách id từ nó ra
        int ID = LinkButton_Delete.CommandArgument.ToInt();

        //Kết nối DB
        DBEntities db = new DBEntities();

        //vào DB, Tìm item có ID phù hợp
        var item = db.Orders.Where(x => x.OrderID == ID).FirstOrDefault();

        //Nếu không có, thì load lại trang
        if (item == null)
        {
            SearchData("error", "Dữ liệu không còn tồn tại");
            return;
        }

        //Xóa những orderdetail liên quan
        foreach (var childItem in item.OrderDetailLists.ToList())
        {
            db.OrderDetailLists.Remove(childItem);
        }

        //Xóa khỏi bảng
        db.Orders.Remove(item);

        //Lưu db
        try
        {
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            ucMessage.ShowError("Không thể xóa dữ liệu này. do có tài khoản tham chiếu");
            return;
        }

        //Load lại trang
        SearchData("success", "Đã xóa dữ liệu", true);

    }
}