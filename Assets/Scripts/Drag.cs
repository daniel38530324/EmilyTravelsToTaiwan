using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [Header("Setting")]
    [SerializeField] private float movementTime = 5f;
    private Camera cam;

    Vector3 newPosition;
    Vector3 dragStartPosition = Vector3.zero;
    Vector3 dragCurrentPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // inputReader.DragEvent +=  HandleDrag;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovements();
    }

    private void OnDestroy() {
        // inputReader.DragEvent -=  HandleDrag;
    }

    private void HandleDrag(InputAction.CallbackContext context){
        string buttonControlPath = "/Mouse/leftButton";

        if (context.started)
        {
            if (context.control.path == buttonControlPath)
            {
                Debug.Log("Button Pressed Down Event - called once when button pressed");

                Ray dragStartRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                RaycastHit hit;

                // if(Physics.Raycast(dragStartRay , out hit))
                // {
                //     Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 100);
                //     hit.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                //     newPosition = hit.transform.position;
                // }
                // Plane dragStartPlane = new Plane(Vector3.up, Vector3.zero);
                // float dragStartEntry;

                // if (dragStartPlane.Raycast(dragStartRay, out dragStartEntry))
                // {
                //     dragStartPosition = dragStartRay.GetPoint(dragStartEntry);
                // }
            }
        }
        else if (context.performed)
        {
            if (context.control.path == buttonControlPath)
            {
                Debug.Log("Button Hold Down - called continously till the button is pressed");

                Ray dragCurrentRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

                newPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                newPosition.z = 10;

                // RaycastHit hit;

                // if(Physics.Raycast(dragCurrentRay , out hit))
                // {
                //     Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 100);
                //     hit.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                //     newPosition = hit.transform.position;
                // }
                /*
                Plane dragCurrentPlane = new Plane(Vector3.up, Vector3.zero);
                float dragCurrentEntry;

                if (dragCurrentPlane.Raycast(dragCurrentRay, out dragCurrentEntry))
                {
                    dragCurrentPosition = dragCurrentRay.GetPoint(dragCurrentEntry);
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
                */
            }

        }
        else if (context.canceled)
        {
            if (context.control.path == buttonControlPath)
            {
                Debug.Log("Button released");
            }
        }

    }

    private void ApplyMovements()
    {
        //transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
        transform.position = newPosition;
        //Debug.Log(newPosition);
    }
}
