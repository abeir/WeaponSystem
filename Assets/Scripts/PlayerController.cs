using Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacter, InputActions.IGameplayActions
{
    private InputActions _inputActions;

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new InputActions();
        _inputActions.Gameplay.SetCallbacks(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Disable();
    }

    public void OnSelectPrev(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponManager?.SelectPrev();
            
            Debug.Log($" -- OnSelectPrev: {WeaponManager?.Current.name}");
        }
    }

    public void OnSelectNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponManager?.SelectNext();
            
            Debug.Log($" -- OnSelectNext: {WeaponManager?.Current.name}");
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponManager?.Fire();
            Debug.Log($" -- OnFire: {WeaponManager?.Current.name}");
            
        }else if (context.canceled)
        {
            WeaponManager?.CancelFire();
        }
    }
}
