<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="Article" %>

<%@ Register Src="~/ucControls/ucPagging.ascx" TagPrefix="uc1" TagName="ucPagging" %>


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
                            <li>Tin tức mới nhất</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="blog_page_section mt-70">
        <div class="container">
            <div class="row">
                <div class="col-lg-9 col-md-12">
                    <div class="blog_wrapper">
                        <div class="row">
                            <asp:Repeater runat="server" ID="Repeater_ArticleList">
                                <ItemTemplate>
                                    <div class="col-lg-4 col-md-4 col-sm-6">
                                        <article class="single_blog">
                                            <figure>
                                                <div class="blog_thumb blog_thumb_cs">
                                                    <a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>
                                                        <img runat="server" src='<%# Eval("Avatar") %>' alt=""></a>
                                                </div>
                                                <figcaption class="blog_content">
                                                    <h4 class="post_title"><a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>
                                                        <%# Eval("Title") %>
                                                    </a></h4>
                                                    <div class="articles_date">
                                                        <p><%# Eval("CreateTime","{0:dd/MM/yyy}") %> | <a href="#"><%# Eval("CreateBy") %></a> </p>
                                                    </div>
                                                </figcaption>
                                            </figure>
                                        </article>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <!--blog pagination area start-->
                    <div class="blog_pagination">
                        <div class="container">
                            <div class="row">
                                <div class="col-12">
                                    <uc1:ucPagging runat="server" ID="ucPagging" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--blog pagination area end-->
                </div>
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
                                            <li style="margin-bottom:15px"><a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><%# Eval("Title") %> </a></li>

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

</asp:Content>

