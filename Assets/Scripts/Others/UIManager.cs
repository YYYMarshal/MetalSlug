using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text healthPoint;
    private GameObject missionStart;
    private GameObject missionComplete;
    private GameObject android;

    private Button btnJ;
    private Button btnU;
    private Button btnK;
    private Button btnI;
    private Player player;
    private void Awake()
    {
        healthPoint = transform.Find("HealthAbout/HealthPoint").GetComponent<Text>();
        missionStart = transform.Find("MissionStart").gameObject;
        missionComplete = transform.Find("MissionComplete").gameObject;

        missionStart.SetActive(false);
        missionComplete.SetActive(false);

        ShowMissionStart();

        android = transform.Find("Android").gameObject;
        android.SetActive(Application.platform == RuntimePlatform.Android);
        Transform control = transform.Find("Android/Control");
        btnJ = control.Find("ButtonJ").GetComponent<Button>();
        btnU = control.Find("ButtonU").GetComponent<Button>();
        btnK = control.Find("ButtonK").GetComponent<Button>();
        btnI = control.Find("ButtonI").GetComponent<Button>();

        player = GameObject.Find("Player").GetComponent<Player>();

        btnJ.onClick.AddListener(() => player.BulletAttack());
        btnU.onClick.AddListener(player.MeleeAttack);
        btnK.onClick.AddListener(player.Jump);
        btnI.onClick.AddListener(player.GrenadeAttack);
    }

    public void UpdateHealthPoint(int num)
    {
        healthPoint.text = num.ToString();
    }
    private void ShowMissionStart()
    {
        missionStart.SetActive(true);
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        SoundManager.PlayMusicByName("MissionOneStart");
        yield return new WaitForSeconds(2f);
        missionStart.SetActive(false);
    }

    public void ShowMissionComplete()
    {
        missionComplete.SetActive(true);
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        SoundManager.PlayMusicByName("Mission Complete");
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(0);
    }


}
