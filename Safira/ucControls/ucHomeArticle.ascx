<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHomeArticle.ascx.cs" Inherits="ucHomeArticle" %>

<!--blog area start-->
<section class="blog_section" style="margin-bottom:0">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <div class="section_title">
                    <p>Các bài viết gần đây</p>
                    <h2>Tin tức mới nhất</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="blog_carousel blog_column3 owl-carousel">
                <asp:Repeater runat="server" ID="Repeater_HomeArticle">
                    <ItemTemplate>
                        <div class="col-lg-3">
                            <article class="single_blog">
                                <figure>
                                    <div class="blog_thumb blog_thumb_cs">
                                        <a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>
                                            <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                    </div>
                                    <figcaption class="blog_content">
                                        <div class="articles_date">
                                            <p><%# Eval("CreateTime","{0:dd/MM/yyyy}") %> | <a href="#"><%# Eval("CreateBy") %></a> </p>
                                        </div>
                                        <h4 class="post_title"><a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'><%# Eval("Title").Left(70,true,true) %></a></h4>
                                        <footer class="blog_footer">
                                            <a  runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>Xem chi tiết</a>
                                        </footer>
                                    </figcaption>
                                </figure>
                            </article>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</section>


