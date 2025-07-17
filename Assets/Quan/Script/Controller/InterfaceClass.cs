public interface IData
{
}

public interface IController
{
    public void Initialize();
    public void Move();
    public void Run();
    public void Attack();
}

public interface IAttack
{
    public void dectectTarget();
    public void atkTarget();
    //public void setData(IData data);
}

public interface ICharacter
{
    //public void setData(IData data);
    public void CreateCharacter();
    public void getStatCharacter();
}

public interface IDamage
{
    public void getHit(float damage);
    public float getHP(float hp);
}


