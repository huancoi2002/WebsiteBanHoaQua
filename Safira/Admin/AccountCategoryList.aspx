<%@ page title="" language="C#" masterpagefile="~/Admin/MasterPages/MasterPage.master" autoeventwireup="true" codefile="AccountCategoryList.aspx.cs" inherits="Admin_Pages_AccountCategoryList" %>

<%@ register src="~/Admin/UserControl/ucMessage.ascx" tagprefix="uc1" tagname="ucMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <a href="Default.html">Bàn Làm Việc</a>
                            <span class="divider">&gt;</span>
                        </li>
                        <li>
                            <a href="AccountCategoryList.html">Loại Tài Khoản</a>
                        </li>
                    </ul>
                </div>

        <div class="workplace">
            <div class="page-header">
                <h1>Loại Tài Khoản
                </h1>
            </div>

            <div class="row-fluid">
                <div class="span3">
                    <div class="head clearfix">
                        <div class="isw-folder">
                        </div>
                        <h1>Danh sách loại tài khoản
                        </h1>
                    </div>
                    <%--<div class="block-fluid">
                        <div class="row-form clearfix">
                            <select size="4" class="category" style="height: 475px;">
                                <option value="SupperAdmin">SupperAdmin</option>
                            </select>
                        </div>
                    </div>--%>
                    <div class="block-fluid">
                        <div class="row-form clearfix">
                            <asp:ListBox runat="server"
                                ID="ListBox_Category"
                                OnSelectedIndexChanged="ListBox_Category_SelectedIndexChanged"
                                AutoPostBack="true"
                                class="category"
                                Style="height: 478px;" />
                        </div>
                    </div>
                </div>
                <div class="span9">
                    <div class="head clearfix">
                        <div class="isw-list">
                        </div>
                        <h1>Chi tiết loại tài khoản đang chọn
                        </h1>
                        <ul class="buttons">
                            <li>
                                <asp:LinkButton
                                    runat="server"
                                    ID="LinkButton_Add"
                                    OnClick="LinkButton_Add_Click"
                                    class="isw-plus tip"
                                    title="Thêm mới" />
                            </li>
                            <li>
                                <asp:LinkButton
                                    runat="server"
                                    ID="LinkButton_Delete"
                                    OnClientClick="return confirm('Bạn có muốn xóa không?')"
                                    OnClick="LinkButton_Delete_Click"
                                    class="isw-delete tip" />
                            </li>
                        </ul>
                    </div>
                    <div class="block-fluid  customize">
                        <div class="row-form clearfix">
                            <div class="span2">
                                Mã loại tài khoản:
                            </div>
                            <div class="span7">
                                <input runat="server" id="input_ID" type="text" class="tipb" />
                                <span>Sử dụng chữ cái (hoặc thêm số). Không gõ dấu và không khoảng trắng. VD: SupperAdmin</span>
                            </div>
                            <div class="span1">
                                Vị trí:
                            </div>
                            <div class="span2" style="float: right;">
                                <input runat="server" id="input_Position" type="text" class="tipb" title="Dùng để sắp xếp thứ tự" />
                                <span>Nhập số hoặc để trống</span>
                            </div>
                        </div>
                        <div class="row-form clearfix">
                            <div class="span2">
                                Tiêu đề:
                            </div>
                            <div class="span10">
                                <input runat="server" id="input_Title" type="text" />
                                <span>VD: Quản trị cao cấp, quản trị viên, ...</span>
                            </div>
                        </div>
                        <div class="row-form clearfix">
                            <div class="span2">
                                Mô tả:
                            </div>
                            <div class="span10">
                                <textarea runat="server" id="textarea_Description" style="min-height: 50px; width: 100%;"></textarea>
                                <span>Nên mô tả chi tiết về các quyền hoặc các giới hạn của loại tài khoản này.</span>
                            </div>
                        </div>
                        <!--Hình đại diện-->
                        <div class="row-form clearfix">
                            <div class="span2">
                                Hình đại diện:
                            </div>
                            <div class="span10">
                                <a runat="server" id="a_Avatar" href="../css/img/no_image.jpg" class="fancybox lightbox-preview" rel="group">
                                    <img runat="server" id="img_Avatar" src="../css/img/no_image.jpg" alt="avatar" class="default-image img-polaroid avatar-preview" style="width: 180px; height: 135px;" />
                                </a>
                                <br />
                                <asp:FileUpload runat="server" ID="FileUpload_Avatar" class="skip" preview="avatar-preview" />
                                <br />
                                <span>Hình đại diện cho bài báo. Bạn có thể upload hình mới nếu muốn.
                                            Các Loại file hỗ trợ: *.jpg, *.jpeg, *.gif, *.png
                                </span>
                            </div>
                        </div>
                        <div class="row-form clearfix">
                            <div class="span2">
                                Trạng thái:
                            </div>
                            <div class="span10">
                                <label class="checkbox inline">
                                    <input runat="server" id="radio_Active" type="radio" checked name="Status" />
                                    Cho phép hiển thị
                                </label>
                                <label class="checkbox inline">
                                    <input runat="server" id="radio_Lock" type="radio" name="Status" />
                                    Tạm ẩn
                                </label>
                            </div>
                        </div>
                        <div class="row-form clearfix">
                            <div class="span2">
                                <asp:LinkButton runat="server" ID="LinkButton_Save" class="btn mybtn" OnClick="LinkButton_Save_Click">
                                    <i class="isw-save"></i>Lưu
                                </asp:LinkButton>
                            </div>
                            <div class="span10">

                                <!--Thông báo-->
                                <uc1:ucmessage runat="server" id="ucMessage" />
                            </div>

                        </div>
                    </div>
                </div>
                <div class="tar">
                    <a href="AccountList.aspx" type="button" class="btn active">
                        <i class="icon-arrow-left"></i>Trở về trang danh sách tài khoản
                    </a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

