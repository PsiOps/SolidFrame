using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidate<out T> where T : IValidatable
	{
		event ValidationTriggerHandler<T> ValidationTrigger; 
	}
}