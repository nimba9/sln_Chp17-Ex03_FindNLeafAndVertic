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
                throw new ArgumentNullException("Cannot insert null value!");
            }

            if (child.hasParent)
            {
                throw new ArgumentException("The node already has a parent!");
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
                throw new ArgumentNullException("Cannot insert null value!");
            }

            this.root = new TreeNode<T>(value);
        }

        public Tree(T value, params Tree<T>[] children) : this(value)
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

        private void TraverseDFS(TreeNode<T> root, string spaces)
        {
            if (this.root == null)
            {
                return;
            }

            Console.WriteLine(spaces + root.Value);
            TreeNode<T> child = null;
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                child = root.GetChild(i);
                TraverseDFS(child, spaces + " ");
            }
        }

        public void TraverseDFS()
        {
            this.TraverseDFS(this.root, string.Empty);
        }

        public void TraverseBFS()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(this.root);
            while (queue.Count > 0)
            {
                TreeNode<T> currentNode = queue.Dequeue();
                Console.Write("{0} ", currentNode.Value);
                for (int i = 0; i < currentNode.ChildrenCount; i++)
                {
                    TreeNode<T> childNode = currentNode.GetChild(i);
                    queue.Enqueue(childNode);
                }
            }
        }

        public void TraverseDFSWithStack()
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(this.root);
            while (stack.Count > 0)
            {
                TreeNode<T> currentNode = stack.Pop();
                Console.Write("{0} ", currentNode.Value);
                for (int i = 0; i < currentNode.ChildrenCount; i++)
                {
                    TreeNode<T> childNode = currentNode.GetChild(i);
                    stack.Push(childNode);
                }
            }
        }
    }

    public class Main_Program
    {
        public static void Main(string[] args)
        {
            Tree<int> tree = new Tree<int>(1, new Tree<int>(2, new Tree<int>(21), new Tree<int>(24)), new Tree<int>(3), new Tree<int>(4, new Tree<int>(43), new Tree<int>(46)));
            CountInternalNodes(tree);
            Console.WriteLine();
            CountLeaves(tree);
        }

        static void CountInternalNodes(Tree<int> tree)
        {
            Stack<TreeNode<int>> myStack = new Stack<TreeNode<int>>();
            myStack.Push(tree.Root);
            Console.Write("Internal nodes: ");

            while (myStack.Count > 0)
            {
                TreeNode<int> currentNode = myStack.Pop();
                if (currentNode.ChildrenCount > 0)
                {
                    for (int i = 0; i < currentNode.ChildrenCount; i++)
                    {
                        TreeNode<int> childNode = currentNode.GetChild(i);
                        myStack.Push(childNode);
                    }

                    Console.Write(currentNode.Value + " ");
                }
            }
        }

        static void CountLeaves(Tree<int> tree)
        {
            Stack<TreeNode<int>> myStack = new Stack<TreeNode<int>>();
            myStack.Push(tree.Root);
            Console.Write("Leaves: ");

            while (myStack.Count > 0)
            {
                TreeNode<int> currentNode = myStack.Pop();
                if (currentNode.ChildrenCount > 0)
                {
                    for (int i = 0; i < currentNode.ChildrenCount; i++)
                    {
                        TreeNode<int> childNode = currentNode.GetChild(i);
                        myStack.Push(childNode);
                    }

                }

                else
                Console.Write(currentNode.Value + " ");
            }
        }
    }
}
