using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Over : MonoBehaviour
{
    public string GameScene;
    public string GameRestartScene;
    public Button RestartButton;
    public HeroManager hm;

 
    public TextMeshProUGUI distancetext;
    public TextMeshProUGUI fuelCantext;



    // Start is called before the first frame update
    void Start()
    {   
        Button restartbtn = RestartButton.GetComponent<Button>();
        restartbtn.onClick.AddListener(TaskOnClickBeg);

        // Retrieve the saved values from PlayerPrefs and display them in the TextMeshProUGUI elements
        float savedDistance = PlayerPrefs.GetFloat("TotalDistance");
        int savedFuelCanCount = PlayerPrefs.GetInt("FuelCanCountKey");
        distancetext.text = (savedDistance * 100).ToString("F0"); // Display distance as a formatted string
        fuelCantext.text = savedFuelCanCount.ToString();

    }



    void TaskOnClickBeg()
    {
        Debug.Log("You have clicked the restart button!");
        SceneManager.LoadScene(GameRestartScene);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
