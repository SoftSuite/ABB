<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinReturn.aspx.cs" Inherits="FG_Transaction_StockinReturn" Title="ใบรับคืนสินค้า" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบรับคืนสินค้า</td> 
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
            <td colspan="3"></td>
        </tr> 
        <tr style="height:25px; width:800px">
            <td valign="top" style="width:615">
                <table border="0" cellpadding="0" cellspacing="0" style="width:615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width:400">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:430px" class="zCombobox">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px"></td>
                                    <td style="width: 190px"></td>
                                    <td style="width: 50px"></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ประเภทรับคืน</td>
                                    <td style="width: 190px">
                                        <asp:DropDownList ID="cmbDocType" runat="server" Width="150px" Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="cmbDocType_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                        <asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="0px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtRefLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtRefTable" runat="server" CssClass="zHidden" Width="0px"></asp:TextBox>
                                    </td>
                                    <td style="width: 50px">
                                        เลขที่</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtRefCode" runat="server" Width="130px" ReadOnly="True" CssClass="zTextbox-View"></asp:TextBox> 
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        รหัสลูกค้า</td>
                                    <td colspan="3" style="width:375px">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="95px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="btnCustomerSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnCustomerSearch_Click" Visible="False" />
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="225px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3" style="width:375px">
                                        <asp:DropDownList ID="cmbTitle" runat="server" Width="90px"></asp:DropDownList> 
                                        <asp:TextBox ID="txtName" runat="server" Width="125px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="125px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3" style="width:375px">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="355px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        โทรศัพท์</td>
                                    <td style="width: 190px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 50px">
                                        โทรสาร</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px"></td>
                                    <td style="width: 190px"></td>
                                    <td style="width: 50px"></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td style="width:5">&nbsp;
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width:205">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:205">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 120px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่รับคืน</td>
                                    <td style="width: 120px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        วันที่รับคืน</td>
                                    <td style="width: 120px">
                                        <uc2:DatePickerControl ID="ctlReceiveDate" runat="server" Enabled="false" />
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
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
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
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="65px" Text='<%# Bind("BARCODE") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="65px" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="110px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProduct" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="145px" Text='<%# Bind("PDNAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtProductView" runat="server" Width="145px" Text='<%# Bind("PDNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="145px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px"></HeaderStyle>
                                    </asp:TemplateField>                                    
 
                                    <asp:TemplateField HeaderText="Lot No">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="115px" Text='<%# Bind("LOTNO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtLotNoView" runat="server" Width="115px" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewLotNo" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="115px" Text=""></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="120px"></HeaderStyle>
                                    </asp:TemplateField>                                    
                                    
                                    <asp:TemplateField HeaderText="จำนวนดี">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="55px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="55px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="จำนวนเสีย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLostQty" runat="server" CssClass="zTextboxR" Width="55px" Text='<%# Bind("QTYLOST") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtLostQtyView" runat="server" Width="55px" Text='<%# Bind("QTYLOST") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLostNewQty" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="45px" Text='<%# Bind("UNITNAME") %>' ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Width="45px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="45px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคา">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPriceView" runat="server" Width="55px" Text='<%# Bind("PRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("NETPRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNetPriceView" runat="server" Width="55px" Text='<%# Bind("NETPRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวนเดิม" Visible =false>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOldQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text='<%# Bind("OLDQTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtOldQtyView" runat="server" Width="55px" Text='<%# Bind("OLDQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtOldNewQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="60px"></HeaderStyle>
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
                                    
                                    <asp:BoundField DataField="REFLOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="REFTABLE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="STATUS">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>                                    
                                    
                                </Columns>
                                
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                                
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteItem"
                                OldValuesParameterFormatString="{0}" SelectMethod="GetItem" TypeName="StockinReturnItem"
                                UpdateMethod="UpdateItem">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="PDNAME" Type="String" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                     <asp:Parameter Name="OLDQTY" Type="Decimal" />
                                     <asp:Parameter Name="QTYLOST" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="UNITNAME" Type="String" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="REFTABLE" Type="String" />
                                    <asp:Parameter Name="STATUS" Type="String" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="LOID" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtStatus" Name="status" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:GridView ID="grvItemNew" runat="server" DataSourceID="NewItemDataSource" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
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

                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <HeaderStyle Width="110px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="65px"></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <HeaderStyle Width="150px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="145px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewLotNo" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="115px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวนดี">
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="จำนวนเสีย">
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLostNewQty" runat="server" CssClass="zTextboxR" Width="55px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextbox-View" ReadOnly="true" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคา" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวนเดิม" visible=false>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOldNewQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="55px" Text="0"></asp:TextBox>
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
                                    
                                    <asp:BoundField DataField="REFLOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="REFTABLE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="STATUS">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>                                    
                                    
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />                                
                            </asp:GridView>
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                                SelectMethod="GetItemBlank" TypeName="StockinReturnItem"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr style="height:5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="2">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:550">
                                <tr>
                                    <td valign="top" style="width: 75px">
                                        &nbsp;หมายเหตุ</td>
                                    <td style="width:540">
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="475px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td> 
            <td style="width:2px">
                &nbsp;
            </td> 
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted;">
                <table border="0" cellpadding="0" cellspacing="0" style="width:190px">
                    <tr style="height:3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td style="width: 80px">
                            พนักงาน</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="100px"/>
                        </td> 
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="100px"/>
                        </td> 
                    </tr> 
                </table>
                <hr/> 
                <table border="0" cellpadding="0" cellspacing="0" style="width:190px">
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td colspan="2">
                            &nbsp;จำนวนเงิน</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True" Width="80px"/>
                        </td> 
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td style="width: 50px">
                            &nbsp;VAT</td>
                        <td style="width: 50px">
                            <asp:TextBox ID="txtVat" runat="server" CssClass="zTextboxR-View" ReadOnly="True" Width="25px" />%
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalVat" runat="server" CssClass="zTextboxR-View" ReadOnly="True" Width="80px" />
                        </td> 
                    </tr>
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td colspan="2">
                            &nbsp;ยอดสุทธิ</td>
                        <td>
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True" Width="80px"/>
                        </td> 
                    </tr> 
                </table>
            </td> 
        </tr>
    </table>    
</asp:Content>