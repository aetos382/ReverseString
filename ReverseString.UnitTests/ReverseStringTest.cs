using System.Text;

using Xunit;

namespace ReverseString.UnitTests;

public class ReverseStringTest
{
    [Theory]
    [InlineData(null!, null!)]
    [InlineData("", "")]
    [InlineData("A", "A")]
    [InlineData("ABC", "CBA")]
    [InlineData("𩸽", "𩸽")]
    [InlineData("\xd867\xde3d", "\xd867\xde3d")]
    [InlineData("森鷗外𠮟る", "る𠮟外鷗森")]
    [InlineData("\U0001f468\x200d\U0001f469", "\U0001f468\x200d\U0001f469")]
    [InlineData("\U0001f468\U0001f3fb\x200d\U0001f469\U0001f3fc\U0001f466\U0001f467", "\U0001f467\U0001f466\U0001f468\U0001f3fb\x200d\U0001f469\U0001f3fc")]
    [InlineData("\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f", "\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f")]
    [InlineData("\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f\U0001f3f4\U000e0067\U000e0062\U000e006e\U000e0069\U000e0072\U000e007f", "\U0001f3f4\U000e0067\U000e0062\U000e006e\U000e0069\U000e0072\U000e007f\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f")]
    public void Test1(
        string? value,
        string? expected)
    {
        Assert.Equal(expected, value.Reverse());
    }

    [Theory]
    [InlineData(null!, null!)]
    [InlineData("", "")]
    [InlineData("A", "A")]
    [InlineData("ABC", "CBA")]
    [InlineData("𩸽", "𩸽")]
    [InlineData("\xd867\xde3d", "\xd867\xde3d")]
    [InlineData("森鷗外𠮟る", "る𠮟外鷗森")]
    [InlineData("\U0001f468\x200d\U0001f469", "\U0001f468\x200d\U0001f469")]
    [InlineData("\U0001f468\U0001f3fb\x200d\U0001f469\U0001f3fc\U0001f466\U0001f467", "\U0001f467\U0001f466\U0001f468\U0001f3fb\x200d\U0001f469\U0001f3fc")]
    [InlineData("\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f", "\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f")]
    [InlineData("\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f\U0001f3f4\U000e0067\U000e0062\U000e006e\U000e0069\U000e0072\U000e007f", "\U0001f3f4\U000e0067\U000e0062\U000e006e\U000e0069\U000e0072\U000e007f\U0001f3f4\U000e0067\U000e0062\U000e0065\U000e006e\U000e0067\U000e007f")]
    public void Test2(
        string? value,
        string? expected)
    {
        Assert.Equal(expected, value.ReverseByLinq());
    }

    [Fact]
    public void Test3()
    {
        const string str = "𩸽";
        const int length = 100000;

        var builder = new StringBuilder(str.Length * length);

        for (int i = 0; i < length; ++i)
        {
            builder.Append(str);
        }

        var x = builder.ToString();
        var y = x.Reverse();

        Assert.Equal(x, y);
    }
}
