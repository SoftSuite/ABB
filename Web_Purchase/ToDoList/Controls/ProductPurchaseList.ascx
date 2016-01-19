<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductPurchaseList.ascx.cs" Inherits="ToDoList_Controls_ProductPurchaseList" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>

<table border="0" width="800px" cellpadding="0" cellspacing="0">
    <tr>
        <td>
           <table border="0" cellpadding="0" cellspacing="0" style="border-right: #cbdaa9 1px solid;
                border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid"
                width="900">
                <tr>
                    <td class="subheadertext" colspan="6">
                        &nbsp;����</td>
                </tr>
                <tr style="height:10">
                    <td width="50">
                    </td>
                    <td width="150">
                    </td>
                    <td width="150">
                    </td>
                    <td width="20">
                    </td>
                    <td style="width: 170px">
                    </td>
                    <td width="360">
                    </td>
                </tr>
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        �Ţ���㺢ͫ���</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        �ѹ���</td>
                    <td width="150">
                        <uc1:DatePickerControl ID="dtpDateFrom" runat="server" />
                    </td>
                    <td align="center" width="20">
                        �֧
                    </td>
                    <td style="width: 170px">
                        <uc1:DatePickerControl ID="dtpDateTo" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>                
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        ������</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbPurchaseType" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>

                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        �Թ���</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                    </td>
                </tr>
               <tr style="height: 25px">
                   <td width="50">
                   </td>
                   <td colspan="4" style="color: red">
                       �����˵�: ! ���¶֧ ��¡�÷���ͧ��ë�����觴�ǹ</td>
                   <td>
                   </td>
               </tr>
                <tr style="height:10">
                    <td width="50">
                    </td>
                    <td width="150">
                    </td>
                    <td width="150">
                    </td>
                    <td width="20">
                    </td>
                    <td style="width: 170px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td> 
    </tr>
    <tr>
        <td class="toolbarplace">
            <uc2:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="���ҧ���觫���" OnNewClick="NewClick"/>
        </td> 
    </tr>
    <tr>
        <td>
           <asp:GridView ID="grvProductPurchase" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***��辺������***</center>" Width="900px" OnRowDataBound="grvProductPurchase_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="LOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>                    
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItem" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                        <HeaderStyle Width="30px" /> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="!">
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="PRLOID" DataNavigateUrlFormatString="../../Transaction/PurchaseRequest.aspx?loid={0}"
                        DataTextField="PRCODE" HeaderText="�Ţ���㺢ͫ���" DataTextFormatString="{0}" >
                        <ItemStyle Width="110px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="110px" /> 
                    </asp:HyperLinkField>                    
                    <asp:BoundField DataField="REQUESTDATE" HeaderText="�ѹ�����觫���" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" SortExpression="REQUESTDATE">
                        <ItemStyle Width="70px" HorizontalAlign="Right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRODUCTNAME" HeaderText="�����Թ���" SortExpression="PRODUCTNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="�ӹǹ" DataFormatString="{0:#,##0.00}" HtmlEncode="False" SortExpression="QTY">
                        <ItemStyle Width="70px" HorizontalAlign="Right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNITNAME" HeaderText="˹���" SortExpression="UNITNAME">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OLDPRICE" HeaderText="�Ҥ����" DataFormatString="{0:#,##0.00}" HtmlEncode="False" SortExpression="OLDPRICE">
                        <ItemStyle Width="80px" Wrap="True" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CURPRICE" HeaderText="�ҤһѨ�غѹ" DataFormatString="{0:#,##0.00}" HtmlEncode="False" SortExpression="CURPRICE">
                        <ItemStyle Width="50px" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MINPRICE" HeaderText="�Ҥҵ���ش" DataFormatString="{0:#,##0.00}" HtmlEncode="False" SortExpression="MINPRICE">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PURCHASETYPENAME" HeaderText="������" SortExpression="PURCHASETYPENAME">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="STATUSNAME" HeaderText="ʶҹ�" SortExpression="STATUSNAME">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRODUCT">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="UNIT">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="STATUS">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="DUEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="URGENT">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>  
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>