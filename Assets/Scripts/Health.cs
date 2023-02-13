using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int initialHealth;

    [SerializeField] private TextMeshProUGUI Text;
    
    private int _health;
    
    // Start is called before the first frame update
    private void Start()
    {
        _health = initialHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        Text.text = $"You have {_health} health left!";
    }
    
    public void Remove()
    {
        _health--;
    }
}
