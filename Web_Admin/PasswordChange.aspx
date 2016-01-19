<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PasswordChange.aspx.cs" Inherits="PasswordChange" Title="����¹���ʼ�ҹ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;����¹���ʼ�ҹ</td> 
        </tr> 
        <tr>
            <td style="width: 351px">
                <table border="0" cellspacing="0" cellpadding="0" style="width: 800px;">
                    <tr>
                        <td rowspan="8" style="height:70; width: 50px;" align="center" valign="top"></td> 
                        <td style="width: 100px; height:25px"></td> 
                        <td></td> 
                    </tr>
                    <tr style="height:25">
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="lblUser" runat="server" AssociatedControlID="CurrentPassword">���ͼ���� :&nbsp;&nbsp;</asp:Label></td>
                        <td style="width: 700px;">
                            <asp:Label ID="lblUserText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height:25">
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">���ʼ�ҹ :&nbsp;&nbsp;</asp:Label></td>
                        <td>
                            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" CssClass="zTextBox" Width="140px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctlChangePassword">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height:25">
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">���ʼ�ҹ���� :&nbsp;&nbsp;</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="zTextBox" Width="140px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                ErrorMessage="New Password is required." ToolTip="New Password is required."
                                ValidationGroup="ctlChangePassword">*</asp:RequiredFieldValidator> 
                        </td>
                    </tr>
                    <tr style="height:25">
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">�׹�ѹ���ʼ�ҹ :&nbsp;&nbsp;</asp:Label> 
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" CssClass="zTextBox" Width="140px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required."
                                ValidationGroup="ctlChangePassword">*</asp:RequiredFieldValidator> 
                        </td>
                    </tr>
                    <tr style="height:25">
                        <td style="height: 25px; color:Red; width: 800px;" colspan="2">
                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="�׹�ѹ���ʼ�ҹ��ͧ�ç�Ѻ���ʼ�ҹ����"
                            ValidationGroup="ctlChangePassword"></asp:CompareValidator> 
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </td>
                    </tr>
                    <tr style="height:25">
                        <td style="width: 100px; height: 25px">
                        </td>
                        <td style="height: 25px">
                            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="����¹���ʼ�ҹ" ValidationGroup="ctlChangePassword" Width="100px" OnClick="ChangePasswordPushButton_Click" />
                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="¡��ԡ" Width="100px" OnClick="CancelPushButton_Click" />
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table> 
</asp:Content>

