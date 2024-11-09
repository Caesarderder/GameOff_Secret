using UnityEngine;

public class InputDataModule: DataModule
{
    #region Fileds 亻尔 女子
    public Vector2 MoveInput;

    public override void OnCreate()
    {
        base.OnCreate();
        Manager<InputManager>.Inst.InputValueBinding<Vector2>(InputManager.MOVE,(isPress, data) =>
        {
            MoveInput=data;
            //Debug.Log(MoveInput);
        });
    }
    public override void OnDestory()
    {
        base.OnDestory();
    }

    #endregion

    #region Methods

    #endregion
}
