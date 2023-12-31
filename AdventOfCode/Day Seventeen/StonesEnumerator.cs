﻿using System.Collections;

namespace Day_Seventeen
{
    internal class StonesEnumerator : IEnumerator<Stone>
    {
        private List<string> _stoneShapes;
        int position = -1;
        public StonesEnumerator(List<string> stoneShapes)
        {
            _stoneShapes = stoneShapes;
        }
        public Stone Current => new Stone(_stoneShapes[position]);

        public int Position { get => position; }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            position = (position + 1) % _stoneShapes.Count;
            return true;
        }
        public Stone GetNext()
        {
            MoveNext();
            return Current;
        }
        public void Reset()
        {
            position = -1;
        }
    }
}
