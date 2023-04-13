namespace Fitness.Backend.Application.DataContracts.Interfaces
{
    public interface IDateTrackeable
    {
        DateTime Created { get; set; }
        DateTime LastUpdated { get; set; }

        void Updated() => LastUpdated = DateTime.UtcNow;
    }
}
