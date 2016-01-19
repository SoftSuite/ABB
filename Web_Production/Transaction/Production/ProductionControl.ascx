<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionControl.ascx.cs" Inherits="Transaction_Production_ProductionControl" %>
<%@ Register Src="TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Src="MaterialCtrl.ascx" TagName="MaterialCtrl" TagPrefix="uc2" %>
<%@ Register Src="ProductFillCtrl.ascx" TagName="ProductFillCtrl" TagPrefix="uc3" %>
<%@ Register Src="RadiationCtrl.ascx" TagName="RadiationCtrl" TagPrefix="uc4" %>
<%@ Register Src="RadiationReturnCtrl.ascx" TagName="RadiationReturnCtrl" TagPrefix="uc5" %>
<%@ Register Src="StockInDetailCtrl.ascx" TagName="StockInDetailCtrl" TagPrefix="uc6" %>
<%@ Register Src="SendQCCtrl.ascx" TagName="SendQCCtrl" TagPrefix="uc7" %>
<%@ Register Src="MaterialLostCtrl.ascx" TagName="MaterialLostCtrl" TagPrefix="uc8" %>
<%@ Register Src="PackageLost.ascx" TagName="PackageLost" TagPrefix="uc9" %>
<%@ Register Src="StockOutDetailCtrl.ascx" TagName="StockOutDetailCtrl" TagPrefix="uc10" %>
<table border = "0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <uc1:TabControl ID="ctlTab" runat="server" OnSelectedChange="ctlTab_SelectedChange" />
        </td> 
    </tr>
    <tr>
        <td>
            <table border = "0" cellpadding="0" cellspacing="0" width="100%" style="BORDER-RIGHT: #91A5BD 1px solid; BORDER-LEFT: #91A5BD 1px solid; BORDER-BOTTOM: #91A5BD 1px solid">
                <tr>
                    <td style="height:20px"></td> 
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPdpLoid" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                        <asp:TextBox ID="txtPdLoid" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                        <uc2:MaterialCtrl ID="ctlMaterialUsing" runat="server" />
                        <uc3:ProductFillCtrl ID="ctlPack" runat="server" Visible="false" />
                        <uc4:RadiationCtrl ID="ctlXRaySending" runat="server" Visible="false" />
                        <uc5:RadiationReturnCtrl ID="ctlXrayReceiving" runat="server" Visible="false" />
                        <uc6:StockInDetailCtrl ID="ctlImport" runat="server" Visible="false" />
                        <uc7:SendQCCtrl ID="ctlQC" runat="server" Visible="false" />
                        <uc8:MaterialLostCtrl ID="ctlMaterialLoss" runat="server" Visible="false" />
                        <uc9:PackageLost ID="ctlPackageLoss" runat="server" Visible="false" />
                        <uc10:StockOutDetailCtrl ID="ctlExport" runat="server" Visible="false" />
                    </td> 
                </tr>
                <tr>
                    <td style="height:20px"></td> 
                </tr>
            </table> 
        </td> 
    </tr>
</table>