<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialCtrl.ascx.cs" Inherits="Transaction_Production_MaterialCtrl" %>
<table border="0" width="1000" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
             EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="800px" OnRowCancelingEdit="gvResult_RowCancelingEdit" OnRowDataBound="gvResult_RowDataBound" OnRowDeleting="gvResult_RowDeleting" OnRowEditing="gvResult_RowEditing" OnRowUpdating="gvResult_RowUpdating"  >
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
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ประเภท"> 
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <HeaderStyle Width="60px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPgName" runat="server" Text='<%# Bind("PGNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="รหัสวัตถุดิบ"> 
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBarCode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ชื่อวัตถุดิบ">
                        <ItemTemplate>
                            <asp:Label ID="lblPDName" runat="server" Text='<%# Bind("MTRNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="หน่วย">
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <HeaderStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblUname" runat="server" Text='<%# Bind("UNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="MASTER">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaster2" runat="server" Text='<%# Bind("MASTER") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Lot No.">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("MLOTNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="จำนวนที่เบิก">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaster" runat="server" Text='<%# Bind("USEQTY2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="ปริมาณการใช้">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUseQty" runat="server" Text='<%# Bind("USEQTY") %>' CssClass="zTextbox" Width="98px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblUseQty" runat="server" Text='<%# Bind("USEQTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="MTRLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="MTRLOID" runat="server" Text='<%# Bind("MTRLOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="PDPLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="PDPLOID" runat="server" Text='<%# Bind("PDPLOID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="PGLOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                        <ItemTemplate>
                            <asp:Label ID="PGLOID" runat="server" Text='<%# Bind("PGLOID") %>'></asp:Label>
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
    <tr style ="height:20px">
        <td>
        </td>
    </tr> 
    <tr>
        <td>
             <table border="0" width="800px" cellpadding="0" cellspacing="0">
                <tr>
                    <td style=" vertical-align :top; width :50px">
                        <asp:Label  ID="lblRemark" runat ="server">วิธีเตรียม</asp:Label>
                    </td>
                    <td width="750px" align="right"> 
                        <asp:TextBox ID="txtProcess" runat ="server" Width = "700px" TextMode="MultiLine" Height="85px" Enabled="False" CssClass="zTextBox" ></asp:TextBox>
                    
                    </td>
                </tr>
            </table>
        </td> 
    </tr> 
</table>




                    