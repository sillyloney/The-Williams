using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickupObject : MonoBehaviour
{
    public Transform holdPoint;
    public float pickupRange = 3f;
    public TextMeshProUGUI infoText;

    GameObject heldObject;

    public Transform dropPoint;

    void Update()
    {
        CheckBoxInfo();

        if (heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
            heldObject.transform.rotation = holdPoint.rotation;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
            {
                TryPickup();
            }
        }
    }

    void CheckBoxInfo()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            interactionDisplay box = hit.collider.GetComponent<interactionDisplay>();

            if (box != null)
            {
                infoText.text = box.interaction;
                return;
            }
        }

        infoText.text = "";
    }

    void TryPickup()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Door"))
            {
                return;
            }
            else if (hit.collider.CompareTag("Box"))
            {

                heldObject = hit.collider.gameObject;

                heldObject.layer = LayerMask.NameToLayer("HeldObject");

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                Collider col = heldObject.GetComponent<Collider>();

                rb.useGravity = false;
                rb.isKinematic = true;

                col.enabled = false;

                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
            }
        }
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        Collider col = heldObject.GetComponent<Collider>();

        heldObject.transform.SetParent(null);

        heldObject.layer = LayerMask.NameToLayer("Default");

        col.enabled = true;

        heldObject.transform.rotation = Quaternion.identity;

        rb.isKinematic = false;
        rb.useGravity = true;

        heldObject = null;
    }

    public bool HasBox()
    {
        return heldObject != null;
    }

    public GameObject DropFromZone()
    {
        GameObject box = heldObject;

        Rigidbody rb = box.GetComponent<Rigidbody>();
        Collider col = box.GetComponent<Collider>();

        box.transform.SetParent(null);

        box.layer = LayerMask.NameToLayer("Default");

        col.enabled = true;

        rb.isKinematic = false;
        rb.useGravity = true;

        heldObject = null;

        return box;
    }
}
