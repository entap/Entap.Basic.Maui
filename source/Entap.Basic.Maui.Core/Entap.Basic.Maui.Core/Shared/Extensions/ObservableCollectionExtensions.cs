using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ObservableCollectionの拡張メソッド
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        #region AddRange
        /// <summary>
        /// ObservableCollection型のデータの末尾に、IEnumerable型のデータを追加する
        /// </summary>
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T>? collection)
        {
            if (collection == null) return;
            if (ValidateCollectionCount(source, collection))
            {
                return;
            }

            var property = GetItemsPropertyInfo<T>();
            var method = GetOnCollectionResetMethodInfo<T>();

            if (property?.GetValue(source) is List<T> list)
            {
                list.AddRange(collection);
                method?.Invoke(source, null);
            }
        }

        const int switchForeachThresold = 2;
        static bool ValidateCollectionCount<T>(ObservableCollection<T> source, IEnumerable<T> collection)
        {
            var count = collection.Count();
            if (count <= switchForeachThresold)
            {
                foreach (var item in collection)
                {
                    source.Add(item);
                }
                return true;
            }

            return false;
        }

        static PropertyInfo? GetItemsPropertyInfo<T>() => typeof(ObservableCollection<T>).GetProperty("Items", BindingFlags.NonPublic | BindingFlags.Instance);
        static MethodInfo? GetOnCollectionResetMethodInfo<T>() => typeof(ObservableCollection<T>).GetMethod("OnCollectionReset", BindingFlags.NonPublic | BindingFlags.Instance);
        #endregion
    }
}
