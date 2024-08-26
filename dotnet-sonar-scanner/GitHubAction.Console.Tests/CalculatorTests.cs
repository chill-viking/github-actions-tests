using GitHubAction.ClassLibrary;
using NSubstitute;

namespace GitHubAction.Console.Tests;

public class CalculatorTests
{
    private IMathsHelper _goodMaths;
    private IMathsHelper _badMaths;

    private Calculator _calculator = null!;

    [SetUp]
    public void BeforeEach()
    {
        _badMaths = Substitute.For<IMathsHelper>();
        _goodMaths = Substitute.For<IMathsHelper>();

        _calculator = new Calculator(_goodMaths, _badMaths);
    }

    [Test]
    public void GetSum_CorrectIsTrue_UsesGoodMaths()
    {
        _goodMaths
            .Sum(Arg.Any<int>(), Arg.Any<int>())
            .Returns(15);

        var actual = _calculator.GetSum(1, 2);

        _goodMaths.Received().Sum(1, 2);
        Assert.That(actual, Is.EqualTo(15));
    }

    [Test]
    public void GetSum_CorrectIsFalse_UsesBadMaths()
    {
        _badMaths
            .Sum(Arg.Any<int>(), Arg.Any<int>())
            .Returns(123);

        var actual = _calculator.GetSum(1, 2, false);

        _badMaths.Received().Sum(1, 2);
        Assert.That(actual, Is.EqualTo(123));
    }
}
