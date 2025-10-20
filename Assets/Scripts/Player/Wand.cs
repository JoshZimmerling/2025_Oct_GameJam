using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    public Player player;

    [SerializeField] private InputActionAsset inputActions;

    private InputAction castSpell1;
    private InputAction castSpell2;
    private InputAction castSpell3;

    private System.Action<InputAction.CallbackContext> spellHandler1;
    private System.Action<InputAction.CallbackContext> spellHandler2;
    private System.Action<InputAction.CallbackContext> spellHandler3;

    public List<Spell> spells;
    public List<float> spellCooldowns;
    public List<float> spellCooldownTimers;

    public float damageModifier;
    public float sizeModifier;
    public float rangeModifier;
    public float cooldownModifier;

    public int currentSpellManaCost;

    private void Start()
    {
        foreach (Spell spell in spells)
        {
            spellCooldowns.Add(spell.cooldown);
            spellCooldownTimers.Add(0f);
        }
    }
    
    private void Update()
    {
        for (int i = 0; i < spellCooldownTimers.Count; i++)
        {
            spellCooldownTimers[i] = Mathf.Max(0, spellCooldownTimers[i] - Time.deltaTime);
        }
    }
    
    void Awake()
    {
        if (inputActions == null)
        {
            inputActions = InputSystem.actions;
        }

        castSpell1 = inputActions?.FindAction("Attack");
        castSpell2 = inputActions?.FindAction("ActiveSpell");
        castSpell3 = inputActions?.FindAction("ActiveSpell2");

        spellHandler1 = ctx => OnAttack(ctx, 0);
        spellHandler2 = ctx => OnAttack(ctx, 1);
        spellHandler3 = ctx => OnAttack(ctx, 2);
    }
    void OnEnable()
    {
        castSpell1.performed += spellHandler1;
        if (!castSpell1.enabled) castSpell1.Enable();

        castSpell2.performed += spellHandler2;
        if (!castSpell2.enabled) castSpell2.Enable();
        
        castSpell3.performed += spellHandler3;
        if (!castSpell3.enabled) castSpell3.Enable();
    }
    void OnDisable()
    {
        if (castSpell1 != null)
        {
            castSpell1.performed -= spellHandler1;
            if (castSpell1.enabled) castSpell1.Disable();
        }

        if (castSpell2 != null)
        {
            castSpell2.performed -= spellHandler2;
            if (castSpell2.enabled) castSpell2.Disable();
        }
        
        if (castSpell3 != null)
        {
            castSpell3.performed -= spellHandler3;
            if (castSpell3.enabled) castSpell3.Disable();
        }
    }

    private void OnAttack(InputAction.CallbackContext context, int spell)
    {
        if (spellCooldownTimers[spell] > 0)
        {
            return;
        }

        Vector2 screenPos = Pointer.current?.position.ReadValue() ?? Vector2.zero;
        var cam = Camera.main;
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        Vector2 world2D = new Vector2(world.x, world.y);
        
        spells[spell].CastSpell(gameObject.transform, world2D, this);
        
        Fishing.CancelCurrentFishing();

        spellCooldownTimers[spell] = spellCooldowns[spell] * cooldownModifier;
    }

    public float GetSpellCooldown(int spell)
    {
        return spellCooldowns[spell];
    }

    public float GetSpellTimer(int spell)
    {
        return spellCooldownTimers[spell];
    }

}