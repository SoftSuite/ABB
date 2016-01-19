<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductReserve.aspx.cs" Inherits="Transaction_ProductReserve" Title="ใบคำร้องขอเบิกวัตถุดิบและบรรจุภัณฑ์" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบคำร้องขอเบิกวัตถุดิบและบรรจุภัณฑ์</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"  NameBtnSubmit="ส่งคลังวัตถุดิบ"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" ClientClickSubmit="return confirm('ต้องการส่งใบคำร้อง ใช่หรือไม่?');"/>
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
                                    <td style="width: 84px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px">
                                        เลขที่การผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox id="txtLotNo" runat="server" Width="145px"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="12px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="12px">0</asp:TextBox>
                                        <asp:TextBox id="txtWareHouse" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox>
                                        <asp:TextBox ID="txtVPLOID" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px">
                                        บาร์โค้ดสินค้า</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPDCode" runat="server" Width="104px" CssClass="zTextbox-View"></asp:TextBox>
                                 </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px">
                                        สินค้าที่ผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="211px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px">
                                        ปริมาณการผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtQty" runat="server" Width="80px" CssClass="zTextbox-View"></asp:TextBox>
                                        <asp:TextBox ID="txtQtyUnit" runat="server" Width="125px" CssClass="zTextbox-View"></asp:TextBox>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr> 
                                  <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px">
                                        จำนวนที่ผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSTDQty" runat="server" Width="80px" CssClass="zTextbox-View"></asp:TextBox>
                                        <asp:TextBox ID="txtPDQtyUnit" runat="server" Width="125px" CssClass="zTextbox-View"></asp:TextBox>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr> 
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 84px"></td>
                                    <td style="width: 135px"><asp:TextBox ID="txtBatchsizeUnit" runat="server" CssClass="zHidden" Width="12px">
                                    </asp:TextBox>
                                        <asp:TextBox ID="txtPacksize" runat="server" CssClass="zHidden" Width="12px">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtPacksizeunit" runat="server" CssClass="zHidden" Width="12px">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtPdpStdqty" runat="server" CssClass="zHidden" Width="12px">
                                        </asp:TextBox></td>
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
                                    <td></td>
                                    <td style="width: 95px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 95px">
                                        เลขที่ใบขอเบิก</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="125px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 95px">
                                        วันที่ขอเบิก</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="125px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr>
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 95px">
                                        </td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtDUEDate" runat="server" CssClass="zHidden" ReadOnly="True"
                                            Width="125px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr>
                              </table> 
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4" style="height: 5px">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" valign="top">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True" DataKeyNames="RANK"
                                Width="700px" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก" ImageUrl="~/Images/icn_save.gif" />
                                            <asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" OnClientClick="return confirm('ลบข้อมูลใช่หรือไม่?');"/>
                                        </ItemTemplate>
                                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="45px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รหัสวัตถุดิบ">
                                        <EditItemTemplate>
                                            <asp:Label ID="txtBarcode" runat="server" Text='<%# Bind("RWBARCODE") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("RWBARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                                        <EditItemTemplate>
                                             <asp:Label ID="txtProduct" runat="server" Text='<%# Bind("RWNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="txtProductView" runat="server" Text='<%# Bind("RWNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ประเภท">
                                        <EditItemTemplate>
                                            <asp:Label ID="txtType" runat="server" Text='<%# Bind("RWGROUPNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtTypeView" runat="server" Text='<%# Bind("RWGROUPNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ปริมาณ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="75px" Text='<%# Convert.ToDouble(Eval("MASTER")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Text='<%# Convert.ToDouble(Eval("MASTER")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="80px" HorizontalAlign="Right"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                           <asp:Label ID="txtUnit" runat="server" Text='<%# Bind("UNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="txtUnitView" runat="server" Text='<%# Bind("UNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="คงคลัง">
                                        <EditItemTemplate>
                                           <asp:Label ID="lblStockQty" runat="server" Text='<%# Convert.ToDouble(Eval("STOCKQTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblStockQtyView" runat="server" Text='<%# Convert.ToDouble(Eval("STOCKQTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="สถานะ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStatusitem" runat="server" Text='<%# Convert.ToDouble(Eval("STOCKQTY"))>= Convert.ToDouble(Eval("MASTER")) ? "ได้" : "ไม่ได้" %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblStatusitemView" runat="server" Text='<%# Convert.ToDouble(Eval("STOCKQTY"))>= Convert.ToDouble(Eval("MASTER")) ? "ได้" : "ไม่ได้" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="RWBARCODE">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="RWNAME">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="RWGROUPNAME">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="MASTER">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="UNAME">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="RANK">
                                        <ControlStyle CssClass="zHidden"></ControlStyle>
                                        <ItemStyle CssClass="zHidden"></ItemStyle>
                                        <HeaderStyle CssClass="zHidden"></HeaderStyle>
                                        <FooterStyle CssClass="zHidden"></FooterStyle>
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PDLOID">
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
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeletePDItem" SelectMethod="GetPDItem" 
                                TypeName="PDReserveItem" UpdateMethod="UpdatePDItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Type="Int32" Name="RANK"></asp:Parameter>
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="RWBARCODE"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="RWNAME"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="RWGROUPNAME"></asp:Parameter>
                                    <asp:Parameter Type="Double" Name="MASTER"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="UNAME"></asp:Parameter>
                                    <asp:Parameter Type="Int32" Name="RANK"></asp:Parameter>
                                    <asp:Parameter Type="Double" Name="PDLOID"></asp:Parameter>
                                    <asp:Parameter Type="Double" Name="UNIT"></asp:Parameter>
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter PropertyName="Text" Type="Double" Name="PDReserve" ControlID="txtLOID"></asp:ControlParameter>
                                    <asp:ControlParameter PropertyName="Text" Type="String" Name="LotNO" ControlID="txtLotNo"></asp:ControlParameter>
                                </SelectParameters>
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
                                        &nbsp;หมายเหตุ</td>
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
                            พนักงานขาย</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                 <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatus1" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px" Font-Italic="False"></asp:TextBox></td> 
                    </tr> 
                </table>
               </td> 
        </tr>
    </table>
</asp:Content>