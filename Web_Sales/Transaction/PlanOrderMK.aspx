<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanOrderMK.aspx.cs" Inherits="Transaction_PlanOrderMK" Title="Ἱ��õ�Ҵ" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;Ἱ��õ�Ҵ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="True" NameBtnPrint = "��������ҳ��â��"
                    BtnReturnShow="false" BtnSaveShow="true" NameBtnSave="�ѹ�֡" BtnSubmitShow="false" NameBtnSubmit="�׹�ѹ"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" NameBtnCancel="¡��ԡἹ" />
                    <uc1:ToolbarControl ID="ctlToolbar2" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="True" NameBtnPrint = "�������ػ��â��"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" />
                    <uc1:ToolbarControl ID="ctlToolbar3" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="True" NameBtnPrint = "�����ŵ�ҧ�ҡ��û���ҳ"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" />
            </td> 
        </tr>
        <tr height="10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000" class="searchTable">
                    <tr height="10px">
                        <td style="width: 25px;"></td>
                        <td style="width: 75px;">
                            <asp:TextBox ID="txtPlan" runat="server" CssClass="zHidden" MaxLength="4" Width="60px"></asp:TextBox></td>
                        <td style="width: 105px;"></td>
                        <td style="width: 75px;"></td>
                        <td style="width: 85px;"></td>
                        <td style="width: 75px"></td>
                        <td style="width: 120px"></td>
                        <td style="width: 75px"></td>
                        <td width="365px"></td>
                    </tr> 
                    <tr height="25px">
                        <td style="width: 25px"></td>
                        <td style="width: 75px" align="right">
                            �Ţ���Ἱ :&nbsp;
                        </td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 75px" align="right">
                            �� �.�. :&nbsp;</td>
                        <td style="width: 85px">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="zTextbox-View" Width="50px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 75px" align="right">
                            �ѹ������ҧ :&nbsp;</td>
                        <td style="width: 120px">
                            <asp:TextBox ID="txtCreateOn" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                        <td align="right" style="width: 75px">
                            ʶҹ� :&nbsp;
                        </td>
                        <td style="width: 365px">
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                    </tr> 
                    <tr height="25">
                        <td style="width: 25px">
                        </td>
                        <td align="right" style="width: 75px">
                            ��͸Ժ�� :&nbsp;
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="zTextbox-View" Width="440px" ReadOnly="True"></asp:TextBox></td>
                        
                    </tr>
                    <tr height="10px">
                        <td style="width: 25px; height: 10px"></td>
                        <td style="width: 75px; height: 10px;"></td>
                        <td style="width: 105px; height: 10px"></td>
                        <td style="width: 75px; height: 10px"></td>
                        <td style="width: 85px; height: 10px;"></td>
                        <td style="width: 75px; height: 10px">
                        <td style="width: 120px"></td>
                        <td style="width: 75px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="3">
            <td>
            </td>
        </tr>
        <tr>
            <td>

            </td>
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <asp:LinkButton ID="btnNewAll" runat="server" CssClass="toolbarbutton" OnClick="btnNewAll_Click" >LinkButton</asp:LinkButton>
                <uc1:ToolbarControl ID="ctlToolbarItem" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="���������" NameBtnDelete="ź�����"
                    OnDeleteClick="DeleteClick" OnNewClick="NewClick"/>
                <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="170px"></asp:TextBox></td> 
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvPlanitem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>" Width="1000px" OnRowDataBound="grvPlanitem_RowDataBound" OnRowCommand="grvPlanitem_RowCommand" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CUSTOMER">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "�����" DataField="CUSTOMERNAME">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText = "%<br>�������" SortExpression="PERCENT" >
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                 <asp:TextBox ID="txtPercent" runat="server" CssClass="zTextboxR" Text='<%# Bind("PERCENT") %>' Width="35px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="M1" HeaderText = "�.�." DataField="M1">
                        <ItemStyle HorizontalAlign="right"/>
							 <HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M2" HeaderText = "�.�." DataField="M2">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M3" HeaderText = "��.�." DataField="M3">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M4" HeaderText = "��.�." DataField="M4">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M5" HeaderText = "�.�." DataField="M5">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M6" HeaderText = "��.�." DataField="M6">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M7" HeaderText = "�.�." DataField="M7">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M8" HeaderText = "�.�." DataField="M8">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M9" HeaderText = "�.�." DataField="M9">
                                                <ItemStyle HorizontalAlign="right"/>
                                                <HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M10" HeaderText = "�.�." DataField="M10">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M11" HeaderText = "�.�." DataField="M11">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                                <asp:BoundField SortExpression="M12" HeaderText = "�.�." DataField="M12">
                                                <ItemStyle HorizontalAlign="right"/>
												<HeaderStyle Width="60px" />
                        </asp:BoundField>
                                             <asp:BoundField DataField="STATUS">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>