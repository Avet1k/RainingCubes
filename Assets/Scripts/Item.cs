using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float MinLifetime = 2;
    [SerializeField] protected float MaxLifetime = 5;
    
    protected Renderer Renderer;
    protected Color DefaultColor;
    protected bool IsCountingLifetime;
    
    public event UnityAction<Item> LifetimeOver;
    
    public Rigidbody RigidbodyLink { get; private set; }
    
    private void Awake()
    {
        RigidbodyLink = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
        
        DefaultColor = Renderer.material.color;
    }

    public virtual void SetActive(bool isActive)
    {
        if (isActive == false)
        {
            IsCountingLifetime = false;
            Renderer.material.color = DefaultColor;
        }
        
        gameObject.SetActive(isActive);
    }
    
    protected IEnumerator AnnouncingRelease(float lifetime)
    {
        var delay = new WaitForSeconds(lifetime);
 
        yield return delay;
 
        AnnounceRelease();
    }

    private void AnnounceRelease()
    {
        LifetimeOver?.Invoke(this);
    }
}
