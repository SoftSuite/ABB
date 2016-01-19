<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendQCCtrl.ascx.cs" Inherits="Transaction_Production_SendQCCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" cellspacing="0" cellpadding="0" width="1000" >

        <tr >
            <td style="height:25px; width: 50px;"></td>
            <td style="width: 150px; height: 25px">
                <asp:Label ID="label1" runat ="server" Text ="วันที่ส่งวิเคราะห์" Width="115px"></asp:Label></td>
            <td style="height:25px;Width:800px">
                &nbsp;<uc1:DatePickerControl ID="PkSendQcDate" runat="server" />
                <asp:Label ID="lable1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                &nbsp; &nbsp;<asp:Button ID="btnSendQc" runat="server" CssClass="zComboBox" Height="29px"
                    OnClick="btnSendQc_Click" Text="ส่ง QC" Width="93px" />
                &nbsp; &nbsp; &nbsp;
                <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Width="54px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" CssClass="zHidden" Width="56px"></asp:TextBox>
                <asp:TextBox ID="txtPdpLoid" runat="server" CssClass="zHidden" Width="56px"></asp:TextBox>
                <asp:TextBox ID="txtPoLoid" runat="server" CssClass="zHidden" Width="56px"></asp:TextBox></td>  
        </tr>
        
        <tr>
            <td colspan ="3" style="height:20px;"></td>
        </tr>
        
        <tr>
            <td colspan ="3" class="subheadertext">การวิเคราะห์คุณภาพ</td>
        </tr>
    <tr>
        <td style="height:20px" colspan="3">
        </td>
    </tr>
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">QC Sampling 1st</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtQcQty1" runat="server" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox>
                <asp:DropDownList  ID="cmbUnit1" runat="server" CssClass="zComboBox"  Enabled="False"></asp:DropDownList></td> 
        </tr>

        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">QC Sampling 2nd</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtQcQty2" runat="server" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox>
                <asp:DropDownList  ID="cmbUnit2" runat="server" CssClass="zComboBox" Enabled="False"></asp:DropDownList></td> 
        </tr>
        
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">QC Sampling 3rd</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtQcQty3" runat="server" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox>
                <asp:DropDownList  ID="cmbUnit3" runat="server" CssClass="zComboBox" Enabled="False"></asp:DropDownList></td> 
        </tr>
        
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">ผลการวิเคราะห์</td>
            <td style=" height:25px; width: 800px;">
            <asp:RadioButton ID="ResultY" runat="server" Text="ผ่าน" GroupName="active" AutoPostBack="True" Enabled="False"  />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="ResultN" runat="server" Text="ไม่ผ่าน" GroupName="active" Enabled="False"  />
            </td>
        </tr>

        <tr>
            <td style=" width: 50px;"></td>
            <td style="vertical-align :top; width: 150px;">หมายเหตุ</td>
            <td style=" width: 800px;">
                <asp:TextBox ID="txtRadiateRemark" runat="server"  CssClass="zTextbox-View" Width="626px" Height="99px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td> 
        </tr>

    </table>
 