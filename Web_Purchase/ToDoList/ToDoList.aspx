<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="ToDoList_ToDoList" Title="ToDoList" %>


<%@ Register Src="Controls/ProductPurchaseList.ascx" TagName="ProductPurchaseListControl" TagPrefix="uc4" %>
<%@ Register Src="Controls/ProductReceiveList.ascx" TagName="ProductReceiveListControl" TagPrefix="uc5" %>
<%@ Register Src="Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;To Do List</td> 
        </tr> 
        <tr style="height:10px">
            <td>
            </td> 
        </tr> 
        <tr style="height:25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="810px">
                    <tr>
                        <td>
                            <uc3:TabControl id="ctlTab" runat="server" OnSelectedChange="ctlTab_SelectedChange">
                            </uc3:TabControl></td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="5px"></td>  
                                    <td>
                                        <uc4:ProductPurchaseListControl ID="ctlProductPurchaseList" runat="server" />
                                        <uc5:ProductReceiveListControl ID="ctlProductReceiveList" runat="server" Visible="false" />
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
