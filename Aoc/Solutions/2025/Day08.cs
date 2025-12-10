namespace Aoc.Solutions._2025;

public sealed class Day08 : IAocSolution
{
    public string SolvePart1(string[] input)
    {
        var points = ParsePoints(input);
        var sortedPairs = GetSortedPairs(points);
        var uf = new UnionFind(points.Length);
        
        var connectionsToMake = points.Length <= 20 ? 10 : 1000;
        
        for (var k = 0; k < connectionsToMake && k < sortedPairs.Length; k++)
        {
            var (i, j) = sortedPairs[k];
            uf.Union(i, j);
        }
        
        var top3 = uf.GetTopComponentSizes(3);
        return top3.Aggregate(1L, (acc, x) => acc * x).ToString();
    }

    public string SolvePart2(string[] input) 
    {
        var points = ParsePoints(input);
        var sortedPairs = GetSortedPairs(points);
        var uf = new UnionFind(points.Length);
        
        var (lastI, lastJ) = (0, 0);
        
        foreach (var (i, j) in sortedPairs)
        {
            if (!uf.TryUnion(i, j)) continue;
            
            (lastI, lastJ) = (i, j);
            
            if (uf.ComponentCount == 1) break;
        }
        
        return (points[lastI].X * points[lastJ].X).ToString();
    }
    
    private static (long X, long Y, long Z)[] ParsePoints(string[] input) =>
        input
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(','))
            .Select(p => (long.Parse(p[0]), long.Parse(p[1]), long.Parse(p[2])))
            .ToArray();

    private static (int I, int J)[] GetSortedPairs((long X, long Y, long Z)[] points)
    {
        var n = points.Length;
        var pairs = new (int I, int J, long DistSq)[n * (n - 1) / 2];
        var idx = 0;
        
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var dx = points[i].X - points[j].X;
                var dy = points[i].Y - points[j].Y;
                var dz = points[i].Z - points[j].Z;
                pairs[idx++] = (i, j, dx * dx + dy * dy + dz * dz);
            }
        }
        
        Array.Sort(pairs, (a, b) => a.DistSq.CompareTo(b.DistSq));
        return pairs.Select(p => (p.I, p.J)).ToArray();
    }
    
    private sealed class UnionFind(int n)
    {
        private readonly int[] _parent = Enumerable.Range(0, n).ToArray();
        private readonly int[] _size = Enumerable.Repeat(1, n).ToArray();
        
        public int ComponentCount { get; private set; } = n;

        private int Find(int x)
        {
            if (_parent[x] != x)
                _parent[x] = Find(_parent[x]);
            return _parent[x];
        }
        
        public bool TryUnion(int x, int y)
        {
            int px = Find(x), py = Find(y);
            if (px == py) return false;
            
            if (_size[px] < _size[py]) (px, py) = (py, px);
            _parent[py] = px;
            _size[px] += _size[py];
            ComponentCount--;
            return true;
        }
        
        public void Union(int x, int y) => TryUnion(x, y);
        
        public IEnumerable<int> GetTopComponentSizes(int count) =>
            Enumerable.Range(0, _parent.Length)
                .Where(i => _parent[i] == i)
                .Select(i => _size[i])
                .OrderByDescending(x => x)
                .Take(count);
    }
}
