<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master"  AutoEventWireup="true" CodeFile="ProductRequestInShopSearch.aspx.cs" Inherits="Web_POS_ProductRequestInShopSearch"  Title="㺢��ԡ����" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;㺢��ԡ����</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�觤�ѧ������ٻ"
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
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                  
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ���</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
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
                            �ѹ�����觼�Ե</td>
                        <td width="150px">
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
                            ʶҹ�</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
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
                        <td width="150px"></td>
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
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="�����"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                <asp:ImageButton ID="btnCopy" AlternateText="�Ѵ�͡"
                                        runat="server" CausesValidation="False" CommandName="copy" ImageUrl="~/Images/icn_copy.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="40px"/>
                            <HeaderStyle Width="40px"/>
                            <FooterStyle Width="40px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ӴѺ">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductRequestInShop.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ�����ԡ" DataTextFormatString="{0}" >
                            <ItemStyle HorizontalAlign="center"/>
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RESERVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���<br>���ԡ" DataField="RESERVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="SELLER" HeaderText = "����˹���" DataField="SELLER">
                            <ItemStyle Width="220px"/>
                            <HeaderStyle Width="220px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="NETPRICE" HeaderText = "�ʹ�ط��" DataField="NETPRICE" HtmlEncode="false" DataFormatString="{0:#,##0.00}">
                            <ItemStyle Width="80px" HorizontalAlign="right"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="STATUS" HeaderText = "ʶҹ�" DataField="STATUS">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="CREATEBY" HeaderText = "��ѡ�ҹ" DataField="CREATEBY">
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>


