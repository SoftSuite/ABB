<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="WH_Transaction_StockInPO_AddProduct" Title="รับวัตถุดิบจากผู้จำหน่าย" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" 
                    BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="false" BtnViewShow="true"
                    OnBackClick="BackClick" OnViewClick="ViewClick" OnNewClick="NewClick" NameBtnBack="กลับ" NameBtnNew="เพิ่มใบสั่งซื้อ" NameBtnView="แสดง"/>
            </td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
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
                                <b>เลขที่ใบส่งของ :</b></td>
                            <td style="width: 106px">
                                <asp:Label ID="lblInvNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 89px">
                                <b>เลขที่ใบสั่งซื้อ :</b></td>
                            <td style="width: 106px">
                                <asp:Label ID="lblPDOrderCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 89px">
                                <b>ผู้จำหน่าย :</b></td>
                            <td style="width: 106px">
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
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
                                <b>หน่วย :</b></td>
                            <td style="width: 120px">
                                <b><asp:Label ID="lblUnitName" runat="server"></asp:Label></b></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 80px">
                                <b>Lot No. :</b></td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox" Width="105px" ></asp:TextBox></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width:80px">
                                <b>จำนวนรับ :</b></td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width:80px">
                                <b>จำนวนส่ง QC :</b></td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtQCQty" runat="server" CssClass="zTextboxR" Width="70px"></asp:TextBox>
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
   <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox> 
    <asp:TextBox ID="txtPDOrder" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox>
    <asp:TextBox ID="txtPOItem" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox>
    <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox>
    <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Width="45px"></asp:TextBox>
   
   
</asp:Content>