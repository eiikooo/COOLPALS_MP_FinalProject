<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSkills.aspx.cs" Inherits="COOLPALS_MP_FinalProject.ManageSkills" %>
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PairEd - Manage Skills</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Sora:wght@300;400;600;700;800&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet" />
    <style>
        *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

        html, body {
            height: 100%;
            font-family: 'DM Sans', sans-serif;
            background: #0D1B3E;
            color: #0D1B3E;
        }

        /* NAV */
        .nav {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 72px;
            height: 78px;
            background: #0D1B3E;
            border-bottom: 1px solid rgba(255,255,255,0.07);
        }
        .nav-brand {
            display: flex;
            align-items: center;
            gap: 16px;
        }
        .nav-logo { height: 52px; width: auto; }
        .nav-site-name {
            font-family: 'Sora', sans-serif;
            font-weight: 700;
            font-size: 1.55rem;
            color: #fff;
            letter-spacing: -0.3px;
        }
        .nav-site-name span { color: #CC0000; }
        .nav-tag {
            font-family: 'Sora', sans-serif;
            font-size: 0.82rem;
            color: rgba(255,255,255,0.32);
            letter-spacing: 0.12em;
            text-transform: uppercase;
            font-weight: 500;
        }

        /* PAGE BODY */
        .page-body {
            min-height: calc(100vh - 78px);
            background: #F5F4F0;
            padding: 52px 72px;
        }

        /* PAGE HEADER */
        .page-header {
            margin-bottom: 36px;
        }
        .page-header-accent {
            width: 44px;
            height: 4px;
            background: #CC0000;
            border-radius: 2px;
            margin-bottom: 16px;
        }
        .page-header h2 {
            font-family: 'Sora', sans-serif;
            font-weight: 800;
            font-size: 2rem;
            color: #0D1B3E;
            letter-spacing: -0.5px;
            margin-bottom: 6px;
        }
        .page-header p {
            font-size: 0.95rem;
            color: #8A8A8A;
            line-height: 1.65;
        }

        /* DIVIDER */
        .divider {
            height: 1px;
            background: #E0DED8;
            margin-bottom: 32px;
        }

        /* SEARCH BAR */
        .search-bar {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 36px;
        }
        .search-label {
            font-family: 'Sora', sans-serif;
            font-size: 0.75rem;
            font-weight: 700;
            letter-spacing: 0.12em;
            text-transform: uppercase;
            color: #0D1B3E;
            white-space: nowrap;
        }
        .search-input {
            padding: 11px 16px;
            border: 1.5px solid #D5D0C8;
            border-radius: 10px;
            font-family: 'DM Sans', sans-serif;
            font-size: 0.95rem;
            color: #0D1B3E;
            background: #fff;
            outline: none;
            width: 260px;
            transition: border-color 0.2s, box-shadow 0.2s;
        }
        .search-input:focus {
            border-color: #CC0000;
            box-shadow: 0 0 0 3px rgba(204,0,0,0.10);
        }
        .btn-search {
            font-family: 'Sora', sans-serif;
            cursor: pointer;
            border: none;
            font-weight: 700;
            letter-spacing: 0.02em;
            border-radius: 10px;
            background: #0D1B3E;
            color: #fff;
            font-size: 0.92rem;
            padding: 11px 24px;
            transition: background 0.2s;
            white-space: nowrap;
        }
        .btn-search:hover { background: #162348; }

        /* SECTION LABEL */
        .section-label {
            font-family: 'Sora', sans-serif;
            font-size: 0.72rem;
            font-weight: 700;
            color: #AEAAA3;
            text-transform: uppercase;
            letter-spacing: 0.12em;
            margin-bottom: 14px;
            display: block;
        }

        /* GRID TABLE */
        .grid-wrap {
            background: #fff;
            border-radius: 14px;
            border: 1px solid #E0DED8;
            overflow: hidden;
            margin-bottom: 36px;
        }
        .grid-wrap table {
            width: 100%;
            border-collapse: collapse;
            font-size: 0.93rem;
        }
        .grid-wrap table th {
            font-family: 'Sora', sans-serif;
            font-size: 0.70rem;
            font-weight: 700;
            letter-spacing: 0.12em;
            text-transform: uppercase;
            color: #AEAAA3;
            background: #F5F4F0;
            padding: 14px 20px;
            text-align: left;
            border-bottom: 1px solid #E0DED8;
        }
        .grid-wrap table td {
            padding: 14px 20px;
            color: #0D1B3E;
            border-bottom: 1px solid #F0EDE8;
            vertical-align: middle;
        }
        .grid-wrap table tr:last-child td { border-bottom: none; }
        .grid-wrap table tr:hover td { background: #FAF9F7; }

        /* NAV BUTTONS */
        .nav-buttons {
            display: flex;
            gap: 12px;
        }
        .btn-primary {
            font-family: 'Sora', sans-serif;
            cursor: pointer;
            border: none;
            font-weight: 700;
            letter-spacing: 0.02em;
            border-radius: 10px;
            background: #CC0000;
            color: #fff;
            font-size: 0.95rem;
            padding: 13px 28px;
            box-shadow: 0 4px 18px rgba(204,0,0,0.22);
            transition: background 0.2s;
            text-align: center;
            text-decoration: none;
            display: inline-block;
        }
        .btn-primary:hover { background: #A80000; }

        .btn-secondary {
            font-family: 'Sora', sans-serif;
            cursor: pointer;
            border: none;
            font-weight: 700;
            letter-spacing: 0.02em;
            border-radius: 10px;
            background: #0D1B3E;
            color: #fff;
            font-size: 0.95rem;
            padding: 13px 28px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            transition: background 0.2s;
        }
        .btn-secondary:hover { background: #162348; }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <!-- NAV -->
    <div class="nav">
        <div class="nav-brand">
            <img src='../Images/PairEdLogo.png' alt="PairEd Logo" class="nav-logo" />
            <span class="nav-site-name">Pair<span>Ed</span></span>
        </div>
        <span class="nav-tag">Student Skill-Sharing Platform</span>
    </div>

    <div class="page-body">

        <!-- PAGE HEADER -->
        <div class="page-header">
            <div class="page-header-accent"></div>
            <h2>Manage Skills</h2>
            <p>Browse the available skills catalog below.</p>
        </div>

        <div class="divider"></div>

        <!-- SEARCH -->
        <div class="search-bar">
            <asp:Label ID="lblSearch" runat="server" Text="Search Skills:" CssClass="search-label" />
            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="e.g. Mathematics, Guitar..." />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="SearchSkills" CssClass="btn-search" />
        </div>

        <!-- SKILLS GRID -->
        <span class="section-label">Available Skills Catalog</span>
        <div class="grid-wrap">

<asp:GridView ID="gvSkills" runat="server" AutoGenerateColumns="False"
    OnRowCommand="gvSkills_RowCommand"
    GridLines="None">

    <Columns>
        <asp:BoundField DataField="SkillName" HeaderText="Skill" />
        <asp:BoundField DataField="CategoryName" HeaderText="Category" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
    </Columns>

</asp:GridView>
        </div>

        <asp:SqlDataSource ID="SqlDataSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:PairEdDBConnection %>"
            SelectCommand="SELECT s.SkillName AS Skill, sc.CategoryName AS Category, s.Description FROM Skills AS s INNER JOIN SkillCategories AS sc ON s.CategoryID = sc.CategoryID">
        </asp:SqlDataSource>

        <!-- NAVIGATION -->
        <div class="nav-buttons">
            <asp:HyperLink ID="lnkBackProfile" runat="server"
                NavigateUrl="~/Pages/Profile.aspx"
                Text="← Back to My Profile"
                CssClass="btn-secondary" />
            <asp:HyperLink ID="lnkLogout" runat="server"
                NavigateUrl="~/Pages/Logout.aspx"
                Text="Logout"
                CssClass="btn-primary" />
        </div>

    </div>

</form>
</body>
</html>