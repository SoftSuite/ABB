<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Stockin_Month_Product.aspx.cs" Inherits="PreReport_Stockin_Month_Product" Title="Untitled Page" %>

<%@ Register Src="Control/CtlProduct.ascx" TagName="CtlProduct" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlProduct ID="CtlProduct1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>
