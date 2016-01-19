<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="BomSearch.aspx.cs" Inherits="Transaction_BomSearch" Title="BOM (Bill of Material)" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;BOM (Bill of Material)</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"  OnNewClick ="NewClick"  OnDeleteClick ="DeleteClick"/>
                
                
            </td> 
        </tr>
        <tr>
            <td  style =" height: 10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <table border="0" cellspacing="0" cellpadding="0" width="800"  class="searchTable">
        <tr>
            <td class="subheadertext" colspan="4">
                &nbsp;ค้นหา</td>
        </tr>

        <tr>
            <td style =" width:50px; height:10px"> </td>
            <td style =" width:100px;"></td>
            <td style =" width:210px;"></td>
            <td style =" width:440px;"></td>
        </tr>
        
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:100px;">ประเภทสินค้า</td>
            <td style="width:210px;">
                <asp:DropDownList ID="cmbProductType" runat="server" Width="300px" CssClass="zComboBox" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True" >
                </asp:DropDownList></td>
            <td style="width: 440px;">
                <asp:TextBox ID="txtProduct" runat="server" Width="41px" Enabled ="false" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:100px; height:25px">กลุ่มสินค้า</td>
            <td style="width:210px; height:25px">
                <asp:DropDownList ID="cmbProductGroup" runat="server" Width="300px" CssClass="zComboBox" >
                </asp:DropDownList></td>
            <td style="height:25px; width: 440px;"></td>
        </tr>
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="width:100px; height:25px">ชื่อสินค้า</td>
            <td style="width:210px; height:25px">
                <asp:TextBox ID="txtProductName" runat="server" Width="300px" CssClass="zTextbox"></asp:TextBox></td>
            <td style="height:25px; width: 440px;">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
            </td>
        </tr>
        
        <tr>
            <td style =" width:50px; height:10px"> </td>
            <td style =" width:100px; height:10px"></td>
            <td style =" width:210px; height:10px"></td>
            <td style =" width:440px; height:10px"></td>
        </tr>
        
    </table>
            </td>
        </tr>
        <tr>
            <td style="height:10px">
            </td>
        </tr>
        <tr style="height: 25px">
            <td>
    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  Width="800px" OnRowDataBound="gvResult_RowDataBound">
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
                            
                            <asp:BoundField DataField="LOID" HeaderText="LOID"  >
                            <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="RANK" HeaderText="ลำดับที่" >
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="BARCODE" HeaderText="รหัสสินค้า"  >
                                <ItemStyle Width="105px" HorizontalAlign="Center" />
                                <HeaderStyle Width="105px" HorizontalAlign="Center" />
                            </asp:BoundField>    
                            
                            <asp:HyperLinkField DataNavigateUrlFields="LOID" DataTextField="NAME" HeaderText="ชื่อสินค้า"
                                DataNavigateUrlFormatString="Bom.aspx?loid={0}" DataTextFormatString="{0}" SortExpression="NAME" />

                            <asp:BoundField DataField="UNITNAME" HeaderText="หน่วยนับ"  >
                                <ItemStyle Width="60px"/>
                                <HeaderStyle Width="60px"/>
                            </asp:BoundField>        
                                               
                            <asp:BoundField DataField="PRODUCTGROUP" HeaderText="กลุ่มสินค้า"  >
                                <ItemStyle Width="150px"/>
                                <HeaderStyle Width="150px"/>
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="PRODUCTTYPE" HeaderText="ประเภทสินค้า"  >
                                <ItemStyle Width="150px"/>
                                <HeaderStyle Width="150px"/>
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="LOTSIZE" HeaderText="จำนวนต่อ Lot" HtmlEncode="false" DataFormatString="{0:#,##0.00}"  >
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
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

