using UnityEngine;

namespace Amalgun2D.Core
{
    public class InputManager : MonoBehaviour
    {
        public enum InputActionGroup
        {
            PlayerActions,
            UIActions
        }
        public static InputManager Instance { get; private set; }

        private PlayerInputActions inputActions;
        public PlayerInputActions.PlayerActions PlayerActions;
        public PlayerInputActions.UIActions UIActions;
        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            inputActions = new PlayerInputActions();
            PlayerActions = inputActions.Player;
            UIActions = inputActions.UI;

            // Do this in Game Loop later
            ToggleInputActionGroup(InputActionGroup.PlayerActions, true);
        }

        public void ToggleInputActionGroup(InputActionGroup group, bool toggle)
        {
            switch (group)
            {
                case InputActionGroup.PlayerActions:
                    if (toggle) PlayerActions.Enable();
                    else PlayerActions.Disable();
                    break;
                case InputActionGroup.UIActions:
                    if (toggle) UIActions.Enable();
                    else UIActions.Disable();
                    break;
            }
        }
    }
}
