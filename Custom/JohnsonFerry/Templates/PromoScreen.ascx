<%@ Control Language="c#" Inherits="ArenaWeb.Templates.PromoScreen" CodeFile="~/Custom/JohnsonFerry/Templates/PromoScreen.ascx.cs" %>

<style type="text/css">
    body,html{margin:0;padding:0;background-color:#000000;}
</style>

<script type="text/javascript">
function refreshAt(hours, minutes, seconds) {
    var now = new Date();
    var then = new Date();

    if (now.getHours() > hours ||
   (now.getHours() == hours && now.getMinutes() > minutes) ||
    now.getHours() == hours && now.getMinutes() == minutes && now.getSeconds() >= seconds) {
        then.setDate(now.getDate() + 1);
    }
    then.setHours(hours);
    then.setMinutes(minutes);
    then.setSeconds(seconds);

    var timeout = (then.getTime() - now.getTime());
    setTimeout(function() { window.location.reload(true); }, timeout);
}

refreshAt(3, 00, 0); //Will refresh the page at 3:00am
refreshAt(9, 00, 0); //Will refresh the page at 9:00am
refreshAt(11, 00, 0); //Will refresh the page at 11:00am
refreshAt(12, 30, 0); //Will refresh the page at 12:30am
refreshAt(17, 30, 0); //Will refresh the page at 5:30am
refreshAt(20, 00, 0); //Will refresh the page at 8:00pm
</script>

<asp:PlaceHolder ID="MainPH" Runat="server"></asp:PlaceHolder>
