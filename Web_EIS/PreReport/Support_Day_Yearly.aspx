<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Support_Day_Yearly.aspx.cs" Inherits="PreReport_Support_Day_Yearly" Title="Untitled Page" %>

<%@ Register Src="Control/CtlMonth.ascx" TagName="CtlMonth" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:CtlMonth ID="CtlMonth1" runat="server" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</asp:Content>

