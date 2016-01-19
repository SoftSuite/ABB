<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ControlTransport.aspx.cs" Inherits="FG_Transaction_ControlTransport" Title="ใบคุมสินค้าส่ง" %>

<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบคุมสินค้าส่ง</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnSubmit="ส่งฝ่ายผลิต" NameBtnPrint="พิมพ์ใบคุมสินค้า"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnPrintClick="PrintClick" />
            <asp:LinkButton ID="btnPrintDetail" runat="server" CssClass="toolbarbutton" OnClick="btnPrintDetail_Click">พิมพ์ใบปะข้างกล่อง</asp:LinkButton>
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="390px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ทะเบียนรถ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCarNo" runat="server" CssClass="zTextbox" Width="208px"></asp:TextBox>
                                        <asp:Label ID="lblRemark" runat="server" Text="*" CssClass="zRemark"></asp:Label>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ชื่อคนขับ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDeliveryName" runat="server" CssClass="zTextbox" Width="208px"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Text="*" CssClass="zRemark"></asp:Label>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>

                                    <td colspan="3">
                                        &nbsp;<asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox></td>
                                </tr>
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" width="250px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 108px">
                                        เลขที่ใบคุม</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 108px">
                                        วันที่</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlDeliveryDate" runat="server" Enabled="False" /></td>
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
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="700px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel"
                                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
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
                                    
                                    <asp:TemplateField HeaderText="เลขที่ Invoice">
                                        <ItemTemplate>
                                            <asp:Label ID="txtInvCodeView" runat="server" Width="95px" Text='<%# Bind("INVCODE") %>'></asp:Label>
                                            <asp:TextBox ID="txtRequisitionView" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInvcode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("INVCODE") %>' OnTextChanged="txtInvcode_TextChanged"></asp:TextBox>
                                            <asp:TextBox ID="txtRequisition" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="imbSearch" runat="server" CausesValidation="True" CommandName="EditSearch"
                                                ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" />  
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInvcode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewInvcode_TextChanged1" ></asp:TextBox>
                                            <asp:TextBox ID="txtNewRequisition" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                           <asp:ImageButton ID="imbNewSearch" runat="server" CausesValidation="True" CommandName="Search"
                                                ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" />  
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สถานที่ส่ง">
                                        <ItemTemplate>
                                            <asp:Label ID="txtContactName" runat="server" Text='<%# Bind("CONTACTNAME") %>'></asp:Label><br>
                                            <asp:Label ID="txtCname" runat="server" Text='<%# Bind("CNAME") %>'></asp:Label><br>
                                            <asp:Label ID="txtAddress" runat="server" Text='<%# Bind("CADDRESS") %>'></asp:Label><br>
                                            <asp:Label ID="txtTel" runat="server" Text='<%# Bind("CTEL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="txtContactName" runat="server" Text='<%# Bind("CONTACTNAME") %>'></asp:Label><br>
                                            <asp:Label ID="txtCname" runat="server" Text='<%# Bind("CNAME") %>'></asp:Label><br>
                                            <asp:Label ID="txtAddress" runat="server" Text='<%# Bind("CADDRESS") %>'></asp:Label><br>
                                            <asp:Label ID="txtTel" runat="server" Text='<%# Bind("CTEL") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="txtNewContactName" runat="server" ></asp:Label><br>
                                            <asp:Label ID="txtNewCname" runat="server" ></asp:Label><br>
                                            <asp:Label ID="txtNewAddress" runat="server" ></asp:Label><br>
                                            <asp:Label ID="txtNewTel" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="จำนวนกล่อง">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBoxQty" runat="server" Width="95px" Text='<%# Convert.ToDouble(Eval("BOXQTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBoxQty" runat="server" CssClass="zTextboxR" Width="95px" Text='<%# Convert.ToDouble(Eval("BOXQTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBoxQty" runat="server" CssClass="zTextboxR" Width="95px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="REQUISITION">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>

                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteDeliveryItem" SelectMethod="GetDeliveryItem" 
                                TypeName="DeliveryItem" UpdateMethod="UpdateDeliveryItem" OldValuesParameterFormatString="{0}" >
                               <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="REQUISITION" Type="Decimal" />
                                    <asp:Parameter Name="BOXQTY" Type="Decimal" />
                                    <asp:Parameter Name="INVCODE" Type="String" />
                                    <asp:Parameter Name="CONTACTNAME" Type="String" />
                                    <asp:Parameter Name="CNAME" Type="String" />
                                    <asp:Parameter Name="CADDRESS" Type="String" />
                                    <asp:Parameter Name="CTEL" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLoid" Name="ctrlDelivery" PropertyName="Text" Type="Double" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="700px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
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
                                    
                                    <asp:TemplateField HeaderText="เลขที่ Invoice">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewInvcode" runat="server" AutoPostBack="true" Width="95px" MaxLength="20" OnTextChanged="txtNewInvcode_TextChanged"></asp:TextBox>
                                            <asp:TextBox ID="txtNewRequisition" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="imbNewSearch" runat="server" CausesValidation="True" CommandName="Search"
                                                ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" /> 
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สถานที่ส่ง">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNewContactName" runat="server"></asp:Label><br>
                                            <asp:Label ID="txtNewCname" runat="server"></asp:Label><br>
                                            <asp:Label ID="txtNewAddress" runat="server"></asp:Label><br>
                                            <asp:Label ID="txtNewTel" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBoxQty" runat="server" CssClass="zTextboxR" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetDeliveryItemBlank" TypeName="DeliveryItem">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                   
                </table>
            </td> 
            <td width="2px">
                &nbsp;
            </td> 
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted;">
                <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="5px" style="height: 25px"></td>
                        <td style="width: 80px; height: 25px;">
                            พนักงาน</td>
                        <td style="width: 105px; height: 25px;">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                    
                </table>
                <hr/>
                <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr>
                        <td width="5px" style="height: 25px">
                        </td>
                        <td style="width: 80px; height: 25px;">
                            จำนวน</td>
                        <td style="width: 105px; height: 25px;">
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>