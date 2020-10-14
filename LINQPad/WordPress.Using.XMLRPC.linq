<Query Kind="Program">
  <NuGetReference>xmlrpcnet</NuGetReference>
  <Namespace>CookComputing.XmlRpc</Namespace>
  <Namespace>xmlrpc</Namespace>
</Query>

void Main()
{

	
	WordPress wp = new WordPress("mecurioJ","l2#gI9n3");
	Hashtable FirstPost = wp.GetPost();
	
	
	object testCategories = wp.GetTaxonomy("category");
	object TestTaxonomies = wp.GetTaxonomies("");
	
	object testTerms = wp.GetTerms("category");
	
//	var foo = testPost.Cast<DictionaryEntry>().Select(tt => 
//			string.Format("public {0} {1} {2}",tt.Value.GetType().Name.ToString(),tt.Key,"{ get ; set ; }"));
//	
//	foo.Dump();

//testCategories.Dump();
//TestTaxonomies.Dump();
FirstPost.Dump();
//var foo = testCategories.Cast<DictionaryEntry>().Select(tt => 
//			string.Format("public {0} {1} {2}",tt.Value.GetType().Name.ToString(),tt.Key,"{ get ; set ; }"));
//	foo.Dump();
	
}

public class WordPress
{
	public String UserName {get;set;}
	public String Password {get;set;}
	
	public WordPress(String userName, String passWord)
	{
		UserName = userName;
		Password = passWord;
	}
	
	public dynamic GetPost()
	{
		IWordPress proxy = XmlRpcProxyGen.Create<IWordPress>();
		return proxy.GetPost(new []{
		"0",
		UserName,
		Password,
		"14"});
	}
	
	public dynamic GetTerms(string taxonomy)
	{
		IWordPress proxy = XmlRpcProxyGen.Create<IWordPress>();
		return proxy.GetTerms(new[]{
		"0",
		UserName,
		Password,
		taxonomy
		});
	}
	
	public dynamic GetTaxonomy(string taxonomy)
	{
		IWordPress proxy = XmlRpcProxyGen.Create<IWordPress>();
		return proxy.GetTaxonomy(new[]{
		"0",
		UserName,
		Password,
		taxonomy
		});
	}
	
	public dynamic GetTaxonomies(string taxonomy)
	{
		IWordPress proxy = XmlRpcProxyGen.Create<IWordPress>();
		return proxy.GetTaxonomies(new[]{
		"0",
		UserName,
		Password,
		taxonomy
		});
	}
}

// Define other methods and classes here
[XmlRpcUrl("http://filichia.com/blog/xmlrpc.php")]
public interface IWordPress : IXmlRpcProxy
{
	/*--Posts---------------------------*/
	[XmlRpcMethod("wp.getPost")]
	dynamic GetPost(object[] args);
    [XmlRpcMethod("wp.getPosts")]
	dynamic GetPosts(object[] args);
    [XmlRpcMethod("wp.newPost")]
	dynamic NewPost(object[] args);
    [XmlRpcMethod("wp.editPost")]
	dynamic EditPost(object[] args);
    [XmlRpcMethod("wp.deletePost")]
	dynamic DeletePost(object[] args);
    [XmlRpcMethod("wp.getPostType")]
	dynamic GetPostType(object[] args);
    [XmlRpcMethod("wp.getPostTypes")]
	dynamic GetPostTypes(object[] args);
    [XmlRpcMethod("wp.getPostFormats")]
	dynamic GetPostFormats(object[] args);
    [XmlRpcMethod("wp.getPostStatusList")]
	dynamic GetPostStatusList(object[] args);
	
	/*--Taxonomies----------------------*/
	[XmlRpcMethod("wp.getTaxonomy")]
	dynamic GetTaxonomy(object[] args);
    [XmlRpcMethod("wp.getTaxonomies")]
	dynamic GetTaxonomies(object[] args);
    [XmlRpcMethod("wp.getTerm")]
	dynamic GetTerm(object[] args);
    [XmlRpcMethod("wp.getTerms")]
	dynamic GetTerms(object[] args);
    [XmlRpcMethod("wp.newTerm")]
	dynamic NewTerm(object[] args);
    [XmlRpcMethod("wp.editTerm")]
	dynamic EditTerm(object[] args);
    [XmlRpcMethod("wp.deleteTerm")] 
	dynamic DeleteTerm(object[] args);
	
	/*--Media---------------------------*/
	[XmlRpcMethod("wp.getMediaItem")] 
	dynamic GetMediaItem(object[] args);
    [XmlRpcMethod("wp.getMediaLibrary")] 
	dynamic GetMediaLibrary(object[] args);
    [XmlRpcMethod("wp.uploadFile")]
	dynamic UploadFile(object [] args);
	
	/*--Comments------------------------*/
	[XmlRpcMethod("wp.getCommentCount")]
	dynamic GetCommentCount(object [] args);
    [XmlRpcMethod("wp.getComment")]
	dynamic GetComment(object [] args);
    [XmlRpcMethod("wp.getComments")]
	dynamic GetComments(object [] args);
    [XmlRpcMethod("wp.newComment")]
	dynamic NewComment(object [] args);
    [XmlRpcMethod("wp.editComment")]
	dynamic EditComment(object [] args);
    [XmlRpcMethod("wp.deleteComment")]
	dynamic DeleteComment(object [] args);
    [XmlRpcMethod("wp.getCommentStatusList")]
	dynamic GetCommentStatusList(object [] args);
	
	/*--Options-------------------------*/
	[XmlRpcMethod("wp.getOptions")]
	dynamic GetOptions(object [] args);
    [XmlRpcMethod("wp.setOptions")]
	dynamic SetOptions(object [] args);
	
	/*----------------------------------*/
	[XmlRpcMethod("wp.getUsersBlogs")]
	dynamic GetUsersBlogs(object [] args);
    [XmlRpcMethod("wp.getUser")]
	dynamic GetUser(object [] args);
    [XmlRpcMethod("wp.getUsers")]
	dynamic GetUsers(object [] args);
    [XmlRpcMethod("wp.getProfile")]
	dynamic GetProfile(object [] args);
    [XmlRpcMethod("wp.editProfile")]
	dynamic EditProfile(object [] args);
    [XmlRpcMethod("wp.getAuthors")]
	dynamic GetAuthors(object [] args);
	/*----------------------------------*/ 
	
	
}