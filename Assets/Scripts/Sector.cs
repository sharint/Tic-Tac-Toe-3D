using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{

    public TicTacToePlayer.Sides state;

    public int sectorIndex;

    private BoardController controller;

    private void Start()
    {
        state = TicTacToePlayer.Sides.none;

        GameObject controllerGameObject = GameObject.FindGameObjectWithTag("GameController");
        controller = controllerGameObject.GetComponent<BoardController>();
    }

    private void OnMouseDown()
    {
        if (state == TicTacToePlayer.Sides.none)
        {
            controller.PlayerTapped(sectorIndex);
        }
        
    }
}
