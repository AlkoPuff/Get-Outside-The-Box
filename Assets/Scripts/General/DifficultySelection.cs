using UnityEngine;

public class DifficultySelection : MonoBehaviour
{

    [SerializeField] private GameObject BasicMovementMechanics;
    [SerializeField] private GameObject StartUIGob;

    [SerializeField] private GameObject CheckpointsMechanic;

    public void SelectIntended()
    {
        GeneralSetup();
    }

    public void SelectCasual()
    {
        GeneralSetup();

        CheckpointsMechanic.SetActive(true);
    }


    private void GeneralSetup()
    {
        BasicMovementMechanics.SetActive(true);
        StartUIGob.SetActive(false);

        MusicManager.Instance.PlayMusic(0);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
