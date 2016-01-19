<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="WH_Master_Product"   Title="�ѵ�شԺ" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�ѵ�شԺ</td> 
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
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            �����ѵ�شԺ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Text="��ҹ" />
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            �������ѵ�شԺ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ������ѵ�شԺ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="205px" >
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            �����ѵ�شԺ(������)</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            �����ѵ�شԺ(�����ѧ���)</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtEName" runat="server" CssClass="zTextbox" MaxLength="200" Width="205px"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            �������</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAbbName" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                  
                     <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ������</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ˹��¹Ѻ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zComboBox" Width="205px"></asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            �Ҥҷع</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCost" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label25" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            �ҤҢ��</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>                            
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            �Ҥҡ�ҧ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtStdPrice" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>                            
                        </td> 
                     <tr height="25">
                        <td style="height: 5px; width: 50px;"></td>
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
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            �������ҡ����觫���</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtime" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            �ѹ&nbsp;<asp:Label ID="Label20" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            �ӹǹ��觫��͵�� Lot</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label21" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                     <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            �������ҡ�ü�Ե</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtimePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            �ѹ&nbsp;<asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr> 

                    <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            �ӹǹ��Ե��� Lot</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSizePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label8" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                     <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ��Ҵ��è�</td> 
                        <td style="width: 150px">
                            <asp:TextBox ID="txtPacksize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;<asp:Label ID="Label22" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                        <td style="width: 50px">
                            ˹���</td> 
                        <td style="width: 200px">
                            <asp:DropDownList ID="cmbUnitPack" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td width="8000px"> 
                    </tr>
                    </tr> 
                        <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ��͹�����觫���</td> 
                        <td colspan="3">
                            <asp:CheckBoxList ID="chkMonth" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                Width="394px">
                                <asp:ListItem Value="1">���Ҥ�</asp:ListItem>
                                <asp:ListItem Value="2">����Ҿѹ��</asp:ListItem>
                                <asp:ListItem Value="3">�չҤ�</asp:ListItem>
                                <asp:ListItem Value="4">����¹</asp:ListItem>
                                <asp:ListItem Value="5">����Ҥ�</asp:ListItem>
                                <asp:ListItem Value="6">�Զع�¹</asp:ListItem>
                                <asp:ListItem Value="7">�á�Ҥ�</asp:ListItem>
                                <asp:ListItem Value="8">�ԧ�Ҥ�</asp:ListItem>
                                <asp:ListItem Value="9">�ѹ��¹</asp:ListItem>
                                <asp:ListItem Value="10">���Ҥ�</asp:ListItem>
                                <asp:ListItem Value="11">��Ȩԡ�¹</asp:ListItem>
                                <asp:ListItem Value="12">�ѹ�Ҥ�</asp:ListItem>
                            </asp:CheckBoxList>
                        </td> 
                    </tr>              

                    <tr height="25">
                        <td style="width: 50px"></td>
                        <td style="width: 150px"></td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtPBLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                        </td>
                        <td style="width: 50px">&nbsp;</td>
                        <td style="width: 200px">&nbsp;</td>
                    </tr>
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>