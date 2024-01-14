<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMiniShoppingCart.ascx.cs" Inherits="ucMiniShoppingCart" %>

<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="header_account_list  mini_cart_wrapper">
            <a href="javascript:void(0)">
                <span class="lnr lnr-cart"></span>
                <span class="item_count" runat="server" id="b_Count">0</span>

            </a>
            <!--mini cart-->
            <div class="mini_cart">
                <div class="cart_gallery">
                    <div class="cart_close">
                        <div class="cart_text">
                            <h3>Giỏ hàng</h3>
                        </div>
                        <div class="mini_cart_close">
                            <a href="javascript:void(0)"><i class="icon-x"></i></a>
                        </div>
                    </div>


                    <asp:Repeater runat="server" ID="Repeater_Cart">
                        <ItemTemplate>
                            <div class="cart_item">
                                <div class="cart_img">
                                    <a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'>
                                        <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                </div>
                                <div class="cart_info">
                                    <a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title") %></a>
                                    <p><%# Eval("Quantity") %> x <span><%# Eval("Price","{0:0,00 đ}") %> </span></p>
                                </div>
                                <div class="cart_remove">
                                    <asp:LinkButton
                                        Text="text"
                                        runat="server"
                                        ID="LinkButton_Remove"
                                        OnClientClick="return confirm('Bạn muốn xóa sản phẩm này không ?')"
                                        OnClick="LinkButton_Remove_Click"
                                        CommandArgument='<%# Eval("ID") %>'>
                                                                      <i class="icon-x"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <div class="mini_cart_table">
                    <div class="cart_table_border">
                        <div class="cart_total">
                            <span>Số lượng:</span>
                            <span class="price" runat="server" id="b_Counts"></span>
                        </div>
                        <div class="cart_total mt-10">
                            <span>Tổng cộng:</span>
                            <span class="price" runat="server" id="b_Amount"></span>
                        </div>
                    </div>
                </div>
                <div class="mini_cart_footer">
                    <div class="cart_button">
                        <a runat="server" href="~/ProductShoppingCart.aspx"><i class="fa fa-shopping-cart"></i>Xem giỏ hàng</a>
                    </div>
                    <div class="cart_button">
                        <a runat="server" href="~/Checkout.aspx"><i class="fa fa-sign-in"></i>Thanh toán</a>
                    </div>

                </div>
            </div>
            <!--mini cart end-->
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
