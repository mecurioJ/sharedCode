<Query Kind="Program">
  <NuGetReference>ID3.Files</NuGetReference>
  <Namespace>Id3</Namespace>
  <Namespace>Id3.Frames</Namespace>
  <Namespace>Id3.Id3v1</Namespace>
  <Namespace>Id3.Id3v2</Namespace>
  <Namespace>Id3.Id3v2.v23</Namespace>
  <Namespace>Id3.Info</Namespace>
</Query>

void Main()
{
	var mp3Files = Directory.GetFiles(@"G:\Nora Roberts Dance Upon the Air", "*.mp3", SearchOption.AllDirectories).ToList();


	var seed = 1;
	var fileList = mp3Files.Select(text =>
	 new
	 {
	 	Disc = Int32.Parse(text.Replace(@"G:\Nora Roberts Dance Upon the Air\Disc", string.Empty).Trim().Substring(0, 2).Replace(@"\", string.Empty)),
		 FileTitle = text
	 }).OrderBy(g => g.Disc).Select(f => (f.FileTitle, seed++));
	 
	 foreach (var fl in fileList)
	{
		//fl.FileTitle.Dump();
		File.Copy(fl.FileTitle,String.Format(@"G:\Dance Upon the Air.Track{0}.mp3",fl.Item2));
		//.Dump();
	}
}

// Define other methods and classes here
