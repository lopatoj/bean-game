using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private GameObject Cat;

    [SerializeField] private int health;

    [SerializeField] private TextMeshProUGUI score;

    private float _wave;
    private int _score;
    
    // Start is called before the first frame update
    void Start()
    {
        _wave = 1;
        _score = -1;
        Death();
    }

    private void AddCat()
    {
        GameObject c = Instantiate(Cat, Vector3.zero + Vector3.left * Random.Range(-1f, 1f) * 100f + Vector3.forward * Random.Range(-1f, 1f) * 100f, Quaternion.identity);
        c.GetComponent<Enemy>().health = health;
        c.GetComponent<Enemy>().Handler = this;

        score.text = $"score = {_score}";

    }

    public void Death()
    {
        _score++;
        
        if (_wave < 20)
        {
            for (int i = 0; i < _wave && i < 10; i++)
            {
                AddCat();
            }

            _wave += .1f;
        }
    }
}