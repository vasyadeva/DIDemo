using DIDemo.Interfaces;

namespace DIDemo.Services
{
    public class Service : ISingletonService, ITransientService, IScopedService
    {
        public int number;
        public Service()
        {
            Random random = new Random();
            number = random.Next(1000000);
        }


        public int ShareNumber()
        {
            return number;
        }
    }
}
