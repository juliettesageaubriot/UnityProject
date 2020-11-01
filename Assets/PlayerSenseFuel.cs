using UnityEngine;

public class PlayerSenseFuel : MonoBehaviour
{
    private int _fuelNumber = 0;
    [SerializeField]
    private int startFuelNumber = 3;
    [SerializeField][TagSelector]
    private string fuelTag = "SenseFuel";

    private void Start()
    {
        _fuelNumber = startFuelNumber;
    }

    public bool UseFuel()
    {
        if (_fuelNumber == 0) return false;
        _fuelNumber--;
        return true;
    }

    public void AddFuel()
    {
        _fuelNumber++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fuelTag)) AddFuel();
    }
}
