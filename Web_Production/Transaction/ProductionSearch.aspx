<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductionSearch.aspx.cs" Inherits="Transaction_ProductionSearch" Title="บันทึกการผลิตผลิตภัณฑ์จากสมุนไพร" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;บันทึกการผลิตผลิตภัณฑ์จากสมุนไพร</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"  OnNewClick ="NewClick" OnDeleteClick="DeleteClick"  />
                
                
            </td> 
        </tr>
        <tr>
            <td  style =" height: 10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="900"  class="searchTable">

        <tr>
            <td colspan="4" class="subheadertext"> &nbsp;ค้นหา</td>
        </tr>
        
        <tr>
            <td style =" width:50px; height:10px"> </td>
            <td style =" width:100px; height:10px"></td>
            <td style=" width:350px; height:10px"></td>
            <td style =" width:540px; height:10px"></td>
        </tr>
        
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:150px; height:25px">เลขที่การผลิต</td>
            <td style="width:350px; height:25px">
                <asp:TextBox  ID="txtLotNo" runat="server" Width="150px" CssClass="zTextbox">
                </asp:TextBox></td>
            <td style="height:25px; width: 540px;">
                <asp:TextBox ID="txtProduct" runat="server" Width="41px" Enabled ="false" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:150px; height:25px">
                วันที่ผลิต</td>
            <td style="width:350px; height:25px">
                <uc2:DatePickerControl ID="PkDateFrom" runat="server" />
                &nbsp; ถึง&nbsp;
                <uc2:DatePickerControl ID="PkDateTo" runat="server" />
            </td>
            <td style="height:25px; width: 540px;"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:150px; height:25px">
                ชื่อผลิตภัณฑ์</td>
            <td style="width:350px; height:25px">
                <asp:TextBox ID="txtProductName" runat="server" Width="325px" CssClass="zTextbox"></asp:TextBox></td>
            <td style="height:25px; width: 540px;">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
            </td>
        </tr>
        
        <tr>
            <td style =" width:50px; height:10px"> </td>
            <td style =" width:150px; height:10px"></td>
            <td style=" width:350px; height:10px"></td>
            <td style =" width:440px; height:10px"></td>
        </tr>
        
    </table>
            </td>
        </tr>
        <tr>
            <td style="height:10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" 
                      EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="900px"  OnRowDataBound="gvResult_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItem" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์บันทึกการผลิต"
                                                ImageUrl="~/Images/icn_print.gif"/>
                                                <asp:ImageButton ID="btnPrintL" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์บันทึกการผลิตอื่นๆ"
                                                ImageUrl="~/Images/icn_print.gif"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px"/>
                                            <HeaderStyle Width="60px"/>
                                            <FooterStyle Width="60px"/>
                                            </asp:TemplateField>
 
                                            <asp:BoundField DataField="PDPLOID" HeaderText="PDPLOID"  >
                                            <ControlStyle CssClass="zHidden" />
                                                <ItemStyle CssClass="zHidden" />
                                                <HeaderStyle CssClass="zHidden" />
                                                <FooterStyle CssClass="zHidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PDLOID" HeaderText="PDLOID"  >
                                            <ControlStyle CssClass="zHidden" />
                                                <ItemStyle CssClass="zHidden" />
                                                <HeaderStyle CssClass="zHidden" />
                                                <FooterStyle CssClass="zHidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ORDERNO" HeaderText="ลำดับที่" >
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="เลขที่การผลิต" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hplLotNo" runat="server" Text='<%# Bind("LOTNO") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MFGDATE" HeaderText="วันที่ผลิต" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}"  >
                                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="80px"/>
                                            </asp:BoundField>    

                                            <asp:BoundField DataField="PDNAME" HeaderText="ชื่อผลิตภัณฑ์"  >
                                            </asp:BoundField>                           
                                            <asp:BoundField DataField="BATCHSIZE" HeaderText="ปริมาณการผลิต"  HtmlEncode="false" DataFormatString="{0:#,##0}">
                                                <ItemStyle Width="100px" HorizontalAlign="right" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="STDQTY" HeaderText="จำนวนตามทฤษฎี"  HtmlEncode="false" DataFormatString="{0:#,##0}">
                                                <ItemStyle Width="120px" HorizontalAlign="right" />
                                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="PDQTY" HeaderText="จำนวนที่ผลิตได้"  HtmlEncode="false" DataFormatString="{0:#,##0}">
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="UNAME" HeaderText="หน่วยนับ"  >
                                                <ItemStyle Width="80px"/>
                                                <HeaderStyle Width="80px"/>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="PRODSTATUSNAME" HeaderText="สถานะ"  >
                                                <ItemStyle Width="100px" HorizontalAlign="center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                                                 
                                            <asp:BoundField DataField="PRODSTATUS" HeaderText="PRODSTATUS"  >
                                                <ControlStyle CssClass="zHidden" />
                                                <ItemStyle CssClass="zHidden" />
                                                <HeaderStyle CssClass="zHidden" />
                                                <FooterStyle CssClass="zHidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RANK" >
                                            <ControlStyle CssClass="zHidden" />
                                                <ItemStyle CssClass="zHidden" />
                                                <HeaderStyle CssClass="zHidden" />
                                                <FooterStyle CssClass="zHidden" />
                                            </asp:BoundField>                           
                                            
                                            
                                        </Columns>
                                        <HeaderStyle CssClass="t_headtext" />
                                        <AlternatingRowStyle CssClass="t_alt_bg" />
                                        <PagerSettings Visible="False" />
                    </asp:GridView> 
            </td>
        </tr>
    </table>
</asp:Content>

