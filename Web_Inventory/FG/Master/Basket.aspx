<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Basket.aspx.cs" Inherits="FG_Master_Basket" Title="ข้อมูลกระเช้า" %>

<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลกระเช้า</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" OnSaveClick="SaveClick"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="กลับหน้าค้นหา" OnBackClick="BackClick" OnCancelClick="CancelClick"  />
                
            </td> 
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td style="height:15px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">รหัสกระเช้า</td>
            <td style="height:24px">
                <asp:TextBox ID="txtCode" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="Label13" runat="server" ForeColor="red" Text="*"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ชื่อกระเช้า ภาษาไทย</td>
            <td style="height:24px">
                <asp:TextBox ID="txtBasketName" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:CheckBox ID = "chkStatus" runat ="server" Text ="ใช้งาน"  />
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ชื่อกระเช้า ภาษาอังกฤษ</td>
            <td style="height:24px">
                <asp:TextBox ID="txtEname" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ชื่อย่อ</td>
            <td style="height:24px">
                <asp:TextBox ID="txtABBName" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">บาร์โค้ด</td>
            <td style="height:24px">
                <asp:TextBox ID="txtBarcode" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">หน่วยนับ</td>
            <td style="height:24px">
                <asp:DropDownList ID="cmbUnit" runat="server" Width="206px" CssClass="zComboBox"></asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ประเภทสินค้า</td>
            <td style="height:24px">
                <asp:DropDownList ID="cmbProductType" runat="server" Width="206px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged"></asp:DropDownList>
                <asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">กลุ่มสินค้า</td>
            <td style="height:24px">
                <asp:DropDownList ID="cmbProductGroup" runat="server" Width="206px" CssClass="zComboBox"></asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ราคาทุน</td>
            <td style="height:24px">
                <asp:TextBox ID="txtCost" runat="server" Width="200px" CssClass="zTextboxR" Enabled="False"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ราคาขาย</td>
            <td style="height:24px">
                <asp:TextBox ID="txtPrice" runat="server" Width="200px" CssClass="zTextboxR"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">ราคากลาง</td>
            <td style="height:24px">
                <asp:TextBox ID="txtSTDPrice" runat="server" Width="200px" CssClass="zTextboxR"></asp:TextBox>
                <asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">การคิดภาษี</td>
            <td style="height:24px">
                <asp:RadioButton ID="radVAT" runat="server" Text="ราคาขายรวม VAT" GroupName="VAT" Width="120px" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radNoVAT" runat="server" Text="ราคาขายไม่รวม VAT" GroupName="VAT" />
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">การคิดส่วนลด</td>
            <td style="height:24px">
                <asp:RadioButton ID="radDiscount" runat="server" Text="คิดส่วนลด" GroupName="Discount" Width="76px" Checked="true" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radNoDiscount" runat="server" Text="ไม่คิดส่วนลด" GroupName="Discount" />
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px">
                การคืนเงิน</td>
            <td style="height:24px">
                <asp:RadioButton ID="radRefund" runat="server" Text="คืนเงิน" GroupName="Refund"  Width="56px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radNoRefund" runat="server" Text="ไม่คืนเงิน" GroupName="Refund" Checked="true"  />
            </td>
        </tr>
         <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:141px; height:24px"></td>
            <td style="height:24px">
                <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                <asp:TextBox ID="txtProductMaster" runat="server" CssClass="zHidden" Width="57px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height:25px" colspan="3"></td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr class="t_headtext" style="height:25px">
            <td>
                รายละเอียดสินค้า
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" 
      EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowCancelingEdit="gvResult_RowCancelingEdit" OnRowEditing="gvResult_RowEditing" OnDataBound="gvResult_DataBound" OnRowDeleting="gvResult_RowDeleting" OnRowDataBound="gvResult_RowDataBound" OnRowUpdating="gvResult_RowUpdating" >
                        <Columns>
                            <asp:TemplateField >
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit"
                                        ImageUrl="~/Images/icn_edit.gif" />&nbsp;<asp:ImageButton ID="imbDelete"
                                        runat="server" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imbSave" runat="server"  CommandName="Update"
                                        ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel"
                                            runat="server" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ลำดับที่">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสสินค้า"> 
                                <ItemStyle HorizontalAlign="Center" Width="135px" />
                                <HeaderStyle Width="135px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBarCode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อสินค้า">
                                <ItemStyle Width="250px" />
                                <HeaderStyle Height="25px" Width="250px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ปริมาณการใช้">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="130px" />
                                <HeaderStyle HorizontalAlign="Center" Width="130px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หน่วย">
                                <ItemStyle HorizontalAlign="Right" Width="110px" />
                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("UNIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COST" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("COST") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRICE" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("PRICE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STDPRICE" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("STDPRICE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        <PagerSettings Visible="False" />
        </asp:GridView> 
        <asp:Panel ID ="PnlHeader" runat ="server" >
        <table border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse" >
            <tr class="t_headtext" align="center">
                <td style="width: 51px; height: 24px;"></td>
                <td style="width: 59px; height: 24px;">ลำดับ</td>
                <td style="width: 135px; height: 24px;">รหัสสินค้า</td>
                <td style="width: 249px; height: 24px;">ชื่อสินค้า</td>
                <td style="width: 130px; height: 24px;">ปริมาณการใช้</td>
                <td style="width: 110px; height: 24px;">หน่วย</td>
            </tr>
       </table>
       </asp:Panel>
       <table>
            <tr>
                <td style="width: 51px; height: 24px;" align="center">
                    <asp:ImageButton id="imbAddSave" runat="server" ImageUrl="~/Images/icn_save.gif" OnClick="imbAddSave_Click" />
                </td>
                <td align="center" style="width: 59px; height: 24px;"><asp:Label id="lblOrderNo" runat="server"></asp:Label></td>
                <td style="width: 135px; height: 24px;">
                    <asp:TextBox ID="txtAddBarcode" runat="server" Width="110px" AutoPostBack="True" OnTextChanged="txtAddBarcode_TextChanged1"></asp:TextBox>
                    <asp:Label id="Label10" runat="server" Text="*" ForeColor="red"></asp:Label>
                </td>
                <td style="width: 249px; height: 24px;">
                    <asp:DropDownList ID="cmbAddProduct" runat="server"  Width="235px" OnSelectedIndexChanged="cmbAddProduct_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    <asp:Label id="Label11" runat="server" Text="*" ForeColor="red"></asp:Label>
                </td>
                <td style="width: 130px; height: 24px;">
                    <asp:TextBox ID="txtAddQuantity" runat="server" Width="110px" CssClass="zTextboxR">1</asp:TextBox>
                    <asp:Label id="Label12" runat="server" Text="*" ForeColor="red"></asp:Label>
                </td>
                <td style="width: 110px; height: 24px;">
                    <asp:TextBox ID="txtAddUnit" runat="server" Width="105px" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAddCost" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAddPrice" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAddSTDPrice" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAddLOID" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAddUnitLOID" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" >
        <tr>
            <td style="height:15px" colspan="2"></td>
        </tr>
        <tr style="height:25px">
            <td valign="top" style="width: 86px">
                หมายเหตุ
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" Width="618px" CssClass="zTextbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height:15px" colspan="2"></td>
        </tr>
    </table>
</asp:Content>

