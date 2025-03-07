using UnityEngine;
using DG.Tweening;

public class ObjectSpinner : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private float rotationAmount = 360f;
    [SerializeField] private Ease easeType = Ease.Linear;
    
    [Header("Loop Settings")]
    [SerializeField] private bool loopRotation = true;
    [SerializeField] private int loops = -1; // -1 means infinite
    [SerializeField] private LoopType loopType = LoopType.Restart;
    
    private Tween rotationTween;

    void Start()
    {
        StartSpinning();
    }

    public void StartSpinning()
    {
        // Kill any existing tween to prevent conflicts
        if (rotationTween != null && rotationTween.IsActive())
        {
            rotationTween.Kill();
        }
        
        // Create the rotation tween on the Z axis (2D rotation)
        rotationTween = transform.DORotate(
            new Vector3(0, 0, rotationAmount), 
            rotationDuration, 
            RotateMode.FastBeyond360)
            .SetEase(easeType)
            .SetLoops(loopRotation ? loops : 0, loopType);
    }

    public void StopSpinning()
    {
        if (rotationTween != null && rotationTween.IsActive())
        {
            rotationTween.Kill();
        }
    }

    private void OnDestroy()
    {
        // Clean up the tween when the object is destroyed
        StopSpinning();
    }
}
