using UnityEngine;

public class PlayerPlanetMovement : PlanetMovementBase
{
    InputDataModule _inputDM;

    Vector2 _moveInput;
    public float MoveSpeed=5f;


    protected override void Awake()
    {
        base.Awake();
        _inputDM = DataModule.Resolve<InputDataModule>();

        Manager<InputManager>.Inst.InputValueBinding<Vector2>(InputManager.MOVE,(isPress, data) =>
        {
            _moveInput=data;
            SetFaceMove(_moveInput.normalized * MoveSpeed);
            //SetFaceRotation(_moveInput.normalized );
        });
    }

    protected override void FaceMove()
    {
        base.FaceMove();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private protected override void Update()
    {
        base.Update();
    }
}
