using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualObjectsArray;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, PlayerManager.OnSelectedCounterChangedEventsArgs e)
    {
       if(e.baseCounterSelected == baseCounter){
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        foreach (GameObject visualObject in visualObjectsArray)
        {
            visualObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualObject in visualObjectsArray)
        {
            visualObject.SetActive(false);
        }
    }
}
