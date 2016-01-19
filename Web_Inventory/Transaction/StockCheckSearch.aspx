<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockCheckSearch.aspx.cs" Inherits="Transaction_StockCheckSearch" Title="��Ǩ�Ѻ�Թ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;��Ǩ�Ѻ�Թ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnNew="���ҧ����" NameBtnPrint="�������§ҹ"
                    OnNewClick="NewClick" OnSubmitClick="SubmitClick" OnPrintClick="PrintClick" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;����
                        </td>
                    </tr>
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            Batch No</td>
                        <td style="width:340px" colspan="3">
                            <asp:DropDownList ID="cmbBatchNo" runat="server" CssClass="zComboBox" Width="340px" AutoPostBack="True" OnSelectedIndexChanged="cmbBatchNo_SelectedIndexChanged" />
                            <asp:TextBox ID="txtBatchNoStatus" runat="server" CssClass="zHidden" Width="340px" /></td>                          
                        <td></td>
                    </tr>
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr>                    
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            Warehouse</td>
                        <td style="width:340px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="340px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            Location</td>
                        <td style="width:340px" colspan="3">
                            <asp:DropDownList ID="cmbLocation" runat="server" CssClass="zComboBox" Width="340px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �ѹ����Ǩ�Ѻ</td>
                        <td style="width:150px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" /></td>
                        <td style="width:20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �����Թ���</td>
                        <td style="width:340px" colspan="3">
                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Width="340px" /></td>
                        <td></td>                            
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �����Թ���</td>
                        <td style="width:340px" colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="340px" /></td>
                        <td></td>                            
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            Lot No</td>
                        <td style="width:340px" colspan="3">
                            <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zComboBox" Width="340px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px"></td>
                        <td style="width:340px" colspan="3">
                            <asp:CheckBox ID="chkDiff" Text="੾����¡�÷���ռŵ�ҧ" runat="server" Width="340px" Checked="true" /></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr style="height:10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvStockCheckItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowDataBound="grvStockCheckItem_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:BoundField SortExpression="NO" HeaderText = "�ӴѺ" DataField="NO" >
                            <ItemStyle Width="50px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="BARCODE" HeaderText = "������" DataField="BARCODE" >
                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PD_NAME" HeaderText = "�����Թ���" DataField="PD_NAME">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="LOTNO" HeaderText = "Lot No" DataField="LOTNO" >
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="L_NAME" HeaderText = "Location" DataField="L_NAME" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="SYSQTY" HeaderText = "�ӹǹ��к�" DataField="SYSQTY" HtmlEncode="False" DataFormatString="{0:#,##0}">
                            <ItemStyle Width="80px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="COUNTQTY" HeaderText = "�ӹǹ�Ѻ��ԧ" DataField="COUNTQTY" HtmlEncode="False" DataFormatString="{0:#,##0}">
                            <ItemStyle Width="80px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="DIFFQTY" HeaderText = "�ŵ�ҧ" DataField="DIFFQTY" HtmlEncode="False" DataFormatString="{0:#,##0}">
                            <ItemStyle Width="50px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="��Ѻ��ا�ʹ">
                            <ItemTemplate>
                                <asp:TextBox ID="txtImproveQty" runat="server" Text='0' CssClass="zTextboxR" Width="85"/>
                            </ItemTemplate>
                            <HeaderStyle Width="90px" /> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�˵ؼ�">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReason" runat="server" CssClass="zTextbox" Width="115"/>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SH_LOID" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PD_LOID">
                            <ControlStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PS_LOID">
                            <ControlStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>    
</asp:Content>