using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SelectOptionMenu : MonoBehaviour
{
    public Button buttonPrefab;

    UnityEvent<int> onSelect;

    List<string> options;
    List<Button> buttons;

    public void Initialize(UnityAction<int> call, List<string> newOptions)
    {
        options = newOptions;
        onSelect.RemoveAllListeners();
        onSelect.AddListener(call);

        for (int i = 0; i < options.Count; i++)
        {
            if (i < buttons.)
        }
    }

    public void Update()
    {
        
    }
}
