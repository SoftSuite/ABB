<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductOrder.aspx.cs" Inherits="Transaction_ProductOrder"  Title="�ѹ�֡��觼�Ե�Թ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;�ѹ�֡��觼�Ե�Թ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="�觽��¼�Ե"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" />
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
                                        ���ʼ���Ե</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="80px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>

                                    <td colspan="3">
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWarehouse" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox></td>
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
                                        �Ţ���ѹ�֡��觼�Ե</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 108px">
                                        �ѹ���ѹ�֡��觼�Ե</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlReqDate" runat="server" Enabled="False" /></td>
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
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" DataSourceID="ItemDataSource" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
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
                                    
                                    <asp:TemplateField HeaderText="�ӴѺ���" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="������">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px"  MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px"  MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�Թ���">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false" Width="145px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ӹǹ">
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="˹���">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbUnitView" runat="server" CssClass="zCombobox" Enabled="false" Width="95px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zCombobox"  Enabled="false" Width="95px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox"  Enabled="false" Width="95px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ѹ����ͧ���">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDateView" runat="server" Width="95px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DUEDATE")).ToString(ABB.Data.Constz.DateFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <uc2:DatePickerControl ID="txtDate" runat="server" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <uc2:DatePickerControl ID="txtNewDate" runat="server" />
                                         
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    


                                    <asp:BoundField DataField="LOID">
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
                                     
                                    <asp:BoundField DataField="QTY">
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
                                     
                                    <asp:BoundField DataField="DUEDATE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                     
                                   
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteRequisitionItem" SelectMethod="GetRequisitionItem" 
                                TypeName="RequisitionItemOrder" UpdateMethod="UpdateRequisitionItem" OldValuesParameterFormatString="{0}" >
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="DUEDATE" Type="DateTime" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="requisition" PropertyName="Text"
                                        Type="Double" />
                                    <asp:ControlParameter ControlID="txtStatus" Name="status" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
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
                                    
                                    <asp:TemplateField HeaderText="�ӴѺ���" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="������">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�Թ���">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ӹǹ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="˹���" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox"  Enabled="false" Width="95px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ѹ����ͧ���" InsertVisible="False">
                                        <ItemTemplate>
                                            <uc2:DatePickerControl ID="txtNewDate" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetRequisitionItemBlank" TypeName="RequisitionItemOrder">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" width="600">
                                <tr>
                                    <td valign="top" width="70px">
                                        &nbsp;�����˵�</td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="530px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
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
                        <td width="5px"></td>
                        <td style="width: 80px">
                            ������¡��</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            ʶҹ�</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
               </td>
               </tr>
               </table>       
</asp:Content>


