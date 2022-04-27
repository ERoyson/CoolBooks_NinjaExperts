using CoolBooks_NinjaExperts.Models;
using System.Runtime.Serialization;

namespace CoolBooks_NinjaExperts.ViewModels
{

	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class DataPoint
	{
		public DataPoint(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}

	public class ShowStatisticsViewModel
    {
        public DataPoint? DataPoint { get; set; }
		public Books? Book { get; set; } = new Books();
		public List<Books>? Books { get; set; } = new List<Books>();
		public Genres? Genre { get; set; } = new Genres();
        public List<Genres>? Genres { get; set; } = new List<Genres>();
		public Authors? Author { get; set; } = new Authors();
		public List<Authors>? Authors { get; set; } = new List<Authors>();
		public List<BooksGenres>? BooksGenres { get; set; } = new List<BooksGenres>();
		public Reviews? Review { get; set; } = new Reviews();
		public List<Reviews>? Reviews { get; set; } = new List<Reviews>();
		public Comments? Comment { get; set; } = new Comments();
		public List<Comments>? Comments { get; set; } = new List<Comments>();

		public int CommentsCount { get; set; }
		public bool IsSelected { get; set; }


	}
}
