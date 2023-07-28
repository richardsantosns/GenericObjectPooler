# GenericObjectPooler
Generic object pooler for Unity

The simpliest, easiest and cleanest way to do object pooling in Unity.

Instructions:
1. Download the PoolingSystem folder, put it inside your Unity Project.
2. Create an empty gameObject that will hold the PoolingSystem.cs script.
3. Attach the GenericPoolableObject.cs to the prefab that you want to pool.
4. Put that prefab to a folder named "PooledObjects" under "Resource" folder
5. If you want to spawn the prefab simply call IPooler.OnPoolRequest?.Invoke("nameOfPrefab", vector3Position) <- the first parameter is the name of the prefab, make sure they have the same name. Second parameter is a Vector3 position where you want to spawn the object.
6. If you want reference to the object after spawning you can do this:
    var objectToSpawn = IPooler.OnPoolRequest?.Invoke("nameOfPrefab", vector3Position);
    objectToSpawn.DoSomething();

That's it!
Enjoy!
