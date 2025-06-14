<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayImge.aspx.cs" Inherits="WebGrid_Image_Path_Database_EF_MVC.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--  <span>Select Image:</span>
        <asp:DropDownList ID="ddlImages" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
            OnSelectedIndexChanged="FetchImage">
            <asp:ListItem Text="Select Image" Value="0" />
        </asp:DropDownList>
        <hr />--%>
        <asp:Image ID="Image1" runat="server" Visible="false" />
        </div>
    </form>
</body>
</html>
