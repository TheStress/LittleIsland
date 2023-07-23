using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    Dictionary<string, bool> foodToFind = new Dictionary<string, bool>
        {
            { "", false}
        };

    // Start is called before the first frame update
    void Start()
    {

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

        //Checking if win
        if (!foodToFind.ContainsValue(false))
        {
            WinGame();
        }
    }
    public void WinGame()
    {

    }

    public void StartGame()
    {

    }
}
