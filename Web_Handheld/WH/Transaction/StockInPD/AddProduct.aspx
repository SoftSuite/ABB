<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="WH_Transaction_StockInPD_AddProduct" Title="�Ѻ�ѵ�شԺ�ҡ���¼�Ե" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" 
                    BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="false" BtnViewShow="true"
                    OnBackClick="BackClick" OnViewClick="ViewClick"/>
            </td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="200">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 75px">
                                <b>�Ţ���㺹���</b></td> 
                            <td style="width: 120px">
                                <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�Թ���</b></td>
                            <td style="width: 120px">
                                <asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr />
                    <table border="0" cellpadding="0" cellspacing="0" width="200">
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�������Թ���</b></td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Width="120px" AutoPostBack="true" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�Ţ����ü�Ե</b></td>
                            <td style="width: 120px">
                                <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zCombobox" Width="120px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width:75px">
                                <b>�ӹǹ�Ѻ</b></td>
                            <td style="width: 120px">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="70px"></asp:TextBox>
                                <b><asp:Label ID="lblUnitName" runat="server"></asp:Label></b></td>
                        </tr>
                    </table>
                    <hr />
                </asp:Panel>
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <table border="0" cellspacing="0" cellpadding="0" width="220" height="150">
                        <tr>
                            <td height="75"></td> 
                        </tr>
                        <tr class="messageLayer" valign="bottom">
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="2" height="3"></td> 
                                    </tr>
                                    <tr>
                                        <td width="50" align="center" valign="middle" height="70">
                                            <img src="../../../Images/msg_Error.gif" /></td>
                                        <td>
                                            <b>
                                                <asp:Label ID="lblError" runat="server"></asp:Label></b></td> 
                                    </tr> 
                                    <tr>
                                        <td colspan="2" headers="2px"></td> 
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
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="¡��ԡ" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
   <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox> 
   <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox> 
   <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox> 
    <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
</asp:Content>