<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PurchaseRequestSearch.aspx.cs" Inherits="Transaction_PurchaseRequestSearch" Title="ใบบันทึกรายการเพื่อการจัดซื้อ/จัดจ้าง" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ใบบันทึกรายการเพื่อการจัดซื้อ/จัดจ้าง</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false" 
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" 
                    OnNewClick="NewClick" OnSubmitClick="SubmitClick" />
                <asp:LinkButton ID="btnVoid" runat="server" CssClass="toolbarbutton" OnClick="btnVoid_Click">LinkButton</asp:LinkButton>
                <asp:LinkButton ID="btnCancelPR" runat="server" CssClass="toolbarbutton" OnClick="btnCancelPR_Click" Visible="False">LinkButton</asp:LinkButton></td> 
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
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox id="txtDivision" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox></td>
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่</td>
                        <td width="150px">
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
                            ประเภท</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbPurchaseType" runat="server" CssClass="zComboBox" Width="150px">
                            </asp:DropDownList></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สินค้า</td>
                        <td width="150px">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะ PR</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusPRFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusPRTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะ PO</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusPOFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusPOTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะตรวจรับ</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
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
                <asp:GridView ID="grvPDRequest" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvPDRequest_RowCommand" OnRowDataBound="grvPDRequest_RowDataBound">
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
                                <asp:ImageButton ID="btnCopy" AlternateText="คัดลอก"
                                        runat="server" CausesValidation="False" CommandName="copy" ImageUrl="~/Images/icn_copy.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px"/>
                            <FooterStyle Width="50px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="PRLOID" DataNavigateUrlFormatString="PurchaseRequest.aspx?loid={0}"
                            DataTextField="PRCODE" HeaderText="เลขที่" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="REQUESTDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่" DataField="REQUESTDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "ชื่อสินค้า" DataField="PRODUCTNAME">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRQTY" HeaderText = "จำนวน PR" DataField="PRQTY" DataFormatString="{0:#,##0.00}" HtmlEncode="false">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="UNITNAME" HeaderText = "หน่วย" DataField="UNITNAME">
                            <ItemStyle Width="60px" HorizontalAlign="center"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="REQUESTBYNAME" HeaderText = "ผู้ขอซื้อ" DataField="REQUESTBYNAME">
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>                       
                        <asp:BoundField SortExpression="PURCHASETYPENAME" HeaderText = "ประเภท" DataField="PURCHASETYPENAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRSTATUSNAME" HeaderText = "สถานะ PR" DataField="PRSTATUSNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="POCODE" HeaderText = "เลขที่ PO" DataField="POCODE">
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="POQTY" HeaderText = "จำนวน PO" DataField="POQTY" DataFormatString="{0:#,##0.00}" HtmlEncode="false">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="POSTATUSNAME" HeaderText = "สถานะ PO" DataField="POSTATUSNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="SENDPODATE" HeaderText = "วันที่ส่งใบสั่งซื้อ" DataField="SENDPODATE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="STCODE" HeaderText = "เลขที่ตรวจรับ" DataField="STCODE">
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="STQTY" HeaderText = "จำนวนตรวจรับ" DataField="STQTY" DataFormatString="{0:#,##0.00}" HtmlEncode="false">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="LOTNO" HeaderText = "LOT NO" DataField="LOTNO">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STSTATUSNAME" HeaderText = "สถานะตรวจรับ" DataField="STSTATUSNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:BoundField DataField="PRILOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="PRSTATUS">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="POSTATUS">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="REDWA">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="REDAP">
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
