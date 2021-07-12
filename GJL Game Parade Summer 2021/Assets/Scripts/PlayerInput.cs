using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputActions playerInput;

    [SerializeField] GameEvent eUseTrampolinePressed;
    [SerializeField] GameEvent eRollPressed;
    private void Awake()
    {
        playerInput = new InputActions();

        playerInput.Gameplay.UseTrampoline.performed += ctx => UseTrampolinePressed();
        playerInput.Gameplay.Roll.performed += ctx => RollPressed();

    }

    private void UseTrampolinePressed()
    {
        Debug.Log("pressed");
        eUseTrampolinePressed.Raise();
    }

    private void RollPressed()
    {
        eRollPressed.Raise();
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
