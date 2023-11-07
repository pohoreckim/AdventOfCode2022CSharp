using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eight
{
    enum Direction
    {
        UP, LEFT, RIGHT, DOWN
    }
    internal class Forest
    {
        public List<(int row, int column, int value)> Trees { get; }
        public Forest()
        {
            Trees = new List<(int row, int column, int value)>();
        }
        public Forest(List<(int row, int column, int value)> trees)
        {
            Trees = trees;
        }
        public IEnumerator<(int, int, int)> GetEnumerator(int index, Direction direction)
        {
            var treeLine = (direction == Direction.UP || direction == Direction.DOWN) ? Trees.Where((x) => x.column == index) : Trees.Where((x) => x.row == index);
            treeLine = (direction == Direction.UP || direction == Direction.RIGHT) ? treeLine.Reverse() : treeLine;
            return new ForestEnum(treeLine.Select((x) => x.value).ToArray(), index);
        }
        public IEnumerable<(int, int, int)> GetEnumerable(int index, Direction direction)
        {
            IEnumerator enumerator = GetEnumerator(index, direction);
            while (enumerator.MoveNext())
            {
                yield return ((int, int, int))enumerator.Current;
            }
        }

        internal class ForestEnum : IEnumerator<(int index, int value, int id)>
        {
            private int[] _trees;
            private int _currentIndex;
            private int _lineId;
            public ForestEnum(int[] trees, int id)
            {
                _trees = trees;
                _currentIndex = -1;
                _lineId = id;
            }
            public (int, int, int) Current { get { return (_currentIndex, _trees[_currentIndex], _lineId); } }

            object IEnumerator.Current { get { return Current; } }

            public void Dispose() { }

            public bool MoveNext()
            {
                return ++_currentIndex < _trees.Length;
            }
            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}
