<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProductGrid.aspx.cs" Inherits="ProductGrid" %>

<%@ Register Src="~/ucControls/ucPagging.ascx" TagPrefix="uc1" TagName="ucPagging" %>
<%@ Register Src="~/ucControls/ucSpecialProduct.ascx" TagPrefix="uc1" TagName="ucSpecialProduct" %>
<%@ Register Src="~/ucControls/ucMyCart.ascx" TagPrefix="uc1" TagName="ucMyCart" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Danh sách sản phẩm</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->
    <div class="shop_area shop_reverse mt-70 mb-70">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-12">
                    <!--sidebar widget start-->
                    <aside class="sidebar_widget">
                        <div class="widget_inner">

                            <div class="widget_list widget_color">
                                <h3>Chọn danh mục sản phẩm</h3>
                                <ul>
                                    <asp:Repeater runat="server" ID="Repeater_Category">
                                        <ItemTemplate>
                                            <li>
                                                <a runat="server" href='<%# Eval("ID","~/ProductGrid.aspx?mid={0}") %>'><%# Eval("Title") %>  <span>(<%# Eval("QuantityProduct") %>)</span></a>
                                            </li>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </ul>
                            </div>

                            <div class="widget_list tags_widget">
                                <h3>Thẻ Tags</h3>
                                <div class="tag_cloud">
                                    <asp:Repeater runat="server" ID="Repeater_Tag">
                                        <ItemTemplate>
                                            <a runat="server" href='<%# Eval("ID","~/ProductGrid.aspx?mid={0}") %>'><%# Eval("Title") %>  </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="widget_list banner_widget">
                                <div class="banner_thumb">
                                    <a href="#">
                                        <img src="assets/img/bg/banner17.jpg" alt=""></a>
                                </div>
                            </div>
                        </div>
                    </aside>
                    <!--sidebar widget end-->
                </div>
                <div class="col-lg-9 col-md-12">
                    <!--shop wrapper start-->
                    <!--shop toolbar start-->
                    <div class="shop_toolbar_wrapper">
                        <div class="shop_toolbar_btn">

                            <button data-role="grid_3" type="button" class="active btn-grid-3" data-toggle="tooltip" title="3"></button>

                            <button data-role="grid_4" type="button" class=" btn-grid-4" data-toggle="tooltip" title="4"></button>
                        </div>
                        <div class="page_amount">
                            <p>Danh Sách <span runat="server" id="span_Categories"></span></p>
                        </div>
                        <div class=" niceselect_option">
                            <asp:DropDownList runat="server"
                                class="form-control"
                                ID="DropDownList_Filter"
                                AutoPostBack="true">
                                <asp:ListItem Text="Giá tăng dần" Value="1" />
                                <asp:ListItem Text="Giá giảm dần" Value="2" />
                                <asp:ListItem Text="Giá dưới 1 triệu" Value="3" />
                                <asp:ListItem Text="Giá từ 1 -> 3 triệu" Value="4" />
                                <asp:ListItem Text="Giá từ 3 -> 7 triệu" Value="5" />
                                <asp:ListItem Text="Giá > 7 triệu" Value="6" />
                            </asp:DropDownList>
                        </div>

                    </div>
                    <!--shop toolbar end-->
                    <div class="row shop_wrapper">
                        <asp:Repeater runat="server" ID="Repeater_Product">
                            <ItemTemplate>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-12 ">
                                    <div class="single_product">
                                        <div class="product_thumb">
                                            <a class="primary_img" runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'>
                                                <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>

                                            <div class="label_product">
                                                <span class="label_new">New</span>
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
                                                    <li class="quick_button"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>' data-tippy="Xem chi tiết" data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true"><span class="lnr lnr-magnifier"></span></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="product_content grid_content">
                                            <h4 class="product_name"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title").Left(50,true,true) %></a></h4>
                                            <div class="price_box">
                                                <span class="current_price"><%# Eval("Price").ToString("0,00 VNĐ") %></span>
                                                <span class="old_price"><%# Eval("OldPrice").ToString("0,00 VNĐ") %></span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="shop_toolbar t_bottom">
                        <uc1:ucPagging runat="server" ID="ucPagging" />
                        <%--<div class="pagination">
                            <ul>
                                <li class="current">1</li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li class="next"><a href="#">next</a></li>
                                <li><a href="#">>></a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <!--shop toolbar end-->
                    <!--shop wrapper end-->
                </div>
            </div>
        </div>
    </div>



    <!-- Main Container -->

    <!-- Main Container End -->

</asp:Content>

