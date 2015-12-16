using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidate<T> where T : IValidatable
	{
		event ValidationTriggerHandler<T> RowValidationTrigger; 
	}
}