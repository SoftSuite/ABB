<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinReturnSearch.aspx.cs" Inherits="FG_Transaction_StockinReturnSearch" Title="��Ѻ�׹�Թ���"%>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;��Ѻ�׹�Թ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�׹�ѹ"
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
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �������Ѻ�׹</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbDocType" runat="server" CssClass="zComboBox" Width="150px">
                            </asp:DropDownList></td>
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ�����Ѻ�׹</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>                            
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����Ѻ�׹</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="50px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>                   
                    <tr height="25px" id="trCustomer" runat="server">
                        <td width="50px"></td>
                        <td width="150px">
                            �١���</td>
                        <td colspan="3" width="340px">
                            <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zComboBox" Width="325px">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ����</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCName" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="50px">
                            ���ʡ��</td>
                        <td style="width: 170px"><asp:TextBox ID="txtCLName" runat="server" CssClass="zTextbox" Width="150px">
                        </asp:TextBox></td>
                        <td></td>                            
                    </tr>  
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ʶҹ�</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px" />
                        </td>
                        <td width="50px" align="center">
                            �֧</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="50px"></td>
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
                <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound">
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
                            </ItemTemplate>
                            <ItemStyle Width="30px"/>
                            <HeaderStyle Width="30px"/>
                            <FooterStyle Width="30px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ӴѺ">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
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
                        <asp:BoundField SortExpression="DOCNAME" HeaderText = "�������Ѻ�׹" DataField="DOCNAME">
                            <ItemStyle Width="120px" HorizontalAlign="center"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField> 
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="StockinReturn.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ�����Ѻ�׹" DataTextFormatString="{0}" >
                            <ItemStyle Width="130px" HorizontalAlign="center"/>
                            <HeaderStyle Width="130px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RECEIVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����Ѻ�׹" DataField="RECEIVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>                   
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "�١���" DataField="CUSTOMERNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="GRANDTOT" HeaderText = "�ʹ���" DataField="GRANDTOT" HtmlEncode="false" DataFormatString="{0:#,##0.00}">
                            <ItemStyle Width="80px" HorizontalAlign="right"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEBY" HeaderText = "��ѡ�ҹ���" DataField="CREATEBY">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "ʶҹ�" DataField="STATUSNAME">
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>    
</asp:Content>