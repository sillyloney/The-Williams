using UnityEngine;
using UnityEngine.InputSystem;
using DoorScript;

public class DoorInteract : MonoBehaviour
{
    public float interactDistance = 3f;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                Door door = hit.collider.GetComponent<Door>();

                if (door != null)
                {
                    door.OpenDoor();
                }
            }
        }
    }
}