using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    private Camera cam;
    private float xAngle;
    private float zAngle;
    private Quaternion target;

    private void Start()
    {
        cam = Camera.main;
        xAngle = cam.transform.rotation.eulerAngles.x;
        zAngle = cam.transform.rotation.eulerAngles.z;
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("General"));
    }
    public void ToTrashSorting()
    {
        StartCoroutine(CameraRotationToTrashSorting());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(true);
        this.transform.Find("ToTrashSortingButton").gameObject.SetActive(false);
    }

    public void ToBlackMarket()
    {
        StartCoroutine(CameraRotationToBlackMarket());
        this.transform.Find("ToBlackMarketButton").gameObject.SetActive(false);
        this.transform.Find("ToTrashSortingButton").gameObject.SetActive(true);
    }

    private IEnumerator CameraRotationToBlackMarket()
    {

        for (float i = 0; i <= 180; i += speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
        BlackMarket.Instance.transform.GetChild(0).gameObject.SetActive(true);
        BlackMarket.Instance.UpdateDemands();
    }

    private IEnumerator CameraRotationToTrashSorting()
    {
        BlackMarket.Instance.transform.GetChild(0).gameObject.SetActive(false);
        for (float i = 180; i >= 0; i -= speed)
        {
            target = Quaternion.Euler(xAngle, i, zAngle);
            cam.transform.rotation = target;
            yield return null;
        }
    }

    private IEnumerator CameraZoomToBlackMarket()
    {
        yield return null;
    }

    IEnumerator LoadScene(string level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
