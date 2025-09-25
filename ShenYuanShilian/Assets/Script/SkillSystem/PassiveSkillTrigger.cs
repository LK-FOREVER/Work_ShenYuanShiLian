using System;

public abstract class PassiveSkillTrigger
{
    public SkillDeployer skillDeployer;
    public int skillConfigId;
    public bool inBattleScene;

    public void Init(SkillDeployer deployer, int id)
    {
        skillDeployer = deployer;
        skillConfigId = id;
        AddListener();
    }

    public abstract void AddListener();

    public abstract void RemoveListener();

    public virtual void Trigger(object sender, EventArgs e)
    {
        skillDeployer.DeploySkill(skillConfigId);
    }

    public virtual void Trigger()
    {
        skillDeployer.DeploySkill(skillConfigId);
    }
}
