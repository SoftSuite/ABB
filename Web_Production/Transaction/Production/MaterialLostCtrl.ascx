<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialLostCtrl.ascx.cs" Inherits="Transaction_Production_MaterialLostCtrl" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<table border="0" width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" />
        </td>
    </tr>
    <tr style="height: 25px">
        <td>
            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
             EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="1000px" OnRowCancelingEdit="gvResult_RowCancelingEdit" OnRowDataBound="gvResult_RowDataBound" OnRowDeleting="gvResult_RowDeleting" OnRowEditing="gvResult_RowEditing" OnRowUpdating="gvResult_RowUpdating"  >
                <Columns>
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit"
                                ImageUrl="~/Images/icn_edit.gif" />
                            <asp:ImageButton ID="imbDelete"
                                runat="server" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imbSave" runat="server"  CommandName="Update"
                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel"
                                    runat="server" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ลำดับ">
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                        <ItemTemplate>
                            <asp:Label ID="lblPdName" runat="server" Text='<%# Bind("MTRNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="จำนวนตามทฤษฎี"> 
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBoMaster" runat="server" Text='<%# Bind("BMASTER") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="จำนวนที่เบิกคลัง">
                        <ItemStyle Width="70px" HorizontalAlign ="right" />
                        <HeaderStyle Height="25px" Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblRqmMaster" runat="server" Text='<%# Bind("USEQTY2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="จำนวนสูญเสียจากวัตถุดิบ">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWasteQtyMat" runat="server" Text='<%# Bind("WASTEQTYMAT") %>' CssClass="zTextbox"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblWasteQtyMat" runat="server" Text='<%# Bind("WASTEQTYMAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="จำนวนสูญเสียจากผู้ปฏิบัติงาน">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWasteQtyMan" runat="server" Text='<%# Bind("WASTEQTYMAN") %>' CssClass="zTextbox"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblWasteQtyMan" runat="server" Text='<%# Bind("WASTEQTYMAN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    
                    <asp:TemplateField HeaderText="จำนวนที่ใช้ผลิตจริง">
                        <ItemStyle Width="70px" HorizontalAlign ="right" />
                        <HeaderStyle  Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblUseQty" runat="server" Text='<%# Bind("USEQTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="จำนวนคืนคลัง">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtReturnQty" runat="server" Text='<%# Bind("RETURNQTY") %>' CssClass="zTextbox"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblReturnQty" runat="server" Text='<%# Bind("RETURNQTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                    <asp:TemplateField HeaderText="จำนวนส่งเปลี่ยน">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtChangeQty" runat="server" Text='<%# Bind("CHANGEQTY") %>' CssClass="zTextbox"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblChangeQty" runat="server" Text='<%# Bind("CHANGEQTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                    <asp:TemplateField HeaderText="% ความสูญเสียวัตถุดิบ">
                        <ItemStyle Width="70px" HorizontalAlign ="right" />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblYieldMat" runat="server" Text='<%# Bind("YIELDMAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="% ความสูญเสียจากผู้ปฏิบัติงาน">
                        <ItemStyle Width="70px" HorizontalAlign ="right"  />
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <asp:Label ID="lblYieldMan" runat="server" Text='<%# Bind("YIELDMAM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="หมายเหตุ">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("REMARK") %>' CssClass="zTextbox"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="left" Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("REMARK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="BLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="BLOID" runat="server" Text='<%# Bind("BLOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="PDPLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="PDPLOID" runat="server" Text='<%# Bind("PDPLOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="MTRLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="MTRLOID" runat="server" Text='<%# Bind("MTRLOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ULOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="ULOID" runat="server" Text='<%# Bind("ULOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRODSTATUS" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="PRODSTATUS" runat="server" Text='<%# Bind("PRODSTATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="POSTATUS" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="POSTATUS" runat="server" Text='<%# Bind("POSTATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>           
                                                             
                </Columns>                      
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
                <PagerSettings Visible="False" />
            </asp:GridView> 
        </td>
    </tr>   
</table>
<table border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse" >
            <tr >
                <td style="width: 50px; height: 24px;"></td>
                <td style="Width: 50px; height: 24px;"></td>
                <td style="width: 200px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 496px; height: 24px;" align ="right" >% ความสุญเสียรวมเฉลี่ย</td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;text-align :right;">
                <asp:Label ID ="lblSumYieldman" runat ="server" Text='<%# Bind("SUMYIELDMAT") %>'> </asp:Label></td>
                <td style="width: 100px; height: 24px; text-align :right;">
                <asp:Label ID ="lblSumYieldmat" runat ="server" Text='<%# Bind("SUMYIELDMAN") %>'> </asp:Label></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
                <td style="width: 100px; height: 24px;"></td>
            </tr>
</table>

                    