<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" codefile="Default.aspx.cs" inherits="_Default" %>

<%@ register src="~/ucControls/ucHomeTab.ascx" tagprefix="uc1" tagname="ucHomeTab" %>
<%@ register src="~/ucControls/ucHomeArticle.ascx" tagprefix="uc1" tagname="ucHomeArticle" %>
<%@ register src="~/ucControls/ucSpecialProduct.ascx" tagprefix="uc1" tagname="ucSpecialProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section class="slider_section slider_s_three">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="slider_area owl-carousel">
                        <div class="single_slider d-flex align-items-center" data-bgimg="assets/img/slider/slider1.jpg">
                            <div class="slider_content slider_c_three">
                                <h1 style="color : white">Sản phẩm đến từ thiên nhiên</h1>
                                <p style="color : white">
                                    Bảo vệ sức khỏe - Cuộc sống tươi đẹp
                                </p>
                                <a href="~/" >Xem thêm </a>
                            </div>
                        </div>
                        <div class="single_slider d-flex align-items-center" data-bgimg="assets/img/slider/slider9.jpg">
                            <div class="slider_content slider_c_three">
                                <h1>Sản phẩm đến từ thiên nhiên</h1>
                                <p>
                                    Bảo vệ sức khỏe - Cuộc sống tươi đẹp
                                </p>
                                <a href="~/">Xem thêm </a>
                            </div>
                        </div>
                        <div class="single_slider d-flex align-items-center" data-bgimg="assets/img/slider/slider2.jpg">
                            <div class="slider_content slider_c_three">
                                <h1>Sản phẩm đến từ thiên nhiên</h1>
                                <p>
                                    Bảo vệ sức khỏe - Cuộc sống tươi đẹp
                                </p>
                                <a href="~/">Xem thêm </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <!--shipping area start-->
   <%-- <div class="shipping_area">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping5.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Miễn phí vận chuyển</h3>
                            <p>Miễn phí vận chuyển khi đơn hàng của bạn trên 200.000 VNĐ</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_2">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping6.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Hổ trợ 24/7</h3>
                            <p>Liên hệ với chúng tôi 24 giờ một ngày, 7 ngày một tuần</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_3">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping7.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>30 ngày đổi trả</h3>
                            <p>Chỉ cần trả lại nó trong vòng 30 ngày để đổi sản phẩm mới</p>

                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_4">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping8.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Thanh toán an toàn</h3>
                            <p>Chúng tôi đảm bảo thanh toán an toàn với PEV</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <!--shipping area end-->
        <%-- ucSpecialProduct --%>
    <div style="margin-top: 45px"></div>
    <uc1:ucspecialproduct runat="server" id="ucSpecialProduct" />

    <!--banner fullwidth area satrt-->
    <div class="banner_fullwidth">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="banner_full_content">
                        <p>Black Fridays !</p>
                        <h2>Giảm đến 50% <span>cho tất cả sản phẩm</span></h2>
                        <a runat="server" href="~/ProductGrid.aspx">Mua sắm ngay</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--banner fullwidth area end-->

    <!--product area start-->
    <uc1:uchometab runat="server" id="ucHomeTab" />
    <!--product area end-->


    <!--banner area start-->
    <div class="banner_area">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 col-md-6">
                    <div class="single_banner">
                        <div class="banner_thumb">
                            <a href="ProductGrid.aspx?mid=9">
                                <img src="assets/img/bg/banner1.jpg" alt=""></a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6">
                    <div class="single_banner">
                        <div class="banner_thumb">
                            <a href="ProductGrid.aspx?mid=10">
                                <img src="assets/img/bg/banner2.jpg" alt=""></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--banner area end-->

    <uc1:uchomearticle runat="server" id="ucHomeArticle" />

  <!--shipping area start-->
    <div class="shipping_area">
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping5.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Miễn phí vận chuyển</h3>
                            <p>Miễn phí vận chuyển khi đơn hàng của bạn trên 200.000 VNĐ</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_2">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping6.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Hổ trợ 24/7</h3>
                            <p>Liên hệ với chúng tôi 24 giờ một ngày, 7 ngày một tuần</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_3">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping7.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>30 ngày đổi trả</h3>
                            <p>Chỉ cần trả lại nó trong vòng 30 ngày để đổi sản phẩm mới</p>

                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="single_shipping col_4">
                        <div class="shipping_icone">
                            <img src="assets/img/about/shipping8.jpg" alt="">
                        </div>
                        <div class="shipping_content">
                            <h3>Thanh toán an toàn</h3>
                            <p>Chúng tôi đảm bảo thanh toán an toàn với PEV</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--shipping area end-->
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJS" runat="Server">
    <script>
        $(function () {
            $(".categories_menu .categories_title").click();
        })
    </script>
</asp:Content>

