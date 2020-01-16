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

    float hue;

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

        inItColor();


    }

    void inItColor()
    {
        hue = Random.Range(0f, 1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hue, 0.6f, 0.8f);
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

    private GameObject GetPlatform()
    {
        GameObject obj = null;
        for(int i = 0; i < platformList.Count; i++)
        {
            if (!platformList[i].activeInHierarchy)
            {
                obj = platformList[i];
                return obj;
            }
        }
        return null;
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

        GameObject platform = GetPlatform();
        platform.SetActive(true);
        platform.transform.position = newPosition;
        platform.transform.rotation = Quaternion.identity;
        platform.transform.localScale = new Vector2(platformWidth, platformHeight);
        platform.transform.SetParent(transform);
        SetColor(platform);
        index++;
    }

    void SetColor(GameObject platform)
    {
        if(Random.Range(0,3) != 0)
        {
            hue += 0.11f;
            if(hue >= 1)
            {
                hue -=  1f;
            }
        }

        platform.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue, 0.6f, 0.8f);
    }
}
