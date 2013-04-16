<%@ Control Language="c#" Inherits="ArenaWeb.Custom.johnsonferry.UserControls.SpiritualGiftsSurvey.Survey" CodeFile="Survey.ascx.cs" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<script type="text/javascript">
  $(document).ready(function () {
    $('#survey1').numericScale();
  });
</script>

<asp:Panel ID="SurveyPanel" runat="server">
<asp:PlaceHolder ID="phCurrentGifts" runat="server" />

<asp:Label ID="lblMessage" runat="server" />

<ol id="survey1" class="survey">
  <li class="question" title="Prophecy">When a situation needs to be corrected I feel a burden to speak up about it in order to correct it.</li>
  <li class="question" title="Shepherd">I feel a special concern for less mature Christians and feel compelled to care for them spiritually.</li>
  <li class="question" title="Teaching">I find it easy and enjoyable to spend time in intensive Bible study.</li>
  <li class="question" title="Encouraging">I am able to help others identify problems and offer solutions.</li>
  <li class="question" title="Giving">I don't understand why others don't give as much and as freely as I do.</li>
  <li class="question" title="Mercy">I am comfortable visiting people who are sick and disabled.</li>
  <li class="question" title="Evangelism">I have greater desire than most to witness to non-Christians.</li>
  <li class="question" title="Administration">If there is no leadership in a group I will step up and take charge.</li>
  <li class="question" title="Serving">I enjoy being called upon to do special jobs around the church.</li>
  <li class="question" title="Prophecy">When issues are being dealt with in a group, I speak up rather than just listening.</li>
  <li class="question" title="Shepherd">I find myself especially concerned that newer Christians will be influenced by false teachers and be harmed in their spiritual growth as a result. </li>
  <li class="question" title="Teaching">Others sometimes accuse me of being too technical or detail-oriented. </li>
  <li class="question" title="Encouraging">I would rather talk personally with someone rather than refer them elsewhere. </li>
  <li class="question" title="Giving">I find myself looking for opportunities to give my money without being asked to give. </li>
  <li class="question" title="Mercy">I have a tendency to think about things for a while before making a decision. </li>
  <li class="question" title="Evangelism">Witnessing to non-Christians comes easily to me. </li>
  <li class="question" title="Administration">I enjoy handling the details of organizing ideas, people, resources, and time in order to have more effective ministry. </li>
  <li class="question" title="Serving">I feel that I am not specifically skilled, but I enjoy doing what needs to be done around the church. </li>
  <li class="question" title="Prophecy">I am able to make quick decisions and am seldom indecisive.</li>
  <li class="question" title="Shepherd">I have a tendency to be too involved, to the point that I fail to involve others; People think that I should always be available and have all the answers. </li>
  <li class="question" title="Teaching">Others say that my communication style is stimulating, enthusiastic, 	and easily understood. </li>
  <li class="question" title="Encouraging">I enjoy encouraging people who are experiencing problems and trials.  </li>
  <li class="question" title="Giving">The quality of gifts I give is important to me.</li>
  <li class="question" title="Mercy">I am comfortable talking to those who are suffering physically and emotionally. </li>
  <li class="question" title="Evangelism">I find myself constantly seeking for opportunities to witness for Christ. </li>
  <li class="question" title="Administration">I make effective and efficient plans for accomplishing the goals of a group. </li>
  <li class="question" title="Serving">I find it hard to say "no", which can lead to being overcommitted perceived as a workaholic. </li>
  <li class="question" title="Prophecy">I am sometimes too honest with people and unintentionally offend them. </li>
  <li class="question" title="Shepherd">It bothers me greatly when something wrong is taught in the church and I seek to correct it immediately.   </li>
  <li class="question" title="Teaching">I prefer to share the meaning of a Biblical phrase or concept rather than simply quote a verse of Scripture. </li>
  <li class="question" title="Encouraging">I am burdened to communicate how Scripture impacts our actions and conduct. </li>
  <li class="question" title="Giving">I sense a great deal of joy in giving, regardless of the response of the one to whom I gave.</li>
  <li class="question" title="Mercy">I sometimes let others use me and walk over me. </li>
  <li class="question" title="Evangelism">I have been accused of being "pushy."</li>
  <li class="question" title="Administration">I hate procrastination but tend to be a perfectionist.</li>
  <li class="question" title="Serving">I prefer being active and doing something rather than just sitting around talking, reading, or listening to a speaker. </li>
  <li class="question" title="Prophecy">I have a strong sense of duty and as a result I tend not to care what people think about what I do.</li>
  <li class="question" title="Shepherd">I am comfortable being responsible for the spiritual welfare and growth of other Christians. </li>
  <li class="question" title="Teaching">I usually organize my thoughts in a systematic way.  </li>
  <li class="question" title="Encouraging">I enjoy one-on-one, person-to-person ministry more than group ministry. </li>
  <li class="question" title="Giving">When I give my money to someone or something, I seek to avoid letting others know about it.</li>
  <li class="question" title="Mercy">I have a hard time not being controlled by my circumstances.</li>
  <li class="question" title="Evangelism">I have a constant burden for non-Christians to be saved and it often influences what I do or say. </li>
  <li class="question" title="Administration">I usually know what I need to be doing and delegate the rest to others.  </li>
  <li class="question" title="Serving">I respond cheerfully when asked to do a job, even if it seems trite.</li>
  <li class="question" title="Prophecy">I have the ability to explain Biblical truth in such a way that it brings others to repentance.</li>
  <li class="question" title="Shepherd">I enjoy seeing others realize their potential for Christ.</li>
  <li class="question" title="Teaching">I find it gratifying to spend hours reading and studying.</li>
  <li class="question" title="Encouraging">I have the ability to motivate others to action and to take the next step in their spiritual journey.</li>
  <li class="question" title="Giving">I enjoy helping people in need by sharing my resources with them.</li>
  <li class="question" title="Mercy">My friends say that I am loyal and available in their time of need.</li>
  <li class="question" title="Evangelism">When I hear about non-Christians, I am immediately prompted to pray.</li>
  <li class="question" title="Administration">I approach challenges with a logical, methodical method of problem solving.</li>
  <li class="question" title="Serving">When I see something that needs to be done, I take initiative without being asked.</li>
  <li class="question" title="Prophecy">Sometimes I am so honest with others that I may offend them. </li>
  <li class="question" title="Shepherd">I love people and tend to be more relationship/people oriented than task oriented.</li>
  <li class="question" title="Teaching">I enjoy explaining new concepts and ideas with others.</li>
  <li class="question" title="Encouraging">I readily see positive qualities in people and enjoy affirming them.</li>
  <li class="question" title="Giving">I am thrifty and save money in order to help those who are less fortunate which sometimes leads to others perceiving me as being stingy.</li>
  <li class="question" title="Mercy">I empathize with others’ feelings and pain.</li>
  <li class="question" title="Evangelism">I share the Gospel every chance I get.</li>
  <li class="question" title="Administration">The details necessary to accomplish an objective seem obvious to me.</li>
  <li class="question" title="Serving">I am usually involved in a variety of activities and volunteer for many different jobs.</li>
  <li class="question" title="Prophecy">I prefer straightforward, direct communication and avoid small talk.</li>
  <li class="question" title="Shepherd">I am willing to study and spend a lot of time if necessary to help those I serve.</li>
  <li class="question" title="Teaching">Others often ask me to help them comprehend complex ideas.</li>
  <li class="question" title="Encouraging">Friends often talk to me when they need support and encouragement. </li>
  <li class="question" title="Giving">Friends say that I am generous.</li>
  <li class="question" title="Mercy">I have the ability to bring comfort to others in their darkest moments.</li>
  <li class="question" title="Evangelism">My friends say that I have a unique ability to channel conversations and to talk to folks about Jesus.</li>
  <li class="question" title="Administration">Time-management is one of my strengths.</li>
  <li class="question" title="Serving">I love to contribute to ministry from behind the scenes and would rather not be recognized for my efforts.</li>
  <li class="question" title="Prophecy">I say things out loud that others are thinking but are afraid to say.</li>
  <li class="question" title="Shepherd">I tend to be a peacemaker and a problem solver.  </li>
  <li class="question" title="Teaching">I have the ability to connect seemingly unrelated ideas and explain the big picture.</li>
  <li class="question" title="Encouraging">Others sometimes perceive me as being insincere because I am so optimistic, even when things look grim.</li>
  <li class="question" title="Giving">I take advantage of opportunities to give financially above and beyond my usual tithes and offerings.</li>
  <li class="question" title="Mercy">I try to go the extra mile to keep peace and harmony in relationships.</li>
  <li class="question" title="Evangelism">I routinely pray for people in my sphere of influence who do not know Jesus.</li>
  <li class="question" title="Administration">I am often too analytical and have a tendency to think too much.</li>
  <li class="question" title="Serving">I am known as being faithful to follow through with my commitments. </li>
</ol>​​


<asp:Button ID="btnSaveGifts" runat="server" onclick="btnSaveGifts_Click" Text="Save Your Gifts to Our Database" style="display: none;" />

<input type="hidden" id="hdnSelectedVals" class="hdnSelectedVals" runat="server" />

</asp:Panel>

<asp:Panel ID="ConfirmationPanel" runat="server" Visible="false">
<h2>Thank You!</h2>
<p>Your Spiritual Gifts have been saved to our database.</p>
<asp:PlaceHolder ID="phCurrentGiftsNew" runat="server" />
</asp:Panel>