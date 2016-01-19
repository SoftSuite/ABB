<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Position.aspx.cs" Inherits="Master_Position" Title="ข้อมูลตำแหน่ง" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลตำแหน่ง</td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
                 BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                 OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
            </td> 
        </tr> 
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="750px">
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            รหัสตำแหน่ง</td> 
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อตำแหน่ง</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                   
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="83px"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>

