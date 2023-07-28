using UnityEngine;

public class GenericPoolableObject : MonoBehaviour, IPoolable
{
    [SerializeField] private int amountToPool = 5;
    [SerializeField] private float activeDuration = 30000;

    public int AmountToPool()
    {
        return amountToPool;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        Invoke("DeSpawn", activeDuration);
    }

    public void DeSpawn()
    {
        PoolingSystem.Instance.ReturnObjectToPool(gameObject, gameObject.name.Replace("(Clone)", ""));
    }
}
