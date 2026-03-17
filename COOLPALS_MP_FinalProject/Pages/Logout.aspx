<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="COOLPALS_MP_FinalProject.Logout" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>PairEd - Logout</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Sora:wght@300;400;600;700;800&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet" />
    <style>
        *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

        html, body {
            height: 100%;
            font-family: 'DM Sans', sans-serif;
            background: #0D1B3E;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
        }

        /* CARD */
        .card {
            background: #F5F4F0;
            border-radius: 20px;
            padding: 56px 60px 48px;
            width: 100%;
            max-width: 420px;
            display: flex;
            flex-direction: column;
            align-items: center;
            text-align: center;
            border: 1px solid #E0DED8;
        }

        .card-accent {
            width: 44px;
            height: 4px;
            background: #CC0000;
            border-radius: 2px;
            margin-bottom: 24px;
        }

        .card h2 {
            font-family: 'Sora', sans-serif;
            font-weight: 800;
            font-size: 1.65rem;
            color: #0D1B3E;
            letter-spacing: -0.5px;
            margin-bottom: 10px;
        }

        .card p {
            font-size: 0.95rem;
            color: #8A8A8A;
            line-height: 1.70;
            margin-bottom: 32px;
        }

        /* BUTTONS */
        .btn-primary {
            font-family: 'Sora', sans-serif;
            cursor: pointer;
            border: none;
            width: 100%;
            display: block;
            font-weight: 700;
            letter-spacing: 0.02em;
            border-radius: 10px;
            background: #CC0000;
            color: #fff;
            font-size: 1.02rem;
            padding: 15px;
            box-shadow: 0 4px 18px rgba(204,0,0,0.28);
            margin-bottom: 12px;
            transition: background 0.2s;
            text-align: center;
            text-decoration: none;
        }
        .btn-primary:hover { background: #A80000; }

        .btn-secondary {
            font-family: 'Sora', sans-serif;
            cursor: pointer;
            border: none;
            width: 100%;
            display: block;
            font-weight: 700;
            letter-spacing: 0.02em;
            border-radius: 10px;
            background: #0D1B3E;
            color: #fff;
            font-size: 1.02rem;
            padding: 15px;
            text-align: center;
            text-decoration: none;
            transition: background 0.2s;
        }
        .btn-secondary:hover { background: #162348; }

        /* FOOTER */
        .card-footer {
            margin-top: 32px;
            padding-top: 20px;
            border-top: 1px solid #E0DED8;
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 8px;
        }
        .card-footer img {
            height: 28px;
            width: auto;
            opacity: 0.28;
        }
        .card-footer span {
            font-family: 'Sora', sans-serif;
            font-size: 0.68rem;
            color: #C0BEB9;
            text-align: center;
            line-height: 1.65;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <div class="card">
        <div class="card-accent"></div>

        <h2>You have successfully logged out.</h2>
        <p>Thank you for using PairEd. We hope to see you again soon!</p>

        <%-- Redirect User --%>
        <asp:HyperLink ID="lnkLogin" runat="server"
            NavigateUrl="~/Pages/Login.aspx"
            Text="Login Again"
            CssClass="btn-primary" />

        <asp:HyperLink ID="lnkHome" runat="server"
            NavigateUrl="~/Pages/Default.aspx"
            Text="Return to Home"
            CssClass="btn-secondary" />

        <div class="card-footer">
            <img src='<%= ResolveUrl("~/Images/PairEdLogo.png") %>' alt="PairEd" />
            <span>For enrolled students only<br />Safe &amp; school-monitored<br />© 2025 PairEd · COOLPALS Final Project</span>
        </div>
    </div>

</form>
</body>
</html>