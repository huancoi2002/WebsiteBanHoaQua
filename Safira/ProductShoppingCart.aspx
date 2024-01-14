<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProductShoppingCart.aspx.cs" Inherits="ProductShoppingCart" %>

<%@ Register Src="~/ucControls/ucShoppingCart.ascx" TagPrefix="uc1" TagName="ucShoppingCart" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="breadcrumbs_area">
                    <div class="breadcrumb_content" style="margin: 0 8px;">
                        <ul>
                            <li><a runat="server" href="~/Default.aspx">Trang chủ</a></li>
                            <li>Giỏ hàng của tôi</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <uc1:ucShoppingCart runat="server" ID="ucShoppingCart" />
</asp:Content>

