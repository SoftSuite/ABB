<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanOrder.aspx.cs" Inherits="Transaction_PlanOrder" Title="แผนการจำหน่ายสินค้า" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;แผนการจำหน่ายสินค้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="ยืนยัน"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSubmitClick="SubmitClick" NameBtnCancel="ยกเลิกแผน" />
            </td> 
        </tr>
        <tr style="height:10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000" class="searchTable">
                    <tr style="height:10px">
                        <td style="width: 25px;"></td>
                        <td style="width: 75px;">
                            <asp:TextBox ID="txtPlan" runat="server" CssClass="zHidden" MaxLength="4" Width="60px"></asp:TextBox></td>
                        <td style="width: 105px;"></td>
                        <td style="width: 75px;"></td>
                        <td style="width: 85px;"></td>
                        <td style="width: 75px"></td>
                        <td style="width: 120px"></td>
                        <td style="width: 75px"></td>
                        <td style="width: 365px"></td>
                    </tr> 
                    <tr style="height:25px">
                        <td style="width: 25px"></td>
                        <td style="width: 75px" align="right">
                            เลขที่แผน :&nbsp;
                        </td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 75px" align="right">
                            ปี พ.ศ. :&nbsp;</td>
                        <td style="width: 85px">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="zTextbox-View" Width="50px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 75px" align="right">
                            วันที่สร้าง :&nbsp;</td>
                        <td style="width: 120px">
                            <asp:TextBox ID="txtCreateOn" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 75px" align="right">
                            วันที่ยืนยัน :&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                    </tr> 
                    <tr style="height:25">
                        <td style="width: 25px">
                        </td>
                        <td align="right" style="width: 75px">
                            คำอธิบาย :&nbsp;
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="zTextbox-View" Width="440px" ReadOnly="True"></asp:TextBox></td>
                        <td align="right" style="width: 75px">
                            สถานะ :&nbsp;
                        </td>
                        <td style="width: 365px">
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr style="height:10px">
                        <td style="width: 25px; height: 10px"></td>
                        <td style="width: 75px; height: 10px;"></td>
                        <td style="width: 105px; height: 10px"></td>
                        <td style="width: 75px; height: 10px"></td>
                        <td style="width: 85px; height: 10px;"></td>
                        <td style="width: 75px; height: 10px"></td>
                        <td style="width: 120px"></td>
                        <td style="width: 75px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr style="height:3">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1000" class="searchTable">
                    <tr style="height:10px">
                        <td style="width: 50px;"></td>
                        <td style="width: 90px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 80px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 65px"></td>
                        <td style="width: 195px"></td>
                        <td style="width: 180px"></td>
                    </tr>
                    <tr style="height:25">
                        <td style="width: 50px" align="center">
                            ค้นหา</td>
                        <td style="width: 90px" align="right">
                            ประเภทสินค้า :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="width: 80px" align="right">
                            กลุ่มสินค้า :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td style="width: 65px" align="right">
                            สินค้า :&nbsp;</td>
                        <td style="width: 195px">
                            <asp:TextBox ID="txtProductKey" runat="server" CssClass="zTextbox" Width="170px"></asp:TextBox></td>
                        <td style="width: 180px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /> 
                        </td>
                    </tr>
                    <tr style="height:10">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 90px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 65px">
                        </td>
                        <td style="width: 195px">
                        </td>
                        <td style="width: 180px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height:10px">
            <td></td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <asp:LinkButton ID="btnNewAll" runat="server" CssClass="toolbarbutton" OnClick="btnNewAll_Click" >LinkButton</asp:LinkButton>
                <uc1:ToolbarControl ID="ctlToolbarItem" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="เพิ่มสินค้า" NameBtnDelete="ลบสินค้า"
                    OnDeleteClick="DeleteClick" OnNewClick="NewClick"/>
                <asp:TextBox ID="txtProduct" runat="server" CssClass="zHidden" Width="170px"></asp:TextBox></td> 
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grvPlanitem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="1000px" OnRowDataBound="grvPlanitem_RowDataBound" OnRowCommand="grvPlanitem_RowCommand" >
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
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "สินค้า" DataField="PRODUCTNAME">
                        </asp:BoundField>
                        <asp:BoundField SortExpression="TYPENAME" HeaderText = "ประเภทสินค้า" DataField="TYPENAME">
                            <ItemStyle Width="120px" />
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField>
                        <asp:TemplateField HeaderText = "ม.ค." SortExpression="M1" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn1" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M1")) ==0 ? space : Convert.ToDouble(Eval("M1")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ก.พ." SortExpression="M2" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn2" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M2")) ==0 ? space : Convert.ToDouble(Eval("M2")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "มี.ค." SortExpression="M3" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn3" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M3")) ==0 ? space : Convert.ToDouble(Eval("M3")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "เม.ย." SortExpression="M4" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn4" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M4")) ==0 ? space : Convert.ToDouble(Eval("M4")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "พ.ค." SortExpression="M5" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn5" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M5")) ==0 ? space : Convert.ToDouble(Eval("M5")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "มิ.ย." SortExpression="M6" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn6" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M6")) ==0 ? space : Convert.ToDouble(Eval("M6")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ก.ค." SortExpression="M7" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn7" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M7")) ==0 ? space : Convert.ToDouble(Eval("M7")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ส.ค." SortExpression="M8" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn8" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M8")) ==0 ? space : Convert.ToDouble(Eval("M8")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ก.ย." SortExpression="M9" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn9" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M9")) ==0 ? space : Convert.ToDouble(Eval("M9")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ต.ค." SortExpression="M10" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn10" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M10")) ==0 ? space : Convert.ToDouble(Eval("M10")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "พ.ย." SortExpression="M11" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn11" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M11")) ==0 ? space : Convert.ToDouble(Eval("M11")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ธ.ค." SortExpression="M12" >
                            <ItemStyle Width="60px" HorizontalAlign="right"/>
                            <HeaderStyle Width="60px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn12" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("M12")) ==0 ? space : Convert.ToDouble(Eval("M12")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>