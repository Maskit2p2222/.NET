<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="VinApp.AddVinDescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 align="center">Admin Panel</h2>
    <div style="padding-top: 2em;">

        <asp:UpdatePanel ID="UpdatePanel1"
            runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; margin-bottom: 25%">
                    <tr style="width: 100%">
                        <td style="width: 100%">
                            <table style="width: 100%">
                                <tr style="width: 100%; height: 100px">

                                    <td style="width: 160px; position: absolute" align="left">

                                        <asp:TextBox ID="TextBox8" CssClass="form-control margin" runat="server" Text="FindMark" ForeColor="#c0c0c0" AutoPostBack="true" OnTextChanged="TextBox8_TextChanged"></asp:TextBox>

                                        <div class="treeview w-20 border">
                                            <asp:TreeView OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ID="TreeView1" runat="server">
                                                <NodeStyle ForeColor="#333333" />


                                            </asp:TreeView>
                                        </div>

                                        <asp:Button ID="Button6" runat="server" Text="AddNewMark" OnClick="Button6_Click" CssClass="btn btn-light btnmargin" />

                                    </td>

                                    <td style="width: 80%">

                                        <h3 runat="server" align="center" id="MainH"></h3>

                                        <div runat="server" id="AddNewMarkDiv" visible="false">
                                            <table style="width: 100%;" class="simple-little-table" runat="server">
                                                <tr style="width: 100%">
                                                    <td>MarkName
                                                    </td>
                                                    <td>AddMark
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td>
                                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server"></asp:TextBox>
                                                        <p runat="server" id="MarkError" visible="false" style="color: red">This brand already exists</p>
                                                    </td>
                                                    <td>
                                                        <asp:Button CssClass="btn btn-dark" runat="server" Text="Add" OnClick="Button7_Click" ID="Button7" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <h4 style="color: red" id="MeaningfulMask" runat="server" visible="false"></h4>

                                        <asp:GridView AllowPaging="true" OnPageIndexChanging="GridViewAll_PageIndexChanging" CssClass="simple-little-table" ID="GridViewAll" runat="server" PageSize="5"></asp:GridView>

                                        <asp:GridView AllowPaging="true" OnPageIndexChanging="GridViewMasks_PageIndexChanging" runat="server" ID="GridViewMasks" CssClass="simple-little-table" Visible="false" AutoGenerateColumns="false" PageSize="5">
                                            <EditRowStyle BackColor="#999999" BorderColor="Black" BorderStyle="Dotted" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mask">
                                                    <ItemTemplate>
                                                        <asp:Label Font-Size="Large" runat="server" ID="LabelMask" Text='<%#Bind("MaskView") %>'></asp:Label>
                                                        <asp:Button runat="server" Text="CheckVinDescs" CssClass="btn btn-light" CommandArgument='<%#Container.DataItemIndex%>' ID="Button10" OnClick="Button10_Click" />
                                                        <asp:TreeView Visible="false" NodeStyle-Font-Bold="true" LeafNodeStyle-HorizontalPadding="30px" runat="server" ID="VinDescTree" Orientation="Vertical" OnSelectedNodeChanged="VinDescTree_SelectedNodeChanged">
                                                        </asp:TreeView>
                                                        <asp:Label runat="server" ID="LabelMaskVisibleFalse" Visible="false" Text='<%#Bind("IdOfMask") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:GridView runat="server" ID="GridViewVinDescs" AllowPaging="true" OnPageIndexChanging="GridViewVinDescs_PageIndexChanging" CssClass="simple-little-table" Visible="false" AutoGenerateColumns="false" PageSize="5">
                                            <EditRowStyle BackColor="#999999" BorderColor="Black" BorderStyle="Dotted" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="VinPart">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelVinPart" Text='<%#Bind("VinPart") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VinPartDecription">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelVinPartDecription" Text='<%#Bind("VinPartDecription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EnumMeaningOfVinParts">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelEnumMeaningOfVinParts" Text='<%#Bind("EnumMeaningOfVinParts") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EnumMeaningOfVinParts">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelMaskOfVinParts" Text='<%#Bind("Mask") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VinDescTree">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" Text="VinDescTree" CssClass="btn btn-light" CommandArgument='<%#Bind("IdOfDescription") %>' ID="Button11" OnClick="Button11_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <h6 runat="server" visible="false" id="VinDescH">*If you want to add new Mark click the button "AddNewMark"</h6>

                                        <asp:Button ID="Button4" runat="server" Text="AddNewMask" Visible="false" OnClick="Button4_Click" CssClass="btn btn-light btnmargin" />

                                        <table style="width: 100%;" class="simple-little-table" runat="server" id="AddMaskTable" visible="false">
                                            <tr style="width: 100%">
                                                <td align="center">Mask</td>
                                                <td align="center">Add</td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="center">
                                                    <asp:TextBox ID="TextBox4" runat="server" MaxLength="17" Text="00000000000000000"></asp:TextBox>
                                                </td>
                                                <td align="center">
                                                    <asp:Button CssClass="btn btn-dark" runat="server" Text="Add" OnClick="Button5_Click" ID="Button5" /></td>
                                            </tr>
                                        </table>

                                        <asp:Button ID="Button8" Visible="false" runat="server" Text="AddMeanengfulMask" CssClass="btn btn-light btnmargin" OnClick="Button8_Click" />

                                        <table style="width: 100%;" class="simple-little-table" runat="server" id="MeaningfulMaskTable" visible="false">
                                            <tr style="width: 100%">
                                                <td align="center">MeaningfulMask </td>
                                                <td align="center">Add</td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="center">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" Visible="false" Text="XXXXXXXXXXX000000"></asp:TextBox></td>
                                                <td align="center">
                                                    <asp:Button CssClass="btn btn-dark" Visible="false" runat="server" Text="Add" OnClick="Button9_Click" ID="Button9" /></td>
                                            </tr>
                                        </table>

                                        <asp:TextBox ID="TextBox7" AutoPostBack="true" runat="server" Visible="false" CssClass="form-control margin" OnTextChanged="TextBox7_TextChanged"></asp:TextBox>

                                        <asp:GridView runat="server" ID="GridViewVins" CssClass="simple-little-table" Visible="false" AutoGenerateColumns="false" OnRowDeleting="GridViewVins_RowDeleting">
                                            <EditRowStyle BackColor="#999999" BorderColor="Black" BorderStyle="Dotted" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="VinTextValue">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelVin" Text='<%#Bind("VinTextValue") %>'></asp:Label>
                                                        <asp:Button runat="server" Text="Info" CssClass="btn btn-light" CommandArgument='<%#Bind("VinTextValue") %>' ID="Button2" OnClick="Button2_Click" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/DeleteImg.png" HeaderStyle-HorizontalAlign="Right" FooterStyle-Width="10" />
                                            </Columns>
                                        </asp:GridView>

                                        <div style="padding-top: 3em">
                                            <asp:GridView ID="GridViewVinsResult" runat="server" Visible="false" CssClass="simple-little-table" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Meaning">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="LabelVin" Text='<%#Bind("EnumMeaning") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="LabelVin" Text='<%#Bind("VinPartDescription") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:GridView runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <InsertItemTemplate>
                                                        <asp:TreeView runat="server">
                                                            <Nodes>
                                                                <asp:TreeNode></asp:TreeNode>
                                                            </Nodes>

                                                        </asp:TreeView>
                                                    </InsertItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>



                                        <asp:Button Visible="false" ID="Button3" runat="server" Text="AddNewVinDesc" OnClick="Button3_Click" CssClass="btn btn-light btnmargin" />

                                        <table style="width: 100%;" class="simple-little-table" runat="server" id="AddVinsDescTable" visible="false">
                                            <tr style="width: 100%">
                                                <td align="center">Mask </td>
                                                <td align="center">Type of Vin part</td>
                                                <td align="center">Vin part</td>
                                                <td align="center">Vin part Description</td>
                                                <td align="center">Add</td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="center">
                                                    <asp:DropDownList CssClass="custom-select" ID="ListBox2" OnTextChanged="ListBox2_TextChanged" AutoPostBack="true" runat="server" Visible="false"></asp:DropDownList></td>
                                                <td align="center">
                                                    <asp:DropDownList CssClass="custom-select" ID="ListBox1" runat="server" Visible="false"></asp:DropDownList></td>
                                                <td align="center">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Visible="false"></asp:TextBox></td>
                                                <td align="center">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" Visible="false"></asp:TextBox></td>
                                                <td align="center">
                                                    <asp:Button CssClass="btn btn-dark" Visible="false" runat="server" Text="Add" OnClick="Button1_Click" ID="Button1" /></td>
                                            </tr>
                                        </table>


                                        <h4 runat="server" id="AddNewVinDescLinksH" align="center" visible="false"></h4>
                                        <table style="width: 100%;" class="simple-little-table" runat="server" id="AddNewVinDescLinks" visible="false">
                                            <tr style="width: 100%">
                                                <td align="center">Mask </td>
                                                <td align="center">Type of Vin part</td>
                                                <td align="center">Vin part</td>
                                                <td align="center">Vin part Description</td>
                                                <td align="center">Add</td>
                                            </tr>
                                            <tr style="width: 100%">
                                                <td align="center">
                                                    <asp:DropDownList CssClass="custom-select" ID="DropDownList1" OnTextChanged="DropDownList1_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                                                <td align="center">
                                                    <asp:DropDownList CssClass="custom-select" ID="DropDownList2" runat="server"></asp:DropDownList></td>
                                                <td align="center">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server"></asp:TextBox></td>
                                                <td align="center">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server"></asp:TextBox></td>
                                                <td align="center">
                                                    <asp:Button CssClass="btn btn-dark" runat="server" Text="Add" OnClick="Button12_Click" ID="Button12" /></td>
                                            </tr>
                                        </table>

                                        <asp:Button CssClass="btn btn-dark" Visible="false" ID="Button13" runat="server" Text="Delete" OnClick="Button13_Click" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>
        <style>
            .btnmargin {
                margin-top: 15px;
                margin-bottom: 15px;
            }

            .margin {
                margin-bottom: 10px;
            }
        </style>


    </div>
</asp:Content>
