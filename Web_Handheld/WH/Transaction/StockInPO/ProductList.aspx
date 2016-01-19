<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="WH_Transaction_StockInPO_ProductList" Title="Untitled Page" %>

<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar1" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" 
                    BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false"  BtnViewShow="false"
                    BtnSubmitShow="false" BtnHelpShow="false" OnNewClick="NewClick" NameBtnNew="�����ѵ�شԺ" />
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" 
                    BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false"  BtnViewShow="false"
                    BtnSubmitShow="true" BtnHelpShow="false" OnNewClick="NewPOClick" OnSubmitClick="SubmitClick" NameBtnNew="�������觫���" NameBtnSubmit="��QC" />
            </td> 
        </tr> 
        <tr>
            <td height="150px"  valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="20">
                                <b>&nbsp;�Ţ���㺹��� : </b>
                                <asp:Label ID="lblCode" runat="server" ></asp:Label></td> 
                        </tr> 
                        <tr>
                            <td height="20">
                                <b>&nbsp;�Ţ�����觢ͧ : </b>
                                <asp:Label ID="lblInvNo" runat="server" ></asp:Label></td> 
                        </tr> 
                        <tr>
                            <td height="20">
                                <b>&nbsp;����˹��� : </b>
                                <asp:Label ID="lblSupplierName" runat="server" ></asp:Label></td> 
                        </tr> 
                        <tr>
                            <td height="20">
                                <b>&nbsp;�Ţ������觫��� :
                                    <asp:DropDownList ID="cmbPR" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                        Width="110px" OnSelectedIndexChanged="cmbPR_SelectedIndexChanged">
                                    </asp:DropDownList></b></td> 
                        </tr> 
                        <tr>
                            <td>
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                    DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>"
                                    Width="220px" OnSelectedIndexChanged="grvData_SelectedIndexChanged" OnRowDataBound="grvData_RowDataBound" OnRowCommand="grvData_RowCommand">
                                    <PagerSettings Visible="False" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDetail" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_View.gif" CommandName="view" AlternateText="��������´" CommandArgument="" />
                                                <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_delete.gif" CommandName="select" AlternateText="ź��¡��" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LOID">
                                            <ControlStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NAME" HeaderText="�ѵ�شԺ">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QCQTY" HeaderText="�ӹǹ" DataFormatString="{0:#,##0}" HtmlEncode="false">
                                            <ItemStyle HorizontalAlign="right" Width="40px" />
                                            <HeaderStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UNITNAME" HeaderText="˹���">
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle Width="50px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="t_selectstyle" />
                                    <HeaderStyle CssClass="t_headtext" />
                                    <AlternatingRowStyle CssClass="t_alt_bg" />
                                </asp:GridView>
                            </td> 
                        </tr> 
                    </table>
                </asp:Panel> 
                <asp:Panel ID="pnlMessage" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100px">
                        <tr>
                            <td height="75px">
                            </td> 
                        </tr>
                        <tr>
                            <td class="messageLayer" valign="bottom">
                                <asp:Panel ID="pnlSave" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="220px">
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                        <tr height="18">
                                            <td width="50" align="center" valign="top">
                                                <img src="../../../Images/msg_Question.gif" /></td>
                                            <td>
                                                <b>�׹�ѹ����觵�Ǩ QC</b></td> 
                                        </tr> 
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                    </table>
                                </asp:Panel> 
                                <asp:Panel ID="pnlDelete" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="220px">
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                        <tr height="18">
                                            <td width="50" align="center" valign="top">
                                                <img src="../../../Images/msg_Question.gif" /></td>
                                            <td>
                                                <b>��ͧ���ź��¡���ѵ�شԺ</b><br />
                                                &nbsp;&nbsp; "<asp:Label ID="lblProductName" runat="server"></asp:Label>"&nbsp;
                                                <b>��������� ?</b>
                                            </td> 
                                        </tr> 
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                    </table>
                                </asp:Panel> 
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
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSave_Click" /></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="¡��ԡ" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
   <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="70px"></asp:TextBox> 
   <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="70px"></asp:TextBox> 
    
  
</asp:Content>