<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucInfo.ascx.cs" Inherits="ucInfo" %>

<div class="breadLine">
    <div class="arrow"></div>
    <div class="adminControl active">
        Xin chào, <b runat="server" id="b_Admin_Username">admin</b>
    </div>
</div>

<div class="admin">
    <div class="image">
        <img runat="server" id="img_Admin_Avatar" src="../../fileuploads/Account/8761ea0b-776c-4504-a845-58d5617d90f4.png" class="use-avatar" />
    </div>
    <ul class="control">
        <li>
            <span class="icon-user"></span>
            <a runat="server" id="a_Admin_FullName" href="AccountEdit.html?id=admin">Lê Minh Hiếu</a>
        </li>
        <li>
            <span class="icon-cog"></span>
            <a runat="server" href="~/Admin/AccountPasswordChange.aspx">Đổi mật khẩu</a>
        </li>
        <li>
            <span class="icon-off"></span>
            <asp:LinkButton Text="Thoát" runat="server" ID="LinkButton_Logout" OnClick="LinkButton_Logout_Click" />
        </li>
    </ul>
</div>
