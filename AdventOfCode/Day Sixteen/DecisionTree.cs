using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Sixteen
{
    internal class DecisionTree
    {
        private List<(string name, int rate, List<(string name, int dist)> neighbours)> _vertexList;
        private string _rootNodeName;
        public int MaxHeight { get; private set; }
        public Node? Root { get; private set; }

        public DecisionTree(List<(string, int, List<(string, int)>)> vertexList, string rootNodeName, int maxHeight)
        {
            _vertexList = vertexList;
            _rootNodeName = rootNodeName;
            MaxHeight = maxHeight;
            Root = CreateTree();
        }
        private Node CreateTree()
        {
            Node root = CreateNode(null, _rootNodeName, MaxHeight, false);
            return root;
        }

        private Node CreateNode(Node parent, string name, int level, bool open)
        {
            var currentVert = _vertexList.Find(x => x.name == name);
            Node result = new Node(parent, level, currentVert.rate, open);
            
            foreach (var neightbour in currentVert.neighbours) 
            {
                var neighbourVert = _vertexList.Find(x => x.name == neightbour.name);
                if (level - neightbour.dist >= 0)
                {
                    result.AddChild(CreateNode(result, neighbourVert.name, level - neightbour.dist, false));
                }
            }
            if (level > 0 && currentVert.rate > 0)
            {
                result.AddChild(CreateNode(result, name, level - 1, true));
            }
            return result;
        }

    }
}
