using UnityEngine;

[RequireComponent(typeof(Bomb))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _force = 200f;
    [SerializeField] private float _radius = 4f;
    
    private Bomb _bomb;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
    }

    private void OnEnable()
    {
        _bomb.LifetimeOver += Explode;
    }

    private void OnDisable()
    {
        _bomb.LifetimeOver -= Explode;
    }

    private void Explode(Item bomb)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody itemRigidbody) == false)
                continue;
            
            itemRigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }
}
