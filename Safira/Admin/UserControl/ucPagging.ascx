<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="ucPagging.ascx.cs"  Inherits="ucPagging" %>

<%--<div class="pagination-area ">
    <ul>
        <li><a runat="server" id="PageFirst" href="#"><i class="fa fa-step-backward" aria-hidden="true"></i></a></li>
        <li><a runat="server" id="PageBack"  href="#"><i class="fa fa-angle-left"></i></a></li>

        <asp:Repeater runat="server" ID="PageRepeater">
            <ItemTemplate>
                <li>
                    <a runat="server" id="PageNumber" class='<%# (Eval("Key").ToInt()==(Request.QueryString["page"].ToInt()==0?1:Request.QueryString["page"].ToInt()))?"active":"" %>' href='<%# Eval("Value") %>'><%# Eval("Key") %></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>

        <li><a runat="server" id="PageNext" href="#"><i class="fa fa-angle-right"></i></a></li>
        <li><a runat="server" id="PageLast" href="#"><i class="fa fa-step-forward" aria-hidden="true"></i></a></li>
    </ul>
</div>--%>


<div class="dataTables_info">
    Trang
    <b runat="server" id="CurrentPageValue">1</b>
    của
    <b runat="server" id="TotalPagesValue">262</b>
    (Tổng cộng:
    <b runat="server" id="TotalItemsValue">1307</b>
    item)
</div>
<div class="dataTables_paginate paging_full_numbers">
    <a runat="server" id="PageFirst" class="paginate_button">&lt;&lt;</a>
    <a runat="server" id="PageBack" class="paginate_button">&lt;</a>
    <span>
        <asp:Repeater runat="server" ID="PageRepeater">
            <ItemTemplate>
                 <a runat="server" id="PageNumber" class='<%# (Eval("Key").ToInt()==(Request.QueryString["page"].ToInt()==0?1:Request.QueryString["page"].ToInt()))?"paginate_button paginate_active":"paginate_button" %>' href='<%# Eval("Value") %>'><%# Eval("Key") %></a>
              <%--  <a runat="server" id="PageNumber" class="paginate_button" data-active="paginate_active">1
                </a>--%>
            </ItemTemplate>
        </asp:Repeater>
    </span>
    <a runat="server" id="PageNext" class="paginate_button">&gt;</a>
    <a runat="server" id="PageLast" class="paginate_button">&gt;&gt;</a>
</div>
