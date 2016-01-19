<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ReturnRequest.aspx.cs" Inherits="Transaction_ReturnRequest" Title="ใบแจ้งคืนวัตถุดิบและบรรจุภัณฑ์" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบแจ้งคืนวัตถุดิบและบรรจุภัณฑ์</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true"  NameBtnSubmit="อนุมัติ"
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
                                        เลขที่การผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox id="txtLotNo" runat="server" Width="145px"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />&nbsp;
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="12px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox>
                                        <asp:TextBox id="txtWareHouse" runat="server" CssClass="zHidden" Width="12px"></asp:TextBox>
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="22px"></asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        สินค้าที่ผลิต</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPDCode" runat="server" Width="104px" CssClass="zTextbox-View"></asp:TextBox>
                                        <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        </td>
                                    <td colspan="3">
                                        &nbsp;</td>
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
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr height="5px">
                                    <td></td>
                                    <td style="width: 86px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 86px">
                                        เลขที่แจ้งคืน</td>
                                    <td style="width: 132px">
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="125px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td></td>
                                    <td style="width: 86px">
                                        วันที่แจ้งคืน</td>
                                    <td style="width: 132px">
                                        <uc2:DatePickerControl ID="ctlReqDate" runat="server" Enabled="false" />
                                    </td>
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
                        <td colspan="4" style="height: 178px" valign="top">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True" DataKeyNames="RANK"
                                Width="600px" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                    ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>    
                                </EditItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                    ImageUrl="~/Images/icn_edit.gif"/>
                                    <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" OnClientClick="return confirm('ลบข้อมูลใช่หรือไม่?');"/>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                                <EditItemTemplate>
                                     <asp:Label ID="txtProduct" runat="server" Width="95px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                </EditItemTemplate>
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="txtProductView" runat="server" Width="95px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="จำนวนที่เบิก">
                                <EditItemTemplate>
                                     <asp:Label ID="txtMaster" runat="server"  Width="55px" Text='<%# Bind("MASTER") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="txtMasterView" runat="server" Width="45px" Text='<%# Bind("MASTER") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="จำนวนที่ใช้">
                                <EditItemTemplate>
                                     <asp:Label ID="txtUseQty" runat="server" Width="55px" Text='<%# Bind("USEQTY") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="txtUseQtyView" runat="server" Width="45px" Text='<%# Bind("USEQTY") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="จำนวนที่เสีย">
                                <EditItemTemplate>
                                     <asp:Label ID="txtWasteQty" runat="server" Width="55px" Text='<%# Bind("WASTEQTYMAT") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="txtWasteQtyView" runat="server" Width="45px" Text='<%# Bind("WASTEQTYMAT") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="จำนวนที่คืน">
                                <EditItemTemplate>
                                     <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="55px" Text='<%# Bind("RETURNQTY") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemTemplate>
                                     <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("RETURNQTY") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                               

                                <asp:BoundField DataField="LOID">
                                <ControlStyle CssClass="zHidden"></ControlStyle>

                                <ItemStyle CssClass="zHidden"></ItemStyle>

                                <HeaderStyle CssClass="zHidden"></HeaderStyle>

                                <FooterStyle CssClass="zHidden"></FooterStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="PRODUCT">
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
                                TypeName="ReturnRequestItem"  UpdateMethod="UpdatePDItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Type="Double" Name="RANK"></asp:Parameter>
                               </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Double"/>
                                    <asp:Parameter Name="PRODUCT" Type="Double" />
                                    <asp:Parameter Name="UNIT" Type="Double" />
                                    <asp:Parameter Name="MASTER" Type="Double" />
                                    <asp:Parameter Name="PRODUCTNAME" Type="String" />
                                    <asp:Parameter Name="USEQTY" Type="Double" />
                                    <asp:Parameter Name="WASTEQTYMAT" Type="Double" />
                                    <asp:Parameter Name="RETURNQTY" Type="Double" />
                                    <asp:Parameter Name="RANK" Type="Double" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLoid" Name="requisition" PropertyName="Text" Type="Double" />
                                    <asp:ControlParameter ControlID="txtRefLoid" Name="pdploid" PropertyName="Text" Type="Double" />
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
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
               </td> 
        </tr>
    </table>
</asp:Content>