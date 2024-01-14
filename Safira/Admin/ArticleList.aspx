<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="Admin_ArticleList" %>

<%@ Register Src="~/Admin/UserControl/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>
<%@ Register Src="~/Admin/UserControl/ucPagging.ascx" TagPrefix="uc1" TagName="ucPagging" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Quản lý danh sách tin tức
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
                    <a href="Default.aspx">Bàn Làm Việc</a>
                    <span class="divider">&gt;</span>
                </li>
                <li>
                    <a href="ArticleCategoryList.aspx">Loại Tin Tức</a>
                    <span class="divider">&gt;</span>
                </li>
                <li>
                    <a href="ArticleList.aspx">Danh Sách Tin Tức</a>
                </li>
            </ul>
        </div>

        <div class="workplace">
            <div class="page-header">
                <h1>Danh sách tin tức
                </h1>
            </div>
            <uc1:ucMessage runat="server" ID="ucMessage" />

            <div class="row-fluid">
                <div class="span12">
                    <div class="head clearfix">
                        <div class="isw-grid">
                        </div>
                        <h1>Danh sách tin tức
                        </h1>
                        <ul class="buttons">
                            <li><a href="ArticleEdit.aspx" class="isw-plus tip" title="Thêm mới"></a></li>
                            <li><a href="#" class="isw-delete tip" title="Xóa chọn" onclick="alert('Chức năng xóa hàng loạt này hiện chưa có.')"></a></li>
                        </ul>
                    </div>
                    <div class="block-fluid table-sorting clearfix">
                        <!--Filter-->
                        <div class="dataTables_filter">
                            <asp:Panel runat="server" ID="Panel_Search" DefaultButton="LinkButton_Search" class="input-append">
                                <asp:DropDownList runat="server" ID="DropDownList_Category" AutoPostBack="true"
                                    OnSelectedIndexChanged="DropDownList_Category_SelectedIndexChanged"
                                    Style="margin-right: 10px; border-radius: 4px;" />

                                <input runat="server" id="input_Title" type="text" placeholder="Lọc theo tiêu đề (tiếng Việt có dấu)" style="width: 250px" />

                                <asp:LinkButton runat="server" ID="LinkButton_Search"
                                    class="btn mybtn input-icon link-search" Style="width: 16px;"
                                    OnClick="LinkButton_Search_Click">
                                             <i class="isw-zoom"></i>&nbsp;
                                </asp:LinkButton>
                            </asp:Panel>
                        </div>

                        <div class="dataTables_length">
                            <asp:LinkButton runat="server" ID="LinkButton_ClearSearch"
                                OnClick="LinkButton_ClearSearch_Click" class="btn input-icon" Style="width: 80px;">
                                 <i class="isw-cancel"></i>Hủy bộ lọc
                            </asp:LinkButton>
                        </div>

                        <!--Content-->
                        <table cellpadding="0" cellspacing="0" width="100%" class="table" id="tPagging">
                            <thead>
                                <tr>
                                    <th width="25px" class="center">
                                        <input type="checkbox" name="checkall" />
                                    </th>
                                    <th width="50px" class="center">Hình
                                    </th>
                                    <th width="50px" class="center">Mã số
                                    </th>
                                    <th>Tiêu đề / Lượt xem / Loại / Mô tả
                                    </th>
                                    <th width="100px" class="center">Người đăng /Ngày
                                    </th>
                                    <th width="200px" class="center">Xem / Xóa / Sửa
                                    </th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="Repeater_Main">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="center middle">
                                                <input type="checkbox" name="checkbox" />
                                            </td>
                                            <td width="102" class="center middle avatar">
                                                <a runat="server" href='<%# Eval("Avatar") %>' rel="group" title='<%# Eval("Title") %>'>
                                                    <img runat="server" src='<%# Eval("Avatar") %>' alt="avatar" style="width: 92px; height: 92px; margin-bottom: 2px;"
                                                        class='<%# Eval("ID","default-image avatar-preview-{0}") %>'
                                                        original="~/fileuploads/article/thumbs/nu-phong-vien-yeu-nhung-0096-1.jpg" />
                                                </a>
                                                <div class="btn-group">
                                                    <button class="btn btn-small btn-success btn-file" style="width: 102px; cursor: pointer;">
                                                        <span class="icon-camera icon-white"></span>Thay hình
                                                        <asp:FileUpload runat="server" ID="FileUpload_Avatar"
                                                            type="file" class="skip" preview='<%# Eval("ID","avatar-preview-{0}") %>' />
                                                    </button>
                                                </div>
                                                <div class="btn-group none-margin hide save-cancel-function">
                                                    <asp:LinkButton runat="server" ID="LinkButton1"
                                                        OnClick="LinkButton_SaveAvatar_Click"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        class="btn btn-warning tip save" title="Lưu hình">
                                                        <i class="isw-save"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="LinkButton_SaveAvatar"
                                                        OnClick="LinkButton_SaveAvatar_Click"
                                                        class="btn btn-inverse tip cancel" title="Hủy lệnh">
                                                        <i class="isw-cancel"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </td>
                                            <td runat="server" class="center middle">
                                                <%# Eval("ID") %>
                                            </td>
                                            <td class="middle">
                                                <h4>
                                                    <%# Eval("Title") %>
                                                    ( <%# Eval("ViewTime") %>)
                                                </h4>
                                                <p>Loại: <b><%# Eval("CatTitle") %></b></p>
                                                <p align="justify">
                                                    <%# Eval("Description") %>
                                                </p>
                                            </td>
                                            <td class="center middle">
                                                <%# Eval("CreateBy") %>
                                                <br />
                                                <%# Eval("CreateTime","{0:dd/MM/yyyy}") %><br />
                                                <%# Eval("CreateTime","{0:HH:mm:ss}") %>
                                            </td>
                                            <td class="center middle function">
                                                <p class="info">
                                                    <a runat="server" title='<%# Eval("Title") %>' class="btn btn-small btn-block block tip fancybox-iframe btn-info"
                                                        href='<%# Eval("ID","ArticleDetail.aspx?id={0}") %>'>
                                                        <span class="icon-zoom-in icon-white"></span>Hiển thị chi tiết
                                                    </a>

                                                    <a runat="server" class="btn btn-small btn-block block tip btn-warning"
                                                        href='<%# Eval("ID","ArticleEdit.aspx?id={0}") %>'>
                                                        <span class="icon-edit icon-white"></span>Chỉnh sửa thông tin
                                                    </a>

                                                    <asp:LinkButton runat="server" ID="LinkButton_Delete"
                                                        OnClick="LinkButton_Delete_Click"
                                                        OnClientClick="return confirm('Bạn có chắc muốn xóa không');"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        class="btn btn-small btn-block block tip btn-danger">
                                                        <span class="icon-trash icon-white"></span>Xóa dữ liệu này
                                                    </asp:LinkButton>
                                                    <%-- Kích hoạt --%>
                                                    <asp:LinkButton runat="server" ID="LinkButton_Active"
                                                        class='<%# Eval("Status").ToBool()==true? "btn btn-small btn-success tip" :"btn btn-small btn-warning tip"  %>'
                                                        OnClick="LinkButton_Active_Click"
                                                        CommandArgument='<%# Eval("ID") %>'>
                                                        <span class='<%# Eval("Status").ToBool()==true? "icon-ok icon-white" : "icon-lock icon-white" %>'></span>

                                                        <%# Eval("Status").ToBool() == true? "Đang kích hoạt" : "Đang tạm khóa"%>
                                                    </asp:LinkButton>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <!--Pagging-->
                        <uc1:ucPagging runat="server" ID="ucPagging" />

                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="Server">
</asp:Content>

