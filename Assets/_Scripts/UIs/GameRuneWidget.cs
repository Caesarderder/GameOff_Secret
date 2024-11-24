using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class GameRuneWidget :MonoBehaviour 
{
    EWorldType _worldType;
    [SerializeField]
    Button btn_close;
    [SerializeField]
    Slot[] slots;
    int CurSelectedSlotIndex;
    Slot CurSelectedSlot = null;
    Slot CurEmptySlot = null;


    public void Init(EWorldType type)
    {
        _worldType = type;
        btn_close.onClick.AddListener(Close);

        Close();
    }
    public void Open()
    {
        gameObject.SetActive(true);

        EventAggregator.Publish(new ChangeGameStateEvent() { 
            WorldType = _worldType,
            GameMode=EGameMode.RunePanel
        });

        Manager<InputManager>.Inst.InputButtonBinding(InputManager.ESC,
            x => {
                if ( x )
                    Close();
            });

    }
    public void Close()
    {
        gameObject.SetActive(false);

        EventAggregator.Publish(new ChangeGameStateEvent() { 
            WorldType = _worldType,
            GameMode=EGameMode.Normal
        });
    }
    void Start()
    {
        GetEmptySlot();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log ("<color=green>逆时针交换符文</color>");
            MoveAntiClockwise();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("<color=green>顺时针交换符文</color>");
            MoveClockwise();
        }
    }
    public void MoveClockwise()
    {
        if (CurEmptySlot != null && CurEmptySlot.adjacentSlots != null && CurEmptySlot.adjacentSlots.Count > 0)
        {
            Slot lastSlot = CurEmptySlot.adjacentSlots[CurEmptySlot.adjacentSlots.Count - 1];
            CurSelectedSlot = lastSlot;
            Debug.Log("<color=red>可与空slot </color>" + CurEmptySlot.slotID + "<color=red>交换符文的slot为: </color>" + CurSelectedSlot.slotID);
            ChangeRune();
            Debug.Log("<color=green>********更换符文结束 </color>");
        }
        else
        {
            Debug.LogError("<color=red>无可交换符文的slot</color>");
        }
    }
    public void MoveAntiClockwise()
    {
        if (CurEmptySlot != null && CurEmptySlot.adjacentSlots != null && CurEmptySlot.adjacentSlots.Count > 0)
        {
            Slot firstSlot = CurEmptySlot.adjacentSlots[0];
            CurSelectedSlot = firstSlot;
            Debug.Log("<color=red>可与空slot </color>" + CurEmptySlot.slotID + "<color=red>交换符文的slot为: </color>" + CurSelectedSlot.slotID);
            ChangeRune();
            Debug.Log("<color=green>********更换符文结束 </color>");
        }
        else
        {
            Debug.LogError("<color=red>无可交换符文的slot</color>");
        }
    }
    public void ChangeRune()
    {
        if (CurSelectedSlot != null && CurEmptySlot != null)
        {
            if (CurSelectedSlot.curRune != null && CurEmptySlot.curRune != null)
            {
                Transform pos_start = CurSelectedSlot.curRune.transform;
                Transform pos_target = CurEmptySlot.curRune.transform;

                Vector3 targetWorldPos = pos_target.position;

                Tweener tweener = pos_start.DOMove(targetWorldPos, 0.5f)
                                    .SetEase(Ease.OutQuad)
                                    .SetAutoKill(true)
                                    .OnComplete(() =>
                                    {
                                        Debug.Log("********符文位置移动动画完成");
                                        CurSelectedSlot.curRune.transform.SetParent(CurEmptySlot.transform, true);
                                        CurEmptySlot.curRune.transform.SetParent(CurSelectedSlot.transform, false);
                                        CurEmptySlot.UpdateCurRune();
                                        CurSelectedSlot.UpdateCurRune();
                                        CurSelectedSlot.isEmpty = true;
                                        CurEmptySlot.isEmpty = false;
                                        CurEmptySlot.CheckIsMatch();
                                        Debug.Log("<color=green>********更新空槽</color>");
                                        GetEmptySlot();
                                    });
            }
            else
            {
                Debug.LogError("<color=red>CurSelectedSlot或CurEmptySlot下的curRune为空，无法执行动画</color>");
            }
        }
        else
        {
            Debug.LogError("<color=red>CurSelectedSlot或CurEmptySlot为空，无法执行符文交换动画</color>");
        }
    }
    private void GetEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot != null && slot.isEmpty)
            {
                CurEmptySlot = slot;
                break;
            }
        }
        if (CurEmptySlot != null)
        {
            Debug.Log("<color=green>成功找到空槽，CurEmptySlotID:</color>" + CurEmptySlot.slotID);
        }
        else
        {
            Debug.LogError("<color=red>未找到空slot</color>");
        }

    }
}
