using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
using TMPro;

public class GameRuneWidget :MonoBehaviour 
{
    EWorldType _worldType;
    [SerializeField]
    Button btn_close;
    Button[] slotBtns;
    Slot[] slots;
    int CurSelectedSlotIndex;
    Slot CurSelectedSlot = null;
    Slot CurEmptySlot = null;
    private bool isGameSuccess = false;
    public TMP_InputField runeDesc;
    bool _canInput;

    public void Init(EWorldType type)
    {
        _worldType = type;
        btn_close.onClick.AddListener(Close);

        Close();
    }
    public void Open()
    {
        slots=GetComponentsInChildren<Slot>();
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

        GetEmptySlot();
        CurSelectedSlot = CurEmptySlot;
        _canChange = true;
        _canInput = true;
    }
    public void Close()
    {
        //Manager<AudioManager>.Inst.PlaySoundEffect(4);   
        gameObject.SetActive(false);

        EventAggregator.Publish(new ChangeGameStateEvent() { 
            WorldType = _worldType,
            GameMode=EGameMode.Normal
        });
    }
    void Start()
    {
        GetEmptySlot();
        slotBtns = GetSlotButtons();
        for (int i = 0; i < slotBtns.Length; i++)
        {
            if (slotBtns[i] != null)
            {
                int index = i;
                slotBtns[i].onClick.AddListener(() =>
                {
                    OnSlotButtonClick(index);
                });
            }
        }

        runeDesc.onEndEdit.AddListener(SaveRuneDesc);
        runeDesc.onSelect.AddListener(x => _canInput = false);
    }
    void Update()
    {
        if(_worldType == EWorldType.A && _canInput)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //Debug.Log ("<color=green>逆时针切换符文</color>");
                MoveAntiClockwise();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //Debug.Log("<color=green>顺时针切换符文</color>");
                MoveClockwise();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("<color=green>交换符文</color>");
                ChangeRune();
            }

        }
        IsAllMatch();
    }
    public void MoveClockwise()
    {
        ChangeSelectedIndex(CurSelectedSlotIndex+ 1);
    }
    public void MoveAntiClockwise()
    {
        ChangeSelectedIndex(CurSelectedSlotIndex-1);
    }

    public void ChangeSelectedIndex(int count)
    {
        if(_worldType==EWorldType.A)
        {
            if (CurEmptySlot != null && CurEmptySlot.adjacentSlots != null && CurEmptySlot.adjacentSlots.Count > 0 && _canChange)
            {
                CurSelectedSlotIndex = count;
                var maxIndex = CurEmptySlot.adjacentSlots.Count - 1;
                if (CurSelectedSlotIndex < 0)
                {
                    CurSelectedSlotIndex = maxIndex;

                }
                else if (CurSelectedSlotIndex > maxIndex)
                {
                    CurSelectedSlotIndex = 0;
                }

                CurSelectedSlot.transform.DOScale(1f, 0.3f);
                CurSelectedSlot = CurEmptySlot.adjacentSlots[CurSelectedSlotIndex];
                CurSelectedSlot.transform.DOScale(1.3f, 0.3f);
                //Debug.Log("<color=red>可与空slot </color>" + CurEmptySlot.slotID + "<color=red>交换符文的slot为: </color>" + CurSelectedSlot.slotID);

                runeDesc.text = DataModule.Resolve<GameStatusDM>().GetRuneDesc(CurSelectedSlot.curRune.runeID);
            }
            else
            {
                Debug.LogError("<color=red>无可交换的符文</color>");
            }
        }
        else
        {

        }

    }

    bool _canChange;
    public void ChangeRune()
    {
        if (CurSelectedSlot != null && CurEmptySlot != null&&_canChange)
        {
            if (CurSelectedSlot.curRune != null && CurEmptySlot.curRune != null)
            {
                Transform pos_start = CurSelectedSlot.curRune.transform;
                Transform pos_target = CurEmptySlot.curRune.transform;

                Vector3 targetWorldPos = pos_target.position;
                _canChange = false;
                Tweener tweener = pos_start.DOMove(targetWorldPos, 0.5f)
                                    .SetEase(Ease.OutQuad)
                                    .SetAutoKill(true)
                                    .OnComplete(() =>
                                    {
                                        CurSelectedSlot.curRune.transform.SetParent(CurEmptySlot.transform, true);
                                        CurEmptySlot.curRune.transform.SetParent(CurSelectedSlot.transform, false);
                                        //Debug.Log("********符文位置移动动画完成");
                                        CurSelectedSlot.isEmpty = true;
                                        CurEmptySlot.isEmpty = false;

                                        CurEmptySlot.UpdateCurRune();
                                        CurSelectedSlot.UpdateCurRune();
                                        CurEmptySlot.CheckIsMatch();
                                        CurSelectedSlot.CheckIsMatch();

                                        if(_worldType==EWorldType.A)
                                        {
                                            GetEmptySlot();
                                            ChangeSelectedIndex(CurSelectedSlotIndex);
                                        }
                                        else
                                        {
                                            CurSelectedSlot = CurEmptySlot;
                                            GetEmptySlot();
                                        }
                                  
                                        pos_start.localScale= Vector3.one;
                                        _canChange=true;
                                        //Debug.Log("<color=green>********更新空槽</color>");
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
    private Button[] GetSlotButtons()
    {
        Button[] buttons = new Button[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            Button btn = slots[i].GetComponent<Button>();
            if (btn != null)
            {
                buttons[i] = btn;
            }
            else
            {
                Debug.LogWarning($"Slot at index {i} does not have a Button component attached.");
            }
        }
        return buttons;
    }
    private void OnSlotButtonClick(int buttonIndex)
    {
        if (_worldType == EWorldType.A || !_canInput)
            return;
        CurSelectedSlot = slots[buttonIndex];
   
        if (CurSelectedSlot != null)
        {
         
            if (IsAdjacentSlot(CurSelectedSlot))
            {

                //Manager<AudioManager>.Inst.PlaySoundEffect(4);
                runeDesc.text = DataModule.Resolve<GameStatusDM>().GetRuneDesc(CurSelectedSlot.curRune.runeID);
                Debug.Log(runeDesc.text);
                ChangeRune();
            }
        }
    }
    private bool IsAdjacentSlot(Slot slot)
    {
        if (CurEmptySlot != null && CurEmptySlot.adjacentSlots != null)
        {
            return CurEmptySlot.adjacentSlots.Contains(slot);
        }
        return false;
    }
    private void IsAllMatch()
    {
        //Manager<AudioManager>.Inst.PlaySoundEffect(6);   
        bool allMatch = true;
        foreach (Slot slot in slots)
        {
            if (slot != null && !slot.getIsMatch())
            {
                allMatch = false;
                break;
            }
        }
        if (allMatch && !isGameSuccess)
        {
            Debug.Log("<color=green>游戏成功</color>");
            isGameSuccess = true;
        }
    }

    private void SaveRuneDesc(string inputText)
    {
        _canInput = true;
        Debug.Log("6666666666666666");
        if (CurSelectedSlot != null && CurSelectedSlot.curRune.runeID != 0)
        {
            DataModule.Resolve<GameStatusDM>().UpdateRuneDesc(CurSelectedSlot.curRune.runeID, inputText);

        }
    }

}
