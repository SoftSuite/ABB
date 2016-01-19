<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutBasketSearch.aspx.cs" Inherits="FG_Transaction_StockoutBasketSearch" Title="�ԡ/�׹������"%>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;�ԡ/�׹������</td> 
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
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ����ԡ/�׹������</td>
                        <td width="150px">
                            <asp:TextBox ID="txtBasketCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>                            
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����ԡ/�׹������</td>
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
                            ���͡�����</td>
                        <td colspan = "3" width="340px">
                            <asp:TextBox ID="txtBasketName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox>
                        </td>
                        <td></td>                            
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �����ԡ</td>
                        <td colspan = "3" width="340px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox>
                        </td>
                        <td></td>                            
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ʶҹ�</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px" />
                        </td>
                        <td width="20px" align="center">
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
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="StockoutBasket.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ����ԡ/�׹" DataTextFormatString="{0}" >
                            <ItemStyle Width="130px" HorizontalAlign="center"/>
                            <HeaderStyle Width="130px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="CHECKDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����ԡ/�׹" DataField="CHECKDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>                   
                        <asp:BoundField SortExpression="CREATEBY" HeaderText = "�����ԡ/�׹" DataField="CREATEBY">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PDNAME" HeaderText = "���͡�����" DataField="PDNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QTY" HeaderText = "�ӹǹ" DataField="QTY">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="UNITNAME" HeaderText = "˹���" DataField="UNITNAME">
                            <ItemStyle Width="60px" HorizontalAlign="center"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STOCKINDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ�����Ҥ�ѧ" DataField="STOCKINDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TYPENAME" HeaderText = "������" DataField="TYPENAME">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "ʶҹ�" DataField="STATUSNAME">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>