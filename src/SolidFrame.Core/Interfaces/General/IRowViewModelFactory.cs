namespace SolidFrame.Core.Interfaces.General
{
	public interface IRowViewModelFactory<TModel, TRowViewModel>
	{
		TRowViewModel Create(TModel model);
	}
}