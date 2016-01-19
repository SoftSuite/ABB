<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RadiationReturnCtrl.ascx.cs" Inherits="Transaction_Production_RadiationReturnCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" cellspacing="0" cellpadding="0" width="1000" >

        <tr >
            <td  style="height:24px; width: 50px;"></td>
            <td style="height:24px; width: 200px;">วันที่รับคืนจากการฉายรังสี</td>
            <td style=" height:24px; width: 800px;">
                <uc1:DatePickerControl ID="PkRadiateRetDate" runat="server" />
                <asp:Label ID="lable1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp;
                <asp:Button ID="btnSend" runat="server" CssClass="zComboBox" Height="29px" OnClick="btnSend_Click"
                    Text="รับคืนจากการฉายรังสี" Width="133px" /></td>  
        </tr>
        <tr>
            <td style="height:24px; width: 50px;"></td>
            <td style="height:24px; width: 200px;">จำนวนที่รับ</td>
            <td style=" height:24px; width: 800px;">
                <asp:TextBox ID="txtRadiateRetQty" runat="server" CssClass="zTextBox" Width="124px"></asp:TextBox>
                <asp:Label ID="Label1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>&nbsp; &nbsp; &nbsp; &nbsp; 
                <asp:DropDownList  ID="cmbRadiateRetUnit" runat="server" CssClass="zComboBox" Width="134px" AutoPostBack="True"></asp:DropDownList> 
                <asp:Label ID="Label2" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label></td> 
        </tr>
        <tr>
            <td style=" width: 50px;"></td>
            <td style="width: 200px; vertical-align :top">หมายเหตุ</td>
            <td style=" width: 800px;">
                <asp:TextBox ID="txtRadiateRetRemark" runat="server"  CssClass="zTextbox" Width="626px" Height="99px" TextMode="MultiLine" ></asp:TextBox></td> 
        </tr>
        <tr>
            <td style=" width: 50px;"> </td> 
            <td style="vertical-align :top" colspan="2">
                <asp:TextBox ID="txtPdpLoid" runat="server" Visible="False" Width="53px"></asp:TextBox>
                <asp:TextBox ID="txtPdLoid" runat="server"  Visible ="false" Width="56px"></asp:TextBox><asp:TextBox ID="txtPoLoid" runat="server" Visible="False" Width="66px"></asp:TextBox>
                <asp:TextBox ID="txtULoid" runat="server"  Visible ="false" Width="58px"></asp:TextBox>
                <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Width="43px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" CssClass="zHidden" Width="51px"></asp:TextBox></td> 
        </tr>
        
    </table>