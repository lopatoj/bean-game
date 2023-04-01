using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Objects from game scene that need to be referenced by this class
    [SerializeField] private int initialHealth;

    [SerializeField] private TextMeshProUGUI Text;
    
    [SerializeField] private TextMeshProUGUI Over;
    
    [SerializeField] private Image Death;
    
    // Health variable
    private int _health;
    
    // Start is called before the first frame update
    private void Start()
    {
        _health = initialHealth;
        Over.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Text.text = $"You have {_health} health left!";
        var color = Death.color;
        color = new Color(color.r, color.g, color.b, (initialHealth - _health) / 700.0f);
        Death.color = color;

        if (_health < 1)
        {
            Time.timeScale = 0;
            Over.enabled = true;
            GetComponentInParent<Movement>().enabled = false;
            GetComponentInChildren<Throw>().enabled = false;

            WaitForSeconds a = new WaitForSeconds(5);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(0);
        }
    }
    
    // Subtract from health
    public void Remove()
    {
        _health--;
    }
}
