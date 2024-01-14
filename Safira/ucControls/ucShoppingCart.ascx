<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucShoppingCart.ascx.cs" Inherits="ucShoppingCart" %>


<div class="shopping_cart_area mt-70">
    <div class="container">
        <form action="#">
            <div class="row">
                <div class="col-12">
                    <div class="table_desc">
                        <div class="cart_page">
                            <table>
                                <thead>
                                    <tr>
                                        <th class="product_thumb">Hình ảnh</th>
                                        <th class="product_name">Tên sản phẩm</th>
                                        <th class="product-price">Giá</th>
                                        <th class="product_quantity">Số lượng</th>
                                        <th class="product_total">Tổng cộng</th>
                                        <th class="product_remove">Xóa</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="Repeater_Main">
                                        <ItemTemplate>
                                            <tr>

                                                <td class="product_thumb">
                                                    <a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'>
                                                        <img runat="server" src='<%# Eval("Avatar") %>' /></a></td>
                                                <td class="product_name"><a  runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title") %></a></td>
                                                <td class="product-price"><%# Eval("Price","{0:0,00 đ}") %></td>
                                                <td class="product_quantity">
                                                    <label>Số lượng</label>
                                                     <div class="input-group">
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton
                                                                runat="server"
                                                                ID="LinkButton_Decrease"
                                                                OnClick="LinkButton_Decrease_Click"
                                                                CommandArgument='<%# Eval("ID") %>'
                                                                ClientIDMode="AutoID"
                                                                 class="btn btn-danger">
                                                              <i class="fa fa-minus"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                        <input type="text" class="form-control input-number"  readonly="readonly" value='<%# Eval("Quantity") %>'>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton
                                                                runat="server"
                                                                ID="LinkButton_Increase"
                                                                OnClick="LinkButton_Increase_Click"
                                                                CommandArgument='<%# Eval("ID") %>'
                                                                ClientIDMode="AutoID"
                                                                class="quantity-right-plus btn btn-success btn-number">
                                                              <i class="fa fa-plus"></i>
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                <td class="product_total"><%# Eval("TotalPrice","{0:0,00 đ}") %></td>
                                                <td class="product_remove">
                                                    <asp:LinkButton runat="server"
                                                        ID="LinkButton_Remove"
                                                        OnClick="LinkButton_Remove_Click"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        OnClientClick="return confirm('Bạn có muốn gỡ loại sản phẩm này không')">
                                                       <i class="fa fa-trash-o"></i>
                                                    </asp:LinkButton></td>

                                            </tr>
                                           
                                        </ItemTemplate>
                                    </asp:Repeater>



                                </tbody>
                            </table>
                        </div>
                        <div class="cart_submit">
                         
                        </div>
                    </div>
                </div>
            </div>
            <!--coupon code area start-->
            <div class="coupon_area">
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="coupon_code left">
                            <h3>Mã giảm giá</h3>
                            <div class="coupon_inner">
                                <p>Nhập mã giảm giá.</p>
                                <input placeholder="Coupon code" type="text">
                                <button type="submit">Áp dụng </button>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="coupon_code right">
                            <h3>Tổng cộng</h3>
                            <div class="coupon_inner">
                                <div class="cart_subtotal">
                                    <p>Tạm tính</p>
                                    <p class="cart_amount" runat="server" id="span_Amounts" ></p>
                                </div>
                                <div class="cart_subtotal ">
                                    <p>Giảm giá</p>
                                    <p class="cart_amount">0 VNĐ</p>
                                </div>
                                <a href="#">Tổng thanh toán</a>

                                <div class="cart_subtotal">
                                    <p>Thành tiền</p>
                                    <p class="cart_amount"  runat="server" id="span_Amount" ></p>
                                </div>
                                <div class="checkout_btn">
                                     <asp:LinkButton Text="Lập đơn hàng"
                                        runat="server"
                                        class="btn btn-success"
                                        ID="LinkButton_Checkout"
                                        OnClientClick="return checkoutButtonClick();"
                                        OnClick="LinkButton_Checkout_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--coupon code area end-->
        </form>
    </div>
</div>


