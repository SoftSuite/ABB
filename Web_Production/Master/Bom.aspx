<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Bom.aspx.cs" Inherits="Master_Bom" Title="BOM (Bill of Material) รายละเอียด" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;BOM (Bill of Material) รายละเอียด</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace" style="height: 8px">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="กลับหน้าค้นหา"  OnSaveClick ="SaveClick" OnBackClick ="BackClick" OnCancelClick="CancelClick" />
            </td> 
        </tr>
        <tr>
            <td  style =" height: 10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
                <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">
                    <tr>
                        <td style="width: 30px; height: 10px">
                        </td>
                        <td style="width: 150px; height: 10px">
                        </td>
                        <td style="width: 500px; height: 10px">
                        </td>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px"></td>
                        <td style="width:150px; height:25px">Barcode</td>
                        <td style=" width:500px; height:25px">
                            <asp:TextBox ID="txtBarcode" runat="server" Width="150px" CssClass="zTextBox" MaxLength="20"  ></asp:TextBox>&nbsp;
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" ImageAlign="AbsMiddle" />
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="150px"></asp:TextBox></td>
                        <td style="height:25px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px"></td>
                        <td style="width:150px; height:25px">
                            สินค้า/วัตถุดิบ</td>
                        <td style="width:500px; height:25px">
                            <asp:DropDownList ID="cmbProduct" runat="server" Width="300px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label  ID="TextBox4" runat="server" CssClass="zRemark">*</asp:Label></td>
                        <td style="height:25px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px"></td>
                        <td style="width:150px; height:25px">กลุ่มสินค้า</td>
                        <td style="width:500px; height:25px">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" Width="300px" CssClass="zComboBox" Enabled="False" >
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td style="height:25px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px"></td>
                        <td style="width:150px; height:25px">ประเภทสินค้า</td>
                        <td style=" width:500px; height:25px">
                            <asp:DropDownList ID="cmbProductType" runat="server" Width="300px" CssClass="zComboBox" Enabled="False" >
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td style="height:25px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px"></td>
                        <td style="width:150px; height:25px" valign="top">วิธีเตรียม</td>
                        <td style="width:500px; height:25px">
                            <asp:TextBox ID="txtProcess" runat="server" Width="500px" Height = "70px" CssClass="zTextbox" TextMode="MultiLine" MaxLength="1000"></asp:TextBox></td>
                        <td style="height:25px;" valign="top">
                            <asp:Label ID="TextBox3" runat="server" CssClass="zRemark">*</asp:Label></td>
                    </tr>
                     <tr>
                        <td style="width:30px; height:25px;">&nbsp;  
                        </td>
                        <td style="width:150px; height:25px">ฉายรังสี</td>
                        <td style="width:500px; height:25px">
                        <asp:RadioButton ID="Radiation" runat="server" Text="ฉายรังสี" GroupName="radiation" AutoPostBack="True"  />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="NonRadiation" runat="server" Text="ไม่ฉายรังสี" GroupName="radiation" AutoPostBack="True"   />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:30px; height:25px;">&nbsp;  
                        </td>
                        <td style="width:150px; height:25px"></td>
                        <td style="width:500px; height:25px">
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px; height: 10px">
                        </td>
                        <td style="width: 150px; height: 10px">
                        </td>
                        <td style="width: 500px; height: 10px">
                        </td>
                        <td style="height: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height:10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
                <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="800px" DataKeyNames="RANK" DataSourceID="ItemDataSource" 
                    OnRowDataBound="grvItem_RowDataBound" OnRowCommand="grvItem_RowCommand" OnRowUpdated="grvItem_RowUpdated"
                    OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating"><PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField >
                            <EditItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server"  CommandName="Update"
                                    ImageUrl="~/Images/icn_save.gif" />
                                <asp:ImageButton ID="imbCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit"
                                    ImageUrl="~/Images/icn_edit.gif" />
                                <asp:ImageButton ID="imbDelete"
                                    runat="server" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Center" Height="24px" />
                            <HeaderStyle Width="45px" Height="24px" />
                            <ItemStyle HorizontalAlign="Center" Width="45px" Height="24px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RANK" HeaderText="ลำดับที่" InsertVisible="false" ReadOnly="true">
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="รหัสวัตถุดิบ">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Text='<%# Bind("BARCODE") %>' AutoPostBack="true"
                                    Width="120px" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtBarcodeNew" runat="server" CssClass="zTextbox" Width="120px" AutoPostBack="true" OnTextChanged="txtBarcodeNew_TextChanged1"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBarCode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="125px" />
                            <ItemStyle Width="125px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                            <EditItemTemplate>
                                <asp:DropDownList ID="cmbMaterial" CssClass="zComboBox" Width="245px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbMaterial_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="cmbMaterialNew" CssClass="zComboBox" Width="245px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbMaterialNew_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="250px" />
                            <ItemStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ประเภท">
                            <EditItemTemplate>
                                <asp:Label ID="txtProductType" runat="server" Text='<%# Bind("PRODUCTTYPE") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="txtProductTypeNew" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductType" runat="server" Text='<%# Bind("PRODUCTTYPE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ปริมาณการใช้">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMaster" runat="server" CssClass="zTextboxR" Text='<%# Bind("MASTER") %>'
                                    Width="95px"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtMasterNew" runat="server" CssClass="zTextboxR" Width="95px"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMaster" runat="server" Text='<%# Convert.ToDouble(Convert.IsDBNull(Eval("MASTER")) ? "0" : Eval("MASTER")).ToString(ABB.Data.Constz.Dbl5Format) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" HorizontalAlign="Right"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="หน่วย">
                            <EditItemTemplate>
                                <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="txtUnitNameNew" runat="server" ></asp:Label>
                                <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteBomItem"
                    OldValuesParameterFormatString="{0}" SelectMethod="GetBomItem" TypeName="BomItem"
                    UpdateMethod="UpdateBomItem">
                    <DeleteParameters>
                        <asp:Parameter Name="RANK" Type="Double" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="RANK" Type="Decimal" />
                        <asp:Parameter Name="BARCODE" Type="String" />
                        <asp:Parameter Name="NAME" Type="String" />
                        <asp:Parameter Name="MASTER" Type="Decimal" />
                        <asp:Parameter Name="PRODUCTTYPE" Type="String" />
                        <asp:Parameter Name="UNITNAME" Type="String" />
                        <asp:Parameter Name="UNIT" Type="Decimal" />
                        <asp:Parameter Name="LOID" Type="Decimal" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtProduct" DefaultValue="0" Name="productBarcode"
                            PropertyName="Text" Type="Double" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  
                    Width="800px" DataSourceID="ItemNewDataSource" Visible="False" OnRowDataBound="grvItemNew_RowDataBound" OnRowCommand="grvItemNew_RowCommand">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                            </ItemTemplate>
                            <HeaderStyle Width="45px" />
                            <ItemStyle HorizontalAlign="Center" Width="45px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ลำดับที่" >
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="รหัสวัตถุดิบ">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBarcodeNew" runat="server" CssClass="zTextbox" Width="120px" AutoPostBack="true" OnTextChanged="txtBarcodeNew_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="125px" />
                            <ItemStyle HorizontalAlign="Center" Width="125px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                            <ItemTemplate>
                                <asp:DropDownList ID="cmbMaterialNew" CssClass="zComboBox" Width="245px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbMaterialNew_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle Width="250px" />
                            <ItemStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ประเภท">
                            <ItemTemplate>
                                <asp:Label ID="txtProductTypeNew" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ปริมาณการใช้">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMasterNew" runat="server" CssClass="zTextboxR" Width="95px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" HorizontalAlign="Right"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="หน่วย">
                            <ItemTemplate>
                                <asp:Label ID="txtUnitNameNew" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView> 
                <asp:ObjectDataSource ID="ItemNewDataSource" runat="server" OldValuesParameterFormatString="{0}"
                    SelectMethod="GetBomItemBlank" TypeName="BomItem"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>