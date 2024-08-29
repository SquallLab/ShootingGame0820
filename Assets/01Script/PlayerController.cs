using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private IWeapon currentWeapon; // c++ ������ / C#����������. 
    private IMovement movement;





    private void Awake()
    {
        currentWeapon = GetComponent<IWeapon>();
        //currentWeapon.Fire();
        movement = GetComponent<IMovement>();
    }

    public void CustomUpdate(Vector2 moveDir)
    {
        movement?.Move(moveDir);
        currentWeapon?.Fire();
    }


    public void StartGame()
    {
        currentWeapon?.SetEnable(true);
        movement?.SetEnable(true);
    }




}
