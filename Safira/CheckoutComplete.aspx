<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckoutComplete.aspx.cs" Inherits="CheckoutComplete" %>

<%@ Register Src="~/ucControls/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #animationfree{
            width: 1200px;
            position: relative;
            animation: myAnimation 2s ease-out infinite;
            -moz-animation: myAnimation 2s ease-out infinite;
            -webkit-animation: myAnimation 20s ease-out infinite;
            -o-animation: myAnimation 2s ease-out infinite;
            animation-duration: 20s;
            animation-timing-function: ease-out;
            animation-direction: alternate;
        }

        @keyframes myAnimation {
            0% {
                left: 0px;
            }

            25% {
                left: 300px;
            }

            50% {
                left: 300px;
            }

            75% {
                left: 0px;
            }

            100% {
                left: 0px;
            }
        }

        /* Hien thi cho Firefox */
        @-moz-keyframes myAnimation {
            from {
                left: 0px;
            }

            to {
                left: 300px;
            }
        }

        /* Hien thi cho Safari and Chrome */
        @-webkit-keyframes myAnimation {
            from {
                left: 0px;
            }

            to {
                left: 300px;
            }
        }

        /* Hien thi cho Opera */
        @-o-keyframes myAnimation {
            from {
                left: 0px;
            }

            to {
                left: 300px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Trạng thái thanh toán</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row" style="margin-top: 20px; margin-left: 3px">
            <div class="col-md-6" style="border: solid; color: #23883a; height: 350px;padding-top:15px;border-radius: 5px;">
                <uc1:ucMessage runat="server" ID="ucMessage" />
                <p id="animationfree">
                    <img style="width: 250px; height: 250px" src="assets/img/icon/giao-hang.png" />
                </p>
            </div>
            <div class="col-md-1">
            </div>
            <div class="col-md-5" style="border: solid; color: #23883a; height: 350px">
                <div style=" font-size: 18px; text-align: center; font-weight: 600; margin-top: 10px;border-radius: 5px;">Chi tiết đơn hàng</div>
                <div style="margin-left: 20px; font-size: 15px; line-height: 40px;">
                    <asp:Repeater runat="server" ID="Repeater_OrderDetail">
                        <ItemTemplate>
                            <div><span class="text-default">Khách hàng:</span>   <%# Eval("FullName")%></div>
                            <div>Email:  <%# Eval("Email")%></div>
                            <div>Số điện thoại:  <%# Eval("Mobi")%></div>
                            <div>Mã đơn hàng:  <%# Eval("ID")%></div>
                            <div>Tổng tiền:   <span class="text-danger"><%# Eval("Total","{0:0,00 đ}")%></span></div>
                            <div>Ngày mua hàng:  <%# Eval("CreateTime")%></div>
                            <div>Địa chỉ:  <%# Eval("Address")%></div>
                            <div>Phương thức thanh toán:  <%# Eval("PaymentMethod").ToInt() == 0?" Tại nhà":" Qua Ngân Lượng" %></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


        </div>
    </div>

</asp:Content>

