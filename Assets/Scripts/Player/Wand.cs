using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    private InputAction basicAttack;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
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
        Fishing.cancelCurrentFishing();
    }

}
