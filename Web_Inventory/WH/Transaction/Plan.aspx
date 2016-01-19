<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="WH_Transaction_Plan" Title="แผนการสั่งวัตถุดิบ" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;แผนการสั่งวัตถุดิบ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnSubmit="ยืนยัน" NameBtnPrint="พิมพ์รายงาน"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnPrintClick="PrintClick" NameBtnCancel="ยกเลิกแผน" />
                <asp:LinkButton ID="btnCalculate" runat="server" CssClass="toolbarbutton" OnClick="btnCalculate_Click">LinkButton</asp:LinkButton></td> 
        </tr>
        <tr height="10px">
            <td style="height: 10px">
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1550" class="searchTable">
                    <tr height="10px">
                        <td style="width: 25px;"></td>
                        <td style="width: 75px;">
                            </td>
                        <td style="width: 105px;"></td>
                        <td style="width: 75px;"></td>
                        <td style="width: 85px;"></td>
                        <td style="width: 75px"></td>
                        <td style="width: 120px"></td>
                        <td style="width: 75px"></td>
                        <td style="width: 910px"></td>
                    </tr> 
                    <tr height="25px">
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
                        <td style="width: 910px">
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox>
                            <asp:TextBox ID="txtPlan" runat="server" CssClass="zHidden" MaxLength="4" Width="60px"></asp:TextBox></td>
                    </tr> 
                    <tr height="25">
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
                        <td style="width: 910px">
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="zTextbox-View" Width="100px" ReadOnly="True"></asp:TextBox></td>
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
                        <td style="width: 910px"></td>
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
                <table border="0" cellpadding="0" cellspacing="0" width="1550" class="searchTable">
                    <tr height="10px">
                        <td style="width: 50px;"></td>
                        <td style="width: 95px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 90px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 65px"></td>
                        <td style="width: 195px"></td>
                        <td style="width: 730px"></td>
                    </tr>
                    <tr height="25">
                        <td align="center" style="width: 50px">
                            ค้นหา</td>
                        <td align="right" style="width: 95px">
                            เดือน :&nbsp;
                        </td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbMonth" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td align="right" style="width: 90px">
                            สถานะวัตถุดิบ :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductStatus" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td align="right" style="width: 65px">
                        </td>
                        <td style="width: 195px">
                        </td>
                        <td style="width: 730px">
                        </td>
                    </tr>
                    <tr height="25">
                        <td style="width: 50px" align="center">
                            </td>
                        <td style="width: 95px" align="right">
                            ประเภทวัตถุดิบ :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="width: 90px" align="right">
                            กลุ่มวัตถุดิบ :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td style="width: 65px" align="right">
                            วัตถุดิบ :&nbsp;</td>
                        <td style="width: 195px">
                            <asp:TextBox ID="txtProductKey" runat="server" CssClass="zTextbox" Width="170px"></asp:TextBox></td>
                        <td style="width: 730px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /> 
                        </td>
                    </tr>
                    <tr height="10">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 95px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 90px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 65px">
                        </td>
                        <td style="width: 195px">
                        </td>
                        <td style="width: 730px">
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
                <asp:GridView ID="grvPlanItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="1550px" OnRowDataBound="grvPlanItem_RowDataBound" OnRowCommand="grvPlanItem_RowCommand" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="วัตถุดิบ">
                            <ItemTemplate>
                                <asp:Label ID="lblProductName" runat="server" Text = '<%# Bind("PRODUCTNAME") %>'></asp:Label><br />
                                &nbsp;&nbsp;-&nbsp;Min&nbsp;:&nbsp;<asp:Label ID="lblMin" runat="server" Text = '<%# Convert.IsDBNull(Eval("MINIMUM")) ? "-" : Convert.ToDouble(Convert.IsDBNull(Eval("MINIMUM")) ? 0 : Eval("MINIMUM")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label><br />
                                &nbsp;&nbsp;-&nbsp;Max&nbsp;:&nbsp;<asp:Label ID="lblMax" runat="server" Text = '<%# Convert.IsDBNull(Eval("MAXIMUM")) ? "-" : Convert.ToDouble(Convert.IsDBNull(Eval("MAXIMUM")) ? 0 : Eval("MAXIMUM")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label><br />
                                &nbsp;&nbsp;-&nbsp;Lot Size&nbsp;:&nbsp;<asp:Label ID="lblLotSize" runat="server" Text = '<%# Convert.IsDBNull(Eval("LOTSIZE")) ? "-" : Convert.ToDouble(Convert.IsDBNull(Eval("LOTSIZE")) ? 0 : Eval("LOTSIZE")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label><br />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="top" /> 
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="TYPE" DataField="TYPE">
                            <ItemStyle Width="60px" Height="20px" />
                            <HeaderStyle Width="60px" Height="20px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText = "1" SortExpression="DAY1" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn1" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY1")) ==0 ? space : Convert.ToDouble(Eval("DAY1")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "2" SortExpression="DAY2" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn2" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY2")) ==0 ? space : Convert.ToDouble(Eval("DAY2")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "3" SortExpression="DAY3" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn3" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY3")) ==0 ? space : Convert.ToDouble(Eval("DAY3")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "4" SortExpression="DAY4" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn4" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY4")) ==0 ? space : Convert.ToDouble(Eval("DAY4")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "5" SortExpression="DAY5" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn5" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY5")) ==0 ? space : Convert.ToDouble(Eval("DAY5")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "6" SortExpression="DAY6" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn6" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY6")) ==0 ? space : Convert.ToDouble(Eval("DAY6")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "7" SortExpression="DAY7" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn7" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY7")) ==0 ? space : Convert.ToDouble(Eval("DAY7")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "8" SortExpression="DAY8" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn8" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY8")) ==0 ? space : Convert.ToDouble(Eval("DAY8")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "9" SortExpression="DAY9" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn9" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY9")) ==0 ? space : Convert.ToDouble(Eval("DAY9")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "10" SortExpression="DAY10" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn10" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY10")) ==0 ? space : Convert.ToDouble(Eval("DAY10")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "11" SortExpression="DAY11" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn11" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY11")) ==0 ? space : Convert.ToDouble(Eval("DAY11")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "12" SortExpression="DAY12" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn12" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY12")) ==0 ? space : Convert.ToDouble(Eval("DAY12")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "13" SortExpression="DAY13" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn13" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY13")) ==0 ? space : Convert.ToDouble(Eval("DAY13")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "14" SortExpression="DAY14" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn14" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY14")) ==0 ? space : Convert.ToDouble(Eval("DAY14")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "5" SortExpression="DAY15" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn15" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY15")) ==0 ? space : Convert.ToDouble(Eval("DAY15")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "16" SortExpression="DAY16" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn16" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY16")) ==0 ? space : Convert.ToDouble(Eval("DAY16")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "17" SortExpression="DAY17" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn17" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY17")) ==0 ? space : Convert.ToDouble(Eval("DAY17")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "18" SortExpression="DAY18" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn18" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY18")) ==0 ? space : Convert.ToDouble(Eval("DAY18")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "19" SortExpression="DAY19" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn19" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY19")) ==0 ? space : Convert.ToDouble(Eval("DAY19")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "20" SortExpression="DAY20" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn20" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY20")) ==0 ? space : Convert.ToDouble(Eval("DAY20")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "21" SortExpression="DAY21" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn21" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY21")) ==0 ? space : Convert.ToDouble(Eval("DAY21")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "22" SortExpression="DAY22" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn22" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY22")) ==0 ? space : Convert.ToDouble(Eval("DAY22")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "23" SortExpression="DAY23" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn23" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY23")) ==0 ? space : Convert.ToDouble(Eval("DAY23")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "24" SortExpression="DAY24" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn24" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY24")) ==0 ? space : Convert.ToDouble(Eval("DAY24")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "25" SortExpression="DAY25" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn25" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY25")) ==0 ? space : Convert.ToDouble(Eval("DAY25")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "26" SortExpression="DAY26" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn26" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY26")) ==0 ? space : Convert.ToDouble(Eval("DAY26")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "27" SortExpression="DAY27" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn27" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY27")) ==0 ? space : Convert.ToDouble(Eval("DAY27")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "28" SortExpression="DAY28" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn28" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY28")) ==0 ? space : Convert.ToDouble(Eval("DAY28")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "29" SortExpression="DAY29" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn29" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY29")) ==0 ? space : Convert.ToDouble(Eval("DAY29")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "30" SortExpression="DAY30" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn30" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY30")) ==0 ? space : Convert.ToDouble(Eval("DAY30")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "31" SortExpression="DAY31" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:LinkButton ID="btn31" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY31")) ==0 ? space : Convert.ToDouble(Eval("DAY31")).ToString(ABB.Data.Constz.IntFormat) %>' />
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