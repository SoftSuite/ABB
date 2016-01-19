<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductReserveSearch.aspx.cs" Inherits="Transaction_ProductReserveSearch" Title="���ͧ���ԡ�ѵ�شԺ��к�è��ѳ��" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;���ͧ���ԡ�ѵ�شԺ��к�è��ѳ��</td> 
                </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�觤�ѧ�ѵ�شԺ"
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
                        <td style="width: 161px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ������ͧ���ԡ</td>
                        <td style="width: 161px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox>
                            </td>
                        <td width="20px" align="center">
                            </td>
                        <td style="width: 170px">
                            </td>
                        <td></td>
                    </tr>    
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ѹ�����ԡ</td>
                        <td style="width: 161px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>                                      
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ���ͼ�Ե�ѳ��</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="319px"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �Ţ����ü�Ե</td>
                        <td style="width: 161px">
                            <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr>
                      <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            �ҡ��ѧ�ѵ�شԺ</td>
                        <td style="width: 161px"><asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center"></td>
                        <td style="width: 170px"></td>
                        <td>
                            </td>
                    </tr>                      
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ʶҹ�</td>
                        <td style="width: 161px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
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
                        <td style="width: 161px"></td>
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
                <asp:GridView ID="grvPDReserve" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="800px" OnRowCommand="grvPDReserve_RowCommand" OnRowDataBound="grvPDReserve_RowDataBound">
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
                                <asp:ImageButton ID="btnCopy" AlternateText="�Ѵ�͡"
                                        runat="server" CausesValidation="False" CommandName="copy" ImageUrl="~/Images/icn_copy.gif"/>
                                <asp:ImageButton ID="btnCancel" AlternateText="¡��ԡ"
                                        runat="server" CausesValidation="False" CommandName="cancelpdrequest" ImageUrl="~/Images/icn_cancel.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px"/>
                            <FooterStyle Width="60px"/>
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
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="ProductReserve.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="����ͧ���ԡ" DataTextFormatString="{0}" >
                            <ItemStyle Width="110px" HorizontalAlign="center"/>
                            <HeaderStyle Width="110px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField SortExpression="REQDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���" DataField="REQDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="LOTNO" HeaderText = "�Ţ����ü�Ե" DataField="LOTNO">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="PDNAME" HeaderText = "���ͼ�Ե�ѳ��" DataField="PDNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>                        
                        <asp:BoundField SortExpression="BATCHSIZE" HeaderText = "�ӹǹ" DataField="BATCHSIZE">
                            <ItemStyle Width="80px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                         <asp:BoundField SortExpression="BATCHSIZEUNITNAME" HeaderText = "˹��¹Ѻ" DataField="BATCHSIZEUNITNAME">
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "ʶҹ�" DataField="STATUSNAME">
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
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