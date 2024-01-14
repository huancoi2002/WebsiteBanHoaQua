<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="about_us" %>

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
                            <li>Về chúng tôi</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <!-- Main Container -->

    <!--about section area -->
    <section class="about_section">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <figure>
                        <figcaption class="about_content">
                            <h1>Luôn tươi mới - Tốt cho sức khỏe - Nguồn gốc rõ ràng</h1>
                            <p>
                                Thực Phẩm DUALEO-X là một hệ thống cung cấp thực phẩm sạch, tất cả các nguyên liệu thực phẩm đều được nhập trực tiếp từ các vựa nông sản và nhà vườn chính gốc.
Chuyên cung cấp các loại thực phẩm tươi sống, thực phẩm nông sản, thực phẩm khô, trái cây, rau củ quả cho các nhà hàng, quán ăn, trường mầm non...
                            </p>
                            <div class="about_signature">
                                <img src="assets/img/about/about-us-signature.png" alt="">
                            </div>
                            <div>
                                <img  src="assets/anh3.jpg"/>
                            </div>
                            <div>
                                <img  src="assets/anh4.jpg"/>
                            </div>
                        </figcaption>
                    </figure>
                </div>
            </div>
        </div>
    </section>
    <!--about section end-->
    <!--chose us area start-->
    <div class="choseus_area" data-bgimg="assets/img/about/about-us-policy-bg.jpg">
        <div class="container">
            <div class="row">
                <div class="col-lg-4 col-md-4">
                    <div class="single_chose">
                        <div class="chose_icone">
                            <img src="assets/img/about/About_icon1.png" alt="">
                        </div>
                        <div class="chose_content">
                            <h3>Luôn tươi mới !</h3>
                            <p>Thực phẩm luôn tươi mới 100% !</p>

                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4">
                    <div class="single_chose">
                        <div class="chose_icone">
                            <img src="assets/img/about/About_icon2.png" alt="">
                        </div>
                        <div class="chose_content">
                            <h3>Tốt Cho Sức Khoẻ !</h3>
                            <p>Nguồn thực phẩm sạch an toàn cho sức khoẻ !</p>

                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4">
                    <div class="single_chose">
                        <div class="chose_icone">
                            <img src="assets/img/about/About_icon3.png" alt="">
                        </div>
                        <div class="chose_content">
                            <h3>Nguồn Gốc Sản Phẩm Rõ Ràng</h3>
                            <p>Các mặt hàng thực phẩm tại Chợ Sớm đều được lấy từ nhà vườn !</p>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--testimonial area start-->
    <div class="faq-client-say-area">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 col-md-6">
                    <div class="faq-client_title">
                        <h2>Chúng tôi có thể giúp gì cho bạn ?</h2>
                    </div>
                    <div class="faq-style-wrap" id="faq-five">
                        <!-- Panel-default -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5 class="panel-title">
                                    <a id="octagon" class="collapsed" role="button" data-bs-toggle="collapse" href="#faq-collapse1" aria-expanded="true" aria-controls="faq-collapse1"><span class="button-faq"></span>
                                        Giao hàng miễn phí nhanh chóng</a>
                                </h5>
                            </div>
                            <div id="faq-collapse1" class="collapse show" aria-expanded="true" role="tabpanel" data-parent="#faq-five">
                                <div class="panel-body">
                                    <p>
                                        MIỄN PHÍ GIAO
