using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    private InputAction basicAttack;

    public Player player;

    public Spell currentSpell;
    
    public float currentSpellCooldown;
    public float cooldownCounter;

    public int currentSpellManaCost;

    private void Start()
    {
        //player = GetComponent<Player>();
    }

    private void Update()
    {
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = 0;
        }
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
        if (cooldownCounter > 0)
        {
            return;
        }
        Vector2 screenPos = Pointer.current?.position.ReadValue() ?? Vector2.zero;
        
        var cam = Camera.main;
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        Vector2 world2D = new Vector2(world.x, world.y);
        currentSpell.CastSpell(gameObject.transform, world2D);
        player.ChangeMana(-currentSpellManaCost);
        Fishing.CancelCurrentFishing();

        cooldownCounter = currentSpellCooldown;
    }
}
