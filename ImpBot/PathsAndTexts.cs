using System;

namespace ImpBot
{
	public class PathsAndTexts
	{
		private readonly string[] _images;
		private readonly string[] _attackTexts;
		private readonly string[] _spellTexts;
		private readonly string[] _dismissTexts;
		private readonly string[] _miscTexts;
		private readonly string[] _shakeTexts;
		private readonly string[] _summonTexts;
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

			#region Imp quotes
			#region Imp attack
			_attackTexts = new[]
			{
                // attack order quotes
                "Is this REALLY necessary?!",
				"This was NOT in my contract!",
				"Can't we all just get along?",
				"Ohhhh sure, send the little guy!"
			};
			#endregion

			#region Imp spell
			_spellTexts = new[]
			{
                 // spell quotes
                "What's in it for me?",
				"Do I have to?!",
				"Ahh! Okay, okay, okay, okay, okay, okay!",
				"Yeah, I'll get right on it."
			};
			#endregion

			#region Imp dismiss
			_dismissTexts = new[]
			{
                // dismissed quotes
                "You know, we've had a lot of fun together, it's been really special, but I think it's time I should start seeing other warlocks. Just a little on the side. No no no it's not you, it's not you, it's me. I just need my space, it's nobody's fault.",
				"Don't call on me, I'll call on you.",
				"Argh! I feel so used!",
				"Goodbye. Thanks.",
				"\\*indistinct grumbling*...I wish...*indistinct grumbling*...wish you were DEAD."
			};
			#endregion

			#region Imp shake
			_shakeTexts = new[]
			{
				"AAaahhh! S-s-stop it!",
				"Hah-ah! Okay, okay, okay!",
				"Nooo-Oooo-ooo...!"
			};
			#endregion

			#region Imp summon
			_summonTexts = new[]
			{
				"What? You mean you can't kill this one by yourself?",
				"No shi rakir no tiros kamil re lok ante refir shi rakir",
				"\\*Demonic cackle*"
			};
			#endregion

			#region Imp misc
			_miscTexts = new[]
			{
                // misc quotes
                "Make yourself useful and help me out on this one!",
				"Release me already, I've had enough!",
				"Maz ruk X rikk xi laz enkil parn zila zilthuras karkun thorje kar x zennshi"
			};
			#endregion

			#endregion

			#region Calcifer quotes
			_calQuotes = System.IO.File.ReadAllLines(@"Texts/CalQuotes.txt");
			#endregion

			_rand = new Random();
		}

		public string RandomImage()
		{
			var randomImageIndex = _rand.Next(_images.Length);
			return _images[randomImageIndex];
		}

		public string RandomAttackText()
		{
			var randomTextIndex = _rand.Next(_attackTexts.Length);
			return _attackTexts[randomTextIndex];
		}

		public string RandomSpellText()
		{
			var randomTextIndex = _rand.Next(_spellTexts.Length);
			return _spellTexts[randomTextIndex];
		}

		public string RandomDismissText()
		{
			var randomTextIndex = _rand.Next(_dismissTexts.Length);
			return _dismissTexts[randomTextIndex];
		}

		public string RandomMiscText()
		{
			var randomTextIndex = _rand.Next(_miscTexts.Length);
			return _miscTexts[randomTextIndex];
		}

		public string RandomCalciferQuote()
		{
			var randomTextIndex = _rand.Next(_calQuotes.Length);
			return _calQuotes[randomTextIndex];
		}

		public string ShakeText()
		{
			var randomShakeIndex = _rand.Next(_shakeTexts.Length);
			return _shakeTexts[randomShakeIndex];
		}

		public string SummonText()
		{
			var randomSummonIndex = _rand.Next(_summonTexts.Length);
			return _summonTexts[randomSummonIndex];
		}

	}
}
