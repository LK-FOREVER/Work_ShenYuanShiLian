public class CommonAttackedTrigger : PassiveSkillTrigger
{
    public override void AddListener()
    {
        EventManager.Instance.AddListener(EventName.CharacterAttacked, Trigger);
    }

    public override void RemoveListener()
    {
        EventManager.Instance.RemoveListener(EventName.CharacterAttacked, Trigger);
    }
}
