using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputActions playerInput;

    [SerializeField] CurrentData current; 

    [SerializeField] GameEvent eUseTrampolinePressed;
    [SerializeField] GameEvent eRollPressed;
    [SerializeField] GameEvent eSwingPressed;
    [SerializeField] GameEvent eJumpPressed;
    private void Awake()
    {
        playerInput = new InputActions();

        playerInput.Gameplay.UseTrampoline.performed += ctx => eUseTrampolinePressed.Raise();
        playerInput.Gameplay.Roll.performed += ctx => eRollPressed.Raise();

        playerInput.Gameplay.Swing.performed += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
        playerInput.Gameplay.Swing.canceled += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
        playerInput.Gameplay.Swing.performed += ctx => eSwingPressed.Raise();

        playerInput.Gameplay.Move.performed += ctx => current.moveInput = playerInput.Gameplay.Move.ReadValue<float>();
        playerInput.Gameplay.Move.canceled += ctx => current.moveInput = playerInput.Gameplay.Move.ReadValue<float>();

        playerInput.Gameplay.Jump.performed += ctx => eJumpPressed.Raise();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
