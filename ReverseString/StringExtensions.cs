﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace ReverseString;

public static class StringExtensions
{
    [return: NotNullIfNotNull("str")]
    public static string? Reverse(
        this string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return string.Create(str.Length, str, static (buffer, str) => {

            const int MaximumStackAllocateSize = 256;

            var pool = ArrayPool<int>.Shared;
            int[]? arrayToReturnToPool = null;

            var indexesLength = str.Length + 1;

            try
            {
                Span<int> indexes = indexesLength <= MaximumStackAllocateSize
                    ? stackalloc int[indexesLength]
                    : arrayToReturnToPool = pool.Rent(indexesLength);

                var source = str.AsSpan();

                for (int i = 0, index = 0;; ++i)
                {
                    indexes[i] = index;

                    var length = StringInfo.GetNextTextElementLength(source.Slice(index));
                    if (length == 0)
                    {
                        indexesLength = i + 1;
                        break;
                    }

                    index += length;
                }

                for (int i = indexesLength - 1; i >= 1; --i)
                {
                    var start = indexes[i - 1];
                    var length = indexes[i] - start;

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
