<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Master_Product"  Title="�Թ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�Թ���</td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
                 BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="˹�������"
                 OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
            </td> 
        </tr> 
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="750px">
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �������Թ���</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Text="��ҹ" /></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ������Թ���</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="205px" >
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �����Թ���</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Labe24" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �����Թ��� ������</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �����Թ��� �����ѧ���</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtEName" runat="server" CssClass="zTextbox" MaxLength="200" Width="205px"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 150px">
                            ��������Թ���</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAbbName" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            </td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ������</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �Ţ����¹���Ѻ��</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtRegisNo" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            </td> 
                    </tr>   
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ˹��¹Ѻ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �Ҥҷع</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCost" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:Label ID="Label19" runat="server" CssClass="zRemark" Text="*"></asp:Label> </td> 
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            �ҤҢ��</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:Label ID="Label18" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            ��äԴ����</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsVat" runat="server" RepeatDirection="Horizontal" Width="305px">
                                <asp:ListItem Value="1">�ҤҢ����� VAT</asp:ListItem>
                                <asp:ListItem Value="0">�ҤҢ�������� VAT</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            ��äԴ��ǹŴ</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsDiscount" runat="server" RepeatDirection="Horizontal" Width="315px">
                                <asp:ListItem Value="1">�Դ��ǹŴ</asp:ListItem>
                                <asp:ListItem Value="0">���Դ��ǹŴ</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            �������ҤҢ��</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsEdit" runat="server" RepeatDirection="Horizontal" Width="347px">
                                <asp:ListItem Value="Y">��</asp:ListItem>
                                <asp:ListItem Value="N">�����</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            ��ä׹�Թ��Ҫԡ
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsRefund" runat="server" RepeatDirection="Horizontal" Width="348px">
                                <asp:ListItem Value="Y">�׹</asp:ListItem>
                                <asp:ListItem Value="N">���׹</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                     <tr height="25">
                        <td width="50" style="height: 5px"></td>
                        <td style="height: 5px; width: 150px;">
                            �Ըա�ù����</td>
                        <td colspan="3">
			                <table>
			                    <tr>
			                        <td>
                                        <asp:RadioButtonList ID="rbtOrderType" runat="server" RepeatDirection="Horizontal" Width="305px" AutoPostBack="True" OnSelectedIndexChanged="rbtOrderType_SelectedIndexChanged" >
                                            <asp:ListItem Value="PD">��Ե�ͧ</asp:ListItem>
                                            <asp:ListItem Value="PO">��ҧ��Ե</asp:ListItem>
                                            <asp:ListItem Value="AR">����ͧ�Ը�</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
			                        <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="zRemark" Text="*"></asp:Label>			    
                                    </td>
                                </tr>
                            </table>			    
                        </td>
                    </tr>
                     <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            �������ҡ����觫���</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtime" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            �ѹ&nbsp;<asp:Label ID="Label20" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            �ӹǹ��觫��͵�� Lot</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label21" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                     <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            �������ҡ�ü�Ե</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtimePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            �ѹ&nbsp;<asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr> 

                    <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            �ӹǹ��Ե��� Lot</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSizePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label8" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 

                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ����</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAge" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px">1</asp:TextBox>
                            ��</td> 
                    </tr> 
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            <span lang="TH" style="font-size: 14pt; line-height: 115%; font-family: 'Angsana New';
                                mso-ansi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: Calibri;
                                mso-hansi-font-family: Calibri; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                                mso-bidi-language: TH">��Ҵ��è�(� </span><span style="font-size: 11pt; line-height: 115%;
                                    font-family: Times New Roman; mso-fareast-font-family: Calibri; mso-ansi-language: EN-US;
                                    mso-fareast-language: EN-US; mso-bidi-language: TH; mso-bidi-font-size: 14.0pt;
                                    mso-bidi-font-family: 'Angsana New'">1 </span><span lang="TH" style="font-size: 14pt;
                                        line-height: 115%; font-family: 'Angsana New'; mso-ansi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                                        mso-fareast-font-family: Calibri; mso-hansi-font-family: Calibri; mso-ansi-language: EN-US;
                                        mso-fareast-language: EN-US; mso-bidi-language: TH">˹��¼�Ե)</span></td> 
                        <td width="150px">
                            <asp:TextBox ID="txtPacksize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;<asp:Label ID="Label22" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                        <td width="50px">
                            ˹���</td> 
                        <td width="200px">
                            <asp:DropDownList ID="cmbUnitPack" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ������Թ���㹡�кǹ��ü�Ե</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProduceGroup" runat="server" CssClass="zComboBox" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>                     
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td style="width: 150px">
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtPBLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>
