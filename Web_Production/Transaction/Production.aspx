<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Production.aspx.cs" Inherits="Transaction_Production" Title="�ѹ�֡��ü�Ե��Ե�ѳ��ҡ��ع��" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>

<%@ Register Src="Production/ProductionControl.ascx" TagName="TabControl" TagPrefix="uc3" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl2" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                �ѹ�֡��ü�Ե��Ե�ѳ��ҡ��ع��</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true" NameBtnPrint = "�����ѹ�֡��ü�Ե"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" OnSaveClick ="SaveClick" OnBackClick ="BackClick" OnPrintClick="PrintClick"  />
                <uc4:ToolbarControl2 ID="ToolbarControl2" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true" NameBtnPrint ="�����ѹ�֡��ü�Ե����"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnPrintClick="PrintClick"  />
            </td> 
        </tr>
        <tr>
            <td  style =" height: 10px">
            </td>
        </tr>
        <tr >
            <td class="searchTable">
                <table border="0" cellspacing="0" cellpadding="0" width="800"  >
                    <tr>
                        <td style="width: 50px; height: 10px">
                        </td>
                        <td style="width: 150px; height: 10px">
                        </td>
                        <td style="width: 250px; height: 10px">
                        </td>
                        <td style="width: 100px; height: 10px">
                        </td>
                        <td style="width: 250px; height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">��������ü�Ե</td>
                        <td style="width: 250px; height: 25px"><asp:DropDownList  ID="cmbType" runat ="server" Width ="221px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbType_SelectedIndexChanged" >
                            <asp:ListItem Value="FG">�Թ���</asp:ListItem>
                            <asp:ListItem Value="WH">�ѵ�شԺ</asp:ListItem>
                        </asp:DropDownList></td>
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="Label3" runat="server" Text="�觤�ѧ" Width="61px" ></asp:Label></td>
                        <td style="width:250px; height:25px"><asp:DropDownList  ID="cmbWarehouse" runat ="server" Width ="190px" CssClass="zComboBox" Enabled="False" >
                            <asp:ListItem Value="1">��ѧ������ٻ</asp:ListItem>
                            <asp:ListItem Value="2">��ѧ�ѵ�شԺ</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                   <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">�Ţ���ѹ�֡��觼�Ե</td>
                       <td style="width: 250px; height: 25px">
                            <asp:TextBox  ID="txtRqCode" runat="server" Width="150px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                            <asp:ImageButton ID="btnSelect" runat="server" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle" OnClick="btnSelect_Click" /></td>
                       <td style="width: 100px; height: 25px">
                           <asp:Label ID="Label2" runat="server" Text="�ѹ�����觼�Ե" ></asp:Label></td>
                        <td style="width:255px; height:25px">
                            <asp:TextBox
                            ID="txtReqDate" runat="server" CssClass="zTextbox-View" Width="83px" ReadOnly="True"></asp:TextBox>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtRqiLoid" runat="server" Width="61px" CssClass="zHidden"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">�Ţ����ü�Ե</td>
                        <td style="width: 250px; height: 25px">
                            <asp:TextBox  ID="txtLotNo" runat="server" Width="150px" CssClass="zTextbox"></asp:TextBox></td>
                        <td style="width: 100px; height: 25px">
                            <asp:Label ID="Label1" runat="server" Text="�ѹ����Ե" Width="61px" ></asp:Label></td>
                        <td style="width:250px; height:25px">
                            <uc2:DatePickerControl
                                ID="dpMfgDate" runat="server" Enabled="true" />
                            &nbsp;
                            <asp:TextBox ID="txtQty" runat="server" Width="50px" CssClass="zHidden"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">
                            �Թ���</td>
                        <td colspan="3" style="height: 25px">
                            <asp:DropDownList ID="cmbProduct" runat="server" Width="377px" OnSelectedIndexChanged ="cmbProduct_SelectedIndexChanged" AutoPostBack ="true" CssClass="zComboBox" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">
                            ��Ҵ��è�</td>
                        <td colspan="3" style="height: 25px">
                            <asp:TextBox ID="txtPackSize" runat="server" Width="104px" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox>
                            <asp:DropDownList ID="cmbUnitPZ" runat ="server" Width ="110px" Enabled="False" CssClass="zComboBox"  ></asp:DropDownList>
                            <asp:TextBox ID="txtPdLoid" runat="server" Enabled="false" Width="22px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPdpLoid" runat="server" Enabled="false" Width="18px" Visible="False">0</asp:TextBox>
                            <asp:TextBox ID="txtPdpStdqty" runat="server" Enabled="false" Width="17px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPoLoid" runat="server" Enabled="false" Width="19px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPOSTATUS" runat="server" Enabled="false" Visible="False" Width="20px"></asp:TextBox>
                            <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Enabled="false"
                                Visible="False" Width="23px"></asp:TextBox>
                            <asp:TextBox ID="txtPdUnit" runat="server" CssClass="zHidden" Enabled="false" Visible="False"
                                Width="23px"></asp:TextBox>
                            <asp:TextBox ID="txtAge" runat="server" CssClass="zHidden" Enabled="false" Visible="False"
                                Width="23px"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50px; height:25px"></td>
                        <td style="width:150px; height:25px">
                            ����ҳ��ü�Ե</td>
                        <td colspan="3" style="height: 25px">
                            <asp:TextBox ID="txtBatchSize" runat="server" Width="101px" CssClass="zTextboxR"></asp:TextBox>
                            <asp:DropDownList  ID="cmbUnitBZ" runat ="server" Width ="110px" CssClass="zComboBox" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width:50px;"></td>
                        <td style="width:150px;" valign="top">
                            �����˵�</td>
                        <td colspan="3" valign="top">
                            <asp:TextBox ID="txtRemark" runat="server" Width="592px" Height ="80px" CssClass="zTextbox" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:50px; height:10px"></td>
                        <td style="width:150px; height:10px"></td>
                        <td style="width: 250px; height: 10px">
                        </td>
                        <td style="width: 100px; height: 10px">
                        </td>
                        <td style="width:250px; height:10px"></td>
                    </tr>
                </table> 
            
            </td> 
        </tr>
        <tr style =" height:10px">
            <td style="height: 6px">
            </td> 
        </tr> 
        <tr>
            <td>
                <uc3:TabControl id="ctlTab" runat="server" />
            </td> 
        </tr>
    </table>
</asp:Content>

