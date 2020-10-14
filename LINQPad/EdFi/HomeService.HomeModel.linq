<Query Kind="Program">
  <Connection>
    <ID>fb112041-3f78-4cec-8506-30cb177ff82a</ID>
    <Persist>true</Persist>
    <Server>bvaserver</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>mjfilichia</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA6CZ+pDyS6k64q01uTaqaNgAAAAACAAAAAAAQZgAAAAEAACAAAAA/wi/zetbMyBcbTDQcJa/gchVwzrv9FC7aCpfPJFeukQAAAAAOgAAAAAIAACAAAADwUHL4QemeWAFnHnWc5Nl4dq34Y1ZQIlhv1Q7XNezy3hAAAACHfed97lYQTFtsyLoxXCdwQAAAAMtENCa7nAZ60KBSi+Ndh0DX85mnTro+1cyXsLtwOGeaWIdJVwwWeKf3e2DoAa5dBIoNpjtd8jcQPhHqou/2DyI=</Password>
    <Database>EdFi_Application</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	LocalEducationAgencies
	.Join(LocalEducationAgencySupports, 
		lea => lea.LocalEducationAgencyId,
		support => support.LocalEducationAgencyId,
		(lea, support) => new {lea,support})
		.Select(t => new{
			t.lea.LocalEducationAgencyId,
			t.lea.Name,
			t.lea.Code,
			t.support.SupportContact,
			t.support.SupportEmail,
			t.support.SupportPhone,
			t.support.TrainingAndPlanningUrl
		})
		.Dump();
}

// Define other methods and classes here
