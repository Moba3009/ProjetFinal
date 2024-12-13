using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private int _numberOfObject;

    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance == this)
        {
            Destroy(gameObject);
        }
            
        //Don'tDestroyOnLoad(this);
    }

    public void IncreaseNumberObject()
    {
        _numberOfObject++;
        UpdateDisplay();
    }

    public void DecreaseNumberObject()
    {
        _numberOfObject--;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _text.text = _numberOfObject.ToString();
    }

}
