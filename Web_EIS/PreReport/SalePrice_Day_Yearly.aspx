<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SalePrice_Day_Yearly.aspx.cs" Inherits="PreReport_SalePrice_Day_Yearly" Title="Untitled Page" %>
<%@ Register Src="Control/CtlMonth_No.ascx" TagName="CtlMonth_No" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlMonth_No ID="CtlMonth1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>

