using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DropZone : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public float dropRange = 3f;

    public Transform player;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        PickupObject pickup = player.GetComponent<PickupObject>();

        if (distance <= dropRange && pickup != null && pickup.HasBox())
        {
            infoText.text = "Press E to Drop";

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                GameObject box = pickup.DropFromZone();
                PlaceBox(box);
            }
        }
        else
        {
            infoText.text = "";
        }
    }

    void PlaceBox(GameObject box)
    {
        GameManager.instance.BoxDelivered();

        int index = GameManager.instance.deliveredBoxes - 1;

        int columns = 2;
        int maxHeight = 2;

        int layer = index / (columns * maxHeight);
        int layerIndex = index % (columns * maxHeight);

        int col = layerIndex % columns;
        int height = layerIndex / columns;

        float spacingX = 1.5f; 
        float spacingZ = 1.5f;

        Vector3 offset = new Vector3(
            (col - 0.5f) * spacingX,   
            height * 0.6f,
            layer * spacingZ
        );

        box.transform.rotation = Quaternion.identity;
        box.transform.position = transform.position + offset;

        box.tag = "Delivered";

        Rigidbody rb = box.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}