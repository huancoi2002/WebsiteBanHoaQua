<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContactForm.ascx.cs" Inherits="ucContactForm" %>
<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div id="contact-form">
            <p>
                <label>Họ và tên (Bắt buộc)</label>
                <input name="name" placeholder="Name *" type="text" runat="server" id="input_Name">
            </p>
            <p>
                <label>Email (Bắt buộc)</label>
                <input name="email" placeholder="Email *" runat="server"  id="input_Email">
            </p>
            <p>
                <label>Số điện thoại (Bắt buộc)</label>
                <input name="subject" placeholder="Phone *" type="text" runat="server" id="input_Phone">
            </p>
            <p>
                <label>Danh mục liên hệ (Bắt buộc)</label>
                <asp:DropDownList runat="server" class="form-control input-sm" ID="DropDownList_Categories" />
            </p>
            <div class="contact_textarea">
                <label>Nội dung</label>
                <textarea placeholder="Message *" name="message" class="form-control2" runat="server" id="textarea_Message"></textarea>
            </div>

            <div class="form-selector">
                <asp:LinkButton class="btn btn-success"
                    runat="server"
                    ID="LinkButton_SendMessage"
                    OnClick="LinkButton_SendMessage_Click">
                        Gửi thư liên hệ
                </asp:LinkButton>

                &nbsp;

                    <asp:LinkButton
                        runat="server"
                        ID="LinkButton_Clear"
                        OnClick="LinkButton_Clear_Click">
                        <i class="fa fa-trast"></i>
                        Nhập lại
                    </asp:LinkButton>

            </div>
            <p class="form-messege" style="margin-top:15px">
                <uc1:ucMessage runat="server" ID="ucMessage" />
            </p>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>
