<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="MemberType.aspx.cs" Inherits="Master_MemberType" Title="ประเภทลูกค้า" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ประเภทลูกค้า
            </td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
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
                            รหัสประเภทลูกค้า</td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Text="" Width="140px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ประเภทลูกค้า</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Text="" Width="140px"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                      <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px"></td> 
                        <td>
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" />
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></td> 
                    </tr>
                     <tr height="25">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            </td>
                    </tr> 
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td colspan="2">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataKeyNames="LOID" DataSourceID="ItemDataSource" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDeleted="grvItem_RowDeleted"
                                OnRowUpdated="grvItem_RowUpdated" OnRowUpdating="grvItem_RowUpdating" ShowFooter="True"
                                Width="250px" OnRowDataBound="grvItem_RowDataBound" OnRowCommand="grvItem_RowCommand">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" AlternateText="แก้ไข" CausesValidation="False"
                                                CommandName="Edit" ImageUrl="~/Images/icn_edit.gif" />
                                            <asp:ImageButton ID="imbDelete" runat="server" AlternateText="ลบ" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                                        </ItemTemplate>
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
                                        <ItemStyle HorizontalAlign="Center"/>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ราคาขั้นต่ำ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLowerPrice" runat="server" Text='<%# Convert.ToDouble(Eval("LOWERPRICE")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLowerPrice" runat="server" CssClass="zTextboxR" MaxLength="10" Width="95px"
                                                Text='<%# Convert.ToDouble(Eval("LOWERPRICE")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLowerPriceNew" runat="server" CssClass="zTextboxR" MaxLength="10" Width="95px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ส่วนลด (%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Convert.ToDouble(Eval("DISCOUNT")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="zTextboxR" MaxLength="2" Width="95px"
                                                Text='<%# Convert.ToDouble(Eval("DISCOUNT")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDiscountNew" runat="server" CssClass="zTextboxR" MaxLength="2" Width="95px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DISCOUNT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LOWERPRICE">
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
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteDiscountStep"
                                OldValuesParameterFormatString="{0}" SelectMethod="GetDiscountStep"
                                TypeName="DiscountStep" UpdateMethod="UpdateDiscountStep">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="LOWERPRICE" Type="Decimal" />
                                    <asp:Parameter Name="DISCOUNT" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" DefaultValue="0" Name="memberType" PropertyName="Text"
                                        Type="Double" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataSourceID="ItemNewDataSource" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="250px" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"/>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ราคาขั้นต่ำ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLowerPriceNew" runat="server" CssClass="zTextboxR" MaxLength="10" Width="95px" Text=""></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ส่วนลด (%)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscountNew" runat="server" CssClass="zTextboxR" MaxLength="2" Width="95px" Text=""></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemNewDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDiscountStepItem" TypeName="DiscountStep"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table></td> 
        </tr>
    </table>
</asp:Content>