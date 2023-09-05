using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractFactory<TProductType> where TProductType : class
{
    protected ObjectPool<TProductType> _pool;

    protected AbstractFactory(int defaultCap = 10, int maxCap = 50) => InitializePool(defaultCap, maxCap);

    public abstract TProductType GetFromPool();
    protected abstract TProductType ConstructNew();
    protected abstract void GetfromPool(TProductType obj);
    protected abstract void ReleazeInPool(TProductType obj);
    protected abstract void DestroyPooled(TProductType obj);
    protected void InitializePool(int defaultCap, int maxCap)
    {
        _pool = new ObjectPool<TProductType>(
            createFunc: ConstructNew,
            actionOnGet: GetfromPool,
            actionOnRelease: ReleazeInPool,
            actionOnDestroy: DestroyPooled,
            collectionCheck: false,
            defaultCapacity: defaultCap,
            maxSize: maxCap);
    }
}
public abstract class AbstractMonoBehaviuorFactory<TProductType> : AbstractFactory<TProductType> where TProductType : MonoBehaviour
{
    protected override void GetfromPool(TProductType obj) => obj.gameObject.SetActive(true);
    protected override void DestroyPooled(TProductType obj) => GameObject.Destroy(obj);
    protected override void ReleazeInPool(TProductType obj) => obj.gameObject.SetActive(false);
}