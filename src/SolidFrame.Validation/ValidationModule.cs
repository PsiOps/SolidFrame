using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Validation.Logics;
using SolidFrame.Validation.Types;

namespace SolidFrame.Validation
{
	public class ValidationModule : IModule
	{
		private readonly IUnityContainer _container;

		public ValidationModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType(typeof(IValidationService<>), typeof(ValidationService<>));
			_container.RegisterType<IValidationServiceDependencies, ValidationServiceDependencies>();
			_container.RegisterType<IValidationRuleFactory, ValidationRuleFactory>();
			_container.RegisterType<IConditionEvaluatorFactory, ConditionEvaluatorFactory>();
		}
	}
}
