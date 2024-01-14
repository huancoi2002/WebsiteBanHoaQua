<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Admin_OrderList" %>

<%@ Register Src="~/Admin/UserControl/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>
<%@ Register Src="~/Admin/UserControl/ucPagging.ascx" TagPrefix="uc1" TagName="ucPagging" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Quản lý danh sách đơn hàng
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="content">
        <div class="workplace">
            <div class="page-header">
                <h1>
                   Danh sách đơn hàng
                </h1>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <uc1:ucMessage runat="server" ID="ucMessage" />
                    <div class="head clearfix">
                        <div class="isw-grid">
                        </div>
                        <h1>
                            Danh sách đơn hàng
                        </h1>
                        <ul class="buttons">
                            <li>
                                <a href="OrderEdit.aspx" class="isw-plus tip" title="Thêm mới"></a>
                            </li>
                            <li>
                                <a href="#" class="isw-delete tip" title="Xóa chọn" onclick="alert('Chức năng xóa hàng loạt này hiện chưa có.')"></a>
                            </li>
                        </ul>
                    </div>

                    <div class="block-fluid table-sorting clearfix">
                        <!--Filter-->
                        <div class="dataTables_filter">
                            <asp:Panel runat="server" DefaultButton="LinkButton_Search" class="input-append">
                                <input type="text" runat="server" id="input_ID" placeholder="Lọc theo đơn hàng..." />
                                <input runat="server" id="input_Title" type="text" placeholder="Lọc theo tiêu đề (tiếng Việt có dấu)" style="width: 250px" />
                                <asp:LinkButton runat="server" ID="LinkButton_Search" class="btn mybtn input-icon link-search"
                                    OnClick="LinkButton_Search_Click" Style="width: 16px;">
                                    <i class="isw-zoom"></i>&nbsp;
                                </asp:LinkButton>

                            </asp:Panel>
                        </div>

                        <div class="dataTables_length">
                            <asp:LinkButton runat="server" ID="LinkButton_ClearSearch"
                                OnClick="LinkButton_ClearSearch_Click"
                                class="btn input-icon" Style="width: 80px;">
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
                                    <th width="50px" class="center">Order ListID
                                    </th>
                                    <th width="50px" >Info Client
                                    </th>
                                    <th>Total Pay
                                    </th>
                                    <th >Order Status
                                    </th>
                                    <th class="center">Create Time
                                    </th>
                                    <th width="200px" class="center">Xem / Delete
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="Repeater_Main">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="center middle">
                                                <input type="checkbox" name="checkbox" />
                                            </td>
                                            <td width="102" class="center middle avatar">
                                                <%# Eval ("ID") %>
                                            </td>
                                            <td class=" middle">Họ tên khách: <%# Eval ("FullName") %>
                                                <br />
                                                Email khách: <%# Eval ("Email") %>
                                                <br />
                                                SĐT khách: <%# Eval ("Mobi") %>
                                                <br />
                                                SĐT2 khách: <%# Eval ("Mobi2") %>
                                                <br />
                                                Phái: <%# Eval ("Gender").ToBool() == true ? "Nam": "Nữ" %>
                                                <br />
                                                Địa chỉ khách: <%# Eval ("Address") %>
                                                <br />
                                            </td>
                                            <td class="middle">
                                                Tổng Tiền: <%# Eval ("Total", "{0:0,00đ}") %>
                                                <br />
                                            </td>
                                            <td class="middle">
                                                 Trạng Thái đơn hàng: <%# Eval ("OrderStatus").ToBool() == true ? "Đã lặp ": "Chưa lặp" %>
                                                <br />
                                                 Trạng Thái giao hàng: <%# Eval ("DeliverStatus").ToBool() == true ? "Đã giao": "Chưa giao" %>
                                                <br />
                                                 Trạng Thái thanh toán: <%# Eval ("ChargeStatus").ToBool() == true ? "Đã thanh toán ": "Chưa thanh toán" %>
                                                <br />
                                                Tổng Tiền: <%# Eval ("Total", "{0:0,00đ}")%>
                                                <br />
                                            </td>
                                            <td class=" center middle">
                                                <%# Eval ("CreateTime", "{0:dd/MM/yyyy HH:mm:ss}") %>
                                            </td>
                                            <td class="center middle function">

                                                <p class="info">
                                                    <a href='<%# Eval("ID", "OrderDetail.aspx?id={0}") %>' class="btn btn-small btn-block block tip fancybox-iframe btn-info" runat="server">
                                                        <span class="icon-zoom-in icon-white "></span>Xem chi tiết
                                                    </a>

                                                    <asp:LinkButton runat="server" ID="LinkButton_Delete"
                                                        OnClick="LinkButton_Delete_Click"
                                                        OnClientClick="return confirm('Bạn có chắc muốn xóa không?');"
                                                        CommandArgument='<%# Eval ("ID") %>'
                                                        class="btn btn-small btn-block block tip btn-danger">
                                                         <span class="icon-trash icon-white"></span> Xóa dữ liệu này
                                                    </asp:LinkButton>
                                                </p>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </tbody>
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

