<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinReturnSearch.aspx.cs" Inherits="FG_Transaction_StockinReturnSearch" Title="ใบรับคืนสินค้า"%>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ใบรับคืนสินค้า</td> 
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
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ประเภทรับคืน</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbDocType" runat="server" CssClass="zComboBox" Width="150px">
                            </asp:DropDownList></td>
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่ใบรับคืน</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="50px"></td>
                        <td style="width: 170px"></td>
                        <td></td>                            
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่รับคืน</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="50px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>                   
                    <tr height="25px" id="trCustomer" runat="server">
                        <td width="50px"></td>
                        <td width="150px">
                            ลูกค้า</td>
                        <td colspan="3" width="340px">
                            <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zComboBox" Width="325px">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อ</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCName" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="50px">
                            นามสกุล</td>
                        <td style="width: 170px"><asp:TextBox ID="txtCLName" runat="server" CssClass="zTextbox" Width="150px">
                        </asp:TextBox></td>
                        <td></td>                            
                    </tr>  
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะ</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px" />
                        </td>
                        <td width="50px" align="center">
                            ถึง</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="50px"></td>
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
                <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound">
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
                            <HeaderStyle Width="50px" />
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
                        <asp:BoundField SortExpression="DOCNAME" HeaderText = "ประเภทรับคืน" DataField="DOCNAME">
                            <ItemStyle Width="120px" HorizontalAlign="center"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField> 
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="StockinReturn.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="เลขที่ใบรับคืน" DataTextFormatString="{0}" >
                            <ItemStyle Width="130px" HorizontalAlign="center"/>
                            <HeaderStyle Width="130px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RECEIVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่รับคืน" DataField="RECEIVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>                   
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="GRANDTOT" HeaderText = "ยอดรวม" DataField="GRANDTOT" HtmlEncode="false" DataFormatString="{0:#,##0.00}">
                            <ItemStyle Width="80px" HorizontalAlign="right"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEBY" HeaderText = "พนักงานขาย" DataField="CREATEBY">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "สถานะ" DataField="STATUSNAME">
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>    
</asp:Content>