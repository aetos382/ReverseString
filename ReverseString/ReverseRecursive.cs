namespace ReverseString;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static partial class StringExtensions
{
    // 再帰処理版。
    // コードは自前版に比べてシンプルだが、比較的短い文字列でもスタック オーバーフローが起きやすい。
    [return: NotNullIfNotNull("str")]
    public static string? ReverseRecursive(
        this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return string.Create(str.Length, str, static (buffer, str) => {

            ReverseCore(str.AsSpan(), ref buffer);

        });

        static int ReverseCore(
            ReadOnlySpan<char> str,
            ref Span<char> buffer)
        {
            var length = StringInfo.GetNextTextElementLength(str);
            if (length == 0)
            {
                return 0;
            }

            var outputOffset = ReverseCore(str.Slice(length), ref buffer);
            str.Slice(0, length).CopyTo(buffer.Slice(outputOffset));

            return outputOffset + length;
        }
    }
}