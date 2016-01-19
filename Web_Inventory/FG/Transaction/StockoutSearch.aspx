<%@ Page Language="C#"   MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutSearch.aspx.cs" Inherits="FG_Transaction_StockoutSearch" Title="ใบเบิกสินค้าออก" %>

<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ใบเบิกสินค้าออก</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="อนุมัติ"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px"  class="searchTable">
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
                            ประเภทขอเบิก</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbDocType" runat="server" CssClass="zComboBox" Width="180px">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่ขอเบิก</td>
                           <td colspan="3">
                            <asp:TextBox ID="txtReqCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่ขอเบิก</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlRequestDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlRequestDateTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ผู้ขอเบิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCreateby" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                      <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ลูกค้า</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zComboBox"  Width="322px"></asp:DropDownList></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่เบิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtStockCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่เบิก</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlApproveDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlApproveDateTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะ</td>
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
                <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvRequisition_RowCommand" OnRowDataBound="grvRequisition_RowDataBound">
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
                            <ItemStyle Width="30px" HorizontalAlign="center"/>
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
                        <asp:BoundField SortExpression="DOCTYPE" HeaderText = "ประเภทขอเบิก" DataField="DOCTYPE">
                            <ItemStyle Width="130px"/>
                            <HeaderStyle Width="130px" />  
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Stockout.aspx?loid={0}"
                            DataTextField="REQCODE" HeaderText="เลขที่ขอเบิก" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RESERVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ขอเบิก" DataField="RESERVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Stockout.aspx?loid={0}"
                            DataTextField="STOCKCODE" HeaderText="เลขที่ใบเบิก" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่เบิก" DataField="CREATEON" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  

                        <asp:BoundField SortExpression="CREATEBY" HeaderText = "ผู้ขอเบิก" DataField="CREATEBY">
                            <ItemStyle Width="80px"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "สถานะ" DataField="STATUSNAME">
                            <ItemStyle Width="100px"/>
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
