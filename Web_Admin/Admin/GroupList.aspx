<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="Admin_GroupList" Title="�Է�������ҹ��к�" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td style="height: 27px" class="headtext">
            &nbsp;�Է�������ҹ��к�</td>
    </tr>
    <tr>
        <td style="height: 16px" class="toolbarplace">
            <uc1:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" 
                BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnDelete="ź�����" NameBtnNew="���������" 
                OnNewClick="NewClick" OnDeleteClick="DeleteClick"/>
        </td>
    </tr>
    <tr>
        <td style="height: 16px">
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="600" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                <tr>
                    <td class="subheadertext" colspan="4" style="height: 25px">
                        &nbsp;����</td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 15px">
                    </td>
                    <td style="width: 160px; height: 15px">
                    </td>
                    <td style="width: 200px; height: 15px">
                    </td>
                    <td style="height: 15px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 25px">
                    </td>
                    <td style="width: 160px; height: 25px">
                        ���͡����</td>
                    <td style="width: 200px; height: 25px">
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="zTextbox" Width="180px"></asp:TextBox></td>
                    <td style="height: 25px">
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click"
                            ToolTip="����" /></td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 15px">
                    </td>
                    <td style="width: 160px; height: 15px">
                    </td>
                    <td style="width: 200px; height: 15px">
                    </td>
                    <td style="height: 15px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowDataBound="gvMain_RowDataBound">
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
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
                    <asp:HyperLinkField HeaderText="���͡����" DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="GroupDetail.aspx?id={0}" DataTextField="DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="MENUCOUNT" HeaderText="�ӹǹ����">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="LOID" >
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>

