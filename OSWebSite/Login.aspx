<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OSWebSite.Login" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent2" runat="server">
<div class="content">
    <div class="login">
                    <h1>
                       Login
                    </h1>
                    </br>
                    <span class="notificacaoErro">
                        <asp:Literal ID="Erro" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginValidationSummary" runat="server" CssClass="notificacaoErro" 
                         ValidationGroup="LoginValidationGroup"/>
                    <div class="center">
                      <fieldset style="text-align: center; display: inline-block">
                            <p>
                                <asp:Label ID="LoginLabel" runat="server" AssociatedControlID="txtLogin" >Login:</asp:Label>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="inputTexto" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtLogin" 
                                     CssClass="notificacaoErro" ErrorMessage="Login é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="LoginValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtSenha">Password:</asp:Label>
                                <asp:TextBox ID="txtSenha" runat="server" CssClass="inputSenha" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SenhaRequired" runat="server" ControlToValidate="txtSenha" 
                                     CssClass="notificacaoErro" ErrorMessage="Senha é obrigatória." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="LoginValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                           </fieldset>
                    </div>
                        <p class="submitButton">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                                 ValidationGroup="LoginValidationGroup" CssClass="button" 
                                onclick="btnLogin_Click" />
                        </p>
    </div>
</div>
</asp:Content>
