using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_RotationSpeed;

    [SerializeField] float m_BallSpeed;

    [SerializeField] float m_CoolDownDuration;
    float m_TimeNextShoot;


    [SerializeField] GameObject m_BallPrefab;
    [SerializeField] Transform m_BallSpawnPoint;
    [SerializeField] MeshRenderer m_BodyMR;

    Transform m_Transform;
    Rigidbody m_Rigidbody;
    private Vector3 inputs = Vector3.zero;

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_TimeNextShoot = Time.time;
    }

    // Update is called once per frame
    private void Update(){

        // On récupére le mouvement souhaité

        inputs = new Vector2(Input.GetAxis("Horizontal"), 0);

        // On oriente le personnage en fonction et on anime le player (isWalking = true)

        if (inputs != Vector3.zero)
        {
            transform.forward = new Vector3(-inputs.x, 0, 0);
        }

        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Ceilling")
            + (1 << LayerMask.NameToLayer("Ennemy"));
        if (Physics.Raycast(m_Transform.position, Vector3.up, out hit, float.PositiveInfinity, layerMask))
        {
            m_BodyMR.material.color = Random.ColorHSV();
        }

    }

    private void FixedUpdate()
    {
        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        bool isFiring = Input.GetButton("Fire1");

        if (isFiring && Time.time > m_TimeNextShoot)
        {
            ShootBall();
            m_TimeNextShoot = Time.time + m_CoolDownDuration;
        }

        /*
        m_Rigidbody.MovePosition(m_Rigidbody.position + hInput * m_Transform.right * m_TranslationSpeed * Time.fixedDeltaTime);
        transform.forward = new Vector3(0, 0, hInput);
        */
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jump");
            m_Rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        }
        //m_Rigidbody.MoveRotation(Quaternion.AngleAxis(hInput * m_RotationSpeed * Time.fixedDeltaTime, Vector3.up) * m_Rigidbody.rotation);

        m_Rigidbody.MovePosition(m_Rigidbody.position + inputs * m_TranslationSpeed * Time.fixedDeltaTime);

    }

    void ShootBall()
    {
        GameObject ballGO = Instantiate(m_BallPrefab, m_BallSpawnPoint.position, Quaternion.identity, null);
        Rigidbody ballRigidbody = ballGO.GetComponent<Rigidbody>();
        ballRigidbody.velocity = m_BallSpeed * m_BallSpawnPoint.forward;

        Destroy(ballGO, 2);
    }
}
