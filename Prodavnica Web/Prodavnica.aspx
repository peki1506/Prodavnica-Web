<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Prodavnica.aspx.cs" Inherits="Prodavnica_Web.Prodavnica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="CSS/StyleSheetProdavnica.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DropDownList ID="DropDownListPol" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged ="DropDownListPol_SelectedIndexChanged" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownListVrsta" AutoPostBack="true" EnableViewState="true" runat="server"></asp:DropDownList>
</asp:Content>
