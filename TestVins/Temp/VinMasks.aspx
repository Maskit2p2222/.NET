<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VinMasks.aspx.cs" Inherits="Temp.VinMasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <link href="Css/MainCSS.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            $('#<%=hfMarkIdInPopUp.ClientID%>').val(divname);
            var div = document.getElementById('div' + divname);
            var tr = document.getElementById('tr' + 'div' + divname)
            var a = document.getElementById('img' + 'div' + divname);
            $('#<%=hfMarkId.ClientID%>').val(divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                tr.style.display = "";
                a.innerText = "↓";
            } else {
                div.style.display = "none";
                tr.style.display = "none";
                a.innerText = "→";
            }
        }
        function pageLoad(sender, args) {
            var markId = $('#<%=hfMarkId.ClientID%>').val();
            if (markId != " ") {
                divexpandcollapse(markId);
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1"
        runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table style="width: 100%">
                            <tr style="width: 100%">
                                <td style="width: 90%">
                                    <h1 align="center" style="font-family: Arial, Helvetica, sans-serif;">Таблица Марок</h1>
                                    <asp:HiddenField runat="server" Value=" " ID="hfMarkId" />
                                    <asp:GridView
                                        runat="server" ID="FakeMarksGrid"
                                        CssClass="paddingsForGrid TableForVinMarks"
                                        Visible="true"
                                        AutoGenerateColumns="false"
                                        PagerSettings-Visible="false"
                                        ShowHeaderWhenEmpty="true"
                                        EnableSortingAndPagingCallbacks="true"
                                        OnRowDataBound="MarksGrid_RowDataBound"
                                        DataKeyNames="MarkId">
                                        <PagerTemplate></PagerTemplate>
                                        <EmptyDataRowStyle Font-Size="Medium" />
                                        <HeaderStyle Height="5%" CssClass="positionStatic" HorizontalAlign="Center" Font-Size="Large" />
                                        <RowStyle Height="5%" Font-Size="Medium" />
                                        <PagerSettings Position="Bottom" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="5%">
                                                <HeaderTemplate>
                                                    <asp:Button runat="server" ID="ButtonPlusMark" CssClass="ButtonWidth btn btn-outline-dark" AutoPostBack="true" Text="+" Font-Size="Large" OnClick="ButtonPlusMark_Click" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-Width="30%" HeaderText="Марка" />
                                            <asp:BoundField HeaderStyle-Width="30%" HeaderText="Значащая маска" />
                                            <asp:BoundField HeaderStyle-Width="30%" HeaderText="Количество описателей" />
                                            <asp:TemplateField HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div id="AdjResultsDiv">
                                        <asp:GridView
                                            runat="server" ID="MarksGrid"
                                            CssClass="paddingsForGrid TableForVinMarks"
                                            Visible="true"
                                            AutoGenerateColumns="false"
                                            PageSize="10"
                                            AllowSorting="true"
                                            AllowPaging="true"
                                            PagerSettings-Visible="false"
                                            ShowHeaderWhenEmpty="true"
                                            EmptyDataText="Нет марок"
                                            EnableSortingAndPagingCallbacks="true"
                                            OnRowDataBound="MarksGrid_RowDataBound"
                                            DataKeyNames="MarkId"
                                            ShowHeader="false">
                                            <PagerTemplate></PagerTemplate>
                                            <EmptyDataRowStyle Font-Size="Medium" />
                                            <HeaderStyle Height="5%" CssClass="positionStatic" HorizontalAlign="Center" Font-Size="Large" />
                                            <RowStyle Height="5%" Font-Size="Medium" />
                                            <PagerSettings Position="Bottom" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <a href="JavaScript:divexpandcollapse('<%# Eval("MarkId") %>');">
                                                            <p id="imgdiv<%# Eval("MarkId") %>" style="font-size: large;">→</p>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderStyle-Width="30%" ItemStyle-Width="30%" HeaderText="Марка" DataField="Mark" />
                                                <asp:BoundField HeaderStyle-Width="30%" ItemStyle-Width="30%" HeaderText="Значащая маска" DataField="MeaningfulMask" />
                                                <asp:BoundField HeaderStyle-Width="30%" ItemStyle-Width="30%" HeaderText="Количество описателей" DataField="CountOfVinDescriptions" />
                                                <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <tr id="trdiv<%# Eval("MarkId") %>" style="display: none">
                                                            <td colspan="100%">
                                                                <div id="div<%# Eval("MarkId") %>" style="display: none; position: relative; left: 15px; overflow: auto">

                                                                    <div runat="server" style="position: relative; left: 15px; overflow: auto">
                                                                        <asp:GridView
                                                                            CssClass="TableForVinDescsSubVinMasks"
                                                                            Width="89.2%"
                                                                            ID="FakeMasksGridView"
                                                                            runat="server"
                                                                            AutoGenerateColumns="false"
                                                                            ShowHeaderWhenEmpty="true">
                                                                            <EmptyDataRowStyle Font-Size="Medium" />
                                                                            <HeaderStyle Height="5%" CssClass="positionStatic" HorizontalAlign="Center" Font-Size="Large" />
                                                                            <RowStyle Height="5%" Font-Size="Medium" />
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Button runat="server" ID="ButtonPlusMask"
                                                                                            CssClass="ButtonWidth btn btn-outline-dark"
                                                                                            AutoPostBack="true" Text="+" Font-Size="Large" OnClick="ButtonPlusMask_Click" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" HeaderText="Маска" />
                                                                                <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" HeaderText="Позиция значищих элементов" />
                                                                                <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" HeaderText="Количество описателей" />

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <div id="AdjResultsDiv3">
                                                                            <asp:GridView
                                                                                CssClass="TableForVinDescsSubVinMasks"
                                                                                Width="90%"
                                                                                ID="MasksGridView"
                                                                                runat="server"
                                                                                AutoGenerateColumns="false"
                                                                                ShowHeaderWhenEmpty="true"
                                                                                ShowHeader="false">
                                                                                <EmptyDataRowStyle Font-Size="Medium" />
                                                                                <HeaderStyle Height="5%" CssClass="positionStatic" HorizontalAlign="Center" Font-Size="Large" />
                                                                                <RowStyle Height="5%" Font-Size="Medium" />
                                                                                <Columns>
                                                                                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                                                        <HeaderTemplate>
                                                                                            <asp:Button runat="server" ID="ButtonPlusMask"
                                                                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                                                                AutoPostBack="true" Text="+" Font-Size="Large" OnClick="ButtonPlusMask_Click" />
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:Button runat="server" ID="DescriptionsInfo"
                                                                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                                                                AutoPostBack="true" Text="?" CommandArgument='<%#Bind("MaskId") %>' Font-Size="Large" OnClick="DescriptionsInfo_Click" />

                                                                                            <asp:Button runat="server" ID="ButtonPlusDescription"
                                                                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                                                                AutoPostBack="true" Text="+" CommandArgument='<%#Bind("MaskId") %>' Font-Size="Large" OnClick="ButtonPlusDescription_Click" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" DataField="Mask" HeaderText="Маска" />
                                                                                    <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" DataField="XPosition" HeaderText="Позиция значищих элементов" />
                                                                                    <asp:BoundField ItemStyle-Width="30%" HeaderStyle-Width="30%" DataField="CountOfDescriptions" HeaderText="Количество описателей" />

                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="parentForPager simple-little-table">
                                        <div class="child">
                                            <a style="font-size: medium">Просматривается </a>
                                            <b>
                                                <asp:Label Font-Size="Medium" runat="server" ID="CurrentPageSize"></asp:Label></b>
                                            <a style="font-size: medium">записей из </a>
                                            <b>
                                                <asp:Label Font-Size="Medium" runat="server" ID="CountRowsSize"></asp:Label></b>
                                            <a style="font-size: medium">,страница: </a>
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtonDoubleArrowLeft" CssClass="ButtonWidth btn btn-outline-dark" AutoPostBack="true" Text="<<" Font-Size="Large" OnClick="ButtonDoubleArrowLeft_Click" />
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtonArrowLeft" CssClass="ButtonWidth btn btn-outline-dark" AutoPostBack="true" Text="<" Font-Size="Large" OnClick="ButtonArrowLeft_Click" />
                                        </div>
                                        <div class="child" style="font-size: Medium;">
                                            <asp:TextBox TextMode="Number" runat="server" CssClass="textBoxWidth" ID="InputPager" AutoPostBack="true" OnTextChanged="InputPager_TextChanged"></asp:TextBox>
                                        </div>
                                        <div style="font-size: large" class="child">
                                            из
                                        </div>
                                        <div style="font-size: large" class="child">
                                            <asp:Label runat="server" ID="PageCount"></asp:Label>
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtonRightArrow" CssClass="ButtonWidth btn btn-outline-dark" AutoPostBack="true" Text=">" Font-Size="Large" OnClick="ButtonRightArrow_Click" />
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtondoubleArrowRight" CssClass="ButtonWidth btn btn-outline-dark" AutoPostBack="true" Text=">>" Font-Size="Large" OnClick="ButtondoubleArrowRight_Click" />
                                        </div>
                                        <div class="child">
                                            <a style="font-size: medium">Применено фильтров - </a>
                                            <b>
                                                <asp:Label Font-Size="medium" runat="server" ID="LabelCountOfFilters"></asp:Label></b>

                                        </div>
                                        <div class="child">
                                            <asp:Button Font-Size="Small" CssClass="ButtonHeight btn btn-outline-dark" runat="server" Text="Сбросить все фильтры" ID="ResetFiltersButton"
                                                OnClick="ResetFiltersButton_Click" />
                                        </div>
                                    </div>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <%-- Окно с добавлением марки--%>
            <asp:Panel ID="AddMarkPopUp" runat="server" Style="display: none;">
                <layouttemplate>
                        <table class="TableForVinDescsSubVinMasks">
                            <thead>
                                <tr align="center">
                                    <th><div class="parentForPager">
                                        
                                        <div class="child" style="padding-left:60px">
                                            <h5 align="center">Добавление марки</h5>
                                        </div>
                                        <div class="child" style="padding-left:68px;">
                                            <a align="left"><asp:Button runat="server" ID="ButtonCloseAddMarkPopUp" CssClass="ButtonWidth_CloseButton"
                                            AutoPostBack="true" Text="X" Font-Size="small" OnClick="ButtonCloseAddMarkPopUp_Click" /></a>
                                        </div>
                                        
                                        </div> 
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td  align="center">
                                                        Марка:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox aria-describedby="basic-addon1"
                                                            CssClass="form-control" runat="server"
                                                            ID="TextBoxMark">   
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        Значащая маска:
                                                    </td>
                                                    <td>
                                                       <asp:TextBox aria-describedby="basic-addon1"
                                                            CssClass="form-control" runat="server" Text="XXXXXXXXXXX000000"
                                                            ID="TextBoxMeaningfulMask">   
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <p runat="server" id="MarkError" visible="false" style="color: red">*Данная марка уже добавлена</p>
                                                 <tr>
                                                    <td align="center">
                                                        <asp:Button CssClass="btn btn-secondary" runat="server" ID="ButtonAddMark" Text="Добавить"
                                                            AutoPostBack="true" OnClick="ButtonAddMark_Click"  />
                                                    </td>
                                                </tr>
                                                
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                     </layouttemplate>
            </asp:Panel>
            <asp:Button ID="FakeOpenButton" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />
            <ajaxToolkit:ModalPopupExtender ID="AddMarkPopUpExtender" runat="server" TargetControlID="FakeOpenButton"
                PopupControlID="AddMarkPopUp" DropShadow="true" CancelControlID="btnfakecancel"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="btnfakecancel" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />



            <%-- Окно с информацией о описателях на маске--%>
            <asp:Panel ID="DesctptionOnMaskPopUp" runat="server" Style="display: none; width: 500px; background-color: #ffffff">

                <table class="TableForVinDescsSubVinMasks">
                    <thead>
                        <tr align="center">
                            <th>
                                <div class="parentForPager">

                                    <div class="child" style="padding-left: 125px">
                                        <h5 align="center">Описатели на Маске</h5>
                                    </div>
                                    <div class="child" style="padding-left: 100px;">
                                        <a align="left">
                                            <asp:Button runat="server" ID="ButtonCloseDesctptionOnMaskPopUp" CssClass="ButtonWidth_CloseButton"
                                                AutoPostBack="true" Text="X" Font-Size="small" OnClick="ButtonCloseDesctptionOnMaskPopUp_Click" /></a>
                            </th>
                        </tr>
                    </thead>
                </table>
                <table class="TableForVinDescsSubVinMasks">
                    <thead>
                        <tr align="center">
                            <th style="width: 80px; font-size: medium">Часть Вина
                            </th>
                            <th style="width: 100px; font-size: medium">Описание части Вина
                            </th>
                            <th style="width: 100px; font-size: medium">Значение части Вина
                            </th>
                        </tr>
                    </thead>
                </table>
                <div id="AdjResultsDiv2">
                    <asp:GridView
                        CssClass="TableForVinDescsSubVinMasks"
                        Width="100%"
                        ID="DescriptionsGridView"
                        runat="server"
                        AutoGenerateColumns="false"
                        ShowHeader="false"
                        ShowHeaderWhenEmpty="true">
                        <EmptyDataRowStyle Font-Size="Medium" />
                        <HeaderStyle Height="5%" CssClass="positionStatic" HorizontalAlign="Center" Font-Size="Large" />
                        <RowStyle Height="5%" Font-Size="Medium" />
                        <Columns>
                            <asp:BoundField ItemStyle-Width="33%" DataField="VinPart" HeaderText="Часть Вина" />
                            <asp:BoundField ItemStyle-Width="33%" DataField="VinPartDecription" HeaderText="Описание части Вина" />
                            <asp:BoundField ItemStyle-Width="33%" DataField="EnumMeaningOfVinParts" HeaderText="Значение части Вина" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="FakeOpenButton2" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />
            <ajaxToolkit:ModalPopupExtender ID="DesctptionOnMaskPopUpExtender" runat="server" TargetControlID="FakeOpenButton2"
                PopupControlID="DesctptionOnMaskPopUp" DropShadow="true" CancelControlID="btnfakecancel2"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="btnfakecancel2" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />




            <%-- Окно с добавлением маски на марку--%>
            <asp:Panel ID="AddMaskOnMarkPopUp" runat="server" Style="display: none; width: 600px; background-color: #ffffff">
                <asp:HiddenField runat="server" ID="hfMarkIdInPopUp" />
                <layouttemplate>
                     
                        <table class="TableForVinDescsSubVinMasks">
                            <thead>
                                <tr align="center">
                                    <th>
                                        <div class="parentForPager">
                                        
                                        <div class="child" style="padding-left:170px">
                                            <h5 align="center">Добавление маски</h5>
                                        </div>
                                        <div class="child" style="padding-left:170px;">
                                            <a align="left"><asp:Button runat="server" ID="ButtonCloseAddMaskOnMarkPopUp" CssClass="ButtonWidth_CloseButton"
                                            AutoPostBack="true" Text="X" Font-Size="small" OnClick="ButtonCloseAddMaskOnMarkPopUp_Click" /></a>
                                    </th>
                                    
                                </tr>
                            </thead>
                         </table>
                     <table class="TableForVinDescsSubVinMasks">
                            <tbody>
                                <tr>
                                    <td>
                                        <a align="center">1</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition1"/>    
                                    </td>
                                    <td>
                                        <a align="center">2</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition2"/>    
                                    </td>
                                    <td>
                                        <a align="center">3</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition3"/>    
                                    </td>
                                    <td>
                                        <a align="center">4</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition4"/>    
                                    </td>
                                    <td>
                                        <a align="center">5</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition5"/>    
                                    </td>
                                    <td>
                                        <a align="center">6</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition6"/>    
                                    </td>
                                    <td>
                                        <a align="center">7</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition7"/>    
                                    </td>
                                    <td>
                                        <a align="center">8</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition8"/>    
                                    </td>
                                    <td>
                                        <a align="center">9</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition9"/>    
                                    </td>
                                    <td>
                                        <a align="center">10</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition10"/>    
                                    </td>
                                    <td>
                                        <a align="center">11</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition11"/>    
                                    </td>
                                    <td>
                                        <a align="center">12</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition12"/>    
                                    </td>
                                    <td>
                                        <a align="center">13</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition13"/>    
                                    </td>
                                    <td>
                                        <a align="center">14</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition14"/>    
                                    </td>
                                    <td>
                                        <a align="center">15</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition15"/>    
                                    </td>
                                    <td>
                                        <a align="center">16</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition16"/>    
                                    </td>
                                    <td>
                                        <a align="center">17</a>
                                        <asp:CheckBox runat="server" ID="checkBoxPosition17"/>    
                                    </td>
                                   
                                    <td align="center">
                                        <asp:Button CssClass="btn btn-secondary" runat="server" ID="ButtonAddMaskOnMark" Text="Добавить"
                                            AutoPostBack="true" OnClick="ButtonAddMaskOnMark_Click"  />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                     </layouttemplate>
            </asp:Panel>
            <asp:Button ID="FakeOpenButton3" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />
            <ajaxToolkit:ModalPopupExtender ID="AddMaskOnMarkPopUpPopupExtender" runat="server" TargetControlID="FakeOpenButton3"
                PopupControlID="AddMaskOnMarkPopUp" DropShadow="true" CancelControlID="btnfakecancel"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="btnfakecancel3" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />


            <%-- Окно с добавлением описателя на маску--%>
            <asp:Panel ID="AddNewDescriptionOnMaskPopUp" runat="server" Style="display: none; width: 600px; background-color: #ffffff">
                <layouttemplate>
                     <table class="TableForVinDescsSubVinMasks">
                            <thead>
                                <tr align="center">
                                    <th>
                                        <div class="parentForPager">
                                        
                                        <div class="child" style="padding-left:170px">
                                            <h5 align="center">Добавление описателя</h5>
                                        </div>
                                        <div class="child" style="padding-left:105px;">
                                            <a align="left"><asp:Button runat="server" ID="ButtonCloseAddNewDescriptionOnMask" CssClass="ButtonWidth_CloseButton"
                                            AutoPostBack="true" Text="X" Font-Size="small" OnClick="ButtonCloseAddNewDescriptionOnMask_Click" /></a>
                                    </th>
                                    
                                </tr>
                            </thead>
                         </table>
                     <table style="width:100%"  class="TableForVinDescsSubVinMasks">
                            <tr style="width:100%">
                                <td style="font-size:medium; width:35%">
                                    <b>Маска</b>
                                </td>
                                <td style="font-size:medium">
                                    <asp:Label runat="server" ID="MaskInAddNewDescriptionOnMaskPopUpLabel"></asp:Label>
                                </td>
                                </tr>
                         <tr style="width:100%">
                                <td style="font-size:medium; width:35%">
                                   <b> Enum вариантон описания</b>
                                </td>
                                <td style="font-size:medium">
                                    <asp:DropDownList runat="server" ID="DropDownOfEnumsInAddNewDescriptionOnMaskPopUp"></asp:DropDownList>
                                </td>
                             </tr>
                         <tr style="width:100%">
                                <td style="font-size:medium; width:35%">
                                    <b>Часть вина</b>
                                </td>
                              <td style="font-size:medium">
                                    <asp:TextBox runat="server" ID="VinPartTextBoxInAddNewDescriptionOnMaskPopUp"></asp:TextBox>
                                </td>
                               
                            </tr>
                         <tr style="width:100%">
                                <td style="font-size:medium; width:35%">
                                    <b>Значение части вина</b>
                                </td>
                               
                                <td style="font-size:medium">
                                    <asp:TextBox runat="server" ID="VinPartDescriptionTextBoxInAddNewDescriptionOnMaskPopUp"></asp:TextBox>
                                </td>
                            </tr>
                          <tr style="width:100%" id="TrOfPossibleDependencies" runat="server" >
                                <td style="font-size:medium; width:35%">
                                    <b>Зависимость от другого описателя</b></br>
                                    <a style="font-size:small">*если нет, оставить пустым</a>
                                </td>
                               
                                <td style="font-size:medium">
                                    <asp:DropDownList runat="server" ID="DropDownOfPossibleDependencies">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                         <tr>
                             
                             <td> 
                                 <asp:Button CssClass="btn btn-secondary" runat="server"
                                    ID="ButtonAddNewVindescriptionOnMask" Text="Добавить"
                                    AutoPostBack="true" OnClick="ButtonAddNewVindescriptionOnMask_Click"  />
                                 </td>
                         </tr>
                     </table>
                 </layouttemplate>
            </asp:Panel>
            <asp:Button ID="FakeOpenButton4" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />
            <ajaxToolkit:ModalPopupExtender ID="AddNewDescriptionOnMaskPopupExtender" runat="server" TargetControlID="FakeOpenButton4"
                PopupControlID="AddNewDescriptionOnMaskPopUp" DropShadow="true" CancelControlID="btnfakecancel"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="btnfakecancel4" runat="server" Text="" Style="display: none" Width="0"
                BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <style>
        th.sortasc a {
            padding: 0 0 0 20px;
            background: url(Images/Arrow_Up.png) no-repeat;
        }

        th.sortdesc a {
            padding: 0 0 0 20px;
            background: url(Images/Arrow_Down.png) no-repeat;
        }

        section {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            color: #fff;
        }

        .paddingsForgrid {
        }

        .main {
            background-color: #27ae60;
        }

        .news {
            background-color: #9b59b6;
        }

        .contacts {
            background-color: #3498db;
        }

        .portfolio {
            background-color: #f1c40f;
        }

        .wrapper {
            position: relative;
            overflow-x: hidden;
        }

        .menu {
            position: fixed;
            left: 0;
            top: 0;
            z-index: 99;
            width: 15%;
            height: 100vh;
            display: flex;
            padding-top: 8%;
            align-items: start;
            justify-content: center;
            background-color: #fff;
            transition: 0.5s;
            transform: translateX(-100%);
        }

        .menu_active {
            transform: translateX(0%);
        }

        .menu-list {
            display: flex;
            justify-content: space-around;
            align-items: center;
            height: 50%;
            flex-direction: column;
        }

            .menu-list a {
                text-decoration: none;
                text-transform: uppercase;
                font-weight: 900;
            }

        .menu-btn {
            width: 30px;
            height: 30px;
            background-color: #333;
            position: absolute;
            right: -35px;
            top: 10%;
        }

        .content {
            transition: 0.5s;
            position: relative;
            z-index: 0;
        }

        .content_active {
            transform: translateX(30%);
        }

        div#AdjResultsDiv {
            width: 100%;
            height: 640px;
            overflow: scroll;
            position: relative;
        }

            div#AdjResultsDiv th {
                top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        div#AdjResultsDiv2 {
            width: 100%;
            height: 250px;
            overflow: scroll;
            position: relative;
        }

            div#AdjResultsDiv2 th {
                top: expression(document.getElementById("AdjResultsDiv2").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        div#AdjResultsDiv3 {
            width: 100%;
            height: 450px;
            overflow: scroll;
            position: relative;
        }

            div#AdjResultsDiv3 th {
                top: expression(document.getElementById("AdjResultsDiv3").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        .positionStatic {
            position: static;
            top: 0;
        }

        .simple-little-table tr {
            text-align: center;
            padding-left: 0px;
            padding: 0,0,0,0;
        }

        .textBoxWidth {
            width: 60px;
            height: 40px;
        }

        .ButtonWidth {
            width: 40px;
            height: 40px
        }

        .ButtonWidth_CloseButton {
            width: 30px;
            height: 30px;
        }

        .ButtonHeight {
            height: 40px
        }

        .parentForPager {
            width: 100%;
            padding-left: 2%;
            padding-top: 5px;
            padding-bottom: 5px;
            font-size: 21px;
        }

        .child {
            display: inline-block;
            float: unset;
        }

        .parent {
            width: 100%;
            padding-left: 2%;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .paddingsForTable {
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
    <style>
        .TableForVinDescsSubVinMasks {
            width: 100%;
            font-family: Arial, Helvetica, sans-serif;
            color: #666;
            font-size: 12px;
            text-shadow: 1px 1px 0px #fff;
            background: #eaebec;
            border: #ccc 1px solid;
            border-collapse: separate;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            -moz-box-shadow: 0 1px 2px #d1d1d1;
            -webkit-box-shadow: 0 1px 2px #d1d1d1;
            box-shadow: 0 1px 2px #d1d1d1;
        }

            .TableForVinDescsSubVinMasks th {
                padding: 5px;
                font-weight: bold;
                border-top: 1px solid #fafafa;
                border-bottom: 1px solid #e0e0e0;
                background: #ededed;
                background: -webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb));
                background: -moz-linear-gradient(top, #ededed, #ebebeb);
            }

                .TableForVinDescsSubVinMasks th:first-child {
                    text-align: left;
                    padding-left: 20px;
                }

            .TableForVinDescsSubVinMasks tr:first-child th:first-child {
                -moz-border-radius-topleft: 3px;
                -webkit-border-top-left-radius: 3px;
                border-top-left-radius: 3px;
            }

            .TableForVinDescsSubVinMasks tr:first-child th:last-child {
                -moz-border-radius-topright: 3px;
                -webkit-border-top-right-radius: 3px;
                border-top-right-radius: 3px;
            }

            .TableForVinDescsSubVinMasks tr {
                text-align: center;
                padding-left: 20px;
            }

                .TableForVinDescsSubVinMasks tr td:first-child {
                    text-align: left;
                    padding-left: 20px;
                    border-left: 0;
                }

                .TableForVinDescsSubVinMasks tr td {
                    padding: 5px;
                    border-top: 1px solid #ffffff;
                    border-bottom: 1px solid #e0e0e0;
                    border-left: 1px solid #e0e0e0;
                    background: #fafafa;
                    background: -webkit-gradient(linear, left top, left bottom, from(#fbfbfb), to(#fafafa));
                    background: -moz-linear-gradient(top, #fbfbfb, #fafafa);
                }

                .TableForVinDescsSubVinMasks tr:nth-child(even) td {
                    background: #f6f6f6;
                    background: -webkit-gradient(linear, left top, left bottom, from(#f8f8f8), to(#f6f6f6));
                    background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
                }

                .TableForVinDescsSubVinMasks tr:last-child td {
                    border-bottom: 0;
                }

                    .TableForVinDescsSubVinMasks tr:last-child td:first-child {
                        -moz-border-radius-bottomleft: 3px;
                        -webkit-border-bottom-left-radius: 3px;
                        border-bottom-left-radius: 3px;
                    }

                    .TableForVinDescsSubVinMasks tr:last-child td:last-child {
                        -moz-border-radius-bottomright: 3px;
                        -webkit-border-bottom-right-radius: 3px;
                        border-bottom-right-radius: 3px;
                    }

            .TableForVinDescsSubVinMasks a:link {
                color: #666;
                font-weight: bold;
                text-decoration: none;
            }

            .TableForVinDescsSubVinMasks a:visited {
                color: #999999;
                font-weight: bold;
                text-decoration: none;
            }

            .TableForVinDescsSubVinMasks a:active,
            .TableForVinDescsSubVinMasks a:hover {
                color: #bd5a35;
                text-decoration: underline;
            }
    </style>
    <style>
        .TableForVinMarks {
            width: 100%;
            font-family: Arial, Helvetica, sans-serif;
            color: #666;
            font-size: 12px;
            text-shadow: 1px 1px 0px #fff;
            background: #eaebec;
            border: #ccc 1px solid;
            border-collapse: separate;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            -moz-box-shadow: 0 1px 2px #d1d1d1;
            -webkit-box-shadow: 0 1px 2px #d1d1d1;
            box-shadow: 0 1px 2px #d1d1d1;
        }

            .TableForVinMarks th {
                padding: 5px;
                font-weight: bold;
                padding: 21px 25px 22px 25px;
                border-top: 1px solid #fafafa;
                border-bottom: 1px solid #e0e0e0;
                border-left: 0px;
                border-right: 0px;
                background: #ededed;
                background: -webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb));
                background: -moz-linear-gradient(top, #ededed, #ebebeb);
            }

                .TableForVinMarks th:first-child {
                    text-align: left;
                    padding-left: 20px;
                }

            .TableForVinMarks tr:first-child th:first-child {
                -moz-border-radius-topleft: 3px;
                -webkit-border-top-left-radius: 3px;
                border-top-left-radius: 3px;
            }

            .TableForVinMarks tr:first-child th:last-child {
                -moz-border-radius-topright: 3px;
                -webkit-border-top-right-radius: 3px;
                border-top-right-radius: 3px;
            }

            .TableForVinMarks tr {
                text-align: center;
                padding-left: 20px;
            }

                .TableForVinMarks tr td:first-child {
                    text-align: left;
                    padding-left: 20px;
                    border-left: 0;
                }

                .TableForVinMarks tr td {
                    padding: 10px;
                    border-top: 0px solid #ffffff;
                    border-bottom: 0px solid #e0e0e0;
                    border-left: 0px solid #e0e0e0;
                    border-right: 0px;
                    background: #fafafa;
                    background: -webkit-gradient(linear, left top, left bottom, from(#fbfbfb), to(#fafafa));
                    background: -moz-linear-gradient(top, #fbfbfb, #fafafa);
                }

                .TableForVinMarks tr:nth-child(even) td {
                    background: #f6f6f6;
                    background: -webkit-gradient(linear, left top, left bottom, from(#f9f9f9), to(#f3f3f3));
                    background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
                }

                .TableForVinMarks tr:last-child td {
                    border-bottom: 0;
                }

                    .TableForVinMarks tr:last-child td:first-child {
                        -moz-border-radius-bottomleft: 3px;
                        -webkit-border-bottom-left-radius: 3px;
                        border-bottom-left-radius: 3px;
                    }

                    .TableForVinMarks tr:last-child td:last-child {
                        -moz-border-radius-bottomright: 3px;
                        -webkit-border-bottom-right-radius: 3px;
                        border-bottom-right-radius: 3px;
                    }

            .TableForVinMarks a:link {
                color: #666;
                font-weight: bold;
                text-decoration: none;
            }

            .TableForVinMarks a:visited {
                color: #999999;
                font-weight: bold;
                text-decoration: none;
            }

            .TableForVinMarks a:active,
            .TableForVinMarks a:hover {
                color: #bd5a35;
                text-decoration: underline;
            }
    </style>
</asp:Content>
