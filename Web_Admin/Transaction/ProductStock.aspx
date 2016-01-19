<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductStock.aspx.cs" Inherits="Transaction_ProductStock" Title="สินค้าคงคลัง" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border ="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;สินค้าคงคลัง</td> 
        </tr> 
        <tr>
            <td style="height:10px">
            </td> 
        </tr> 
        <tr height="25">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000" class="searchTable">
                    <tr>
                        <td class="subheadertext" colspan="3">
                            &nbsp;ค้นหา</td>
                    </tr>
                    <tr>
                        <td style="height:10px; width: 50px">
                        </td>
                        <td style="height:10px; width: 150px">
                        </td>
                        <td style="height:10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height:25px; width: 50px">
                        </td>
                        <td style="height:25px; width: 150px">
                            คลังสินค้า</td> 
                        <td>
                            <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbWarehouse_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td> 
                    </tr> 
                    <tr >
                        <td style="height:10px; width: 50px">
                        </td>
                        <td style="height:10px; width: 150px">
                        </td>
                        <td style="height:10px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height:10px;">
            </td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="grvStock" Width="600px" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" DataKeyNames="LOID" DataSourceID="StockDataSource" OnRowDataBound="grvStock_RowDataBound" OnRowUpdated="grvStock_RowUpdated" OnRowUpdating="grvStock_RowUpdating">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                    ImageUrl="~/Images/icn_edit.gif"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                    ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                        runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                            </EditItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <HeaderStyle Width="40px" HorizontalAlign="Center" />
                            <FooterStyle Width="40px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO" HeaderText="ลำดับ" InsertVisible="False" ReadOnly="True">
                            <ItemStyle Width="50px" HorizontalAlign="center"/>
                            <HeaderStyle Width="50px" /> 
                        </asp:BoundField>
                       <asp:BoundField DataField="LOID" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTNAME" HeaderText="สินค้า" InsertVisible="False" ReadOnly="True">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="จำนวน">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Convert.ToDouble(Convert.IsDBNull(Eval("QTY")) ? "0" : Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="75px" Text='<%# Convert.ToDouble(Convert.IsDBNull(Eval("QTY")) ? "0" : Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="UNITNAME" HeaderText="หน่วย" InsertVisible="False" ReadOnly="True">
                            <ItemStyle Width="120px" />
                            <HeaderStyle Width="120px" /> 
                        </asp:BoundField>
                    </Columns> 
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                    <PagerSettings Visible="False" />
                </asp:GridView>
                <asp:ObjectDataSource ID="StockDataSource" runat="server" SelectMethod="GetStockItem"
                    TypeName="ProductStockItem" UpdateMethod="UpdateStock">
                    <UpdateParameters>
                        <asp:Parameter Name="LOID" Type="Decimal" />
                        <asp:Parameter Name="PRODUCTNAME" Type="String" />
                        <asp:Parameter Name="QTY" Type="Decimal" />
                        <asp:Parameter Name="UNITNAME" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="cmbWarehouse" DefaultValue="0" Name="warehouse"
                            PropertyName="SelectedValue" Type="Double" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td> 
        </tr>
    </table>
</asp:Content>

