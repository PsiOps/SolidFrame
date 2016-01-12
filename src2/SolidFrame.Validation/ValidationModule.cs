using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Validation.Logics;
using SolidFrame.Validation.Types;

namespace SolidFrame.Validation
{
	public static class ValidationModule
	{
		public static void BootstrapValidation(this IUnityContainer container)
		{
			container.RegisterType(typeof(IValidationService<>), typeof(ValidationService<>));
			container.RegisterType<IValidationServiceDependencies, ValidationServiceDependencies>();
			container.RegisterType<IValidationRuleFactory, ValidationRuleFactory>();
			container.RegisterType<IConditionEvaluatorFactory, ConditionEvaluatorFactory>();
		}
	}
}
