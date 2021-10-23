using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: TicTacToePlayer
{
    public Enemy(string name, int score) : base(name, score) { }

    public override string Turn(int sectorIndex)
    {
        sectorIndex = GetRandomNumber();
        return base.Turn(sectorIndex);
        
    }

    private int GetRandomNumber()
    {
        List<int> freeSectors = GetFreeSectors();
        int freeSectorsCount = freeSectors.Count;
        int randomNumber = Random.Range(0, freeSectorsCount);
        int randomSectorIndex = freeSectors[randomNumber];
        return randomSectorIndex;
    }
}
