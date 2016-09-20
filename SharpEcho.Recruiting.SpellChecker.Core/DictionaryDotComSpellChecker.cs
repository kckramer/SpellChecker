using SharpEcho.Recruiting.SpellChecker.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace SharpEcho.Recruiting.SpellChecker.Core
{
    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked
    /// 
    /// Example: http://dictionary.reference.com/browse/SharpEcho would lookup the word SharpEcho
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not
    /// </summary>
    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        /// <summary>
        /// Returns false if http://dictionary.reference.com/browse/<word> sends back a non-successful status code
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public bool Check(string word)
        {
            var result = false;

            // TODO: Move URI generation into separate class so we can interface that out and create a test implementation so our tests don't actually hit an endpoint.
            var uri = string.Format("http://dictionary.reference.com/browse/{0}", word.Replace(" ", ""));

            var responseTask = GetHttpResponse(uri);
            responseTask.Wait();

            if (responseTask.Result.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns http response from the passed in uri
        /// </summary>
        /// <param name="uri">The uri to get the response from</param>
        /// <returns>Returns a task that contains the http response recieved from the passed in uri</returns>
        private async Task<HttpResponseMessage> GetHttpResponse(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return await httpClient.GetAsync(uri);
            }
        }
    }
}
