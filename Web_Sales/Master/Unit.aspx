<%@ Page Language="C#" MasterPageFile="~/Template/Page1.Master" AutoEventWireup="true" CodeFile="Unit.aspx.cs" Inherits="Master_Unit"  Title="˹��¹Ѻ" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarCtl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;˹��¹Ѻ
            </td> 
        </tr> 
        <tr height="25">
            <td class="toolbarplace">
                <uc2:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
            </td> 
        </tr>

        <tr height="25px">
            <td></td> 
        </tr> 
        <tr>
            <td style="height: 65px">
                <table border= "0" cellspacing="0" cellpadding="0" width="800px">
                    <tr �height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ����˹��¹Ѻ</td>
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" Text="" Width="254px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ����˹��¹Ѻ������</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Text="" Width="254px"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                      <tr �height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ����˹��¹Ѻ�����ѧ���</td>
                        <td>
                            <asp:TextBox ID="txtEName" runat="server" CssClass="zTextbox" Text="" Width="254px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>

                    </tr> 
                     <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td width="150" style="height: 5px">
                            ������Ѻ</td>
                        <td>
			<table>
			<tr>
			<td>
                            <asp:RadioButtonList ID="rbtIsType" runat="server" RepeatDirection="Horizontal" Width="250px">
                                <asp:ListItem Selected="True" Value="ALL">������</asp:ListItem>
                                <asp:ListItem Value="FG">�Թ���������ٻ</asp:ListItem>
                                <asp:ListItem Value="WH">�ѵ�شԺ</asp:ListItem>
                            </asp:RadioButtonList></td>
			    <td>
                            <asp:Label ID="Label9" runat="server" CssClass="zRemark" Text="*"></asp:Label>
			    </td></tr></table>
			    </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px"></td> 
                        <td>
                            <asp:CheckBox ID="chkActive" runat="server" Text="��ҹ" /></td> 
                    </tr> 
                     <tr height="25">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></td>
                    </tr> 
                </table></td> 
        </tr>
    </table>
</asp:Content>