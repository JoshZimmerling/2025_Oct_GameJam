using System;
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
        // screen-space mouse/touch position (pixels)
        Vector2 screenPos = Pointer.current?.position.ReadValue() ?? Vector2.zero;

        // to world (2D example):
        var cam = Camera.main;
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        Vector2 world2D = new Vector2(world.x, world.y);
        currentSpell.CastSpell(gameObject.transform, world2D);
        Debug.Log((world2D));
        Fishing.CancelCurrentFishing();
    }
}
