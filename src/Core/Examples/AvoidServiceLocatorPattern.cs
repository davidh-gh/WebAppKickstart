using Core.Examples.Interfaces;

namespace Core.Examples
{
    public class AvoidServiceLocatorPattern
    {
        private readonly IDemo? _demo1;
        private readonly IDemo? _demo2;

        // do not use this pattern
        public AvoidServiceLocatorPattern(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                _demo1 = (IDemo?)serviceProvider.GetService(typeof(IDemo));
                _demo2 = (IDemo?)serviceProvider.GetService(typeof(IDemo));
            }
        }

        // instead, use dependency injection with specific parameters
        public AvoidServiceLocatorPattern(IDemo demo1, IDemo demo2)
        {
            _demo1 = demo1;
            _demo2 = demo2;
            // use demos here
        }

        public void DoSomething()
        {
            // Use _demo1 and _demo2 here
            if (_demo1 != null)
            {
                // Perform actions with _demo1
            }

            if (_demo2 != null)
            {
                // Perform actions with _demo2
            }
        }
    }
}