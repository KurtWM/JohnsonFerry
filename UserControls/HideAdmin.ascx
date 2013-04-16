<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HideAdmin.ascx.cs" Inherits="ArenaWeb.UserControls.custom.johnsonferry.HideAdmin" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<script type="text/javascript">
    $(document).ready(function() {
        var count = 0;
        $('p#ToggleAdmin').each(function() {
            var $thisParagraph = $(this);
            var count = 0;
            $thisParagraph.click(function() {
                count++;
                $('div.editImage').toggleClass('hidden');
                if (count % 2 == 0) {
                    $thisParagraph.find('span').text('Hide Edit Buttons');
                } else {
                    $thisParagraph.find('span').text('Show Edit Buttons');
                }
            });
        });

        // Sliding Panel: http://spyrestudios.com/how-to-create-a-sexy-vertical-sliding-panel-using-jquery-and-css3/
        var count2 = 0;
        $(".admin-popout-panel-trigger").click(function() {
            count2++;
            $("#AdminPanel").toggle("fast");
            $(this).toggleClass("active");
            if (count2 % 2 == 0) {
                $(this).css("background-image", "url(images/fancy_right.png)");
            }
            else {
                $(this).css("background-image", "url(images/fancy_left.png)");
            }
            return false;
        });
    });


</script>

<style type="text/css">
#AdminPanel 
{
    position: fixed;
    z-index: 1000;
    top: 15px;
    left: 0;
    display: none;
    background: #000000;
    color: #FFFFFF;
    border-top:1px solid #444444;
    border-right:1px solid #444444;
    border-bottom:1px solid #444444;
    border-left:1px solid #111111;
    width: 220px;
    height: auto;
    padding: 30px 30px 30px 130px;
    -moz-border-radius-topleft: 0px;
    -moz-border-radius-topright: 20px;
    -moz-border-radius-bottomright: 20px;
    -moz-border-radius-bottomleft: 0px;
    -webkit-border-radius: 0px 20px 20px 0px;
    border-radius: 0px 20px 20px 0px; 
    filter: alpha(opacity=85);
    opacity: .85;
}

a.admin-popout-panel-trigger 
{
    display: block;
    position: fixed;
    z-index: 1001;
    text-decoration: none;
    top: 45px; left: 0;
    letter-spacing:-1px;
    font-family: verdana, helvetica, arial, sans-serif;
    color:#fff;
    padding: 10px 40px 10px 5px;
    font-weight: 700;
    background:rgba(51,51,51,0.7) url(images/fancy_right.png) 90% 70% no-repeat;
    border:1px solid #444444;
    -moz-border-radius-topleft: 0px;
    -moz-border-radius-topright: 20px;
    -moz-border-radius-bottomright: 20px;
    -moz-border-radius-bottomleft: 0px;
    -webkit-border-radius: 0px 20px 20px 0px;
    border-radius: 0px 20px 20px 0px; 
}

p.adminButton
{
    cursor: pointer;
    background-color: rgba(100,200,100,0.5);
    color: rgba(255,255,255,0.8);
    text-decoration: none;
    padding: 3px 18px;
    -moz-border-radius-topleft: 10px;
    -moz-border-radius-topright: 10px;
    -moz-border-radius-bottomright: 10px;
    -moz-border-radius-bottomleft: 10px;
    -webkit-border-radius: 10px 10px 10px 10px;
    border-radius: 10px 10px 10px 10px;  
}

p.adminButton a
{
    color: rgba(255,255,255,0.8);
    text-decoration: none;
}

.hidden
{
    display: none !important;
}

</style>

<div id="AdminPanel">
    <p>This Admin Panel contains tools to make administrating the site easier. New tools can be added as needed.</p>
    <p id="ToggleAdmin" class="adminButton"><span>Hide Edit Buttons</span>&nbsp;&nbsp;&nbsp;<img src="/Images/edit_no_shadow.gif" style="position: absolute;" /></p>
    <p id="QuickEdit" class="adminButton"><a onclick="javascript:if(location.href.search(/page=/i)!=-1) {var regex=new RegExp(&quot;[\\?&amp;]page=([^&amp;]*)&quot;);var qs=regex.exec(window.location.href);if(qs!=null) {location.href=&quot;http://arena/default.aspx?page=34&amp;pageid=&quot;+qs[1];}}" href="javascript:void(0);">Quick Edit this Page</a></p>
</div>
<a class="admin-popout-panel-trigger" href="#">Admin</a>