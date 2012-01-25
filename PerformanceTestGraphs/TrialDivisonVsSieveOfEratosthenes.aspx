<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrialDivisonVsSieveOfEratosthenes.aspx.cs" Inherits="PerformanceTestGraphs.TrialDivisonVsSieveOfEratosthenes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trial Division Vs Sieve of Eratosthenes</title>        
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.flot.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/TrialDivisonVsSieveOfEratosthenes.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <link href="Styles/main.css" rel="stylesheet" type="text/css" />    
</head>
<body class="sunny">
    <form id="form1" runat="server">
        <div class="center">
            <h1>Trial Division Vs Sieve of Eratosthenes</h1>
        
            <label for="tags">Find all primes less than or equal to </label>
	        <input id="limit" value="5000"/> 
            <button id="test">Test</button>
            <img id="waitWheel" src="Styles/images/wait-wheel.gif" alt="processing..."/>
            <div class="message">
                Trial Divison : Time taken = <label id="tdTimeTaken">-</label> ms | Last Prime Found = <label id="tdLastPrimeFound">-</label> <br />
                Sieve of Eratosthenes : Time taken = <label id="soeTimeTaken">-</label> ms | Last Prime Found = <label id="soeLastPrimeFound">-</label>
            </div>
            <div class="chartWrapper">
                <div class="y-label">
                    <img src="Styles/images/y-label.png" alt="y axis: Time taken in milliseconds to find (x) primes"/>
                </div>
                <div id="comparisonChart" class="chart"></div>
                <span>x axis : Number of prime numbers found</span>
            </div>                                    
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Services/SieveOfEratosthenes.svc" />
                <asp:ServiceReference Path="~/Services/TrialDivision.svc" />
            </Services>
        </asp:ScriptManager>
    </form>
</body>
</html>
