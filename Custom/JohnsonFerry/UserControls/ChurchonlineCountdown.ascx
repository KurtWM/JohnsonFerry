<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChurchonlineCountdown.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.ChurchonlineCountdown" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<script type="text/javascript">
  countdowntimer('<%# NextEventUrlSetting %>', '<%# CountdownIdSetting %>', '<%# CountdownContainerSetting %>', '<%# LinkUrlSetting %>');
</script>

