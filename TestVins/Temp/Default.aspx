<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VinApp._Default" Culture="auto:en-US" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <link href="Css/MainCSS.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1"
        runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table style="width: 100%">
                            <tr style="width: 100%">
                                <td style="width: 90%">
                                    <h1 align="center" style="font-family: Arial, Helvetica, sans-serif;">Таблица расшифрованных винов</h1>

                                    <div class="wrapper">
                                        <div class="menu" id="divMenu" runat="server">
                                            <img src="Images/Лупа.png" title="Фильтры" class="menu-btn" />
                                            <table class="simple-little-table paddingsForTable">
                                                <thead>
                                                    <td>
                                                        <h4 align="center">
                                                        Фильтры:</td>
                                                </thead>
                                                <tr>
                                                    <td style="font-size: large">Марка:
                                                        <br />
                                                        <input id="marks" autocomplete="off" aria-describedby="basic-addon1"
                                                            class="form-control" onchange="changeInput(this)" />
                                                        <asp:HiddenField runat="server" ID="hfMarks" />
                                                        <p style="font-size: small">*начните вводить марку</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: large">Модель:
                                                        <br />
                                                        <input id="models" autocomplete="off" aria-describedby="basic-addon1"
                                                            class="form-control" onchange="changeInputModels(this)" />
                                                        <asp:HiddenField runat="server" ID="hfModels" />
                                                        <p style="font-size: small">*начните вводить модель</p>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: large">
                                                        <asp:Button runat="server" ID="FilterButton" OnClick="FilterButton_Click"
                                                            CssClass="btn btn-secondary" Text="Применить" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <asp:GridView
                                        runat="server" ID="fakeGridView"
                                        CssClass="MyTable"
                                        Visible="true"
                                        AutoGenerateColumns="false"
                                        AllowSorting="true"
                                        AllowPaging="true"
                                        PagerSettings-Visible="false"
                                        OnSorting="VinsGrid_Sorting"
                                        ShowHeaderWhenEmpty="true"
                                        EnableSortingAndPagingCallbacks="true"
                                        BorderWidth="0">
                                        <PagerTemplate></PagerTemplate>
                                        <EmptyDataRowStyle Font-Size="Medium" />
                                        <HeaderStyle Height="5%" HorizontalAlign="Center" Font-Size="Large" />
                                        <RowStyle Height="5%" Font-Size="Medium" />
                                        <PagerSettings Position="Bottom" />
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="27.82%" ItemStyle-Width="28.3%" HeaderText="VIN" SortExpression="VinValue">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="fakeLabelVinValue" Text='<%#Bind("VinValue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="13.0%" ItemStyle-Width="13.4%" HeaderText="Марка" SortExpression="Mark">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="fakeLabelMark" Text='<%#Bind("Mark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="12.2%" ItemStyle-Width="12.3%" HeaderText="Модель" SortExpression="Model">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="fakeLabelModel" Text='<%#Bind("Model") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="12.2%" ItemStyle-Width="12.41%" HeaderText="Серийный Номер" SortExpression="SerialNumber">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="fakeLabelSerialNumber" Text='<%#Bind("SerialNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="31.8%" ItemStyle-Width="31.8%">
                                                <ItemTemplate>
                                                    <asp:Button CssClass="btn btn-secondary"
                                                        runat="server"
                                                        ID="fakePopUpButton"
                                                        Text="Полная информация"
                                                        OnClick="PopUpButton_Click"
                                                        CommandArgument='<%#Bind("VinValue") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <div id="AdjResultsDiv">
                                        <asp:GridView
                                            runat="server" ID="VinsGrid"
                                            CssClass="MyTable"
                                            Visible="true"
                                            AutoGenerateColumns="false"
                                            PageSize="20"
                                            OnPageIndexChanging="VinsGrid_PageIndexChanging"
                                            AllowSorting="true"
                                            AllowPaging="true"
                                            PagerSettings-Visible="false"
                                            OnSorting="VinsGrid_Sorting"
                                            ShowHeaderWhenEmpty="true"
                                            EmptyDataText="Нет винов"
                                            EnableSortingAndPagingCallbacks="true"
                                            ShowHeader="false">
                                            <PagerTemplate></PagerTemplate>
                                            <EmptyDataRowStyle Font-Size="Medium" />
                                            <HeaderStyle Height="5%" HorizontalAlign="Center" Font-Size="Large" />
                                            <RowStyle Height="5%" Font-Size="Medium" />
                                            <PagerSettings Position="Bottom" />
                                            <Columns>

                                                <asp:TemplateField HeaderStyle-Width="28.3%" ItemStyle-Width="28.3%" HeaderText="VIN" SortExpression="VinValue">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelVinValue" Text='<%#Bind("VinValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="13.4%" ItemStyle-Width="13.4%" HeaderText="Марка" SortExpression="Mark">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelMark" Text='<%#Bind("Mark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="12.3%" ItemStyle-Width="12.3%" HeaderText="Модель" SortExpression="Model">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelModel" Text='<%#Bind("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="12.41%" ItemStyle-Width="12.41%" HeaderText="Серийный Номер" SortExpression="SerialNumber">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="LabelSerialNumber" Text='<%#Bind("SerialNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="31.8%" ItemStyle-Width="31.8%">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-secondary"
                                                            runat="server"
                                                            ID="PopUpButton"
                                                            Text="Полная информация"
                                                            OnClick="PopUpButton_Click"
                                                            CommandArgument='<%#Bind("VinValue") %>' />
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
                                            <asp:Button runat="server"
                                                ID="ButtonDoubleArrowLeft"
                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                AutoPostBack="true" Text="<<"
                                                Font-Size="Large" OnClick="ButtonDoubleArrowLeft_Click" />
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server"
                                                ID="ButtonArrowLeft"
                                                CssClass="ButtonWidth
                                                btn btn-outline-dark"
                                                AutoPostBack="true"
                                                Text="<"
                                                Font-Size="Large"
                                                OnClick="ButtonArrowLeft_Click" />
                                        </div>
                                        <div class="child" style="font-size: Medium;">
                                            <asp:TextBox TextMode="Number"
                                                runat="server" CssClass="textBoxWidth"
                                                ID="InputPager" AutoPostBack="true"
                                                OnTextChanged="InputPager_TextChanged"></asp:TextBox>
                                        </div>
                                        <div style="font-size: large" class="child">
                                            из
                                        </div>
                                        <div style="font-size: large" class="child">
                                            <asp:Label runat="server" ID="PageCount"></asp:Label>
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtonRightArrow"
                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                AutoPostBack="true" Text=">" Font-Size="Large"
                                                OnClick="ButtonRightArrow_Click" />
                                        </div>
                                        <div class="child">
                                            <asp:Button runat="server" ID="ButtondoubleArrowRight"
                                                CssClass="ButtonWidth btn btn-outline-dark"
                                                AutoPostBack="true" Text=">>" Font-Size="Large"
                                                OnClick="ButtondoubleArrowRight_Click" />
                                        </div>
                                        <div class="child">
                                            <a style="font-size: medium">Применено фильтров - </a>
                                            <b>
                                                <asp:Label Font-Size="medium" runat="server" ID="LabelCountOfFilters"></asp:Label></b>

                                        </div>
                                        <div class="child">
                                            <asp:Button Font-Size="Small"
                                                CssClass="ButtonHeight btn btn-outline-dark"
                                                runat="server" Text="Сбросить все фильтры"
                                                ID="ResetFiltersButton"
                                                OnClick="ResetFiltersButton_Click" />
                                        </div>
                                        <div class="child">
                                            <a style="font-size: medium">Размер страницы - </a>
                                            <asp:DropDownList CssClass="textBoxWidth dropDownListTextStyle"
                                                runat="server"
                                                OnSelectedIndexChanged="VinsGridPageSizeDropDown_SelectedIndexChanged"
                                                AutoPostBack="true"
                                                ID="VinsGridPageSizeDropDown">
                                                <asp:ListItem Selected="True">20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>

                                                <asp:ListItem>80</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <td></td>

        </ContentTemplate>

    </asp:UpdatePanel>



    <script>
        function pageLoad(sender, args) {

            $('.menu-btn').on('click', function (e) {
                e.preventDefault();
                $('.menu').toggleClass('menu_active');
                $('.content').toggleClass('content_active');
            })
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Vin.asmx/GetMarksByMarkPart",
                data: JSON.stringify({ markPart: $('#marks').val() }),
                datatype: "json",
                success: function (data) {
                    if (data.d) {
                        var content = $.parseJSON(data.d);
                        $('#marks').autocomplete({
                            source: content
                        });

                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    alert(errorthrown);
                }
            });
        }

        function changeInput(sender) {
            $('#<%=hfMarks.ClientID%>').val($('#marks').val());
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Vin.asmx/GetModelsByMark",
                data: JSON.stringify({ mark: $('#' + sender.id).val() }),
                datatype: "json",
                success: function (data) {
                    if (data.d) {
                        var content = $.parseJSON(data.d);

                        $('#models').autocomplete({
                            source: content
                        });
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    alert(errorthrown);
                }
            })
        }
        function changeInputModels(sender) {
            $('#<%=hfModels.ClientID%>').val($('#models').val());
        }

    </script>
    <style>
        th.sortasc a {
            padding: 0 0 0 20px;
            background: url(Images/Arrow_Up.png) no-repeat;
        }

        .displayNone {
            display: none;
        }

        th.sortdesc a {
            padding: 0 0 0 20px;
            background: url(Images/Arrow_Down.png) no-repeat;
        }

        .dropDownListTextStyle {
            font-size: medium;
        }

        section {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            color: #fff;
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

        .z-index {
            position: absolute;
            z-index: 9999;
            background-color: white;
            width: 10%;
            display: none;
            border: 1px;
            box-shadow: 1px;
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

        .positionStatic {
            position: fixed;
        }

        .simple-little-table tr {
            text-align: center;
            padding-left: 0px;
        }

        .textBoxWidth {
            width: 60px;
            height: 40px;
        }

        .ButtonWidth {
            width: 40px;
            height: 40px
        }

        .DropDownL35t_width {
            height: 35px;
            font-size: 5px;
            font-display: block;
            font-weight: 100;
            align-content: flex-start;
            align-items: flex-start;
            padding-left: 2px;
            padding-left: 2px;
        }

        .ButtonHeight {
            height: 40px
        }

        .div_width {
            width: 10%;
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
        .MyTable {
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

            .MyTable th {
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

                .MyTable th:first-child {
                    text-align: left;
                    padding-left: 20px;
                }

            .MyTable tr:first-child th:first-child {
                -moz-border-radius-topleft: 3px;
                -webkit-border-top-left-radius: 3px;
                border-top-left-radius: 3px;
            }

            .MyTable tr:first-child th:last-child {
                -moz-border-radius-topright: 3px;
                -webkit-border-top-right-radius: 3px;
                border-top-right-radius: 3px;
            }

            .MyTable tr {
                text-align: center;
                padding-left: 20px;
            }

                .MyTable tr td:first-child {
                    text-align: left;
                    padding-left: 20px;
                    border-left: 0;
                }

                .MyTable tr td {
                    padding: 10px;
                    border-top: 0px solid #ffffff;
                    border-bottom: 0px solid #e0e0e0;
                    border-left: 0px solid #e0e0e0;
                    border-right: 0px;
                    background: #fafafa;
                    background: -webkit-gradient(linear, left top, left bottom, from(#fbfbfb), to(#fafafa));
                    background: -moz-linear-gradient(top, #fbfbfb, #fafafa);
                }

                .MyTable tr:nth-child(even) td {
                    background: #f6f6f6;
                    background: -webkit-gradient(linear, left top, left bottom, from(#f9f9f9), to(#f3f3f3));
                    background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
                }

                .MyTable tr:last-child td {
                    border-bottom: 0;
                }

                    .MyTable tr:last-child td:first-child {
                        -moz-border-radius-bottomleft: 3px;
                        -webkit-border-bottom-left-radius: 3px;
                        border-bottom-left-radius: 3px;
                    }

                    .MyTable tr:last-child td:last-child {
                        -moz-border-radius-bottomright: 3px;
                        -webkit-border-bottom-right-radius: 3px;
                        border-bottom-right-radius: 3px;
                    }

                .MyTable tr:hover td {
                    background: #f2f2f2;
                    background: -webkit-gradient(linear, left top, left bottom, from(#f2f2f2), to(#f0f0f0));
                    background: -moz-linear-gradient(top, #f2f2f2, #f0f0f0);
                }

            .MyTable a:link {
                color: #666;
                font-weight: bold;
                text-decoration: none;
            }

            .MyTable a:visited {
                color: #999999;
                font-weight: bold;
                text-decoration: none;
            }

            .MyTable a:active,
            .MyTable a:hover {
                color: #bd5a35;
                text-decoration: underline;
            }
    </style>
    <script>
</script>
</asp:Content>
