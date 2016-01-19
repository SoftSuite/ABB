<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductReturn.aspx.cs" Inherits="Transaction_ProductReturn" Title="ใบส่งคืนสินค้า/วัตถุดิบ" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบส่งคืนสินค้า/วัตถุดิบ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="ยืนยัน"
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr style="height:10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr style="height:25px" width="800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="490px" class="zCombobox">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        เลขที่ใบแจ้งส่งคืน</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSTCode" runat="server" Width="134px" CssClass="zTextbox"></asp:TextBox><asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtSTLoid" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtSupplier" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        ผู้จำหน่าย</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSupplierName" runat="server" Width="314px" CssClass="zTextbox-View"></asp:TextBox>
                                        </td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtName" runat="server" Width="314px" CssClass="zTextbox"></asp:TextBox>
                                        </td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="315px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        โทรศัพท์</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 50px">
                                        โทรสาร</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        ลงวันที่</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlPDReturnDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:75px">
                                        วันที่แจ้งส่งคืน</td>
                                    <td style="width: 132px">
                                        <uc2:DatePickerControl ID="ctlSTDate" runat="server" Enabled="false" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                               
                            </table> 
                        </td>
                    </tr> 
                    <tr style="height:5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                             <asp:GridView ID="grvPDReturn" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        

                        <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  

                       <asp:BoundField SortExpression="BARCODE" HeaderText = "บาร์โค้ด" DataField="BARCODE">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "สินค้า" DataField="PRODUCTNAME">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="LOTNO" HeaderText = "Lot No" DataField="LOTNO">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="QTY" HeaderText = "จำนวน" DataField="QTY">
                            <ItemStyle Width="100px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="UNITNAME" HeaderText = "หน่วย" DataField="UNITNAME">
                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>      
                                       
                        <asp:BoundField SortExpression="PRICE" HeaderText = "ราคา" DataField="PRICE">
                            <ItemStyle Width="100px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        
                        <asp:BoundField SortExpression="NETPRICE" HeaderText = "รวมเงิน" DataField="NETPRICE">
                            <ItemStyle Width="100px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
                            &nbsp;
                            
                           
                        </td>
                    </tr>
                    <tr style="height:5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" width="620">
                                <tr>
                                    <td valign="top" width="310px">
				    สาเหตุ <br>
				    <asp:TextBox ID="txtReason" runat="server" Height="50px" TextMode="MultiLine" Width="300px" CssClass="zTextbox"></asp:TextBox>
                                      </td>
                                    <td>
				    หมายเหตุ <br>
				    <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="300px" CssClass="zTextbox"></asp:TextBox></td> 
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
                    <tr style="height:3px">
                        <td colspan="3"></td>
                    </tr> 
                    
                    <tr style="height:25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
              
            </td> 
        </tr>
    </table>
</asp:Content>


