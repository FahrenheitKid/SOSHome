using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Refs")]
    public Transform spawnPoints;

    private Transform playerPoints;
    private Transform dogPoints;
    private Transform npcPoints;



    private void Awake()
    {
        if (spawnPoints)
        {
            playerPoints = spawnPoints.GetChild(0);
            dogPoints = spawnPoints.GetChild(1);
            npcPoints = spawnPoints.GetChild(2);
        }
    }

    void Start() {

    }

    public Vector3 GetRandomPlayerPoint()
    {
        int random = Random.Range(0, playerPoints.childCount);
        return playerPoints.GetChild(random).position;
    }

    public Vector3[] GetRandomDogPoints(int count)
    {
        List<Vector3> selectedPoints = new List<Vector3>();

        do
        {
            int random = Random.Range(0, dogPoints.childCount);
            Vector3 point = dogPoints.GetChild(random).position;

            if (!selectedPoints.Contains(point))
            {
                selectedPoints.Add(point);
            }
        } while (selectedPoints.Count < count);

        return selectedPoints.ToArray();
    }

    public Vector3[] GetRandomNPCPoints(int count)
    {
        List<Vector3> selectedPoints = new List<Vector3>();

        do
        {
            int random = Random.Range(0, npcPoints.childCount);
            Vector3 point = npcPoints.GetChild(random).position;

            if (!selectedPoints.Contains(point))
            {
                selectedPoints.Add(point);
            }
        } while (selectedPoints.Count < count);

        return selectedPoints.ToArray();
    }
}
