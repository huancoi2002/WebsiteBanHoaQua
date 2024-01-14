using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using System.Threading;

public partial class Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucMessage_Top.HideAll();
            ucMessage_Bottom.HideAll();
            LoadData();
        }
    }

    public void LoadData()
    {
        //Nếu khách đã đăng nhập, Thì load thông tin tài khoản
        if (SessionUtility.Client != null)
        {
            //Chọn checked
            RadioButton_AccountInfo.Checked = true;
            //Ẩn thông báo
            ucMessage_Top.HideAll();
            ucMessage_Bottom.HideAll();

            //Load Thông tin
            LoadAccountInfo();

        }
        //ngược lại cho nhập thông tin mới
        else
        {
            RadioButton_NewInfo.Checked = true;

            //Load Trắng form
            ucMessage_Top.ShowInfo("Quý khách vui lòng <a href='/Account.aspx' style='text-decoration:underline;'>đăng nhập</a> để quản lý đơn hàng tốt hơn");
            ucMessage_Bottom.ShowInfo("Quý khách vui lòng <a href='/Account.aspx'  style='text-decoration:underline;'>đăng nhập</a> để quản lý đơn hàng tốt hơn");

            LoadNewInfo();
        }

        span_Amount.InnerHtml = SessionUtility.Cart.Amount.ToString("0,00 đ");
        Repeater_Product.DataSource = SessionUtility.Cart.CartItems.Values.ToList();
        Repeater_Product.DataBind();
    }

    private void LoadAccountInfo()
    {
        //Hiển thị thông tin tài khoản
        input_Email.Value = SessionUtility.Client.Email;
        input_FullName.Value = SessionUtility.Client.FullName;
        input_Phone.Value = SessionUtility.Client.Mobi;
        textarea_Address.Value = SessionUtility.Client.Address;

       


    }

    private void LoadNewInfo()
    {
        //Xóa trắng form
        input_Email.Value = string.Empty;
        input_FullName.Value = string.Empty;
        input_Phone.Value = string.Empty;

        textarea_Address.Value = string.Empty;

    }

    private bool IsValid()
    {
        //Khai báo biến tạm lấy giá trị trên FORM
        string email = input_Email.Value.Trim();
        string fullName = input_FullName.Value.Trim();
        string mobil = input_Phone.Value.Trim();
        string address = textarea_Address.Value.Trim();

      
        int paymentMethod = 0;
        if (input_checkout_online.Checked)
        {
            paymentMethod = 1;
        }


        //Kiểm tra hợp lệ
        if (!email.IsEmailFormat())
        {
            ucMessage_Bottom.ShowError("Vui lòng nhập email đúng định dạng");
            return false;
        }

        if (fullName.IsNullOrEmpty())
        {
            ucMessage_Bottom.ShowError("Vui lòng nhập họ tên người nhận hàng...");
            return false;
        }

        if (mobil.IsNullOrEmpty())
        {
            ucMessage_Bottom.ShowError("Vui lòng nhập số điện thoại người nhận hàng...");
            return false;
        }

       
        if (address.IsNullOrEmpty())
        {
            ucMessage_Bottom.ShowError("Vui lòng nhập đại chỉ người nhận hàng...");
            return false;
        }


        Cart cart = SessionUtility.Cart;
        cart.Email = email;
        cart.FullName = fullName;
        cart.Mobi = mobil;
        cart.Address = address;
        cart.PaymentMethod = paymentMethod;

        return true;
    }

    protected void RadioButton_AccountInfo_CheckedChanged(object sender, EventArgs e)
    {
        if (SessionUtility.Client != null)
        {
            LoadAccountInfo();
        }
        else
        {
            ucMessage_Top.ShowWarning("Để sử dụng chức năng này vui lòng<a href='/Account.aspx'  style='text-decoration:underline;'> đăng nhập</a>");
            ucMessage_Bottom.ShowWarning("Để sử dụng chức năng này vui lòng<a href='/Account.aspx'  style='text-decoration:underline;'> đăng nhập</a>");

            RadioButton_AccountInfo.Checked = false;
            RadioButton_NewInfo.Checked = true;
        }
    }

    protected void RadioButton_NewInfo_CheckedChanged(object sender, EventArgs e)
    {
        LoadNewInfo();
    }

    protected void Button_Checkout_Click(object sender, EventArgs e)
    {
        //Kiểm tra tính hợp lệ
        if (!IsValid())
        {
            return;
        }
        else
        {
            //Lấy giỏ hàng hiện tại
            Cart cart = SessionUtility.Cart;

            //Tạo 1 đơn hàng mới
            Order order = new Order();
            order.Total = cart.Amount;
            order.Email = cart.Email;
            order.FullName = cart.FullName;
            order.Mobi = cart.Mobi;
            order.Address = cart.Address;
            order.Gender = cart.Gender;
            order.PaymentMethod = cart.PaymentMethod;


            order.OrderStatus = true;//Trạng thái đặt hàng: rồi

            order.DeliverStatus = false;// Trạng thái giao hàng: chưa

            order.ChargeStatus = false;//Trạng thái trả tiền: chưa

            order.CreateTime = DateTime.Now;
            //Nếu khách đã đăng nhập, thì lưu mã khách hàng vào đơn hàng
            if (SessionUtility.Client != null)
            {
                order.ClientID = SessionUtility.Client.ClientID;
            }
            string imageList = string.Empty;
            //Thêm danh sách sản phẩm vào đơn hàng đó
            foreach (var item in cart.CartItems.Values)
            {
                imageList += item.Avatar + ";";

                OrderDetailList detail = new OrderDetailList();
                detail.ProductID = item.ID;
                detail.Quantity = item.Quantity;
                detail.Price = item.Price;

                order.OrderDetailLists.Add(detail);

            }
            //Xóa dấu ; cuối cùng của ImageList
            imageList = imageList.Remove(imageList.Length - 1);
            //Gán vào ImageList
            order.ImageList = imageList;
            //Lưu vào DB
            DBEntities db = new DBEntities();
            //Thêm đơn hàng mới(Bước trên) vào bảng trong DB
            db.Orders.Add(order);
            //Lưu thay đổi (lưu đơn hàng)
            db.SaveChanges();
            //Xóa giỏ hàng
            cart.CartItems.Clear();
            //Gửi 1 email xác nhận đơn hàng đã thanh toán dành cho mình tự làm
            string email = input_Email.Value.Trim();
            string fullName = input_FullName.Value.Trim();
            string address = textarea_Address.Value.Trim();



            //Chuyển đến trang thanh toán
            //Lấy url của website của mình
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

            //Kiểm tra phương thức thanh toán
            if (order.PaymentMethod == 0)
            {  
                //Thanh toán tại nhà
                string returnUrl = "{0}/CheckoutComplete.aspx?payment_type={1}&order_code={2}";
                returnUrl = returnUrl.StringFormat(baseUrl, order.PaymentMethod, order.OrderID);
                Response.Redirect(returnUrl);
                return;
            }
            else
            {
                //Thanh toán tại nganluong
                string returnUrl = "{0}/CheckoutComplete.aspx".StringFormat(baseUrl);
                string transaction_info = "{0}thanh toán đơn hàng".StringFormat(order.OrderID);
                string order_code = order.OrderID.ToString();
                string receiver = "huynhmopy@gmail.com";
                string price = order.Total.ToSafetyString(); //Giá demo tối thiểu
                NL_Checkout nl = new NL_Checkout();
                string url = nl.buildCheckoutUrl(returnUrl, receiver, transaction_info, order_code, price);
                Response.Redirect(url);
            }

        }
    }
}