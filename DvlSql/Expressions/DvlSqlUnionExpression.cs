using System.Collections;

namespace DvlSql.Expressions;

public class DvlSqlUnionExpression : DvlSqlExpression, IList<(DvlSqlFullSelectExpression Expression, UnionType? Type)>
{
    private readonly List<(DvlSqlFullSelectExpression expression, UnionType? type)> _selectExpressions = [];

    public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
    public override DvlSqlExpression Clone()
    {
        throw new NotImplementedException();
    }

    IEnumerator<(DvlSqlFullSelectExpression, UnionType?)> IEnumerable<(DvlSqlFullSelectExpression Expression, UnionType? Type)>.GetEnumerator() => 
        new UnionExpressionEnumerator(_selectExpressions);

    public IEnumerator<(DvlSqlFullSelectExpression Expression, UnionType? Type)> GetEnumerator() => 
        new UnionExpressionEnumerator(_selectExpressions);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public void Add((DvlSqlFullSelectExpression, UnionType?) item) => 
        _selectExpressions.Add(item);
    
    public void Add(DvlSqlFullSelectExpression item) => 
        _selectExpressions.Add((item, default));

    public void Clear() => _selectExpressions.Clear();

    public bool Contains((DvlSqlFullSelectExpression, UnionType?) item) => 
        _selectExpressions.Contains(item);

    public void CopyTo((DvlSqlFullSelectExpression, UnionType?)[] array, int arrayIndex) => 
        _selectExpressions.CopyTo(array, arrayIndex);

    public bool Remove((DvlSqlFullSelectExpression, UnionType?) item) =>
        _selectExpressions.Remove(item);

    public int Count => _selectExpressions.Count;

    public bool IsReadOnly => false;

    public int IndexOf((DvlSqlFullSelectExpression, UnionType?) item) =>
        _selectExpressions.IndexOf(item);

    public void Insert(int index, (DvlSqlFullSelectExpression, UnionType?) item) =>
        _selectExpressions.Insert(index, item);

    public void RemoveAt(int index) =>
        _selectExpressions.RemoveAt(index);

    public (DvlSqlFullSelectExpression Expression, UnionType? Type) this[int index]
    {
        get => _selectExpressions[index];
        set => _selectExpressions[index] = value;
    }
}

public class UnionExpressionEnumerator(List<(DvlSqlFullSelectExpression, UnionType?)> selectExpressions) : IEnumerator<(DvlSqlFullSelectExpression Expression, UnionType? Type)>
{
    private readonly List<(DvlSqlFullSelectExpression, UnionType?)> _selectExpressions = selectExpressions;
    private int _position = -1;

    public bool MoveNext()
    {
        _position++;
        return _position < _selectExpressions.Count;
    }

    public void Reset() => _position = -1;

    public (DvlSqlFullSelectExpression Expression, UnionType? Type) Current
    {
        get
        {
            try
            {
                return _selectExpressions[_position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose() => Reset();
}

public enum UnionType
{
    Union,
    UnionAll
}