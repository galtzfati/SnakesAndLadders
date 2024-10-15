using SnakesAndLadders.Models;

namespace SnakesAndLadders.GameLogic;

internal class GameEndpointsRandomizer
{
    private readonly Random _random = new Random();
    private readonly List<int> _allRows;
    private readonly Dictionary<int, List<int>> _rowToPositions;
    private readonly int _boardSize;

    public GameEndpointsRandomizer(int boardSize)
    {
        _boardSize = boardSize;
        _allRows = Enumerable.Range(1, _boardSize).ToList();
        _rowToPositions = getRowToPositionsMapping(_allRows);
    }
    public Link<int> CreateUniqueRandomLink()
    {
        if (_allRows.Count <= 1)
        {
            throw new InvalidOperationException("The maximum number of links has been reached.");
        }
        int endpoint1 = createUniqueRandomEndpoint(out int endpoint1Row);
        
        int endpoint2Row = _allRows.Except([endpoint1Row]).ToList()[_random.Next(_allRows.Count - 1)];
        int endpoint2 = _rowToPositions[endpoint2Row][_random.Next(_rowToPositions[endpoint2Row].Count)];

        _rowToPositions[endpoint2Row].Remove(endpoint2);

        if (_rowToPositions[endpoint2Row].Count == 0)
        {
            _rowToPositions.Remove(endpoint2Row);
            _allRows.Remove(endpoint2Row);
        }

        return new Link<int>(endpoint1, endpoint2);
    }
    public int CreateUniqueRandomEndpoint()
    {
        if (_allRows.Count == 0)
        {
            throw new InvalidOperationException("The maximum number of endpoints has been reached.");
        }

        return createUniqueRandomEndpoint(out int _);
    }
    private int createUniqueRandomEndpoint(out int endpointRow)
    {
        endpointRow = _allRows[_random.Next(_allRows.Count)];
        int endpoint = _rowToPositions[endpointRow][_random.Next(_rowToPositions[endpointRow].Count)];

        _rowToPositions[endpointRow].Remove(endpoint);

        if (_rowToPositions[endpointRow].Count == 0)
        {
            _rowToPositions.Remove(endpointRow);
            _allRows.Remove(endpointRow);
        }

        return endpoint;
    }
    private Dictionary<int, List<int>> getRowToPositionsMapping(IEnumerable<int> rows)
    {
        Dictionary<int, List<int>> rowToPositions = new Dictionary<int, List<int>>();

        foreach (int row in rows)
        {
            rowToPositions[row] = Enumerable.Range((row - 1) * _boardSize + 1, _boardSize).ToList();
        }

        // remove winning position
        rowToPositions[_boardSize].Remove(_boardSize * _boardSize);

        return rowToPositions;
    }
}
