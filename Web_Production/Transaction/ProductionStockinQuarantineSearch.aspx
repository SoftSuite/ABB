<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductionStockinQuarantineSearch.aspx.cs" Inherits="Transaction_ProductionStockinQuarantineSearch" Title="บันทึกการรับผลิตภัณฑ์เข้าคลังกักกัน" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;บันทึกการรับผลิตภัณฑ์เข้าคลังกักกัน</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"  />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="300px"></td> 
                        <td style="width: 50px"></td>
                        <td></td>
                    </tr>
                   
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่เข้าคลังสำเร็จรูป</td>
                        <td width="300px">
                            <uc2:DatePickerControl ID="ctlMFGDate" runat="server" />
                        </td>
                        <td style="width: 50px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อผลิตภัณฑ์</td>
                        <td width="300px">
                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="321px">
                            </asp:DropDownList></td>
                        <td style="width: 50px"></td>
                        <td></td>                            
                    </tr>                      

                       
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่การผลิต</td>
                         <td width="300px">
                             <asp:TextBox ID="txtLotNo" runat="server" Width="101px"></asp:TextBox></td>
                            <td style="width: 50px">
                                &nbsp;</td>
                        <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>                     

                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="300px"></td>
                        <td style="width: 50px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPDOrder" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvPDOrder_RowCommand" OnRowDataBound="grvPDOrder_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์"
                                    ImageUrl="~/Images/icn_print.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="30px"/>
                            <HeaderStyle Width="30px"/>
                            <FooterStyle Width="30px"/>
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

                        <asp:BoundField SortExpression="MFGDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่" DataField="MFGDATE" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>                       
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "ชื่อผลิตภัณฑ์" DataField="PRODUCTNAME">
                            <ItemStyle Width="200px"/>
                            <HeaderStyle Width="200px" />  
                        </asp:BoundField>  
                        
                        <asp:TemplateField HeaderText="เลขที่การผลิต" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                            <ItemTemplate>
                                 <asp:HyperLink ID="hplLotNo" runat="server" Text='<%# Bind("LOTNO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField SortExpression="QUARANTINEQTY" HeaderText = "จำนวน" DataField="QUARANTINEQTY">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="UNITNAME" HeaderText = "หน่วย" DataField="UNITNAME">
                            <ItemStyle Width="80px"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="QUARANTINEREMARK" HeaderText = "หมายเหตุ" DataField="QUARANTINEREMARK">
                            <ItemStyle Width="200px" />
                            <HeaderStyle Width="200px" />  
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>


