<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SupplierSearch.aspx.cs" Inherits="Master_SupplierSearch" Title="ข้อมูลบริษัท/ผู้จำหน่าย" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลบริษัท/ผู้จำหน่าย</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr>
        <tr style="height: 25px">
            <td style="height:10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">
        <tr>
            <td class="subheadertext" colspan="3"  >
                ค้นหาผู้จำหน่าย
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">รหัสผู้จำหน่าย   
            </td>
            <td style="width: 600px"><asp:TextBox ID="txtSupCode" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>    
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">ชื่อผู้จำหน่าย
            </td>
            <td style="width: 600px"><asp:TextBox ID="txtSupName" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
            <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="imbSearch_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 150px; height: 10px">
            </td>
            <td style="height: 10px; width: 600px;">
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td style="height:10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
      EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  OnRowDataBound="gvResult_RowDataBound" Width="800px">
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
                            <asp:BoundField DataField="CODE" HeaderText="รหัสผู้จำหน่าย">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" Height="25px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ชื่อผู้จำหน่าย">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSupplierName" runat="server" Text='<%# Bind("SUPPLIERNAME") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                                <HeaderStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TAXID" HeaderText="เลขประจำตัวผู้เสียภาษีอากร"  >
                                <ItemStyle Width="200px" HorizontalAlign="Center" />
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="CNAME" HeaderText="ชื่อผู้ติดต่อ"  >
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TEL" HeaderText="โทรศัพท์" >
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                <HeaderStyle Width="90px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        <PagerSettings Visible="False" />
                    </asp:GridView> 
            </td>
        </tr>
    </table>
    <br />
    
</asp:Content>

