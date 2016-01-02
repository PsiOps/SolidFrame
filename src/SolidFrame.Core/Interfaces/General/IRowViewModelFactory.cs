
namespace SolidFrame.Core.Interfaces.General
{
	public interface IRowViewModelFactory<TModel, TRowViewModel>
		where TRowViewModel : class, TModel
		where TModel : class, IHaveId

	{
		TRowViewModel Create(TModel model);
	}
}