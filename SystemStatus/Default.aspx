<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SystemStatus._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="container">

        <div>
            <h4 style="text-align:center; margin-top:25px;">
                System Status 
                <br />
                <progress value="0" max="10" id="progressBar"></progress>
            </h4>
        </div>
        <center>
            <asp:GridView ID="GridView1" runat="server" CssClass="Table table-sm gridview" HeaderStyle-BackColor="#CCCCCC" HeaderStyle-ForeColor="Black" OnRowDataBound="onRowBound">
            </asp:GridView>
        </center>
    </div>

    <script type="text/javascript">
        var timeleft = 10;
        var downloadTimer = setInterval(function () {
            if (timeleft <= 0) {
                clearInterval(downloadTimer);
            }
            document.getElementById("progressBar").value = 10 - timeleft;
            console.log(timeleft);
            timeleft -= 1;
        }, 30000);

        setTimeout(function () {
            window.location.reload(1);
        }, timeleft * 30000);
    </script>
</asp:Content>
