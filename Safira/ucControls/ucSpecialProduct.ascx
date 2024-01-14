<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSpecialProduct.ascx.cs" Inherits="ucSpecialProduct" %>


<div class="product_area product_deals mb-65">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="section_title">
                    <p>Giảm giá sốc </p>
                    <h2>Ưu đãi trong tuần</h2>
                </div>
            </div>
        </div>
        <div class="product_container">
            <div class="row">
                <div class="col-12">
                    <div class="product_carousel product_column5 owl-carousel">
                        <asp:Repeater runat="server" ID="Repeater_SpecialProduct">
                            <ItemTemplate>
                                <article class="single_product">
                                    <figure>
                                        <div class="product_thumb product_thumb_cs">
                                            <a class="primary_img" runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'>
                                                <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                            <div class="label_product">
                                                <span class="label_sale">Sale</span>
                                                <span class="label_new">New</span>
                                            </div>
                                            <div class="product_timing">
                                                <div data-countdown="2022/12/15"></div>
                                            </div>
                                            <div class="action_links">
                                                <ul>
                                                    <li class="add_to_cart">
                                                        <asp:LinkButton
                                                            Text="Mua ngay"
                                                            runat="server"
                                                            class="action add-to-cart"
                                                            data-tippy="Thêm vào giỏ hàng"
                                                            data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true"
                                                            ID="Button_AddCart"
                                                            CommandArgument='<%# Eval("ID") %>'
                                                            ClientIDMode="AutoID"
                                                            OnClientClick="alert('Thêm giỏ hàng thành công')"
                                                            OnClick="Button_AddCart_Click">
                                                            <span class="lnr lnr-cart"></span>
                                                        </asp:LinkButton>

                                                    </li>
                                                    <li class="quick_button"><a  runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>' data-tippy="Xem chi tiết" data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true"><span class="lnr lnr-magnifier"></span></a></li>

                                                </ul>
                                            </div>
                                        </div>
                                        <figcaption class="product_content">
                                            <h4 class="product_name"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title").Left(50,true,true) %></a></h4>
                                            <div class="price_box">
                                                <span class="current_price"><%# Eval("Price").ToString("0,00 VNĐ") %></span>
                                                <span class="old_price"><%# Eval("OldPrice").ToString("0,00 VNĐ") %></span>
                                            </div>
                                        </figcaption>
                                    </figure>
                                </article>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


