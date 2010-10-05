<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CadastroUsuario.aspx.cs" Inherits="OSWebSite.CadastroUsuario" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">
    <div>
                    <h1>
                       Cadastrar Usuário
                    </h1>
                    </br>
                    <p>
                        A senha deve ter no mínimo de <%= "x"  %> carácteres.
                    </p>
                    <span class="notificacaoErro">
                        <asp:Literal ID="Erro" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="CadastroUsuarioValidationSummary" runat="server" CssClass="notificacaoErro" 
                         ValidationGroup="CadastroUsuarioValidationGroup"/>
                      <fieldset class="cadastro">
                            <p>
                                <asp:Label ID="LoginLabel" runat="server" AssociatedControlID="txtLogin">Login:</asp:Label>
                                <asp:TextBox ID="txtLogin" runat="server" CssClass="inputTexto" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtLogin" 
                                     CssClass="notificacaoErro" ErrorMessage="Login é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="NomeCompletoLabel" runat="server" AssociatedControlID="txtNomeCompleto">Nome Completo:</asp:Label>
                                <asp:TextBox ID="txtNomeCompleto" runat="server" CssClass="inputTexto" 
                                    MaxLength="80"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NomeCompletoRequired" runat="server" ControlToValidate="txtNomeCompleto" 
                                     CssClass="notificacaoErro" ErrorMessage="Nome Completo é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="CpfLabel" runat="server" AssociatedControlID="txtCpf">CPF:</asp:Label>
                                <asp:TextBox ID="txtCpf" runat="server" CssClass="inputTexto"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CpfRequired" runat="server" ControlToValidate="txtCpf" 
                                     CssClass="notificacaoErro" ErrorMessage="CPF é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="CpfValidator" runat="server" ControlToValidate="txtCpf" 
                                     CssClass="notificacaoErro" ErrorMessage="CPF inválido." 
                                     ValidationGroup="CadastroUsuarioValidationGroup" Display="Dynamic" 
                                    ValidationExpression="(^\d{3}\.\d{3}\.\d{3}-\d{2})|(^\d{3}\d{3}\d{3}\d{2})$"></asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="txtEmail">E-mail:</asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="inputTexto"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail" 
                                     CssClass="notificacaoErro" ErrorMessage="E-mail é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="EmailValidator" runat="server" ControlToValidate="txtEmail" 
                                     CssClass="notificacaoErro" ErrorMessage="Email inválido." 
                                     ValidationGroup="CadastroUsuarioValidationGroup" Display="Dynamic" 
                                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>

                            </p>
                            <p>
                                <asp:Label ID="DtNascimentoLabel" runat="server" AssociatedControlID="txtDtNascimento">Data de Nascimento:</asp:Label>
                                <asp:TextBox ID="txtDtNascimento" runat="server" CssClass="inputTexto" 
                                    MaxLength="9"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtDtNascimento_MaskedEditExtender" runat="server" 
                                    ClipboardText="Por segurança o Ctrl+V não funcionará." Mask="99/99/9999" 
                                    MaskType="Date" TargetControlID="txtDtNascimento" 
                                    UserDateFormat="DayMonthYear" ClipboardEnabled="False">
                                </asp:MaskedEditExtender>
                                <asp:RequiredFieldValidator ID="DtNascimentoRequired" runat="server" ControlToValidate="txtDtNascimento" 
                                     CssClass="notificacaoErro" ErrorMessage="Data de Nascimento é obrigatório." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="DtNascimentoValidator" runat="server" ControlToValidate="txtDtNascimento" 
                                     CssClass="notificacaoErro" ErrorMessage="Dada de Nascimento inválida." 
                                     ValidationGroup="CadastroUsuarioValidationGroup" Display="Dynamic" 
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <asp:Label ID="SexoLabel" runat="server" AssociatedControlID="ddlSexo">Sexo:</asp:Label>
                                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="dropdownlist" 
                                    Width="140px">
                                    <asp:ListItem Value="1" Selected="True">Mascullino</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="False">Feminino</asp:ListItem>
                                </asp:DropDownList>
                                
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtSenha">Password:</asp:Label>
                                <asp:TextBox ID="txtSenha" runat="server" CssClass="inputSenha" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SenhaRequired" runat="server" ControlToValidate="txtSenha" 
                                     CssClass="notificacaoErro" ErrorMessage="Senha é obrigatória." ToolTip="Campo Obrigatório" 
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="txtConfirmaSenha">Confirm Password:</asp:Label>
                                <asp:TextBox ID="txtConfirmaSenha" runat="server" CssClass="inputSenha" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtConfirmaSenha" CssClass="notificacaoErro" Display="Dynamic" 
                                     ErrorMessage="Confirmação de senha é obrigatória." ID="ConfirmaSenhaRequired" runat="server" 
                                     ToolTip="Campo Obrigatório" ValidationGroup="CadastroUsuarioValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="ComparacaoSenha" runat="server" ControlToCompare="txtSenha" ControlToValidate="txtConfirmaSenha" 
                                     CssClass="notificacaoErro" Display="Dynamic" ErrorMessage="Senhas não conferem, devem ser iguais."
                                     ValidationGroup="CadastroUsuarioValidationGroup">*</asp:CompareValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CadastrarUsuario" runat="server" CommandName="MoveNext" Text="Cadastrar Usuário" 
                                 ValidationGroup="CadastroUsuarioValidationGroup" CssClass="button" />
                        </p>
    </div>
</div>
</asp:Content>
