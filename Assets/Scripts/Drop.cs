using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drop : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    
}