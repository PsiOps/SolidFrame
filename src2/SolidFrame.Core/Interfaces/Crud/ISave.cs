using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Crud
{
	public interface ISave
	{
		bool CanSave();

		void Save();

		event CanCrudChangedHandler CanSaveChanged;
	}
}