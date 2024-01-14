<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleDetail.aspx.cs" Inherits="ArticleDetail" %>

<%@ Register Src="~/ucControls/ucArticleCategories.ascx" TagPrefix="uc1" TagName="ucArticleCategories" %>
<%@ Register Src="~/ucControls/ucPopularPosts.ascx" TagPrefix="uc1" TagName="ucPopularPosts" %>
<%@ Register Src="~/ucControls/ucArticleTags.ascx" TagPrefix="uc1" TagName="ucArticleTags" %>




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
                            <li>Chi tiết tin tức</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--blog body area start-->
    <div class="blog_details">
        <div class="container">
            <div class="row">
                <div class="col-lg-9 col-md-12">
                    <!--blog grid area start-->
                    <div class="blog_wrapper blog_wrapper_details">
                        <article class="single_blog">

                            <asp:Repeater runat="server" ID="Repeater_ArticleDetail">
                                <ItemTemplate>
                                    <figure>
                                        <div class="post_header">
                                            <h3 class="post_title"><%# Eval("Title") %></h3>
                                            <div class="blog_meta">
                                                <p>Đăng bởi : <a href="#"><%# Eval("CreateBy") %></a> / Vào lúc : <a href="#"><%# Eval("CreateTime","{0,0:dd/MM/yyyy}") %></a> / In : <a href="#">Company, Image, Travel</a></p>
                                            </div>
                                        </div>
                                        <div class="blog_thumb text-center">
                                            <a href="#">
                                                <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                        </div>
                                        <figcaption class="blog_content">
                                            <div class="post_content">
                                                <%# Eval("Description") %>
                                                <hr />
                                                <%# Eval("Content") %>
                                            </div>
                                            <div class="entry_content">
                                                <div class="post_meta">
                                                    <span>Tags: </span>
                                                    <span><a href="#">, fashion</a></span>
                                                    <span><a href="#">, t-shirt</a></span>
                                                    <span><a href="#">, white</a></span>
                                                </div>

                                                <div class="social_sharing">
                                                    <p>share this post:</p>
                                                    <ul>
                                                        <li><a href="#" title="facebook"><i class="fa fa-facebook"></i></a></li>
                                                        <li><a href="#" title="twitter"><i class="fa fa-twitter"></i></a></li>
                                                        <li><a href="#" title="pinterest"><i class="fa fa-pinterest"></i></a></li>
                                                        <li><a href="#" title="google+"><i class="fa fa-google-plus"></i></a></li>
                                                        <li><a href="#" title="linkedin"><i class="fa fa-linkedin"></i></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </figcaption>
                                    </figure>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                      
                        <div class="related_posts">
                            <h3>Tin tức liên quan</h3>
                            <div class="row">
                                <asp:Repeater runat="server" ID="Repeater_ArticleRelated">
                                    <ItemTemplate>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <article class="single_related">
                                                <figure>
                                                    <div class="related_thumb related_thumb_cs">
                                                        <a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>
                                                            <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                                    </div>
                                                    <figcaption class="related_content">
                                                        <h4><a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'><%# Eval("Title") %></a></h4>
                                                        <div class="blog_meta">
                                                            <span class="author">Tạo bởi : <a href="#"><%# Eval("CreateBy") %></a> | </span>
                                                            <span class="meta_date"><%# Eval("CreateTime","{0,0:dd/MM/yyyy}") %>	</span>
                                                        </div>
                                                    </figcaption>
                                                </figure>
                                            </article>
                                        </div>

                                        
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                        <div class="comments_box">
                            <h3>3 Comments	</h3>
                            <div class="comment_list">
                                <div class="comment_thumb">
                                    <img src="assets/img/blog/comment3.png.jpg" alt="">
                                </div>
                                <div class="comment_content">
                                    <div class="comment_meta">
                                        <h5><a href="#">Admin</a></h5>
                                        <span>October 16, 2022 at 1:38 am</span>
                                    </div>
                                    <p>Trang cập nhập rất nhiều tin tức mới</p>
                                    <div class="comment_reply">
                                        <a href="#">Reply</a>
                                    </div>
                                </div>

                            </div>
                            <div class="comment_list list_two">
                                <div class="comment_thumb">
                                    <img src="assets/img/blog/comment3.png.jpg" alt="">
                                </div>
                                <div class="comment_content">
                                    <div class="comment_meta">
                                        <h5><a href="#">Demo</a></h5>
                                        <span>October 16, 2022 at 1:38 am</span>
                                    </div>
                                    <p>Quisque semper nunc vitae erat pellentesque, ac placerat arcu consectetur</p>
                                    <div class="comment_reply">
                                        <a href="#">Reply</a>
                                    </div>
                                </div>
                            </div>
                            <div class="comment_list">
                                <div class="comment_thumb">
                                    <img src="assets/img/blog/comment3.png.jpg" alt="">
                                </div>
                                <div class="comment_content">
                                    <div class="comment_meta">
                                        <h5><a href="#">Admin</a></h5>
                                        <span>October 16, 2022 at 1:38 am</span>
                                    </div>
                                    <p>Quisque orci nibh, porta vitae sagittis sit amet, vehicula vel mauris. Aenean at</p>
                                    <div class="comment_reply">
                                        <a href="#">Reply</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <!--blog grid area start-->
                <div class="col-lg-3 col-md-12">
                    <div class="blog_sidebar_widget">
                        <div class="widget_list widget_categories">
                            <div class="widget_title">
                                <h3>Danh mục tin tức</h3>
                            </div>
                            <ul>
                                <asp:Repeater runat="server" ID="Repeater_Category">
                                    <ItemTemplate>
                                        <li>
                                            <a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><%# Eval("Title") %>  <span>(<%# Eval("QuantityProduct") %>)</span></a>
                                        </li>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="widget_list widget_tag">
                            <div class="widget_title">
                                <h3>Thẻ Tag Tin tức</h3>
                            </div>
                            <div class="tag_widget">
                                <ul>
                                    <asp:Repeater runat="server" ID="Repeater_Tag">
                                        <ItemTemplate>
                                            <li style="margin-bottom: 15px"><a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><%# Eval("Title") %> </a></li>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--blog section area end-->

    <!-- Main Container -->

    <!-- Main Container End -->
</asp:Content>

