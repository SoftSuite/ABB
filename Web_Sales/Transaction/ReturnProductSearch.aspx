<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ReturnProductSearch.aspx.cs" Inherits="Transaction_ReturnProductSearch" Title="��Ѻ�׹�Թ��ҽҡ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;��Ѻ�׹�Թ��ҽҡ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�觤�ѧ"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;����</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 182px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ�����Ѻ�׹�ҡ���</td>
                        <td style="width: 182px">
                            <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����͡��Ѻ�׹�ҡ���</td>
                        <td style="width: 182px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �����١���</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ʶҹ�</td>
                        <td style="width: 182px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 182px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowCommand="grvRequisition_RowCommand" OnRowDataBound="grvRequisition_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
<asp:TemplateField><HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            
</HeaderTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="25px"></HeaderStyle>
<ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False">
<ItemStyle Width="60px"></ItemStyle>

<HeaderStyle Width="60px"></HeaderStyle>
<ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="�����"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                
 </ItemTemplate>

<FooterStyle Width="60px"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="�ӴѺ���">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:HyperLinkField HeaderText="�Ţ�����Ѻ�׹�ҡ���" DataTextField="CODE" DataTextFormatString="{0}" DataNavigateUrlFormatString="ReturnProduct.aspx?loid={0}" DataNavigateUrlFields="LOID">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</asp:HyperLinkField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="REQDATE" SortExpression="REQDATE" HeaderText="ŧ�ѹ���">
<ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUSTOMERNAME" SortExpression="CUSTOMERNAME" HeaderText="�١���">
<ItemStyle Width="150px"></ItemStyle>

<HeaderStyle Width="150px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="STATUSNAME" SortExpression="STATUSNAME" HeaderText="ʶҹ�">
<ItemStyle Width="130px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="130px" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CREATEBY" SortExpression="CREATEBY" HeaderText="��ѡ�ҹ���">
<ItemStyle Width="130px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="130px" HorizontalAlign="Center"></HeaderStyle>
</asp:BoundField>
</Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>