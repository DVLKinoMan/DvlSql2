using System.Text.RegularExpressions;
using DvlSql.Expressions;
using NUnit.Framework;
using static DvlSql.ExpressionHelpers;

namespace DvlSql.SqlServer.Tests.Select;

[TestFixture]
public class Where
{
    private readonly DvlSqlMs _sql =
        new(
            StaticConnectionStrings.ConnectionStringForTest);

    private const string TableName = "dbo.Words";

    [Test]
    [TestCase("Chars", 1)]
    public void WithIntConstantExpression(string colName, int value)
    {
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(colName) == value)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {colName} = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Text", "David")]
    public void WithStringConstantExpression(string colName, string value)
    {
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(colName) == value)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {colName} = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount", 5.25)]
    public void WithDoubleConstantExpression(string colName, double value)
    {
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(colName) == value)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {colName} = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    public void WithDateTimeConstantExpression()
    {
        var dateTime = new DateTime(2019, 11, 11);
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol("CreatedTime") == dateTime)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE CreatedTime = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount", 5.25)]
    [TestCase("Number", 2.9)]
    public void WithGreaterExpression(string colName, double value)
    {
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(colName) > value)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {colName} > @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("text")]
    public void WithIsNullExpression(string fieldName)
    {
        var actualSelect = _sql.From(TableName)
            .Where(IsNullExp(fieldName))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {fieldName} IS NULL"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("text")]
    public void WithIsNotNullExpression(string fieldName)
    {
        var actualSelect1 = _sql.From(TableName)
            .Where(IsNotNullExp(fieldName))
            .Select()
            .ToString();

        var actualSelect2 = _sql.From(TableName)
            .Where(!IsNullExp(fieldName))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {fieldName} IS NOT NULL"
        );
        Assert.Multiple(
            () =>
            {
                Assert.That(Regex.Escape(actualSelect1!), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2!), Is.EqualTo(expectedSelect));
            });
    }

    [Test]
    [TestCase("Name", "%d")]
    public void WithLikeExpression(string fieldName, string pattern)
    {
        var actualSelect = _sql.From(TableName)
            .Where(LikeExp(fieldName, pattern))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {fieldName} LIKE '{pattern}'"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Name", "%d")]
    public void WithNotLikeExpression(string fieldName, string pattern)
    {
        var actualSelect1 = _sql.From(TableName)
            .Where(NotLikeExp(fieldName, pattern))
            .Select()
            .ToString();

        var actualSelect2 = _sql.From(TableName)
            .Where(!LikeExp(fieldName, pattern))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {fieldName} NOT LIKE '{pattern}'"
        );

        Assert.Multiple(
            () =>
            {
                Assert.That(Regex.Escape(actualSelect1!), Is.EqualTo(expectedSelect));
                Assert.That(Regex.Escape(actualSelect2!), Is.EqualTo(expectedSelect));
            });
    }

    [Test]
    [TestCase("Id")]
    public void WithInExpression(string col)
    {
        var actualSelect = _sql.From(TableName)
            .Where(InExp(col, 1, 2, 3))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col} IN ( @p1, @p2, @p3 )"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Id", "dbo.Sentences")]
    public void WithInSelectExpression(string col, string tableName)
    {
        var actualSelect = _sql.From(TableName)
            .Where(InExp(col, FullSelectExp(SelectExp(col), FromExp(tableName))))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col} IN ( SELECT {col} FROM {tableName} )"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    public void AndWithoutInnerExpressions()
    {
        var actualSelect = _sql.From(TableName)
            .Where(AndExp())
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    public void OrWithoutInnerExpressions()
    {
        var actualSelect = _sql.From(TableName)
            .Where(OrExp())
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount", "Chars")]
    public void AndWithConstantExpressions(string col1, string col2)
    {
        var number = 5.25;
        int secondNumber = 3;
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(col1) == number &
                   ConstantExpCol(col2) == secondNumber)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE ( {col1} = @p1 AND {col2} = @p2 )"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount", "Chars")]
    public void OrWithConstantExpressions(string col1, string col2)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(ConstantExpCol(col1) == number |
                   ConstantExpCol(col2) == 3)
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE ( {col1} = @p1 OR {col2} = @p2 )"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotEqualityWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) == number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} <> @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotLessWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) < number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} >= @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotGreaterWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) > number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} <= @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotLessOrEqualWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) <= number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} > @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotGreaterOrEqualWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) >= number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} < @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotNotGreaterWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) > number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} <= @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotNotLessWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) < number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} >= @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("Amount")]
    public void NotNotEqualWithConstantExpressions(string col1)
    {
        var number = 5.25;
        var actualSelect = _sql.From(TableName)
            .Where(!(ConstantExpCol(col1) != number))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {col1} = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }

    [Test]
    [TestCase("OtherTable")]
    public void WithExistsExpression(string tableName)
    {
        var actualSelect = _sql.From(TableName)
            .Where(ExistsExp(FullSelectExp(SelectExp(), FromExp(tableName))))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE EXISTS( SELECT * FROM {tableName} )");

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }
    
    [Test]
    [TestCase("Amount", 5)]
    public void WithIntMemberExpression(string memberName, int value)
    {
        var actualSelect = _sql.From(TableName)
            .Where(1 <= MemberExpInt(memberName))
            .Select()
            .ToString();

        var expectedSelect = Regex.Escape(
            $"SELECT * FROM {TableName}{Environment.NewLine}" +
            $"WHERE {memberName} = @p1"
        );

        Assert.That(Regex.Escape(actualSelect!), Is.EqualTo(expectedSelect));
    }
}