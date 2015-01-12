<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Zoeken.aspx.cs" Inherits="Website2.Zoeken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:TextBox ID="TbSearch" runat="server"></asp:TextBox>
<asp:DropDownList ID="DdlCategorie" runat="server">
    <asp:ListItem>Anime</asp:ListItem>
    <asp:ListItem>Manga</asp:ListItem>
    <asp:ListItem>Personage</asp:ListItem>
</asp:DropDownList>
    <br />
    <asp:Button ID="BtnZoeken" runat="server" OnClick="BtnZoeken_Click" Text="Zoeken" />
    <asp:Label ID="LbErrorZoek" runat="server" Font-Size="Large" Text="Label" Visible="False"></asp:Label>
    <br />
<asp:GridView ID="GridViewItems" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#E9E7E2" />
    <SortedAscendingHeaderStyle BackColor="#506C8C" />
    <SortedDescendingCellStyle BackColor="#FFFDF8" />
    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
</asp:GridView>
    <br />
    <asp:Label ID="LbErrorToevoeg" runat="server" Font-Size="Large" Text="Label" Visible="False"></asp:Label>
    <br />
    <br />
<asp:Button ID="BtnVoegToe" runat="server" Text="Toevoegen aan lijst" OnClick="BtnVoegToe_Click" Visible="False" />
</asp:Content>

