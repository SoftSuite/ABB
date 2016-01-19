<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductType.aspx.cs" Inherits="Master_ProductType" Title="ประเภทสินค้า" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ประเภทสินค้า</td> 
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
                            รหัสประเภทสินค้า</td> 
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="20" Width="200px"></asp:TextBox>
                            <asp:Label ID="Label11" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อประเภทสินค้า</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="200px"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td width="150" style="height: 5px">
                            ใช้สำหรับ</td>
                        <td>
			<table>
			<tr>
			<td>
                            <asp:RadioButtonList ID="rbtIsType" runat="server" RepeatDirection="Horizontal" Width="250px">
                                <asp:ListItem Value="FG">สินค้าสำเร็จรูป</asp:ListItem>
                                <asp:ListItem Value="WH">วัตถุดิบ</asp:ListItem>
                                <asp:ListItem Value="OT">อื่นๆ</asp:ListItem>
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
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" /></td> 
                    </tr> 
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></td>
                    </tr>
                </table>
            </td> 
        </tr> 
    </table>
</asp:Content>