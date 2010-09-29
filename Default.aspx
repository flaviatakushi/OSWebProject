<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="content">
    <form runat="server" id="form1">
       <table style="width: 100%;">
        <tr>
            <td>
               Texto:
            </td>
            <td>
                <input id="Text1" type="text" /></td>
        </tr>
        <tr>
            <td>
               Texto: <span>teste</span>
            </td>
            <td>
                <input id="Text2" type="text" /></td>
        </tr>
        <tr>
            <td>
                Botao</td>
            <td>
                <input id="Submit1" type="submit" value="submit" /></td>

        </tr>
        <tr>
            <td>
                seletor:             </td>
            <td>
                <input id="Radio1" type="radio" /><input id="Checkbox1" type="checkbox" /><select 
                    id="Select1" name="D1">
                    <option></option>
                </select></td>

        </tr>
    </table>
      </form>
    </div>
        
</asp:Content>

