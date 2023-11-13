using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Sixteen
{
    internal class Node
    {
        public string Name { get; private set; }
        public Node? Parent { get; private set; }
        public int Level { get; private set; }
        public List<Node> Children { get; private set; }
        public int Value { get { return Open ? Level * FlowRate : 0; } }
        public int FlowRate { get; private set; }
        public bool Open { get; private set; }
        public Node(string name, Node? parent, int level, int flowRate, bool open)
        {
            Parent = parent;
            Level = level;
            FlowRate = flowRate;
            Children = new List<Node>();
            Open = open;
            Name = name;
        }
        public void AddChild(Node node)
        {
            Children.Add(node);
        }
    }
}
