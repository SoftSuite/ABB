<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductInvoiceSearch.aspx.cs" Inherits="Transaction_ProductInvoiceSearch"  Title="������Ѻ�Թ" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;������Ѻ�Թ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�觤�ѧ"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;����</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ������</td>
                        <td width="150px">
                            <asp:DropDownList ID="cmbRequisitionType" runat="server" CssClass="zComboBox" Width="150px">
                            </asp:DropDownList></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ���������Ѻ�Թ</td>
                        <td width="150px">
                            <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����Ѻ�ͧ</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlReserveFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlReserveTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ������Թ���</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlReqFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlReqTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �����١���</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ʶҹ�</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
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
                <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowDataBound="grvRequisition_RowDataBound">
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
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="�����"
                                    ImageUrl="~/Images/icn_print.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="30px"/>
                            <HeaderStyle Width="30px"/>
                            <FooterStyle Width="30px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ӴѺ">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductInvoice.aspx?loid={0}"
                            DataTextField="INVCODE" HeaderText="�Ţ���<br>�����" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="REQDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���<br>�͡�����" DataField="REQDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "�١���" DataField="CUSTOMERNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductInvoice.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ���<br>�Ѻ����觫���" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RESERVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���<br>�Ѻ�ͧ" DataField="RESERVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="SENDDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���<br>���Թ���" DataField="SENDDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
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