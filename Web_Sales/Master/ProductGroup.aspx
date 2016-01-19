<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductGroup.aspx.cs" Inherits="Master_ProductGroup" Title="กลุ่มสินค้า" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;กลุ่มสินค้า</td> 
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
                            รหัสกลุ่มสินค้า</td> 
                        <td>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="20" Width="150px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ประเภทสินค้า</td> 
                        <td>
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="200px">
                            </asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อกลุ่มสินค้า</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="200px"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px"></td> 
                        <td>
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" />
                        </td> 
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