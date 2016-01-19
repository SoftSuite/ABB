<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="OfficerSearch.aspx.cs" Inherits="Master_OfficerSearch" Title="�����ž�ѡ�ҹ" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�����ž�ѡ�ҹ</td> 
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
         <tr>
            <td>
                <asp:GridView ID="grvItem" runat="server" Width="400px" CssClass="t_tablestyle" EmptyDataText="<center>***��辺������***</center>"  AutoGenerateColumns="False" OnRowDataBound="grvItem_RowDataBound">
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
                        <asp:BoundField DataField="USERID" HeaderText="��������к�">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" /> 
                        </asp:BoundField>
                        
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Officer.aspx?loid={0}"
                            DataTextField="USERNAME" HeaderText="����-���ʡ��" DataTextFormatString="{0}" >
                            <ItemStyle Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:HyperLinkField>
                        
                        <asp:BoundField DataField="NICKNAME" HeaderText="�������">
                            <ItemStyle Width="80px"/>
                            <HeaderStyle Width="80px" /> 
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="DVNAME" HeaderText="����">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="TEL" HeaderText="���Ѿ��">
                            <ItemStyle Width="80px"/>
                            <HeaderStyle Width="80px" />
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
