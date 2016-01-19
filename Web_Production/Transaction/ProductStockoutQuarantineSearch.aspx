<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductStockoutQuarantineSearch.aspx.cs" Inherits="Transaction_ProductStockoutQuarantineSearch" Title="�ѹ�֡��è��¼�Ե�ѳ���͡�ҡ��ѧ�ѡ�ѹ" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;�ѹ�֡��è��¼�Ե�ѳ���͡�ҡ��ѧ�ѡ�ѹ</td> 
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
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 155px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
   
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ�����Ҥ�ѧ������ٻ</td>
                        <td style="width: 155px">
                            <uc2:DatePickerControl ID="ctlSendFGDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlSendFGDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                                       <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����Ե</td>
                        <td style="width: 155px">
                            <uc2:DatePickerControl ID="ctlMFGDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlMFGDateTo" runat="server" /></td>
                        <td></td>
                    </tr>                                                 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ���ͼ�Ե�ѳ��</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProduct" runat="server" Width="326px" >
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ����ü�Ե</td>
                        <td style="width: 155px">
                            <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>                     

                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 155px"></td>
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
                <asp:GridView ID="grvPDOrder" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="900px" OnRowCommand="grvPDOrder_RowCommand" OnRowDataBound="grvPDOrder_RowDataBound" OnRowCreated="grvPDOrder_RowCreated">
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

                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "���ͼ�Ե�ѳ��" DataField="PRODUCTNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>  
                        <asp:TemplateField HeaderText="�Ţ����ü�Ե" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <ItemTemplate>
                                 <asp:HyperLink ID="hplLotNo" runat="server" Text='<%# Bind("LOTNO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="MFGDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����Ե" DataField="MFGDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>   
                        <asp:BoundField SortExpression="PDQTY" HeaderText = "�ʹ��ԧ" DataField="PDQTY">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QCQTY1" HeaderText = "1st" DataField="QCQTY1">
                            <ItemStyle Width="50px" />
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QCQTY2" HeaderText = "2nd" DataField="QCQTY2">
                            <ItemStyle Width="50px" />
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QCQTY3" HeaderText = "3rd" DataField="QCQTY3">
                            <ItemStyle Width="50px" />
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="BADQTY" HeaderText = "�ʹ����" DataField="BADQTY">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="SENDFGQTY" HeaderText = "�ʹ����" DataField="SENDFGQTY">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="SENDFGDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ�����Ҥ�ѧ" DataField="SENDFGDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>   
                        <asp:BoundField SortExpression="QUARANTINEREMARK" HeaderText = "�����˵�" DataField="QUARANTINEREMARK">
                            <ItemStyle Width="120px" />
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PRODUCT">
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
</asp:Content>
