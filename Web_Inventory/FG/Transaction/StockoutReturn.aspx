<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutReturn.aspx.cs" Inherits="FG_Transaction_StockoutReturn" Title="����觤׹�Թ���" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;����觤׹�Թ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit = "�����Ѵ����"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick = "SubmitClick"/>
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
                                        ���ʼ���˹���</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="80px" CssClass="zTextbox"></asp:TextBox><asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                </tr> 
                                
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px" valign="top">
                                        ���˵ء���觤׹</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtReason" runat="server" Height="50px" TextMode="MultiLine" Width="299px" CssClass="zTextbox" ></asp:TextBox>
                                        <asp:Label ID="lblRemark" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                                </tr> 
                                
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        �Ţ������</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        �ѹ�����</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlCreateDate" runat="server" Enabled="false" /></td>
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
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" DataSourceID="ItemDataSource" 
                                OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="���"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ź"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="�ѹ�֡"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="¡��ԡ"
                                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="������¡������"
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
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
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
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbLotView" runat="server" CssClass="zCombobox" Enabled="false" Width="145px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbLot" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbLot_SelectedIndexChanged"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewLot" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewLot_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Invoice �Ѻ���">
                                        <ItemTemplate>
                                            <asp:Label ID="txtInvNoView" runat="server" Width="45px" Text='<%# Bind("INVNO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInvNo" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("INVNO") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewInvNo" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" ></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ӹǹ���׹">
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="˹���">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbUnitView" runat="server" CssClass="zCombobox" Enabled="false" Width="95px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zCombobox" Enabled="false" Width="95px"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox" Enabled="false" Width="95px"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�Ҥ�">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPriceView" runat="server" Width="45px" Text='<%# Bind("PRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="����Թ">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNetPriceView" runat="server" Width="45px" Text='<%# Bind("NETPRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="NETPRICE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                     
                                    <asp:BoundField DataField="ISVAT">
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
                                     
                                    <asp:BoundField DataField="DISCOUNT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                     
                                    <asp:BoundField DataField="PRICE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                   
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteStockOutItem" SelectMethod="GetStockOutItem" 
                                TypeName="StockOutReturnItem" UpdateMethod="UpdateStockOutItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal"/>
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="DISCOUNT" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" /> 
                                    <asp:Parameter Name="ISVAT" Type="String" />  
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                    <asp:Parameter Name="INVNO" Type="String" />  
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="stockout" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtStatus" Name="status" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="������¡������"
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
                                    
                                    <asp:TemplateField HeaderText="Lot No">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewLot" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewLot_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Invoice �Ѻ���" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewInvNo" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�ӹǹ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="˹���" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox" Enabled="false" Width="95px"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="�Ҥ�" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="����Թ" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetStockOutItemBlank" TypeName="StockOutReturnItem" DeleteMethod="DeleteStockOutItem" UpdateMethod="UpdateStockOutItem">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="DISCOUNT" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="ISVAT" Type="String" />
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                </UpdateParameters>
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
                            ��ѡ�ҹ</td>
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
                <hr/> 
                <table border="0" cellpadding="0" cellspacing="0" width="190px">



                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2" style="width: 62px">
                            &nbsp;���</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                   
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>