cho 5 đơn hàng đầu tiên từ 100.000đ
(Với các khách hàng lần đầu mua hàng tại Safira kể từ ngày 7/5/2022 sẽ áp dụng chính sách mới: Miễn phí giao hàng cho 5 đơn hàng đầu tiên từ 100.000đ tối đa 15.000đ)
                                    </p>
                                     <%--  --%> 
                                </div>
                            </div>
                        </div>
                        <!--// Panel-default -->

                        <!-- Panel-default -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5 class="panel-title">
                                    <a class="collapsed" role="button" data-bs-toggle="collapse" href="#faq-collapse2" aria-expanded="false" aria-controls="faq-collapse2"><span class="button-faq"></span>More Than 30 Years In The Business</a>
                                </h5>
                            </div>
                            <div id="faq-collapse2" class="collapse" aria-expanded="false" role="tabpanel" data-parent="#faq-five">
                                <div class="panel-body">
                                    <p>Nam liber tempor cum soluta nobis eleifend option.</p>
                                    <p>
                                        Congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me.
                                    </p>
                                    <p>Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum.</p>
                                </div>
                            </div>
                        </div>
                        <!--// Panel-default -->

                        <!-- Panel-default -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5 class="panel-title">
                                    <a class="collapsed" role="button" data-bs-toggle="collapse" href="#faq-collapse3" aria-expanded="false" aria-controls="faq-collapse3"><span class="button-faq"></span>100% Organic Foods</a>
                                </h5>
                            </div>
                            <div id="faq-collapse3" class="collapse" role="tabpanel" data-parent="#faq-five">
                                <div class="panel-body">
                                    <p>Nam liber tempor cum soluta nobis eleifend option.</p>
                                    <p>
                                        Congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me.
                                    </p>
                                    <p>Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum.</p>
                                </div>
                            </div>
                        </div>
                        <!--// Panel-default -->

                        <!-- Panel-default -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5 class="panel-title">
                                    <a class="collapsed" role="button" data-bs-toggle="collapse" href="#faq-collapse4" aria-expanded="false" aria-controls="faq-collapse4"><span class="button-faq"></span>Best Shopping Strategies</a>
                                </h5>
                            </div>
                            <div id="faq-collapse4" class="collapse" role="tabpanel" data-parent="#faq-five">
                                <div class="panel-body">
                                    <p>Nam liber tempor cum soluta nobis eleifend option.</p>
                                    <p>
                                        Congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me.
                                    </p>
                                    <p>Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum.</p>
                                </div>
                            </div>
                        </div>
                        <!--// Panel-default -->
                    </div>

                </div>
                <div class="col-lg-6 col-md-6">
                    <!--testimonial area start-->
                    <div class="testimonial_area  testimonial_about">
                        <div class="section_title">
                            <h2>Khách hàng của chúng tôi nói gì?</h2>
                        </div>
                        <div class="testimonial_container">
                            <div class="testimonial_carousel testimonial-two owl-carousel">
                                <div class="single_testimonial">
                                    <div class="testimonial_thumb">
                                        <a href="#">
                                            <img src="assets/img/about/testimonial1.png" alt=""></a>
                                    </div>
                                    <div class="testimonial_content">
                                        <div class="testimonial_icon_img">
                                            <img src="assets/img/about/testimonials-icon.png" alt="">
                                        </div>
                                        <p>Tôi rất yên tâm, hài lòng với tất cả các sản phẩm được bán trên Safira </p>
                                        <a href="#">Lindsy Neloms</a>
                                    </div>
                                </div>
                                <div class="single_testimonial">
                                    <div class="testimonial_thumb">
                                        <a href="#">
                                            <img src="assets/img/about/testimonial2.png" alt=""></a>
                                    </div>
                                    <div class="testimonial_content">
                                        <div class="testimonial_icon_img">
                                            <img src="assets/img/about/testimonials-icon.png" alt="">
                                        </div>
                                        <p>I'm so happy with all of the themes, great support, could not wish for more. These people are geniuses! Kudo's from the Netherlands!..</p>
                                        <a href="#">Rebecka Filson</a>
                                    </div>
                                </div>
                                <div class="single_testimonial">
                                    <div class="testimonial_thumb">
                                        <a href="#">
                                            <img src="assets/img/about/testimonial3.png" alt=""></a>
                                    </div>
                                    <div class="testimonial_content">
                                        <div class="testimonial_icon_img">
                                            <img src="assets/img/about/testimonials-icon.png" alt="">
                                        </div>
                                        <p>I'm so happy with all of the themes, great support, could not wish for more. These people are geniuses! Kudo's from the Netherlands!..</p>
                                        <a href="#">Amber Laha</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--testimonial area end-->
                </div>
            </div>
        </div>
    </div>
    <!--testimonial area end-->

</asp:Content>

