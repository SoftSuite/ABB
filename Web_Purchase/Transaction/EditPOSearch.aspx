<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="EditPOSearch.aspx.cs" Inherits="Transaction_EditPOSearch" Title="บันทึกขอแก้ไขใบสั่งซื้อ" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;บันทึกขอแก้ไขใบสั่งซื้อ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="อนุมัติแก้ไข"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td>
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr style="height:10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่บันทึกข้อความ</td>
                        <td width="150px">
                            <asp:TextBox ID="txtPECode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>                            
                    </tr>    
                    <tr style="height:25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่บันทึกข้อความ</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่ใบสั่งซื้อ</td>
                        <td width="150px">
                            <asp:TextBox ID="txtPOCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>                            
                    </tr>                      
                    <tr style="height:25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่สั่งซื้อ</td>
                        <td width="150px"><uc2:DatePickerControl ID="ctlPODateFrom" runat="server" /></td>
                        <td width="20px">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlPODateTo" runat="server" /></td>
                        <td></td>
                    </tr>
                       
                    <tr style="height:25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ผู้จำหน่าย</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zComboBox" Width="321px">
                            </asp:DropDownList></td>
                       
                        <td></td>
                    </tr>                     
                    <tr style="height:25px">
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
                    <tr style="height:10px">
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
        <tr style="height:10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPDOrder" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvPDOrder_RowCommand" OnRowDataBound="grvPDOrder_RowDataBound">
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
                        <asp:BoundField DataField="PELOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="POOLD">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PONEW">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="PELOID" DataNavigateUrlFormatString="EditPO.aspx?loid={0}"
                            DataTextField="PECODE" HeaderText="เลขที่" DataTextFormatString="{0}" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="POEDITDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่" DataField="POEDITDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>                       
                        <asp:BoundField SortExpression="SUPPLIERNAME" HeaderText = "ผู้จำหน่าย" DataField="SUPPLIERNAME">
                            <ItemStyle Width="160px"/>
                            <HeaderStyle Width="160px" />  
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="PELOID" DataNavigateUrlFormatString="EditPO.aspx?loid={0}"
                            DataTextField="POCODE" HeaderText="เลขที่ใบสั่งซื้อ" DataTextFormatString="{0}" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="REASON" HeaderText = "เหตุผลการแก้ไข" DataField="REASON">
                            <ItemStyle Width="180px" />
                            <HeaderStyle Width="180px" />  
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