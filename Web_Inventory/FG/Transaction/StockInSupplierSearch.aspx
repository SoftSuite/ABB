<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockInSupplierSearch.aspx.cs" Inherits="FG_Transaction_StockInSupplierSearch" Title="��Ǩ�Ѻ��Ե�ѳ��������ٻ" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;��Ǩ�Ѻ��Ե�ѳ��������ٻ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�觵�Ǩ QC"
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
                            �Ţ���㺵�Ǩ�Ѻ</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSTCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ����Ǩ�Ѻ</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlReceiveFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlRecriveTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                   <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ����觢ͧ</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtInvNo" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                                       <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ������觫���</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPoCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                                       <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ�����觫���</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlOrderFrom" runat="server"  />
                            </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlOrderTo" runat="server" /></td>
                        <td></td>
                    </tr> 

                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ����˹���</td>
                        <td colspan="3"><asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ������ QC</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtQCCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
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
                    Width="900px" OnRowCommand="grvRequisition_RowCommand" OnRowDataBound="grvRequisition_RowDataBound">
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
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px"/>
                            <FooterStyle Width="60px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ӴѺ���">
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

                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="StockInSupplier.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="�Ţ����Ǩ�Ѻ" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="RECEIVEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����Ǩ�Ѻ" DataField="RECEIVEDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="INVNO" HeaderText = "�Ţ�����觢ͧ" DataField="INVNO">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="SUPPLIERNAME" HeaderText = "����˹���" DataField="SUPPLIERNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="QCCODE" HeaderText = "�Ţ������ QC" DataField="QCCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                        
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "�Թ���" DataField="PRODUCTNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        
                        <asp:BoundField SortExpression="QCDUEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ��� QC ��Ǩ����" DataField="QCDUEDATE" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                          
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "ʶҹ�" DataField="STATUSNAME">
                            <ItemStyle Width="80px"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  

                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>
