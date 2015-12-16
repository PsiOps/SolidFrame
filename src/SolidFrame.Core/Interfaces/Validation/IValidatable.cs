using SolidFrame.Core.Interfaces.General;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidatable : IHaveId
	{
		string ValidationName { get; }
	}
}