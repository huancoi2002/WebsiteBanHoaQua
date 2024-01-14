<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSearch.ascx.cs" Inherits="ucSearch" %>

<div class="top-search">
    <div id="search">
        <div>
            <div class="input-group">
                <asp:Panel runat="server" DefaultButton="LinkButton_Search" class="input-append">
                    <asp:DropDownList runat="server" ID="DropDownList_Categories" class="cate-dropdown hidden-xs hidden-sm" name="category_id" />
                    <input type="text" class="form-control search-query" placeholder="Bạn muốn tìm gì ?" runat="server" id="input_Search">
                    <asp:LinkButton Text="Search" runat="server" class="btn btn-success" ID="LinkButton_Search" OnClick="LinkButton_Search_Click">
                    <i class="fa fa-search"></i>
                        Tìm kiếm
                    </asp:LinkButton>
                </asp:Panel>

            </div>
        </div>
    </div>
</div>
