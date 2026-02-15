using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Represents a dictionary that provides notifications when items are added, removed, or the entire collection is refreshed.
/// </summary>
/// <remarks>
/// ObservableDictionary is useful for scenarios where changes to a key-value collection need to be
/// observed, such as data binding in user interfaces. It raises collection change and property change notifications
/// compatible with data binding frameworks that support INotifyCollectionChanged and INotifyPropertyChanged. All
/// dictionary modification methods raise the appropriate events to notify listeners of changes. Enumeration and read
/// operations do not raise events.
/// </remarks>
/// <typeparam name="TKey">The type of keys in the dictionary. Must be non-nullable.</typeparam>
/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
    where TKey : notnull
{
    private readonly IDictionary<TKey, TValue> _dictionary;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableDictionary() : this(new Dictionary<TKey, TValue>()) { }

    public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
    {
        _dictionary = new Dictionary<TKey, TValue>(dictionary);
    }

    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set
        {
            bool isUpdate = _dictionary.ContainsKey(key);
            var oldValue = isUpdate ? _dictionary[key] : default;
            _dictionary[key] = value;

            if (isUpdate)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,
                    new KeyValuePair<TKey, TValue>(key, value),
                    new KeyValuePair<TKey, TValue>(key, oldValue!)));
            else
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                    new KeyValuePair<TKey, TValue>(key, value)));

            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
        }
    }

    public void Add(TKey key, TValue value)
    {
        _dictionary.Add(key, value);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(IndexerName);
    }

    public bool Remove(TKey key)
    {
        if (_dictionary.TryGetValue(key, out TValue? value) && _dictionary.Remove(key))
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value)));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            return true;
        }
        return false;
    }

    public void Clear()
    {
        _dictionary.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        OnPropertyChanged(nameof(Count));
        OnPropertyChanged(IndexerName);
    }

    // Helper to notify UI of indexer changes
    private const string IndexerName = "Item[]";

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    // Boilerplate IDictionary implementation
    public ICollection<TKey> Keys => _dictionary.Keys;
    public ICollection<TValue> Values => _dictionary.Values;
    public int Count => _dictionary.Count;
    public bool IsReadOnly => _dictionary.IsReadOnly;
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => _dictionary.TryGetValue(key, out value);
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    public bool Contains(KeyValuePair<TKey, TValue> item) => _dictionary.Contains(item);
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _dictionary.CopyTo(array, arrayIndex);
    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
}
