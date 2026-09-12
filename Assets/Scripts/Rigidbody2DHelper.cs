using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rigidbody2DHelper : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    // These public methods will show up cleanly in Unity Events!
    public void SetBodyTypeToDynamic() => rb.bodyType = RigidbodyType2D.Dynamic;
    public void SetBodyTypeToKinematic() => rb.bodyType = RigidbodyType2D.Kinematic;
    public void SetBodyTypeToStatic() => rb.bodyType = RigidbodyType2D.Static;
    public void SetVelocityAsAnotherRigidbody(Rigidbody2D otherRb) => rb.linearVelocity = otherRb.linearVelocity;
}
