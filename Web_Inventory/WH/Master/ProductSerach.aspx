<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductSerach.aspx.cs" Inherits="WH_Master_ProductSerach" Title="ข้อมูลวัตถุดิบ" %>

<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>

    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลวัตถุดิบ</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnPrintClick="PrintClick" />
                    <asp:LinkButton ID="btnDownload" runat="server" CssClass="toolbarbutton">LinkButton</asp:LinkButton>
            </td> 
        </tr> 
        <tr>
            <td height="10">
            </td>
        </tr>
         <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000" class="searchTable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;ค้นหา</td>
                    </tr>
                    <tr height="10">
                        <td height="10" style="width: 50px">
                        </td>
                        <td height="10" style="width: 150px">
                        </td>
                        <td height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ประเภทวัตถุดิบ</td> 
                        <td>
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td> 
                    </tr> 
                   <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            กลุ่มวัตถุดิบ</td> 
                        <td>
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="205px">
                            </asp:DropDownList></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อวัตถุดิบ</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>&nbsp;
                               <asp:ImageButton id="btnSearch" runat="server" OnClick="btnSearch_Click" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle">
                            </asp:ImageButton></td> 
                    </tr> 
                      <tr height="10px">
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>  
                </table>
            </td> 
        </tr>  
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvProduct" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="1000px" OnRowDataBound="grvProduct_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="30px" HorizontalAlign="Center"/> 
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                       <asp:BoundField DataField="PRODUCTMASTER" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BARCODE" HeaderText="บาร์โค้ด">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="PRODUCTMASTER" DataNavigateUrlFormatString="Product.aspx?loid={0}"
                            DataTextField="NAME" HeaderText="ชื่อวัตถุดิบ" DataTextFormatString="{0}" >
                            <ItemStyle Width="220px" />
                            <HeaderStyle Width="220px" /> 
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="UNIT" HeaderText="หน่วยนับ">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle Width="80px" /> 
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTGROUPNAME" HeaderText="กลุ่มสินค้า">
                            <ItemStyle Width="100px" />
                            <HeaderStyle Width="100px" /> 
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCTTYPENAME" HeaderText="ประเภทวัตถุดิบ">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                            <HeaderStyle Width="150px" /> 
                        </asp:BoundField>
                        <asp:BoundField DataField="COST" HeaderText="ราคาทุน">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <HeaderStyle Width="70px" /> 
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

