using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;

namespace NiceCalc
{
	public enum DictionaryChangedAction
	{
		Add,
		Remove,
		Replace,
		Clear
	}

	public class DictionaryChangedEventArgs<TKey, TValue> : EventArgs
	{
		public DictionaryChangedAction Action => _action;
		public KeyValuePair<TKey, TValue> OldValue => _oldValue;
		public KeyValuePair<TKey, TValue> NewValue => _newValue;

		private DictionaryChangedAction _action;
		private KeyValuePair<TKey, TValue> _oldValue;
		private KeyValuePair<TKey, TValue> _newValue;

		public DictionaryChangedEventArgs(DictionaryChangedAction action, KeyValuePair<TKey, TValue> oldValue, KeyValuePair<TKey, TValue> newValue)
		{
			_action = action;
			_oldValue = oldValue;
			_newValue = newValue;
		}
	}

	public delegate void DictionaryChangedEventHandler<TKey, TValue>(object sender, DictionaryChangedEventArgs<TKey, TValue> e);

	public class ObservableDictionary<TKey, TValue> : IListSource, IDictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
	{
		public virtual event PropertyChangedEventHandler PropertyChanged;
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;
		public virtual event DictionaryChangedEventHandler<TKey, TValue> DictionaryChanged;

		public IEqualityComparer<TKey> Comparer { get { return _comparer ?? EqualityComparer<TKey>.Default; } }

		public int Count { get { return Entries.Count; } }

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => (ICollection<TKey>)Keys;
		public IReadOnlyCollection<TKey> Keys { get { return new ReadOnlyCollection<TKey>(Entries.Select(kvp => kvp.Key).ToList()); } }

		ICollection<TValue> IDictionary<TKey, TValue>.Values => (ICollection<TValue>)Values;
		public IReadOnlyCollection<TKey> Values { get { return new ReadOnlyCollection<TKey>(Entries.Select(kvp => kvp.Key).ToList()); } }

		public bool IsReadOnly => false;
		public bool ContainsListCollection => false;

		public static readonly string IndexerPropertyName = "Item[]";
		public static readonly KeyValuePair<TKey, TValue> DefaultKeyValuePair = default(KeyValuePair<TKey, TValue>);

		protected List<KeyValuePair<TKey, TValue>> Entries;
		private IEqualityComparer<TKey> _comparer = null;

		public ObservableDictionary()
		{
			Entries = new List<KeyValuePair<TKey, TValue>>();
		}

		public ObservableDictionary(IEqualityComparer<TKey> comparer)
			: this()
		{
			_comparer = comparer;
		}

		public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			Entries = new List<KeyValuePair<TKey, TValue>>(collection);
		}

		public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
		{
			_comparer = comparer;
			Entries = new List<KeyValuePair<TKey, TValue>>(collection);
		}

		public virtual TValue this[TKey key]
		{
			get
			{
				if (!TryGetEntry(key, out KeyValuePair<TKey, TValue> found))
				{
					throw new KeyNotFoundException("The given key was not present in the dictionary.");
				}
				return found.Value;
			}
			set
			{
				if (!TryGetEntry(key, out KeyValuePair<TKey, TValue> oldKvp))
				{
					Add(key, value);
				}
				else
				{
					KeyValuePair<TKey, TValue> newKvp = new KeyValuePair<TKey, TValue>(key, value);
					Replace(oldKvp, newKvp);
				}
			}
		}

		public IList GetList()
		{
			BindingList<KeyValuePair<TKey, TValue>> result = new BindingList<KeyValuePair<TKey, TValue>>(Entries);
			return result;
		}

		public bool ContainsKey(TKey key)
		{
			return TryGetEntry(key, out KeyValuePair<TKey, TValue> found);
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return Entries.Contains(item);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (!TryGetEntry(key, out KeyValuePair<TKey, TValue> found))
			{
				value = default(TValue);
				return false;
			}
			value = found.Value;
			return true;
		}

		protected bool TryGetEntry(TKey key, out KeyValuePair<TKey, TValue> entry)
		{
			entry = FindEntry(key);
			return !entry.Equals(default(KeyValuePair<TKey, TValue>));
		}

		protected KeyValuePair<TKey, TValue> FindEntry(TKey key)
		{
			return Entries.Find(kvp => Comparer.Equals(kvp.Key, key));
		}

		public virtual void Add(TKey key, TValue value)
		{
			if (TryGetEntry(key, out KeyValuePair<TKey, TValue> found))
			{
				throw new ArgumentException($"An item with the same key has already been added: [{string.Join(", ", Entries.Select(kvp => $"[\"{kvp.Key}\", \"{kvp.Value}\"]"))}]");
			}

			KeyValuePair<TKey, TValue> newValue = new KeyValuePair<TKey, TValue>(key, value);
			Add(newValue);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Entries.Add(item);
			int index = Entries.IndexOf(item);

			RaiseDictionaryChanged(DictionaryChangedAction.Add, DefaultKeyValuePair, item);
			RaisePropertyChanged(nameof(Count));
			RaisePropertyChanged(nameof(Keys));
			RaisePropertyChanged(nameof(Values));
			RaisePropertyChanged(IndexerPropertyName);
			RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}


		private void Replace(KeyValuePair<TKey, TValue> oldValue, KeyValuePair<TKey, TValue> newValue)
		{
			int index = Entries.IndexOf(oldValue);
			Entries.Remove(oldValue);
			Entries.Add(newValue);

			RaiseDictionaryChanged(DictionaryChangedAction.Replace, oldValue, newValue);
			RaisePropertyChanged(nameof(Values));
			RaisePropertyChanged(IndexerPropertyName);
			RaiseCollectionChanged(NotifyCollectionChangedAction.Replace, oldValue, newValue, index);
		}

		public virtual bool Remove(TKey key)
		{
			if (!TryGetEntry(key, out KeyValuePair<TKey, TValue> found))
			{
				return false;
			}
			return Remove(found);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			int index = Entries.IndexOf(item);
			bool result = Entries.Remove(item);
			RaiseDictionaryChanged(DictionaryChangedAction.Remove, item, DefaultKeyValuePair);
			RaisePropertyChanged(nameof(Count));
			RaisePropertyChanged(nameof(Keys));
			RaisePropertyChanged(nameof(Values));
			RaisePropertyChanged(IndexerPropertyName);
			RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
			return result;
		}

		public virtual void Clear()
		{
			Entries.Clear();

			RaiseDictionaryChanged(DictionaryChangedAction.Clear, DefaultKeyValuePair, DefaultKeyValuePair);
			RaisePropertyChanged(nameof(Count));
			RaisePropertyChanged(nameof(Keys));
			RaisePropertyChanged(nameof(Values));
			RaisePropertyChanged(IndexerPropertyName);
			RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Entries.CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return Entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Entries.GetEnumerator();
		}

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void RaiseDictionaryChanged(DictionaryChangedAction action, KeyValuePair<TKey, TValue> oldValue, KeyValuePair<TKey, TValue> newValue)
		{
			DictionaryChanged?.Invoke(this, new DictionaryChangedEventArgs<TKey, TValue>(action, oldValue, newValue));
		}

		protected virtual void RaiseCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> item, int index)
		{
			RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		protected virtual void RaiseCollectionChanged(NotifyCollectionChangedAction action, KeyValuePair<TKey, TValue> oldItem, KeyValuePair<TKey, TValue> newItem, int index)
		{
			RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, oldItem, newItem, index));
		}

		protected virtual void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			CollectionChanged?.Invoke(this, args);
		}
	}
}
