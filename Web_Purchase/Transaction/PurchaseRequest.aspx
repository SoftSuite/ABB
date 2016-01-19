<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PurchaseRequest.aspx.cs" Inherits="Transaction_PurchaseRequest" Title="ใบบันทึกรายการเพื่อการจัดซื้อ/จัดจ้าง" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="headtext">
                &nbsp;ใบบันทึกรายการเพื่อการจัดซื้อ/จัดจ้าง</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnPrintClick="PrintClick" OnSubmitClick="SubmitClick"/>
                <asp:LinkButton ID="btnVoid" runat="server" CssClass="toolbarbutton" OnClick="btnVoid_Click">LinkButton</asp:LinkButton>
                <asp:LinkButton ID="btnCancelPR" runat="server" CssClass="toolbarbutton" OnClick="btnCancelPR_Click" Visible="False">LinkButton</asp:LinkButton></td> 
        </tr>
        <tr style="height:10px">
            <td>
            </td>
        </tr> 
        <tr style="height:25px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:500px" class="zCombobox">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px"></td>
                                    <td style="width: 385px"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        ประเภท</td>
                                    <td>
                                        <asp:DropDownList ID="cmbPurchaseType" runat="server" Width="150px" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="cmbPurchaseType_SelectedIndexChanged" />
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="zRemark" />
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtRequestByID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtDivision" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px" valign="top">
                                        ความต้องการ</td>
                                    <td>
                                        <asp:TextBox ID="txtRequirement" runat="server" Height="50px" TextMode="MultiLine" Width="360px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px" valign="top">
                                        เหตุผลในการขอซื้อ</td>
                                    <td>
                                        <asp:TextBox ID="txtReason" runat="server" Height="50px" TextMode="MultiLine" Width="360px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:Label ID="lblRemark" runat="server" Text="*" CssClass="zRemark"></asp:Label>
                                    </td>
                                </tr>
                                  <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px" valign="top">
                                        จากบริษัท/ห้างร้าน</td>
                                    <td>
                                        <asp:TextBox ID="txtFromCompany" runat="server" Height="21px" Width="360px"></asp:TextBox>
                                      </td>
                                </tr>
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px"></td>
                                    <td style="width: 385px"></td>
                                </tr>                              
                            </table> 
                        </td>
                        <td style="width: 3px">&nbsp;
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width:100%">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:300">
                                <tr style="height:5px">
                                    <td style="width: 5px; height: 5px;"></td>
                                    <td style="width: 100px; height: 5px;"></td>
                                    <td style="width: 190px; height: 5px;"></td>
                                    <td style="width: 5px; height: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 100px">
                                        เลขที่ใบขอซื้อ</td>
                                    <td style="width: 190px">
                                        <asp:TextBox ID="txtPDRequestCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="120px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 100px">
                                        ลงวันที่</td>
                                    <td style="width: 190px"><uc2:DatePickerControl ID="ctlRequestDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 100px">พนักงาน</td>
                                    <td style="width: 190px">
                                        <asp:TextBox ID="txtRequestBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="120px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 100px">สถานะ</td>
                                    <td style="width: 190px">
                                        <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="120px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height:10px">
            <td>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:GridView id="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    DataKeyNames="LOID" DataSourceID="ItemDataSource" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowDeleted="grvItem_RowDeleted"
                    OnRowUpdated="grvItem_RowUpdated" OnRowUpdating="grvItem_RowUpdating" ShowFooter="True">
                    <columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก" ImageUrl="~/Images/icn_save.gif" /> 
                                <asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่" ImageUrl="~/Images/icn_save.gif" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข" ImageUrl="~/Images/icn_edit.gif"/>
                                <asp:ImageButton ID="imbDelete" AlternateText="ลบ" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" ImageAlign="AbsMiddle"/>
                            </ItemTemplate>

                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <FooterStyle Width="50px" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                                
                        <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                            <HeaderStyle Width="50px"></HeaderStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="เร่งด่วน">
                            <FooterTemplate>
                                <asp:CheckBox ID="chkUrgent" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUrgent" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <HeaderStyle Width="50px" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="เตรียมวัตถุดิบ">
                            <FooterTemplate>
                                <asp:CheckBox ID="chkMaterial" runat="server" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMaterial" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <HeaderStyle Width="50px" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสสินค้า">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="110px" MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                <asp:ImageButton ID="imbSearch" runat="server" CausesValidation="True" AlternateText="ค้นหา" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" OnClick="imbSearch_Click" /> 
                                <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="1px" Text='<%# Bind("PRODUCT") %>'></asp:TextBox>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Width="1px" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="110px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
                                <asp:ImageButton ID="imbNewSearch" runat="server" CausesValidation="True" AlternateText="ค้นหา" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" OnClick="imbNewSearch_Click" /> 
                                <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtBarcodeView" runat="server" Width="145px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="160px" />
                            <ItemStyle Width="160px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="สินค้า">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox-View" Width="295px" Text='<%# Bind("PRODUCTNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewProductName" runat="server" CssClass="zTextbox-View" Width="295px" Text=""></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtProductView" runat="server" Width="295px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="จำนวน">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="55px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtQtyView" runat="server" Width="55px" Text='<%# Bind("QTY") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="หน่วย">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUnitName" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="75px" Text="0"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewUnitName" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="75px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtUnitView" runat="server" Width="75px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ราคาเดิม">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("OLDPRICE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtOldPriceView" runat="server" Width="55px" Text='<%# Bind("OLDPRICE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ราคาปัจจุบัน">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCurPrice" runat="server" CssClass="zTextboxR" Width="55px" Text='<%# Bind("CURPRICE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewCurPrice" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtCurPriceView" runat="server" Width="55px" Text='<%# Bind("CURPRICE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ราคาต่ำสุด">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMinPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("MINPRICE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewMinPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtMinPriceView" runat="server" Width="55px" Text='<%# Bind("MINPRICE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MIN">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMinStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("MINSTOCK") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewMinStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtMinStockView" runat="server" Width="55px" Text='<%# Bind("MINSTOCK") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MAX">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMaxStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("MAXSTOCK") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewMaxStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtMaxStockView" runat="server" Width="55px" Text='<%# Bind("MAXSTOCK") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="STOCK">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("STOCK") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtStockView" runat="server" Width="55px" Text='<%# Bind("STOCK") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="3เดือน">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLast3Mon" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("LAST3MON") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewLast3Mon" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtLast3MonView" runat="server" Width="55px" Text='<%# Bind("LAST3MON") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ปีที่แล้ว">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastYear" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("LASTYEAR") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNewLastYear" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtLastYearView" runat="server" Width="55px" Text='<%# Bind("LASTYEAR") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="กำหนดส่ง">
                            <EditItemTemplate>
                                <uc2:DatePickerControl ID="ctlDueDate" DateValue='<%# Bind("DUEDATE") %>' runat="Server"></uc2:DatePickerControl>
                            </EditItemTemplate>
                            <FooterTemplate>
                                 <uc2:DatePickerControl ID="ctlNewDueDate" DateValue='' runat="Server"></uc2:DatePickerControl>
                            </FooterTemplate>
                            <ItemTemplate>
                                <uc2:DatePickerControl ID="ctlDueDateView" DateValue='<%# Bind("DUEDATE" ) %>' Enabled="false" runat="Server"></uc2:DatePickerControl>
                            </ItemTemplate>
                            <HeaderStyle Width="130px" />
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="PRODUCT">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="QTY">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="UNIT">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="MINSTOCK">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
    
                        <asp:BoundField DataField="MAXSTOCK">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="STOCK">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="OLDPRICE">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="CURPRICE">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="MINPRICE">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="LAST3MON">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="LASTYEAR">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="URGENT">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ISMATERIAL">
                            <ControlStyle CssClass="zHidden"></ControlStyle>
                            <ItemStyle CssClass="zHidden"></ItemStyle>
                            <HeaderStyle CssClass="zHidden"></HeaderStyle>
                            <FooterStyle CssClass="zHidden"></FooterStyle>
                        </asp:BoundField>
                    </columns>
                    <headerstyle cssclass="t_headtext" />
                    <alternatingrowstyle cssclass="t_alt_bg" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeletePRItem" SelectMethod="GetPRItem" 
                    TypeName="PRItem" OldValuesParameterFormatString="{0}" UpdateMethod="UpdatePRItem">
                    <DeleteParameters>
                        <asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
                    </DeleteParameters>
                    <updateparameters>
                        <asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="UNIT"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="MINSTOCK"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="MAXSTOCK"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="STOCK"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="OLDPRICE"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="CURPRICE"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="MINPRICE"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="LAST3MON"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="LASTYEAR"></asp:Parameter>
                        <asp:Parameter Type="String" Name="URGENT"></asp:Parameter>
                        <asp:Parameter Type="String" Name="ISMATERIAL"></asp:Parameter>
                        <asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
                        <asp:Parameter Type="String" Name="BARCODE"></asp:Parameter>
                        <asp:Parameter Type="DateTime" Name="DUEDATE"></asp:Parameter>
                        </updateparameters>
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="Double" Name="PDRequest" ControlID="txtLOID"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" Name="status" ControlID="txtStatus"></asp:ControlParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                
                <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                    ImageUrl="~/Images/icn_save.gif" />
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="เร่งด่วน">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUrgent" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="เตรียมวัตถุดิบ">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMaterial" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="บาร์โค้ด">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="110px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                <asp:ImageButton ID="imbNewSearch" runat="server" CausesValidation="True" AlternateText="ค้นหา" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" OnClick="imbNewSearchNew_Click" /> 
                                <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="160px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="สินค้า">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewProductName" CssClass="zTextbox-View" runat="server" Width="295px" Text=""></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="จำนวน">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="55px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtNewUnitName" CssClass="zTextbox-View" runat="server" Width="75px" Text=""></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ราคาเดิม" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewOldPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>                                        
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ราคาปัจจุบัน" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewCurPrice" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ราคาต่ำสุด" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewMinPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MIN" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewMinStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>                                        
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MAX" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewMaxStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>                                        
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="STOCK" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewStock" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="3เดือน" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewLast3Mon" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ปีที่แล้ว" InsertVisible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNewLastYear" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="กำหนดส่ง" InsertVisible="False">
                            <ItemTemplate>
                                <uc2:DatePickerControl ID="ctlNewDueDate" DateValue='' runat="Server"></uc2:DatePickerControl>
                            </ItemTemplate>
                            <HeaderStyle Width="130px" />
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle BorderWidth="0px" Width="1200px"  />
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView> 
                <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                    SelectMethod="GetPRItemBlank" TypeName="PRItem"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr style="height:5px">
            <td>
            </td>
        </tr> 
        <tr>
            <td style="height: 65px">
                <table border="0" cellspacing="0" cellpadding="0" style="width:700px">
                    <tr>
                        <td valign="top" style="width:110px">
                            &nbsp;หมายเหตุ</td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="360px" CssClass="zTextbox"></asp:TextBox></td> 
                    </tr> 
                </table> 
            </td>
        </tr>
    </table>
</asp:Content>

