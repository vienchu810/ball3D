using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour
{
    [Header("InGame")]
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;

    public Material ballMat;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    private void Awake()
    {
        ballMat=FindObjectOfType<Ball>().transform.GetChild(0).GetComponent<MeshRenderer>().material;
        currentLevel=GameObject.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>();
        nextLevel=GameObject.Find("NextLevelText").GetComponent<TextMeshProUGUI>();
        levelSlider.transform.parent.GetComponent<Image>().color=ballMat.color+Color.gray;
        levelSlider.color = ballMat.color;
        currentLevelImg.color = ballMat.color;
        nextLevelImg.color = ballMat.color;
    }
    void Start()
    {
        currentLevel.text=PlayerPrefs.GetInt("Level",1).ToString();
        nextLevel.text=(PlayerPrefs.GetInt("Level",1)+1).ToString();
    }
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }    
}
