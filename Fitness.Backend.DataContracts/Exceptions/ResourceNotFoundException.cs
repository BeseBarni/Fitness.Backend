namespace Fitness.Backend.Application.DataContracts.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public override string Message => string.Format("The resource - ({0}) - you're trying to access does not exist",Resource);

        public string? Resource { get; set; }

        public ResourceNotFoundException(string resource)
        {
            Resource = resource;
        }
        public ResourceNotFoundException()
        {

        }
    }
}
