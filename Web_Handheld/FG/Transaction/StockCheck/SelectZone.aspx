<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="SelectZone.aspx.cs" Inherits="FG_Transaction_StockCheck_SelectZone" Title="ตรวจนับสินค้า" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false" BtnDeleteShow="false" 
                    BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="true"  BtnViewShow="false"
                    BtnSubmitShow="false" BtnHelpShow="false" OnBackClick="BackClick" />
            </td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr height="5">
                        <td>
                        </td>
                    </tr>
                    <tr height="20px">
                        <td>
                            &nbsp;<b>Batch No : </b><asp:Label ID="lblBatchNo" runat="server" ></asp:Label>
                        </td>
                    </tr> 
                    <tr height="20px">
                        <td>
                            &nbsp;<b>Location : </b><asp:Label ID="lblWarehouseName" runat="server"></asp:Label>
                        </td>
                    </tr> 
                </table>
                <hr />
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr height="20px">
                        <td>
                            <b>&nbsp;โซน : </b>
                                <asp:DropDownList ID="cmbLocation" runat="server" CssClass="zComboBox" Width="120px">
                                </asp:DropDownList></td>
                    </tr>
                </table> 
            </td> 
        </tr>
        <tr>
            <td class="subheadertext">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click" /></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
   <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="70px"></asp:TextBox>
</asp:Content>