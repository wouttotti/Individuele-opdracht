<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Website2.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label>
&nbsp;&nbsp;
    <asp:TextBox ID="TbNaam" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TbEmail" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Geboortedatum"></asp:Label>
    <asp:Calendar ID="cGeboortedatum" runat="server" DayNameFormat="Shortest"></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Wachtwoord"></asp:Label>
&nbsp;&nbsp;
    <asp:TextBox ID="TbWachtwoord" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="BtnRegister" runat="server" Text="Registreren" OnClick="BtnRegister_Click" />
    <asp:Label ID="LbError" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>
