using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyURL
{
    internal class TinyURLTable
    {
        // maps unique ID to original URL
        private IDictionary<string, string> _codecs;
        // maps unique ID to retrieval frequency
        private IDictionary<string, int> _codecFrequencies;

        public TinyURLTable()
        {
            _codecs = new Dictionary<string, string>();
            _codecFrequencies = new Dictionary<string, int>();
        }

        public bool Find(string ID)
        {
            return _codecs.ContainsKey(ID);
        }

        public void Put(string ID, string url)
        {
            if (Find(ID))
                return;
            _codecs.Add(ID, url);
            _codecFrequencies.Add(ID, 0);
        }

        public string? Get(string ID)
        {
            if (_codecs.TryGetValue(ID, out string? res))
            {
                // increment frequency
                _codecFrequencies[ID]++;
            }
            return res;
        }

        public int GetFrequency(string ID)
        {
            return _codecFrequencies[ID];
        }

        public void Remove(string ID)
        {
            if (_codecs.ContainsKey(ID))
                _codecs.Remove(ID);
            if (_codecFrequencies.ContainsKey(ID))
                _codecFrequencies.Remove(ID);
        }
    }
}
