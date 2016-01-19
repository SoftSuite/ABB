<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ControlStockSearch.aspx.cs" Inherits="FG_Master_ControlStockSearch" Title="�Ǻ�������ҳ�Թ���" %>

<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;�Ǻ�������ҳ�Թ���/�ѵ�شԺ</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
                
            </td> 
        </tr>
        <tr style="height: 25px">
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
                    <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">
                        <tr>
                            <td class="subheadertext" colspan="6">
                                &nbsp;����</td>
                        </tr>
                        <tr>
                            <td style="height:10px; width: 50px;"></td>
                            <td style="width:100px; height:10px;">
                            </td>
                            <td style="height:10px; width: 210px;">
                            </td>
                            <td style="width: 50px; height: 10px">
                            </td>
                            <td style="width: 200px; height: 10px">
                            </td>
                            <td style="height:10px; width: 440px;">
                            </td>
                        </tr>   
                        <tr>
                            <td style="width:50px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ��ѧ
                            </td>
                            <td  style="height:24px; width: 210px;">
                                <asp:TextBox ID="txtWHName" runat="server" ReadOnly="true" CssClass="zTextbox-view" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 50px; height: 24px">
                            </td>
                            <td style="width: 200px; height: 24px">
                            </td>
                            <td style="height:10px; width: 440px;">
                                <asp:TextBox ID="txtWarehouse" runat="server" CssClass="zHidden" ReadOnly="true"
                                    Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width:50px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ������</td>
                            <td  style="height:24px; width: 210px;">
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 50px; height: 24px">
                                �֧</td>
                            <td style="width: 200px; height: 24px">
                                <asp:TextBox ID="txtBarcodeTo" runat="server" CssClass="zTextbox" Width="200px">
                                </asp:TextBox></td>
                            <td style="height:10px; width: 440px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                �����Թ���/�ѵ�شԺ</td>
                            <td  style="height:24px; width: 210px;">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="200px"></asp:TextBox>
                            </td>
                            <td style="width: 50px; height: 24px">
                            </td>
                            <td style="width: 200px; height: 24px">
                            </td>
                            <td style="height:10px; width: 440px;">&nbsp;
                                <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="imbSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td height="10" style="width: 50px">
                            </td>
                            <td height="10" style="width: 100px">
                            </td>
                            <td height="10" style="width: 210px">
                            </td>
                            <td height="10" style="width: 50px">
                            </td>
                            <td height="10" style="width: 200px">
                            </td>
                            <td height="10" style="width: 440px">
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr style="height: 25px">
            <td height="10">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
      EmptyDataText="<center>***��辺������***</center>" OnRowDataBound="gvResult_RowDataBound" Width="800px"  >
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PDMINMAXLOID" HeaderText="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDERNO" HeaderText="�ӴѺ���" >
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="������">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplBarCode" runat="server" Text='<%# Bind("BARCODE") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BARCODE" HeaderText="BARCODE">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTNAME" HeaderText="�����Թ���">
                                <ItemStyle Width="250px" />
                                <HeaderStyle Width="250px" Height="25px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STANDARD" HeaderText="����ҳ�����"  >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="MINIMUM" HeaderText="����ҳ����ش"  >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAXIMUM" HeaderText="����ҳ�٧�ش" >
                                <ItemStyle Width="110px" HorizontalAlign="Right" />
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
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

