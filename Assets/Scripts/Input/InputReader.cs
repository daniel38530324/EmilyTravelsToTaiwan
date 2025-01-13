using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "New InputReader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MoveEvent; 
    public event Action<InputAction.CallbackContext> ShotEvent;
    private Controls controls;

    private void OnEnable(){
        if(controls == null){
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }

    void IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if(context.performed){
            ShotEvent?.Invoke(context);
        }
    }
}
