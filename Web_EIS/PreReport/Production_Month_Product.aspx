<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Production_Month_Product.aspx.cs" Inherits="PreReport_Production_Month_Product" Title="Production_Month_Product" %>

<%@ Register Src="Control/CtlProduct_Production.ascx" TagName="CtlProduct" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>

    <uc1:CtlProduct ID="CtlProduct1" runat="server" />
</asp:Content>

