<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" codefile="Contact.aspx.cs" inherits="Contact" %>

<%@ register src="~/ucControls/ucMessage.ascx" tagprefix="uc1" tagname="ucMessage" %>
<%@ register src="~/ucControls/ucContactForm.ascx" tagprefix="uc1" tagname="ucContactForm" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <!-- Breadcrumbs -->

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Liên hệ với chúng tôi</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="contact_area" style="margin-top: 50px">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 col-md-6">
                    <div class="contact_message content">
                        <h3>Liên hệ với chúng tôi</h3>
                        <p>Chúng tôi chuyên cung cấp các sản phẩm thực phẩm sạch an toàn cho sức khỏe con người</p>
                        <ul>
                            <li><i class="fa fa-fax"></i>Địa chỉ : Xuân Khanh, Sơn Tây, Hà Nội</li>
                            <li><i class="fa fa-envelope-o"></i><a href="#">huanbghd@gmail.com</a></li>
                            <li><i class="fa fa-phone"></i><a href="tel:0867506329">0867506329</a>  </li>
                        </ul>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6">
                    <div class="contact_message form">
                        <h3>Nhập thông tin liên hệ</h3>

                        <uc1:uccontactform runat="server" id="ucContactForm" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->


</asp:Content>

