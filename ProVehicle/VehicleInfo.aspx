<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehicleInfo.aspx.cs" Inherits="ProVehicle.VehicleInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
    <title></title>  
    <style type="text/css">  
        body  
        {  
            font-family: Arial;  
            font-size: 10pt;  
        }  
        table  
        {  
            border: 1px solid #ccc;  
            border-collapse: collapse;  
            background-color: #fff;  
        }  
        table th  
        {  
            background-color: #ff7f00;  
            color: #fff;  
            font-weight: bold;  
        }  
        table th, table td  
        {  
            padding: 5px;  
            border: 1px solid #ccc;  
        }  
        table, table table td  
        {  
            border: 0px solid #ccc;  
        }  
        .button {  
    background-color: #0094ff; /* Blue */  
    border: none;  
    color: white;  
    padding: 15px 32px;  
    text-align: center;  
    text-decoration: none;  
    display: inline-block;  
    font-size: 16px;  
}  
    </style> 
    
    <script type ="text/javascript" >
        function showButton() {
            document.getElementById("<%=btnUpload.ClientID%>").style.display = "";
        }
    </script>
</head> 
<body>
    <form id="form1" runat="server">  
    <asp:FileUpload ID="FileUpload1"  CssClass="button"  runat="server" onchange="showButton()"/>  
    <asp:Button ID="btnUpload" CssClass="button" runat="server" Text="Upload" OnClick="UploadCSV" style="display:none" />  
    <hr />  
    <asp:GridView ID="GridView1" runat="server">  
    </asp:GridView>  
        <br />
        <br />
        <asp:Label ID="lblPopularVehicle" runat="server"></asp:Label>
    </form> 
</body>
</html>
