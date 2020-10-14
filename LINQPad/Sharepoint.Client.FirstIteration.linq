<Query Kind="Program">
  <GACReference>Microsoft.SharePoint.Client, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.DocumentManagement, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Runtime, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Search, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Search.Applications, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Taxonomy, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.UserProfiles, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <Namespace>Microsoft.SharePoint.Client</Namespace>
</Query>

void Main()
{
	//SharePoint Client Side Authentication
	ClientContext ctx = new ClientContext(@"https://plusconsulting.sharepoint.com/PublicWebSite/")
	{
		AuthenticationMode = ClientAuthenticationMode.FormsAuthentication,
		FormsAuthenticationLoginInfo = new FormsAuthenticationLoginInfo{
		LoginName = "jfilichia@plusconsulting.com",
		Password = "iem!adm1n"
		}
	};
	
	Site site = ctx.Site;
	Web root = site.RootWeb;
	//User currUser = root.CurrentUser;
	
	ctx.Load(site);
	ctx.Load(root);
	//ctx.Load(currUser);
	
	List cat = root.GetCatalog(0);
	
	ctx.Load(cat);
	
	cat.ItemCount.Dump();
	
	
	
	
	
	/*
	//List Creation Sample
	ListCreationInformation lci = new ListCreationInformation()
	{
		Title = "Contacts CSOM",
		Description = "Contacts Created by Client Side Object Model",
		TemplateType = (Int32)ListTemplateType.Contacts,
		QuickLaunchOption = QuickLaunchOptions.On
	};
	*/
	//List newList = ctx.Web.Lists.Add(lci);
	
	//ctx.ExecuteQuery();
	
	
	
	
	
}
