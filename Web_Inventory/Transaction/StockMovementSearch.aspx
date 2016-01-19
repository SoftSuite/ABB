<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockMovementSearch.aspx.cs" Inherits="Transaction_StockMovementSearch" Title="ประวัติการเคลื่อนไหวสินค้า" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ประวัติการเคลื่อนไหวสินค้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"
                    OnPrintClick="PrintClick" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1100" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา
                        </td>
                    </tr>                
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            วันที่เคลื่อนไหว</td>
                        <td style="width:170px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" /></td>
                        <td style="width:20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ประเภทสินค้า</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                     <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            กลุ่มสินค้า</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ชื่อสินค้า</td>
                        <td style="width:360px" colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="360px" /></td>
                        <td></td>                            
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            คลัง</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ย้ายจากโซน</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouseFrom" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ย้ายไปที่โซน</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouseTo" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>
                    <tr style="height: 10px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                        </td>
                        <td colspan="3" style="width: 360px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>
        <tr style="height:10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvStockMovementItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="1100px">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:BoundField SortExpression="NO" HeaderText = "ลำดับ" DataField="NO" >
                            <ItemStyle Width="50px" HorizontalAlign="center" />
                            <HeaderStyle Width="50px" Height="18px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "ชื่อสินค้า" DataField="PRODUCTNAME">
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่" DataField="CREATEON" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>    
                        <asp:BoundField SortExpression="FROMWAREHOUSENAME" HeaderText = "ย้ายจากคลัง" DataField="FROMWAREHOUSENAME" >
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="FROMZONENAME" HeaderText = "ย้ายจากโซน" DataField="FROMZONENAME" >
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TOWAREHOUSENAME" HeaderText = "ย้ายไปที่คลัง" DataField="TOWAREHOUSENAME" >
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TOZONENAME" HeaderText = "ย้ายไปที่โซน" DataField="TOZONENAME" >
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QTY" HeaderText = "จำนวน" DataField="QTY" DataFormatString="{0:#,##0.00}" HtmlEncode="False">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>
                         <asp:BoundField SortExpression="UNITNAME" HeaderText = "หน่วย" DataField="UNITNAME">
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>                        
                        <asp:BoundField SortExpression="TYPENAME" HeaderText = "ชื่อเอกสาร" DataField="TYPENAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="DOCCODE" HeaderText = "เลขที่อ้างอิง" DataField="DOCCODE">
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