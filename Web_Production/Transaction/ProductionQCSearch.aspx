<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductionQCSearch.aspx.cs" Inherits="Transaction_ProductionQCSearch" Title="�ѹ�֡����觼�Ե�ѳ��������ٻ��Ǩ��������س�Ҿ" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;�ѹ�֡����觼�Ե�ѳ��������ٻ��Ǩ��������س�Ҿ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"  />
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
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px"></td>
                        <td style="width: 152px"></td> 
                        <td width="20px"></td>
                        <td width="155px"></td>
                    </tr>
                   
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            �ѹ�������������</td>
                        <td style="width: 152px">
                            <uc2:DatePickerControl ID="ctlSendQCDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlSendQCDateTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            �ѹ����Ե</td>
                        <td style="width: 152px">
                            <uc2:DatePickerControl ID="ctlMfgDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlMfgDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            �ѹ����������</td>
                        <td style="width: 152px">
                            <uc2:DatePickerControl ID="ctlEXPDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlEXPDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            �Ţ����ü�Ե</td>
                         <td colspan = 3>
                             <asp:TextBox ID="txtLotNo" runat="server" Width="294px"></asp:TextBox></td>
                            <td width="50px">
                                &nbsp;</td>

                    </tr>  
                    <tr height="25px">
                        <td width="50px" style="height: 25px"></td>
                        <td style="width: 80px; height: 25px">
                            ���ͼ�Ե�ѳ��</td>
                        <td colspan = 3 style="height: 25px">
                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="300px">
                            </asp:DropDownList></td>
                        <td width="50px" style="height: 25px"><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                           
                    </tr>                      
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px"></td>
                        <td style="width: 152px"></td> 
                        <td width="20px"></td>
                        <td width="155px"></td>
                    </tr>
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPDOrder" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowCommand="grvPDOrder_RowCommand" OnRowDataBound="grvPDOrder_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="�����"
                                    ImageUrl="~/Images/icn_print.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="30px"/>
                            <HeaderStyle Width="30px"/>
                            <FooterStyle Width="30px"/>
                        </asp:TemplateField>

                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                        
                        <asp:BoundField DataField="PRODUCT">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                         
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "���ͼ�Ե�ѳ��" DataField="PRODUCTNAME">
                            <ItemStyle Width="200px"/>
                            <HeaderStyle Width="200px" />  
                        </asp:BoundField> 
                        <asp:TemplateField HeaderText="�Ţ����ü�Ե" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <ItemTemplate>
                                 <asp:HyperLink ID="hplLotNo" runat="server" Text='<%# Bind("LOTNO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="MFGDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����������Ե" DataField="MFGDATE" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>                       

                        <asp:BoundField SortExpression="QCQTY1" HeaderText = "�ӹǹ�������" DataField="QCQTY1">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="SENDQCDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ�������������" DataField="SENDQCDATE" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>   
                        <asp:BoundField SortExpression="QCREMARK" HeaderText = "�����˵�" DataField="QCREMARK">
                            <ItemStyle Width="180px" />
                            <HeaderStyle Width="180px" />  
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>


