<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test1.aspx.cs" Inherits="WebFormDemo.Test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body style="height: 406px; width: 725px">
    <form id="form1" runat="server">
        <div style="width: 209px">
            姓名：
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div>
            性别：<asp:DropDownList ID="Sex" runat="server">
                <asp:ListItem Text="男" Value="1" />
                <asp:ListItem Text="女" Value="0" />
            </asp:DropDownList>
        </div>
        <div>
            爱好：
            <asp:CheckBoxList ID="chkLike" runat="server">
<%--                <asp:ListItem Text="足球" Value="1" />
                <asp:ListItem Text="蓝球" Value="2" />
                <asp:ListItem Text="羽毛球" Value="3" />--%>
            </asp:CheckBoxList>
        </div>
        <div>
            学历：
            <asp:RadioButtonList ID="rdoEducation" runat="server">
                <asp:ListItem Text="小学" Value="1"/>
                <asp:ListItem Text="初中" Value="2"/>
                <asp:ListItem Text="高中" Value="3"/>
            </asp:RadioButtonList>
        </div>
        <div>
            是否党员：
            <asp:CheckBox ID="chkIsDy" runat="server"/>
        </div>
        <asp:Button ID="Button1" runat="server" Height="49px" OnClick="Button1_Click" Text="测试" Width="212px" />
    </form>
</body>
</html>
