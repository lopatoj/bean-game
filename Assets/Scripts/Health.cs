using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private int initialHealth;

    [SerializeField] private TextMeshProUGUI Text;
    
    // Health variable
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
    
    // Subtract from health
    public void Remove()
    {
        _health--;
    }
}
