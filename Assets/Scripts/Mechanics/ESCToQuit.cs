using UnityEngine;

public class ESCToQuit : MonoBehaviour
{

    [SerializeField] private GameObject QuittingGob;

    public float holdTime = 3f;
    private float timer = 0f;

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!QuittingGob.activeSelf)
                QuittingGob.SetActive(true);

            timer += Time.deltaTime;

            if (timer >= holdTime)
            {
                Application.Quit();
            }
        }
        else
        {
            if (QuittingGob.activeSelf)
                QuittingGob.SetActive(false);

            timer = 0f;
        }
#endif
    }
}
