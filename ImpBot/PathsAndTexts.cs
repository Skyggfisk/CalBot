using System;

namespace CalBot
{
	public class PathsAndTexts
	{
		private readonly string[] _images;
		private readonly string[] _calQuotes;
		private readonly Random _rand;

		public PathsAndTexts()
		{
			#region Image paths
			_images = new[]
				{
							"Images/image1.jpg",
							"Images/image2.png",
							"Images/image3.jpg",
							"Images/image4.jpg"
				};
			#endregion

			_calQuotes = System.IO.File.ReadAllLines(@"Texts/CalQuotes.txt");

			_rand = new Random();
		}

		public string RandomImage()
		{
			var randomImageIndex = _rand.Next(_images.Length);
			return _images[randomImageIndex];
		}

		public string RandomCalciferQuote()
		{
			var randomTextIndex = _rand.Next(_calQuotes.Length);
			return _calQuotes[randomTextIndex];
		}

	}
}
