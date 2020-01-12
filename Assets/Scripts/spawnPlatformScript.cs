using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlatformScript : MonoBehaviour
{
    public GameObject platformPrefab;

    int index = 0;
    [SerializeField]
    float platformWidth = 2f, platformHeight = 0.5f;
    int minX = -4,maxX = 4;

    public static spawnPlatformScript instance = null;

    List<GameObject> platformList = new List<GameObject>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        MakeObjects();

        for (int i = 0; i< 5; i++)
        {
            MakePlatform();
        }
        
    }

    private void MakeObjects()
    {
        for( int i=0; i<5; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformList.Add(platform);
        }
    }

    GameObject getPlatform()
    {
        GameObject obj = null;
    }

    

    public void MakePlatform()
    {
        int randomPosX;
        if(index == 0)
        {
            randomPosX = 0;
        }
        else
        {
            randomPosX =  Random.Range(minX, maxX);
        }

        Vector2 newPosition = new Vector2(randomPosX, index * 5);

        GameObject platform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
        platform.transform.SetParent(transform);
        platform.transform.localScale = new Vector2(platformWidth, platformHeight);
        index++;
    }
}
