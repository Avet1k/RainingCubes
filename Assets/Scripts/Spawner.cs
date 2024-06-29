using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] private T _item;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _maxCapacity = 10;

    private ObjectPool<T> _pool;
    private int _createdItemsCounter = 0;

    public event UnityAction<int> ItemCreated;
    public event UnityAction<int> ChangedActiveItems;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_item),
            actionOnGet: (item) => Spawn(item),
            actionOnRelease: (item) => item.SetActive(false),
            actionOnDestroy: (item) => Destroy(item),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxCapacity);
    }

    protected virtual void Spawn(T item)
    {
        item.RigidbodyLink.velocity = Vector3.zero;
        item.SetActive(true);
        ItemCreated?.Invoke(++_createdItemsCounter);
        ChangedActiveItems?.Invoke(_pool.CountActive);
    }

    protected void GetItem()
    {
        T item = _pool.Get();
        item.LifetimeOver += ReleaseItem;
    }
    
    protected virtual void ReleaseItem(Item item)
    {
        if (item is T typedItem)
        {
            typedItem.LifetimeOver -= ReleaseItem;
            _pool.Release(typedItem);
            ChangedActiveItems?.Invoke(_pool.CountActive);
        }
    }
}
