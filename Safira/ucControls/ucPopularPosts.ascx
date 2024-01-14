<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPopularPosts.ascx.cs" Inherits="ucPopularPosts" %>

<div class="block blog-module">
    <div class="sidebar-bar-title">
        <h3>Bài viết phổ biến</h3>
    </div>
    <div class="block_content">
        <!-- layered -->
        <div class="layered">
            <div class="layered-content">
                <ul class="blog-list-sidebar">
                    <asp:Repeater runat="server" ID="Repeater_PopularPost">
                        <ItemTemplate>
                            <li>
                                <div class="post-thumb">
                                    <a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'>
                                        <img src='<%# Eval("Avatar") %>' alt="Blog"></a>
                                </div>
                                <div class="post-info">
                                    <h5 class="entry_title"><a runat="server" href='<%# Eval("ID","~/ArticleDetail.aspx?id={0}") %>'><%# Eval("Title") %></a></h5>
                                    <div class="post-meta"><span class="date"><i class="fa fa-calendar"></i><%# Eval("CreateTime","{0:dd/MM/yyyy}") %></span> <span class="comment-count"><i class="fa fa-comment-o"></i><%# Eval("ViewTime") %> </span></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!-- ./layered -->
    </div>
</div>
