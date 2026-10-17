using System.Collections;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class EntityAttributes : MonoBehaviour
{

    public bool Inside = false; 

    public float Speed = 12f;

    public float JumpHeight = 3f;


    public PlayerInventory PlayerInventory;


    public Container CurrentOxygen; 

    public Container CurrentWater;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        //need to have the Oxygen and Water go down here. Oxygen faster. 
    }



}
