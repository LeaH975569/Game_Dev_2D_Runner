using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button BeginButton;
    public string GameScene;
    public Button MenuButton;
    public string MenuScene;

    // Start is called before the first frame update
    void Start()
    {
        Button Begbtn = BeginButton.GetComponent<Button>(); // getting button component
        Begbtn.onClick.AddListener(TaskOnClickBeg); // add the button

        Button Menubtn = MenuButton.GetComponent<Button>(); // getting button component
        Menubtn.onClick.AddListener(TaskOnClickMenu); // add the button
    }
    void TaskOnClickBeg()
    {
        Debug.Log("You have clicked Begin Button!");
        SceneManager.LoadScene(GameScene);
    }

    void TaskOnClickMenu()
    {
        Debug.Log("You have clicked Menu Button!");
        SceneManager.LoadScene(MenuScene);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
