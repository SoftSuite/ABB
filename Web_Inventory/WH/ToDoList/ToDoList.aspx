<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="WH_ToDoList_ToDoList" Title="ToDoList" %>

<%@ Register Src="Controls/MinStockControl.ascx" TagName="MinStockControl" TagPrefix="uc4" %>
<%@ Register Src="Controls/StockInControl.ascx" TagName="StockInControl" TagPrefix="uc5" %>
<%@ Register Src="Controls/StockOutControl.ascx" TagName="StockOutControl" TagPrefix="uc6" %>
<%@ Register Src="Controls/ExpireControl.ascx" TagName="ExpireControl" TagPrefix="uc7" %>
<%@ Register Src="Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="~/Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;To Do List</td> 
        </tr> 
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="810px">
                    <tr>
                        <td>
                            &nbsp;<uc3:TabControl ID="ctlTab" runat="server" OnSelectedChange="ctlTab_SelectedChange"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="5px"></td>  
                                    <td>
                                        <uc4:MinStockControl ID="ctlMinStock" runat="server" />
                                        <uc5:StockInControl ID="ctlStockIn" runat="server" Visible="false" />
                                        <uc6:StockOutControl ID="ctlStockOut" runat="server" Visible="false" />
                                        <uc7:ExpireControl ID="ctlExpire" runat="server" Visible="false"/>
                                    </td> 
                                    <td width="5px"></td>  
                                </tr> 
                            </table>
                        </td> 
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>