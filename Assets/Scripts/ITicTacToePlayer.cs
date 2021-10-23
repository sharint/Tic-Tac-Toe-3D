using UnityEngine;

public interface ITicTacToePlayer
{
    

    void SetSide(TicTacToePlayer.Sides side);
    string Turn(int sectorIndex);
}

    
