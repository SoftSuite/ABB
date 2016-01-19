<%@ Page Language="C#" MasterPageFile="~/Template/Page1.Master" AutoEventWireup="true" CodeFile="TestTemplate.aspx.cs" Inherits="TestTemplate" Title="Untitled Page" %>

<%@ Register Assembly="WebControls" Namespace="WebControls" TagPrefix="cc1" %>
<%@ Register Src="Controls/ToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;หน่วยนับ
            </td> 
        </tr> 
        <tr height="25">
            <td class="toolbarplace">
                <uc2:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="true" BtnSaveShow="false" BtnSubmitShow="false" />
            </td> 
        </tr>
        <tr>
            <td class="subheadertext">
                &nbsp;ชื่อบริษัทลูกค้าสมาชิก</td> 
        </tr>
        <tr height="25px">
            <td></td> 
        </tr> 
        <tr>
            <td style="height: 65px">
                <table border= "0" cellspacing="0" cellpadding="0" width="800px">
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            รหัสประเภทสินค้า 
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="zTextbox-view" ReadOnly="True" Text=""></asp:TextBox>
                        </td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อประเภทสินค้า 
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="zTextbox" Text=""></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                </table></td> 
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MenuContent">
    <cc1:systemmenu id="SystemMenu1" runat="server"></cc1:systemmenu>
</asp:Content>
