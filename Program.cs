using System;

class Program
{
    static void Main()
    {
   
        BinaryTree<int> tree = new BinaryTree<int>();

      
        tree.Add(5);
        tree.Add(3);
        tree.Add(7);
        tree.Add(2);
        tree.Add(4);
        tree.Add(6);
        tree.Add(8);

        Console.WriteLine("In-order traversal (foreach):");
        foreach (var item in tree)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("\n");

        Console.WriteLine("Pre-order traversal:");
        foreach (var item in tree.PreOrderTraversal())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("\n");

        Console.WriteLine("Post-order traversal:");
        foreach (var item in tree.PostOrderTraversal())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("\n");

        Console.WriteLine("In-order traversal (lambda):");
        foreach (var item in tree.InOrderLambda())
        {
            Console.Write(item + " ");
        }
        Console.WriteLine("\n");

       
        Console.WriteLine("Testing PostOrderIterator.Previous():");
        var postIterator = new BinaryTree<int>.PostOrderIterator(tree.Root);
        while (postIterator.MoveNext())
        {
            Console.Write(postIterator.Current + " ");
        }
        Console.WriteLine("\nNow going backwards:");
        while (postIterator.Previous())
        {
            Console.Write(postIterator.Current + " ");
        }
    }
}