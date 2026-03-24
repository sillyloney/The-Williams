using UnityEngine;

public class BoxFix : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Freeze", 0.8f);
    }

    void Freeze()
    {
        rb.isKinematic = true;
    }
}
