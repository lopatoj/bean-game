using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private GameObject Bean;

    [SerializeField] private float cooldown;

    [SerializeField] private float force;

    [SerializeField] private Transform Hand;

    [SerializeField] private float lifetime;

    [SerializeField] private GameObject Player;

    private float throwTimer;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && throwTimer > cooldown)
        {
            Launch();
            throwTimer = 0f;
        }

        throwTimer += Time.deltaTime;
    }

    private void Launch()
    {
        var b = Instantiate(Bean, Hand.position, Random.rotation);

        b.GetComponent<Rigidbody>()
            .AddForce(transform.forward * force + Player.GetComponent<CharacterController>().velocity,
                ForceMode.VelocityChange);
        b.GetComponent<Rigidbody>().AddTorque(Random.rotation.eulerAngles);

        Destroy(b, lifetime);
    }
}