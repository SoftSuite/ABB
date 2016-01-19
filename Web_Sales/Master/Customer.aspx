<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Master_Customer" Title="�����ź���ѷ/�١���" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�����ź���ѷ/�١���</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl id="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" OnCancelClick="CancelClick"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="��Ѻ˹�Ҥ���" OnBackClick="BackClick" OnSaveClick="SaveClick">
                </uc1:ToolbarControl></td> 
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%" >
        <tr>
            <td class="subheadertext" colspan="3" style="height: 24px"  >
                ���ͺ���ѷ/�١���
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����١���   
            </td>
            <td><asp:TextBox ID="txtCusCode" runat="server" CssClass="zTextbox"></asp:TextBox>
                <asp:Label ID="LabelCusCode" runat="server" Text="*" ForeColor="red"></asp:Label>
                <asp:TextBox id="txtLOID" runat="server" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ʶҹ�ͧ���
            </td>
            <td><asp:RadioButton ID="radPersonal" runat="server" Text="�ؤ��" GroupName="status" AutoPostBack="True" OnCheckedChanged="radPersonal_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radPrivate" runat="server" Text="����ѷ�͡��" GroupName="status" AutoPostBack="True" OnCheckedChanged="radPrivate_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radOrganize" runat="server" Text="ͧ���/˹��§ҹ�Ѱ" GroupName="status" AutoPostBack="True" OnCheckedChanged="radOrganize_CheckedChanged" />
            </td>
        </tr>
        <tr id="trPSNumber" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���ʻ�Шӵ�ǻ�ЪҪ�
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtPersonID" runat="server" CssClass="zTextbox" Width="250px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trPSName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����١���
            </td>
            <td><asp:DropDownList ID="cmbTitle" runat="server" Width="85px" CssClass="zComboBox"></asp:DropDownList><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                <asp:TextBox ID="txtFirstname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                &nbsp;���ʡ��&nbsp;
                <asp:TextBox ID="txtLastname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr id="trPrivateTaxNumber" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�Ţ��Шӵ�Ǽ����������
            </td>
            <td><asp:TextBox ID="txtTaxNumber" runat="server" CssClass="zTextbox" Width="250px"></asp:TextBox>
                <asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trPrivateName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���ͺ���ѷ
            </td>
            <td><asp:TextBox ID="txtPrivateName" runat="server" CssClass="zTextbox" Width="477px"></asp:TextBox>
                <asp:Label ID="Label10" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr id="trORGName" runat="server">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">����ͧ���/˹��§ҹ
            </td>
            <td><asp:TextBox ID="txtOrganizeName" runat="server" CssClass="zTextbox" Width="477px"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�������١���
            </td>
            <td style="height: 25px"><asp:DropDownList ID="cmbMemberType" runat="server" Width="477px" CssClass="zComboBox"></asp:DropDownList>
                <asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�ѹ�������Ҫԡ
            </td>
            <td style="height: 25px">
                <uc2:DatePickerControl id="dtpEFDate" runat="server"></uc2:DatePickerControl>
                <asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ѹ����������&nbsp;&nbsp;
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
                ���͹䢡�ê����Թ
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���͹䢡�ê���
            </td>
            <td><asp:DropDownList ID="cmbPaymentCondition" runat="server" Width="240px" CssClass="zComboBox">
                    <asp:ListItem Value="">���͡</asp:ListItem>
                    <asp:ListItem Value="CA">�Թʴ</asp:ListItem>
                    <asp:ListItem Value="CC">�ѵ��ôԵ</asp:ListItem>
                    <asp:ListItem Value="CR">�Թ����</asp:ListItem>
                </asp:DropDownList>   
                <asp:Label ID="Label11" runat="server" ForeColor="red" Text="*"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�ôԵ
            </td>
            <td><asp:TextBox ID="txtCreditPeriod" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;�ѹ</td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ǧ�Թ�ôԵ
            </td>
            <td><asp:TextBox ID="txtCreditAmount" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;�ҷ</td>
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
                ����������ѷ/�١���/��Ҫԡ (�����Ũ��ʴ��������Ѻ�Թ)
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�������
            </td>
            <td><asp:TextBox ID="txtCusAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���
            </td>
            <td><asp:TextBox ID="txtCusRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�ѧ��Ѵ
            </td>
            <td><asp:DropDownList ID="cmbCusProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbCusProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����
            </td>
            <td><asp:DropDownList ID="cmbCusAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbCusAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�Ӻ�
            </td>
            <td><asp:DropDownList ID="cmbCusDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">������ɳ���
            </td>
            <td><asp:TextBox ID="txtCusZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���Ѿ��
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
                ���ͼ��Դ���
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">
                �ӹ�˹��</td>
            <td><asp:DropDownList ID="cmbCTitle" runat="server" Width="83px" CssClass="zComboBox"></asp:DropDownList>
                ����&nbsp;<asp:TextBox ID="txtContactFirstname" runat="server" CssClass="zTextbox" Width="153px"></asp:TextBox>&nbsp;���ʡ��&nbsp;
                <asp:TextBox ID="txtContactLastname" runat="server" CssClass="zTextbox" Width="156px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���Ѿ��
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtContactTel" runat="server" CssClass="zTextbox" Width="158px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��Ͷ��&nbsp;
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
            <td style="width:145px; height:25px;">�������
            </td>
            <td><asp:TextBox ID="txtContactAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Button ID="btnContactAddress" runat="server" Text="����͹����������ѷ" OnClick="btnContactAddress_Click" /></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���
            </td>
            <td><asp:TextBox ID="txtContactRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�ѧ��Ѵ
            </td>
            <td><asp:DropDownList ID="cmbContactProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����
            </td>
            <td><asp:DropDownList ID="cmbContactAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�Ӻ�
            </td>
            <td><asp:DropDownList ID="cmbContactDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">������ɳ���
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
                ʶҹ������Թ��� (�����Ũ��ʴ��㺤���Թ�����)
            </td>
        </tr>
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">����ʶҹ���
            </td>
            <td style="height: 25px"><asp:TextBox ID="txtDeliveryPlace" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;������&nbsp;
                <asp:DropDownList ID="cmbDeliveryBy" runat="server" Width="158px" CssClass="zComboBox">
                    <asp:ListItem Value="ZZ">����к�</asp:ListItem>
                    <asp:ListItem Value="CU">�Ѻ�ͧ</asp:ListItem>
                    <asp:ListItem Value="CA">����ö��ŹԸ�</asp:ListItem>
                    <asp:ListItem Value="TR">���º���ѷ�Ѻ��ҧ����</asp:ListItem>
                    <asp:ListItem Value="MA">�觷ҧ��ɳ���</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�������
            </td>
            <td><asp:TextBox ID="txtDeliveryAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Button ID="btnDeliveryAddress" runat="server" Text="����͹����������ѷ" OnClick="btnDeliveryAddress_Click" /></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���
            </td>
            <td><asp:TextBox ID="txtDeliveryRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�ѧ��Ѵ
            </td>
            <td><asp:DropDownList ID="cmbDeliveryProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbDeliveryProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����
            </td>
            <td><asp:DropDownList ID="cmbDeliveryAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbDeliveryAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�Ӻ�
            </td>
            <td><asp:DropDownList ID="cmbDeliveryDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">������ɳ���
            </td>
            <td><asp:TextBox ID="txtDeliveryZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">���Ѿ��
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
                �����˵�
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

