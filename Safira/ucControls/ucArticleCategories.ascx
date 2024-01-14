<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucArticleCategories.ascx.cs" Inherits="ucArticleCategories" %>

<div class="block blog-module">
    <div class="sidebar-bar-title">
        <h3>Danh mục tin tức</h3>
    </div>
    <div class="block_content">
        <!-- layered -->
        <div class="layered layered-category">
            <div class="layered-content">
                <ul class="tree-menu">
                    <asp:Repeater runat="server" ID="Repeater_ArticleCategories">
                        <ItemTemplate>
                            <li><a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><i class="fa fa-angle-right"></i>&nbsp; <%# Eval("Title") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!-- ./layered -->
    </div>
</div>
