<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuArea.ascx.cs" Inherits="ucMenuArea" %>
<%@ Register Src="~/ucControls/ucArticleMenu.ascx" TagPrefix="uc1" TagName="ucArticleMenu" %>
<%@ Register Src="~/ucControls/ucLoginMenu.ascx" TagPrefix="uc1" TagName="ucLoginMenu" %>
<%@ Register Src="~/ucControls/ucMiniShoppingCart.ascx" TagPrefix="uc1" TagName="ucMiniShoppingCart" %>



<script>
    function activeProductMainMenu(mid) {
        $("li[data-mid=" + mid + "]").attr("class", "active");
    }
</script>
<!-- Start Menu Area -->
    <header>
    <div class="main_header header_three color_three">
        <div class="header_top">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div class="header_social">
                            
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="language_currency text-right">
                            <ul>
                                
                               
                            </ul>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="header_middle">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-2 col-md-3 col-sm-3 col-3">
                        <div class="logo">
                            <a runat="server" href="~/">
                                <img src="assets/img/logo/logo.png" alt=""></a>
                        </div>
                    </div>
                    <div class="col-lg-10 col-md-6 col-sm-7 col-8">
                        <div class="header_right_info">
                            <div class="search_container mobail_s_none flex-grow-1" style="padding-left: 72px; padding-right: 40px;">
                                <asp:Panel runat="server" DefaultButton="LinkButton_Search" class="input-group hover_category">
                                    <%--<asp:DropDownList runat="server" ID="DropDownList_Search" class="cate-dropdown hidden-xs hidden-sm select_option">
                                        <asp:ListItem Value="search-product" Text="Sản phẩm" />
                                        <asp:ListItem Value="search-article" Text="Tin tức" />
                                    </asp:DropDownList>--%>
                                    <input type="text" runat="server" id="input_Search" class="form-control search-query" placeholder="Bạn muốn mua gì?" name="search">

                                    <asp:LinkButton runat="server" class="btn btn-main btn-search" ID="LinkButton_Search" OnClick="LinkButton_Search_Click">
                                    <i class="lnr lnr-magnifier"></i>
                                    </asp:LinkButton>
                                </asp:Panel>

                            </div>
                            <div class="header_account_area">
                                <div class="header_account_list register">
                                    <ul runat="server" id="a_Login">
                                        <li><a runat="server" href="~/Account.aspx">Đăng ký</a></li>
                                        <li><span>/</span></li>
                                        <li><a runat="server" href="~/Account.aspx">Đăng nhập</a></li>
                                    </ul>
                                    <ul runat="server" id="a_wellcome">
                                        <li><a runat="server" href="~/ProductShoppingCart.aspx">Xin chào ! <span runat="server" id="span_FullName"></span></a></li>
                                        <li runat="server" id="div_Logout" class="li_logout">
                                            <asp:LinkButton Text="text"  data-tippy="Đăng xuất"
                                                            data-tippy-placement="top" data-tippy-arrow="true" data-tippy-inertia="true" class="btn btn-sm btn-light" runat="server" ID="LinkButton_Logout" OnClick="LinkButton_Logout_Click">
                                                 <i class="ion-log-out"></i> 
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>


                                <%-- Shopping cart --%>
                                <uc1:ucminishoppingcart runat="server" id="ucMiniShoppingCart" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="header_bottom">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-12 col-md-6 mobail_s_block">
                        <div class="search_container">

                            <div class="hover_category">
                                <select class="select_option" name="select" id="categori2">
                                    <option selected value="1">Select a categories</option>
                                    <option value="2">Accessories</option>
                                    <option value="3">Accessories & More</option>
                                    <option value="4">Butters & Eggs</option>
                                    <option value="5">Camera & Video </option>
                                    <option value="6">Mornitors</option>
                                    <option value="7">Tablets</option>
                                    <option value="8">Laptops</option>
                                    <option value="9">Handbags</option>
                                    <option value="10">Headphone & Speaker</option>
                                    <option value="11">Herbs & botanicals</option>
                                    <option value="12">Vegetables</option>
                                    <option value="13">Shop</option>
                                    <option value="14">Laptops & Desktops</option>
                                    <option value="15">Watchs</option>
                                    <option value="16">Electronic</option>
                                </select>
                            </div>
                            <div class="search_box">
                                <input placeholder="Search product..." type="text">
                                <button type="submit"><span class="lnr lnr-magnifier"></span></button>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-3">
                       
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-7 col-8">

                        <!--main menu start-->
                        <div class="main_menu menu_position">
                            <nav>
                                <ul>
                                    <li>
                                        <a class="active" runat="server" href="~/">Trang chủ</a>

                                    </li>

                                    <li>
                                        <a runat="server" href="~/ProductGrid.aspx">Sản phẩm<i class="fa fa-angle-down"></i></a>
                                        <ul class="sub_menu pages">
                                            <asp:Repeater runat="server" ID="Repeater_MenuArea">
                                                <ItemTemplate>
                                                    <li><a runat="server" href='<%# Eval("ID","~/ProductGrid.aspx?mid={0}") %>'><%# Eval("Title") %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </ul>
                                    </li>
                                    <li>
                                        <a runat="server" href="~/ArticleList.aspx">Tin tức<i class="fa fa-angle-down"></i></a>
                                        <ul class="sub_menu pages">
                                            <asp:Repeater runat="server" ID="Repeater_MenuArticle">
                                                <ItemTemplate>
                                                    <li><a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><%# Eval("Title") %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </ul>
                                    </li>
                                    <li>
                                        <a runat="server" href="~/AboutUs.aspx">Giới thiệu</a>

                                    </li>
                                    <li><a runat="server" href="~/Contact.aspx">Liên hệ</a></li>
                                </ul>
                            </nav>
                        </div>
                        <!--main menu end-->
                    </div>
                    <div class="col-lg-3 ">
                        <div class="call-support ">
                            <p><a href="tel: 0867506329"> 0867506329</a>Hổ trợ khách hàng</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</header>



