<%@ control language="C#" autoeventwireup="true" codefile="ucHomeTab.ascx.cs" inherits="ucHomeTab" %>

<div class="product_area  mb-64">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="product_header">
                    <div class="section_title">
                        <br />
                        <h2>Sản phẩm mới nhất</h2>
                    </div>
                    <div class="product_tab_btn">
                        <ul class="nav" role="tablist" id="nav-tab">
                            <asp:repeater runat="server" id="Repeater_Main">
                                <itemtemplate>
                                    <li><a href='#tab<%# Eval("ID") %>' data-toggle="tab" aria-expanded="false" class=" <%# Container.ItemIndex ==0 ?"active":"" %>"><%# Eval("Title") %></a> </li>
                                </itemtemplate>
                            </asp:repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="product_container">
            <div class="row">
                <div class="col-12">
                    <div class="tab-content">

                        <asp:repeater runat="server" id="Repeater_TabContent">
                            <itemtemplate>
                                <div id='tab<%# Eval("ID") %>' role="tabpanel" class="tab-pane fade show <%# Container.ItemIndex ==0 ?"active":"" %>">
                                    <div class="product_carousel product_column5 owl-carousel">
                                        <asp:repeater runat="server" id="Repeater_ProductTab" datasource='<%# Eval("ProductTab") %>'>
                                            <itemtemplate>
                                                <div class="product_items">
                                                    <article class="single_product">
                                                        <figure>
                                                            <div class="product_thumb product_thumb_cs">
                                                                <a class="primary_img" runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'>
                                                                    <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>

                                                                <div class="label_product">
                                                                    <span class="label_new">New</span>
                                                                </div>
                                                                <div class="action_links">
                                                                    <ul>
                                                                        <li class="add_to_cart">
                                                                            <asp:linkbutton
                                                                                text="Mua ngay"
                                                                                runat="server"
                                                                                class="action add-to-cart"
                                                                                data-tippy="Thêm vào giỏ hàng"
                                                                                data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true"
                                                                                id="Button_AddCart"
                                                                                commandargument='<%# Eval("ID") %>'
                                                                                clientidmode="AutoID"
                                                                                onclientclick="alert('Thêm giỏ hàng thành công')"
                                                                                onclick="Button_AddCart_Click">
                                                                                <span class="lnr lnr-cart"></span>
                                                                            </asp:linkbutton>

                                                                        </li>
                                                                        <li class="quick_button"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>' data-tippy="Xem chi tiết" data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true"><span class="lnr lnr-magnifier"></span></a></li>
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
                                                </div>
                                            </itemtemplate>
                                        </asp:repeater>
                                    </div>
                                </div>

                            </itemtemplate>
                        </asp:repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


