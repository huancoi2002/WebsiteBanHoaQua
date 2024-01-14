<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUpperHeader.ascx.cs" Inherits="ucUpperHeader" %>

<div class="col-md-6 col-sm-5 col-xs-6">
    <!-- Default Welcome Message -->
    <div class="welcome-msg hidden-xs">Chào mừng <span runat="server" id="span_FullName" style="color: red">khách hàng</span>&nbsp;đến với SMarket! </div>
</div>

<!-- top links -->
<div class="headerlinkmenu col-lg-6 col-md-6 col-sm-7 col-xs-6 text-right">
    <div class="links">
        <div class="jtv-user-info">
            <div class="dropdown">
                <a class="current-open" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" href="#"><span>Tài khoản của (<span runat="server" id="span_FullNames">tôi </span>)</span><i class="fa fa-angle-down"></i></a>
                <ul class="dropdown-menu" role="menu">
                    <li><a runat="server" id="a_Login" href="~/Account.aspx">Đăng nhập</a></li>
                    <li><a runat="server" id="a_Register" href="~/Account.aspx">Đăng ký</a></li>
                    <li>
                        <asp:LinkButton
                            Text="Đăng xuất"
                            runat="server"
                            ID="LinkButton_Logout"
                            Visible="false"
                            OnClick="LinkButton_Logout_Click" /></li>
                </ul>
            </div>
        </div>
    </div>
</div>
