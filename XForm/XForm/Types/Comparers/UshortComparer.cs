
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using XForm.Data;
using XForm.Query;
using XForm.Transforms;

namespace XForm.Types.Comparers
{
    /// <summary>
    ///  IDataBatchComparer for ushort[].
    ///  NOTE: Comparers are generated by 'XForm generateComparers'; see ComparerGenerator.cs
    /// </summary>
    internal class UshortComparer : IDataBatchComparer
    {
        internal static ComparerExtensions.Where<ushort> s_WhereNative = null;

		public void WhereEqual(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] == rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] == rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] == rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.Equal, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] == rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] == rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

		public void WhereNotEqual(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] != rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] != rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] != rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.NotEqual, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] != rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] != rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

		public void WhereLessThan(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] < rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] < rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] < rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.LessThan, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] < rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] < rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

		public void WhereLessThanOrEqual(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] <= rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] <= rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] <= rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.LessThanOrEqual, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] <= rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] <= rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

		public void WhereGreaterThan(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] > rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] > rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] > rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.GreaterThan, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] > rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] > rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

		public void WhereGreaterThanOrEqual(DataBatch left, DataBatch right, RowRemapper result)
        {
            result.ClearAndSize(left.Count);
            ushort[] leftArray = (ushort[])left.Array;
            ushort[] rightArray = (ushort[])right.Array;

            // Check how the DataBatches are configured and run the fastest loop possible for the configuration.
            if (left.IsNull != null || right.IsNull != null)
            {
                // Slowest Path: Null checks and look up indices on both sides. ~65ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    int leftIndex = left.Index(i);
                    int rightIndex = right.Index(i);
                    if (left.IsNull != null && left.IsNull[leftIndex]) continue;
                    if (right.IsNull != null && right.IsNull[rightIndex]) continue;
                    if (leftArray[leftIndex] >= rightArray[rightIndex]) result.Add(i);
                }
            }
            else if (left.Selector.Indices != null || right.Selector.Indices != null)
            {
                // Slow Path: Look up indices on both sides. ~55ms for 16M
                for (int i = 0; i < left.Count; ++i)
                {
                    if (leftArray[left.Index(i)] >= rightArray[right.Index(i)]) result.Add(i);
                }
            }
            else if (!right.Selector.IsSingleValue)
            {
                // Faster Path: Compare contiguous arrays. ~20ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                int leftIndexToRightIndex = right.Selector.StartIndexInclusive - left.Selector.StartIndexInclusive;
                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] >= rightArray[i + leftIndexToRightIndex]) result.Add(i - zeroOffset);
                }
            }
            else if (!left.Selector.IsSingleValue)
            {
                // Fastest Path: Contiguous Array to constant. ~15ms for 16M
                int zeroOffset = left.Selector.StartIndexInclusive;
                ushort rightValue = rightArray[0];

                if (s_WhereNative != null)
                {
                    s_WhereNative(leftArray, left.Selector.StartIndexInclusive, left.Selector.Count, (byte)CompareOperator.GreaterThanOrEqual, rightValue, (byte)BooleanOperator.Or, result.Vector.Array, 0);
                    return;
                }

                for (int i = left.Selector.StartIndexInclusive; i < left.Selector.EndIndexExclusive; ++i)
                {
                    if (leftArray[i] >= rightValue) result.Add(i - zeroOffset);
                }
            }
            else
            {
                // Single Static comparison. ~0.7ms for 16M [called every 10,240 rows]
                if (leftArray[left.Selector.StartIndexInclusive] >= rightArray[right.Selector.StartIndexInclusive])
                {
                    result.All(left.Count);
                }
            }
        }

    }
}