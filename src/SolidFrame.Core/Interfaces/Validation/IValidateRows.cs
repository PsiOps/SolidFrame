using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidateRows<T>
	{
		event ValidationTriggerHandler<T> RowValidationTrigger; 
	}
}