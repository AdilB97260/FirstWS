<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Processing.aspx.cs" Inherits="Processing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your payment is processing...</title>
   <%-- <link rel="shortcut icon" href="https://fredforfirst.com/wp-content/uploads/2019/07/fff.ico" />--%>
</head>
<body onload="myFunction()" style="margin: 0;">
    <form id="form1" runat="server">

        <div style="position: absolute; top: 24%; left: 30%; width: 40%; margin: 0px 0px 0px 0px; text-align: center">
            <span>
                <img src="/images/processing.gif" />
            </span>
            <br />
            <span>Your Payment is being processed... do not refresh this page</span>
        </div>
    </form>
</body>
    <script>
        document.ready(window.setTimeout(location.href = "http://parishraffle.com/process.aspx", 1000));
        //document.ready(window.setTimeout(location.href = "/process.aspx", 1000));
    </script>

</html>
