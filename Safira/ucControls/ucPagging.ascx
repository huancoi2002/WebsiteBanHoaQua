<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="ucPagging.ascx.cs"  Inherits="ucPagging" %>

<div class="pagination">
    <ul>
        <li><a runat="server" id="PageFirst" href="#"><i class="fa fa-step-backward" aria-hidden="true"></i></a></li>
        <li><a runat="server" id="PageBack"  href="#"><i class="fa fa-angle-left"></i></a></li>

        <asp:Repeater runat="server" ID="PageRepeater">
            <ItemTemplate>
                <li>
                    <a runat="server" id="PageNumber" class='<%# (Eval("Key").ToInt()==(Request.QueryString["page"].ToInt()==0?1:Request.QueryString["page"].ToInt()))?"current":"" %>' href='<%# Eval("Value") %>'><%# Eval("Key") %></a>
                </li>
            </ItemTemplate>
        </asp:Repeater>

        <li><a runat="server" id="PageNext" href="#"><i class="fa fa-angle-right"></i></a></li>
        <li><a runat="server" id="PageLast" href="#"><i class="fa fa-step-forward" aria-hidden="true"></i></a></li>
    </ul>
</div>
