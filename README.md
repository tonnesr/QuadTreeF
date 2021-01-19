[![License](https://img.shields.io/github/license/tonnesr/QuadTreeF?color=blue)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/Tonnes.QuadTreeF)](https://www.nuget.org/packages/Tonnes.QuadTreeF/)

# QuadTreeF
A generic QuadTree implementation where all nodes are stored in the leafs of the tree, and rectangles plus points are stored as floats.

## MonoGame

Extensions for MonoGame can be found [here](https://github.com/tonnesr/QuadTreeF.MonoGame)

## Usage example

```c#

  public void Examples() {
    // Create a new tree
    RectangleF bounds = new RectangleF(0, 0, 10, 10); 
    QuadTree<int> tree = new QuadTree<int>(bounds);

    // Add a node to the tree
    tree.Add(0f, 1f, 100); 
    tree.Add(new QuadNode<int>(0f, 1f, 100));

    // Remove a node from the tree
    tree.Remove(1f, 0f);
    tree.Remove(new PointF(1f, 0));

    // Try to get a node from a tree. Will return null if no node is found
    QuadNode<int> node = tree.Search(0f, 1f);
    QuadNode<int> node2 = tree.Search(new PointF(0f, 1f));

    // Check if the tree contains a node at a point
    bool containsNode = tree.Contains(0f, 1f); 
    bool containsNode2 = tree.Contains(new PointF(0f, 1f));

    // Clear all nodes (+ branches and leafs) from tree
    tree.Clear();

    // Check equality of two trees
    bool equals = tree.Equals(tree2);
  }

```

## Compatibility

