<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ToDoList_ProductAnalysisSearch.aspx.cs" Inherits="QC_ToDoList_ProductAnalysisSearch" Title="��¡����������س�Ҿ" %>
<%@ Register Src="~/Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;��¡����������س�Ҿ</td> 
                </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="�駼š�õ�Ǩ"
                    OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="600px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;To Do List</td>
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
                            �Ţ����觵�Ǩ</td>
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
                            �ѹ����觵�Ǩ</td>
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
                            �Ţ����Ѻ/��Ե</td>
                        <td style="width: 161px">
                            <asp:TextBox ID="txtLotNo" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td> <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>                     
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            </td>
                        <td style="width: 161px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px" Visible="False">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            </td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px" Visible="False">
                        </asp:DropDownList></td>
                        <td>
                           </td>
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
                    EmptyDataText="<center>***��辺������***</center>" OnRowDataBound="grvPDReserve_RowDataBound">
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
                        <asp:TemplateField HeaderText="�ӴѺ">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:TemplateField HeaderText="�Ţ����觵�Ǩ" SortExpression="QCCODE">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkAnalysis" Text="" runat="server"></asp:HyperLink> 
                            </ItemTemplate>
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="QCDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ����觵�Ǩ" DataField="QCDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="CODE" HeaderText = "�Ţ����Ѻ/��Ե" DataField="CODE">
                            <ItemStyle Width="150px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="PDNAME" HeaderText = "�����Թ���" DataField="PDNAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />  
                        </asp:BoundField>                        
                        <asp:BoundField SortExpression="QTY" HeaderText = "�ӹǹ" DataField="QTY">
                            <ItemStyle Width="50px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                         <asp:BoundField SortExpression="UNAME" HeaderText = "˹���" DataField="UNAME">
                            <ItemStyle Width="50px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="APPROVER" HeaderText = "����觵�Ǩ" DataField="APPROVER">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="DVNAME" HeaderText = "����" DataField="DVNAME">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="STATUSVAL" HeaderText = "ʶҹ�" DataField="STATUSVAL">
                            <ItemStyle Width="120px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>  
                        <asp:BoundField DataField="TABLENAME">
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

