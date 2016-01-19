<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="CreditCardSearch.aspx.cs" Inherits="Master_CreditCardSearch" Title="ข้อมูลประเภทบัตรเครดิต" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลประเภทบัตรเครดิต</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" 
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr> 
        <tr>
            <td style="height:10px">
            </td>
        </tr>
        <tr style="height:25px">
            <td>
                
            </td>
        </tr>
        <tr style="height:25px">
            <td style="height:10px">
            </td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="grvCreditCard" runat="server" Width="400px" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" OnRowDataBound="grvCreditCard_RowDataBound">
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
                        <asp:BoundField DataField="CODE" HeaderText="รหัสบัตรเครดิต">
                            <ItemStyle Width="100px" />
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>

                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="CreditCard.aspx?loid={0}"
                            DataTextField="NAME" HeaderText="ชื่อบัตรเครดิต" DataTextFormatString="{0}" >
                            <ItemStyle Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:HyperLinkField>
                        
                        <asp:BoundField DataField="CHARGE" HeaderText="ค่าธรรมเนียม(%)" DataFormatString="{0:#,##0.00}" HtmlEncode="false">
                            <ItemStyle Width="120px" HorizontalAlign="right"/>
                            <HeaderStyle Width="120px" />
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
