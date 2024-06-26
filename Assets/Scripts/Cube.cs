using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody)),
 RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private static readonly int s_platformLayer = 3;
    
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Color _defaultColor;
    private bool _isCounting;
    private float _minLifetime = 2;
    private float _maxLifetime = 5;

    public event UnityAction<Cube> LifetimeOver;
        
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isCounting)
            return;
        
        if (other.gameObject.layer == s_platformLayer)
        {
            var lifeTime = Random.Range(_minLifetime, _maxLifetime);
            _isCounting = true;
            _renderer.material.color = ChooseRandomColor();
            Invoke(nameof(AnnounceReleasing), lifeTime);
        }
    }

    public void SetActive(bool isActive)
    {
        if (isActive == false)
        {
            _isCounting = false;
            _renderer.material.color = _defaultColor;
        }

        gameObject.SetActive(isActive);
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    private Color ChooseRandomColor()
    {
        float min = 0.001f;
        float max = 1f;

        var color = new Color(
            r: Random.Range(min, max),
            g: Random.Range(min, max),
            b: Random.Range(min, max));

        return color;
    }

    private void AnnounceReleasing()
    {
        LifetimeOver?.Invoke(this);
    }
}
