<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="CustomerSearch.aspx.cs" Inherits="Master_CustomerSearch" Title="����ѷ/�١���" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;����ѷ/�١���</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="100%"  class="searchTable">
        <tr>
            <td class="subheadertext" colspan="3"  >
                &nbsp;���Һ���ѷ/�١���
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
            <td><asp:TextBox ID="txtCusCode" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>    
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">ʶҹ�ͧ���
            </td>
            <td><asp:RadioButton ID="radPersonal" runat="server" Text="�ؤ��" GroupName="status" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radPrivate" runat="server" Text="����ѷ�͡��" GroupName="status" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="radOrganize" runat="server" Text="ͧ���/˹��§ҹ�Ѱ" GroupName="status" />
            </td>
        </tr> 
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�����١���/����ѷ/ͧ���
            </td>
            <td>
                 <asp:TextBox ID="txtCusName" runat="server" CssClass="zTextbox" Width="240px"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;���ʡ�� 
                 <asp:TextBox ID="txtLastName" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:50px; height:25px;">&nbsp;  
            </td>
            <td style="width:145px; height:25px;">�������١���
            </td>
            <td style="height: 25px" valign="bottom">
                 <asp:DropDownList ID="cmbMemberType" runat="server" Width="240px" CssClass="zComboBox"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; height: 10px">
            </td>
            <td style="width: 145px; height: 10px">�ѧ��Ѵ
            </td>
            <td style="height: 10px">
                <asp:DropDownList ID="cmbProvince" runat="server" Width="240px" CssClass="zComboBox"></asp:DropDownList> 
                <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="imbSearch_Click" ImageAlign="AbsMiddle" /> 
            </td>
        </tr>
    </table>
            </td>
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
      EmptyDataText="<center>***��辺������***</center>"  AllowPaging="False" OnRowDataBound="gvResult_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LOID" HeaderText="LOID" HeaderStyle-CssClass="zHidden" ControlStyle-CssClass="zHidden" FooterStyle-CssClass="zHidden" ItemStyle-CssClass="zHidden">
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDERNO" HeaderText="�ӴѺ���" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="�����١���">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" Height="25px"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="�����١���" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplCusName" runat="server" Text='<%# Bind("CUSNAME") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MEMBERTYPE" HeaderText="�������١���"  >
                                <ItemStyle Width="100px"/>
                                <HeaderStyle Width="100px"/>
                            </asp:BoundField>                           
                            <asp:BoundField DataField="CUSTOMERTYPE" HeaderText="ʶҹ�ͧ���"  >
                                <ItemStyle Width="130px"/>
                                <HeaderStyle Width="130px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="EPDATE" HeaderText="�ѹ�������">
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                <HeaderStyle Width="90px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYMENT" HeaderText="���͹䢡�ê���" >
                                <ItemStyle Width="100px"/>
                                <HeaderStyle Width="100px"/>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        <PagerSettings Visible="False" />
                    </asp:GridView> 
            </td>
        </tr>
    </table>
</asp:Content>

