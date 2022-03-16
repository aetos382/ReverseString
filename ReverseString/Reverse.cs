namespace ReverseString;

using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static partial class StringExtensions
{
    // 自前でインデックスを管理する版。
    // コードはやや複雑だが、速くてメモリ消費も少ない。
    [return: NotNullIfNotNull("str")]
    public static string? Reverse(
        this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return string.Create(str.Length, str, static (buffer, str) => {

            const int MaximumStackAllocationLength = 256;

            var pool = ArrayPool<int>.Shared;
            int[]? arrayToReturnToPool = null;

            var indicesLength = str.Length + 1;

            try
            {
                Span<int> indices = indicesLength <= MaximumStackAllocationLength
                    ? stackalloc int[indicesLength]
                    : arrayToReturnToPool = pool.Rent(indicesLength);

                var source = str.AsSpan();

                for (int i = 0, index = 0;; ++i)
                {
                    indices[i] = index;

                    var length = StringInfo.GetNextTextElementLength(source.Slice(index));
                    if (length == 0)
                    {
                        indicesLength = i + 1;
                        break;
                    }

                    index += length;
                }

                for (int i = indicesLength - 1; i >= 1; --i)
                {
                    var start = indices[i - 1];
                    var length = indices[i] - start;

                    source.Slice(start, length).CopyTo(buffer);
                    buffer = buffer.Slice(length);
                }
            }
            finally
            {
                if (arrayToReturnToPool is not null)
                {
                    pool.Return(arrayToReturnToPool);
                }
            }
        });
    }
}
