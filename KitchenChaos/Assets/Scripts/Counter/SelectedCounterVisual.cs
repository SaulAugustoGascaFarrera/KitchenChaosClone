using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    public List<GameObject> visualGameObjectList;
    //public List<Material> visualMaterialsList;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
        Hide();
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();

        }else
        {
            Hide(); 
        }
    }

    private void Show()
    {
        foreach(GameObject visualObject in visualGameObjectList)
        {
            visualObject.SetActive(true);
        }


           
    }

    private void Hide()
    {
        foreach (GameObject visualObject in visualGameObjectList)
        {
            visualObject.SetActive(false);
        }
    }
}
