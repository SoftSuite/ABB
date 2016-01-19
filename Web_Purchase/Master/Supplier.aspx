<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="Master_Supplier" Title="�����ź���ѷ/����˹���" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�����ź���ѷ/����˹���</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl id="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" OnCancelClick="CancelClick"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="��Ѻ˹�Ҥ���" OnBackClick="BackClick" OnSaveClick="SaveClick">
                </uc1:ToolbarControl></td> 
        </tr>
        <tr style="height: 25px">
            <td class="subheadertext">
                &nbsp;���ͺ���ѷ/����˹���
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="550" >
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">���ʼ���˹���   
            </td>
            <td style="width: 351px"><asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="120px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="*" CssClass="zRemark"></asp:Label>
                <asp:TextBox id="txtLOID" runat="server" ReadOnly="True" Visible="False">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 50px; height: 25px">
            </td>
            <td style="width: 150px; height: 25px">
                �Ţ��Шӵ�Ǽ����������
            </td>
            <td style="width: 351px">
                <asp:TextBox ID="txtTaxNumber" runat="server" CssClass="zTextbox" Width="120px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr id="trSupplierName" runat="server" style="color: #000000">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">
                ���ͼ��Ѵ��˹���
            </td>
            <td style="width: 351px"><asp:TextBox ID="txtSupplierName" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Label ID="Label10" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">ʶҹС����ҹ
            </td>
            <td style="height: 25px; width: 351px;">
                <asp:CheckBox ID="chkActive" runat="server" Text="��ҹ" />
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 150px; height: 10px">
            </td>
            <td style="height: 10px; width: 351px;">
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="subheadertext">
                &nbsp;����������ѷ/����˹��� (�����Ũ��ʴ�����觫���)
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="550" >
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�������
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr style="color: #000000">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">���
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr style="color: #000000">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�ѧ��Ѵ
            </td>
            <td style="width: 350px"><asp:DropDownList ID="cmbSupProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbSupProvince_SelectedIndexChanged">
                </asp:DropDownList>   
                <asp:Label ID="Label5" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr style="color: #000000">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�����
            </td>
            <td style="width: 350px"><asp:DropDownList ID="cmbSupAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbSupAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
                <asp:Label ID="Label6" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr style="color: #000000">
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�Ӻ�
            </td>
            <td style="width: 350px"><asp:DropDownList ID="cmbSupDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">������ɳ���
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">���Ѿ��
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupTel" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">Fax
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupFax" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">E-Mail
            </td>
            <td style="width: 350px"><asp:TextBox ID="txtSupEmail" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 150px; height: 10px">
            </td>
            <td style="height: 10px; width: 350px;">
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="subheadertext">
                &nbsp;���ͼ��Դ���
            </td>
        </tr>
        <tr style="height: 25px">
            <td width="550">
    <table border="0" cellspacing="0" cellpadding="0" width="700" >
        <tr>
            <td style="height: 10px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">
                �ӹ�˹��</td>
            <td style="width: 500px"><asp:DropDownList ID="cmbCTitle" runat="server" Width="80px" CssClass="zComboBox"></asp:DropDownList>
                ����&nbsp;<asp:TextBox ID="txtContactFirstname" runat="server" CssClass="zTextbox" Width="100px"></asp:TextBox>&nbsp;���ʡ��&nbsp;
                <asp:TextBox ID="txtContactLastname" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">���Ѿ��
            </td>
            <td style="height: 25px; width: 500px;"><asp:TextBox ID="txtContactTel" runat="server" CssClass="zTextbox" Width="158px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;��Ͷ��&nbsp;
                <asp:TextBox ID="txtContactMobile" runat="server" CssClass="zTextbox" Width="156px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">E-Mail
            </td>
            <td style="width: 500px"><asp:TextBox ID="txtContactEmail" runat="server" CssClass="zTextbox" Width="474px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�������
            </td>
            <td style="width: 500px"><asp:TextBox ID="txtContactAddress" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                <asp:Button ID="btnContactAddress" runat="server" Text="����͹����������ѷ" OnClick="btnContactAddress_Click" /></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">���
            </td>
            <td style="width: 500px"><asp:TextBox ID="txtContactRoad" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�ѧ��Ѵ
            </td>
            <td style="width: 500px"><asp:DropDownList ID="cmbContactProvince" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactProvince_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�����
            </td>
            <td style="width: 500px"><asp:DropDownList ID="cmbContactAmphur" runat="server" Width="240px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbContactAmphur_SelectedIndexChanged">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">�Ӻ�
            </td>
            <td style="width: 500px"><asp:DropDownList ID="cmbContactDistrict" runat="server" Width="240px" CssClass="zComboBox">
                </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:150px; height:25px;">������ɳ���
            </td>
            <td style="width: 500px"><asp:TextBox ID="txtContactZipCode" runat="server" CssClass="zTextbox" Width="240px" MaxLength="5"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 150px; height: 10px">
            </td>
            <td style="height: 10px; width: 500px;">
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="subheadertext">
                &nbsp;�����˵�</td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="700" >
        <tr>
            <td style="height: 10px" colspan="2"></td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 650px"><asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" Width="617px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="height: 10px; width: 650px;">
            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>

