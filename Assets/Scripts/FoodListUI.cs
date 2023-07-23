using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FoodListUI : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList(Dictionary<string, bool> foodList)
    {
        string output = "Find:";
        foreach(KeyValuePair<string, bool> food in foodList)
        {
            output += "\n" + food.Key;
            if(food.Value)
            {
                output += " 1/1";
            }
            else
            {
                output += " 0/1";
            }
        }
        text.text = output;
    }
}
