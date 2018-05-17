<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="Label13" runat="server" Text="Welcome to this music recommendation engine! To use this engine enter the names of up to five musicians below that share some qualities. Then the engine will pick out which qualities are shared across all of the artists you entered and provide recommendations based on that."></asp:Label><br />
    
    <asp:Label ID="Label11" runat="server" Text="how obscure do you want the recommendation to be?"></asp:Label><br />
    <!--lets user configure results -->
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem>somewhat obscure</asp:ListItem>
        <asp:ListItem>well known</asp:ListItem>
        <asp:ListItem>very obscure</asp:ListItem>
    </asp:DropDownList>

    <!--labels used to enter user data-->
    <div>
        <div>
            <asp:Label ID="artist1LBL" runat="server" Text="Label">artist 1:</asp:Label><br>
            <asp:TextBox ID="artist1TXT" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="artist2LBL" runat="server" Text="Label">artist 2:</asp:Label><br>
            <asp:TextBox ID="artist2TXT" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="artist3LBL" runat="server" Text="Label">artist 3:</asp:Label><br>
            <asp:TextBox ID="artist3TXT" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="artist4LBL" runat="server" Text="Label">artist 4:</asp:Label><br>
            <asp:TextBox ID="artist4TXT" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="artist5LBL" runat="server" Text="Label">artist 5:</asp:Label><br>
            <asp:TextBox ID="artist5TXT" runat="server"></asp:TextBox>
        </div>
        <asp:Button ID="recommendationButton" runat="server" Text="generate recommendations" OnClick="recommendationButton_Click" /><br />
        <!-- first 5 labels show what tags belong to each artist -->
        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label2" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label3" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label4" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label5" runat="server" Text=" "></asp:Label><br /><br />
        <asp:Label ID="Label12" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="Label6" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label7" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label8" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label9" runat="server" Text=" "></asp:Label><br />
        <asp:Label ID="Label10" runat="server" Text=" "></asp:Label><br />


    </div>
</asp:Content>



