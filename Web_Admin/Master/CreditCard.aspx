<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="CreditCard.aspx.cs" Inherits="Master_CreditCard" Title="ข้อมูลประเภทบัตรเครดิต"%>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลประเภทบัตรเครดิต</td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
                 BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                 OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
            </td> 
        </tr> 
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="750px">
                    <tr style="height:25px">
                        <td style="width:50px">
                        </td>
                        <td style="width:150px">
                            รหัสบัตรเครดิต</td> 
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="0px"></asp:TextBox>
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="true" MaxLength="20" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr style="height:25px">
                        <td style="width:50px">
                        </td>
                        <td style="width:150px">
                            ชื่อบัตรเครดิต</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr style="height:25px">
                        <td style="width:50px">
                        </td>
                        <td style="width:150px">
                            ค่าธรรมเนียม(%)</td> 
                        <td>
                            <asp:TextBox ID="txtCharge" runat="server" CssClass="zTextboxR" MaxLength="50" Width="205px">0.00</asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>                    
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>

