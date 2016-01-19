<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ReturnRequest.aspx.cs" Inherits="Transaction_ReturnRequest" Title="ใบลดหนี้" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบลดหนี้</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"  NameBtnSubmit="ส่งคลังสำเร็จรูป"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr height="10px">
            <td style="width: 750px">
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top" style="width: 750px">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width: 515px;">
                            <table border="0" cellspacing="0" cellpadding="0" width="505" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px"></td>
                                    <td style="width: 180px"></td>
                                    <td style="width: 50px"></td>
                                    <td style="width: 170px"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        เลขที่ใบเสร็จ</td>
                                    <td colspan="3">
                                        <asp:TextBox id="txtInvoicecode" runat="server" Width="165px" AutoPostBack="True" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="zHidden" Width="23px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="20px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="20px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="20px">0</asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        รหัสลูกค้า</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="80px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="312px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbTitle" runat="server" Width="80px" CssClass="zComboBox"></asp:DropDownList> 
                                        <asp:TextBox ID="txtName" runat="server" Width="110px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="190px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="395px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px">
                                        โทรศัพท์</td>
                                    <td style="width: 180px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="165px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 50px">
                                        โทรสาร</td>
                                    <td style="width: 170px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="165px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 100px"></td>
                                    <td style="width: 180px"></td>
                                    <td style="width: 50px"></td>
                                    <td style="width: 170px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td style="width: 5px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width: 230px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="230">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 90px;"></td>
                                    <td style="width: 130px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 90px">
                                        เลขที่ใบลดหนี้</td>
                                    <td style="width: 130px">
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="120px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 90px">
                                        วันที่</td>
                                    <td style="width: 130px">
                                        <uc2:DatePickerControl ID="ctlReqDate" runat="server" Enabled="false" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr>
                              </table> 
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4" style="height: 2px">
                        </td>
                    </tr> 
                     <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" width="600">
                                <tr>
                                    <td valign="top" width="150px">
                                        &nbsp;เหตุผลในการลดหนี้</td>
                                    <td>
                                        <asp:TextBox id="txtReason" runat="server" Width="448px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="750px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating" DataSourceID="ItemDataSource">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                                ImageUrl="~/Images/icn_save.gif" />
                                            <asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoView" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblBarcode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="110px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductNameView" runat="server" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblProduct" CssClass="zHidden" runat="server" Text='<%# Bind("PRODUCT") %>'></asp:Label>
                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtyView" runat="server" Text='<%# Convert.ToDouble(Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="45px" Text='<%# Convert.ToDouble(Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>' CssClass="zTextboxR"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitNameView" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblUnit" CssClass="zHidden" runat="server" Text='<%# Bind("UNIT") %>'></asp:Label>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคา">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("PRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Convert.ToDouble(Eval("PRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="ส่วนลด">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOLDDISCOUNT" runat="server" Text='<%# Convert.ToDouble(Eval("OLDDISCOUNT")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblOLDDISCOUNT" runat="server" Text='<%# Convert.ToDouble(Eval("OLDDISCOUNT")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("NETPRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNetPrice" runat="server" Text='<%# Convert.ToDouble(Eval("NETPRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle Width="85px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="DISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscountView" runat="server" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("DISCOUNT")  %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="OLDQTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOldQtyView" runat="server" Text='<%# Bind("OLDQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblOldQty" runat="server" Text='<%# Bind("OLDQTY")  %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:BoundField> 
                                     
                                    <asp:TemplateField HeaderText="REFLOID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefLOIDView" runat="server" Text='<%# Bind("REFLOID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblRefLOID" runat="server" Text='<%# Bind("REFLOID")  %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteRequisitionItem" SelectMethod="GetRequisitionItem" 
                                TypeName="RequisitionRequestItem" UpdateMethod="UpdateRequisitionItem">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="requisition" PropertyName="Text" Type="Double" DefaultValue="0" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCTNAME" Type="String" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="UNITNAME" Type="String" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="DISCOUNT" Type="Decimal" />
                                    <asp:Parameter Name="OLDQTY" Type="Decimal" />
                                    <asp:Parameter Name="OLDDISCOUNT" Type="Decimal" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                </UpdateParameters>
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
                <table border="0" cellpadding="0" cellspacing="0" width="250">
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;มูลค่าตามใบกำกับภาษีเดิม</td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtOldTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;มูลค่าที่ถูกต้อง</td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;ผลต่าง</td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtDifference" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                   <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 89px">
                            &nbsp;VAT</td>
                        <td style="width: 40px">
                            <asp:TextBox ID="txtVat" runat="server" CssClass="zTextboxR-View" Width="25px" MaxLength="2" AutoPostBack="True" ReadOnly="True"></asp:TextBox>%</td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtTotalVat" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr>
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;รวม</td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25">
                        <td width="5">
                        </td>
                        <td colspan="3">
                            &nbsp;<asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>