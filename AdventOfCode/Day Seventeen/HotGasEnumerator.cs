using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Seventeen
{
    internal class HotGasEnumerator : IEnumerator<char>
    {
        private char[] _hotGasJets;
        public int Length
        {
            get { return _hotGasJets.Length; }
        }
        private int position = -1;
        public char Current => _hotGasJets[position];
        object IEnumerator.Current => Current;
        public int Position { get => position; }

        public HotGasEnumerator(char[] hotGasJets)
        {
            _hotGasJets = hotGasJets;
        }
        public HotGasEnumerator(string s)
        {
            _hotGasJets = s.ToCharArray();
        }
        public void Dispose() { }
        public bool MoveNext()
        {
            position = (position + 1) % _hotGasJets.Length;
            return true;
        }
        public void Reset()
        {
            position = -1;
        }
    }
}
