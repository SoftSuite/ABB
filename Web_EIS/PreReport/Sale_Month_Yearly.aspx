<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Sale_Month_Yearly.aspx.cs" Inherits="PreReport_Sale_Month_Yearly" Title="Untitled Page" %>
<%@ Register Src="Control/CtlYearly.ascx" TagName="CtlYearly" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlYearly ID="CtlYearly1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>

