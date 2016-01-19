<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutOther.aspx.cs" Inherits="WH_Transaction_Stockout" Title="ใบเบิกวัสดุอื่นๆ" %>

<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบเบิกวัสดุอื่นๆ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="อนุมัติ"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnPrintClick="PrintClick"   OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td style="width: 184px">
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; height: 277px;">
                            <table border="0" cellspacing="0" cellpadding="0" width="390px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px"></td>
                                    <td style="width: 190px"></td>
                                    <td></td>
                                    <td style="width: 116px"></td>
                                </tr> 
                                <tr height="25px" id="trType" runat=server>
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px">
                                        ประเภท</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbDocType" runat="server" Width="143px" AutoPostBack="True" OnSelectedIndexChanged="cmbDocType_SelectedIndexChanged1"></asp:DropDownList>
                                        <asp:TextBox ID="txtRefNo" runat="server" Width="94px" OnTextChanged="txtRefNo_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                        <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtPD" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtInvCode" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox><asp:TextBox
                                            ID="txtRef1" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox><asp:TextBox
                                                ID="txtRef2" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox></td>
                                </tr>
                                  <tr height="25px" id = "trDivision" runat="server">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px">
                                        หน่วยงาน</td>
                                    <td colspan="3">
                                        <asp:DropDownList id="cmbDivision" runat="server" CssClass="zCombobox" Width="282px">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                                </tr> 
                                 <tr height="25px" id = "trSupportRefCode" runat="server">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px">
                                        อ้างอิง</td>
                                    <td colspan="3"><asp:TextBox ID="txtSupportRefCode" runat="server" Width="273px" OnTextChanged="txtRefNo_TextChanged" AutoPostBack="True">
                                    </asp:TextBox></td>
                                </tr> 
                                <tr height="25px" id= "trSupportCause" runat="server">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px">
                                        สาเหตุการเบิก</td>
                                    <td colspan="3"><asp:TextBox ID="txtSupportCause" runat="server" Height="58px" TextMode="MultiLine" Width="273px" CssClass="zTextbox">
                                    </asp:TextBox>
                                        <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 136px"></td>
                                    <td style="width: 190px">
                                        &nbsp;</td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px" style="height: 277px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; height: 277px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่ใบเบิก</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtStockCode" runat="server" CssClass="zTextbox-View"
                                            Width="110px" ReadOnly="True"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        ลงวันที่</td>
                                    <td style="width: 132px"><asp:TextBox ID="txtDate" runat="server" CssClass="zTextbox-View"
                                            Width="110px" ReadOnly="True"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        </td>
                                    <td style="width: 132px">
                                        &nbsp;<uc2:DatePickerControl ID="ctlCreateDate" runat="server" Enabled="false" Visible="false" />
                                        </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                 <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        </td>
                                    <td style="width: 132px">
                                        </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        </td>
                                    <td style="width: 132px">
                                </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                 <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        </td>
                                    <td style="width: 132px">
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
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataKeyNames="NO" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating" ShowFooter="True" Width="615px" DataSourceID="ItemDataSource" >
                                <PagerSettings Visible="False" />
                                <Columns>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="บันทึก" CausesValidation="True"
                                                CommandName="Update" ImageUrl="~/Images/icn_save.gif" />
                                            <asp:ImageButton ID="imbCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                                                CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        
</FooterTemplate>

<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" AlternateText="แก้ไข" CausesValidation="False"
                                                CommandName="Edit" ImageUrl="~/Images/icn_edit.gif" />
                                            <asp:ImageButton ID="imbDelete" runat="server" AlternateText="ลบ" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                                        
</ItemTemplate>

<FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="บาร์โค้ด"><EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true"
                                                MaxLength="20" OnTextChanged="txtBarcode_TextChanged" Text='<%# Bind("BARCODE") %>'
                                                Width="95px"></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged1" Width="95px"></asp:TextBox>
                                        
</FooterTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>' ></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า"><EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Enabled="false"
                                                OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" Width="145px">
                                            </asp:DropDownList>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged1" Width="145px">
                                            </asp:DropDownList>
                                        
</FooterTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                <asp:Label ID="lblProductView" runat="server" Width="145px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField Visible="False" HeaderText="Lot No"><EditItemTemplate>
                                            <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zCombobox"
                                                Width="80px"  AutoPostBack="True" OnSelectedIndexChanged="cmbLotNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" CssClass="zCombobox"
                                               Width="80px"   AutoPostBack="True" OnSelectedIndexChanged="cmbLotNoNew_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        
</FooterTemplate>

<HeaderStyle Width="85px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="lblLotNoView" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField Visible="False" HeaderText="คงเหลือ"><EditItemTemplate>
                                            <asp:TextBox ID="txtRemainQty" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Text='<%# Bind("REMAINQTY") %>'
                                                Width="45px"></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtRemainQtyNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtRemainQtyView" runat="server" Text='<%# Bind("REMAINQTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน"><EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Text='<%# Bind("QTY") %>'
                                                Width="45px"></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Text="0" Width="45px"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย"><EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOID" runat="server" CssClass="zHidden" Text='<%# Bind("REFLOID") %>'></asp:TextBox>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:Label ID="lblUnitNameNew" runat="server" Text=""></asp:Label>
                                        
</FooterTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtUnitNameView" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="UNIT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRICE">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="REFLOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteStockOutItem"
                                OldValuesParameterFormatString="{0}" SelectMethod="GetStockOutItem"
                                TypeName="StockoutWHOtherItem" UpdateMethod="UpdateStockOutItem">
                                <DeleteParameters>
                                    <asp:Parameter Name="No" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="NO" Type="Decimal" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="REMAINQTY" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="UNITNAME" Type="String" />
                                    <asp:Parameter Name="PRODUCTNAME" Type="String" />
                                    <asp:Parameter Name="REQUISITION" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="stockOut" PropertyName="Text" Type="string" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataKeyNames="NO" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                                OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound" Width="615px" DataSourceID="NewItemDataSource">
                                <PagerSettings Visible="False" />
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px" />
                                <Columns>
<asp:TemplateField ShowHeader="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        
</ItemTemplate>

<FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="บาร์โค้ด">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged" Width="95px"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า">
<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged" Width="145px">
                                            </asp:DropDownList>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField Visible="False" HeaderText="Lot No">
<HeaderStyle Width="85px"></HeaderStyle>
<ItemTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" AutoPostBack="True" CssClass="zCombobox" Width="80px" OnSelectedIndexChanged="cmbLotNoNew_SelectedIndexChanged"></asp:DropDownList>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField Visible="False" HeaderText="คงเหลือ">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtRemainQtyNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="lblUnitNameNew" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden"></asp:TextBox>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                                SelectMethod="GetStockOutItemBlank" TypeName="StockOutWHItem"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing="0" cellpadding="0" width="600">
                                <tr>
  
                                    <td>
                                    <table border="0" cellspacing="0" cellpadding="0" width="600">
                                    <tr>
                                    <td valign="top" style="width: 105px">
                                        &nbsp;หมายเหตุ </td>
										<td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="385px" CssClass="zTextbox"></asp:TextBox></td> 
                                    </tr>
                                    </table>
                                </td>
                                </tr>
                                 </table> 
                        </td>
                    </tr> 
                </table>
            </td> 
            <td width="2px">
                &nbsp;
            </td> 
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted; width: 184px;">
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
                            </td>
                        <td>
                            </td> 
                    </tr>
                    <tr height="25px">
                        <td width="5px" style="height: 21px"></td>
                        <td style="width: 50px; height: 21px;">
                            &nbsp;</td>
                        <td style="width: 50px; height: 21px;">
                            </td>
                        <td style="height: 21px">
                            </td> 
                    </tr>
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                            </td> 
                    </tr> 
                    <tr height="25">
                        <td width="5">
                        </td>
                        <td colspan="2">
                        </td>
                        <td>
                            </td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>
