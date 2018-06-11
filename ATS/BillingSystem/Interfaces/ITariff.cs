namespace BillingSystem.Interfaces
{
    public interface ITariff
    {
        int FreeMinutes { get; }
        double PricePerSecond { get; }
        double CalculateCost(ATSLib.Classes.CallInfo call);
    }
}
