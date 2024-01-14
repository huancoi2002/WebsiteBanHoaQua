<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMyCart.ascx.cs" Inherits="ucMyCart" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="block sidebar-cart">
            <div class="sidebar-bar-title">
                <h3>Giỏ hàng của tôi</h3>
            </div>
            <div class="block-content">
                <p class="amount">Bạn đã có <a href="shopping_cart.html"><b runat="server" id="b_Count" style="color:red"></b>&nbsp;sản phẩm</a>trong giỏ hàng.</p>
                <ul>
                    <asp:Repeater runat="server" ID="Repeater_Cart">
                        <ItemTemplate>
                            <li class="item row">
                                <a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>' title="Sample Product" class="col-xs-3 product-image" style='padding: 0;
    margin-left: 14px;'>
                                <img src='<%# Eval("Avatar") %>' alt="Sample Product ">
                                </a>
                                <div class="product-details col-xs-7">
                                    <div class="access">
                                        <asp:LinkButton
                                            Text="text"
                                            runat="server"
                                            ID="LinkButton_Remove"
                                            class="remove-cart"
                                            OnClientClick="return confirm('Bạn muốn xóa sản phẩm này không ?')"
                                            OnClick="LinkButton_Remove_Click"
                                            CommandArgument='<%# Eval("ID") %>'>
                                        <i class="icon-close"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <p class="product-name"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title") %></a> </p>
                                    <strong><%# Eval("Quantity") %></strong> x <span class="price"><%# Eval("Price","{0:0,00 đ}") %></span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="summary">
                    <p class="subtotal"><span class="label">Tổng tiền:</span> <span runat="server" id="span_Amount" class="price">$27.99</span> </p>
                </div>
                <div class="cart-checkout">
                    <a runat="server" href="~/ProductShoppingCart.aspx" class="button button-checkout" title="Submit" type="submit"><i class="fa fa-check"></i><span>Thanh toán</span></a>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
