<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionSlider.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.PromotionSlider" %>

<script src="/Custom/JohnsonFerry/Scripts/jquery.cycle.all.js" type="text/javascript"></script>

<script src="/Custom/JohnsonFerry/Scripts/countdownTimer.js" type="text/javascript"></script>

<style>
#churchonline_counter {
    overflow: auto;
    padding: 10px;
    margin: 0 auto 0 auto;
    display: none;
}

#churchonline_counter .description, #churchonline_counter .time li .label {
    font-size: 0.8em;
}
#churchonline_counter .time {
    list-style: none;
    padding: 0;
    margin: 10px 0 0 0;
}

#churchonline_counter .time li {
    float: left;
    padding: 0 10px;
    text-align: center;
}
#churchonline_counter .time li:first-child {
    padding-left: 0;
}
#churchonline_counter .time li span {
    font-size: 1.2em;
}
#churchonline_counter .live {
    display: none;
    font-weight: bold;
    text-transform: uppercase;
    color: #43a800;
    font-size: 14;
    text-align: center;
}
</style>

<asp:PlaceHolder ID="phSlider" runat="server"></asp:PlaceHolder>

