<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Master_Customer" Title="ข้อมูลบริษัท/ลูกค้า" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลบริษัท/ลูกค้า</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl id="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" OnCancelClick="CancelClick"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="กลับหน้าค้นหา" OnBackClick="BackClick" OnSaveClick="SaveClick">
                </uc1:ToolbarControl></td> 
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3" style="height: 24px"  >
                ชื่อบริษัท/ลูกค้า
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">รหัสลูกค้า   
            </td>
            <td><asp:TextBox ID="txtCusCode" runat="server" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="LabelCusCode" runat="server" Text="*" ForeColor="red"></asp:Label>
                <asp:TextBox id="txtLOID" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">สถานะองค์กร
            </td>
            <td><asp:RadioButton ID="radPersonal" runat="server" Text="บุคคล" GroupName="status" AutoPostBack="True" OnCheckedChanged="radPersonal_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radPrivate" runat="server" Text="บริษัทเอกชน" GroupName="status" AutoPostBack="True" OnCheckedChanged="radPrivate_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radOrganize" runat="server" Text="องค์กร/หน่วยงานรัฐ" GroupName="status" AutoPostBack="True" OnCheckedChanged="radOrganize_CheckedChanged" />
            </td>
        </tr>
        <tr id="trPSNumber" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">รหัสประจำตัวประชาชน
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtPersonID" runat="server" CssClass="zTextbox" Width="250px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trPSName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ชื่อลูกค้า
            </td>
            <td><asp:DropDownList ID="cmbTitle" runat="server" Width="85px" CssClass="zComboBox"></asp:DropDownList><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                <asp:TextBox ID="txtFirstname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                &nbsp;นามสกุล&nbsp;
                <asp:TextBox ID="txtLastname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr id="trPrivateTaxNumber" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">เลขประจำตัวผู้เสียภาษี
            </td>
            <td><asp:TextBox ID="txtTaxNumber" runat="server" CssClass="zTextbox" Width="250px"></asp:TextBox>
                <asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trPrivateName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ชื่อบริษัท
            </td>
            <td><asp:TextBox ID="txtPrivateName" runat="server" CssClass="zTextbox" Width="477px"></asp:TextBox>
                <asp:Label ID="Label10" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trORGName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ชื่อองค์กร/หน่วยงาน
            </td>
            <td><asp:TextBox ID="txtOrganizeName" runat="server" CssClass="zTextbox" Width="477px"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ประเภทลูกค้า
            </td>
            <td style="height: 25px"><asp:DropDownList ID="cmbMemberType" runat="server" Width="477px" CssClass="zComboBox"></asp:DropDownList>
                <asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">วันที่เป็นสมาชิก
            </td>
            <td style="height: 25px">
                <uc2:DatePickerControl id="dtpEFDate" runat="server"></uc2:DatePickerControl>
                <asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;วันที่หมดอายุ&nbsp;&nbsp;
                <uc2:DatePickerControl ID="dtpEPDate" runat="server" />
                <asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3"  >
                เงื่อนไขการชำระเงิน
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">เงื่อนไขการชำระ
            </td>
            <td><asp:DropDownList ID="cmbPaymentCondition" runat="server" Width="240px" CssClass="zComboBox">
                    <asp:ListItem Value="">เลือก</asp:ListItem>
                    <asp:ListItem Value="CA">เงินสด</asp:ListItem>
                    <asp:ListItem Value="CC">บัตรเครดิต</asp:ListItem>
                    <asp:ListItem Value="CR">สินเชื่อ</asp:ListItem>
                </asp:DropDownList>   
                <asp:Label ID="Label11" runat="server" ForeColor="red" Text="*"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">เครดิต
            </td>
            <td><asp:TextBox ID="txtCreditPeriod" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;วัน</td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">วงเงินเครดิต
            </td>
            <td><asp:TextBox ID="txtCreditAmount" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;บาท</td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3"  >
                ที่อยู่บริษัท/ลูกค้า/สมาชิก (ข้อมูลจะแสดงในใบเสร็จรับเงิน)
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ที่อยู่
            </td>
            <td><asp:TextBox ID="txtCusAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ถนน
            </td>
            <td><asp:TextBox ID="txtCusRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">จังหวัด
            </td>
            <td><asp:DropDownList ID="cmbCusProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbCusProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">อำเภอ
            </td>
            <td><asp:DropDownList ID="cmbCusAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbCusAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ตำบล
            </td>
            <td><asp:DropDownList ID="cmbCusDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">รหัสไปรษณีย์
            </td>
            <td><asp:TextBox ID="txtCusZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">โทรศัพท์
            </td>
            <td><asp:TextBox ID="txtCusTel" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">Fax
            </td>
            <td><asp:TextBox ID="txtCusFax" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">E-Mail
            </td>
            <td><asp:TextBox ID="txtCusEmail" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3"  >
                ชื่อผู้ติดต่อ
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">
                คำนำหน้า</td>
            <td><asp:DropDownList ID="cmbCTitle" runat="server" Width="83px" CssClass="zComboBox"></asp:DropDownList>
                ชื่อ&nbsp;<asp:TextBox ID="txtContactFirstname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>&nbsp;นามสกุล&nbsp;
                <asp:TextBox ID="txtContactLastname" runat="server" CssClass="zTextbox" Width="156px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">โทรศัพท์
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtContactTel" runat="server" CssClass="zTextbox" Width="158px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มือถือ&nbsp;
                <asp:TextBox ID="txtContactMobile" runat="server" CssClass="zTextbox" Width="156px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">E-Mail
            </td>
            <td><asp:TextBox ID="txtContactEmail" runat="server" CssClass="zTextbox" Width="474px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ที่อยู่
            </td>
            <td><asp:TextBox ID="txtContactAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Button ID="btnContactAddress" runat="server" Text="เหมือนที่อยู่บริษัท" OnClick="btnContactAddress_Click" /></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ถนน
            </td>
            <td><asp:TextBox ID="txtContactRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">จังหวัด
            </td>
            <td><asp:DropDownList ID="cmbContactProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">อำเภอ
            </td>
            <td><asp:DropDownList ID="cmbContactAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ตำบล
            </td>
            <td><asp:DropDownList ID="cmbContactDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">รหัสไปรษณีย์
            </td>
            <td><asp:TextBox ID="txtContactZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3"  >
                สถานที่ส่งสินค้า (ข้อมูลจะแสดงในใบคุมสินค้าส่ง)
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ชื่อสถานที่
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtDeliveryPlace" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;ขนส่งโดย&nbsp;
                <asp:DropDownList ID="cmbDeliveryBy" runat="server" Width="158px" CssClass="zComboBox">
                    <asp:ListItem Value="ZZ">ไม่ระบุ</asp:ListItem>
                    <asp:ListItem Value="CU">รับเอง</asp:ListItem>
                    <asp:ListItem Value="CA">ส่งโดยรถมูลนิธิ</asp:ListItem>
                    <asp:ListItem Value="TR">ส่งโดยบริษัทรับจ้างขนส่ง</asp:ListItem>
                    <asp:ListItem Value="MA">ส่งทางไปรษณีย์</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ที่อยู่
            </td>
            <td><asp:TextBox ID="txtDeliveryAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Button ID="btnDeliveryAddress" runat="server" Text="เหมือนที่อยู่บริษัท" OnClick="btnDeliveryAddress_Click" /></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ถนน
            </td>
            <td><asp:TextBox ID="txtDeliveryRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">จังหวัด
            </td>
            <td><asp:DropDownList ID="cmbDeliveryProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbDeliveryProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">อำเภอ
            </td>
            <td><asp:DropDownList ID="cmbDeliveryAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbDeliveryAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ตำบล
            </td>
            <td><asp:DropDownList ID="cmbDeliveryDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">รหัสไปรษณีย์
            </td>
            <td><asp:TextBox ID="txtDeliveryZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">โทรศัพท์
            </td>
            <td><asp:TextBox ID="txtDeliveryTel" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">Fax
            </td>
            <td><asp:TextBox ID="txtDeliveryFax" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">E-Mail
            </td>
            <td><asp:TextBox ID="txtDeliveryEmail" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="2"  >
                หมายเหตุ
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="2"></td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td><asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" Width="617px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="height: 10px">
            </td>
        </tr>
    </table>
</asp:Content>

