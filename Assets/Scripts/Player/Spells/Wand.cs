using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

public class Wand : MonoBehaviour
{
    public Player player;

    [SerializeField] private InputActionAsset inputActions;

    private Spell primaryActiveSpell;
    private InputAction primaryActiveSpellAction;
    private Action<InputAction.CallbackContext> primarySpellHandler;
    private Spell secondaryActiveSpell;
    private InputAction secondaryActiveSpellAction;
    private Action<InputAction.CallbackContext> secondarySpellHandler;

    public float damageModifier;
    public float sizeModifier;
    public float rangeModifier;
    public float cooldownModifier;

    private PlayerInventory playerInventoryScript;
    private WandItem equippedWandItem;
        
    private void Start()
    {
        playerInventoryScript = player.inventory;
        UpdateWandStats();
        ResetEquippedSpells();
    }

    private void Awake()
    {
        if (inputActions == null)
        {
            inputActions = InputSystem.actions;
        }

        primaryActiveSpellAction = inputActions?.FindAction("PrimaryActiveSpell");
        secondaryActiveSpellAction = inputActions?.FindAction("SecondaryActiveSpell");

        primarySpellHandler = ctx => OnAttack(ctx, primaryActiveSpell);
        secondarySpellHandler = ctx => OnAttack(ctx, secondaryActiveSpell);
    }

    private void Update()
    {
        primaryActiveSpell.cooldownTimer = Mathf.Max(0, primaryActiveSpell.cooldownTimer - Time.deltaTime);
        secondaryActiveSpell.cooldownTimer = Mathf.Max(0, secondaryActiveSpell.cooldownTimer - Time.deltaTime);
    }

    public void UpdateWandStats()
    {
        equippedWandItem = (WandItem)playerInventoryScript.GetEquippedItemByItemType(ItemType.WAND); 
        
        damageModifier = equippedWandItem.damageModifier;
        sizeModifier = equippedWandItem.sizeModifier;
        rangeModifier = equippedWandItem.rangeModifier;
        cooldownModifier = equippedWandItem.cooldownModifier;
    }

    public void ResetEquippedSpells()
    {
        primaryActiveSpell = Resources.Load<GameObject>("Spells/" + ((ActiveSpellItem)playerInventoryScript.GetEquippedItemByItemType(ItemType.PRIMARY_ACTIVE_SPELL)).spellName).GetComponent<Spell>();
        secondaryActiveSpell = Resources.Load<GameObject>("Spells/" + ((ActiveSpellItem)playerInventoryScript.GetEquippedItemByItemType(ItemType.SECONDARY_ACTIVE_SPELL)).spellName).GetComponent<Spell>();
        GameObject.Find("Player Canvas").GetComponent<PlayerUIHandler>().UpdateSpellIcons();
    }
    
    void OnEnable()
    {
        primaryActiveSpellAction.performed += primarySpellHandler;
        if (!primaryActiveSpellAction.enabled) primaryActiveSpellAction.Enable();

        secondaryActiveSpellAction.performed += secondarySpellHandler;
        if (!secondaryActiveSpellAction.enabled) secondaryActiveSpellAction.Enable();
    }
    void OnDisable()
    {
        if (primaryActiveSpellAction != null)
        {
            primaryActiveSpellAction.performed -= primarySpellHandler;
            if (primaryActiveSpellAction.enabled) primaryActiveSpellAction.Disable();
        }

        if (secondaryActiveSpellAction != null)
        {
            secondaryActiveSpellAction.performed -= secondarySpellHandler;
            if (secondaryActiveSpellAction.enabled) secondaryActiveSpellAction.Disable();
        }
    }

    private void OnAttack(InputAction.CallbackContext context, Spell spell)
    {   
        if (spell.cooldownTimer > 0)
        {
            return;
        }

        Vector2 screenPos = Pointer.current?.position.ReadValue() ?? Vector2.zero;
        var cam = Camera.main;
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane));
        Vector2 world2D = new Vector2(world.x, world.y);
        
        spell.CastSpell(gameObject.transform, world2D, this);
        
        Fishing.CancelCurrentFishing();

        spell.cooldownTimer = spell.cooldown * cooldownModifier;
    }

    public Spell GetPrimaryActiveSpell()
    {
        return primaryActiveSpell;
    }

    public Spell GetSecondaryActiveSpell()
    {
        return secondaryActiveSpell;
    }

    public void DisableSpells()
    {
        OnDisable();
    }

    public void EnableSpells()
    {
        OnEnable();
    }

}