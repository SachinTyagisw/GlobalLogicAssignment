using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlobalLogic.Utilities
{
    /// <summary>
    /// This method shall compare the string and sorted. It supports different type of content numeric, string, alphanumeric.
    /// </summary>
    public class CustomComparer : Comparer<string>, IDisposable
    {
        private Dictionary<string, string[]> propertyCollection;
        public CustomComparer()
        {
            propertyCollection = new Dictionary<string, string[]>();
        }

        public void Dispose()
        {
            propertyCollection.Clear();
            propertyCollection = null;
        }

        public override int Compare(string source, string target)
        {
            if (source == target)
            {
                return 0;
            }
            string[] newSource, newTarget;
            if (!propertyCollection.TryGetValue(source, out newSource))
            {
                newSource = Regex.Split(source.Replace(" ", string.Empty), "([0-9]+)");
                propertyCollection.Add(source, newSource);
            }
            if (!propertyCollection.TryGetValue(target, out newTarget))
            {
                newTarget = Regex.Split(target.Replace(" ", string.Empty), "([0-9]+)");
                propertyCollection.Add(target, newTarget);
            }

            for (int i = 0; i < newSource.Length && i < newTarget.Length; i++)
            {
                if (newSource[i] != newTarget[i])
                {
                    return PartCompare(newSource[i], newTarget[i]);
                }
            }
            if (newTarget.Length > newSource.Length)
            {
                return 1;
            }
            else if (newSource.Length > newTarget.Length)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        private static int PartCompare(string left, string right)
        {
            int sourceValue, targetValue;
            if (!int.TryParse(left, out sourceValue))
            {
                return left.CompareTo(right);
            }

            if (!int.TryParse(right, out targetValue))
            {
                return left.CompareTo(right);
            }

            return sourceValue.CompareTo(targetValue);
        }
    }
}
