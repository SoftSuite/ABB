<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="BasketSearch.aspx.cs" Inherits="FG_Master_BasketSearch" Title="ค้นหาข้อมูลกระเช้า" %>

<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ค้นหาข้อมูลกระเช้า</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
                
            </td> 
        </tr>
        <tr style="height: 25px">
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">
        <tr>
            <td colspan="4" class="subheadertext">&nbsp;ค้นหา</td>
        </tr>
        <tr>
            <td height="10" style="width: 50px">
            </td>
            <td height="10" style="width: 100px">
            </td>
            <td height="10" style="width: 210px">
            </td>
            <td height="10" style="width: 440px">
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:100px; height:24px">รหัสกระเช้า</td>
            <td style="width:210px; height:24px">
                <asp:TextBox ID="txtBarcode" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox></td>
            <td style="height:24px; width: 440px;"></td>
        </tr>
        <tr>
            <td style="width:50px; height:24px"></td>
            <td style="width:100px; height:24px">ชื่อกระเช้า</td>
            <td style="width:210px; height:24px">
                <asp:TextBox ID="txtBasketName" runat="server" Width="200px" CssClass="zTextbox"></asp:TextBox></td>
            <td style="height:24px; width: 440px;">
                <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="imbSearch_Click"/>
            </td>
        </tr>
        <tr>
            <td height="10" style="width: 50px">
            </td>
            <td height="10" style="width: 100px">
            </td>
            <td height="10" style="width: 210px">
            </td>
            <td height="10" style="width: 440px">
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
      EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="gvResult_RowDataBound" Width="800px" >
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
                            <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Basket.aspx?loid={0}"
                            DataTextField="BARCODE" HeaderText="รหัสกระเช้า" DataTextFormatString="{0}" >
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        </asp:HyperLinkField>
                           
                            <asp:BoundField DataField="BARCODE" HeaderText="BARCODE">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTNAME" HeaderText="ชื่อกระเช้า">
                                <ItemStyle Width="300px" />
                                <HeaderStyle Width="300px" Height="25px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COST" HeaderText="ราคาทุน"  >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="PRICE" HeaderText="ราคาขาย"  >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
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

