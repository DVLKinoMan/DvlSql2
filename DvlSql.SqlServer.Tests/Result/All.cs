﻿using DvlSql.SqlServer.Tests.Classes;
using NUnit.Framework;
using static DvlSql.SqlServer.Tests.Result.Helpers;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer.Tests.Result;

[TestFixture]
public class All
{
    private const string TableName = "dbo.Words";

    #region Parameters

    private static readonly object[] ParametersWithoutWhere =
        {
            new object[]
            {
                new List<int>() {1, 2, 3}, true
            },
            new object[]
            {
                new List<string>(), true
            },
            new object[]
            {
                new List<SomeClass>()
                    {new(1, "David"), new(2, "Lasha"), new(3, "SomeGuy")},
                true
            },
            new object[]
            {
                new List<SomeClass>()
                    {},
                true
            }
        };

    private static readonly object[] ParametersWithWhereFor5Letters =
        new[]
        {
            new object[]
            {
                new List<SomeClass>()
                    {new(1, "David"), new (2, "Lasha"), new (3, "SomeGuy")},
                false
            },
            new object[]
            {
                new List<SomeClass>()
                    {new (1, "David"), new (-2, "Lasha"), new (3, "Tamta")},
                false
            }
        };
    #endregion

    [Test]
    [TestCaseSource(nameof(ParametersWithoutWhere))]
    public async Task AllWithoutWhere<T>(List<T> data, bool expected)
    {
        var readerMoq = CreateDataReaderMock(data);
        var commandMoq = CreateSqlCommandMock<T>(readerMoq);
        var moq = CreateConnectionMock<T>(commandMoq);

        var actual = await new DvlSqlMs(moq.Object)
            .From(TableName)
            .Select()
            .AllAsync();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [TestCaseSource(nameof(ParametersWithWhereFor5Letters))]
    public void AllWithWheref1IsPositive<T>(List<T> data, bool expected)
    {
        var readerMoq = CreateDataReaderMock(data);
        var commandMoq = CreateSqlCommandMock<bool>(readerMoq);
        var moq = CreateConnectionMock<bool>(commandMoq);

        var actual = new DvlSqlMs(moq.Object)
            .From(TableName)
            .Where(ConstantExpCol("f1") > 0)
            .Select()
            .AllAsync()
            .Result;

        Assert.That(actual, Is.EqualTo(expected));
    }
}