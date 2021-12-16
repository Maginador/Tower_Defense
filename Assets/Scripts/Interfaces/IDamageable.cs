namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void RecoverHealth(int recover);
        void Die();
    }
}