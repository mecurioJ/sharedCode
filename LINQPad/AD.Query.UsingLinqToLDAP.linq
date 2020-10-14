<Query Kind="Program">
  <NuGetReference>LinqToLdap</NuGetReference>
  <Namespace>LinqToLdap</Namespace>
  <Namespace>LinqToLdap.Collections</Namespace>
  <Namespace>LinqToLdap.EventListeners</Namespace>
  <Namespace>LinqToLdap.Exceptions</Namespace>
  <Namespace>LinqToLdap.Helpers</Namespace>
  <Namespace>LinqToLdap.Logging</Namespace>
  <Namespace>LinqToLdap.Mapping</Namespace>
  <Namespace>LinqToLdap.Mapping.PropertyMappingBuilders</Namespace>
  <Namespace>LinqToLdap.QueryCommands</Namespace>
  <Namespace>LinqToLdap.QueryCommands.Options</Namespace>
  <Namespace>LinqToLdap.TestSupport</Namespace>
  <Namespace>LinqToLdap.Transformers</Namespace>
  <Namespace>System.DirectoryServices.Protocols</Namespace>
</Query>

void Main()
{
	var config = new LdapConfiguration();
	config.ConfigureFactory("emllc.loc");
	
	var context = new DirectoryContext(config);
	var users = context.Query("OU=EMLLC,DC=emllc,DC=loc", objectClass: "User", objectCategory: "CN=Person,CN=Schema,CN=Configuration,DC=emllc,DC=loc")
	.Cast<IDirectoryAttributes>()
	.Select(t => new{
		samaccountname = t["samaccountname"].ToString(),
		Title = (t["title"] == null) ? String.Empty : t["title"].ToString(),
		Name = (t["name"] == null) ? String.Empty : t["name"].ToString(),
		LastName = (t["sn"] == null) ? String.Empty : t["sn"].ToString(),
		FirstName = (t["givenName"] == null) ? String.Empty : t["givenName"].ToString(),
		Mail = (t["mail"] == null) ? String.Empty : t["mail"].ToString(),
		userPrincipalName = (t["userprincipalname"] == null) ? String.Empty : t["userprincipalname"].ToString()
	}).OrderBy(ti => ti.Title)
	;
	
	users.Dump();
	
}

// Define other methods and classes here
