<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="StockInList.aspx.cs" Inherits="WH_Transaction_StockInPD_StockInList" Title="�Ѻ�ѵ�شԺ�ҡ���¼�Ե" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" 
                    BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="true" BtnViewShow="false" 
                    OnNewClick="NewClick" OnHelpClick="HelpClick"/>
            </td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                        DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" OnSelectedIndexChanged="grvData_SelectedIndexChanged"
                        Width="220px">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_Submit.gif" CommandName="select" AlternateText="���͡��¡��" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="�Ţ���㺹���">
                                <ItemStyle HorizontalAlign="center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="APPROVEDATE" DataFormatString="{0 : dd/MM/yyyy}" HtmlEncode="False" HeaderText="�ѹ���">
                                <ItemStyle Width="70px" />
                                <HeaderStyle Width="70px" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" />
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnlMessage" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="75px"></td> 
                        </tr>
                        <tr>
                            <td class="messageLayer" valign="bottom">
                                <table border="0" cellpadding="0" cellspacing="0" width="220px">
                                    <tr height="3">
                                        <td colspan="3"></td> 
                                    </tr>
                                    <tr height="18">
                                        <td rowspan="4" width="30" align="center" valign="top">
                                            <img src="../../../Images/msg_Question.gif" /></td>
                                        <td colspan="2">
                                            <b>���ҧ㺹����ѵ�شԺ��Ҥ�ѧ����������� ?</b>
                                        </td> 
                                    </tr> 
                                    <tr height="3">
                                        <td colspan="2"></td> 
                                    </tr>
                                    <tr height="18">
                                        <td>&nbsp;&nbsp;<b>�Ţ��� : </b>
                                            <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                                        <td>
                                        </td> 
                                    </tr> 
                                    <tr height="18">
                                        <td>&nbsp;&nbsp;<b>�ѹ��� : </b>
                                            <asp:Label ID="lblDate" runat="server"></asp:Label></td> 
                                        <td>
                                        </td> 
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
                <table border="0" id="test" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="btnSelect" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSelect_Click"/></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="��Ѻ����" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
                <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></td> 
        </tr> 
    </table>


</asp:Content>