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
        public PlayerInputActions.PlayerActions playerActions;
        public PlayerInputActions.UIActions uiActions;
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
            playerActions = inputActions.Player;
            uiActions = inputActions.UI;

            // Do this in Game Loop later
            ToggleInputActionGroup(InputActionGroup.PlayerActions, true);
        }

        public void ToggleInputActionGroup(InputActionGroup group, bool toggle)
        {
            switch (group)
            {
                case InputActionGroup.PlayerActions:
                    if (toggle) playerActions.Enable();
                    else playerActions.Disable();
                    break;
                case InputActionGroup.UIActions:
                    if (toggle) uiActions.Enable();
                    else uiActions.Disable();
                    break;
            }
        }
    }
}
