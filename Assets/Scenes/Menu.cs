using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button MenuButton;
    public string GameScene;

    public Button BeginButton;



    // Start is called before the first frame update
    void Start()
    {
        Button Begbtn = BeginButton.GetComponent<Button>(); // getting button component
        Begbtn.onClick.AddListener(TaskOnClickBeg); // add the button
    }



    void TaskOnClickBeg()
    {
        Debug.Log("You have clicked Begin Button!");
        SceneManager.LoadScene(GameScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
