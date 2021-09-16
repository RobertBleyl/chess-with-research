// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputActions.inputactions'

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
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""StandardInput"",
            ""id"": ""6e92bffe-46f9-498c-8800-40494a1857f6"",
            ""actions"": [
                {
                    ""name"": ""PlacePieceOnBoard"",
                    ""type"": ""Button"",
                    ""id"": ""9049d318-90ee-4eb8-86f3-240170465ca4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3008cd09-d789-4c32-9be9-cfd21ad7bc5b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard+Mouse"",
                    ""action"": ""PlacePieceOnBoard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard+Mouse"",
            ""bindingGroup"": ""Keyboard+Mouse"",
            ""devices"": []
        }
    ]
}");
        // StandardInput
        m_StandardInput = asset.FindActionMap("StandardInput", throwIfNotFound: true);
        m_StandardInput_PlacePieceOnBoard = m_StandardInput.FindAction("PlacePieceOnBoard", throwIfNotFound: true);
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

    // StandardInput
    private readonly InputActionMap m_StandardInput;
    private IStandardInputActions m_StandardInputActionsCallbackInterface;
    private readonly InputAction m_StandardInput_PlacePieceOnBoard;
    public struct StandardInputActions
    {
        private @InputActions m_Wrapper;
        public StandardInputActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlacePieceOnBoard => m_Wrapper.m_StandardInput_PlacePieceOnBoard;
        public InputActionMap Get() { return m_Wrapper.m_StandardInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandardInputActions set) { return set.Get(); }
        public void SetCallbacks(IStandardInputActions instance)
        {
            if (m_Wrapper.m_StandardInputActionsCallbackInterface != null)
            {
                @PlacePieceOnBoard.started -= m_Wrapper.m_StandardInputActionsCallbackInterface.OnPlacePieceOnBoard;
                @PlacePieceOnBoard.performed -= m_Wrapper.m_StandardInputActionsCallbackInterface.OnPlacePieceOnBoard;
                @PlacePieceOnBoard.canceled -= m_Wrapper.m_StandardInputActionsCallbackInterface.OnPlacePieceOnBoard;
            }
            m_Wrapper.m_StandardInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlacePieceOnBoard.started += instance.OnPlacePieceOnBoard;
                @PlacePieceOnBoard.performed += instance.OnPlacePieceOnBoard;
                @PlacePieceOnBoard.canceled += instance.OnPlacePieceOnBoard;
            }
        }
    }
    public StandardInputActions @StandardInput => new StandardInputActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard+Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IStandardInputActions
    {
        void OnPlacePieceOnBoard(InputAction.CallbackContext context);
    }
}
