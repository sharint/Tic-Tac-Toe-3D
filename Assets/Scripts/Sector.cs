using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public enum States {none ,cross, circle };

    public States state;

    public int id;

    private void Start()
    {
        state = States.none;
    }

    private void OnMouseDown()
    {
        if (state == States.none)
        {
            Board board = gameObject.GetComponentInParent<Board>();
            board.PlayerTurn(id);
            state = States.cross;
        }
        
    }
}
