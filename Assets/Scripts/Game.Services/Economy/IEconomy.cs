namespace Game.Services.Economy
{
    public interface IEconomy
    {
        int Balance { get; }
        bool TrySpend(int amount);
        void Grant(int amount);
    }
}