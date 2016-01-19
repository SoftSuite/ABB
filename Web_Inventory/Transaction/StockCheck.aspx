<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockCheck.aspx.cs" Inherits="Transaction_StockCheck" Title="ตรวจนับสินค้า" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ตรวจนับสินค้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false"
                    OnSaveClick="SaveClick" OnCancelClick="CancelClick" OnBackClick="BackClick" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td style="height: 10px">
            </td> 
        </tr>
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px">
                    <tr style="height:5px">
                        <td style="width:5px;"></td>
                        <td style="width:100px"></td>
                        <td style="width:135px"></td>
                        <td></td>
                    </tr>
                    <tr style="height:5px">
                        <td style="width:5px;"></td>
                        <td style="width:100px">Batch No:</td>
                        <td style="width:135px"><asp:TextBox ID="txtBatchNo" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="130px" /></td>
                        <td></td>
                    </tr> 
                    <tr style="height:5px">
                        <td style="width:5px;"></td>
                        <td style="width:100px">วันที่ตรวจนับ:</td>
                        <td style="width:135px">
                            <asp:TextBox ID="txtCheckDate" runat="server" CssClass="zTextbox" ReadOnly="True" Width="130px" />
                            <uc2:DatePickerControl ID="ctlCheckDate" runat="server" Enabled="false" Visible="false" /></td>
                        <td><asp:TextBox ID="txtHour" runat="server" CssClass="zHidden" ReadOnly="True" Width="30px" />
                            <asp:TextBox ID="txtMinute" runat="server" CssClass="zHidden" ReadOnly="True" Width="30px" />
                            <asp:TextBox ID="txtSecond" runat="server" CssClass="zHidden" ReadOnly="True" Width="30px" /></td>
                    </tr>
                    <tr style="height:5px">
                        <td style="width:5px;"></td>
                        <td style="width:100px">คลัง:</td>
                        <td style="width:135px"><asp:DropDownList ID="cmbWarehouseName" runat="server" CssClass="zComboBox" Width="130px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:5px">
                        <td style="width:5px;"></td>
                        <td style="width:100px"></td>
                        <td style="width:135px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>        
    </table>
</asp:Content>