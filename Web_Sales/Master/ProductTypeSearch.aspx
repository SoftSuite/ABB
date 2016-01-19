<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductTypeSearch.aspx.cs" Inherits="Master_ProductTypeSearch" Title="�������Թ���" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�������Թ���</td> 
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
                <asp:GridView ID="grvProductType" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***��辺������***</center>"  AutoGenerateColumns="False" Width="400px" OnRowDataBound="grvProductType_RowDataBound">
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
                       <asp:TemplateField HeaderText="�ӴѺ���">
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
                        <asp:BoundField DataField="CODE" HeaderText="���ʻ������Թ���">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductType.aspx?loid={0}"
                            DataTextField="NAME" HeaderText="���ͻ������Թ���" DataTextFormatString="{0}" >
                            <ItemStyle Width="220px" />
                            <HeaderStyle Width="220px" /> 
                        </asp:HyperLinkField>
                       <asp:TemplateField HeaderText="������Ѻ">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# (Eval("TYPE").ToString() == ABB.Data.Constz.ProductType.Type.FG.Code ?  ABB.Data.Constz.ProductType.Type.FG.Name : (Eval("TYPE").ToString() == ABB.Data.Constz.ProductType.Type.WH.Code ?  ABB.Data.Constz.ProductType.Type.WH.Name : ABB.Data.Constz.ProductType.Type.Others.Name)) %>'></asp:Label> 
                            </ItemTemplate> 
                            <HeaderStyle Width="150px" /> 
                       </asp:TemplateField> 
                    </Columns> 
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                    <PagerSettings Visible="False" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>
