<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PromotionSales.aspx.cs" Inherits="Master_PromotionSales" Title="กำหนดราคาสินค้าส่งเสริมการขาย" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarCtl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;กำหนดราคาสินค้าส่งเสริมการขาย
            </td> 
        </tr> 
        <tr height="25">
            <td class="toolbarplace">
                <uc2:ToolbarCtl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
            </td> 
        </tr>

        <tr height="25px">
            <td></td> 
        </tr> 
        <tr>
            <td style="height: 65px">
                <table border= "0" cellspacing="0" cellpadding="0" width="800px">
                    <tr ้height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            รหัสส่งเสริมการขาย</td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Text="" Width="254px"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อการส่งเสริมการขาย</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Text="" Width="254px"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            คลังสินค้า</td> 
                        <td>
                             <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="254px" OnSelectedIndexChanged="cmbWarehouse_SelectedIndexChanged">
                            </asp:DropDownList> <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                    <tr height="25px" class="zHidden">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            โซน</td> 
                        <td>
                             <asp:DropDownList ID="cmbZone" runat="server" CssClass="zComboBox" Width="254px">
                            </asp:DropDownList> <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr ้height="25px" class="zHidden">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            วันที่กำหนด</td>
                        <td><uc1:DatePickerControl ID="ctlCreateOn" runat="server" Enabled="false" />
                        </td>
                    </tr>  
                    <tr ้height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            วันที่เริ่มใช้</td>
                        <td>
                            <uc1:DatePickerControl ID="ctlEFDate" runat="server" />
                            &nbsp;<asp:Label ID="Label5" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr>  
                    <tr ้height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            วันที่สิ้นสุด</td>
                        <td>
                            <uc1:DatePickerControl ID="ctlEPDate" runat="server" />
                            &nbsp;<asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr>  
                    <tr ้height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ราคาซื้อรวมขั้นต่ำ</td>
                        <td>
                            <asp:TextBox ID="txtLowerPrice" runat="server" CssClass="zTextboxR" Text="" Width="80px" MaxLength="5"></asp:TextBox>
                            บาท</td>
                    </tr>  
                    <tr ้height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ส่วนลด (%)</td>
                        <td>
                            <asp:TextBox ID="txtDISCOUNT" runat="server" CssClass="zTextboxR" Text="" Width="80px" MaxLength="2" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                         <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr>  
                     <tr height="10">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>&nbsp;
                            </td>
                    </tr> 
                </table>
            </td>
        </tr>
        <tr>
            <td class="zHidden">
                <asp:LinkButton ID="btnNewAll" runat="server" CssClass="toolbarbutton" OnClick="btnNewAll_Click" >LinkButton</asp:LinkButton>
            </td> 
        </tr> 
        <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True" Visible="false"
                                Width="700px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
                                OnRowDataBound="grvItem_RowDataBound" OnRowCommand="grvItem_RowCommand" onRowDeleted="grvItem_RowDeleted"
                                OnRowUpdating="grvItem_RowUpdating" OnRowUpdated="grvItem_RowUpdated">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel"
                                                runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="278" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" ></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false" Width="278"></asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="278" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="280px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="58" Text='<%# Bind("UNAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Text='<%# Bind("UNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="58"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาเดิม">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="78" Text='<%# Convert.ToDouble(Eval("PRICEOLD")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtOldPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("PRICEOLD")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="78" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาใหม่">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" Width="78" MaxLength="20" Text='<%# Convert.ToDouble(Eval("PRICENEW")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNewPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("PRICENEW")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="78" MaxLength="20" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="BARCODE">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="NAME">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="UNAME">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRICEOLD">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRICENEW">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeletePromotionItem" SelectMethod="GetPromotionItem" 
                                TypeName="PromotionItem" UpdateMethod="UpdatePromotionItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal"/>
                                    <asp:Parameter Name="NAME" Type="String" />
                                    <asp:Parameter Name="UNAME" Type="String" />
                                    <asp:Parameter Name="PRICEOLD" Type="Decimal" />
                                    <asp:Parameter Name="PRICENEW" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />  
                                    <asp:Parameter Name="PRODUCT" Type="String" />  
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
                               <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="loid" PropertyName="Text"
                                        Type="Double" />
                                    </SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="700px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" 
                                OnRowDataBound="grvItemNew_RowDataBound" OnRowCommand="grvItemNew_RowCommand">
                                <PagerSettings Visible="False" />
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton id="imbSave" runat="server" ImageUrl="~/Images/icn_save.gif" CommandName="Insert" CausesValidation="True"></asp:ImageButton> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <HeaderStyle Width="280px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:DropDownList id="cmbNewProduct" runat="server" Width="278px" CssClass="zCombobox" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="58px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาเดิม" InsertVisible="False">
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="78px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาใหม่">
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="78px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                           <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetPromotionSaleItemBlank" TypeName="PromotionItemOrder" DataObjectTypeName="ABB.Data.Sales.PromotionSalesItemData">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
</asp:Content>