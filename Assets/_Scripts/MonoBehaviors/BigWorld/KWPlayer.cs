using UnityEngine;

public class KWPlayer : MonoBehaviour
{
    CharacterController characterController;
    InputDataModule inputDataModule;
    PlanetMovementBase _movement;
    public Vector3 Dir;
    public bool IsMove;

    private void Awake()
    {
        characterController=GetComponent<CharacterController>();        
        inputDataModule = DataModule.Resolve<InputDataModule>();
        _movement=GetComponent<PlanetMovementBase>();
    }
    public void SetPlanetCenter(Transform center)
    {
        _movement.SetPlanetCenter(center);
    }    

    private void Update()
    {
        
        if(IsMove)
        characterController.Move(transform.rotation*Dir*Time.deltaTime);        
    }


}
