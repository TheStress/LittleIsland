using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    private Dictionary<string, bool> foodToFind = new Dictionary<string, bool>();

    [SerializeField]
    private List<string> foodNames;
    [SerializeField]
    private FoodListUI foodListUI;
    [SerializeField]
    private GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string food in foodNames)
        {
            foodToFind.Add(food, false);
        }

        foodListUI.UpdateList(foodToFind);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FoundFood(string foodName)
    {
        //Checking off the List
        if(foodToFind.ContainsKey(foodName)) {
            foodToFind[foodName] = true;
        }

        //Updating UI
        foodListUI.UpdateList(foodToFind);

        //Checking if win
        if (!foodToFind.ContainsValue(false))
        {
            WinGame();
        }
    }
    public void WinGame()
    {
        Debug.Log("Won game");
        winScreen.SetActive(true);
    }

    public void StartGame()
    {

    }
}
