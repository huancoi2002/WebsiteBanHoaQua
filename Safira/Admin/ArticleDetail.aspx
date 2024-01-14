<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleDetail.aspx.cs" Inherits="Admin_ArticleDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater runat="server" ID="Repeater_Detail">
            <ItemTemplate>
                <div>
                    <div id="div_Content" style="text-align: justify;">
                        <p>
                            <%# Eval("Content") %>
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
