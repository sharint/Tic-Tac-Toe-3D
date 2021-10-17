using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: TicTacToePlayer
{
    public Enemy(): base()
    {
        
    }

    public override void Turn()
    {
        base.Turn();
        Debug.Log("Player turn");
    }

}
