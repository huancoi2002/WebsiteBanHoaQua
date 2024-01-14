<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" codefile="ProductDetail.aspx.cs" inherits="ProductDetail" %>

<%@ register src="~/ucControls/ucSpecialProduct.ascx" tagprefix="uc1" tagname="ucSpecialProduct" %>


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
                            <li>Chi tiết sản phẩm</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Repeater runat="server" ID="Repeater_Product">
        <itemtemplate>

            <!--product details start-->
            <div class="product_details mt-30 mb-70">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6 col-md-6">
                            <div class="product-details-tab">
                                <div id="img-1" class="zoomWrapper single-zoom">
                                    <a href="#">
                                        <img id="zoom1" runat="server" clientidmode="Static" src='<%# Eval("Avatar").ToSafetyString().Replace("~/","/") %>' data-zoom-image='<%# Eval("Avatar").ToSafetyString().Replace("~/","/") %>' alt="big-1">
                                    </a>
                                </div>
                                <div class="single-zoom-thumb">
                                    <ul class="s-tab-zoom owl-carousel single-product-active" id="gallery_01">
                                        <asp:Repeater runat="server" ID="Repeater_ImageList" DataSource='<%# Eval("ImageList").SplitToText(";") %>'>
                                            <itemtemplate>
                                                <li>
                                                    <a href='<%# Eval("Text") %>' class="elevatezoom-gallery active" data-update="" data-image='<%# Eval("Text") %>' data-zoom-image='<%# Eval("Text") %>'>
                                                        <img runat="server" src='<%# Eval("Text") %>' alt="Thumbnail 2" />
                                                    </a>
                                                </li>
                                            </itemtemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="product_d_right">
                                <form action="#" >

                                    <h1><a href="#"><%# Eval("Title") %></a></h1>

                                    <div class=" product_ratting">
                                        <ul>
                                            <li><a href="#"><i class="icon-star"></i></a></li>
                                            <li><a href="#"><i class="icon-star"></i></a></li>
                                            <li><a href="#"><i class="icon-star"></i></a></li>
                                            <li><a href="#"><i class="icon-star"></i></a></li>
                                            <li><a href="#"><i class="icon-star"></i></a></li>
                                            <li class="review"><a href="#">(Đánh giá khách hàng ) </a></li>
                                        </ul>

                                    </div>
                                    <div class="price_box">
                                        <span class="current_price"><%# Eval("Price","{0:0,00 ₫}") %></span>
                                        <span class="old_price"><%# Eval("OldPrice","{0:0,00 ₫}") %></span>

                                    </div>
                                    <div class="product_desc">
                                        <%# Eval("Description") %>'
                                        <%--<ul class="parameter">
                                            <asp:Repeater runat="server" ID="Repeater_Description" DataSource='<%# Eval("Description").SplitToTextValue(":", ";") %>'>
                                                <ItemTemplate>
                                                    <li class="title">
                                                        <div class="d-flex ">
                                                            <span style="min-width: 200px; font-weight: 500"><%# Eval("Text") %>:  </span>

                                                            <div><%# Eval("Value") %></div>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>--%>
                                    </div>
                                    
                                    <div class="row">

                                        <div class="col-lg-4 col-md-4" style="float: left; width: 135px; display: flex; }">
                                            <input onclick="var result = document.getElementById('txtQuantity'); var qty = result.value; if( !isNaN(qty) &amp; qty > 1 ) result.value--;return false;" type='button' value='-' style="background: #fff; float: left; border: 1px solid #e1e1e1; cursor: pointer; font-weight: 600; outline: none; height: 45px; width: 45px; text-align: center; border-radius: 0; font-size: 20px; color: black; padding: 0; display: flex; justify-content: center; align-items: center; line-height: unset;" />
                                            <input ID='txtQuantity' min='1' name='quantity' type='text' value='1' placeholder="1" style="background: #fff; font-weight: 600; height: 45px; text-align: center; width: 45px; border: 1px solid #e1e1e1; border-left: none; border-right: none; border-radius: 0; float: left; -webkit-appearance: none; font-size: 15px; color: black; padding: 0;" />
                                            <input onclick="var result = document.getElementById('txtQuantity'); var qty = result.value; if( !isNaN(qty) &amp; qty < 99 ) result.value++;return false;" type='button' value='+' style="background: #fff; float: left; border: 1px solid #e1e1e1; cursor: pointer; font-weight: 600; outline: none; height: 45px; width: 45px; text-align: center; border-radius: 0; font-size: 20px; color: black; padding: 0; display: flex; justify-content: center; align-items: center; line-height: unset;" />
                                        </div>


                                        

                                        <div class="product_variant quantity col-lg-4 col-md-4" style=" margin:0; ">
                                            <asp:Button Text="Thêm vào giỏ hàng"
                                                runat="server"
                                                ID="LinkButton_AddCart"
                                                OnClick="LinkButton_AddCart_Click"
                                                CommandArgument='<%# Eval("ID") %>'
                                                ClientIDMode="AutoID"
                                                Style="margin: 0; min-width: 175px; border-radius: unset;"
                                                class="button"
                                                OnClientClick="alert('Thêm giỏ hàng thành công')"></asp:Button>
                                        </div>
                                        <div class="product_variant quantity col-lg-4 col-md-4" style="border-radius: unset; margin:0; ">
                                            <asp:Button Text="Mua ngay"
                                                runat="server"
                                                ID="LinkButton_ToCart"
                                                OnClick="LinkButton_ToCart_Click"
                                                CommandArgument='<%# Eval("ID") %>'
                                                ClientIDMode="AutoID"
                                                Style="margin: 0; min-width: 175px; border-radius: unset;"
                                                class="button"></asp:Button>
                                        </div>
                                            
                                    </div>
                            </form>


                        </div>
                    </div>
                </div>
            </div>
            </div>
            <!--product details end-->

            <!--product info start-->
            <div class="product_d_info mb-65">
                <div class="container">
                    <div class="row">
                        <div class="col-12">
                            <div class="product_d_inner">
                                <div class="product_info_button">
                                    <ul class="nav" role="tablist" id="nav-tab">
                                        <li>
                                            <a class="active" data-toggle="tab" href="#info" role="tab" aria-controls="info" aria-selected="false">Mô tả sản phẩm</a>
                                        </li>

                                        <li>
                                            <a data-toggle="tab" href="#reviews" role="tab" aria-controls="reviews" aria-selected="false">Reviews (1)</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-content">
                                    <div class="tab-pane fade show active" id="info" role="tabpanel">
                                        <div class="product_info_content">
                                            <%# Eval("Content") %>
                                        </div>
                                    </div>


                                    <div class="tab-pane fade" id="reviews" role="tabpanel">
                                        <div class="reviews_wrapper">
                                            <h2>1 review for Donec eu furniture</h2>
                                            <div class="reviews_comment_box">
                                                <div class="comment_thmb">
                                                    <img src="assets/img/blog/comment2.jpg" alt="">
                                                </div>
                                                <div class="comment_text">
                                                    <div class="reviews_meta">
                                                        <div class="star_rating">
                                                            <ul>
                                                                <li><a href="#"><i class="icon-star"></i></a></li>
                                                                <li><a href="#"><i class="icon-star"></i></a></li>
                                                                <li><a href="#"><i class="icon-star"></i></a></li>
                                                                <li><a href="#"><i class="icon-star"></i></a></li>
                                                                <li><a href="#"><i class="icon-star"></i></a></li>
                                                            </ul>
                                                        </div>
                                                        <p><strong>admin </strong>-1/5/2022</p>
                                                        <span>roadthemes</span>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="comment_title">
                                                <h2>Đánh giá khách hàng </h2>
                                                <p>Your email address will not be published.  Required fields are marked </p>
                                            </div>
                                            <div class="product_ratting mb-10">
                                                <h3>Your rating</h3>
                                                <ul>
                                                    <li><a href="#"><i class="icon-star"></i></a></li>
                                                    <li><a href="#"><i class="icon-star"></i></a></li>
                                                    <li><a href="#"><i class="icon-star"></i></a></li>
                                                    <li><a href="#"><i class="icon-star"></i></a></li>
                                                    <li><a href="#"><i class="icon-star"></i></a></li>
                                                </ul>
                                            </div>
                                            <div class="product_review_form">
                                                <form action="#">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <label for="review_comment">Your review </label>
                                                            <textarea name="comment" id="review_comment"></textarea>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6">
                                                            <label for="author">Name</label>
                                                            <input id="author" type="text">
                                                        </div>
                                                        <div class="col-lg-6 col-md-6">
                                                            <label for="email">Email </label>
                                                            <input id="email" type="text">
                                                        </div>
                                                    </div>
                                                    <button type="submit">Submit</button>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--product info end-->

        </itemtemplate>
    </asp:Repeater>


    <section class="product_area related_products">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="section_title">
                        <h2>Sản phẩm liên quan</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="product_carousel product_column5 owl-carousel">
                        <asp:Repeater runat="server" ID="Repeater_ProductRelated">
                            <itemtemplate>
                                <article class="single_product">
                                    <figure>
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
                                        <figcaption class="product_content">
                                            <h4 class="product_name"><a runat="server" href='<%# Eval("ID","~/ProductDetail.aspx?id={0}") %>'><%# Eval("Title").Left(50,true,true) %></a></h4>
                                            <div class="price_box">
                                                <span class="current_price"><%# Eval("Price").ToString("0,00 VNĐ") %></span>
                                                <span class="old_price"><%# Eval("OldPrice").ToString("0,00 VNĐ") %></span>
                                            </div>
                                        </figcaption>
                                    </figure>
                                </article>
                            </itemtemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>

