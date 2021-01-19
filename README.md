[![License](https://flat.badgen.net/github/license/micromatch/micromatch)](https://opensource.org/licenses/MIT)

# QuadTreeF
A generic QuadTree implementation where all nodes are stored in the leafs of the tree, and rectangles plus points are stored as floats.

## Usage example:

```c#
  // Create a new tree
  public void CreateTree() {
    RectangleF bounds = new RectangleF(0, 0, 10, 10); // Bounds/Rectangle of tree
    QuadTree<int> tree = new QuadTree<int>(bounds, 5); // Construct a new QuadTree instance with (0, 0, 10, 10) bounds and max 5 nodes per leaf
  }

  // Add nodes to trees
  public void AddNode(QuadTree<int> tree) { // Adds a node at x: 0, y: 1 with value 100
    tree.Add(0f, 1f, 100); 
    tree.Add(new QuadNode<int>(0f, 1f, 100));
  }

  // Remove nodes from trees
  public void RemoveNode(QuadTree<int> tree) { // Removes node at x: 1, y: 0
    tree.Remove(1f, 0f);
    tree.Remove(new PointF(1f, 0));
  }

  // Get nodes from trees
  public void GetNodes(QuadTree<int> tree) { // Returns null if not found
    QuadNode<int> node = tree.Search(0f, 1f); 
    QuadNode<int> node2 = tree.Search(new PointF(0f, 1f));
  }

  // Check if tree contains a node at point
  public void TreeContainsNode(QuadTree<int> tree) { // Checks if tree contains a note at point x: 0, y: 1
    bool containsNode = tree.Contains(0f, 1f); 
    bool containsNode = tree.Contains(new PointF(0f, 1f));
  }

  // Clear all nodes from tree
  public void ClearTree(QuadTree<int> tree) => tree.Clear(); // Removes all nodes and leafs/branches in tree. Root bounds and max nodes remains.

  // Check equality of two trees
  public bool IsEqual(QuadTree<int> tree, QuadTree<int> tree2) => tree?.Equals(tree2) ?? false;
```
