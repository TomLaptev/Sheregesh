using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
     [SerializeField] public ObjectsGenerator ObjectsGenerator;
    public GameObject _roadPrefab;
    public List<GameObject> roads = new List<GameObject>();//список тех, кто на сцене
    public int maxRoadTiles;
    public int lengthRoadTile;
    public float maxSpeed;
    public float currentSpeed = 0;
    public bool flagDestroyRoads = false;

   void Start()
    {
        ResetLevel(); 
    }
 
    void FixedUpdate()
    {
        if (currentSpeed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -lengthRoadTile)
        {
            Destroy(roads[0]); //удаляем сам объекТ
            roads.RemoveAt(0); //удаляем из списка
            flagDestroyRoads = true;
            
            CreateNextRoadTile();
        }
    }

    void CreateNextRoadTile()
    {
        Vector3 pos = new Vector3(3, 0, 0);
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, lengthRoadTile);
        }
        GameObject go = Instantiate(_roadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
        
    }

    public void StartLevel()
    {
        currentSpeed = maxSpeed;
    }

    public void ResetLevel()
    {
        currentSpeed = 0;
       
        while (roads.Count > 0)
        {
            Destroy(roads[0]); //удаляем сам объект
            roads.RemoveAt(0); //удаляем из списка
        }
        for (int i = 0; i < maxRoadTiles; i++)
        {
            CreateNextRoadTile();  
        }

                 
         ObjectsGenerator.CreateObjects();  

    } 
}
