
using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Crud
{
	public interface IAdd
	{
		bool CanAdd();

		void Add();

		event CanCrudChangedHandler CanAddChanged;
	}
}