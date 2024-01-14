<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucArticleMenu.ascx.cs" Inherits="ucArticleMenu" %>

<ul class="dropdown">
    <asp:Repeater runat="server" ID="Repeater_ArticleMenu">
        <ItemTemplate>
            <li><a runat="server" href='<%# Eval("ID","~/ArticleList.aspx?mid={0}") %>'><%# Eval("Title") %> </a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
