<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionWaitList.ascx.cs" Inherits="ToDoList_Controls_ProductionWaitList" %>
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
                        �Ţ���㺺ѹ�֡��觼�Ե</td>
                    <td style="width:150px">
                        <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="145px"></asp:TextBox></td>
                    <td align="center" style="width:20px">
                        �֧
                    </td>
                    <td style="width:150px">
                        <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="145px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr style="height:25">
                    <td style="width:50px">
                    </td>
                    <td style="width:150px">
                        �ѹ�����觼�Ե</td>
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
           <asp:GridView ID="grvProductionWait" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***��辺������***</center>" Width="900px" OnRowCommand="grvProductionWait_RowCommand" 
                OnRowDataBound="grvProductionWait_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="RQILOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RQLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>                    
                    <asp:TemplateField ShowHeader="False">
                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imbNew" runat="server" CausesValidation="False" CommandName="NewProduction" AlternateText="���ҧ��¡�úѹ�֡��ü�Ե����"
                                ImageUrl="~/Images/icn_new.gif" />
                        </ItemTemplate>
                        <FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CODE" HeaderText="�Ţ���ѹ�֡��觼�Ե" SortExpression="CODE">
                        <ItemStyle Width="170px" HorizontalAlign="center"/>
                        <HeaderStyle Width="170px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PDNAME" HeaderText="�����Թ���" SortExpression="PDNAME">
                        <ItemStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REQDATE" HeaderText="�ѹ�����觼�Ե" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="REQDATE">
                        <ItemStyle Width="100px" HorizontalAlign="center"/>
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="QTY" HeaderText="�ӹǹ��觼�Ե" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="QTY">
                        <ItemStyle Width="120px" HorizontalAlign="right"/>
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNAME" HeaderText="˹���" SortExpression="UNAME">
                        <ItemStyle Width="70px" HorizontalAlign="center"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OFFICER" HeaderText="�����觼�Ե" SortExpression="OFFICER">
                        <ItemStyle Width="120px" HorizontalAlign="center"/>
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>