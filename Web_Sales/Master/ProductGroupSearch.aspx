<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductGroupSearch.aspx.cs" Inherits="Master_ProductGroupSearch" Title="กลุ่มสินค้า" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;กลุ่มสินค้า</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr> 
        <tr height="25">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvProductGroup" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="600px" OnRowDataBound="grvProductGroup_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="30px" /> 
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                        <asp:BoundField DataField="LOID" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CODE" HeaderText="รหัสกลุ่มสินค้า">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductGroup.aspx?loid={0}"
                            DataTextField="NAME" HeaderText="ชื่อกลุ่มสินค้า" DataTextFormatString="{0}" >
                            <ItemStyle Width="220px" />
                            <HeaderStyle Width="220px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="PRODUCTTYPENAME" HeaderText="ประเภทสินค้า">
                            <ItemStyle Width="200px" />
                            <HeaderStyle Width="200px" /> 
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

