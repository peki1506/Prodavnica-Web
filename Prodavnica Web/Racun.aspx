<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Racun.aspx.cs" Inherits="Prodavnica_Web.Racun" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/StyleSheetRacun.css" />
    <script src="JS/JavaScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" OnRowDeleting="GridView1_RowDeleting" DataKeyNames ="ukupno,datum,popust,ID,ID1" AutoGenerateColumns="False" runat="server">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="ukupno" HeaderText="Ukupno" Visible="false" SortExpression="ukupno" />
            <asp:BoundField DataField="popust" HeaderText="Popust" Visible="false" SortExpression="popust" />
            <asp:BoundField DataField="datum" HeaderText="Datum" Visible="false" SortExpression="datum" />
            <asp:BoundField DataField="ID1" HeaderText="ID1" Visible="false" SortExpression="id1" />
            <asp:BoundField DataField="naziv" HeaderText="Naziv" SortExpression="naziv" />
            <asp:BoundField DataField="brend" HeaderText="Brend" SortExpression="brend" />
            <asp:BoundField DataField="velicina" HeaderText="Veličina" SortExpression="velicina" />
            <asp:BoundField DataField="boja" HeaderText="Boja" SortExpression="boja" />
            <asp:BoundField DataField="cena" HeaderText="Cena" SortExpression="cena" />
            <asp:BoundField DataField="kolicina" HeaderText="Količina" SortExpression="kolicina" />  
            <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Obrisi artikal" ShowHeader="True" Text="Obrisi" />
        </Columns>
    </asp:GridView>
    <div class="ukupnoDiv">
        <div class="label">Ukupan Iznos:</div>
        <div id="ukupno" class="template" runat="server"></div>
    </div>
    <div class="vremeDiv">
        <div class="label">Vreme kupovine:</div>
        <div id="vreme" class="template" runat="server"></div>
    </div>
    <div class="popustDiv">
        <div class="label">Popust:</div>
        <div id="popust" class="template" runat="server"></div>
    </div>
    <asp:Button ID="Btn_Plati" OnClick="Btn_Plati_Click" runat="server" Text="Plati" />
</asp:Content>
