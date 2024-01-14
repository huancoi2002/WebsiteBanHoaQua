<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="AccountPasswordChange.aspx.cs" Inherits="Admin_Pages_AccountPasswordChange" %>

<%@ Register Src="~/Admin/UserControl/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Đổi mật khẩu
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="content">
        <div class="breadLine">
            <ul class="breadcrumb">
                <!--Nút ẩn/hiện menu bên góc trái-->
                <li>
                    <a href="#" title="Ẩn thanh menu" class="sidebarControl menu-arrow">
                        <span class="icon-arrow-left"></span>
                    </a>
                </li>
                <!--Thanh breadcrumb-->
                <li>
                    <a runat="server" href="~/Admin/Default.aspx">Bàn Làm Việc</a>
                    <span class="divider">&gt;</span>
                </li>
                <li>
                    <a runat="server" href="~/Admin/AccountPasswordChange.aspx">Đổi mật khẩu</a>
                </li>
            </ul>
        </div>

        <asp:Panel runat="server" DefaultButton="LinkButton_Save" class="workplace">
            <!--Tiêu đề-->
            <div class="page-header">
                <h1>Đổi mật khẩu
                </h1>
            </div>
            <!--Nội dung-->
            <div class="row-fluid">
                <div class="span12">
                    <div class="head clearfix">
                        <div class="isw-list">
                        </div>
                        <h1>Mời nhập mật khẩu mới
                        </h1>
                    </div>
                    <div class="block-fluid">
                        <!--Username-->
                        <div class="row-form clearfix">
                            <div class="span2">
                                Tên đăng nhập:
                            </div>
                            <div class="span2">
                                <input runat="server" id="input_Username" type="text" class="tip" readonly />
                            </div>
                            <div class="span8">
                                <span class="warning-mess">Bạn đang đổi mật khẩu cho tài khoản này</span>
                            </div>
                        </div>

                        <!--Mật khẩu-->
                        <div class="row-form clearfix">
                            <div class="span2">
                                Mật khẩu mới:
                            </div>
                            <div class="span2">
                                <input runat="server" id="input_Password" type="password" />
                            </div>
                            <div class="span1">
                                Nhập lại:
                            </div>
                            <div class="span2">
                                <input runat="server" id="input_RePassword" type="password" />
                            </div>
                            <div class="span5">
                                <span class="warning-mess">Mật khẩu mới dùng để đăng nhập, cần nhập 2 lần giống nhau.
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Button-->
            <div class="row-fluid">
                <div class="span12" style="margin-top: -20px; background-color: #F2F2F2;">
                    <div class="block-fluid  customize">
                        <div class="row-form clearfix">
                            <div class="span2">
                                <asp:LinkButton ID="LinkButton_Save" Text="text" runat="server" class="btn mybtn" OnClick="LinkButton_Save_Click">
                                     <i class="isw-save"></i>Lưu
                                </asp:LinkButton>
                            </div>
                            <div class="span10">
                                <!--Thông báo-->
                                <uc1:ucMessage runat="server" ID="ucMessage" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Link trở về-->
            <div class="tar">
                <a runat="server" href="~/Admin/Default.aspx" type="button" class="btn active">
                    <i class="icon-arrow-left"></i>Trở về trang chủ
                </a>
            </div>

        </asp:Panel>
    </div>
</asp:Content>

