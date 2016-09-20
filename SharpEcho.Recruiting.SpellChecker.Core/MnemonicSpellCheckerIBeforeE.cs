using SharpEcho.Recruiting.SpellChecker.Contracts;

namespace SharpEcho.Recruiting.SpellChecker.Core
{
    /// <summary>
    /// This spell checker implements the following rule:
    /// "I before E, except after C" is a mnemonic rule of thumb for English spelling.
    /// If one is unsure whether a word is spelled with the sequence ei or ie, the rhyme
    /// suggests that the correct order is ie unless the preceding letter is c, in which case it is ei. 
    /// 
    /// Examples: believe, fierce, collie, die, friend, deceive, ceiling, receipt would be evaulated as spelled correctly
    /// heir, protein, science, seeing, their, and veil would be evaluated as spelled incorrectly.
    /// </summary>
    public class MnemonicSpellCheckerIBeforeE : ISpellChecker
    {
        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public bool Check(string word)
        {
            var result = true;

            if (word.Contains("ie"))
            {
                result = ValidateIBeforeE(word);
            }

            if (result == true && word.Contains("ei"))
            {
                result = ValidateEBeforeI(word);
            }

            return result;
        }

        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when all substrings of "ie" in the word are not preceded by a "c"</returns>
        private bool ValidateIBeforeE(string word)
        {
            var validationResult = true;

            var charactersInWord = word.ToCharArray();

            for (var i = 1; i < charactersInWord.Length; i++)
            {
                if (i < charactersInWord.Length - 1)
                {
                    if (charactersInWord[i] == 'i' && charactersInWord[i + 1] == 'e')
                    {
                        if (charactersInWord[i - 1] == 'c')
                        {
                            validationResult = false;
                            break;
                        }
                    }
                }
            }

            return validationResult;
        }

        /// <summary>
        /// Returns false if the word contains a letter sequence of "ei" when it is not immediately preceded by c
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when all substrings of "ei" in the word are preceded by a "c"</returns>
        private bool ValidateEBeforeI(string word)
        {
            var validationResult = true;

            var charactersInWord = word.ToCharArray();

            for (var i = 1; i < charactersInWord.Length; i++)
            {
                if (i < charactersInWord.Length - 1)
                {
                    if (charactersInWord[i] == 'e' && charactersInWord[i + 1] == 'i')
                    {
                        if (charactersInWord[i - 1] != 'c')
                        {
                            validationResult = false;
                            break;
                        }
                    }
                }
            }

            return validationResult;
        }
    }
}
