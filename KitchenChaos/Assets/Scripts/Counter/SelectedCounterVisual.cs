using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject visualGameObject;

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
        visualGameObject.SetActive(true);   
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
