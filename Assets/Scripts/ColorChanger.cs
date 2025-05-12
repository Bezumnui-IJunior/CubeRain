using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.white;
    private InfectionTrigger _objectHit;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Reset()
    {
        _objectHit = null;
        _renderer.material.color = _initialColor;
    }

    public void RandomizeColor(InfectionTrigger trigger)
    {
        if (_objectHit == trigger)
            return;

        _objectHit = trigger;
        _renderer.material.color = Random.ColorHSV();
    }
}