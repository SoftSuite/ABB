<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master"  AutoEventWireup="true" CodeFile="ProductRequestInShop.aspx.cs" Inherits="Web_POS_ProductRequestInShop"   Title="ใบขอเบิกภายใน" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบขอเบิกภายใน</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="ส่งคลังสำเร็จรูป"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="390px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ถึง</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbRefWarehouse" runat="server" Enabled="False" Width="220px">
                                            <asp:ListItem Value="1">คลังผลิตภัณฑ์สำเร็จรูป</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ผู้ขอเบิก</td>
                                    <td colspan="3"><asp:TextBox ID="txtWarehouseName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="217px">
                                    </asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>

                                    <td colspan="3">
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWarehouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox id="txtCustomer" runat="server" CssClass="zHidden" Width="30px">
                                        </asp:TextBox></td>
                                </tr>
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" width="250px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 108px">
                                        เลขที่ขอเบิก</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 108px">
                                        วันที่ขอเบิก</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlReqDate" runat="server" Enabled="False" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                
                            </table> 
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                   <tr height="5px">
                        <td colspan="4">
                            <uc1:ToolbarControl ID="ctlToolbarItem" runat="server" BtnBackShow="false" BtnCancelShow="false"
                                BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                                BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="เพิ่มสินค้า" NameBtnDelete="ลบสินค้า"
                                OnNewClick="ItemNewClick" OnDeleteClick="ItemDeleteClick" />
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="170px"></asp:TextBox>
                            <asp:TextBox ID="txtSelectProduct" runat="server" CssClass="zHidden" Width="170px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="715px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
                                OnRowDataBound="grvItem_RowDataBound">
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
                                    
                                    <asp:BoundField DataField="RANK" SortExpression="RANK" HeaderText="ลำดับที่" InsertVisible="False" ReadOnly="True">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="BARCODE" SortExpression="BARCODE" HeaderText="บาร์โค้ด">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="PRODUCTNAME" SortExpression="PRODUCTNAME" HeaderText="สินค้า">
                                    </asp:BoundField> 
                                     
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Convert.ToDouble(Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>' OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="UNITNAME" SortExpression="UNITNAME" HeaderText="หน่วย">
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="PRICE" SortExpression="PRICE" HeaderText="ราคาต่อหน่วย" HtmlEncode="False" DataFormatString="{0:#,##0.00}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="NETPRICE" SortExpression="NETPRICE" HeaderText="รวมเงิน" HtmlEncode="False" DataFormatString="{0:#,##0.00}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundField> 

                                    <asp:BoundField DataField="STOCKQTY" SortExpression="STOCKQTY" HeaderText="คงคลัง" HtmlEncode="False" DataFormatString="{0:#,##0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:BoundField> 

                                    <asp:BoundField DataField="NORMALDISCOUNT">
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="ISVAT">
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:BoundField> 
                                     
                                    <asp:BoundField DataField="PRODUCT">
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:BoundField> 
                                     
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" SelectMethod="GetRequisitionItem" TypeName="RequisitionItemReserve" 
                                OldValuesParameterFormatString="{0}">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="requisition" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtWareHouse" Name="warehouse" PropertyName="Text" Type="Double" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" width="600">
                                <tr>
                                    <td valign="top" width="70px">
                                        &nbsp;หมายเหตุ</td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="530px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td> 
            <td width="2px">
                &nbsp;
            </td> 
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted;">
                <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            พนักงานขาย</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                                <table border="0" cellpadding="0" cellspacing="0" width="190px">

                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;ยอดสุทธิ</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 50px">
                            &nbsp;</td>
                        <td style="width: 50px">
                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="zHidden" Width="25px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtDiscount_TextChanged" ReadOnly="True">0</asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr>
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 50px">
                            &nbsp;</td>
                        <td style="width: 50px">
                            <asp:TextBox ID="txtVat" runat="server" CssClass="zHidden" Width="25px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtVat_TextChanged" ReadOnly="True"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtTotalVat" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr>
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtNet" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25">
                        <td width="5">
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLowerPrice" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px">0</asp:TextBox>
                            <asp:TextBox ID="txtMemberDiscount" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px">0</asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td>
                    </tr>
                </table>
                 </td> 
        </tr>
    </table>
               
</asp:Content>
