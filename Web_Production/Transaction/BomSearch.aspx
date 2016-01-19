<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="BomSearch.aspx.cs" Inherits="Transaction_BomSearch" Title="BOM (Bill of Material)" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                BOM (Bill of Material)</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" />
                
                
            </td> 
        </tr>
        <tr style="height: 25px">
            <td  style =" height: 10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">

        <tr>
            <td style =" width:50px; height:5px"> </td>
            <td style =" width:100px; height:5px"></td>
            <td style =" width:210px; height:5px"></td>
            <td style =" width:440px; height:5px"></td>
        </tr>
        
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:100px; height:24px">ประเภทสินค้า</td>
            <td style="width:210px; height:24px">
                <asp:DropDownList ID="cmbProductType" runat="server" Width="300px" CssClass="zComboBox" >
                </asp:DropDownList></td>
            <td style="height:24px; width: 440px;">
                <asp:TextBox ID="txtProduct" runat="server" Width="41px" Enabled ="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td style =" width:50px; height:5px"> </td>
            <td style =" width:100px; height:5px"></td>
            <td style =" width:210px; height:5px"></td>
            <td style =" width:440px; height:5px"></td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:100px; height:24px">กลุ่มสินค้า</td>
            <td style="width:210px; height:24px">
                <asp:DropDownList ID="cmbProductGroup" runat="server" Width="300px" CssClass="zComboBox" >
                </asp:DropDownList></td>
            <td style="height:24px; width: 440px;"></td>
        </tr>
        
        <tr>
            <td style =" width:50px; height:5px"> </td>
            <td style =" width:100px; height:5px"></td>
            <td style =" width:210px; height:5px"></td>
            <td style =" width:440px; height:5px"></td>
        </tr>
        
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:100px; height:24px">ชื่อสินค้า</td>
            <td style="width:210px; height:24px">
                <asp:TextBox ID="txtProductName" runat="server" Width="300px" CssClass="zTextbox"></asp:TextBox></td>
            <td style="height:24px; width: 440px;">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
            </td>
        </tr>
        
        <tr>
            <td style =" width:50px; height:5px"> </td>
            <td style =" width:100px; height:5px"></td>
            <td style =" width:210px; height:5px"></td>
            <td style =" width:440px; height:5px"></td>
        </tr>
        
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td style="height:10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
      EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="800px" >
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
                            <asp:BoundField DataField="LOID" HeaderText="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDERNO" HeaderText="ลำดับที่" >
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="รหัสสินค้า">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplBarCode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BARCODE" HeaderText="BARCODE">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTNAME" HeaderText="ชื่อสินค้า">
                                <ItemStyle Width="300px" />
                                <HeaderStyle Width="300px" Height="25px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UNAME" HeaderText="หน่วยนับ"  >
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="PGNAME" HeaderText="กลุ่มสินค้า"  >
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="PTNAME" HeaderText="ประเภทสินค้า"  >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="LOTSIZE" HeaderText="จำนวนต่อ Lot"  >
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
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

