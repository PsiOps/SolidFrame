
using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces
{
	public interface IAdd
	{
		bool CanAdd();

		void Add();

		event CanCrudChangedHandler CanAddChanged;
	}
}