<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Account" %>

<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <!-- Breadcrumbs -->

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Tài khoản</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Breadcrumbs End -->

    <div class="customer_login">
        <div class="container">
            <div class="row">
                <!--login area start-->
                <div class="col-lg-6 col-md-6">
                    <div class="account_form">
                        <h2>Đăng nhập</h2>
                        <asp:Panel runat="server" DefaultButton="LinkButton_Login">
                            <%--Login--%>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <p>
                                        <label>Email <span>*</span></label>
                                        <input runat="server" id="input_emailLongin" type="text">
                                    </p>
                                    <p>
                                        <label>Mật khẩu <span>*</span></label>
                                        <input runat="server" id="input_passwordLogin" type="password">
                                    </p>
                                    <p>
                                        <uc1:ucMessage runat="server" ID="ucMessage" />
                                    </p>
                                    <div class="login_submit d-flex justify-content-between">
                                        <a href="#">Quên mật khẩu?</a>
                                        <asp:Button
                                            class="button"
                                            Text="Đăng nhập" runat="server"
                                            ID="LinkButton_Login"
                                            OnClick="LinkButton_Login_Click"></asp:Button>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>


                    </div>
                </div>
                <!--login area start-->

                <!--register area start-->
                <div class="col-lg-6 col-md-6">
                    <div class="account_form register">
                        <h2>Đăng ký</h2>
                        <div>


                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" DefaultButton="LinkButton_Register" class="box-authentication col-xs-6">
                                        <p>
                                            <label>Nhập họ và tên  <span>*</span></label>
                                            <input type="text" runat="server" id="input_fullName">
                                        </p>
                                        <p>
                                            <label>Nhập địa chỉ Email  <span>*</span></label>
                                            <input type="text" runat="server" id="input_Email">
                                        </p>
                                        <p>
                                            <label>Mật khẩu <span>*</span></label>
                                            <input type="password" runat="server" id="input_Password">
                                        </p>
                                        <p>
                                            <label>Mật lại khẩu <span>*</span></label>
                                            <input type="password" runat="server" id="input_RePassword">
                                        </p>
                                        <p>
                                            <uc1:ucMessage runat="server" ID="ucMessage_Register" />
                                        </p>
                                        <div class="login_submit">
                                            <asp:Button Text="Đăng ký" runat="server" class="button" ID="LinkButton_Register" OnClick="LinkButton_Register_Click"></asp:Button>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <!--register area end-->
            </div>
        </div>
    </div>

    <!-- Main Container -->
    <section class="main-container col1-layout">
        <div class="main container">
            <div class="page-content">
                <div class="account-login row">

                    <%-- ucRegister --%>
                </div>
            </div>
        </div>
    </section>

    <!-- Main Container End -->
</asp:Content>

