namespace ReverseString;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static partial class StringExtensions
{
    // お手軽 LINQ 版。
    // 再帰版よりスタック オーバーフローは起きづらいが、一番遅いしメモリ消費量も一番多い。
    [return: NotNullIfNotNull("str")]
    public static string? ReverseByLinq(
        this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return string.Join(null, str.EnumerateTextElement().Reverse());
    }

    public static IEnumerable<string> EnumerateTextElement(
        this string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.Length == 0)
        {
            yield break;
        }

        var e = StringInfo.GetTextElementEnumerator(str);
        while (e.MoveNext())
        {
            yield return e.GetTextElement();
        }
    }
}
