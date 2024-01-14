<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<%@ Register Src="~/ucControls/ucShoppingCart.ascx" TagPrefix="uc1" TagName="ucShoppingCart" %>
<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function checkoutButtonClick() {
            $(".checkout-button").click();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Thông tin thanh toán</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="Checkout_section mt-70">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <uc1:ucMessage runat="server" ID="ucMessage_Top" />
                    <div class="user-actions">
                        <label style="margin: 20px 100px 50px 0;">Thông tin khách hàng:</label>
                        <asp:RadioButton Text=" Sử dụng thông tin tài khoản"
                            runat="server"
                            GroupName="info" AutoPostBack="true"
                            ID="RadioButton_AccountInfo"
                            OnCheckedChanged="RadioButton_AccountInfo_CheckedChanged" />

                        &nbsp; &nbsp;
                                    <asp:RadioButton Text=" Nhập thông tin mới"
                                        runat="server"
                                        GroupName="info" AutoPostBack="true"
                                        ID="RadioButton_NewInfo"
                                        OnCheckedChanged="RadioButton_NewInfo_CheckedChanged" />
                    </div>

                </div>
            </div>
            <div class="checkout_form">
                <div class="row">
                    <div class="col-lg-6 col-md-6">

                        <h3>Thông tin thanh toán</h3>
                        <div class="row">

                            <div class="col-lg-6 mb-20">
                                <label>Họ và tên <span>*</span></label>
                                <input type="text" runat="server" id="input_FullName">
                            </div>
                            <div class="col-lg-6 mb-20">
                                <label>Email </label>
                                <input type="text" runat="server" id="input_Email">
                            </div>
                            <div class="col-12 mb-20">
                                <label>Số điện thoại <span>*</span></label>
                                <input type="text" runat="server" id="input_Phone">
                            </div>
                            <div class="col-12 mb-20">
                                <label>Địa chỉ <span>*</span></label>
                                <input type="text" runat="server" id="textarea_Address">
                            </div>

                        </div>

                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div>
                            <h3>Đơn hàng</h3>
                            <div class="order_table table-responsive">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Sản phẩm</th>
                                            <th>Tổng cộng</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="Repeater_Product">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("Title") %> <strong>× <%# Eval("Quantity") %></strong></td>
                                                    <td><%# Eval("TotalPrice","{0:0,00 đ}") %></td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>

                                        <tr class="order_total">
                                            <th>Thành tiền</th>
                                            <td><strong runat="server" id="span_Amount"></strong></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                            <div class="payment_method">
                                <div class="panel-default">
                                    <input type="radio" runat="server" id="input_checkout_offline" name="checkout-option"  checked="true" />

                                    <a href="#method" data-bs-toggle="collapse" aria-controls="method">&nbsp;Thanh toán tại nhà</a>
                                    <div id="method" class="collapse one" data-parent="#accordion">
                                        <div class="card-body1">
                                            <p>Nhận hàng kiểm tra và thanh toán tiền cho shipper</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-default">
                                    <input type="radio" runat="server" id="Radio1" name="checkout-option"  checked="true" />

                                    <a href="#test" data-bs-toggle="collapse" aria-controls="method">&nbsp;Chuyển khoản trực tiếp và sẽ báo cho nhân viên cửa hàng</a>
                                    <div id="test" class="collapse one" data-parent="#accordion">
                                        <div class="card-body1">
                                            <img src="assets/qr.jpg" />
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-default">
                                    <input type="radio" runat="server" id="input_checkout_online" name="checkout-option" />

                                    <a href="#method1" data-bs-toggle="collapse" aria-controls="method">&nbsp;Thanh toán qua ngân lượng</a>
                                    <div id="method1" class="collapse one" data-parent="#accordion">
                                        <div class="card-body1">
                                            <p>Thanh toán Online</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="order_button" style="margin-top: 15px">
                                    <asp:Button Text="Đặt hàng ngay"
                                        Style="color: #fff; background-color: #198754; border-color: #198754;"
                                        runat="server"
                                        class="btn btn-success"
                                        ID="LinkButton_Checkout"
                                        OnClick="Button_Checkout_Click" />
                                </div>
                            </div>
                            <p>
                                <uc1:ucMessage runat="server" ID="ucMessage_Bottom" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

