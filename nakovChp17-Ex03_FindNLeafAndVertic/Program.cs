using System;
using System.Collections.Generic;

namespace Trees
{
    public class TreeNode<T>
    {
        private T value;
        private bool hasParent;
        private List<TreeNode<T>> children;
        public TreeNode(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }
            this.value = value;
            this.children = new List<TreeNode<T>>();
        }
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        public int ChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }
        public void AddChild(TreeNode<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(
                    "Cannot insert null value!");
            }

            if (child.hasParent)
            {
                throw new ArgumentException(
                    "The node already has a parent!");
            }

            child.hasParent = true;
            this.children.Add(child);
        }
        public TreeNode<T> GetChild(int index)
        {
            return this.children[index];
        }
    }
    public class Tree<T>
    {
        private TreeNode<T> root;

        public Tree(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                    "Cannot insert null value!");
            }

            this.root = new TreeNode<T>(value);
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                this.root.AddChild(child.root);
            }
        }
        public TreeNode<T> Root
        {
            get
            {
                return this.root;
            }
        }


    }

    public class Program
    {
        static int countInnerLeafs = 0;
        static int countLeafs = 0;

        public static void Main(string[] args)
        {
            Tree<int> tree =
            new Tree<int>(7,
                new Tree<int>(19,
                    new Tree<int>(1),
                    new Tree<int>(12),
                    new Tree<int>(12),
                    new Tree<int>(31)),
                new Tree<int>(21),
                new Tree<int>(14,
                    new Tree<int>(23),
                    new Tree<int>(6))
            );

            int num = int.Parse(Console.ReadLine());
            
            Console.WriteLine(CountLeafsAndInner(tree));
        }
        static void CountLeafsAndInner(Tree<int> tree)
        {
            Stack<TreeNode<int>> stack = new Stack<TreeNode<int>>();
            stack.Push(tree.Root);
            while (stack.Count > 0)
            {
                TreeNode<int> currentNode = stack.Pop();
                if (currentNode.ChildrenCount > 0)
                {
                    countInnerLeafs++;
                    for (int i = 0; i < currentNode.ChildrenCount; i++)
                    {
                        TreeNode<int> childNode = currentNode.GetChild(i);
                        stack.Push(childNode);
                    }
                }
                else countLeafs++;

            }
        }
    }
}

