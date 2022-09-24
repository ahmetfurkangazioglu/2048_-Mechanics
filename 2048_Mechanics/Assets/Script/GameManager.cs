using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MyLibrary;

public class GameManager : MonoBehaviour
{
    [Header("Ball Operation")]
    public Sprite[] AllBallSprite;
    [SerializeField] TextMeshProUGUI BallAmountTxt;
    [SerializeField] GameObject[] Balls;
    [SerializeField] GameObject BallShotPlatform;
    [SerializeField] GameObject BallShotPoint;
    [SerializeField] GameObject NextBallPoint;
    [Header("Effect Operation")]
    [SerializeField] ParticleSystem BombEffect;
    [SerializeField] ParticleSystem[] BoxEffects;
    [Header("Mission Operation")]
    [SerializeField] List<Missions> missions = new List<Missions>();
    [SerializeField] List<MissionTool> missionTools = new List<MissionTool>();
    [Header("Panel Opreation")]
    [SerializeField] GameObject[] GeneralPanel;
    int MissionBall,MissionBox, MissionBallIndex,MissionBoxIndex, HowManyMission;
    bool isMissionBall, isMissionBox, Locked;
    int BallAmount, CurrentEffect, CurrentBallIndex;
    GameObject CurrentBall;


    private void Start()
    {
        Time.timeScale = 1;
        SetMission();
        BallAmount = Balls.Length;
        BallAmountTxt.text = BallAmount.ToString();
        SetBall();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !Locked)
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
        if (Input.GetMouseButtonUp(0) && !Locked)
        {
                BallAmount--;
                BallAmountTxt.text = BallAmount.ToString();
                CurrentBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                CurrentBall.GetComponent<CircleCollider2D>().isTrigger = false;
                CurrentBall.gameObject.transform.SetParent(null);
                CurrentBall.GetComponent<Ball>().StartInvok();
                if (CurrentBallIndex != Balls.Length)
                {
                    BallOperation();
                    if (CurrentBallIndex != Balls.Length)
                    {
                        NextBall();
                    }
                }
                else
                {
                    Invoke("Lose", 3f);
                    Locked = true;
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
        BoxMissionOperation();
        SoundManager.Instance.AllSounds[4].Play();
        BoxEffects[CurrentEffect].gameObject.transform.position = pos;
        BoxEffects[CurrentEffect].gameObject.SetActive(true);
        if (CurrentEffect != BoxEffects.Length - 1)
            CurrentEffect++;
        else
            CurrentEffect = 0;
    }
    public void BallMissionOperation(int value)
    {
        if (isMissionBall)
        {
            if (MissionBall == value)
            {
                isMissionBall = false;
                missionTools[MissionBallIndex].CompletedImage.SetActive(true);
                HowManyMission--;
                ResultControl();
            }
        }
    }
    public void GeneralOperation(string Value)
    {
        switch (Value)
        {
            case "Pause":
                Time.timeScale = 0;
                GeneralPanel[0].SetActive(true);
                Locked = true;
                break;
            case "Continue":
                Time.timeScale = 1;
                GeneralPanel[0].SetActive(false);
                Invoke("ChamgeLocked", 0.2F);
                break;
            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "BackMenu":
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
                break;
        }
    }
    void BoxMissionOperation()
    {
        if (isMissionBox)
        {
            MissionBox--;
            if (MissionBox == 0)
            {
                missionTools[MissionBoxIndex].CompletedImage.SetActive(true);
                HowManyMission--;
                ResultControl();
            }
        }
    }
    void SetMission()
    {
        for (int i = 0; i < missions.Count; i++)
        {
            HowManyMission++;
            missionTools[i].MissionImage.GetComponent<Image>().sprite = missions[i].MissonSprite;
            missionTools[i].MissionTxt.text = missions[i].MissionValue.ToString();
            missionTools[i].MissionObject.SetActive(true);
            if (missions[i].MissionType=="Ball")
            {
                isMissionBall = true;
                MissionBall = missions[i].MissionValue;
                MissionBallIndex = i;
            }
            else if (missions[i].MissionType == "Box")
            {
                isMissionBox = true;
                MissionBox = missions[i].MissionValue;
                MissionBoxIndex = i;
            }
        }
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
    void ResultControl()
    {
        SoundManager.Instance.AllSounds[1].Play();
        if (HowManyMission==0)
        {
            Win();
        }
    }
    void Win()
    {
        SoundManager.Instance.AllSounds[3].Play();
        GeneralPanel[2].SetActive(true);
        CancelInvoke();
        Locked = true;
    }
    void Lose()
    {
        SoundManager.Instance.AllSounds[2].Play();
        GeneralPanel[1].SetActive(true);
        Locked = true;
    }
    void ChamgeLocked()
    {
        Locked = false;
    }
}
