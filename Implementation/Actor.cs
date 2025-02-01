using Application;

namespace Implementation
{
    public class Actor : IApplicationActor
    {
        public int Id {  get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; }
    }


}
