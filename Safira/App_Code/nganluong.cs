using System.Web;
using System.Collections;

public class NL_Checkout
{

    private string nganluong_url = "https://www.nganluong.vn/checkout.php";
    //private string merchant_site_code = "66269"; //949821   //thay mã merchant site mà bạn đã đăng ký vào đây
    //private string secure_pass = "8e040a424a07abb53aff28a99ecce84d";  //thay mật khẩu giao tiếp giữa website của bạn với NganLuong.vn mà bạn đã đăng ký vào dây
    //private string nganluong_url = "https://sandbox.nganluong.vn:8088/nl30/checkout.php";


    // private string merchant_site_code = "66272"; //949821   //thay mã merchant site mà bạn đã đăng ký vào đây
    //private string secure_pass = "5886f10f2a1ad77d6e09904abe5dbed2";


    private string merchant_site_code = "39244"; //949821   //thay mã merchant site mà bạn đã đăng ký vào đây
    private string secure_pass = "654321";  //thay mật khẩu giao tiếp giữa website của bạn với NganLuong.vn mà bạn đã đăng ký vào dây



    //private string merchant_site_code = "66272"; //949821   //thay mã merchant site mà bạn đã đăng ký vào đây
     //string secure_pass = "aa5dbf9e701dfab55910a17a31423e07";  //thay mật khẩu giao tiếp giữa website của bạn với NganLuong.vn mà bạn đã đăng ký vào dây

    //public string nganluong_url = "https://www.nganluong.vn/checkout.php";
    //public string merchant_site_code = "51474"; //thay mã merchant site mà b?n dã dang ký vào dây
    //public string secure_pass = "9d5cd66d5337f1bacb256b653293129f";	//thay m?t kh?u giao ti?p gi?a website c?a b?n v?i NgânLu?ng.vn mà b?n dã dang ký vào dây


    //private string merchant_site_code = "66272";    //thay mã merchant site mà bạn đã đăng ký vào đây
    //private string secure_pass = "5886f10f2a1ad77d6e09904abe5dbed2";
    public string GetMD5Hash(string input)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

        byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

        bs = x.ComputeHash(bs);

        System.Text.StringBuilder s = new System.Text.StringBuilder();

        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }

        string md5string = s.ToString();

        return md5string;
    }

    public string buildCheckoutUrl(string return_url, string receiver, string transaction_info, string order_code, string price)
    {
        string secure_code = "";
        secure_code += this.merchant_site_code;
        secure_code += " " + HttpUtility.UrlEncode(return_url).ToLower();
        secure_code += " " + receiver;
        secure_code += " " + transaction_info;
        secure_code += " " + order_code;
        secure_code += " " + price;
        secure_code += " " + this.secure_pass;

        Hashtable ht = new Hashtable();
        ht.Add("merchant_site_code", this.merchant_site_code);
        ht.Add("return_url", HttpUtility.UrlEncode(return_url).ToLower());
        ht.Add("receiver", receiver);
        ht.Add("transaction_info", transaction_info);
        ht.Add("order_code", order_code);
        ht.Add("price", price);
        ht.Add("secure_code", this.GetMD5Hash(secure_code));

        string redirect_url = this.nganluong_url;

        if (redirect_url.IndexOf("?") == -1)
        {
            redirect_url += "?";
        }
        else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1)
        {
            redirect_url += "&";
        }

        string url = "";

        IDictionaryEnumerator en = ht.GetEnumerator();

        while (en.MoveNext())
        {
            if (url == "")
                url += en.Key.ToString() + "=" + en.Value.ToString();
            else
                url += "&" + en.Key.ToString() + "=" + en.Value.ToString();
        }

        string rdu = redirect_url + url;

        return rdu;
    }

    public bool verifyPaymentUrl(string transaction_info, string order_code, string price, string payment_id, string payment_type, string error_text, string secure_code)
    {
        string str = "";
        str += " " + transaction_info;
        str += " " + order_code;
        str += " " + price;
        str += " " + payment_id;
        str += " " + payment_type;
        str += " " + error_text;
        str += " " + this.merchant_site_code;
        str += " " + this.secure_pass;

        string verify_secure_code = "";
        verify_secure_code = this.GetMD5Hash(str);
        if (verify_secure_code == secure_code)
            return true;
        else
            return false;
    }

}