using System;
using UnityEngine;

public interface IPoolable
{
    int AmountToPool();
    void Spawn();
    void DeSpawn();
}
