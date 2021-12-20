// GENERATED AUTOMATICALLY FROM 'Assets/Code/Scripts/Minigame/MinigameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MinigameControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MinigameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MinigameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""063424b3-6f6e-40b9-9d3a-04aff91698dd"",
            ""actions"": [
                {
                    ""name"": ""TouchPoint"",
                    ""type"": ""Value"",
                    ""id"": ""10b67753-a235-4a81-b5b8-d4bba0483a33"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""d1b76d21-23ba-4206-8ddc-cfed0afe6dc5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91d2f481-8df2-4a80-aba8-081d14dd59b1"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc457f03-4cee-47aa-9ffc-8bdd99041021"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c59f24b-09e5-4219-81e4-7abd518f831b"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06c24823-9485-4466-8cef-78e93b2395cf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_TouchPoint = m_Player.FindAction("TouchPoint", throwIfNotFound: true);
        m_Player_TouchPress = m_Player.FindAction("TouchPress", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_TouchPoint;
    private readonly InputAction m_Player_TouchPress;
    public struct PlayerActions
    {
        private @MinigameControls m_Wrapper;
        public PlayerActions(@MinigameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchPoint => m_Wrapper.m_Player_TouchPoint;
        public InputAction @TouchPress => m_Wrapper.m_Player_TouchPress;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @TouchPoint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPoint;
                @TouchPoint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPoint;
                @TouchPoint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPoint;
                @TouchPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTouchPress;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchPoint.started += instance.OnTouchPoint;
                @TouchPoint.performed += instance.OnTouchPoint;
                @TouchPoint.canceled += instance.OnTouchPoint;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnTouchPoint(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
    }
}
