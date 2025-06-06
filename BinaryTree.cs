using System;
using System.Collections;
using System.Collections.Generic;

public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
{
    public class Node
    {
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node root;

    public void Add(T value)
    {
        if (root == null)
        {
            root = new Node(value);
            return;
        }

        AddRecursive(root, value);
    }

    private void AddRecursive(Node node, T value)
    {
        if (value.CompareTo(node.Value) < 0)
        {
            if (node.Left == null)
            {
                node.Left = new Node(value) { Parent = node };
            }
            else
            {
                AddRecursive(node.Left, value);
            }
        }
        else
        {
            if (node.Right == null)
            {
                node.Right = new Node(value) { Parent = node };
            }
            else
            {
                AddRecursive(node.Right, value);
            }
        }
    }


    public class PreOrderIterator : IEnumerator<T>
    {
        private Node current;
        private readonly Node root;
        private readonly Stack<Node> stack;

        public PreOrderIterator(Node root)
        {
            this.root = root;
            stack = new Stack<Node>();
            if (root != null) stack.Push(root);
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (stack.Count == 0)
            {
                current = null;
                return false;
            }

            current = stack.Pop();
            if (current.Right != null) stack.Push(current.Right);
            if (current.Left != null) stack.Push(current.Left);

            return true;
        }

        public void Reset()
        {
            stack.Clear();
            if (root != null) stack.Push(root);
            current = null;
        }
    }

 
    public class PostOrderIterator : IEnumerator<T>
    {
        private Node current;
        private readonly Node root;
        private readonly Stack<Node> stack;
        private readonly HashSet<Node> visited;

        public PostOrderIterator(Node root)
        {
            this.root = root;
            stack = new Stack<Node>();
            visited = new HashSet<Node>();
            if (root != null) stack.Push(root);
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            while (stack.Count > 0)
            {
                current = stack.Peek();

                if (visited.Contains(current))
                {
                    stack.Pop();
                    return true;
                }

                visited.Add(current);
                if (current.Right != null) stack.Push(current.Right);
                if (current.Left != null) stack.Push(current.Left);
            }

            current = null;
            return false;
        }

        public void Reset()
        {
            stack.Clear();
            visited.Clear();
            if (root != null) stack.Push(root);
            current = null;
        }

        public bool Previous()
        {
            if (current == null) return false;

            if (current.Right == null)
            {
                if (current.Parent == null || current == current.Parent.Right)
                {
                    current = null;
                    return false;
                }

                current = current.Parent;
                return true;
            }

            Node y = current.Right.Left;
            if (y == null)
            {
                current = current.Right;
                return true;
            }

            while (y.Left != null)
            {
                y = y.Left;
            }
            current = y;
            return true;
        }
    }

  
    public class InOrderIterator : IEnumerator<T>
    {
        private Node current;
        private readonly Node root;
        private readonly Stack<Node> stack;

        public InOrderIterator(Node root)
        {
            this.root = root;
            stack = new Stack<Node>();
            PushLeftNodes(root);
        }

        private void PushLeftNodes(Node node)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node.Left;
            }
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (stack.Count == 0)
            {
                current = null;
                return false;
            }

            current = stack.Pop();
            PushLeftNodes(current.Right);
            return true;
        }

        public void Reset()
        {
            stack.Clear();
            PushLeftNodes(root);
            current = null;
        }
    }

   
    public IEnumerator<T> GetEnumerator()
    {
        return new InOrderIterator(root);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public IEnumerable<T> PreOrderTraversal()
    {
        return new IterableWrapper(new PreOrderIterator(root));
    }

    public IEnumerable<T> PostOrderTraversal()
    {
        return new IterableWrapper(new PostOrderIterator(root));
    }

    public IEnumerable<T> InOrderTraversal()
    {
        return new IterableWrapper(new InOrderIterator(root));
    }


    public IEnumerable<T> InOrderLambda()
    {
        List<T> result = new List<T>();
        InOrderLambda(root, result);
        return result;
    }

    private void InOrderLambda(Node node, List<T> result)
    {
        if (node == null) return;
        InOrderLambda(node.Left, result);
        result.Add(node.Value);
        InOrderLambda(node.Right, result);
    }

 
    private class IterableWrapper : IEnumerable<T>
    {
        private readonly IEnumerator<T> enumerator;

        public IterableWrapper(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
            this.enumerator.Reset();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

  
    public Node Root => root;
}