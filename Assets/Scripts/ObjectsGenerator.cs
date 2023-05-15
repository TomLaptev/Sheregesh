using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerator : MonoBehaviour
{
    [SerializeField] public RoadGenerator RoadGenerator;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] float _rotationSpeed;
    [SerializeField] public GameObject[] _barriersPrefabs;
    [SerializeField] public GameObject[] _roadSiteItemsPrefabs;
    [SerializeField] public int _numberBarriers;
    List<GameObject> coins = new List<GameObject>();//список тех, кто на сцене
    List<GameObject> barriers = new List<GameObject>();//список тех, кто на сцене
    List<GameObject> roadSiteItems = new List<GameObject>();//список тех, кто на сцене


    void FixedUpdate()
    {
        if (RoadGenerator.currentSpeed != 0)
        {
            CreateObjects();

            foreach (var coin in coins)
            {
                coin.transform.position -= new Vector3(0, 0, RoadGenerator.currentSpeed * Time.deltaTime);
                coin.transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
            }
            if (coins[0].transform.position.z < -RoadGenerator.lengthRoadTile)
            {
                Destroy(coins[0]); //удаляем сам объект
                coins.RemoveAt(0); //удаляем из списка
            }

            foreach (var barrier in barriers)
            {
                barrier.transform.position -= new Vector3(0, 0, RoadGenerator.currentSpeed * Time.deltaTime);
            }
            if (barriers[0].transform.position.z < -RoadGenerator.lengthRoadTile)
            {
                Destroy(barriers[0]); //удаляем сам объект
                barriers.RemoveAt(0); //удаляем из списка
            }

            foreach (var roadSiteItem in roadSiteItems)
            {
                roadSiteItem.transform.position -= new Vector3(0, 0, RoadGenerator.currentSpeed * Time.deltaTime);
            }
            if (roadSiteItems[0].transform.position.z < -RoadGenerator.lengthRoadTile)
            {
                Destroy(roadSiteItems[0]); //удаляем сам объект
                roadSiteItems.RemoveAt(0); //удаляем из списка
            }


        }
    }
    public void CreateObjects()
    {
        Vector3 pos = Vector3.zero;
        Vector3 posCoin = Vector3.zero;
        Vector3 posBarrier = Vector3.zero;
        Vector3 posRoadSiteLeftItem = Vector3.zero;
        Vector3 posRoadSiteRightItem = Vector3.zero;

        if (RoadGenerator.flagDestroyRoads)
        {
            pos = posCoin = posBarrier = posRoadSiteLeftItem = posRoadSiteRightItem = RoadGenerator.roads[RoadGenerator.roads.Count - 1].transform.position;

            posCoin += new Vector3(Random.Range(-2, 3) * 1.5f, Random.Range(2, 7) * 0.5f, Random.Range(-RoadGenerator.lengthRoadTile / 3, RoadGenerator.lengthRoadTile / 3));
            for (var i = 0; i < Random.Range(1, 4); i++) //кол-во монет в секции
            {
                posCoin += new Vector3(0, 0, 0.75f);
                GameObject goCoins = Instantiate(_coinPrefab, posCoin, Quaternion.identity);
                coins.Add(goCoins);
            }
            for (var i = 0; i < _numberBarriers; i++)
            {
                posBarrier = pos + new Vector3(Random.Range(-2, 3) * 1.5f, 0, Random.Range(-8, 9));
                GameObject goBarrier = Instantiate(_barriersPrefabs[Random.Range(0, _barriersPrefabs.Length)], posBarrier, Quaternion.identity);
                goBarrier.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                barriers.Add(goBarrier);
                for (int j = 0; j < i; j++)
                {
                    if (Mathf.Abs(barriers[i].transform.position.x - barriers[j].transform.position.x) < 1.5f &&
                    Mathf.Abs(barriers[i].transform.position.z - barriers[j].transform.position.z) < 2f)
                    {
                        barriers[i].SetActive(false);
                    }
                }

            }
            for (var i = 0; i < Random.Range(1, 6); i++)
            {
                posRoadSiteLeftItem = pos + new Vector3(Random.Range(0, -8) * 2 - 6, 0, Random.Range(-3, 4) * 3);
                GameObject goRoadSiteLeftItem = Instantiate(_roadSiteItemsPrefabs[Random.Range(0, _roadSiteItemsPrefabs.Length)], posRoadSiteLeftItem, Quaternion.identity);
                goRoadSiteLeftItem.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                roadSiteItems.Add(goRoadSiteLeftItem);

                posRoadSiteRightItem = pos + new Vector3(Random.Range(0, 8) * 2 + 6, 0, Random.Range(-3, 4) * 3);
                GameObject goRoadSiteRightItem = Instantiate(_roadSiteItemsPrefabs[Random.Range(0, _roadSiteItemsPrefabs.Length)], posRoadSiteRightItem, Quaternion.identity);
                goRoadSiteRightItem.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                roadSiteItems.Add(goRoadSiteRightItem);
            }


        }
        else if (RoadGenerator.currentSpeed == 0)
        {
            for (var i = 0; i < RoadGenerator.maxRoadTiles; i++)
            {
                if (i == 0)
                {
                    posCoin = RoadGenerator.roads[0].transform.position + new Vector3(Random.Range(-2, 3) * 1.5f, 1, Random.Range(1, 3) * 4);
                }
                else posCoin = RoadGenerator.roads[i].transform.position + new Vector3(Random.Range(-2, 3) * 1.5f, 1, Random.Range(-2, 3) * 4);

                GameObject goCoins = Instantiate(_coinPrefab, posCoin, Quaternion.identity);
                coins.Add(goCoins);

                if (i == 0)
                {
                    posBarrier = RoadGenerator.roads[0].transform.position + new Vector3(Random.Range(-2, 3) * 1.5f, 0, Random.Range(1, 3) * 5);
                }
                else posBarrier = RoadGenerator.roads[i].transform.position + new Vector3(Random.Range(-2, 3) * 1.5f, 0, Random.Range(-1, 3) * 5);
                
                _barriersPrefabs[i].transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);

                GameObject goBarrier = Instantiate(_barriersPrefabs[Random.Range(0, _barriersPrefabs.Length)], posBarrier, Quaternion.identity);
                goBarrier.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                barriers.Add(goBarrier);

                posRoadSiteLeftItem = RoadGenerator.roads[i].transform.position + new Vector3(Random.Range(-2, 0) * 10, 0, Random.Range(1, 3) * 5);

                posRoadSiteRightItem = RoadGenerator.roads[i].transform.position + new Vector3(Random.Range(1, 3) * 10, 0, Random.Range(1, 3) * 5);

                GameObject goRoadSiteLeftItem = Instantiate(_roadSiteItemsPrefabs[Random.Range(0, _roadSiteItemsPrefabs.Length)], posRoadSiteLeftItem, Quaternion.identity);
                goRoadSiteLeftItem.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                roadSiteItems.Add(goRoadSiteLeftItem);
                GameObject goRoadSiteRightItem = Instantiate(_roadSiteItemsPrefabs[Random.Range(0, _roadSiteItemsPrefabs.Length)], posRoadSiteRightItem, Quaternion.identity);
                goRoadSiteRightItem.transform.eulerAngles = new Vector3(0, Random.Range(-2, 3) * 30, 0);
                roadSiteItems.Add(goRoadSiteRightItem);
            }
        }

        RoadGenerator.flagDestroyRoads = false;
    }

    public void ResetObjects()
    {
        while (coins.Count > 0)
        {
            Destroy(coins[0]); //удаляем сам объект
            coins.RemoveAt(0); //удаляем из списка
        }

        while (barriers.Count > 0)
        {
            Destroy(barriers[0]); //удаляем сам объект
            barriers.RemoveAt(0); //удаляем из списка
        }

        while (roadSiteItems.Count > 0)
        {
            Destroy(roadSiteItems[0]); //удаляем сам объект
            roadSiteItems.RemoveAt(0); //удаляем из списка
        }
    }
}
