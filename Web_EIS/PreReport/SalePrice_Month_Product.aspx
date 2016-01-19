<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SalePrice_Month_Product.aspx.cs" Inherits="PreReport_SalePrice_Month_Product" Title="Untitled Page" %>

<%@ Register Src="Control/CtlProduct_No.ascx" TagName="CtlProduct_No" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlProduct_No ID="CtlProduct1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>

