using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] model;
    [HideInInspector]
    public GameObject[] modelPrefabs = new GameObject[4];
    public GameObject winPrefabs;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    float i = 0;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);
       if(level>9)
       {
            addOn = 0;
       }
        ModelSelection();
        float random=Random.value;
        for(i = 0; i>-level-addOn;i-=0.5f)
        {
            if(level<=20)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(0, 2)]);
            }    
            else if(level<=50)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(1, 3)]);
            }
            else if(level<=100)
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(2, 4)]);
            }
            else
            {
                temp1 = Instantiate(modelPrefabs[Random.Range(3, 4)]);
            }
            temp1.transform.position=new Vector3(0,i-0.01f,0);
            temp1.transform.eulerAngles = new Vector3(0, i *8, 0);

            if (Mathf.Abs(i) >= level * 0.3f && Mathf.Abs(i) <= level * 0.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;

            }
            else if (Mathf.Abs(i) >= level * 0.8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                if(random>0.75f)
                {
                    temp1.transform.eulerAngles += Vector3.up * 180;
                }    
            }    

            temp1.transform.parent = FindObjectOfType<Rotator>().transform;
        }
        temp2 = Instantiate(winPrefabs);
        temp2.transform.position=new Vector3(0,i-0.01f,0);
    }
    private void ModelSelection()
    {
        int randomModel = Random.Range(0, 5);
        switch(randomModel)
        {
            case 0:
                for(int i=0;i<4;i++)
                {
                    modelPrefabs[i] = model[i];
                }
                break;
            case 1:

                for (int i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = model[i+4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = model[i+8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = model[i+12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    modelPrefabs[i] = model[i+16];
                }
                break;

        }
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }    
    //https://www.youtube.com/watch?v=SLqF-Xsfbd0
}
