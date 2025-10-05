using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    private InputAction basicAttack;

    public Spell currentSpell;
    
    private void OnEnable()
    {
        basicAttack = InputSystem.actions.FindAction("Attack");
        
        basicAttack.Enable();
        basicAttack.performed += OnAttack;
    }

    private void OnDisable()
    {
        basicAttack.performed -= OnAttack;
        basicAttack.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack triggered");
        currentSpell.castSpell();
        Fishing.CancelCurrentFishing();
    }
}
