using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Sprite[] AllBallSprite;
    [SerializeField] TextMeshProUGUI BallAmount;
    [SerializeField] GameObject[] Balls;
    [SerializeField] GameObject BallShotPlatform;
    [SerializeField] GameObject BallShotPoint;
    [SerializeField] GameObject NextBallPoint;
    [SerializeField] ParticleSystem BombEffect;
    [SerializeField] ParticleSystem[] BoxEffects;

    int CurrentEffect;
    int CurrentBallIndex;
    GameObject CurrentBall;

    private void Start()
    {
        BallAmount.text = Balls.Length.ToString();
        SetBall();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("Platform"))
                {
                    Vector2 MosuePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    BallShotPlatform.transform.position = Vector2.MoveTowards(BallShotPlatform.transform.position, new Vector2(MosuePos.x, BallShotPlatform.transform.position.y), 30 * Time.deltaTime);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CurrentBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            CurrentBall.gameObject.transform.SetParent(null);
            CurrentBall.GetComponent<Ball>().StartInvok();
            if (CurrentBallIndex != Balls.Length)
            {
                BallOperation(); 
                if (CurrentBallIndex!=Balls.Length)
                {
                    NextBall();
                }
            }
            else
            {
                Debug.Log("wait, lose, ");
            }
        }
    }

    public void ExplosionEffect(Vector2 pos)
    {
        BombEffect.gameObject.transform.position = pos;
        BombEffect.gameObject.SetActive(true);
    }
    public void BoxExplosion(Vector2 pos)
    {
        BoxEffects[CurrentEffect].gameObject.transform.position = pos;
        BoxEffects[CurrentEffect].gameObject.SetActive(true);
        if (CurrentEffect != BoxEffects.Length - 1)
            CurrentEffect++;
        else
            CurrentEffect = 0;
    }
    void SetBall()
    {
        BallOperation();
        NextBall();
    }
    void BallOperation()
    {
        Balls[CurrentBallIndex].gameObject.transform.SetParent(BallShotPlatform.transform);
        Balls[CurrentBallIndex].transform.position = BallShotPoint.transform.position;
        Balls[CurrentBallIndex].SetActive(true);
        CurrentBall = Balls[CurrentBallIndex];
        CurrentBallIndex++;
    }
    void NextBall()
    {
        Balls[CurrentBallIndex].transform.position = NextBallPoint.transform.position;
        Balls[CurrentBallIndex].SetActive(true);
    }
}
