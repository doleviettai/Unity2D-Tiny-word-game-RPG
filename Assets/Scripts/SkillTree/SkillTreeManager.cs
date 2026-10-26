using UnityEngine;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text pointsText;
    public int availablePoints;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed += handleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoint;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed -= handleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoint;
    }

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoint(slot));
        }
        UpdateAbilityPoint(0);
    }

    private void CheckAvailablePoint(SkillSlot slot)
    {
        if (availablePoints > 0)
        {
            slot.TryUpgradeSkillUp();
        }
    }

    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if(availablePoints > 0)
        {
            UpdateAbilityPoint(-1);
        }
    }

    

    private void handleSkillMaxed(SkillSlot skillSlot)
    {
        foreach(SkillSlot slot in skillSlots)
        {
            if(!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.UnLock();
            }
            
        }
    }

    public void UpdateAbilityPoint(int amount)
    {
        availablePoints += amount;
        pointsText.text = "Point: " + availablePoints;
    }
}
