using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _platformLayerMask;
    
    private Renderer _renderer;
    private Color _defaultColor;
    private bool _isCountingLifetiime;
    private float _minLifetime = 2;
    private float _maxLifetime = 5;

    public event UnityAction<Cube> LifetimeOver;

    public Rigidbody RigidbodyLink { get; private set; }
        
    private void Awake()
    {
        RigidbodyLink = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isCountingLifetiime)
            return;
        
        if ((_platformLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            float lifeTime = Random.Range(_minLifetime, _maxLifetime);
            _isCountingLifetiime = true;
            _renderer.material.color = ChooseRandomColor();
            StartCoroutine(nameof(AnnouncingReleasing), lifeTime);
        }
    }

    public void SetActive(bool isActive)
    {
        if (isActive == false)
        {
            _isCountingLifetiime = false;
            _renderer.material.color = _defaultColor;
        }

        gameObject.SetActive(isActive);
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

    private IEnumerator AnnouncingReleasing(float lifetime)
    {
        var delay = new WaitForSeconds(lifetime);

        yield return delay;

        LifetimeOver?.Invoke(this);
    }
}
