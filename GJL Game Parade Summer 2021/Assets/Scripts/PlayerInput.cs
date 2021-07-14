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
    private void Awake()
    {
        playerInput = new InputActions();

        playerInput.Gameplay.UseTrampoline.performed += ctx => eUseTrampolinePressed.Raise();
        playerInput.Gameplay.Roll.performed += ctx => eRollPressed.Raise();

        playerInput.Gameplay.Swing.performed += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
        playerInput.Gameplay.Swing.canceled += ctx => current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
        playerInput.Gameplay.Swing.performed += ctx => eSwingPressed.Raise();
    }

    //private void UseTrampolinePressed()
    //{
    //    //Debug.Log("pressed");
    //    eUseTrampolinePressed.Raise();
    //}

    //private void RollPressed()
    //{
    //    eRollPressed.Raise();
    //}

    //private void SwingValue()
    //{
    //    current.swingInput = 1;
    //    //current.swingInput = playerInput.Gameplay.Swing.ReadValue<float>();
    //}

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
