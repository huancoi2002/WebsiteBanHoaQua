<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="Admin_OrderDetail" %>

<%@ Register Src="~/Admin/UserControl/ucCSS.ascx" TagPrefix="uc1" TagName="ucCSS" %>
<%@ Register Src="~/Admin/UserControl/ucJS.ascx" TagPrefix="uc1" TagName="ucJS" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết đơn hàng
    </title>
    <uc1:ucCSS runat="server" ID="ucCSS" />
    <style>
        .order-table input[type=text] {
            border: none;
            box-shadow: none;
            background-color: transparent !important;
            border-bottom: 2px dotted #808080;
            border-radius: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="workplace">
            <div class="page-header">
                <h1>Chi tiết đơn hàng</h1>
               
            </div>
            <div class="row-fluid">
                <div class="span12">
                    <div class="block-fluid customize accordion">
                        <h3>Nội dung chi tiết</h3>
                        <div class="article-basic-info order-table">
                            <div>
                                <!--Số hóa đơn-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Số hóa đơn:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_ID" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Tên Khách-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Tên khách hàng:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_Fullname" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Email-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Email:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_Email" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Số điện thoại-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Số điện thoại 1:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_Mobi" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Số điện thoại 2-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Số điện thoại 2:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_Mobi2" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Số hóa đơn-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Địa chỉ:
                                    </div>
                                    <div class="span10">
                                        <input runat="server" id="input_Address" type="text" disabled="disabled" />
                                    </div>
                                </div>

                                <!--Trạng thái-->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Trạng thái:
                                    </div>
                                    <div class="span10">
                                        <label class="checkbox inline">
                                            <input runat="server" id="radio_Order" name="radio" type="radio"  />
                                            Đặt hàng:
                                        </label>
                                        <label class="checkbox inline">
                                            <input runat="server" id="radio_Deliver" name="radio" type="radio"  />
                                            Giao hàng:
                                        </label>
                                        <label class="checkbox inline">
                                            <input runat="server" id="radio_Charge" name="radio" type="radio"  />
                                            Thanh toán:
                                        </label>
                                    </div>
                                </div>

                                <!-- Chi tiết -->
                                <div class="row-form clearfix">
                                    <div class="span2">
                                        Chi tiết
                                    </div>
                                    <div class="span10">
                                        <table cellpadding="0" cellspacing="0" width="100%" class="table" id="tPagging">
                                            <thead>
                                                <tr>
                                                    <th class="center">Số thứ tự
                                                    </th>

                                                    <th>Tên sản phẩm
                                                    </th>

                                                    <th class="center">Hình ảnh
                                                    </th>

                                                    <th class="center">Đơn vị
                                                    </th>

                                                    <th class="center">Số lượng
                                                    </th>

                                                    <th>Giá
                                                    </th>

                                                    <th>Thành tiền
                                                    </th>
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Detail">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="center middle">
                                                                <%#Container.ItemIndex + 1 %>
                                                            </td>

                                                            <td class="middle">
                                                                <%# Eval("Title") %>
                                                            </td>

                                                            <td class="center middle">
                                                                <img runat="server" src='<%# Eval("Avatar") %>' width="100" />
                                                            </td>

                                                            <td class="center middle">Sản Phẩm
                                                            </td>

                                                            <td class="center middle">
                                                                <%# Eval("Quantity") %>
                                                            </td>

                                                            <td class="center middle">
                                                                <%# Eval("Price", "{0:0,00đ}") %>
                                                            </td>

                                                            <td class="middle">
                                                                <%# (Eval("Quantity").ToInt() * Eval("Price").ToDouble()).ToString("0,00đ")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <tr>
                                                    <td colspan="7" style="text-align: right">Tổng tiền:
                                                        <b runat="server" id="b_Total">10.000.000 đ
                                                        </b>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <uc1:ucJS runat="server" ID="ucJS" />
    </form>
</body>
</html>
