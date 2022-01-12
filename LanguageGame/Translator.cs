using System;
using System.Globalization;
using System.Text;

namespace LanguageGame
{
    public static class Translator
    {
        /// <summary>
        /// Translates from English to Pig Latin. Pig Latin obeys a few simple following rules:
        /// - if word starts with vowel sounds, the vowel is left alone, and most commonly 'yay' is added to the end;
        /// - if word starts with consonant sounds or consonant clusters, all letters before the initial vowel are
        ///   placed at the end of the word sequence. Then, "ay" is added.
        /// Note: If a word begins with a capital letter, then its translation also begins with a capital letter,
        /// if it starts with a lowercase letter, then its translation will also begin with a lowercase letter.
        /// </summary>
        /// <param name="phrase">Source phrase.</param>
        /// <returns>Phrase in Pig Latin.</returns>
        /// <exception cref="ArgumentException">Thrown if phrase is null or empty.</exception>
        /// <example>
        /// "apple" -> "appleyay"
        /// "Eat" -> "Eatyay"
        /// "explain" -> "explainyay"
        /// "Smile" -> "Ilesmay"
        /// "Glove" -> "Oveglay".
        /// </example>
        public static string TranslateToPigLatin(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase) || phrase.Length == 0)
            {
                throw new ArgumentException("Source string cannot be null or empty or whitespace.", nameof(phrase));
            }

            var sb = new StringBuilder();
            int index = 0;
            bool isLetter = false;
            for (int i = 0; i < phrase.Length; i++)
            {
                if (i == phrase.Length - 1 && char.IsLetter(phrase[i]))
                {
                    sb.Append(phrase.Substring(index).TranslateToPigLatinPhrase());
                    continue;
                }

                if (char.IsLetter(phrase[i]) || phrase[i] == '’')
                {
                    isLetter = true;
                }
                else if (isLetter)
                {
                    sb.Append(phrase.Substring(index, i - index).TranslateToPigLatinPhrase());
                    isLetter = false;
                    sb.Append(phrase[i]);
                    index = i + 1;
                }
                else
                {
                    sb.Append(phrase[i]);
                    index = i + 1;
                }
            }

            return sb.ToString();
        }

        public static string TranslateToPigLatinPhrase(this string word)
        {
            if (string.IsNullOrWhiteSpace(word) || word.Length == 0)
            {
                throw new ArgumentException("Source string cannot be null or empty or whitespace.", nameof(word));
            }

            if (IsVowel(word[0]))
            {
                return word + "yay";
            }

            bool isUpper = char.IsUpper(word[0]);
            string str = word.ToLower(CultureInfo.CurrentCulture);
            int i = 0;
            for (; i < word.Length; i++)
            {
                if (IsVowel(word[i]))
                {
                    break;
                }
            }

            str = str[i..] + str[..i] + "ay";
            if (isUpper)
            {
                str = char.ToUpper(str[0], CultureInfo.CurrentCulture) + str[1..];
            }

            return str;
        }

        private static bool IsVowel(char c)
        {
            return "aeiou".IndexOf(c, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
