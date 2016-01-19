<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="AddProductQC.aspx.cs" Inherits="FG_Transaction_StockInPO_AddProductQC" Title="รับสินค้าจากผู้จำหน่าย" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" 
                    BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="false" BtnViewShow="true"
                    OnBackClick="BackClick" OnViewClick="ViewClick" NameBtnView="แสดงรายการ"/>
            </td> 
        </tr> 
        <tr>
            <td >
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="150" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="200">
                                    <tr height="20">
                                        <td width="5">
                                        </td> 
                                        <td style="width: 89px">
                                            <b>เลขที่ใบตรวจรับ :</b></td> 
                                        <td style="width: 106px">
                                            <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                                    </tr>
                                    <tr height="20">
                                        <td width="5">
                                        </td>
                                        <td style="width: 89px">
                                            <b>วันที่ :</b></td>
                                        <td style="width: 106px">
                                            <asp:Label ID="lblReceiveDate" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr height="20">
                                        <td width="5">
                                        </td>
                                        <td style="width: 89px">
                                            <b>เลขที่ใบแจ้ง QC :</b></td>
                                        <td style="width: 106px">
                                            <asp:Label ID="lblQCCode" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                                <hr />
                                <table border="0" cellpadding="0" cellspacing="0" width="200">
                                    <tr height="20">
                                        <td width="5">
                                        </td>
                                        <td style="width: 80px">
                                            <b>บาร์โค้ดสินค้า :</b></td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Width="105px" AutoPostBack="true" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr height="20">
                                        <td width="5">
                                        </td>
                                        <td style="width: 80px">
                                            <b>ชื่อสินค้า :</b></td>
                                        <td style="width: 120px">
                                            <asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr height="20">
                                        <td width="5">
                                        </td>
                                        <td style="width:80px">
                                            <b>จำนวนรับ :</b></td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="70px"></asp:TextBox>
                                            <b><asp:Label ID="lblUnitName" runat="server"></asp:Label></b></td>
                                    </tr>
                                </table>
                            </td>
                        </tr> 
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlMessage" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="75"></td> 
                        </tr>
                        <tr>
                            <td class="messageLayer" valign="bottom">
                                <table border="0" cellpadding="0" cellspacing="0" width="220">
                                    <tr height="3">
                                        <td colspan="2"></td> 
                                    </tr>
                                    <tr height="18">
                                        <td width="50" align="center" valign="top">
                                            <img src="../../../Images/msg_Error.gif" /></td>
                                        <td>
                                            <b><asp:Label ID="lblError" runat="server"></asp:Label></b>
                                            </td> 
                                    </tr> 
                                    <tr height="3">
                                        <td colspan="2"></td> 
                                    </tr>
                                </table>
                            </td> 
                        </tr> 
                    </table> 
                </asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
   <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox> 
   <asp:TextBox ID="txtStockInItem" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox>
   
</asp:Content>