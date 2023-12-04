namespace Movies_Watchlist_API.Extensions
{
    public static class EditMovieNameExtension
    {
        private static List<string> exceptWords = new List<string>()
            {
                "v", "na", "za", "s", "o", "u", "in", "on", "at", "a",  "ale", "i","and", "or", "but", "if", "the"
            };

        public static string EditMovieName(this string movieName)
        {

            var tempNameShorted = movieName.Split(new char[] { ':', '-' },StringSplitOptions.RemoveEmptyEntries);

            var wordsInName = tempNameShorted[0].Split(' ');

            List<string> words = new List<string>();
            foreach (var item in wordsInName)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (exceptWords.Contains(item.ToLower()) && wordsInName[0].ToLower() != item.ToLower())
                    {
                        words.Add(item.ToLower());    
                    }
                    else
                    { 
                    string wordWithFirstUpper = char.ToUpper(item[0]) + item.Substring(1).ToLower();
                    words.Add(wordWithFirstUpper);
                    }
                }

            }

            var result = string.Join(" ", words);

            return result;
        }

    }
}
