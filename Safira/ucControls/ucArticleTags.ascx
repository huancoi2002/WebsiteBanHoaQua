<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucArticleTags.ascx.cs" Inherits="ucArticleTags" %>

<div class="popular-tags-area block">
    <div class="sidebar-bar-title">
        <h3>Thẻ tags</h3>
    </div>
    <div class="tag">
        <ul>
            <asp:Repeater runat="server" ID="Repeater_ArticleTags">
                <ItemTemplate>
                    <li><a href="#"><%# Eval("Text") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
