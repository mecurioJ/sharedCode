<Query Kind="Statements">
  <Connection>
    <ID>57b8e721-62f2-4f6a-88ff-6c40878b38a8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>SNLBanker_ApplicationDB</Database>
  </Connection>
</Query>

var patter =@"(\w*)";//@"^(\w*)";
var ValueParse = @".&";
//This is the pattern from the expression Tree for Responsibility_Name_Display
var ExpressionTreePattern = "rtrim(Resp_Code) + ': ' + isnull(Resp_Name, '')";
var ValueString = ("[AJM: Ezra Patel]").Replace("[",string.Empty).Replace("]",string.Empty);

//Regex.Matches(expression,patter).Cast<Match>().Select(v => v.Value).Where(v => !String.IsNullOrEmpty(v)).Dump();
ValueString.Dump();