
namespace SolidFrame.Core.Interfaces.General
{
	public interface IRowViewModelFactory<in TModel, out TRowViewModel>
		where TRowViewModel : class, IRowViewModel<TModel>
		where TModel : class, IHaveId

	{
		TRowViewModel Create(TModel model);
	}

	public interface IRowViewModel<out TModel>
	{
		TModel ToModel();
	}
}