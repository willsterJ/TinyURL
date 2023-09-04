using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TinyURL
{
    internal class URLCodecManager
    {
        private TinyURLTable _urlTable;

        public URLCodecManager()
        {
            _urlTable = new TinyURLTable();
        }
        public string ConvertToTinyURL(string longURL)
        {
            // create unique short url from long url
            string uniqueHash = ComputeSHA256(longURL);
            string tinyUrl = "tinyurl.com/" + uniqueHash;
            return tinyUrl;
        }
        public void AddGeneratedURL(string originalURL)
        {
            _urlTable.Put(ConvertToTinyURL(originalURL), originalURL);
        }
        public void AddGeneratedURL(string originalURL, string newURL)
        {
            _urlTable.Put(newURL, originalURL);
        }
        public string GetLongURL(string url)
        {
            if (_urlTable.Find(url))
                return _urlTable.Get(url);
            else
                return url;
        }
        // searches if url or its converted unique URL is in url table
        public bool FindURL(string url)
        {
            return (_urlTable.Find(url)) || (_urlTable.Find(ConvertToTinyURL(url)));
        }
        public void RemoveURL(string inUrl)
        {
            if (FindURL(inUrl))
                _urlTable.Remove(inUrl);
            else if (FindURL(ConvertToTinyURL(inUrl)))
                _urlTable.Remove(ConvertToTinyURL(inUrl));
        }
        public int GetUsageStatistics(string inUrl)
        {
            if (_urlTable.Find(inUrl))
                return _urlTable.GetFrequency(inUrl);
            else
                return _urlTable.GetFrequency(ConvertToTinyURL(inUrl));
        }
        // internal function to compute unique hash value
        private string ComputeSHA256(string s)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
                return Convert.ToHexString(hashValue);
            }
        }
    }
}
