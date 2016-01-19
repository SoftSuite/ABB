<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RadiationCtrl.ascx.cs" Inherits="Transaction_Production_RadiationCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" cellspacing="0" cellpadding="0" width="1000" >

        <tr >
            <td  style="height:34px; width: 50px;"></td>
            <td style="height:34px; width: 150px;">วันที่ส่งไปฉายรังสี</td>
            <td style=" height:34px; width: 800px;">
                <uc1:DatePickerControl ID="PkRadiateDate" runat="server" />
                <asp:Label ID="lable1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:Button ID="btnSend" runat="server" Text="ส่งฉายรังสี" Width="93px" CssClass="zComboBox" OnClick="btnSend_Click" Height="29px" /></td>  
        </tr>
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">จำนวนที่ส่ง</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtRadiateQty" runat="server" CssClass="zTextbox" Width="123px"></asp:TextBox>
                <asp:Label ID="Label1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                <asp:DropDownList  ID="cmbRadiateUnit" runat="server" CssClass="zComboBox" Width="141px"  AutoPostBack="True"></asp:DropDownList> 
                <asp:Label ID="Label2" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label></td> 
        </tr>
        <tr>
            <td style=" width: 50px;"></td>
            <td style="width: 150px; vertical-align :top">หมายเหตุ</td>
            <td style=" width: 800px;">
                <asp:TextBox ID="txtRadiateRemark" runat="server"  CssClass="zTextbox" Width="626px" Height="99px" TextMode="MultiLine"></asp:TextBox></td> 
        </tr>
        <tr>
            <td style=" width: 50px;"> </td> 
            <td colspan="2">
                <asp:TextBox ID="txtPdpLoid" runat="server"  Visible ="false"></asp:TextBox>
                <asp:TextBox ID="txtPdLoid" runat="server"  Visible ="false"></asp:TextBox><asp:TextBox ID="txtPoLoid" runat="server"  Visible ="false"></asp:TextBox>
                <asp:TextBox ID="txtULoid" runat="server"  Visible ="false"></asp:TextBox>
                <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Width="41px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" CssClass="zHidden" Width="37px"></asp:TextBox></td>
        </tr>
    </table>