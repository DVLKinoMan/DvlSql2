using System.Data;
using DvlSql.SqlServer.Tests.Classes;
using Moq;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;
using static DvlSql.SqlServer.Tests.Result.Helpers;

namespace DvlSql.SqlServer.Tests.Result;

[TestFixture]
public class Any
{
    private const string TableName = "dbo.Words";

    #region Parameters

    private static readonly object[] ParametersWithoutWhere =
        new[]
        {
            [
                new List<int>() {1, 2, 3}, true
            ],
            [
                new List<string>(), false
            ],
            [
                new List<SomeClass>()
                    {new (1, "David"), new (2, "Lasha"), new (3, "SomeGuy")},
                true
            ],
            new object[]
            {
                new List<SomeClass>(),
                false
            }
        };

    private static readonly object[] ParametersWithWhereFor5Letters =
        new[]
        {
            [
                new List<SomeClass>()
                    {new(1, "David"), new(2, "Lasha"), new(3, "SomeGuy")},
                true
            ],
            new object[]
            {
                new List<SomeClass>()
                    {new(-1, "David"), new(-2, "Lasha"), new(-3, "SomeGuy")},
                true
            }
        };
    #endregion


    public static Mock<IDvlSqlConnection> CreateConnectionMockWithSqlStringContains<T>(Mock<IDvlSqlCommand> commandMoq, CommandType commandType = CommandType.Text)
    {
        var moq = new Mock<IDvlSqlConnection>();

        moq.Setup(m => m.ConnectAsync(It.IsAny<Func<IDvlSqlCommand, Task<T>>>(),
                It.Is<string>(s => s.Contains("SELECT TOP 1")), It.Is<CommandType>(com => com == commandType),
                It.IsAny<DvlSqlParameter[]>()))
            .Returns((Func<IDvlSqlCommand, Task<T>> func, string _,
                CommandType _, DvlSqlParameter[] _) => func(commandMoq.Object));

        return moq;
    }

    [Test]
    [TestCaseSource(nameof(ParametersWithoutWhere))]
    public void AnyWithoutWhere<T>(List<T> data, bool expected)
    {
        var readerMoq = CreateDataReaderMock(data);
        var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
        var moq = CreateConnectionMockWithSqlStringContains<bool>(commandMoq);

        var actual = new DvlSqlMs(moq.Object, new DvlSqlOptions())
            .From(TableName)
            .Select()
            .AnyAsync()
            .Result;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCaseSource(nameof(ParametersWithWhereFor5Letters))]
    public void AnyWithWheref1IsPositive<T>(List<T> data, bool expected)
    {
        var readerMoq = CreateDataReaderMock(data);
        var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
        var moq = CreateConnectionMockWithSqlStringContains<bool>(commandMoq);

        var actual = new DvlSqlMs(moq.Object, new DvlSqlOptions())
            .From(TableName)
            .Where(ConstantExpCol("f1") > 0)
            .Select()
            .AnyAsync()
            .Result;

        Assert.That(actual, Is.EqualTo(expected));
    }
}