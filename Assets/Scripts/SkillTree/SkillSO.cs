using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill Tree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int maxLevel = 5;
    public Sprite skillIcon;

}
