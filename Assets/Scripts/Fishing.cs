using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fishing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D myCollider;

    PolygonCollider2D playerCollider;

    private void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
        playerCollider = player.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        
    }
}
