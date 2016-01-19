<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinReturn.aspx.cs" Inherits="WH_Transaction_StockinReturn" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3" title="ใบรับคืนวัตถุดิบ">
                &nbsp;ใบรับคืนวัตถุดิบ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"  NameBtnSubmit="ยืนยัน"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" />
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
                                        ใบแจ้งคืนวัตถุดิบ</td>
                                    <td colspan="3">
                                        <asp:TextBox id="txtRequisitionCode" runat="server" Width="145px"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="12px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox>
                                        <asp:TextBox id="txtWareHouse" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox>
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="22px"></asp:TextBox>
                                        <asp:TextBox ID="txtPPLoid" runat="server" CssClass="zHidden" Width="22px"></asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        สินค้าที่ผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPDCode" runat="server" Width="104px" CssClass="zTextbox-View"></asp:TextBox>
                                        <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        จำนวน</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="zTextbox-View" Width="104px"></asp:TextBox>
                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextbox-View" Width="98px"></asp:TextBox></td>
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
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr height="5px">
                                    <td></td>
                                    <td style="width: 86px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 86px">
                                        เลขที่ใบรับคืน</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="125px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 86px">
                                        ลงวันที่</td>
                                    <td style="width: 132px">
                                        <uc2:DatePickerControl ID="ctlReceiveDate" runat="server" Enabled="false" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 86px">
                                        วันที่แจ้งคืน</td>
                                    <td style="width: 132px">
                                        <uc2:DatePickerControl ID="ctlReqdate" runat="server" Enabled="false" />
                                    </td>
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
                        <td colspan="4" style="height: 5px">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="height: 178px" valign="top">
                            <asp:GridView ID="grvItem" DataKeyNames="RANK" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="600px" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="บันทึก" CausesValidation="True"
                                                CommandName="Update" ImageUrl="~/Images/icn_save.gif" />
                                            <asp:ImageButton ID="imbCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                                                CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" AlternateText="แก้ไข" CausesValidation="False"
                                                CommandName="Edit" ImageUrl="~/Images/icn_edit.gif" />
                                            <asp:ImageButton ID="imbDelete" runat="server" AlternateText="ลบ" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true"
                                                MaxLength="20" OnTextChanged="txtBarcode_TextChanged" Text='<%# Bind("BARCODE") %>'
                                                Width="95px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged1" Width="95px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Enabled="false"
                                                OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" Width="145px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged1" Width="145px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false"
                                                Width="145px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot No">
										<EditItemTemplate>
                                            <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zCombobox"
                                                Width="80px" >
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" CssClass="zCombobox"
                                               Width="80px"  >
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="85px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotNoView" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Text='<%# Bind("QTY") %>'
                                                Width="45px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Text="0" Width="45px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOID" runat="server" CssClass="zHidden" Text='<%# Bind("REFLOID") %>'></asp:TextBox>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:Label ID="lblUnitNameNew" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitNameView" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="ราคา">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Text='<%# Bind("PRICE") %>'
                                                Width="45px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtPriceView" runat="server" Text='<%# Bind("PRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="รวมเงิน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Text='<%# Bind("NETPRICE") %>'
                                                Width="45px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNetPriceNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtNetPriceView" runat="server" Text='<%# Bind("NETPRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="UNIT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRICE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REFLOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                                <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteItem" SelectMethod="GetItem" 
                                TypeName="StockinReturnItemWH"  UpdateMethod="UpdateItem" OldValuesParameterFormatString="{0}">
                            <DeleteParameters>
                                    <asp:Parameter Type="Double" Name="RANK"></asp:Parameter>
                               </DeleteParameters>
                                <UpdateParameters>
                                 <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="UNITNAME" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLoid" Name="loid" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtRefLoid" Name="refloid" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtPPLoid" Name="pploid" PropertyName="Text" Type="Double" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                             <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                 EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                                OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound" Width="615px" DataSourceID="NewItemDataSource">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <FooterStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged" Width="145px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Lot No">
                             <ItemTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" AutoPostBack="True" CssClass="zCombobox" Width="80px" ></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="85px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitNameNew" runat="server"  Width="95px"></asp:Label>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden"></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ราคา">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                   <asp:TemplateField HeaderText="รวมเงิน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNetPriceNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px" />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetItemBlank" TypeName="StockinReturnItemWH" DeleteMethod="DeleteItem"></asp:ObjectDataSource>
                           
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
                            พนักงาน</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                 <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="3px">
                        <td colspan="3"></td>
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
               </td> 
        </tr>
    </table>
</asp:Content>
