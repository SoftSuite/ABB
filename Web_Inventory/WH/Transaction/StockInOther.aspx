<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockInOther.aspx.cs" Inherits="WH_Transaction_StockInOther" Title="ตรวจรับรายการอื่น" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ตรวจรับรายการอื่น</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" NameBtnPrint="พิมพ์ใบส่งตรวจ QC"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnSubmit="ส่งตรวจ QC"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitQCClick"/>
					 <uc1:ToolbarControl ID="ctlToolbar2" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true" NameBtnSubmit="ยืนยันรับเข้าคลัง"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnPrint="พิมพ์ใบตรวจรับ"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitFNClick" />            </td> 
        </tr>
        <tr height="10px">
            <td>            </td>
            <td>            </td> 
            <td>            </td> 
        </tr> 
        <tr height="25px" width="960px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="960">
                    <tr>
                        <td valign="top" width="420px" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="420px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        เลขที่ใบส่งของ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtInvNo" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
										<asp:TextBox ID="txtPopup" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:Label ID="lblRemark" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        รหัสผู้จำหน่าย</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSenderCode" runat="server" Width="80px" CssClass="zTextbox"></asp:TextBox><asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /><asp:TextBox ID="txtSenderName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>                                </td>
                                </tr> 
                            </table>                        </td>
                        <td width="3px">                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" width="320px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 120px">
                                        เลขที่ใบตรวจรับ</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtStockInCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 120px">
                                        วันที่ตรวจรับ</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlReceiveDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                				<tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:120px">
                                       สถานะ</td>
                                    <td style="width: 132px">
                                       <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr>  
				<tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:120px">
              </td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="100px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 

                            </table>                        
     </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4">                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="960px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
                                OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />                                        </FooterTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="110px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" Width="110px" MaxLength="20" Text='<%# Bind("BARCODE") %>' ></asp:TextBox> 
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" Width="110px" MaxLength="20" ></asp:TextBox>
					    <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="60px" ></asp:TextBox>
					    <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="60px" ></asp:TextBox>
					    <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Width="60px" ></asp:TextBox>
					     <asp:ImageButton ID="btnNewSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnNewSearchItem_Click"  />
                                        </FooterTemplate>
                                         <ItemStyle Width="160px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="160px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false" Width="100px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Enabled="false" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Enabled="false" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                                                                                          
                                    <asp:TemplateField HeaderText="Lot no">
                                        <ItemTemplate>
                                            <asp:Label ID="txtLotNoView" runat="server" Width="70px" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextboxR" Width="70px" Text='<%# Bind("LOTNO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewLotNo" runat="server" CssClass="zTextboxR" Width="70px" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="75px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPOCodeView" runat="server" Width="120px" Text='<%# Bind("CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPOCode" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" ReadOnly="true" Width="120px" MaxLength="20" Text='<%# Bind("CODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPOCode" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" ReadOnly="true" Width="120px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="จำนวนสั่ง">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPQtyView" runat="server" Width="50px" Text='<%# Bind("PQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="50px" Text='<%# Bind("PQTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="50px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>
                                                                                                           
                                    <asp:TemplateField HeaderText="จำนวนรับ">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSQtyView" runat="server" Width="50px" Text='<%# Bind("SQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSQty" runat="server" CssClass="zTextboxR"  Width="50px" Text='<%# Bind("SQTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewSQty" runat="server" CssClass="zTextboxR"  Width="50px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbUnitView" runat="server" CssClass="zCombobox" Enabled="false" Width="70px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zCombobox" Enabled="false" Width="70px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox" Enabled="false" Width="70px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="70px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="เหตุผล">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRemarkView" runat="server" Width="85px" Text='<%# Bind("REMARK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="zTextboxR" Width="85px" Text='<%# Bind("REMARK") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewRemark" runat="server" CssClass="zTextboxR" Width="85px" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="90px" />
                                    </asp:TemplateField>
                                    
                                    
                                 <asp:BoundField DataField="LOID">
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
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteStockInItem" SelectMethod="GetStockInItem" 
                                TypeName="StockInItem" UpdateMethod="UpdateStockInItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="CODE" Type="String" />
                                    <asp:Parameter Name="QCQTY" Type="Decimal" />
                                    <asp:Parameter Name="PQTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="QCREMARK" Type="String" />
                                    <asp:Parameter Name="QCRESULT" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                    <asp:Parameter Name="SQTY" Type="Decimal" />
                                    <asp:Parameter Name="REMARK" Type="String" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="stockin" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtStatus" Name="status" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="110px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
					    <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="75px" ></asp:TextBox>
					    <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="75px" ></asp:TextBox>
					    <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Width="75px" ></asp:TextBox>
					     <asp:ImageButton ID="btnNewSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnNewSearch_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="160px" HorizontalAlign="Center"  />
                                    </asp:TemplateField>
                                   
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Enabled = "false" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewLotNo" runat="server" CssClass="zTextboxR" Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="75px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งซื้อ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewPOCode" runat="server" AutoPostBack="true" CssClass="zTextboxR-View" ReadOnly="true" Width="115px" MaxLength="20" ></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="120px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวนสั่ง">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewPQty" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>                                    
                                                                        
                                    <asp:TemplateField HeaderText="จำนวนรับ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewSQty" runat="server" CssClass="zTextboxR" Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox" Enabled = "false" Width="70px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="เหตุผล" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewRemark" runat="server" CssClass="zTextboxR" Width="85px" ></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="90px" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                                SelectMethod="GetStockInItemBlank" TypeName="StockInItem" DeleteMethod="DeleteStockInItem" UpdateMethod="UpdateStockInItem">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="CODE" Type="String" />
                                    <asp:Parameter Name="QCQTY" Type="Decimal" />
                                    <asp:Parameter Name="PQTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="QCREMARK" Type="String" />
                                    <asp:Parameter Name="QCRESULT" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                    <asp:Parameter Name="SQTY" Type="Decimal" />
                                </UpdateParameters>
                            </asp:ObjectDataSource>                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">                        </td>
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
                            </table>                        </td>
                    </tr>
                </table>            </td> 
        </tr>
    </table>
</asp:Content>
