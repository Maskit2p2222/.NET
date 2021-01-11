<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpVinFullInfo.aspx.cs" Inherits="Temp.PopUpVinFullInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/MainCSS.css" type="text/css" rel="stylesheet" />
    <link href="Css/TableForPopUp.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:GridView ID="GridViewVinDecdingResultsTable" runat="server" Visible="true" CssClass="simple-little-table" AutoGenerateColumns="false">
            <HeaderStyle Height="5%" HorizontalAlign="Center" Font-Size="Medium" />
            <RowStyle Height="5%" Font-Size="Small" />
            <Columns>
                <asp:TemplateField HeaderText="Вид">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LabelVin" Text='<%#Bind("EnumMeaning") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Значение">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LabelVinPartDescription" Text='<%#Bind("VinPartDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
