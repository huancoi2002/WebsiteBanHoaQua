using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.IO;
using System.Drawing;

public partial class Admin_OrderDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    public void LoadData()
    {
        int ID = Request.QueryString["id"].ToInt();

        DBEntities db = new DBEntities();
        var item = db.Orders.Where(x => x.OrderID == ID).Select(x => new {
            MaDonHang = x.OrderID,
            TenKhachHang = x.FullName,
            Email = x.Email,
            DiaChi = x.Address,
            SoDTCaNhan = x.Mobi,
            SoDtNha = x.Mobi2,
            TongTien = x.Total,
            x.OrderStatus,
            x.ChargeStatus,
            x.DeliverStatus,
            x.OrderDetailLists

        }).FirstOrDefault();

        if (item == null)
        {
            return;
        }

        input_ID.Value = item.MaDonHang.ToSafetyString();
        input_Fullname.Value = item.TenKhachHang;
        input_Email.Value = item.Email;
        input_Mobi.Value = item.SoDTCaNhan;
        input_Mobi2.Value = item.SoDtNha;
        input_Address.Value = item.DiaChi;
        b_Total.InnerHtml = item.TongTien.Value.ToString("0,00 đ");

        if (item.OrderStatus == true)
            radio_Order.Checked = true;
        else
            radio_Order.Checked = false;

        if (item.DeliverStatus == true)
            radio_Deliver.Checked = true;
        else
            radio_Deliver.Checked = false;

        if (item.ChargeStatus == true)
            radio_Charge.Checked = true;
        else
            radio_Charge.Checked = false;

        //Kết bảng lấy thêm thông tin sản phẩm
        var otherInfo = item.OrderDetailLists.Select(x => new
        {
            ID = x.ProductID,
            Title = x.Product.Title,
            Avatar = x.Product.Avatar,
            Quantity = x.Product.Quantity,
            Price = x.Product.Price
        });

        Repeater_Detail.DataSource = otherInfo;
        Repeater_Detail.DataBind();
    }

    protected void LinkButton_PrintExel_Click(object sender, EventArgs e)
    {
    
    }

   
}