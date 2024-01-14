<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ActiveAcount.aspx.cs" Inherits="ActiveAcount" %>

<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="clearfix"></div>
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="message">
                    <div runat="server" id="div_Alert" class="">
                        Xin chúc mừng:
                    </div>
                    <div runat="server" class="content" id="div_Content">
                        Bạn đã kích hoạt tài khoản thành công ! Vui lòng đăng nhập
                    </div>
                    <a runat="server" id="a_login" href="~/Account.aspx" class="btn btn-success">Đăng nhập</a>
                     <a runat="server" href="~/Default.aspx" class="btn btn-info">Về trang chủ</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

