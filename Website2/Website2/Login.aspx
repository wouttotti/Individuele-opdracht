<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Website2.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:TextBox ID="TbEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Wachtwoord"></asp:Label>

    &nbsp;

    <asp:TextBox ID="TbWachtwoord" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="BtnInlog" runat="server" Text="Inloggen" OnClick="BtnInlog_Click" />
<asp:Label ID="LbError" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="BtnUitlog" runat="server" OnClick="BtnUitlog_Click" Text="Uitloggen" Visible="False" />
</asp:Content>
