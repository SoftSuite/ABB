<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinProductionSearch.aspx.cs" Inherits="Transaction_StockinProductionSearch" Title="ใบนำส่งผลิตภัณฑ์เข้าคลัง" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;<asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></td> 
                </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="ยืนยัน"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="600px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 161px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่ใบนำส่ง</td>
                        <td style="width: 161px">
                            <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px" align="center">
                            </td>
                        <td style="width: 170px"><asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="150px">
                        </asp:TextBox></td>
                        <td></td>
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่นำส่ง</td>
                        <td style="width: 161px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>                                      
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อสินค้า</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="331px"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่การผลิต</td>
                        <td style="width: 161px">
                            <asp:TextBox ID="txtLotNoFrom" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td width="20px">
                            &nbsp;ถึง</td>
                        <td style="width: 170px"><asp:TextBox ID="txtLotNoTo" runat="server" CssClass="zTextbox" Width="150px">
                        </asp:TextBox></td>
                        <td></td>
                    </tr>                     
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่ผลิต</td>
                        <td style="width: 161px">
                            <uc2:DatePickerControl ID="ctlProduceDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlProduceDateTo" runat="server" /></td>
                        <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>                     
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 161px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
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
                <asp:GridView ID="grvStockIn" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="600px" OnRowCommand="grvStockIn_RowCommand" OnRowDataBound="grvStockIn_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์"
                                    ImageUrl="~/Images/icn_print.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="30px"/>
                            <HeaderStyle Width="30px"/>
                            <FooterStyle Width="30px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="70px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="เลขที่ใบนำส่ง" SortExpression="">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkStockIn" Text="" runat="server"></asp:HyperLink> 
                            </ItemTemplate>
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "ลงวันที่" DataField="CREATEON" >
                            <ItemStyle Width="150px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="LOTNO" HeaderText = "เลขที่การผลิต" DataField="LOTNO">
                            <ItemStyle Width="150px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>    
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "สถานะ" DataField="STATUSNAME">
                            <ItemStyle Width="150px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>  
                        <asp:BoundField DataField="PRODUCETYPE">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>
