using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExternalFactor : MonoBehaviour {

    [SerializeField] private Canvas     GameOverUI;
    [SerializeField] private float      fallDeathHeight;
    [SerializeField] private float      damageWait;
    [SerializeField] private float      clearWait;

    public  bool cantOnFlag;
    public  bool hitStepFlag;
    private bool onFloor;
    private int  resetTimer;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        resetTimer++;

        // loadしてplayに入るとture～～～なのでfalseにする
        if (resetTimer < 60) { // 時間指定は遷移系考慮
            cantOnFlag  = false;
            hitStepFlag = false;
        }
    }

    void FixedUpdate()
    {
        // fallDeathHeightより低い位置まで落ちたら
        if (transform.position.y < fallDeathHeight) {
            StartCoroutine("WaitGoGameOver");
        }

        // PlayerControllerを使わずに段差と落下
        if (!onFloor) {
            gameObject.GetComponent<Rigidbody>().drag = 0; 
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else {
            gameObject.GetComponent<Rigidbody>().drag = 10;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        // 段差
        if (hitStepFlag && !cantOnFlag) {
            transform.Translate(Vector3.up * 0.15f);
        }
    }

    
    void OnCollisionEnter(Collision col)
    {
        // ゲームオーバーへ
        if (col.gameObject.tag == "DeathTag") {
            StartCoroutine("WaitGoGameOver");
        }
        // ゲームクリアへ
        if (col.gameObject.name == "GameClearObj") {
            StartCoroutine("WaitGoGameClear");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // ゲームオーバーへ
        if (col.gameObject.tag == "DeathTag") {
            StartCoroutine("WaitGoGameOver");
        }
    }


    //-------------------
    // ゲームオーバーへ
    IEnumerator WaitGoGameOver()
    {
        if (GameState.state == (int)state.play) {
            //SoundManager.GetInstance().Play("SE_GameOver", SoundManager.PLAYER_TYPE.NONLOOP, true);
        }

        GameState.state = (int)state.death;
        GameObject.Find("FilmManager").GetComponent<FilmManager>().enabled = false;

        // モーション分待ってゲームオーバーへ
        yield return new WaitForSeconds(damageWait);

        GameOverUI.gameObject.SetActive(true);
    }
    
    //-------------------
    // ゲームクリアへ
    IEnumerator WaitGoGameClear()
    {
        GameState.state = (int)state.clear;

        // モーション分待ってゲームクリアへ
        yield return new WaitForSeconds(clearWait);

        SceneManager.LoadScene("MasterClear");
    }


    //-------------------
    // 床と接触している
    void OnTriggerStay(Collider other)
    {
        onFloor = true;
    }

    //-------------------
    // 床と接触してない
    void OnTriggerExit(Collider other)
    {
        onFloor = false;
    }
}