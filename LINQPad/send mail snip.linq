<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
</Query>

void Main()
{
	System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(
		"Randy.Terrell@flyfrontier.com",
		"F9ITPST@flyfrontier.com",
		"RE: Im bringing Breakfast yall!!!",
		"I'm thinking on Friday, Dave. Does that work?");
	msg.To.Add("F9ITBusinessIntelligence@flyfrontier.com");
	msg.To.Add("F9AppDev@flyfrontier.onmicrosoft.com");
	using(var clnt = new System.Net.Mail.SmtpClient("mailrelay.flyfrontier.com",25))
	{
		clnt.Send(msg);
	}
	
}

// Define other methods and classes here