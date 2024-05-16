<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Prodavnica.aspx.cs" Inherits="Prodavnica_Web.Prodavnica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/StyleSheet2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownListPol" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged ="DropDownListPol_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownListVrsta" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="DropDownListVrsta_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownListBrend" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="DropDownListBrend_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="naziv" HeaderText="Naziv" SortExpression="naziv" />
            <asp:BoundField DataField="pol" HeaderText="Pol" SortExpression="pol" />
            <asp:BoundField DataField="brend" HeaderText="Brend" SortExpression="brend" />
            <asp:BoundField DataField="velicina" HeaderText="Veličina" SortExpression="velicina" />
            <asp:BoundField DataField="boja" HeaderText="Boja" SortExpression="boja" />
            <asp:BoundField DataField="cena" HeaderText="Cena" SortExpression="cena" />
            <asp:BoundField DataField="kolicina" HeaderText="Količina" Visible="False" SortExpression="kolicina" />
            <asp:CommandField ShowSelectButton="True"/>
        </Columns>
    </asp:GridView>
</asp:Content>