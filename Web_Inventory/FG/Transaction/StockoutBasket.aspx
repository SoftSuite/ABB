<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutBasket.aspx.cs" Inherits="FG_Transaction_StockoutBasket" Title="รายละเอียดเบิก/คืนกระเช้า" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;รายละเอียดเบิก/คืนกระเช้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnPrintClick="PrintClick" OnSubmitClick="SubmitClick" NameBtnSubmit="ยืนยัน"/>
            </td> 
        </tr>
        <tr style="height:10px">
            <td colspan="3">
            </td>
        </tr> 
        <tr style="height:25px; width:800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width:597px">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:590px" class="zCombobox">
                                <tr style="height:5px">
                                    <td colspan="7"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ประเภท</td>
                                    <td colspan="4" style="width:515">
                                        <asp:DropDownList ID="cmbBasketType" runat="server" Width="175px" Enabled="true">
                                            <asp:ListItem Value="NEW">จัดกระเช้าใหม่</asp:ListItem>
                                            <asp:ListItem Value="RET">คืนกระเช้าเดิม</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtPDLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="0px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px" valign="top">
                                        ผู้ผลิต</td>
                                    <td colspan="4" style="width:515">
                                        <asp:TextBox ID="txtSupplier" runat="server" Width="510px" ReadOnly="true" CssClass="zTextbox-View" Text="คลังผลิตภัณฑ์สำเร็จรูป" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px" valign="top">
                                        รหัสกระเช้า</td>
                                    <td colspan="2" style="width:180">
                                        <asp:TextBox ID="txtBarCode" runat="server" Width="140px" ReadOnly="true" CssClass="zTextbox-View" />
                                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                    </td>
                                    <td colspan="2" style="width:335">
                                        <asp:TextBox ID="txtBasketName" runat="server" Width="330px" ReadOnly="true" CssClass="zTextbox-View" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px" valign="top">
                                        จำนวน</td>
                                    <td style="width: 90">
                                        <asp:TextBox ID="txtBasketQty" runat="server" Width="80px" CssClass="zTextboxR" Text="1" AutoPostBack="true" OnTextChanged="txtBasketQty_TextChanged" />
                                    </td>
                                    <td style="width: 90">
                                        <asp:TextBox ID="txtBasketunit" runat="server" Width="80px" ReadOnly="true" CssClass="zTextbox-View"/>
                                    </td>
                                    <td style="width: 65px">
                                        ยอดสุทธิ
                                    </td>
                                    <td style="width: 270px">
                                        <asp:TextBox ID="txtShowPrice" runat="server" Width="265px" ReadOnly="true" CssClass="zTextbox-View" />
                                        <asp:TextBox ID="txtPrice" runat="server" Width="0px" ReadOnly="true" CssClass="zHidden" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr style="height:5px">
                                    <td colspan="7"></td>
                                </tr> 
                            </table>
                        </td>
                        <td style="width: 3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width:200px">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:195px">
                                <tr style="height:5px">
                                    <td colspan="4"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 85px">
                                        เลขที่ใบจัดกระเช้า</td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtBasketCode" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="100px" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 85px">
                                        ลงวันที่</td>
                                    <td style="width: 100px"><uc2:DatePickerControl ID="ctlCheckDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 85px">
                                        สถานะ</td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="100px" /></td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 85px">
                                        Lot No</td>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="100px" /></td>
                                    <td style="width: 5px;"></td>
                                </tr>                                 
                            </table> 
                        </td>
                    </tr> 
                    <tr style="height:5px">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView id="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
                                OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                                runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับ" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="BARCODE">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false" Width="145px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zCombobox" Width="115px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewLotNo" runat="server" CssClass="zCombobox" Width="115px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbLotNoView" runat="server" CssClass="zCombobox" Enabled="false" Width="115px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px"></HeaderStyle>
                                    </asp:TemplateField>                                    
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("UNITNAME") %>' ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Width="45px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="LOID" >
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
                                    
                                    <asp:BoundField DataField="UNIT">
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
                                    
                                    <asp:BoundField DataField="PACKAGE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRODUCTSTOCK">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                </Columns>
                                
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            
                            <asp:ObjectDataSource id="ItemDataSource" runat="server" DeleteMethod="DeleteBasketItem"
                                OldValuesParameterFormatString="{0}" SelectMethod="GetBasketItem" TypeName="BasketItem"
                                UpdateMethod="UpdateBasketItem">
                                <deleteparameters>
                                    <asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
                                </deleteparameters>
                                <updateparameters>
                                    <asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="BARCODE"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="PDNAME"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="LOTNO"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="UNIT"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="UNITNAME"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PACKAGE"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PRODUCTSTOCK"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
                                </updateparameters>
                                <selectparameters>
                                    <asp:ControlParameter PropertyName="Text" Type="Double" Name="BasketLOID" ControlID="txtLOID"></asp:ControlParameter>
                                    <asp:ControlParameter PropertyName="Text" Type="String" Name="status" ControlID="txtStatus"></asp:ControlParameter>
                                </selectparameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView id="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <Columns>
                                
                                    <asp:TemplateField ShowHeader="False">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่" ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับ" InsertVisible="False">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BARCODE">
                                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                        <HeaderStyle Width="150px" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewLotNo" runat="server" CssClass="zCombobox" Width="115px"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวน">
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="LOID" >
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
                                    
                                    <asp:BoundField DataField="UNIT">
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
                                    
                                    <asp:BoundField DataField="PACKAGE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRODUCTSTOCK">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            
                            <asp:ObjectDataSource id="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}" 
                                SelectMethod="GetBasketItemBlank" TypeName="BasketItem">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>                     
                    <tr style="height:5px">
                        <td colspan="3">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:600">
                                <tr>
                                    <td valign="top" style="width:70px">
                                        &nbsp;หมายเหตุ</td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="530px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
     
</asp:Content>