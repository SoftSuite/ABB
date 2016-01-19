<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SalePrice_Month_Yearly.aspx.cs" Inherits="PreReport_SalePrice_Month_Yearly" Title="Untitled Page" %>
<%@ Register Src="Control/CtlYearly_No.ascx" TagName="CtlYearly_No" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlYearly_No ID="CtlYearly1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>

