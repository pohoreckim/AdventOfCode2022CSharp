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
        public List<int> LeafValues { get; private set; }
        public DecisionTree(List<(string, int, List<(string, int)>)> vertexList, string rootNodeName, int maxHeight)
        {
            _vertexList = vertexList;
            _rootNodeName = rootNodeName;
            MaxHeight = maxHeight;
            Root = CreateTree();
            LeafValues = new List<int>();
            CalculateLeafValues(Root, 0);
        }
        private Node CreateTree()
        {
            Node root = CreateNode(null, _rootNodeName, MaxHeight, _vertexList.Select(x => x.name).ToList());
            return root;
        }
        private Node CreateNode(Node parent, string name, int level, List<string> open)
        {
            var currentVert = _vertexList.Find(x => x.name == name);
            Node result = new Node(name, parent, level, currentVert.rate, true);
            open.Remove(currentVert.name);

            int[] dists = Dijkstra(currentVert.name);
            for (int i = 0; i < _vertexList.Count; i++)
            {
                if (open.Contains(_vertexList[i].name))
                {
                    if (level - dists[i] >= 0)
                    {
                        result.AddChild(CreateNode(result, _vertexList[i].name, level - dists[i], open.Select(x => new string(x)).ToList()));
                    }
                }
            }          
            //foreach (var neightbour in currentVert.neighbours) 
            //{
            //    var neighbourVert = _vertexList.Find(x => x.name == neightbour.name);
            //    if (level - neightbour.dist >= 0)
            //    {
            //        result.AddChild(CreateNode(result, neighbourVert.name, level - neightbour.dist, false));
            //    }
            //}
            //if (level > 0 && currentVert.rate > 0)
            //{
            //    result.AddChild(CreateNode(result, name, level - 1, true));
            //}
            return result;
        }
        private int[] Dijkstra(string source)
        {
            List<(string name, int dist)> Q = new List<(string, int)>();
            int verticesCount = _vertexList.Count;
            int[] dists = new int[verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                Q.Add((_vertexList[i].name, int.MaxValue));
                dists[i] = int.MaxValue;
            }
            int id = Q.FindIndex(x => x.name == source);
            Q[id] = (Q[id].name, 1);
            dists[id] = 1;

            while (Q.Count > 0)
            {
                id = Q.IndexOf(Q.MinBy(x => x.dist));
                var u = Q[id];
                Q.RemoveAt(id);

                foreach (var edge in _vertexList.Find(x => x.name == u.name).neighbours)
                {
                    if (Q.Any(x => x.name == edge.name))
                    {
                        int alt = u.dist + edge.dist;
                        id = Q.FindIndex(x => x.name == edge.name);
                        var vId = _vertexList.FindIndex(x => x.name == edge.name);
                        if (alt < Q[id].dist)
                        {
                            Q[id] = (Q[id].name, alt);
                            dists[vId] = alt;
                        }
                    }
                }
            }
            return dists;
        }
        private void CalculateLeafValues(Node n, int cumulativeValue)
        {
            cumulativeValue += n.Value;
            if (n.Level == 0 || n.Children.Count == 0)
            {
                LeafValues.Add(cumulativeValue);
            }
            else
            {
                foreach (var child in n.Children)
                {
                    CalculateLeafValues(child, cumulativeValue);
                }
            }
        }
    }
}
