<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CosmosDBdemoOnGoogleMaps._Default" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <cc1:GMap ID="GMap1" runat="server" Width="800px" Height="500px"/>
    </div>
</asp:Content>
