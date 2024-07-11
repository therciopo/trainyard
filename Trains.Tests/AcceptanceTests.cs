using Xunit;

namespace Trains.Tests;

public class AcceptanceTests
{
    private readonly ITrainsStarter _trainStarter;

    public AcceptanceTests()
    {
        this._trainStarter = new TrainsStarter();
    }



    [Fact]
    public void SimpleTestCase()
    {
        var test = new[] { "00000ACDGC", "00000000DG" };
        
        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal("A,1,2;C,1,0;DG,1,2;C,1,0", result);
    }

    [Fact]
    public void IntermediateTestCase()
    {
        var test = new[] { "00000AGCAG", "000DCACGDG" };        

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal("AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0", result);
    }

    [Fact]
    public void ComplexTestCase()
    {
        var test = new[]
        {
            "00DGCDGEFA",
            "0ACCACCGDE",
            "CDGADGADEE",
            "DGDGADGCAA",
            "0AADGADCGD",
            "ACGDCGDEGD",
        };

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal("D,1,2;G,1,5;C,1,0;DA,2,1;C,2,0;C,2,0;A,2,1;C,2,0;C,2,0;C,3,0;DGD,4,2;GAD,4,2;G,4,3;C,4,0;GAA,5,4;DGA,5,4;D,5,4;C,5,0;A,6,5;C,6,0;GD,6,5;C,6,0", result);
    }

    [Fact]
    public void ImpossibleTestCase()
    {
        var test = new[] { "AAAAAAAAAA", "AAAAACAAAA" };

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void MoreThan10TestCase()
    {
        var test = new[] { "0000AAAAAAAAAA", "AAAAACAAAA" };

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void SimpleTestCaseWithDestinationLast()
    {
        var test = new[] { "0000000000", "CAAAAAAAAA" };

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal("C,2,0", result);
    }
    [Fact]
    public void SimpleTestCaseWithDestinationSecondLast()
    {
        var test = new[] { "0000000000", "0CAAAAAAAA" };

        var result = this._trainStarter.Start(test, 'C');

        Assert.Equal("C,2,0", result);
    }
}