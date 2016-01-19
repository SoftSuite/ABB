<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="Admin_UserDetail" Title="�Է�������ҹ��к�" %>
<%@ Register Src="../Controls/Z2BoxControl.ascx" TagName="Z2BoxControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server" >
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td style="height: 27px" class="headtext">
            &nbsp;��������´�Է�������ҹ�ͧ�����</td>
    </tr>
    <tr>
        <td class="toolbarplace">
            <uc1:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="true" BtnCancelShow="true" BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" OnBackClick="BackClick" onSaveClick="SaveClick" OnCancelClick="CancelClick"/>
        </td>
    </tr>
    <tr>
        <td style="height: 15px">
            <asp:TextBox ID="txtRoleID" runat="server" Visible="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding-left:10px; padding-right:10px; padding-bottom:10px; padding-top:10px">        
            <asp:Label ID="lblRoleError" runat="server" ForeColor="Red" Text="�ѧ��������ҧ�Է�������ҹ ��س��к��дѺ����� ��С��ѹ�֡"
                Visible="False"></asp:Label>
        <table cellspacing="0" cellpadding="0" border="0" width="650" bgcolor="#eeeeee">
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 15px; text-align: right">
                </td>
                <td style="height: 15px">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 23px; text-align: right">
                    ���������ҹ :</td>
                <td>
                    <asp:Label ID="lblUID" runat="server" ForeColor="#0000C0"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 23px; text-align: right">
                    ���ͼ����ҹ :</td>
                <td>
                    <asp:Label ID="lblUName" runat="server" ForeColor="#0000C0"></asp:Label></td>
            </tr>
            <tr>
                <td style="width:120px; height:23px; text-align:right; padding-right:5px">
                    �дѺ�����ҹ :</td> 
                <td>
                        <asp:DropDownList ID="cmbLevel" runat="server" CssClass="zComboBox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cmbLevel_SelectedIndexChanged">
                            <asp:ListItem Value="U">�����ҹ�к�</asp:ListItem>
                            <asp:ListItem Value="M">���˹�ҧҹ</asp:ListItem>
                            <asp:ListItem Value="A">�������к�</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <asp:Panel ID="pnlExtSystem" runat="Server" Width="100%" Visible="false">
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 23px; text-align: right">
                    �к� HHT :</td>
                <td>
                    <asp:CheckBox ID="chkHHT" runat="server" /></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 23px; text-align: right">
                    �к� POS :</td>
                <td>
                    <asp:CheckBox ID="chkPOS" runat="server" /></td>
            </tr>
            </asp:Panel>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 15px; text-align: right">
                </td>
                <td style="height: 15px">
                </td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td style="height: 15px;padding-left:10px">
            <asp:Panel ID="pnlRole" runat="server"  Width="100%">
                <table cellspacing="0" cellpadding="0" border="0" width="650">
                    <tr>
                        <td style="height:15px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnbGroup" runat="server" OnClick="lnbGroup_Click">[��˹������]</asp:LinkButton>
                            <asp:LinkButton ID="lnbMenu" runat="server" OnClick="lnbMenu_Click">[��˹��Է������������]</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 15px">
                        <hr size="1" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eeeeee" style="padding-top:10px; padding-bottom:10px">
                            <asp:Panel ID="pnlGroup" runat="server"  Width="100%">
                                <uc2:Z2BoxControl ID="z2Group" runat="server" txtDestHead="���������������Է���" txtSourceHead="�����������" />
                           </asp:Panel>
                            <asp:Panel ID="pnlMenu" runat="server"  Width="100%">
                                <uc2:Z2BoxControl ID="z2Menu" runat="server" txtDestHead="���ٷ�����Է��������ҹ" txtSourceHead="���ٷ�����" />
                           </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Content>

