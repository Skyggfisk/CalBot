using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpBot
{
    class PathsAndTexts
    {
        private string[] images;
        private string[] attackTexts;
        private string[] spellTexts;
        private string[] dismissTexts;
        private string[] miscTexts;
        private string[] shakeTexts;
        private string[] summonTexts;
        //private string helpText;
        private Random rand;

        public PathsAndTexts()
        {
            #region Imp images
            images = new string[]
                {
                            "Images/image1.jpg",
                            "Images/image2.png",
                            "Images/image3.jpg",
                            "Images/image4.jpg"
                };
            #endregion

            #region Imp quotes
            #region Imp attack
            attackTexts = new string[]
            {
                        // attack order quotes
                        "Is this REALLY necessary?!",
                        "This was NOT in my contract!",
                        "Can't we all just get along?",
                        "Ohhhh sure, send the little guy!"
            };
            #endregion
            #region Imp spell
            spellTexts = new string[]
            {
                 // spell quotes
                        "What's in it for me?",
                        "Do I have to?!",
                        "Ahh! Okay, okay, okay, okay, okay, okay!",
                        "Yeah, I'll get right on it."
            };
            #endregion
            #region Imp dismiss
            dismissTexts = new string[]
            {
                // dismissed quotes
                        "You know, we've had a lot of fun together, it's been really special, but I think it's time I should start seeing other warlocks. Just a little on the side. No no no it's not you, it's not you, it's me. I just need my space, it's nobody's fault.",
                        "Don't call on me, I'll call on you.",
                        "Argh! I feel so used!",
                        "Goodbye. Thanks.",
                        "\\*indistinct grumbling*...I wish...*indistinct grumbling*...wish you were DEAD."
            };
            #endregion
            #region Imp misc
            miscTexts = new string[]
            {
                // misc quotes
                        "What? You mean you can't kill this one by yourself?",
                        "Make yourself useful and help me out on this one!",
                        "Release me already, I've had enough!",
                        "Alright, I'm on it! Stop yelling!",
                        "No shi rakir no tiros kamil re lok ante refir shi rakir",
                        "Maz ruk X rikk xi laz enkil parn zila zilthuras karkun thorje kar x zennshi"
            };
            #endregion

            #endregion

            #region Imp shake
            shakeTexts = new string[]
            {
                "AAaahhh! S-s-stop it!",
                "Hah-ah! Okay, okay, okay!",
                "Nooo-Oooo-ooo...!"
            };
            #endregion

            #region Imp summon
            summonTexts = new string[]
            {
                "\\*Demonic cackle*"
            };
            #endregion

            rand = new Random();
        }



        public string RandomImage()
        {
            int randomImageIndex = rand.Next(images.Length);
            string imageToPost = images[randomImageIndex];
            return imageToPost;
        }

        public string RandomAttackText()
        {
            int randomTextIndex = rand.Next(attackTexts.Length);
            string textToPost = attackTexts[randomTextIndex];
            return textToPost;
        }

        public string RandomSpellText()
        {
            int randomTextIndex = rand.Next(spellTexts.Length);
            string textToPost = spellTexts[randomTextIndex];
            return textToPost;
        }

        public string RandomDismissText()
        {
            int randomTextIndex = rand.Next(dismissTexts.Length);
            string textToPost = dismissTexts[randomTextIndex];
            return textToPost;
        }

        public string RandomMiscText()
        {
            int randomTextIndex = rand.Next(miscTexts.Length);
            string textToPost = miscTexts[randomTextIndex];
            return textToPost;
        }

        public string ShakeText()
        {
            int randomShakeIndex = rand.Next(shakeTexts.Length);
            string text = shakeTexts[randomShakeIndex];
            return text;
        }

    }
}
