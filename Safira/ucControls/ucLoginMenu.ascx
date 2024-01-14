<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLoginMenu.ascx.cs" Inherits="ucLoginMenu" %>
<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<div id="div-forms">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="login-form">
                <div class="modal-body">
                    <div id="div-login-msg"><span id="text-login-msg">Nhập địa chỉ Email</span></div>
                    <input runat="server" id="input_Email" class="form-control" type="text" placeholder="Email">
                    <input runat="server" id="input_Password" class="form-control" type="password" placeholder="Password">
                    <div class="checkbox">
                        <label>
                            <input type="checkbox">
                            Nhớ mật khẩu
                        </label>
                    </div>
                    <uc1:ucMessage runat="server" ID="ucMessage" />
                </div>
                <div class="modal-footer">
                    <div>
                        <asp:Button Text="Đăng nhập" runat="server" class="btn-login" ID="Button_Login" OnClick="Button_Login_Click" />
                    </div>
                    <div>
                        <button runat="server" onclick="location.href='Account.aspx'" id="login_register_btn" type="button" class="btn btn-link">Đăng kí</button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
