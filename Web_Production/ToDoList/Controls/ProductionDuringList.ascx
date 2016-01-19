<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionDuringList.ascx.cs" Inherits="ToDoList_Controls_ProductionDuringList" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>

<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
           <table border="0" cellpadding="0" cellspacing="0" style="border-right: #cbdaa9 1px solid;
                border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid">
                <tr>
                    <td class="subheadertext" colspan="6">
                        &nbsp;����</td>
                </tr>
                <tr style="height:10px">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td style="width:20px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td style="width:370px">
                    </td>
                </tr>
                <tr style="height:25px">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                        �Ţ����ü�Ե</td>
                    <td style="width:150px">
                        <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox" Width="145px"></asp:TextBox></td>
                    <td align="center" style="width:20px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height:25">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                        �ѹ����Ե</td>
                    <td style="width:150px">
                        <uc1:DatePickerControl ID="dtpDateFrom" runat="server" />
                    </td>
                    <td align="center" style="width:20px">
                        �֧
                    </td>
                    <td style="width:150px">
                        <uc1:DatePickerControl ID="dtpDateTo" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>                

                <tr style="height:25">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                        ���ͼ�Ե�ѳ��</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr style="height:10px">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td style="width:20px">
                    </td>
                    <td style="width:150px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td> 
    </tr>
    <tr>
        <td>
           <asp:GridView ID="grvProductionDuring" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***��辺������***</center>" OnRowDataBound="grvProductionDuring_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="PDPLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="POLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOTNO">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�Ţ����ü�Ե" SortExpression="LOTNO">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkLotNo" Text="" runat="server"></asp:HyperLink> 
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="center"/>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDNAME" HeaderText="�����Թ���" SortExpression="PDNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MFGDATE" HeaderText="�ѹ����Ե" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="MFGDATE">
                        <ItemStyle Width="75px" HorizontalAlign="center"/>
                        <HeaderStyle Width="75px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STDQTY" HeaderText="�ӹǹ��觼�Ե" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="STDQTY">
                        <ItemStyle Width="85px" HorizontalAlign="right"/>
                        <HeaderStyle Width="85px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDQTY" HeaderText="�ӹǹ��Ե��" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="PDQTY">
                        <ItemStyle Width="85px" HorizontalAlign="right"/>
                        <HeaderStyle Width="85px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNAME" HeaderText="˹���" SortExpression="UNAME">
                        <ItemStyle Width="55px" HorizontalAlign="center"/>
                        <HeaderStyle Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DUEDATE" HeaderText="�ѹ����Ե����" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="DUEDATE">
                        <ItemStyle Width="80px" HorizontalAlign="center"/>
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUPPLIERNAME" HeaderText="�ѹ�֡��觼�Ե" SortExpression="SUPPLIERNAME">
                        <ItemStyle Width="100px" HorizontalAlign="center"/>
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QUARANTINEDATE" HeaderText="�ѹ�����Ҥ�ѧ�ѡ�ѹ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="QUARANTINEDATE">
                        <ItemStyle Width="110px" HorizontalAlign="center"/>
                        <HeaderStyle Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SENDQCDATE" HeaderText="�ѹ����觵�Ǩ QC" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="SENDQCDATE">
                        <ItemStyle Width="100px" HorizontalAlign="center"/>
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QCDUEDATE" HeaderText="�ѹ��� QC ��Ǩ�ͺ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="QCDUEDATE">
                        <ItemStyle Width="100px" HorizontalAlign="center"/>
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QCRESULT" HeaderText="�š�õ�Ǩ�ͺ" SortExpression="QCRESULT">
                        <ItemStyle Width="55px" HorizontalAlign="center"/>
                        <HeaderStyle Width="55px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SENDFGDATE" HeaderText="�ѹ����͡�ҡ��ѧ�ѡ�ѹ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="SENDFGDATE">
                        <ItemStyle Width="140px" HorizontalAlign="center"/>
                        <HeaderStyle Width="140px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>