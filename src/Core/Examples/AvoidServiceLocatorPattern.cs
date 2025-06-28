using Core.Examples.Interfaces;

namespace Core.Examples
{
    public class AvoidServiceLocatorPattern
    {
        // do not use this pattern
        public AvoidServiceLocatorPattern(IServiceProvider serviceProvider)
        {

        }

        // instead, use dependency injection with specific parameters
        public AvoidServiceLocatorPattern(IDemo demo1, IDemo demo2)
        {
            // use demos here
        }
    }
}