using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _repeatRate = 0.1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _maxCapacity = 10;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => Spawn(cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxCapacity);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private void GetCube()
    {
        var cube = _pool.Get();
        cube.LifetimeOver += ReleaseCube;
    }
    
    private void Spawn(Cube cube)
    {
        cube.transform.position = ChooseRandomPosition();
        cube.GetRigidbody().velocity = Vector3.zero;
        cube.SetActive(true);
    }

    private void ReleaseCube(Cube cube)
    {
        cube.LifetimeOver -= ReleaseCube;
        
        _pool.Release(cube);
    }

    private Vector3 ChooseRandomPosition()
    {
        var position = transform.position;
        var halfScaleX = transform.localScale.x / 2;
        var halfScaleZ = transform.localScale.z / 2;

        position.x = Random.Range(-halfScaleX, halfScaleX);
        position.z = Random.Range(-halfScaleZ, halfScaleZ);

        return position;
    }

    private IEnumerator Spawning()
    {
        bool isWorking = true;
        var delay = new WaitForSeconds(_repeatRate);

        while (isWorking)
        {
            GetCube();
            yield return delay;
        }
    }
}
