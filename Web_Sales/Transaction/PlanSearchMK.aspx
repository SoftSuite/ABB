<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanSearchMK.aspx.cs" Inherits="Transaction_PlanSearchMK" Title="Ἱ��õ�Ҵ" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
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
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnSubmit="�׹�ѹ"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="700" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;����</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td style="width: 100px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            �� �.�.</td>
                        <td width="150px">
                            <asp:TextBox ID="txtYearFrom" runat="server" CssClass="zTextbox" MaxLength="4" Width="80px"></asp:TextBox></td>
                        <td width="20px">
                            �֧</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtYearTo" runat="server" CssClass="zTextbox" MaxLength="4" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtNewLOID" runat="server" CssClass="zHidden" MaxLength="4" Width="60px"></asp:TextBox></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            �ѹ������ҧ</td>
                        <td width="150px"><uc2:DatePickerControl ID="ctlCreateFrom" runat="server" />
                            </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlCreateTo" runat="server" /></td>
                        <td></td>
                    </tr> 

                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            ʶҹ�</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="130px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="130px">
                        </asp:DropDownList></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td style="width: 100px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPlan" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="700px" OnRowCommand="grvPlan_RowCommand" OnRowDataBound="grvPlan_RowDataBound" >
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
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="��������ҳ��â��"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                    <asp:ImageButton ID="btnPrint2" runat="server" CausesValidation="False" CommandName="print" AlternateText="�������ػ��â��"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                    <asp:ImageButton ID="btnPrint3" runat="server" CausesValidation="False" CommandName="print" AlternateText="�����ŵ�ҧ�ҡ��û���ҳ"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                <asp:ImageButton ID="btnCopy" AlternateText="�Ѵ�͡"
                                        runat="server" CausesValidation="False" CommandName="copy" ImageUrl="~/Images/icn_copy.gif"/>

                                <asp:TextBox ID="txtNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="90px"/>
                            <HeaderStyle Width="90px"/>
                            <FooterStyle Width="90px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ӴѺ���">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="PlanOrderMK.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ���Ἱ�ҹ" DataTextFormatString="{0}" >
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:HyperLinkField> 
                        <asp:BoundField SortExpression="YEAR" HeaderText = "�� �.�." DataField="YEAR">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ������ҧ" DataField="CREATEON" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>

                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "ʶҹ�" DataField="STATUSNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>