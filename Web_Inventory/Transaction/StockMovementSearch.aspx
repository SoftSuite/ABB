<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockMovementSearch.aspx.cs" Inherits="Transaction_StockMovementSearch" Title="����ѵԡ������͹����Թ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;����ѵԡ������͹����Թ���</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"
                    OnPrintClick="PrintClick" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1100" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;����
                        </td>
                    </tr>                
                    <tr style="height:10px">
                        <td colspan="6"></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �ѹ�������͹���</td>
                        <td style="width:170px">
                            <uc2:DatePickerControl ID="ctlDateFrom" runat="server" /></td>
                        <td style="width:20px" align="center">
                            �֧</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �������Թ���</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                     <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ������Թ���</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            �����Թ���</td>
                        <td style="width:360px" colspan="3">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="360px" /></td>
                        <td></td>                            
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ��ѧ</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ���¨ҡ⫹</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouseFrom" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td></td>
                    </tr>
                    <tr style="height:25px">
                        <td style="width:50px"></td>
                        <td style="width:150px">
                            ����价��⫹</td>
                        <td style="width:360px" colspan="3">
                            <asp:DropDownList ID="cmbWarehouseTo" runat="server" CssClass="zComboBox" Width="360px" /></td>
                        <td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr>
                    <tr style="height: 10px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                        </td>
                        <td colspan="3" style="width: 360px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>
        <tr style="height:10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvStockMovementItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="1100px">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:BoundField SortExpression="NO" HeaderText = "�ӴѺ" DataField="NO" >
                            <ItemStyle Width="50px" HorizontalAlign="center" />
                            <HeaderStyle Width="50px" Height="18px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "�����Թ���" DataField="PRODUCTNAME">
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "�ѹ���" DataField="CREATEON" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>    
                        <asp:BoundField SortExpression="FROMWAREHOUSENAME" HeaderText = "���¨ҡ��ѧ" DataField="FROMWAREHOUSENAME" >
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="FROMZONENAME" HeaderText = "���¨ҡ⫹" DataField="FROMZONENAME" >
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TOWAREHOUSENAME" HeaderText = "����价���ѧ" DataField="TOWAREHOUSENAME" >
                            <ItemStyle Width="120px"/>
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TOZONENAME" HeaderText = "����价��⫹" DataField="TOZONENAME" >
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QTY" HeaderText = "�ӹǹ" DataField="QTY" DataFormatString="{0:#,##0.00}" HtmlEncode="False">
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>
                         <asp:BoundField SortExpression="UNITNAME" HeaderText = "˹���" DataField="UNITNAME">
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px" />  
                        </asp:BoundField>                        
                        <asp:BoundField SortExpression="TYPENAME" HeaderText = "�����͡���" DataField="TYPENAME">
                            <ItemStyle Width="150px"/>
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField SortExpression="DOCCODE" HeaderText = "�Ţ�����ҧ�ԧ" DataField="DOCCODE">
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