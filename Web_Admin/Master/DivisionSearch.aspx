<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="DivisionSearch.aspx.cs" Inherits="Master_DivisionSearch" Title="ข้อมูลฝ่าย" %>

<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลฝ่าย</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr> 
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr height="25">
            <td>
                
            </td>
        </tr>
        <tr height="25">
            <td height="10">
            </td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="grvDivision" runat="server" Width="400px" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" OnRowDataBound="grvDivision_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle  Width="30px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="30px"  HorizontalAlign="Center"/> 
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
                        <asp:BoundField DataField="CODE" HeaderText="รหัส">
                            <ItemStyle Width="80px" />
                            <HeaderStyle Width="80px" /> 
                        </asp:BoundField>

                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Division.aspx?loid={0}"
                            DataTextField="TNAME" HeaderText="ชื่อภาษาไทย" DataTextFormatString="{0}" >
                            <ItemStyle Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:HyperLinkField>
                        
                        <asp:BoundField DataField="ABBNAME" HeaderText="ชื่อย่อ">
                            <ItemStyle Width="100px" />
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="DEPARTMENTNAME" HeaderText="ส่วนงาน">
                            <ItemStyle Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:BoundField>
                            
                            <asp:BoundField DataField="DEPARTMENT" >
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
        <tr>
            <td>
                &nbsp;</td> 
        </tr> 
    </table>
</asp:Content>
