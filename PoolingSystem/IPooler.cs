using System;
using UnityEngine;

public interface IPooler
{
    public static Func<string, Vector3, GameObject> OnPoolRequest;
}
