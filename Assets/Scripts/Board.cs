using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<List<int>> winConditions;

    public enum States { none, cross, circle };

    private const int countSectors = 9;
    private int turnsCount = 0;

    public List<GameObject> sectors;

    public List<int> freeSectors;
    public List<int> playerSectors;
    public List<int> enemySectors;


    public GameObject sectorPrefab;

    public Material cross;
    public Material circle;

    private bool gameOver = false;

    private void Start()
    {

        CreateBoard();
        for(int i = 0; i < sectors.Count; i++)
        {
            freeSectors.Add(i);
        }
        
    }

    private void CreateBoard()
    {
        for(int i = 0; i < countSectors; i++)
        {
            Vector3 position = new Vector3(i % 3, i / 3, 0);
            GameObject sector = Instantiate(sectorPrefab, position, sectorPrefab.transform.rotation);
            sector.transform.SetParent(gameObject.transform);
            sector.GetComponent<Sector>().sectorIndex = i;
            sectors.Add(sector);
        }
    }

    public void Turn()
    {
        
    }

    private void EnemyTurn()
    {
        int randomID = Random.Range(0, freeSectors.Count);
        int sectorIndex = freeSectors[randomID];
        GameObject sector = sectors[sectorIndex];
        //sector.GetComponent<Sector>().state = Sector.States.cross;
        //SetStateSector(States.cross, sector);
        freeSectors.Remove(sectorIndex);
        enemySectors.Add(randomID);
        turnsCount += 1;
        if (IsGameOver())
        {
            print("Win");
        }
    }

    private bool IsGameOver()
    {
        if (playerSectors.Count < 3 || enemySectors.Count < 3)
        {
            return false;
        }
        if(turnsCount >= 8)
        {
            gameOver = true;
            return true;
        }
        for(int i = 1; i < 5; i++)
        {
            if (check(i))
            {
                gameOver = true;
                return true;
            }
        }

        return false;
        
    }

    private bool check(int step)
    {
        int counter = 0;
        playerSectors.Sort();
        for (int i = playerSectors[0]; i < 9; i += step)
        {
            if (playerSectors[counter] != i)
            {
                return false;
            }
            counter += 1;
        }
        return true;

    }

    public void PlayerTurn(int id)
    {
        if (gameOver)
        { 
            return;
        }
        GameObject sector = sectors[id];
        //SetStateSector(States.circle, sector);
        freeSectors.Remove(id);
        playerSectors.Add(id);
        turnsCount += 1;
        EnemyTurn();
        
    }

    
    
}
