<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckVin.aspx.cs" Inherits="VinApp.CheckVin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="align-content: center">Check Vin</h2>
    <div class="input-group-prepend">
        <asp:TextBox aria-describedby="basic-addon1" CssClass="form-control" runat="server" ID="TextBox1" Text="Enter Vin" MaxLength="17">   
        </asp:TextBox>
        <asp:Button CssClass="btn btn-outline-secondary" runat="server" ID="Button1" OnClick="Button1_Click" Text="Check" />

    </div>
    <asp:Label ID="Label1" runat="server" Text="" Visible="false" BorderStyle="Solid"></asp:Label>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
