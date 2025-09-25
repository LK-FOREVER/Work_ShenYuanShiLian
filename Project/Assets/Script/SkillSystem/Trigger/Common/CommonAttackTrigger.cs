public class CommonAttackTrigger : PassiveSkillTrigger
{
    public override void AddListener()
    {
        EventManager.Instance.AddListener(EventName.CharacterAttack, Trigger);
    }

    public override void RemoveListener()
    {
        EventManager.Instance.RemoveListener(EventName.CharacterAttack, Trigger);
    }
}
