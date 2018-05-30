using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    [TestClass]
    public class TopologicalSorting
    {
        class Project
        {
            public string Name { get; private set; }
            public string[] Dependencies { get; private set; }

            public Project(string name, params string[] dependencies)
            {
                Name = name;
                Dependencies = dependencies;
            }
        }

        [TestMethod]
        public void Sort()
        {
            var a = new Project("A", "B");
            var b = new Project("B", "C");
            var c = new Project("C");

            var unsorted = new[] { a, b, c };
            var expected = new[] { c, b, a };

            SoftwareUnderTest(unsorted, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CyclicDependency()
        {
            var a = new Project("A", "B");
            var b = new Project("B", "C");
            var c = new Project("C", "A");

            var unsorted = new[] { a, b, c };
            SoftwareUnderTest(unsorted, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissignDependency()
        {
            var a = new Project("A", "B", "X");
            var b = new Project("B", "C");
            var c = new Project("C");

            var unsorted = new[] { a, b, c };

            SoftwareUnderTest(unsorted, null);
        }

        private void SoftwareUnderTest(Project[] unsorted, Project[] expected)
        {
            var actual = unsorted.TopoSort(x => x.Name, x => x.Dependencies).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }

    public class WaitList<TItem, TKey>
    {
        class Node<T>
        {
            private int dependencyCount;
            public T Item { get; private set; }

            public Node(T item, int dependencyCount)
            {
                Item = item;
                this.dependencyCount = dependencyCount;
            }

            public bool DecreaseDependencyCount()
            {
                dependencyCount--;
                return (dependencyCount == 0);
            }
        }

        private readonly Dictionary<TKey, List<Node<TItem>>> dependencies = new Dictionary<TKey, List<Node<TItem>>>();

        public void Add(TItem item, ICollection<TKey> pendingDependencies)
        {
            var node = new Node<TItem>(item, pendingDependencies.Count);

            foreach (var dependency in pendingDependencies)
            {
                Add(dependency, node);
            }
        }

        public IEnumerable<TItem> Remove(TKey key)
        {
            List<Node<TItem>> nodeList;
            var found = dependencies.TryGetValue(key, out nodeList);

            if (found)
            {
                dependencies.Remove(key);
                return nodeList.Where(x => x.DecreaseDependencyCount()).Select(x => x.Item);
            }

            return null;
        }

        private void Add(TKey key, Node<TItem> node)
        {
            List<Node<TItem>> nodeList;
            var found = dependencies.TryGetValue(key, out nodeList);

            if (!found)
            {
                nodeList = new List<Node<TItem>>();
                dependencies.Add(key, nodeList);
            }

            nodeList.Add(node);
        }

        public int Count
        {
            get { return dependencies.Count; }
        }
    }

    public class TopoSortEnumerator<TItem, TKey> : IEnumerator<TItem>
    {
        private readonly IEnumerator<TItem> source;
        private readonly Func<TItem, TKey> getKey;
        private readonly Func<TItem, IEnumerable<TKey>> getDependencies;
        private readonly HashSet<TKey> sortedItems;
        private readonly Queue<TItem> readyToOutput;
        private readonly WaitList<TItem, TKey> waitList = new WaitList<TItem, TKey>();

        private TItem current;

        public TopoSortEnumerator(IEnumerable<TItem> source, Func<TItem, TKey> getKey, Func<TItem, IEnumerable<TKey>> getDependencies)
        {
            this.source = source.GetEnumerator();
            this.getKey = getKey;
            this.getDependencies = getDependencies;

            readyToOutput = new Queue<TItem>();
            sortedItems = new HashSet<TKey>();
        }

        public TItem Current
        {
            get { return current; }
        }

        public void Dispose()
        {
            source.Dispose();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            while (true)
            {
                if (readyToOutput.Count > 0)
                {
                    current = readyToOutput.Dequeue();
                    Release(current);
                    return true;
                }

                if (!source.MoveNext())
                {
                    break;
                }

                Process(source.Current);
            }

            if (waitList.Count > 0)
            {
                throw new ArgumentException("Cyclic dependency or missing dependency.");
            }

            return false;
        }

        public void Reset()
        {
            source.Reset();
            sortedItems.Clear();
            readyToOutput.Clear();
            current = default(TItem);
        }

        private void Process(TItem item)
        {
            var pendingDependencies = getDependencies(item)
                .Where(key => !sortedItems.Contains(key))
                .ToArray();

            if (pendingDependencies.Length > 0)
            {
                waitList.Add(item, pendingDependencies);
            }
            else
            {
                readyToOutput.Enqueue(item);
            }
        }

        private void Release(TItem item)
        {
            var key = getKey(item);
            sortedItems.Add(key);

            var releasedItems = waitList.Remove(key);
            if (releasedItems != null)
            {
                foreach (var releasedItem in releasedItems)
                {
                    readyToOutput.Enqueue(releasedItem);
                }
            }
        }
    }

    public static class Extensions
    {
        class DummyEnumerable<T> : IEnumerable<T>
        {
            private readonly Func<IEnumerator<T>> getEnumerator;

            public DummyEnumerable(Func<IEnumerator<T>> getEnumerator)
            {
                this.getEnumerator = getEnumerator;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return getEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public static IEnumerable<TItem> TopoSort<TItem, TKey>(this IEnumerable<TItem> source, Func<TItem, TKey> getKey, Func<TItem, IEnumerable<TKey>> getDependencies)
        {
            var enumerator = new TopoSortEnumerator<TItem, TKey>(source, getKey, getDependencies);
            return new DummyEnumerable<TItem>(() => enumerator);
        }
    }
}
