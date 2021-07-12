// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Actions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""4b06d4fc-6415-40d7-ae1a-6e11fd758c4e"",
            ""actions"": [
                {
                    ""name"": ""Use Trampoline"",
                    ""type"": ""Button"",
                    ""id"": ""4ffcc458-70bd-457e-97db-45cc687fd909"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""8242fea0-7289-4057-82f7-a3c43aa7b1bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swing"",
                    ""type"": ""Button"",
                    ""id"": ""94e6a9cb-3d86-4c5a-a88f-e0dcc61e0c5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""45672c7a-c61e-4dbb-8400-50d4b20dd567"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use Trampoline"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19f4ff3d-cd06-4c11-ad6b-c46c78f2923e"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cefcd055-4de0-4ad3-8b87-86f66f271ce1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_UseTrampoline = m_Gameplay.FindAction("Use Trampoline", throwIfNotFound: true);
        m_Gameplay_Roll = m_Gameplay.FindAction("Roll", throwIfNotFound: true);
        m_Gameplay_Swing = m_Gameplay.FindAction("Swing", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_UseTrampoline;
    private readonly InputAction m_Gameplay_Roll;
    private readonly InputAction m_Gameplay_Swing;
    public struct GameplayActions
    {
        private @InputActions m_Wrapper;
        public GameplayActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @UseTrampoline => m_Wrapper.m_Gameplay_UseTrampoline;
        public InputAction @Roll => m_Wrapper.m_Gameplay_Roll;
        public InputAction @Swing => m_Wrapper.m_Gameplay_Swing;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @UseTrampoline.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseTrampoline;
                @UseTrampoline.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseTrampoline;
                @UseTrampoline.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUseTrampoline;
                @Roll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Swing.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwing;
                @Swing.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwing;
                @Swing.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwing;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UseTrampoline.started += instance.OnUseTrampoline;
                @UseTrampoline.performed += instance.OnUseTrampoline;
                @UseTrampoline.canceled += instance.OnUseTrampoline;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Swing.started += instance.OnSwing;
                @Swing.performed += instance.OnSwing;
                @Swing.canceled += instance.OnSwing;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnUseTrampoline(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnSwing(InputAction.CallbackContext context);
    }
}