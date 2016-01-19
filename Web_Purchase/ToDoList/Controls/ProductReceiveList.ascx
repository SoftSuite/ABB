<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReceiveList.ascx.cs" Inherits="ToDoList_Controls_ProductReceiveList" %>
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
                        �Ţ������觫���</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        �ѹ�����觫���</td>
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
                        �Թ��ҷ����觫���</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>

                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        ����˹���</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
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
        <td>
            <asp:GridView ID="grvProductReceive" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***��辺������***</center>" Width="900px" OnRowDataBound="grvProductReceive_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="POLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DUEDATE" HeaderText="��˹���" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="DUEDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="POLOID" DataNavigateUrlFormatString="../../Transaction/PurchaseOrder.aspx?loid={0}"
                        DataTextField="POCODE" HeaderText="�Ţ������觫���" DataTextFormatString="{0}" >
                        <ItemStyle Width="110px" HorizontalAlign="center"/>
                        <HeaderStyle Width="110px" /> 
                    </asp:HyperLinkField>                    
                    <asp:BoundField DataField="ORDERDATE" HeaderText="�ѹ���ͫ���" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="ORDERDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUPPLIERNAME" HeaderText="����˹���" SortExpression="SUPPLIERNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>                        
                    <asp:BoundField DataField="PRODUCTNAME" HeaderText="�����Թ���" SortExpression="PRODUCTNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="�ӹǹ��觫���" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="QTY">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RECEIVEQTY" HeaderText="�ӹǹ�Ѻ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="RECEIVEQTY">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="UNITNAME" HeaderText="˹���" SortExpression="UNITNAME">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUSPRODUCT" HeaderText="ʶҹШѴ����" SortExpression="STATUSPRODUCT">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>